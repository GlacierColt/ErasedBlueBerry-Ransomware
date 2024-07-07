using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ErasedBlueBerry.form;
using Microsoft.Win32;

namespace ErasedBlueBerry
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Application.Run(new Form1());
        }

        public static void AddToStartup(string appName, string executablePath)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            try
            {
                rk.SetValue(appName, executablePath);
                Console.WriteLine("Added to startup: " + appName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding to startup: " + ex.Message);
            }
            finally
            {
                if (rk != null)
                    rk.Close();
            }
        }
    }
}
