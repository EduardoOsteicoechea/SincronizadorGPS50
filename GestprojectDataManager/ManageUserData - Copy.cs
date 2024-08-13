//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Data.SqlTypes;
//using System.Windows.Forms;

//namespace GestprojectDataManager
//{
//    public static class ManageUserData
//    {
//        public static string GestprojectSynchronizatorUserDataTableName { get; set; } = "INT_SAGE_USERDATA";

//        public static string GestprojectSynchronizatorUserDataIdFieldName { get; set; } = "ID";
//        public static string GestprojectSynchronizatorUserDataTerminalFieldName { get; set; } = "SAGE_50_LOCAL_TERMINAL_PATH";
//        public static string GestprojectSynchronizatorUserDataUsernameFieldName { get; set; } = "SAGE_50_USER_NAME";
//        public static string GestprojectSynchronizatorUserDataPasswordFieldName { get; set; } = "SAGE_50_PASSWORD";
//        public static string GestprojectSynchronizatorUserDataCompanyGroupFieldName { get; set; } = "SAGE_50_COMPANY_GROUP_NAME";
//        public static string GestprojectSynchronizatorUserDataRememberFieldName { get; set; } = "REMEMBER";
//        public static string GestprojectSynchronizatorUserDataLastUpdateFieldName { get; set; } = "LAST_UPDATE";


//        public static string Sage50CompanyGroupDataTableName { get; set; } = "INT_SAGE_USERDATA_COMPANY_GROUPS";

//        public static string Sage50CompanyGroupIdFieldName { get; set; } = "ID";
//        public static string Sage50CompanyGroupDataTerminalFieldName { get; set; } = "SAGE_50_LOCAL_TERMINAL_PATH";
//        public static string Sage50CompanyGroupDataUsernameFieldName { get; set; } = "SAGE_50_USER_NAME";
//        public static string Sage50CompanyGroupDataPasswordFieldName { get; set; } = "SAGE_50_PASSWORD";   
//        public static string Sage50CompanyGroupCompanyName { get; set; } = "COMPANY_GROUP_NAME";
//        public static string Sage50CompanyGroupCompanyMainCode { get; set; } = "COMPANY_GROUP_MAIN_CODE";
//        public static string Sage50CompanyGroupCompanyCode { get; set; } = "COMPANY_GROUP_CODE";
//        public static string Sage50CompanyGroupCompanyGuid { get; set; } = "COMPANY_GROUP_GUID_ID";
//        public static string Sage50CompanyGroupWasLastSelected { get; set; } = "WAS_LAST_SELECTED";
//        public static string Sage50CompanyGroupLastUpdate { get; set; } = "LAST_UPDATE";

//        public static bool Save
//        (
//            System.Data.SqlClient.SqlConnection connection,
//            string sage50LocalTerminalPath,
//            string sage50Username,
//            string sage50Password,
//            string selectedCompanyGroupName,
//            List<string> availableCompanyGroupNameList,
//            List<string> availableCompanyGroupMainCodeList,
//            List<string> availableCompanyGroupCodeList,
//            List<string> availableCompanyGroupGuidIdList
//        )
//        {
//            try
//            {
//                if(CheckIfGestprojectUserDataTableExists(connection))
//                {
//                    if(!CkeckIfGestprojectUserDataTableIsEmpty(connection))
//                    {
//                        UpdateGestprojectUserDataTable(
//                            connection,
//                            sage50LocalTerminalPath,
//                            sage50Username,
//                            sage50Password,
//                            selectedCompanyGroupName
//                        );
//                        PopulateSage50CompanyGroupDataTable(
//                            connection,
//                            sage50LocalTerminalPath,
//                            sage50Username,
//                            sage50Password,
//                            selectedCompanyGroupName,
//                            availableCompanyGroupNameList,
//                            availableCompanyGroupMainCodeList,
//                            availableCompanyGroupCodeList,
//                            availableCompanyGroupGuidIdList
//                        );
//                        return true;
//                    }
//                    else
//                    {
//                        PopulateGestprojectUserDataTable(
//                            connection,
//                            sage50LocalTerminalPath,
//                            sage50Username,
//                            sage50Password,
//                            selectedCompanyGroupName
//                        );
//                        PopulateSage50CompanyGroupDataTable(
//                            connection,
//                            sage50LocalTerminalPath,
//                            sage50Username,
//                            sage50Password,
//                            selectedCompanyGroupName,
//                            availableCompanyGroupNameList,
//                            availableCompanyGroupMainCodeList,
//                            availableCompanyGroupCodeList,
//                            availableCompanyGroupGuidIdList
//                        );
//                        return true;
//                    };
//                }
//                else 
//                {
//                    CreateGestprojectUserDataTable(connection);
//                    PopulateGestprojectUserDataTable(
//                        connection,
//                        sage50LocalTerminalPath,
//                        sage50Username,
//                        sage50Password,
//                        selectedCompanyGroupName
//                    );
//                    CreateSage50CompanyGroupDataTable(connection);
//                    PopulateSage50CompanyGroupDataTable(
//                        connection,
//                        sage50LocalTerminalPath,
//                        sage50Username,
//                        sage50Password,
//                        selectedCompanyGroupName,
//                        availableCompanyGroupNameList,
//                        availableCompanyGroupMainCodeList,
//                        availableCompanyGroupCodeList,
//                        availableCompanyGroupGuidIdList
//                    );
//                    return true;
//                };
//            }
//            catch(SqlException ex)
//            {
//                MessageBox.Show($"Error: \n\n{ex.Message}");
//                return false;
//            };
//        }

