using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using TestWCFService;
using System.ServiceModel.Description;

namespace TestWCFHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:35001/TestService/service");
            using(ServiceHost host = new ServiceHost(typeof(TestService), baseAddress))
            {
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                host.Description.Behaviors.Add(smb);
                host.Open();
                Console.WriteLine("Service is hosting");
                Console.ReadKey();
                host.Close();
            }
        }
    }
}
