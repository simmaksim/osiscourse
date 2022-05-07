using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using Microsoft.Win32;

namespace ProcessesInformation
{
    public class ServiceInformation
    {
        private Lazy<ServiceController[]> services = new Lazy<ServiceController[]>(() => { return ServiceController.GetServices(); });
        private List<ServiceInformation> info;

        public string Name { get; private set; }
        public string Status { get; private set; }
        public string Description { get; private set; }
        public string ServiceName { get; private set; }

        public List<ServiceInformation> GetServiceInfo()
        {
            info = new List<ServiceInformation>();

            foreach (var serv in services.Value)
            {
                try
                {
                    var regKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\services\\" + serv.ServiceName);

                    if ((regKey != null) && (regKey.GetValue("Description") != null))
                        info.Add(new ServiceInformation() { Name = serv.DisplayName, Status = serv.Status.ToString(), Description = regKey.GetValue("Description").ToString(), ServiceName = serv.ServiceName });
                    else
                        info.Add(new ServiceInformation() { Name = serv.DisplayName, Status = serv.Status.ToString(), Description = String.Empty, ServiceName = serv.ServiceName });

                    regKey.Close();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return info;
        }

        public void ChangeStatus(int index)
        {
            var item = services.Value[index];

            if (item.Status.Equals(ServiceControllerStatus.Stopped))
                item.Start();

            else
                item.Stop();
        }
    }
}
