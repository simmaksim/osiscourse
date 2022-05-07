using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ProcessesInformation
{
    public class RAMInformation
    {
        private List<List<RAMInformation>> info;

        public string NameProperty { get; private set; }
        public string Value { get; private set; }

        public List<List<RAMInformation>> GetRAMInfo()
        {
            info = new List<List<RAMInformation>>();
            var query = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");

            foreach (ManagementObject obj in query.Get())
            {
                info.Add(new List<RAMInformation> {
                    new RAMInformation() { NameProperty = "Bank label", Value = obj["BankLabel"].ToString() },
                    new RAMInformation() { NameProperty = "Capacity", Value = Math.Round(double.Parse(obj["Capacity"].ToString())/1024/1024/1024, 2).ToString() + " GB"},
                    new RAMInformation() { NameProperty = "Data width", Value = obj["DataWidth"].ToString()},
                    new RAMInformation() { NameProperty = "Device locator", Value = obj["DeviceLocator"].ToString() },
                    new RAMInformation() { NameProperty = "Manufacturer", Value = obj["Manufacturer"].ToString() },
                    new RAMInformation() { NameProperty = "Part number", Value = obj["PartNumber"].ToString() },
                    new RAMInformation() { NameProperty = "Serial number", Value = obj["SerialNumber"].ToString() },
                    new RAMInformation() { NameProperty = "Speed", Value = obj["Speed"].ToString() },
                    new RAMInformation() { NameProperty = "Tag", Value = obj["Tag"].ToString() },
                    new RAMInformation() { NameProperty = "Total width", Value = obj["TotalWidth"].ToString() },
                    new RAMInformation() { NameProperty = "", Value = ""} });


            }

            return info;
        }
    }
}
