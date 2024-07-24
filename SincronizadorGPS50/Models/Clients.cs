using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
    public class GestprojectClient
    {
        public int PAR_ID { get; set; }
        public string PAR_SUBCTA_CONTABLE { get; set; }
        public string PAR_NOMBRE { get; set; }
        public string PAR_NOMBRE_COMERCIAL { get; set; }
        public string PAR_CIF_NIF { get; set; }
        public string PAR_DIRECCION_1 { get; set; }
        public string PAR_CP_1 { get; set; }
        public string PAR_LOCALIDAD_1 { get; set; }
        public string PAR_PROVINCIA_1 { get; set; }
        public string PAR_PAIS_1 { get; set; }
        public string synchronization_status { get; set; } = "";
        public string sage50_client_code { get; set; } = "";
        public string sage50_guid_id { get; set; } = "";
        public string sage50_instance { get; set; } = "";
        public string comments { get; set; } = "";
        public DateTime last_record { get; set; } = DateTime.Now;
        public int synchronization_table_id { get; set; }
    }
    public class Sage50Client
    {
        public string CODIGO { get; set; }
        public string CIF { get; set; }
        public string NOMBRE { get; set; }
        public string NOMBRE2 { get; set; }
        public string DIRECCION { get; set; }
        public string CODPOST { get; set; }
        public string POBLACION { get; set; }
        public string PROVINCIA { get; set; }
        public string PAIS { get; set; }
        public string EMAIL { get; set; }
        public string HTTP { get; set; }
        public string GUID_ID { get; set; }
        public string CODIGO_TIPO
        {
            get
            {
                return CODIGO.Substring(0, 4);
            }
        }
        public int CODIGO_NUMERO
        {
            get
            {
                return int.Parse(CODIGO.Substring(4));
            }
        }
    }
    public class CommonClient
    {
        public int GestprojectId { get; set; }
        public string GestprojectAccountingSubaccount { get; set; }
        public string GestprojectName { get; set; }
        public string GestprojectCommercialName { get; set; }
        public string GestprojectCIFValueOrNIFValue { get; set; }
        public string GestprojectAddress1 { get; set; }
        public string GestprojectPostalCode1 { get; set; }
        public string GestprojectLocation1 { get; set; }
        public string GestprojectProvince1 { get; set; }
        public string GestprojectCountry1 { get; set; }

        public string Sage50Code { get; set; }
        public string Sage50CIF { get; set; }
        public string Sage50Name { get; set; }
        public string Sage50Name2 { get; set; }
        public string Sage50Address { get; set; }
        public string Sage50PostalCode { get; set; }
        public string Sage50CityOrTown { get; set; }
        public string Sage50Province { get; set; }
        public string Sage50Country { get; set; }
        public string Sage50Email { get; set; }
        public string Sage50HTTP { get; set; }
        public string Sage50ClientCodeType
        {
            get
            {
                return Sage50Code.Substring(0, 4);
            }
        }
        public int Sage50ClientCodeNumber
        {
            get
            {
                return int.Parse(Sage50Code.Substring(4));
            }
        }
        public bool IsSincronizable()
        {
            if(GestprojectCIFValueOrNIFValue != "" && Sage50CIF != "" && GestprojectCIFValueOrNIFValue != null && Sage50CIF != null)
            {
                return true;
            }
            else
            {
                return false;
            };
        }
    }
}
