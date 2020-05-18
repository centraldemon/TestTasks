using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Abstract;

namespace TestTask.Domain
{
    class FixWageEmployee : BaseEmployee
    {
        public double FixWage { get; set; }
        public override double GetAverageMonthWage()
        {
            return FixWage;
        }
    }
}
