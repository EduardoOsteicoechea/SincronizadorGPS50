//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SincronizadorGPS50
//{
//    internal class CreateSage50MissingClients
//    {
//        internal CreateSage50MissingClients()
//        {
//            for(int i = 0; i < DataHolder.Sage50ClientClassList.Count; i++)
//            {
//                if(DataHolder.Sage50ClientClassList[i].CIF != "" && DataHolder.Sage50ClientClassList[i].CIF != null)
//                {
//                    DataHolder.Sage50CIFList.Add(DataHolder.Sage50ClientClassList[i].CIF);
//                };
//            }

//            int Sage50HigestCodeNumber = DataHolder.Sage50ClientClassList.First().CODIGO_NUMERO;
//            for(int i = 0; i < DataHolder.Sage50ClientClassList.Count; i++)
//            {
//                if(DataHolder.Sage50ClientClassList[i].CODIGO_NUMERO > Sage50HigestCodeNumber)
//                {
//                    Sage50HigestCodeNumber = DataHolder.Sage50ClientClassList[i].CODIGO_NUMERO;
//                }
//            };

//            int counter = Sage50HigestCodeNumber + 1;

//            for(int i = 0; i < DataHolder.GestprojectSynchronizableClientClassList.Count; i++)
//            {
//                GestprojectClient gestProjectClient = DataHolder.GestprojectSynchronizableClientClassList[i];

//                if(!DataHolder.Sage50CIFList.Contains(gestProjectClient.PAR_CIF_NIF))
//                {
//                    Customer customer = new Customer();
//                    clsEntityCustomer clsEntityCustomerInstance = new clsEntityCustomer();

//                    if(counter < 10)
//                    {
//                        clsEntityCustomerInstance.codigo = "4300000" + counter;
//                    }
//                    else if(counter < 100)
//                    {
//                        clsEntityCustomerInstance.codigo = "430000" + counter;
//                    }
//                    else
//                    {
//                        clsEntityCustomerInstance.codigo = "43000" + counter;
//                    };

//                    clsEntityCustomerInstance.pais = gestProjectClient.PAR_CP_1;
//                    clsEntityCustomerInstance.nombre = gestProjectClient.PAR_NOMBRE;
//                    clsEntityCustomerInstance.cif = gestProjectClient.PAR_CIF_NIF;
//                    clsEntityCustomerInstance.direccion = gestProjectClient.PAR_DIRECCION_1;
//                    clsEntityCustomerInstance.provincia = gestProjectClient.PAR_PROVINCIA_1;
//                    clsEntityCustomerInstance.tipo_iva = "03";

//                    customer._Create(clsEntityCustomerInstance);

//                    counter++;
//                };
//            }
//        }
//    }
//}