//        public static bool CheckIfGestprojectUserDataTableExists(System.Data.SqlClient.SqlConnection connection)
//        {
//            try
//            {
//                connection.Open();

//                string sqlString = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE \"TABLE_NAME\" = '{GestprojectSynchronizatorUserDataTableName}'";

//                using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
//                {
//                    int? Sage50SincronizationTableCount = (int)sqlCommand.ExecuteScalar();
//                    if(Sage50SincronizationTableCount != null)
//                    {
//                        return Sage50SincronizationTableCount > 0;
//                    }
//                    else
//                    {
//                        return false;
//                    };
//                };
//            }
//            catch(SqlException ex)
//            {
//                MessageBox.Show($"Error: \n\n{ex.Message}");
//                return false;
//            }
//            finally
//            {
//                connection.Close();
//            };
//        }

//        public static bool CreateGestprojectUserDataTable(System.Data.SqlClient.SqlConnection connection)
//        {
//            try
//            {
//                connection.Open();
                
//                string sqlString = $@"
//                    CREATE TABLE 
//                        {GestprojectSynchronizatorUserDataTableName} 
//                        (
//                            {GestprojectSynchronizatorUserDataIdFieldName} INT PRIMARY KEY IDENTITY(1,1), 
//                            {GestprojectSynchronizatorUserDataTerminalFieldName} VARCHAR(MAX),
//                            {GestprojectSynchronizatorUserDataUsernameFieldName} VARCHAR(MAX), 
//                            {GestprojectSynchronizatorUserDataPasswordFieldName} VARCHAR(MAX), 
//                            {GestprojectSynchronizatorUserDataCompanyGroupFieldName} VARCHAR(MAX),
//                            {GestprojectSynchronizatorUserDataRememberFieldName} BIT,
//                            {GestprojectSynchronizatorUserDataLastUpdateFieldName} DATETIME DEFAULT GETDATE() NOT NULL
//                        )
//                    ;";

//                using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
//                {
//                    sqlCommand.ExecuteNonQuery();
//                };

//                return true;
//            }
//            catch(SqlException ex)
//            {
//                MessageBox.Show($"Error: \n\n{ex.Message}");
//                return false;
//            }
//            finally
//            {
//                connection.Close();
//            };
//        }

        

//        public static bool DisableRememberInGestprojectUserDataTable
//        (
//            System.Data.SqlClient.SqlConnection connection
//        )
//        {
//            try
//            {
//                connection.Open();

//                string sqlString = $@"
//                UPDATE {GestprojectSynchronizatorUserDataTableName} SET {GestprojectSynchronizatorUserDataRememberFieldName}=0;";

//                using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
//                {
//                    sqlCommand.ExecuteNonQuery();
//                };

//                return true;
//            }
//            catch(SqlException ex)
//            {
//                MessageBox.Show($"Error: \n\n{ex.Message}");
//                return false;
//            }
//            finally
//            {
//                connection.Close();
//            };
//        }


