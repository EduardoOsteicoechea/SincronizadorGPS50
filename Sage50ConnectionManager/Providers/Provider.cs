﻿using System;
using System.Collections.Generic;
using System.Data;
using sage.ew.db;
using sage.ew.cliente;
using sage.ew.cliente.Clases;
using sage.ew.global;
using sage.ew.global.Diccionarios;
using sage.ew.functions;
using SincronizadorGPS50;
using SincronizadorGPS50.Sage50Connector;
using System.Windows.Forms;
using sage.ew.docscompra;

namespace SincronizadorGPS50.Sage50Connector
{
   public class Provider : BaseMaster
   {
      #region propiedades

      private LinkFuncSage50 _oLinkFuncs = new LinkFuncSage50();
      //private Cliente _oProveedor = null;
      private sage.ew.docscompra.Proveedor _oProveedor = null;
      private int _nDigitos = Convert.ToInt32(EW_GLOBAL._GetLenCampo(KeyDiccionarioLenCampos.wn_digitos));

      #endregion propiedades

      #region constructor
      public Provider()
      {
         psDb = "gestion";
         psTable = "proveed";
      }
      #endregion constructor

      public override bool _Load(ref dynamic toeCustomer)
      {
         bool llOk = false;

         if(toeCustomer != null)
         {
            if(!string.IsNullOrWhiteSpace(toeCustomer.codigo) && toeCustomer.codigo.Trim().Length == _nDigitos)
            {
               if(FUNCTIONS._Es_Proveedor(toeCustomer.codigo))
               {
                  //_oProveedor = new Cliente();
                  _oProveedor = new Proveedor();
                  _oProveedor._Codigo = toeCustomer.codigo;

                  toeCustomer.nombre = _oProveedor._Nombre;
                  //toeCustomer.razoncomercial = _oProveedor._RazonComercial;
                  toeCustomer.pais = _oProveedor._Pais;
                  //toeCustomer.cif = _oProveedor._NIF;
                  toeCustomer.direccion = _oProveedor._Direccion;
                  toeCustomer.poblacion = _oProveedor._Poblacion;
                  toeCustomer.provincia = _oProveedor._Provincia;
                  //toeCustomer.telefono = _oProveedor._Telefono;
                  //toeCustomer.recargo = _oProveedor._Recargo;
                  toeCustomer.codpos = _oProveedor._CodPost;
                  //toeCustomer.contado = _oProveedor._ClienteContado;
                  toeCustomer.tipo_iva = _oProveedor._TipoIVA;
                  //toeCustomer.tipo_ret = _oProveedor._RetencionTipo;
                  //toeCustomer.modoret = (_oProveedor._RetencionBaseFactura == Cliente.TipoRetencion.SobreFactura) ? 2 : 1;
                  toeCustomer.existeregistro = _oProveedor._Existe_Registro();

                  llOk = true;
               }
               else
               { this._Error_Message += _oProveedor._Error_Message + "\r\n"; }
            }
            else
            { this._Error_Message += "No se a indicado el códgo del cliente o la longitud del codigo es diferente a " + _nDigitos + " digitos \r\n"; }


         }

         return llOk;
      }


