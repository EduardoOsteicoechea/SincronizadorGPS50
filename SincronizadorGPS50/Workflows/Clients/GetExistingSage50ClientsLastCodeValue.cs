using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class GetExistingSage50ClientsLastCodeValue
    {
        internal int Value { get; set; }
        internal int NextValue { get; set; }
        internal GetExistingSage50ClientsLastCodeValue() 
        {
            int Sage50HigestCodeNumber = DataHolder.Sage50ClientClassList.First().CODIGO_NUMERO;
            for(int i = 0; i < DataHolder.Sage50ClientClassList.Count; i++)
            {
                if(DataHolder.Sage50ClientClassList[i].CODIGO_NUMERO > Sage50HigestCodeNumber)
                {
                    Sage50HigestCodeNumber = DataHolder.Sage50ClientClassList[i].CODIGO_NUMERO;
                }
            };

            Value = Sage50HigestCodeNumber;
            NextValue = Value + 1;
        }
    }
}
