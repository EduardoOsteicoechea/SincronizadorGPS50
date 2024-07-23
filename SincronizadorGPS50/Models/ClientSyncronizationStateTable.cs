using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
    internal class ClientSyncronizationStateTable
    {
        public string Estado { get; set; }
        public int Id { get; set; }
        public int Id_Gestproject { get; set; }
        //public string Cuenta_Contable { get; set; }
        public string Subcuenta_Contable { get; set; }
        public string Guid_Sage50 { get; set; }
        public string Nombre { get; set; }
        public string Nombre_Comercial { get; set; }
        public string CIF_NIF { get; set; }
        public string Direccion { get; set; }
        public string Codigo_Postal { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
        public string Sage50LocalInstance { get; set; }
        public string Comentarios { get; set; }
    }
}
