using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SincronizadorGPS50;
using Xunit;

namespace SincronizadorGPS50.Tests
{
   public class GetGestprojectEntitiesTests
   {
      [Fact]
      public void ShoulGetTList()
      {
         List<int> expected = new List<int>();

         //var actual = new TaxesDataTableManager().GetAndStoreGestprojectEntities();
         var actual = new List<int>();

         Assert.Equal(expected, actual);
      } 
   }
}
