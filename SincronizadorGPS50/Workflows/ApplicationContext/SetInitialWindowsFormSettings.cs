
using System;
using System.Diagnostics;

namespace SincronizadorGPS50 {
   internal class SetInitialWindowsFormSettings {
      internal bool IsSuccessful { get; set; } = false;
      internal SetInitialWindowsFormSettings() {
         System.Windows.Forms.Application.EnableVisualStyles();
         System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

         IsSuccessful = true;
      }
   }
}