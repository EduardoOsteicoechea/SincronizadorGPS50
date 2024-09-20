using sage.ew.cliente;
using sage.ew.db;
using SincronizadorGPS50.Sage50Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SincronizadorGPS50.Sage50Connector
{
   public class CreateSage50Project
   {
      public string EntityCode { get; set; } = "";
      public string GUID_ID { get; set; } = "";
      public CreateSage50Project
      (
         string country,
         string name,
         string cif,
         string postalCode,
         string address,
         string province,
         string ivaType = "03",
         ((string sageDispactcherMechanismRoute, string tableName) dispatcherAndName, List<(string name, Type type)> tableFieldsAlongTypes) sageTableDataProvider = default
      )
      {
         try
         {
            int nextCodeAvailable = new GetSage50Projects(sageTableDataProvider).NextCodeAvailable;
            //int nextCodeAvailable = new GetSage50Projects().NextCodeAvailable;

            StringBuilder fieldsToQueryStringBuilder = new StringBuilder();
            foreach(var item in sageTableDataProvider.tableFieldsAlongTypes)
            {
               fieldsToQueryStringBuilder.Append($"{item.name},");
            };
            fieldsToQueryStringBuilder.ToString().TrimEnd(',');

            Obra entity = new Obra();

            //if(nextCodeAvailable < 10000)
            //{
            //   if(nextCodeAvailable < 10)
            //   {
            //      entity._Codigo = "4300000" + nextCodeAvailable;
            //      EntityCode = entity._Codigo;
            //   }
            //   else if(nextCodeAvailable < 100)
            //   {
            //      entity._Codigo = "430000" + nextCodeAvailable;
            //      EntityCode = entity._Codigo;
            //   }
            //   else if(nextCodeAvailable < 1000)
            //   {
            //      entity._Codigo = "43000" + nextCodeAvailable;
            //      EntityCode = entity._Codigo;
            //   }
            //   else
            //   {
            //      entity._Codigo = "4300" + nextCodeAvailable;
            //      EntityCode = entity._Codigo;
            //   };


            // Combinar codigo de Gestproject con nombre de proyecto separado por un espacio
            // El código sencillamente consecutivo
            entity._Codigo = nextCodeAvailable++.ToString();
            entity._Nombre = name.Trim();
            entity._Codpost = postalCode.Trim();
            entity._Poblacion = cif.Trim();
            entity._Provincia = address.Trim();

            if(entity._Save())
            {
               string getSage50EntitySQLQuery = $@"
                  SELECT 
                     guid_id 
                  FROM 
                     {DB.SQLDatabase("gestion","proveed")}
                  WHERE 
                     codigo='{EntityCode}'
                  ;";

               DataTable sage50EntityDataTable = new DataTable();

               DB.SQLExec(getSage50EntitySQLQuery, ref sage50EntityDataTable);

               GUID_ID = sage50EntityDataTable.Rows[0].ItemArray[0].ToString().Trim();
            }
            //else
            //{
            //   MessageBox.Show(
            //       "Error en la creación del cliente empleando estos datos: " + "\n\n" +
            //       "EntityCode: " + clsEntityCustomerInstance.codigo + "\n" +
            //       "country: " + clsEntityCustomerInstance.pais + "\n" +
            //       "name: " + clsEntityCustomerInstance.nombre + "\n" +
            //       "postalCode: " + clsEntityCustomerInstance.codpos + "\n" +
            //       "cif: " + clsEntityCustomerInstance.cif + "\n" +
            //       "address: " + clsEntityCustomerInstance.direccion + "\n" +
            //       "province: " + clsEntityCustomerInstance.provincia + "\n" +
            //       "ivaType: " + clsEntityCustomerInstance.tipo_iva + "\n"
            //   );
            //};
         //}
            //else
            //{
            //   MessageBox.Show("Sage50 admite un máximo de 9999 clientes por grupo de empresas y su base de clientes de Gestproject supera éste límite.");
            //};
         }
         catch(System.Exception exception)
         {
            throw ApplicationLogger.ReportError(
               MethodBase.GetCurrentMethod().DeclaringType.Namespace,
               MethodBase.GetCurrentMethod().DeclaringType.Name,
               MethodBase.GetCurrentMethod().Name,
               exception
            );
         };
}
   }
}