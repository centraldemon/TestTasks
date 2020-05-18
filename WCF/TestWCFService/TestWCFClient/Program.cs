using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using TestWCFService;

namespace TestWCFClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var client = new Client())
            {
                Console.WriteLine(client.GetData("Hello"));
                Console.WriteLine(client.GetDateTimeNow().ToShortDateString());
            }
        }
    }
}