//         public static bool PopulateGestprojectUserDataTable
//        (
//            System.Data.SqlClient.SqlConnection connection,
//            string sage50LocalTerminalPath,
//            string sage50Username,
//            string sage50Password,
//            string selectedCompanyGroupName
//        )
//        {
//            try
//            {
//                connection.Open();

//                string sqlString = $@"
//                INSERT INTO {GestprojectSynchronizatorUserDataTableName}
//                    (
//                        {GestprojectSynchronizatorUserDataTerminalFieldName},
//                        {GestprojectSynchronizatorUserDataUsernameFieldName},
//                        {GestprojectSynchronizatorUserDataPasswordFieldName},
//                        {GestprojectSynchronizatorUserDataCompanyGroupFieldName},
//                        {GestprojectSynchronizatorUserDataRememberFieldName}
//                    )
//                VALUES
//                    (
//                        '{sage50LocalTerminalPath.Trim()}',
//                        '{sage50Username.Trim()}',
//                        '{Encryptor.Encrypt(sage50Password.Trim())}',
//                        '{selectedCompanyGroupName.Trim()}',
//                        0
//                    )
//                ;";

//                using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
//                {
//                    sqlCommand.ExecuteNonQuery();
//                };

//                return true;
//            }
//            catch(SqlException ex)
//            {
//                MessageBox.Show($"Error: \n\n{ex.Message}");
//                return false;
//            }
//            finally
//            {
//                connection.Close();
//            };
//        }



//        public static bool CreateSage50CompanyGroupDataTable(System.Data.SqlClient.SqlConnection connection)
//        {
//            try
//            {
//                connection.Open();

//                string sqlString = $@"
//                    CREATE TABLE 
//                        {Sage50CompanyGroupDataTableName} 
//                        (
//                            {Sage50CompanyGroupIdFieldName} INT PRIMARY KEY IDENTITY(1,1), 
//                            {Sage50CompanyGroupDataTerminalFieldName} VARCHAR(MAX),
//                            {Sage50CompanyGroupDataUsernameFieldName} VARCHAR(MAX), 
//                            {Sage50CompanyGroupDataPasswordFieldName} VARCHAR(MAX), 
//                            {Sage50CompanyGroupCompanyName} VARCHAR(MAX),
//                            {Sage50CompanyGroupCompanyMainCode} VARCHAR(MAX),
//                            {Sage50CompanyGroupCompanyCode} VARCHAR(MAX), 
//                            {Sage50CompanyGroupCompanyGuid} VARCHAR(MAX), 
//                            {Sage50CompanyGroupWasLastSelected} BIT,
//                            {Sage50CompanyGroupLastUpdate} DATETIME DEFAULT GETDATE() NOT NULL
//                        )
//                    ;";

//                using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
//                {
//                    sqlCommand.ExecuteNonQuery();
//                };

//                return true;
//            }
//            catch(SqlException ex)
//            {
//                MessageBox.Show($"Error: \n\n{ex.Message}");
//                return false;
//            }
//            finally
//            {
//                connection.Close();
//            };
//        }
//        public static bool PopulateSage50CompanyGroupDataTable
//        (
//            System.Data.SqlClient.SqlConnection connection,
//            string sage50LocalTerminalPath,
//            string sage50Username,
//            string sage50Password,
//            string selectedCompanyGroupName,
//            List<string> availableCompanyGroupNameList,
//            List<string> availableCompanyGroupMainCodeList,
//            List<string> availableCompanyGroupCodeList,
//            List<string> availableCompanyGroupGuidIdList
//        )
//        {
//            try
//            {
//                connection.Open();
                
//                string deleteCommandSql = $"DELETE FROM { Sage50CompanyGroupDataTableName}";

//                using(SqlCommand sqlCommand = new SqlCommand(deleteCommandSql, connection))
//                {
//                    sqlCommand.ExecuteNonQuery();
//                };

