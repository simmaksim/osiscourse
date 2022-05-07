using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessesInformation
{
    public class ThreadsInfo
    {
        #region Fields
        private Process process;
        private List<ThreadsInfo> threads;
        #endregion

        #region Properties
        public int ThreadID { get; private set; }
        public string ThreadState { get; private set; }
        public string Priority { get; private set; }
        #endregion

        public List<ThreadsInfo> GetThreadsInformationById(int id)
        {
            threads = new List<ThreadsInfo>();
            process = Process.GetProcessById(id);
            var th = process.Threads;

            for (var i = 0; i < process.Threads.Count; i++)
            {
                threads.Add(new ThreadsInfo() { ThreadID = th[0].Id, ThreadState = th[0].ThreadState.ToString(), Priority = th[0].PriorityLevel.ToString() });
            }

            return threads;
        }
    }
}
