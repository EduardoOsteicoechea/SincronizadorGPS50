using System.Data;
using System.Reflection;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class CreateTableControl
    {
        internal DataTable Table { get; set; }
        public CreateTableControl() 
        {
            Table = new DataTable();
            PropertyInfo[] SincronizationTableProperties = typeof(ClientsSynchronizationTable).GetProperties();

            for(int i = 0; i < SincronizationTableProperties.Length; i++)
            {
                if(i > SincronizationTableProperties.Length-1)
                {
                    Table.Columns[i].MaxLength = 200;
                };
                PropertyInfo property = SincronizationTableProperties[i];
                Table.Columns.Add(property.Name, System.Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            };
        }
    }
}
