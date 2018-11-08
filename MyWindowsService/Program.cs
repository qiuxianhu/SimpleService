using System.ServiceProcess;

namespace MyWindowsService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main(string [] args)
        {
            const string SERVICE_NAME = "MyWindowsService";
            if (args.Length>0&&(args[0].ToLower()=="-install"||args[0].ToLower()=="-i"))
            {
                if (!ServiceIsExisted(SERVICE_NAME))
                {
                    System.Configuration.Install.ManagedInstallerClass.InstallHelper(new string[] { string.Concat(SERVICE_NAME,".exe")});
                    ServiceController c = new ServiceController(SERVICE_NAME);
                    c.Start();
                }
            }
            else if(args.Length>0&&(args[0].ToLower()== "-uninstall" || args[0].ToLower()=="-u"))
            {
                if (ServiceIsExisted(SERVICE_NAME))
                {
                    System.Configuration.Install.ManagedInstallerClass.InstallHelper(new string[] { "/u", string.Concat(SERVICE_NAME, ".exe")});
                }
            }
            else
            {
                ServiceBase[] ServicesToRun= { new Service1() };                
                ServiceBase.Run(ServicesToRun);
            }            
        }
        /// <summary>
        /// 判断是否了安装该服务
        /// </summary>
        /// <param name="svcName"></param>
        /// <returns></returns>
        private static bool ServiceIsExisted(string svcName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController s in services)
            {
                if (string.CompareOrdinal(s.ServiceName,svcName)==0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
