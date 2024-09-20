using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   public class DataVisualizer<T>
   {
      // SingleItemPrinter
      public DataVisualizer(string listTitle, T item) 
      {
         StringBuilder sb = new StringBuilder();
         sb.Append($"{listTitle}\n\n");
         sb.AppendLine($"{item.ToString()}");
         MessageBox.Show(sb.ToString());
      }

      // ListPrinter
      public DataVisualizer(string listTitle, IEnumerable<T> list) 
      {
         StringBuilder sb = new StringBuilder();
         int counter = 1;
         sb.Append($"{listTitle}\n\n");
         foreach (T item in list)
         {
            string printableCounterValue = string.Empty;
            if(counter.ToString().Length < 2)
            {
               printableCounterValue = $"0{counter}";
            }
            else
            {
               printableCounterValue = $"{counter}";
            };
            sb.AppendLine($"{printableCounterValue} --- {item.ToString()}");
            counter++;
         };
         MessageBox.Show(sb.ToString());
      }
   }
}