//                for (global::System.Int32 i = 0; i < availableCompanyGroupNameList.Count; i++)
//                {
//                    byte wasLastSelected = (byte)(selectedCompanyGroupName == availableCompanyGroupNameList[i] ? 1 : 0);
//                    string sqlString = $@"
//                    INSERT INTO {Sage50CompanyGroupDataTableName}
//                        (
//                            {Sage50CompanyGroupDataTerminalFieldName},
//                            {Sage50CompanyGroupDataUsernameFieldName},
//                            {Sage50CompanyGroupDataPasswordFieldName},
//                            {Sage50CompanyGroupCompanyName},
//                            {Sage50CompanyGroupCompanyMainCode},
//                            {Sage50CompanyGroupCompanyCode},
//                            {Sage50CompanyGroupCompanyGuid},
//                            {Sage50CompanyGroupWasLastSelected}
//                        )
//                    VALUES
//                        (
//                            '{sage50LocalTerminalPath.Trim()}',
//                            '{sage50Username.Trim()}',
//                            '{Encryptor.Encrypt(sage50Password.Trim())}',
//                            '{availableCompanyGroupNameList[i].Trim()}',
//                            '{availableCompanyGroupMainCodeList[i].Trim()}',
//                            '{availableCompanyGroupCodeList[i].Trim()}',
//                            '{availableCompanyGroupGuidIdList[i].Trim()}',
//                            {wasLastSelected}
//                        )
//                    ;";

//                    using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
//                    {
//                        sqlCommand.ExecuteNonQuery();
//                    };
//                };

//                return true;
//            }
//            catch(SqlException ex)
//            {
//                MessageBox.Show($"Error: \n\n{ex.Message}");
//                return false;
//            }
//            finally
//            {
//                connection.Close();
//            };
//        }

//        public static bool UpdateGestprojectUserDataTable
//        (
//            System.Data.SqlClient.SqlConnection connection,
//            string sage50LocalTerminalPath,
//            string sage50Username,
//            string sage50Password,
//            string selectedCompanyGroupName
//        )
//        {
//            try
//            {
//                connection.Open();

//                string sqlString = $@"
//                UPDATE {GestprojectSynchronizatorUserDataTableName}
//                SET 
//                    {GestprojectSynchronizatorUserDataTerminalFieldName}='{sage50LocalTerminalPath.Trim()}',
//                    {GestprojectSynchronizatorUserDataUsernameFieldName}='{sage50Username.Trim()}',
//                    {GestprojectSynchronizatorUserDataPasswordFieldName}='{Encryptor.Encrypt(sage50Password.Trim())}',
//                    {GestprojectSynchronizatorUserDataCompanyGroupFieldName}='{selectedCompanyGroupName.Trim()}',
//                    {GestprojectSynchronizatorUserDataRememberFieldName}=1
//                ;";

//                using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
//                {
//                    sqlCommand.ExecuteNonQuery();
//                };

//                return true;
//            }
//            catch(SqlException ex)
//            {
//                MessageBox.Show($"Error: \n\n{ex.Message}");
//                return false;
//            }
//            finally
//            {
//                connection.Close();
//            };
//        }

//        public static bool DisableRememberUserDataFeature
//        (
//            System.Data.SqlClient.SqlConnection connection
//        )
//        {
//            try
//            {
//                connection.Open();

//                string sqlString = $@"
//                UPDATE {GestprojectSynchronizatorUserDataTableName}
//                SET
//                    {GestprojectSynchronizatorUserDataRememberFieldName}=0
//                ;";

//                using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
//                {
//                    sqlCommand.ExecuteNonQuery();
//                };

//                return true;
//            }
//            catch(SqlException ex)
//            {
//                MessageBox.Show($"Error: \n\n{ex.Message}");
//                return false;
//            }
//            finally
//            {
//                connection.Close();
//            };
//        }

//        public static bool CheckIfRememberUserDataOptionWasActivated(System.Data.SqlClient.SqlConnection connection)
//        {
//            try
//            {
//                connection.Open();

//                string sqlString = $@"
//                SELECT
//                    {GestprojectSynchronizatorUserDataRememberFieldName}
//                FROM 
//                    {GestprojectSynchronizatorUserDataTableName}
//                ;";

//                using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
//                {
//                    using(SqlDataReader reader = sqlCommand.ExecuteReader())
//                    {
//                        while(reader.Read())
//                        {
//                            return Convert.ToInt32(reader.GetValue(0)) == 1;
//                        };
//                    };
//                };
//                return false;
//            }
//            catch(SqlException ex)
//            {
//                MessageBox.Show($"Error: \n\n{ex.Message}");
//                return false;
//            }
//            finally
//            {
//                connection.Close();
//            };
//        }

