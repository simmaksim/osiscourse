using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ProcessesInformation
{
    public class OSInformation
    {
        private List<OSInformation> info;

        public string NameProperty { get; private set; }
        public string Value { get; private set; }

        public List<OSInformation> GetOSInfo()
        {
            info = new List<OSInformation>();
            var query = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");

            foreach (ManagementObject obj in query.Get())
            {
                info.Add(new OSInformation() { NameProperty = "Build number", Value = obj["BuildNumber"].ToString() });
                info.Add(new OSInformation() { NameProperty = "Caption", Value = obj["Caption"].ToString() });
                info.Add(new OSInformation() { NameProperty = "Free physical memory", Value = Math.Round(double.Parse(obj["FreePhysicalMemory"].ToString())/1024/1024, 2).ToString() + " MB"});
                info.Add(new OSInformation() { NameProperty = "Free virtual memory", Value = Math.Round(double.Parse(obj["FreeVirtualMemory"].ToString())/1024/1024, 2).ToString() + " MB"});
                info.Add(new OSInformation() { NameProperty = "Name", Value = obj["Name"].ToString() });
                info.Add(new OSInformation() { NameProperty = "OSType", Value = obj["OSType"].ToString() });
                info.Add(new OSInformation() { NameProperty = "Registered user", Value = obj["RegisteredUser"].ToString() });
                info.Add(new OSInformation() { NameProperty = "Serial number", Value = obj["SerialNumber"].ToString() });
                info.Add(new OSInformation() { NameProperty = "Service pack major version", Value = obj["ServicePackMajorVersion"].ToString() });
                info.Add(new OSInformation() { NameProperty = "Service pack minor version", Value = obj["ServicePackMinorVersion"].ToString() });
                info.Add(new OSInformation() { NameProperty = "Status", Value = obj["Status"].ToString() });
                info.Add(new OSInformation() { NameProperty = "System device", Value = obj["SystemDevice"].ToString() });
                info.Add(new OSInformation() { NameProperty = "System directory", Value = obj["SystemDirectory"].ToString() });
                info.Add(new OSInformation() { NameProperty = "System drive", Value = obj["SystemDrive"].ToString() });
                info.Add(new OSInformation() { NameProperty = "Version", Value = obj["Version"].ToString() });
                info.Add(new OSInformation() { NameProperty = "Windows directory", Value = obj["WindowsDirectory"].ToString() });
            }

            return info;
        }
    }
}
