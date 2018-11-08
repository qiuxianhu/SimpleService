using MyWindowsServiceHost;
using System.ServiceProcess;

namespace MyWindowsService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Global.Start();
        }

        protected override void OnStop()
        {
            Global.Stop();
        }
    }
}
