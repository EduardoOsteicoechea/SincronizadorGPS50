using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50.Configuration
{
    internal class SerializatorBinderModifier : System.Runtime.Serialization.SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            if(assemblyName.Equals("NA"))
                return Type.GetType(typeName);
            else
                return defaultBinder.BindToType(assemblyName, typeName);
        }
        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            // specify a neutral code for the assembly name to be recognized by the BindToType method.
            assemblyName = "NA";
            typeName = serializedType.FullName;
        }
    }
}