      /// <summary>
      /// Método para crear un cliente
      /// Usa como parametro la entidad de clsEntityCustomer
      /// </summary>
      /// <param name="toeCustomer"></param>
      /// <returns></returns>
      public override bool _Create(dynamic toeCustomer)
      {
         bool llOk = false;
         string lsPais = string.Empty, lsCodPos = string.Empty, lsTipoIva = string.Empty, lsTipoRet = string.Empty;
         this._Error_Message = string.Empty;

         if(toeCustomer != null)
         {
            if(!string.IsNullOrWhiteSpace(toeCustomer.codigo) && !string.IsNullOrWhiteSpace(toeCustomer.nombre) && toeCustomer.codigo.Trim().Length == _nDigitos)
            {
               // comprobamos que el codigo ingresado sea del tipo valido para cliente.
               if(FUNCTIONS._Es_Proveedor(toeCustomer.codigo))
               {
                  //_oProveedor = new Cliente();
                  _oProveedor = new Proveedor();
                  _oProveedor._Codigo = toeCustomer.codigo;

                  if(!_oProveedor._Existe_Registro())
                  {
                     _oProveedor._New(toeCustomer.codigo);

                     if(Convert.ToBoolean(toeCustomer.contado) == true)
                     {
                        // si es una cuenta de cliente contado (creamos la cuenta con valores fijos.. previsionalmente)
                        //_oProveedor._ClienteContado = toeCustomer.contado;
                        //_oProveedor._Nombre = "CLIENTE CONTADO";
                        _oProveedor._Nombre = "PROVEEDOR CONTADO";
                        _oProveedor._Pais = _oLinkFuncs._CountryCompany();
                        _oProveedor._NIF = "";
                     }
                     else
                     {
                        _oProveedor._Nombre = toeCustomer.nombre;

                        // Tratamiento de codigo de país
                        _ConvertData("pais", toeCustomer, ref _oProveedor);

                        if(!string.IsNullOrWhiteSpace(toeCustomer.cif))
                           _oProveedor._NIF = toeCustomer.cif;
                        if(!string.IsNullOrWhiteSpace(toeCustomer.fpago))
                           _oProveedor._FormaPago = toeCustomer.fpago;
                     }

                     _oProveedor._RazonComercial = toeCustomer.razoncomercial;
                     _oProveedor._Direccion = toeCustomer.direccion;
                     _oProveedor._Poblacion = toeCustomer.poblacion;
                     _oProveedor._Provincia = toeCustomer.provincia;
                     _oProveedor._Telefono = toeCustomer.telefono;
                     _oProveedor._Recargo = toeCustomer.recargo;

                     // Tratamiento códigos postales
                     _ConvertData("codpos", toeCustomer, ref _oProveedor);

                     // Tratamiento tipo Iva
                     _ConvertData("tipo_iva", toeCustomer, ref _oProveedor);

                     //Tratamiento tipo Retencion
                     _ConvertData("tipo_ret", toeCustomer, ref _oProveedor);

                     //Tratamiento IBAN
                     _ConvertData("iban", toeCustomer, ref _oProveedor);

                     // Grabamos el cliente
                     llOk = _oProveedor._Save();
                     if(llOk)
                     {
                        // Si le enviamos la coleccion del mandato del cliente, la agregamos
                        if(toeCustomer.mandato != null)
                        {
                           if(string.IsNullOrEmpty(toeCustomer.cliente))
                              toeCustomer.cliente = toeCustomer.codigo;

                           this._MandateCustomer(toeCustomer.mandato);
                           //    clsEntityMandate loMandato = new clsEntityMandate();
                           //    loMandato = (clsEntityMandate)toeCustomer.mandato;
                           //    loMandato.cliente = _oProveedor._Codigo;
                           //    loMandato = null;
                        }
                     }
                     else
                     { this._Error_Message += _oProveedor._Error_Message + "\r\n"; }
                  }
                  else
                  { this._Error_Message += "El código de cliente ya existe\r\n"; }
               }
            }
            _oProveedor = null;
         }

         return llOk;

      }

      public override bool _Update(dynamic toeCustomer)
      {
         bool llOk = false;
         this._Error_Message = string.Empty;

         if(toeCustomer != null)
         {
            //_oProveedor = new Cliente();
            _oProveedor = new Proveedor();
            _oProveedor._Codigo = toeCustomer.codigo;
            _oProveedor._Nombre = toeCustomer.nombre;

            //_oProveedor._ClienteContado = toeCustomer.contado;

            _oProveedor._NIF = toeCustomer.cif;
            _oProveedor._FormaPago = toeCustomer.fpago;

            _oProveedor._RazonComercial = toeCustomer.razoncomercial;
            _oProveedor._Direccion = toeCustomer.direccion;
            _oProveedor._Poblacion = toeCustomer.poblacion;
            _oProveedor._Provincia = toeCustomer.provincia;
            _oProveedor._Telefono = toeCustomer.telefono;
            _oProveedor._Recargo = toeCustomer.recargo;

            // Tratamiento códigos postales
            _ConvertData("codpos", toeCustomer, ref _oProveedor);

            // Tratamiento tipo Iva
            _ConvertData("tipo_iva", toeCustomer, ref _oProveedor);

            //Tratamiento tipo Retencion
            _ConvertData("tipo_ret", toeCustomer, ref _oProveedor);

            //Tratamiento IBAN
            _ConvertData("iban", toeCustomer, ref _oProveedor);

            // Grabamos el cliente
            llOk = _oProveedor._Save();
            if(llOk)
            {
               // Si le enviamos la coleccion del mandato del cliente, la agregamos
               if(toeCustomer.mandato != null)
               {
                  if(string.IsNullOrEmpty(toeCustomer.cliente))
                     toeCustomer.cliente = toeCustomer.codigo;

                  this._MandateCustomer(toeCustomer.mandato);
                  //    clsEntityMandate loMandato = new clsEntityMandate();
                  //    loMandato = (clsEntityMandate)toeCustomer.mandato;
                  //    loMandato.cliente = _oProveedor._Codigo;
                  //    loMandato = null;
               }
            }
            else
            { this._Error_Message += _oProveedor._Error_Message + "\r\n"; }
         }

         return llOk;
      }

