using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Abstract
{
    abstract class BaseEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public abstract double GetAverageMonthWage();
        public override string ToString()
        {
            return $"Employee: id: {Id} name: {Name} average month wage: {GetAverageMonthWage()}";
        }
    }
}
