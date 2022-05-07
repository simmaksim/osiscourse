using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessesInformation
{
    public class SoftwareInformation
    {
        private List<SoftwareInformation> info;

        public string Caption { get; private set; }
        public string Date { get; private set; }

        public List<SoftwareInformation> GetSoftwareInfo(out bool isReady)
        {
            info = new List<SoftwareInformation>();

            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        try
                        {
                            if (sk.GetValue("DisplayName") == null) continue;
                            info.Add(new SoftwareInformation() { Caption = sk.GetValue("DisplayName").ToString(), Date = sk.GetValue("InstallDate").ToString() });
                        }
                        catch (Exception ex)
                        { }
                    }
                }
            }

            isReady = true;
            return info;
        }

    }
}
