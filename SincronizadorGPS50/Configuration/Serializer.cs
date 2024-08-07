using System;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
    public class Serializer
    {
        public static bool SerializeObject(Object objectInstance, String filePath)
        {
            System.IO.FileStream fileStream = null;
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormater;
            bool returnValue;

            try
            {
                fileStream = System.IO.File.Create(filePath);
                binaryFormater = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormater.Serialize(fileStream, objectInstance);
                fileStream.Close();
                returnValue = true;
            }
            catch
            {
                returnValue = false;
            }
            finally
            {
                binaryFormater = null;
            }
            return returnValue;
        }

        public static Object DeserializeObject(String filePath)
        {
            System.IO.FileStream fileStream = null;
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = null;
            Object tempObject = null;

            try
            {
                if(System.IO.File.Exists(filePath))
                {
                    fileStream = System.IO.File.Open(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    //tempObject = binaryFormatter.Deserialize(fileStream);
                    tempObject = binaryFormatter.Deserialize(fileStream);
                    fileStream.Close();
                };
            }
            catch (System.Exception ex)
            {
                tempObject = null;
                string aa = ex.Message;
            }
            finally
            {
                binaryFormatter = null;
            }
            return tempObject;
        }
    }

    public override Type BindToType(string assemblyName, string typeName)
    {
        if(assemblyName.Equals("NA"))
            return Type.GetType(typeName);
        else
            return defaultBinder.BindToType(assemblyName, typeName);
    }
}

