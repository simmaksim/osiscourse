using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProcessesInformation
{
    public class DiskInformation
    {
        #region Fields
        private List<DiskInformation> info;
        #endregion

        #region Properties
        public string Capacity { get; private set; }
        public string DriveLetter { get; private set; }
        public string DriveType { get; private set; }
        public string FileSystem { get; private set; }
        public string FreeSpace { get; private set; }
        #endregion

        #region Methods
        public List<DiskInformation> GetDrivesInfo()
        {
            info = new List<DiskInformation>();
            var allDrives = DriveInfo.GetDrives();

            foreach(var d in allDrives)
            {
                if (!d.IsReady) continue;

                info.Add(new DiskInformation()
                {
                    DriveType = d.DriveType.ToString(),
                    DriveLetter = d.RootDirectory.ToString(),
                    Capacity = Math.Round(double.Parse(d.TotalSize.ToString()) / 1024 / 1024 / 1024, 2).ToString() + " GB",
                    FileSystem = d.DriveFormat.ToString(),
                    FreeSpace = Math.Round(double.Parse(d.TotalFreeSpace.ToString()) / 1024 / 1024 / 1024, 2).ToString() + " GB"
                });
            }
            return info;
        }
        #endregion
    }
}