      public override bool _Delete(dynamic toeCustomer)
      {
         bool llOk = false;
         this._Error_Message = string.Empty;

         if(toeCustomer != null)
         {
            _oProveedor = new Proveedor(toeCustomer.codigo);
            //_oProveedor = new Cliente(toeCustomer.codigo);

            llOk = _oProveedor._Delete();

            if(!llOk)
               this._Error_Message += _oProveedor._Error_Message + "\r\n";
         }

         return llOk;
      }

      public bool _MandateCustomer(clsEntityMandate toMandate)
      {
         bool llOk = false;
         string lsSQL = string.Empty;
         DataTable loDatosCliente = new DataTable();

         Dictionary<string, object> _Filtro = new Dictionary<string, object>();
         _Filtro.Add("mandato", toMandate.numero);

         _Vista loVista = new _Vista("COMUNES", "MANDATOS");

         loVista._Requery(_Filtro);

         if(loVista._Reccount == 0)
         {
            // Insert
            lsSQL = "Select a.nombre, a.cif, a.direccion, a.poblacion, a.pais, a.codpost as codpos, b.codigo as banc_cli, b.iban, b.cuentaiban, b.swift " +
                    "From {0} a Left Join {1} b On a.codigo = b.cliente and orden = 1 " +
                    "Where a.codigo = {2} ";
            lsSQL = string.Format(lsSQL, DB.SQLDatabase("CLIENTES"), DB.SQLDatabase("BANC_CLI"), toMandate.cliente);

            DB.SQLExec(lsSQL, ref loDatosCliente);


            loVista._AppendBlank();
            loVista._CurrentRow["cliente"] = toMandate.cliente;
            loVista._CurrentRow["linia"] = 1;
            loVista._CurrentRow["mandato"] = toMandate.numero;
            loVista._CurrentRow["fecha_fin"] = toMandate.fecha_fin;
            loVista._CurrentRow["fecha_fir"] = toMandate.fecha_fir;
            loVista._CurrentRow["defecto"] = toMandate.defecto;
            loVista._CurrentRow["tipo"] = (toMandate.tipo == 0 ? 1 : toMandate.tipo);
            loVista._CurrentRow["tipo_pago"] = (toMandate.tipo_pago == 0 ? 1 : toMandate.tipo_pago);

            // Datos cliente
            loVista._CurrentRow["cli_nomb"] = loDatosCliente.Rows[0]["nombre"].ToString();
            loVista._CurrentRow["cli_direc"] = loDatosCliente.Rows[0]["direccion"].ToString();
            loVista._CurrentRow["cli_nif"] = loDatosCliente.Rows[0]["cif"].ToString();

            loVista._CurrentRow["cli_pais"] = loDatosCliente.Rows[0]["pais"].ToString();
            loVista._CurrentRow["cli_codpos"] = loDatosCliente.Rows[0]["codpos"].ToString();
            loVista._CurrentRow["cli_iban"] = loDatosCliente.Rows[0]["iban"].ToString().Trim() + loDatosCliente.Rows[0]["cuentaiban"].ToString().Trim();
            loVista._CurrentRow["cli_bic"] = loDatosCliente.Rows[0]["swift"];

            loVista._CurrentRow["mandcont"] = 1;
            loVista._CurrentRow["numefe"] = toMandate.numefe;
            loVista._CurrentRow["numefpro"] = toMandate.numefpro;
            loVista._CurrentRow["estpro"] = false;
            loVista._CurrentRow["banc_cli"] = loDatosCliente.Rows[0]["banc_cli"].ToString();

            //// Datos empresa
            //lcPais = Funcs.paisempresa();
            //loVista._CurrentRow["emprcif"] = CodAcreedorSepaEmpresa(lcPais);
            //loVista._CurrentRow["emprnom"] = EW_GLOBAL._GetVariable("wc_empnombre1").ToString().Trim();
            //loVista._CurrentRow["emprnom2"] = EW_GLOBAL._GetVariable("wc_empnombre2").ToString().Trim();
            //loVista._CurrentRow["emprdirec"] = EW_GLOBAL._GetVariable("wc_empdireccion").ToString().Trim();
            //loVista._CurrentRow["emprcodpos"] = EW_GLOBAL._GetVariable("wc_empcodpos").ToString().Trim();
            //loVista._CurrentRow["emprpob"] = EW_GLOBAL._GetVariable("wc_emppoblacion").ToString().Trim();
            //loVista._CurrentRow["emprprov"] = EW_GLOBAL._GetVariable("wc_empprovincia").ToString().Trim();
            //loVista._CurrentRow["emprpais"] = lcPais;

            loVista._CurrentRow["guid_id"] = Guid.NewGuid().ToString().ToUpper();

            loDatosCliente.Dispose();
            loDatosCliente = null;

         }
         else
         {
            // Update
            loVista._CurrentRow["fecha_fir"] = toMandate.fecha_fir;
            loVista._CurrentRow["fecha_fin"] = toMandate.fecha_fin;
            loVista._CurrentRow["numefe"] = toMandate.numefe;
            loVista._CurrentRow["numefpro"] = toMandate.numefpro;
            loVista._CurrentRow["tipo"] = (toMandate.tipo == 0 ? 1 : toMandate.tipo);
            loVista._CurrentRow["tipo_pago"] = (toMandate.tipo_pago == 0 ? 1 : toMandate.tipo_pago);
         }

         llOk = loVista._TableUpdate();

         loVista = null;

         return llOk;
      }


