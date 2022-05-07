using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProcessesInformation
{
    public class SystemProcess
    {
        #region Fields
        private List<SystemProcess> processes;
        #endregion

        #region Properties
        public string ProcessName { get; private set; }
        public int ProcessID { get; private set; }
        public int Threads { get; private set; }
        #endregion

        #region Methods
        public List<SystemProcess> GetProcesses()
        {
            processes = new List<SystemProcess>();
            foreach (var process in Process.GetProcesses())
            {
                processes.Add(new SystemProcess() { ProcessName = process.ProcessName, ProcessID = process.Id, Threads = process.Threads.Count});
            }

            return processes;
        }

        public void KillProcessById(int id)
        {
            var process = Process.GetProcessById(id);
            
            try
            {
                process.Kill();
            }
            catch
            {
                throw new Exception("Kill exception!");
            }

        }
        #endregion
    }
}
