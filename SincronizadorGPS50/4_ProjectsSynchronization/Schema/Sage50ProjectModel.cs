﻿namespace SincronizadorGPS50
{
   public class Sage50ProjectModel
   {
      public string GUID_ID { get; set; }
      public string CODIGO { get; set; }
      public string NOMBRE { get; set; }
      public string DIRECCION { get; set; }
      public string POBLACION { get; set; }
      public string PROVINCIA { get; set; }
      public string CODPOST { get; set; }
      public string CODIGO_TIPO
      {
         get {
            return CODIGO.Substring(0, 4);
         }
      }
      public int CODIGO_NUMERO
      {
         get {
            return int.Parse(CODIGO.Substring(4));
         }
      }
   }
}