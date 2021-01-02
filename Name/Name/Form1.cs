using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.IO;

namespace Name
{
    
    public partial class Form1 : Form
    {
        List<Person> people2;
        
        public int totalcount;

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            people2.Sort((x, y) => x.name.CompareTo(y.name));
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.DataSource = this.people2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            people2.Sort((x, y) => y.name.CompareTo(x.name));
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.DataSource = this.people2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            people2.Sort((x, y) => x.amount.CompareTo(y.amount));
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.DataSource = this.people2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            people2.Sort((x, y) => y.amount.CompareTo(x.amount));
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.DataSource = this.people2;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            var t = people2.Find(x => 
            {
                if (x.name == textBox3.Text)
                    return true;

                else
                    return false;
            });

            if (t != null)
                textBox4.Text = t.name+" has " + t.amount.ToString();
            else
                textBox4.Text = "Cant't find name in list";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string createText;
            people2 = new List<Person>();

            if (!File.Exists("names.json"))
            {
                MessageBox.Show("cant find names.json");
                Application.Exit();
            }

            else
            {
                createText = File.ReadAllText("names.json");
                DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(createText);
                DataTable dataTable = dataSet.Tables["names"];




                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine(row["name"] + " - " + row["amount"]);

                    people2.Add(new Person() { name = row["name"].ToString(), amount = int.Parse(row["amount"].ToString()) });

                    totalcount += int.Parse(row["amount"].ToString());
                }


                people2.Sort((x, y) => y.amount.CompareTo(x.amount));

                dataGridView1.DataSource = this.people2;
                dataGridView1.Width = 220;


                this.textBox1.ForeColor = Color.Black;
                this.textBox1.ReadOnly = true;

                this.textBox1.Text = "Total amount = " + totalcount.ToString();

            }

        }
    }

    public class Person
    {
        public string name { get; set; }

        public int amount { get; set; }
    }
}