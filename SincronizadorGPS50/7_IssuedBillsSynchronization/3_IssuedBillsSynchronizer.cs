using sage.ew.docventatpv;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   public class IssuedBillsSynchronizer : IEntitySynchronizer<GestprojectIssuedBillModel, Sage50IssuedBillModel>
   {
      public IGestprojectConnectionManager GestprojectConnectionManager { get; set; }
      public SqlConnection Connection { get; set; }
      public ISage50ConnectionManager SageConnectionManager { get; set; }
      public ISynchronizationTableSchemaProvider TableSchema { get; set; }
      public ewDocVentaTPV Document {get;set;}
      public ewDocVentaLinTPV DetailManager {get;set;}

      public void Synchronize
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider tableSchema,
         List<int> selectedIdList
      )
      {
         try
         {
            GestprojectConnectionManager = gestprojectConnectionManager;
            Connection = GestprojectConnectionManager.GestprojectSqlConnection;
            SageConnectionManager = sage50ConnectionManager;
            TableSchema = tableSchema;

            CreateDocument();

            // GetGestprojectIssuedInvoices
               //GetParticipan
               //GetPaymentMethod
               //GetCompany
            // AppendInvoicesDetailsToInvoices
               // Get details by FCP_ID
               // Get details account
               // Get details IvaType
               // Get details Definition
               // Get details Units
               // Get details Price
            // CreateSalesAlbaranInSage

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

      public void CreateDocument()
      {
         try
         {
            Document = new ewDocVentaTPV();
            Document._Cabecera._Cliente = "43000002";
            Document._Cabecera._FormaPago = "01";
            Document._New("01", "", "");

            DetailManager = Document._AddLinea();
            DetailManager._Cuenta = "70000001";
            DetailManager._TipoIva = "11";
            DetailManager._Definicion = "linea1";
            DetailManager._Unidades = 2;
            DetailManager._Precio = 2500;
            DetailManager._Recalcular_Importe();

            if(DetailManager._Save() == false) throw new Exception("Error: We couldn't register the detail " + DetailManager._Definicion);

            DetailManager = Document._AddLinea();
            DetailManager._Cuenta = "64000000";
            DetailManager._TipoIva = "11";
            DetailManager._Definicion = "linea2";
            DetailManager._Unidades = 2;
            DetailManager._Precio = 500;
            DetailManager._Recalcular_Importe();

            if(DetailManager._Save() == false) throw new Exception("Error: We couldn't register the detail " + DetailManager._Definicion);

            Document._Totalizar();
            
            if(Document._Save() == false) throw new Exception("Error: We couldn't register the document");
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