//        public static bool CkeckIfGestprojectUserDataTableIsEmpty(System.Data.SqlClient.SqlConnection connection)
//        {
//            try
//            {
//                connection.Open();

//                string sqlString = $@"
//                    IF EXISTS 
//                        (SELECT 1 FROM {GestprojectSynchronizatorUserDataTableName}) 
//                        SELECT 1 
//                    ELSE 
//                        SELECT 0
//                ";

//                using(SqlCommand command = new SqlCommand(sqlString, connection))
//                {
//                    int result = (int)command.ExecuteScalar();
//                    return result == 0;
//                };
//            }
//            catch(SqlException ex)
//            {
//                MessageBox.Show($"Error: \n\n{ex.Message}");
//                return false;
//            }
//            finally
//            {
//                connection.Close();
//            };
//        }

//        public static SynchronizerUserRememberableDataModel GetSynchronizerUserRememberableDataForConnection(System.Data.SqlClient.SqlConnection connection)
//        {
//            try
//            {
//                connection.Open();

//                SynchronizerUserRememberableDataModel userRememberableDataModel = new SynchronizerUserRememberableDataModel();

//                string sqlString = $@"
//                SELECT
//                    {GestprojectSynchronizatorUserDataTerminalFieldName},
//                    {GestprojectSynchronizatorUserDataUsernameFieldName},
//                    {GestprojectSynchronizatorUserDataPasswordFieldName},
//                    {GestprojectSynchronizatorUserDataCompanyGroupFieldName},
//                    {GestprojectSynchronizatorUserDataRememberFieldName}
//                FROM 
//                    {GestprojectSynchronizatorUserDataTableName}
//                ;";

//                using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
//                {
//                    using(SqlDataReader reader = sqlCommand.ExecuteReader())
//                    {
//                        while(reader.Read())
//                        {
//                            userRememberableDataModel.Sage50LocalTerminalPath = Convert.ToString(reader.GetValue(0));
//                            userRememberableDataModel.Sage50Username = Convert.ToString(reader.GetValue(1));
//                            userRememberableDataModel.Sage50Password = Encryptor.UnEncrypt(Convert.ToString(reader.GetValue(2)));
//                            userRememberableDataModel.Sage50CompanyGroupName = Convert.ToString(reader.GetValue(3));
//                            userRememberableDataModel.Remember = Convert.ToByte(reader.GetValue(4));
//                        };
//                    };
//                };

//                string sqlString2 = $@"
//                SELECT
//                    {Sage50CompanyGroupCompanyName},
//                    {Sage50CompanyGroupCompanyMainCode},
//                    {Sage50CompanyGroupCompanyCode},
//                    {Sage50CompanyGroupCompanyGuid}
//                FROM 
//                    {Sage50CompanyGroupDataTableName}
//                ;";

//                using(SqlCommand sqlCommand = new SqlCommand(sqlString2, connection))
//                {
//                    using(SqlDataReader reader = sqlCommand.ExecuteReader())
//                    {
//                        while(reader.Read())
//                        {
//                            userRememberableDataModel.Sage50AvailableCompanyGroupsNameList.Add(Convert.ToString(reader.GetValue(0)));
//                            userRememberableDataModel.Sage50AvailableCompanyGroupsMainCodeList.Add(Convert.ToString(reader.GetValue(1)));
//                            userRememberableDataModel.Sage50AvailableCompanyGroupsCodeList.Add(Convert.ToString(reader.GetValue(2)));
//                            userRememberableDataModel.Sage50AvailableCompanyGroupsGuidIdList.Add(Convert.ToString(reader.GetValue(3)));
//                        };
//                    };
//                };

//                return userRememberableDataModel;
//            }
//            catch(SqlException ex)
//            {
//                MessageBox.Show($"Error: GetSynchronizerUserRememberableDataForConnection");
//                MessageBox.Show($"Error: \n\n{ex.Message}");
//                return null;
//            }
//            finally
//            {
//                connection.Close();
//            };
//        }
//    }
//}
