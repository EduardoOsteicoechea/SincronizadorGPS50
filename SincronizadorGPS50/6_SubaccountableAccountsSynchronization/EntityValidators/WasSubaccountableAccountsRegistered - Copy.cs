//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace SincronizadorGPS50
//{
//   internal class WasSubaccountableAccountRegistered
//   {
//      public bool ItWas { get; set; } = false;
//      public WasSubaccountableAccountRegistered
//      (
//         SqlConnection connection,
//         string tableName,
//         string columnName,
//         (string columnName, dynamic value) condition1,
//         (string columnName, dynamic value) condition2 = default
//      )
//      {
//         try
//         {
//            connection.Open();
                 
//            StringBuilder sqlCondition = new StringBuilder();

//            //MessageBox.Show(
//            //   "At: " + MethodBase.GetCurrentMethod().DeclaringType.Name + "." + MethodBase.GetCurrentMethod().Name + "\n" +
//            //   "condition1: " + condition1 + "\n" +
//            //   "condition2: " + condition2 + "\n"
//            //);

//            if(condition1.value != null && condition1.value.GetType() == typeof(string))
//            {
//               if( condition1.value == "" || condition1.value == -1 )
//               {
//                  if(condition2.value != null && condition2.value != -1)
//                  {
//                     //MessageBox.Show("entered\n condition1");
//                     sqlCondition.Append($"{condition2.columnName}={DynamicValuesFormatters.Formatters[condition2.value.GetType()](condition2.value)}");
//                     ExecuteQuery(connection, tableName, columnName, sqlCondition.ToString());          
//                  };
//               }
//               else
//               {
//                     //MessageBox.Show("entered\n condition2");
//                  sqlCondition.Append($"{condition1.columnName}={DynamicValuesFormatters.Formatters[condition1.value.GetType()](condition1.value)}");
//                  ExecuteQuery(connection, tableName, columnName, sqlCondition.ToString());           
//               };
//            }
//            else
//            {
//               if(condition1.value == null || condition1.value == -1)
//               {
//                  if(condition2.value != -1)
//                  {
//                        //MessageBox.Show("entered\n condition3");
//                     sqlCondition.Append($"{condition2.columnName}={DynamicValuesFormatters.Formatters[condition2.value.GetType()](condition2.value)}");
//                     ExecuteQuery(connection, tableName, columnName, sqlCondition.ToString());
//                  }
//               }
//               else
//               {
//                     //MessageBox.Show("entered\n condition4");
//                  sqlCondition.Append($"{condition1.columnName}={DynamicValuesFormatters.Formatters[condition1.value.GetType()](condition1.value)}");
//                  ExecuteQuery(connection, tableName, columnName, sqlCondition.ToString());
//               };
//            };
//         }
//         catch(SqlException exception)
//         {
//            throw ApplicationLogger.ReportError(
//               MethodBase.GetCurrentMethod().DeclaringType.Namespace,
//               MethodBase.GetCurrentMethod().DeclaringType.Name,
//               MethodBase.GetCurrentMethod().Name,
//               exception
//            );
//         }
//         finally
//         {
//            connection.Close();
//         };
//      }

//      public void ExecuteQuery(SqlConnection connection, string tableName, string columnName, string sqlCondition)
//      {         
//         try
//         {
//            string sqlString = $@"
//               SELECT 
//                  {columnName}
//               FROM 
//                  {tableName}
//               WHERE 
//                  {sqlCondition}
//            ";

//            //if(tableName == "INT_SAGE_SYNCHRONIZATION_ENTITY_DATA_SUBACCOUNTABLE_ACCOUNTS")
//            //   MessageBox.Show(sqlString);

//            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
//            {
//               using(SqlDataReader reader = sqlCommand.ExecuteReader())
//               {
//                  while(reader.Read())
//                  {
//                     if(reader.GetValue(0).GetType().Name != "DBNull" || System.Convert.ToString(reader.GetValue(0)) != "")
//                     {
//                        ItWas = true;
//                        break;
//                     }
//                  };
//               };
//            };
//         }
//         catch(SqlException exception)
//         {
//            throw ApplicationLogger.ReportError(
//               MethodBase.GetCurrentMethod().DeclaringType.Namespace,
//               MethodBase.GetCurrentMethod().DeclaringType.Name,
//               MethodBase.GetCurrentMethod().Name,
//               exception
//            );
//         }
//      }
//   }
//}
