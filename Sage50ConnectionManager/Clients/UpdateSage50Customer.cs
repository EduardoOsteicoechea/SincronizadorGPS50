using sage.ew.db;
using System;
using System.Data;
using System.Windows.Forms;

namespace SincronizadorGPS50.Sage50Connector
{
   public class UpdateSage50Customer
   {
      public string ClientCode { get; set; } = "";
      public string GUID_ID { get; set; } = "";
      public UpdateSage50Customer
      (
         string guid_id,
         string country,
         string name,
         string cif,
         string postalCode,
         string address,
         string province,
         string ivaType = "03"
      )
      {
         try
         {
            string getSage50CustomerSQLQuery = $@"
                UPDATE 
                  {DB.SQLDatabase("gestion","clientes")} 
                SET 
                  cif='{cif}',
                  nombre='{name}',
                  direccion='{address}',
                  codpost='{postalCode}',
                  provincia='{province}',
                  pais='{country}'
                WHERE 
                  guid_id='{guid_id}'";

            DataTable sage50CustomersDataTable = new DataTable();

            DB.SQLExec(getSage50CustomerSQLQuery, ref sage50CustomersDataTable);
         }
         catch(System.Exception exception)
         {
            throw new Exception($"En:\n\nSincronizadorGPS50.Sage50Connector\n.UpdateSage50Customer:\n\n{exception.Message}");
         };
      }
   }
}