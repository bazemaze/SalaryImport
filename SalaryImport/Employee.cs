using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalaryImport
{
    public class Employee
    {
        public char Type { get; set; }
        public string PersonId { get; set; }
        public string CompanyId { get; set; }
        public DateTime StartDate { get; set; }
        public string[] SalaryComponentIds { get; set; }
        public string Id { get; set; }

        public double Salary { get; set; }
        public List<SalaryComponent> PMLList { get; set; }
        public string PMLstring { get; set; }


        internal void CalculateSalary(List<SalaryComponent> salaryComponents)
        {
            foreach (var e in this.SalaryComponentIds)
            {
                foreach (var s in salaryComponents)
                {
                    if (e == s.Id && s.Type == 1)
                    {
                        this.Salary += s.Amount * 12;
                        break; // assumed that an employee can only have 1 salary component of type 1
                    }
                }
            }
        }
        internal void PopulatePMLList(List<SalaryComponent> salaryComponents)
        {
            this.PMLList = new List<SalaryComponent>();

            foreach (var e in this.SalaryComponentIds)
            {
                foreach (var s in salaryComponents)
                {
                    if (e == s.Id)
                    {
                        PMLList.Add(s);
                        break; //A specific salary component for an employee can only match one of the Id's in salaryComponents list
                    }
                }
            }
            PMLList = PMLList.OrderBy(p => p.FromDate).ToList(); //Order the list so that the output strings are written correct later
        }
        /// <summary>
        /// Sets the PMLstring (date + amount) to be visualized. No PML (double)values are calculated as is the case with "Salary"
        /// </summary>
        internal void CalculatePML()
        {
            foreach (var salaryComponent in this.PMLList)
            {
                if (this.Type == 'A')
                {
                    if (salaryComponent.Type == 1)
                    {
                        this.PMLstring += salaryComponent.FromDate.ToShortDateString() + ": ";
                        this.PMLstring += (salaryComponent.Amount * 12.2).ToString();
                        break;
                    }
                }
                else if (this.Type == 'B')
                {
                    if (salaryComponent.Type == 2)
                    {
                        double type3 = GetMostRecentType(salaryComponent, 3);
                        this.PMLstring += " ";
                        this.PMLstring += $"{salaryComponent.FromDate.ToShortDateString()}: {salaryComponent.Amount * 12 + type3}";
                    }
                    else if (salaryComponent.Type == 3)
                    {
                        double type2 = GetMostRecentType(salaryComponent, 2);
                        this.PMLstring += " ";
                        this.PMLstring += $"{salaryComponent.FromDate.ToShortDateString()}: {salaryComponent.Amount + type2}";
                    }
                }
            }
        }
        private double GetMostRecentType(SalaryComponent salaryComponent, int type)
        {
            int ind = this.PMLList.FindIndex(p => p.Equals(salaryComponent)); //PMLList should be ordered by date by now
            if (ind == 0)
            {
                return 0;
            }
            else
            {
                var previousElements = this.PMLList.GetRange(0, ind);
                var correctSalaryComponent = previousElements.OrderByDescending(sc => sc.FromDate).Where(el => el.Type == type).First();
                if (type == 2)
                {
                    return (correctSalaryComponent.Amount * 12);
                }
                else if (type == 3)
                {
                    return correctSalaryComponent.Amount;
                }
                else
                {
                    throw new Exception("Employee.cs: Method: GetMostRecentType: 'Type must be 2 or 3'");
                }
            }
        }
    }
}

