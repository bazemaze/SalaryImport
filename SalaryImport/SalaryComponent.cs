using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryImport
{
    public class SalaryComponent
    {
        public int Type { get; set; }
        public double Amount { get; set; }
        public DateTime FromDate { get; set; }
        public string Id { get; set; }

        //In order to compare objects e.g. in HelpClass.cs line 40 "if (salaryComponents.Contains(item))"
        //and return true if two different objects have the same Id
        public override bool Equals(object obj)
        {
            return ((SalaryComponent)obj).Id == Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        //
    }
}
