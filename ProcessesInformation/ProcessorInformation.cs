using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ProcessesInformation
{
    public class ProcessorInformation
    {
        private List<ProcessorInformation> info;

        public string NameProperty { get; private set; }
        public string Value { get; private set; }

        public List<ProcessorInformation> GetProcessorInfo()
        {
            info = new List<ProcessorInformation>();
            var query = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

            foreach (ManagementObject obj in query.Get())
            {
                info.Add(new ProcessorInformation() { NameProperty = "Address width", Value = obj["AddressWidth"].ToString() });
                info.Add(new ProcessorInformation() { NameProperty = "Architecture", Value = obj["Architecture"].ToString() });
                info.Add(new ProcessorInformation() { NameProperty = "Availability", Value = obj["Availability"].ToString() });
                info.Add(new ProcessorInformation() { NameProperty = "Caption", Value = obj["Caption"].ToString() });
                info.Add(new ProcessorInformation() { NameProperty = "CpuStatus", Value = obj["CpuStatus"].ToString() });
                info.Add(new ProcessorInformation() { NameProperty = "Creation class name", Value = obj["CreationClassName"].ToString() });
                info.Add(new ProcessorInformation() { NameProperty = "Data width", Value = obj["DataWidth"].ToString() });
                info.Add(new ProcessorInformation() { NameProperty = "L2CacheSize", Value = obj["L2CacheSize"].ToString() });
                info.Add(new ProcessorInformation() { NameProperty = "Manufacturer", Value = obj["Manufacturer"].ToString() });
                info.Add(new ProcessorInformation() { NameProperty = "Max clock speed", Value = obj["MaxClockSpeed"].ToString() });
                info.Add(new ProcessorInformation() { NameProperty = "Name", Value = obj["Name"].ToString() });
                info.Add(new ProcessorInformation() { NameProperty = "Number of cores", Value = obj["NumberOfCores"].ToString() });
                info.Add(new ProcessorInformation() { NameProperty = "Number of logical processors", Value = obj["NumberOfLogicalProcessors"].ToString() });
                info.Add(new ProcessorInformation() { NameProperty = "ProcessorId", Value = obj["ProcessorId"].ToString() });
                info.Add(new ProcessorInformation() { NameProperty = "Current clock speed", Value = obj["CurrentClockSpeed"].ToString() });
                info.Add(new ProcessorInformation() { NameProperty = "Socket designation", Value = obj["SocketDesignation"].ToString() });
                info.Add(new ProcessorInformation() { NameProperty = "Current voltage", Value = obj["CurrentVoltage"].ToString() });
                info.Add(new ProcessorInformation() { NameProperty = "Status", Value = obj["Status"].ToString() });
            }

            return info;
        }
    }
}
