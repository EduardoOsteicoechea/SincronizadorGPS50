using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   internal class VisualizePropertiesAndValues<T>
   {
      public VisualizePropertiesAndValues(string title, T entity)
      {
         StringBuilder stringBuilder = new StringBuilder();
         stringBuilder.Append($"{title}\n\n");
         foreach(var item in entity.GetType().GetProperties())
         {
            stringBuilder.Append($"{item.Name}: {item.GetValue(entity)}\n");
         };
         MessageBox.Show(stringBuilder.ToString());
      }
      public VisualizePropertiesAndValues(string title, List<T> entityList)
      {
         foreach(T entity in entityList)
         {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{title}\n\n");
            foreach(var item in entity.GetType().GetProperties())
            {
               stringBuilder.Append($"{item.Name}: {item.GetValue(entity)}\n");
            };
            MessageBox.Show(stringBuilder.ToString());
         };
      }
   }
}