      public decimal _Obtener_LimiteCredito(string tsCliente)
      {
         decimal lnTotal = 0M;

         if(!string.IsNullOrEmpty(tsCliente.Trim()))
         {
            _oProveedor = new Proveedor(tsCliente);
            //_oProveedor = new Cliente(tsCliente);
            //clsLimiteCredito loLimiteCredito = _oProveedor._LimiteCredito;
            //// posiblemente se tenga que recargar
            //loLimiteCredito._Load();


            //// decimal lnPedidos1 = loLimiteCredito._TotalPedidosPendiente;
            //// decimal lnAlba1 = loLimiteCredito._TotalAlbaranesPendiente;
            //// decimal lnPrevisions1 = loLimiteCredito._TotalPrevisiones;
            //// decimal lnRemesas1 = loLimiteCredito._TotalRemesas;
            //// decimal lnNegociacion1 = loLimiteCredito._TotalFacturasNegociacionCobros;
            //// decimal lnImpagos = loLimiteCredito._NumImpagos; 
            //// decimal lnTotalImpagos1 = loLimiteCredito._TotalImpagados;
            //// decimal lnTotalEfectos = loLimiteCredito._TotalEfectos;
            ////decimal lnTotal1 = loLimiteCredito._Total;
            //loLimiteCredito._Calcular();

            //lnTotal = _oProveedor.LimiteCredito() - loLimiteCredito._Total;

         }

         return lnTotal;
      }

      public bool _Excedido_LimiteCredito(string tsCliente)
      {
         bool llExcedido = false;
         decimal lnCredito = 0M;
         decimal lnTotal = 0M;

         if(!string.IsNullOrEmpty(tsCliente.Trim()))
         {
            //_oProveedor = new Cliente(tsCliente);
            _oProveedor = new Proveedor(tsCliente);
            //lnCredito = _oProveedor._Credito;
            //if(lnCredito > 0)
            //{

            //   clsLimiteCredito loLimiteCredito = _oProveedor._LimiteCredito;
            //   // posiblemente se tenga que recargar
            //   loLimiteCredito._Load();

            //   //lnTotal = loCliente.LimiteCredito();
            //   lnTotal = loLimiteCredito._Total;

            //   llExcedido = lnTotal > lnCredito ? true : false;
            //}
         }

         return llExcedido;
      }

