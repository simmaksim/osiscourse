using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessesInformation
{
    public class CurrentProcess
    {
        #region Fields
        private Process process;
        private List<CurrentProcess> info;
        #endregion

        #region Properties
        public string Property { get; private set; }
        public string Value { get; private set; }
        #endregion

        #region Methods
        public List<CurrentProcess> GetCurrentProcessInformationById(int id)
        {
            info = new List<CurrentProcess>();
            process = Process.GetProcessById(id);

            info.Add(new CurrentProcess() { Property = "Name", Value = process.ProcessName.ToString() });
            info.Add(new CurrentProcess() { Property = "Handle", Value = process.Handle.ToString() });
            info.Add(new CurrentProcess() { Property = "Maximum working set size", Value = Math.Round(double.Parse(process.MaxWorkingSet.ToString()) / 1024 / 1024, 2).ToString() + "MB" });
            info.Add(new CurrentProcess() { Property = "Minimum working set size", Value = Math.Round(double.Parse(process.MinWorkingSet.ToString()) / 1024 / 1024, 2).ToString() + "MB" });
            info.Add(new CurrentProcess() { Property = "Peak virtual memory size", Value = Math.Round(double.Parse(process.PeakVirtualMemorySize.ToString()) / 1024 / 1024, 2).ToString() + "MB" });
            info.Add(new CurrentProcess() { Property = "Peak working set size", Value = Math.Round(double.Parse(process.PeakWorkingSet.ToString()) / 1024 / 1024, 2).ToString() + "MB" });
            info.Add(new CurrentProcess() { Property = "Base priority", Value = process.BasePriority.ToString() });
            info.Add(new CurrentProcess() { Property = "SessionID", Value = process.SessionId.ToString() });
            info.Add(new CurrentProcess() { Property = "Working set size", Value = Math.Round(double.Parse(process.WorkingSet.ToString()) / 1024 / 1024, 2).ToString() + "MB" });
            info.Add(new CurrentProcess() { Property = "Main module", Value = process.MainModule.ToString() });
            info.Add(new CurrentProcess() { Property = "Paged memory size", Value = Math.Round(double.Parse(process.PagedMemorySize.ToString()) / 1024 / 1024, 2).ToString() + "MB" });
            info.Add(new CurrentProcess() { Property = "Paged system memory size", Value = Math.Round(double.Parse(process.PagedSystemMemorySize.ToString()) / 1024 / 1024, 2).ToString() + "MB" });
            info.Add(new CurrentProcess() { Property = "User processor time", Value = process.UserProcessorTime.ToString() });


            return info;
        }
        #endregion
    }
}
