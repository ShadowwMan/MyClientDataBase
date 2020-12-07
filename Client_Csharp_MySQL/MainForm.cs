using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace Client_Csharp_MySQL
{
    public partial class MainForm : Form
    {
        public string result_requist;
        public string[] name_databases = new string[10];
        public string[] name_tables = new string[100];
        public int count_database = 0;
        public int count_table = 0;
        bool key;
        public int index_x = 0;

        public MainForm()
        {
            InitializeComponent();
            Program.command = new MySqlCommand("show databases", Program.mysqlconnection);
            Program.reader = Program.command.ExecuteReader();

            while (Program.reader.Read())
            {
                name_databases[count_database] = Program.reader[0].ToString();
                treeView1.Nodes.Add(Program.reader[0].ToString());
                count_database++;

            }
            Program.reader.Close();

            for (int i = 0; i < count_database; i++)
            {
                Program.command.CommandText = "use " + name_databases[i];
                Program.reader = Program.command.ExecuteReader();
                while (Program.reader.Read())
                {
                    MessageBox.Show(Program.reader[0].ToString());
                }
                Program.reader.Close();

                Program.command.CommandText = "show tables";
                Program.reader = Program.command.ExecuteReader();
                while (Program.reader.Read())
                {
                    treeView1.Nodes[i].Nodes.Add(Program.reader[0].ToString());
                }
                Program.reader.Close();
            }  
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void выходИзУчетнойЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
            this.Show();
            this.Close();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void выходИзПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainPanel_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e){}

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView1.Columns.Clear();
            key = false;
            for ( int i = 0; i < count_database; i++)
            {
                for ( int j = 0; j < treeView1.Nodes[i].Nodes.Count; j++)
                {
                    if ( treeView1.Nodes[i].Nodes[j].Text == treeView1.SelectedNode.ToString().Substring(10))
                    {
                        Program.command = new MySqlCommand("use " + name_databases[i], Program.mysqlconnection);
                        key = true;
                        break;
                    }
                }
                
            }

            if (key)
            {
                Program.reader = Program.command.ExecuteReader();
                Program.reader.Read();
                Program.reader.Close();
                int yy = 0;
                Program.command = new MySqlCommand("SELECT count(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = \'" + treeView1.SelectedNode.ToString().Substring(10) + "\'", Program.mysqlconnection);
                Program.reader = Program.command.ExecuteReader();
                while (Program.reader.Read())
                {
                    yy = Convert.ToInt32(Program.reader[0]);
                }
                Program.reader.Close();

                Program.command = new MySqlCommand("show full columns from " + treeView1.SelectedNode.ToString().Substring(10), Program.mysqlconnection);
                Program.reader = Program.command.ExecuteReader();
                while (Program.reader.Read())
                {
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = Program.reader[0].ToString() });
                    var index = dataGridView2.Rows.Add();
                    for (int i = 0; i < 9; i++)
                    {
                        dataGridView2.Rows[index].Cells[i].Value = Program.reader[i].ToString();
                    }
                }
                Program.reader.Close();

                Program.command = new MySqlCommand("select * from " + treeView1.SelectedNode.ToString().Substring(10) + " limit " + textBox1.Text, Program.mysqlconnection);
                Program.reader = Program.command.ExecuteReader();
                while (Program.reader.Read())
                {
                    var index = dataGridView1.Rows.Add();
                    for (int i = 0; i < yy; i++)
                    {
                        dataGridView1.Rows[index].Cells[i].Value = Program.reader[i].ToString();
                    }
                }
                Program.reader.Close();


            }
        }

        private void консольToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (consolToolStripMenuItem.Checked == true)
            {
                splitContainer1.Panel2Collapsed = true;
                consolToolStripMenuItem.Checked = false;
            }
            else if(consolToolStripMenuItem.Selected == false)
            {
                consolToolStripMenuItem.Checked = true;
                splitContainer1.Panel2Collapsed = false;
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text[richTextBox1.Text.Length-1] == '\n')
            {
                Program.command = new MySqlCommand(richTextBox1.Text.Substring(index_x), Program.mysqlconnection);
                Program.reader = Program.command.ExecuteReader();
                Program.reader.Read();
                Program.reader.Close();
                index_x = richTextBox1.Text.Length;
            }

        }

        private void отчет1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            report2 report2 = new report2();
            report2.ShowDialog();
        }

        private void отчет2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            report1 report1 = new report1();
            report1.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
