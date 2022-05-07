using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessesInformation
{
    public class ProcessorUsage
    {
        const float sampleFrequencyMillis = 1000;

        private object syncLock = new object();
        private PerformanceCounter counter;
        private PerformanceCounter ramCounter;
        private float result;
        private DateTime lastSampleTime;

        public ProcessorUsage()
        {
            this.counter = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
            this.ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public float GetCurrentValue()
        {
            if ((DateTime.UtcNow - lastSampleTime).TotalMilliseconds > sampleFrequencyMillis)
            {
                lock (syncLock)
                {
                    if ((DateTime.UtcNow - lastSampleTime).TotalMilliseconds > sampleFrequencyMillis)
                    {
                        result = counter.NextValue();
                        lastSampleTime = DateTime.UtcNow;
                    }
                }
            }

            return result;
        }

        public float GetAvailableRAM()
        { 
            return ramCounter.NextValue();
        }
    }
}
