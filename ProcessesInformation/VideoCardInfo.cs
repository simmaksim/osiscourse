using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace ProcessesInformation
{
    public class VideoCardInformation
    {
        private List<List<VideoCardInformation>> info;

        public string NameProperty { get; private set; }
        public string Value { get; private set; }

        public List<List<VideoCardInformation>> GetVideoCardInfo()
        {
            info = new List<List<VideoCardInformation>>(); 
            var query = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");

            foreach (ManagementObject obj in query.Get())
            {
                info.Add(new List<VideoCardInformation> {
                    new VideoCardInformation() { NameProperty = "Adapter compatibility", Value = obj["AdapterCompatibility"].ToString() },
                    new VideoCardInformation() { NameProperty = "Adapter DAC type", Value = obj["AdapterDACType"].ToString() },
                    new VideoCardInformation() { NameProperty = "Adapter RAM", Value = Math.Round(double.Parse(obj["AdapterRAM"]. ToString()) / 1024 / 1024, 2).ToString() + " MB"},
                    new VideoCardInformation() { NameProperty = "Availability", Value = obj["Availability"].ToString() },
                    new VideoCardInformation() { NameProperty = "Description", Value = obj["Description"].ToString() },
                    new VideoCardInformation() { NameProperty = "DeviceID", Value = obj["DeviceID"].ToString() },
                    new VideoCardInformation() { NameProperty = "Driver version", Value = obj["DriverVersion"].ToString() },
                    new VideoCardInformation() { NameProperty = "Name", Value = obj["Name"].ToString() },
                    new VideoCardInformation() { NameProperty = "VideoArchitecture", Value = obj["VideoArchitecture"].ToString() },
                    new VideoCardInformation() { NameProperty = "VideoProcessor", Value = obj["VideoProcessor"].ToString() },
                    new VideoCardInformation() { NameProperty = "", Value = ""} });


            }

            return info;
        }

    }
}
