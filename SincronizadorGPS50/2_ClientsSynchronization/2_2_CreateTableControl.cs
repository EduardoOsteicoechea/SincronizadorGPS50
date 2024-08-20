using SincronizadorGPS50.GestprojectDataManager;
using System.Data;
using System.Reflection;

namespace SincronizadorGPS50
{
   internal class CreateTableControl
   {
      internal DataTable Table { get; set; }
      public CreateTableControl()
      {
         try
         {
            Table = new DataTable();
            PropertyInfo[] SincronizationTableProperties = typeof(ClientSynchronizationTableSchema).GetProperties();

            // 0. SynchronizationTableClientIdColumn
            Table.Columns.Add(
               ClientSynchronizationTableSchema.SynchronizationTableClientIdColumn.ColumnUserFriendlyNane,
               ClientSynchronizationTableSchema.SynchronizationTableClientIdColumn.ColumnValueType
            );
            // 1. SynchronizationStatusColumn
            Table.Columns.Add(
               ClientSynchronizationTableSchema.SynchronizationStatusColumn.ColumnUserFriendlyNane,
               ClientSynchronizationTableSchema.SynchronizationStatusColumn.ColumnValueType
            );
            // 2. GestprojectClientIdColumn
            Table.Columns.Add(
               ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnUserFriendlyNane,
               ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnValueType
            );

            // 3. GestprojectClientAccountableSubaccountColumn
            Table.Columns.Add(
                ClientSynchronizationTableSchema.GestprojectClientAccountableSubaccountColumn.ColumnUserFriendlyNane,
                ClientSynchronizationTableSchema.GestprojectClientAccountableSubaccountColumn.ColumnValueType
            );
            // 4. GestprojectClientNameColumn
            Table.Columns.Add(
                ClientSynchronizationTableSchema.GestprojectClientNameColumn.ColumnUserFriendlyNane,
                ClientSynchronizationTableSchema.GestprojectClientNameColumn.ColumnValueType
            );
            // 5. GestprojectClientCommercialNameColumn
            Table.Columns.Add(
                ClientSynchronizationTableSchema.GestprojectClientCommercialNameColumn.ColumnUserFriendlyNane,
                ClientSynchronizationTableSchema.GestprojectClientCommercialNameColumn.ColumnValueType
            );
            // 6. GestprojectClientCIFNIFColumn
            Table.Columns.Add(
                ClientSynchronizationTableSchema.GestprojectClientCIFNIFColumn.ColumnUserFriendlyNane,
                ClientSynchronizationTableSchema.GestprojectClientCIFNIFColumn.ColumnValueType
            );
            // 7. GestprojectClientAddressColumn
            Table.Columns.Add(
                ClientSynchronizationTableSchema.GestprojectClientAddressColumn.ColumnUserFriendlyNane,
                ClientSynchronizationTableSchema.GestprojectClientAddressColumn.ColumnValueType
            );
            // 8. GestprojectClientPostalCodeColumn
            Table.Columns.Add(
                ClientSynchronizationTableSchema.GestprojectClientPostalCodeColumn.ColumnUserFriendlyNane,
                ClientSynchronizationTableSchema.GestprojectClientPostalCodeColumn.ColumnValueType
            );
            // 9. GestprojectClientLocalityColumn
            Table.Columns.Add(
                ClientSynchronizationTableSchema.GestprojectClientLocalityColumn.ColumnUserFriendlyNane,
                ClientSynchronizationTableSchema.GestprojectClientLocalityColumn.ColumnValueType
            );
            // 10. GestprojectClientProvinceColumn
            Table.Columns.Add(
                ClientSynchronizationTableSchema.GestprojectClientProvinceColumn.ColumnUserFriendlyNane,
                ClientSynchronizationTableSchema.GestprojectClientProvinceColumn.ColumnValueType
            );
            // 11. GestprojectClientCountryColumn
            Table.Columns.Add(
                ClientSynchronizationTableSchema.GestprojectClientCountryColumn.ColumnUserFriendlyNane,
                ClientSynchronizationTableSchema.GestprojectClientCountryColumn.ColumnValueType
            );

            // 12. Sage50ClientCodeColumn
            Table.Columns.Add(
               ClientSynchronizationTableSchema.Sage50ClientCodeColumn.ColumnUserFriendlyNane,
               ClientSynchronizationTableSchema.Sage50ClientCodeColumn.ColumnValueType
            );
            // 13. Sage50ClientGuidIdColumn
            Table.Columns.Add(
               ClientSynchronizationTableSchema.Sage50ClientGuidIdColumn.ColumnUserFriendlyNane,
               ClientSynchronizationTableSchema.Sage50ClientGuidIdColumn.ColumnValueType
            );
            // 14. Sage50ClientCompanyGroupNameColumn
            Table.Columns.Add(
               ClientSynchronizationTableSchema.Sage50ClientCompanyGroupNameColumn.ColumnUserFriendlyNane,
               ClientSynchronizationTableSchema.Sage50ClientCompanyGroupNameColumn.ColumnValueType
            );
            // 15. Sage50ClientCompanyGroupCodeColumn
            Table.Columns.Add(
               ClientSynchronizationTableSchema.Sage50ClientCompanyGroupCodeColumn.ColumnUserFriendlyNane,
               ClientSynchronizationTableSchema.Sage50ClientCompanyGroupCodeColumn.ColumnValueType
            );
            // 16. Sage50ClientCompanyGroupMainCodeColumn
            Table.Columns.Add(
               ClientSynchronizationTableSchema.Sage50ClientCompanyGroupMainCodeColumn.ColumnUserFriendlyNane,
               ClientSynchronizationTableSchema.Sage50ClientCompanyGroupMainCodeColumn.ColumnValueType
            );
            // 17. Sage50ClientCompanyGroupGuidIdColumn
            Table.Columns.Add(
               ClientSynchronizationTableSchema.Sage50ClientCompanyGroupGuidIdColumn.ColumnUserFriendlyNane,
               ClientSynchronizationTableSchema.Sage50ClientCompanyGroupGuidIdColumn.ColumnValueType
            );
            // 18. ClientLastUpdateTerminalColumn
            Table.Columns.Add(
               ClientSynchronizationTableSchema.ClientLastUpdateTerminalColumn.ColumnUserFriendlyNane,
               ClientSynchronizationTableSchema.ClientLastUpdateTerminalColumn.ColumnValueType
            );
            // 19. ParentUserIdColumn
            Table.Columns.Add(
               ClientSynchronizationTableSchema.GestprojectClientParentUserIdColumn.ColumnUserFriendlyNane,
               ClientSynchronizationTableSchema.GestprojectClientParentUserIdColumn.ColumnValueType
            );
            // 20. CommentsColumn
            Table.Columns.Add(
               ClientSynchronizationTableSchema.CommentsColumn.ColumnUserFriendlyNane,
               ClientSynchronizationTableSchema.CommentsColumn.ColumnValueType
            );

            for(int i = 0; i < Table.Columns.Count; i++)
            {
               if(i >= Table.Columns.Count - 1)
               {
                  Table.Columns[i].MaxLength = 300;
               };
            };
         }
         catch (System.Exception exception)
         {
            throw new System.Exception(
               $"At:\n\nSincronizadorGPS50.GestprojectDataManager\n.CreateTableControl:\n\n{exception.Message}"
            );
         };
      }
   }
}
