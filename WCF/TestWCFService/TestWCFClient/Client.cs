using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TestWCFService;

namespace TestWCFClient
{
    class Client: ClientBase<ITestService>
    {
        public string GetData(string str)
        {
            return base.Channel.GetData(str);
        }
        public DateTime GetDateTimeNow()
        {
            return base.Channel.GetDateTimeNow();
        }
    }
}
