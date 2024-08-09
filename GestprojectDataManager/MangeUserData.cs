using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GestprojectDataManager
{
    public static class ManageUserData
    {
        public static string GestprojectSynchronizatorUserDataTableName { get; set; } = "INT_SAGE_USERDATA";
        public static string GestprojectSynchronizatorUserDataIdFieldName { get; set; } = "ID";
        public static string GestprojectSynchronizatorUserDataTerminalFieldName { get; set; } = "SAGE_50_LOCAL_TERMINAL_PATH";
        public static string GestprojectSynchronizatorUserDataUsernameFieldName { get; set; } = "SAGE_50_USER_NAME";
        public static string GestprojectSynchronizatorUserDataPasswordFieldName { get; set; } = "SAGE_50_PASSWORD";
        public static string GestprojectSynchronizatorUserDataCompanyGroupFieldName { get; set; } = "SAGE_50_COMPANY_GROUP_NAME";
        public static string GestprojectSynchronizatorUserDataRememberFieldName { get; set; } = "REMEMBER";
        public static string GestprojectSynchronizatorUserDataLastUpdateFieldName { get; set; } = "LAST_UPDATE";
        public static bool Save
        (
            System.Data.SqlClient.SqlConnection connection,
            string sage50LocalTerminalPath,
            string sage50Username,
            string sage50Password,
            string enterpryseGroupName
        )
        {
            try
            {
                if(CheckIfGestprojectUserDataTableExists(connection))
                {
                    if(!CkeckIfGestprojectUserDataTableIsEmpty(connection))
                    {
                        UpdateGestprojectUserDataTable(
                            connection,
                            sage50LocalTerminalPath,
                            sage50Username,
                            sage50Password,
                            enterpryseGroupName
                        );
                        return true;
                    }
                    else
                    {
                        PopulateGestprojectUserDataTable(
                            connection,
                            sage50LocalTerminalPath,
                            sage50Username,
                            sage50Password,
                            enterpryseGroupName
                        );
                        return true;
                    };
                }
                else 
                {
                    CreateGestprojectUserDataTable(connection);
                    PopulateGestprojectUserDataTable(
                        connection,
                        sage50LocalTerminalPath,
                        sage50Username,
                        sage50Password,
                        enterpryseGroupName
                    );
                    return true;
                };
            }
            catch(SqlException ex)
            {
                MessageBox.Show($"Error: \n\n{ex.Message}");
                return false;
            };
        }

        public static bool CheckIfGestprojectUserDataTableExists(System.Data.SqlClient.SqlConnection connection)
        {
            try
            {
                connection.Open();

                string sqlString = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE \"TABLE_NAME\" = '{GestprojectSynchronizatorUserDataTableName}'";

                using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
                {
                    int? Sage50SincronizationTableCount = (int)sqlCommand.ExecuteScalar();
                    if(Sage50SincronizationTableCount != null)
                    {
                        return Sage50SincronizationTableCount > 0;
                    }
                    else
                    {
                        return false;
                    };
                };
            }
            catch(SqlException ex)
            {
                MessageBox.Show($"Error: \n\n{ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            };
        }

        public static bool CreateGestprojectUserDataTable(System.Data.SqlClient.SqlConnection connection)
        {
            try
            {
                connection.Open();
                
                string sqlString = $@"
                    CREATE TABLE 
                        {GestprojectSynchronizatorUserDataTableName} 
                        (
                            {GestprojectSynchronizatorUserDataIdFieldName} INT PRIMARY KEY IDENTITY(1,1), 
                            {GestprojectSynchronizatorUserDataTerminalFieldName} VARCHAR(MAX),
                            {GestprojectSynchronizatorUserDataUsernameFieldName} VARCHAR(MAX), 
                            {GestprojectSynchronizatorUserDataPasswordFieldName} VARCHAR(MAX), 
                            {GestprojectSynchronizatorUserDataCompanyGroupFieldName} VARCHAR(MAX),
                            {GestprojectSynchronizatorUserDataRememberFieldName} BIT,
                            {GestprojectSynchronizatorUserDataLastUpdateFieldName} DATETIME DEFAULT GETDATE() NOT NULL
                        )
                    ;";

                using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
                {
                    sqlCommand.ExecuteNonQuery();
                };

                return true;
            }
            catch(SqlException ex)
            {
                MessageBox.Show($"Error: \n\n{ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            };
        }

        public static bool CkeckIfGestprojectUserDataTableIsEmpty(System.Data.SqlClient.SqlConnection connection)
        {
            try
            {
                connection.Open();

                string sqlString = $@"
                    IF EXISTS 
                        (SELECT 1 FROM {GestprojectSynchronizatorUserDataTableName}) 
                        SELECT 1 
                    ELSE 
                        SELECT 0
                ";

                using(SqlCommand command = new SqlCommand(sqlString, connection))
                {
                    int result = (int)command.ExecuteScalar();
                    return result == 0;
                };
            }
            catch(SqlException ex)
            {
                MessageBox.Show($"Error: \n\n{ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            };
        }

        public static bool PopulateGestprojectUserDataTable
        (
            System.Data.SqlClient.SqlConnection connection,
            string sage50LocalTerminalPath,
            string sage50Username,
            string sage50Password,
            string enterpryseGroupName
        )
        {
            try
            {
                connection.Open();

                string sqlString = $@"
                INSERT INTO {GestprojectSynchronizatorUserDataTableName}
                    (
                        {GestprojectSynchronizatorUserDataTerminalFieldName},
                        {GestprojectSynchronizatorUserDataUsernameFieldName},
                        {GestprojectSynchronizatorUserDataPasswordFieldName},
                        {GestprojectSynchronizatorUserDataCompanyGroupFieldName},
                        {GestprojectSynchronizatorUserDataRememberFieldName}
                    )
                VALUES
                    (
                        '{sage50LocalTerminalPath.Trim()}',
                        '{sage50Username.Trim()}',
                        '{Encryptor.Encrypt(sage50Password.Trim())}',
                        '{enterpryseGroupName.Trim()}',
                        0
                    )
                ;";

                using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
                {
                    sqlCommand.ExecuteNonQuery();
                };

                return true;
            }
            catch(SqlException ex)
            {
                MessageBox.Show($"Error: \n\n{ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            };
        }

        public static bool UpdateGestprojectUserDataTable
        (
            System.Data.SqlClient.SqlConnection connection,
            string sage50LocalTerminalPath,
            string sage50Username,
            string sage50Password,
            string enterpryseGroupName
        )
        {
            try
            {
                connection.Open();

                string sqlString = $@"
                UPDATE {GestprojectSynchronizatorUserDataTableName}
                SET 
                    {GestprojectSynchronizatorUserDataTerminalFieldName}='{sage50LocalTerminalPath.Trim()}',
                    {GestprojectSynchronizatorUserDataUsernameFieldName}='{sage50Username.Trim()}',
                    {GestprojectSynchronizatorUserDataPasswordFieldName}='{Encryptor.Encrypt(sage50Password.Trim())}',
                    {GestprojectSynchronizatorUserDataCompanyGroupFieldName}='{enterpryseGroupName.Trim()}',
                    {GestprojectSynchronizatorUserDataRememberFieldName}=1
                ;";

                using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
                {
                    sqlCommand.ExecuteNonQuery();
                };

                return true;
            }
            catch(SqlException ex)
            {
                MessageBox.Show($"Error: \n\n{ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            };
        }

        public static bool DisableRememberUserDataFeature
        (
            System.Data.SqlClient.SqlConnection connection
        )
        {
            try
            {
                connection.Open();

                string sqlString = $@"
                UPDATE {GestprojectSynchronizatorUserDataTableName}
                SET
                    {GestprojectSynchronizatorUserDataRememberFieldName}=0
                ;";

                using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
                {
                    sqlCommand.ExecuteNonQuery();
                };

                return true;
            }
            catch(SqlException ex)
            {
                MessageBox.Show($"Error: \n\n{ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            };
        }

        public static bool CheckIfRememberUserDataOptionWasActivated(System.Data.SqlClient.SqlConnection connection)
        {
            try
            {
                connection.Open();

                string sqlString = $@"
                SELECT
                    {GestprojectSynchronizatorUserDataRememberFieldName}
                FROM 
                    {GestprojectSynchronizatorUserDataTableName}
                ;";

                using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
                {
                    using(SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            return Convert.ToInt32(reader.GetValue(0)) == 1;
                        };
                    };
                };
                return false;
            }
            catch(SqlException ex)
            {
                MessageBox.Show($"Error: \n\n{ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            };
        }

        public static SynchronizerUserRememberableDataModel GetSynchronizerUserRememberableDataForConnection(System.Data.SqlClient.SqlConnection connection)
        {
            try
            {
                connection.Open();

                string sqlString = $@"
                SELECT
                    {GestprojectSynchronizatorUserDataTerminalFieldName},
                    {GestprojectSynchronizatorUserDataUsernameFieldName},
                    {GestprojectSynchronizatorUserDataPasswordFieldName},
                    {GestprojectSynchronizatorUserDataCompanyGroupFieldName},
                    {GestprojectSynchronizatorUserDataRememberFieldName}
                FROM 
                    {GestprojectSynchronizatorUserDataTableName}
                ;";

                using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
                {
                    using(SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            SynchronizerUserRememberableDataModel userRememberableDataModel = new SynchronizerUserRememberableDataModel();
                            userRememberableDataModel.Sage50LocalTerminalPath = Convert.ToString(reader.GetValue(0));
                            userRememberableDataModel.Sage50Username = Convert.ToString(reader.GetValue(1));
                            userRememberableDataModel.Sage50Password = Encryptor.UnEncrypt(Convert.ToString(reader.GetValue(2)));
                            userRememberableDataModel.Sage50CompanyGroupName = Convert.ToString(reader.GetValue(3));
                            userRememberableDataModel.Remember = Convert.ToByte(reader.GetValue(4));

                            return userRememberableDataModel;
                        };
                    };
                };
                return null;
            }
            catch(SqlException ex)
            {
                MessageBox.Show($"Error: \n\n{ex.Message}");
                return null;
            }
            finally
            {
                connection.Close();
            };
        }
    }
}
