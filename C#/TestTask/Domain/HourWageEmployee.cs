using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Abstract;

namespace TestTask.Domain
{
    class HourWageEmployee : BaseEmployee
    {
        public double HourWage { get; set; }
        public override double GetAverageMonthWage()
        {
            return 20.8 * 8 * HourWage;
        }
    }
}
