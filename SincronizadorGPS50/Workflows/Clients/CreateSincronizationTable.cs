using System;
using System.Data;
using System.Reflection;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class CreateClientsSincronizationTable
    {
        internal DataTable Table { get; set; }
        public CreateClientsSincronizationTable() 
        {
            Table = new DataTable();
            PropertyInfo[] SincronizationTableProperties = typeof(ClientSyncronizationStateTable).GetProperties();

            for(int i = 0; i < SincronizationTableProperties.Length; i++)
            {
                PropertyInfo property = SincronizationTableProperties[i];
                Table.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            };
        }
    }
}
