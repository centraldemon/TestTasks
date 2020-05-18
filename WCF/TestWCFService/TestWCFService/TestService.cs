﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TestWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TestService : ITestService
    {
        public string GetData(string value)
        {
            return string.Format("You entered: {0}", value);
        }

        public DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }
    }
}
