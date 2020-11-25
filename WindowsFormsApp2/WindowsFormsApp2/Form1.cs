using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private BindingSource bindingSource1 = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // your code here 
                string CSVFilePathName = @"data.csv";
                string[] Lines = File.ReadAllLines(CSVFilePathName);
                string[] Fields;
                Fields = Lines[0].Split(new char[] { '	' });
                int Cols = Fields.GetLength(0);
                DataTable dt = new DataTable();
                //1st row must be column names; force lower case to ensure matching later on.
                for (int i = 0; i < Cols; i++)
                {
                    dt.Columns.Add(Fields[i].ToLower(), typeof(string));
                }
                DataRow Row;
                for (int i = 1; i < Lines.GetLength(0); i++)
                {
                    Fields = Lines[i].Split(new char[] { '	' });
                    Row = dt.NewRow();
                    for (int f = 0; f < Cols; f++) 
                    { 
                        Row[f] = Fields[f];
                    }
                    dt.Rows.Add(Row);
                    
                }
                dataGridView1.DataSource = dt;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is " + ex.ToString());
                throw;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                        try
                        {
                            int columnCount = dataGridView1.Columns.Count;
                            string columnNames = "";
                            string[] outputCsv = new string[dataGridView1.Rows.Count];
                            for (int i = 0; i < columnCount-1; i++)
                            {
                                columnNames += dataGridView1.Columns[i].HeaderText.ToString() + "	";
                            }
                            columnNames += dataGridView1.Columns[3].HeaderText.ToString();
                            outputCsv[0] += columnNames;

                            for (int i = 1; (i) < dataGridView1.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount-1; j++)
                                {
                                    outputCsv[i] += dataGridView1.Rows[i-1].Cells[j].Value.ToString() + "	";
                                }
                                outputCsv[i] += dataGridView1.Rows[i-1].Cells[3].Value.ToString();
                            }
                            File.WriteAllLines("data.csv", outputCsv, Encoding.UTF8);
                            MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
