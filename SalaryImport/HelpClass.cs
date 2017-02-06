using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace SalaryImport
{
    public class HelpClass
    {
        public List<Person> persons = new List<Person>();
        public List<Company> companies = new List<Company>();
        public List<Employee> employees;
        public List<SalaryComponent> salaryComponents = new List<SalaryComponent>();

        public void InitData()
        {
            var jsonPersons = File.ReadAllText(@"..\..\JSON\Persons.json");
            persons = JsonConvert.DeserializeObject<List<Person>>(jsonPersons);

            var jsonCompanies = File.ReadAllText(@"..\..\JSON\Companies.json");
            companies = JsonConvert.DeserializeObject<List<Company>>(jsonCompanies);

            var jsonEmployees = File.ReadAllText(@"..\..\JSON\Employees.json");
            employees = new List<Employee>();
            employees = JsonConvert.DeserializeObject<List<Employee>>(jsonEmployees);
        }
        /// <summary>
        /// Populates the salaryComponents List with data from selected JSON file
        /// </summary>
        /// <param name="fileName"></param>
        public void ReadSelectedData(string fileName)
        {
            
            List<SalaryComponent> newSalaryComponents;
            string jsonSalaryComponents = File.ReadAllText(fileName);
            //jsonSalaryComponents = ConcatenateJsonStrings(jsonSalaryComponents);

            newSalaryComponents = JsonConvert.DeserializeObject<List<SalaryComponent>>(jsonSalaryComponents);

            //If new data contain salary components with same id's as old data -> Overwrite old data
            foreach (var item in newSalaryComponents)
            {
                if (salaryComponents.Contains(item))
                {
                    salaryComponents.Remove(item);
                }
            }
            salaryComponents.AddRange(newSalaryComponents);
        }

        //private string ConcatenateJsonStrings(string json)
        //{
        //    var charsToRemove = new string[] { "[", "]" };
        //    foreach (var c in charsToRemove)
        //    {
        //        json = json.Replace(c, string.Empty);
        //    }
        //    json = json.Insert(json.Length, ",");
        //    json = json.Insert(0, "[");
        //    json = json.Insert(json.Length, "]");

        //    return json;
        //}

        internal void CalculateSalaries()
        {
            foreach (var employee in employees)
            {
                employee.CalculateSalary(salaryComponents);
            }
        }

        internal void CalculatePMLs()
        {
            foreach (var employee in employees)
            {
                employee.PopulatePMLList(salaryComponents);
                employee.CalculatePML();
            }
        }
    }
}
