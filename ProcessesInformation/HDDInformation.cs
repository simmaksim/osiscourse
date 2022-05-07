using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ProcessesInformation
{
    public class HDDInformation
    {
        private List<HDDInformation> info;

        public string NameProperty { get; private set; }
        public string Value { get; private set; }

        public List<HDDInformation> GetHDDInfo()
        {
            info = new List<HDDInformation>();

            foreach(var model in GetModelsInfo())
            {
                var query = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE Model = '" + model + "'");

                foreach (ManagementObject obj in query.Get())
                {
                    AddInList("Model", obj);
                    AddInList("Partitions", obj);
                    AddInList("TotalCylinders", obj);
                    AddInList("TotalSectors", obj);
                    AddInList("TotalTracks", obj);
                    AddInList("TracksPerSylinder", obj);
                    AddInList("DeviceID", obj);
                    AddInList("InterfaceType", obj);
                    AddInList("Manufacturer", obj);
                    AddInList("SerialNumber", obj);
                    AddInList("Size", obj);
                    AddInList("BytesPerSector", obj);

                    info.Add(new HDDInformation() { NameProperty = "", Value = "" });
                }
            }

            return info;
        }

        private List<string> GetModelsInfo()
        {
            var query = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            var models = new List<string>();

            foreach (ManagementObject obj in query.Get())
            {
                models.Add(obj["Model"].ToString());
            }

            return models;
        }

        private void AddInList(string value,  ManagementObject obj)
        {
            try
            {
                info.Add(new HDDInformation() { NameProperty = value, Value = obj[value].ToString() });
            }
            catch { }
        }
    }
}