      //private void _ConvertData(string tsTipo, dynamic toeCustomer, ref Cliente toCliente)
      private void _ConvertData(string tsTipo, dynamic toeCustomer, ref Proveedor toCliente)
      {
         switch(tsTipo.ToLower())
         {
            case "pais":
               string lsPais = _oLinkFuncs._VerificateCountry(toeCustomer.pais);
               //string lsPais = toeCustomer.pais;
               if(!string.IsNullOrWhiteSpace(lsPais))
                  toCliente._Pais = lsPais;
               else
                  this._Error_Message += string.Format("¡El pais {0} no existe!", toeCustomer.pais) + "\r\n";
               break;

            case "codpos":
               if(!string.IsNullOrWhiteSpace(toeCustomer.codpos))
               {
                  //string lsCodPos = _oLinkFuncs._VerificatePostalCode(toeCustomer.codpos);
                  string lsCodPos = toeCustomer.codpos;
                  if(!string.IsNullOrWhiteSpace(lsCodPos))
                     toCliente._CodPost = lsCodPos;
                  else
                     this._Error_Message += string.Format("¡El código postal {0} no existe!", toeCustomer.codpos) + "\r\n";
               }
               break;

            case "tipo_iva":
               if(!string.IsNullOrWhiteSpace(toeCustomer.tipo_iva))
               {
                  //string lsTipoIva = _oLinkFuncs._VerificateTaxType(toeCustomer.tipo_iva);
                  string lsTipoIva = toeCustomer.tipo_iva;
                  if(!string.IsNullOrWhiteSpace(lsTipoIva))
                     toCliente._TipoIVA = lsTipoIva;
                  else
                     this._Error_Message += string.Format("¡El tipo de iva {0} no existe!", toeCustomer.tipo_iva) + "\r\n";
               }
               break;

            case "tipo_ret":
               if(!string.IsNullOrWhiteSpace(toeCustomer.tipo_ret))
               {
                  string lsTipoRet = _oLinkFuncs._VerificateRetentionType(toeCustomer.tipo_ret);
                  if(!string.IsNullOrWhiteSpace(lsTipoRet))
                  {
                     toCliente._RetencionTipo = lsTipoRet;

                     if(toeCustomer.modoret == 2)
                        toCliente._RetencionBaseFactura = Proveedor.TipoRetencion.SobreFactura;
                        //toCliente._RetencionBaseFactura = Cliente.TipoRetencion.SobreFactura;
                     else
                        toCliente._RetencionBaseFactura = Proveedor.TipoRetencion.SobreBase;
                        //toCliente._RetencionBaseFactura = Cliente.TipoRetencion.SobreBase;

                     toCliente._RetencionFiscal = true;
                  }
                  else
                     this._Error_Message += string.Format("¡El tipo de retención {0} no existe!", toeCustomer.tipo_ret) + "\r\n";
               }
               break;

            case "iban":
               if(!string.IsNullOrWhiteSpace(toeCustomer.iban))
               {
                  clsBankAccount loCtaBanco = new clsBankAccount();

                  loCtaBanco.IBAN = toeCustomer.iban;
                  toCliente._BancoPredet_TipoCta = loCtaBanco.tipocta;
                  toCliente._BancoPredet_Nombre = (string.IsNullOrWhiteSpace(toeCustomer.nombrebanco) ? loCtaBanco.nombre : toeCustomer.nombrebanco);
                  toCliente._BancoPredet_Iban = loCtaBanco.iban;
                  toCliente._BancoPredet_CuentaIban = loCtaBanco.cuentaiban;

                  toCliente._BancoPredet_Codban = loCtaBanco.codban;
                  toCliente._BancoPredet_Succur = loCtaBanco.succur;
                  toCliente._BancoPredet_Digcon = loCtaBanco.digcon;
                  toCliente._BancoPredet_CtaCuenta = loCtaBanco.ctacuenta;

                  toCliente._BancoPredet_Swift = toeCustomer.swift;

                  loCtaBanco = null;
               }
               break;


            default:
               break;
         }
      }
   }
}