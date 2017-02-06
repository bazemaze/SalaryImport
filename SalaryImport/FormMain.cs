using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SalaryImport
{
    public partial class FormMain : Form
    {
        HelpClass hc;
        public FormMain()
        {
            InitializeComponent();
            hc = new HelpClass();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectFileDialog = SelectFile();

            if (selectFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    hc.InitData();

                    hc.ReadSelectedData(selectFileDialog.FileName);

                    hc.CalculateSalaries();

                    hc.CalculatePMLs();

                    InsertDataToTable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void InsertDataToTable()
        {
            dataGridView1.Rows.Clear();
            foreach (var e in hc.employees)
            {
                var person = hc.persons.Find(p => p.Id == e.PersonId);
                dataGridView1.Rows.Add(person.Name, person.PersonalIdNumber, e.StartDate.ToShortDateString(), e.Type, e.Salary, e.PMLstring);
            }
        }

        private static OpenFileDialog SelectFile()
        {
            OpenFileDialog selectFileDialog = new OpenFileDialog();
            selectFileDialog.InitialDirectory = @"..\..\JSON\";
            selectFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            selectFileDialog.FilterIndex = 0;
            selectFileDialog.RestoreDirectory = true;
            return selectFileDialog;
        }
    }
}
