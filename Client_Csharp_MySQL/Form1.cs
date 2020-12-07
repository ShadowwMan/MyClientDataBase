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
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Program.mysqlbuilder = new MySqlConnectionStringBuilder();
        }

        //accept
        private void button1_Click(object sender, EventArgs e)
        {
            ////Program.mysqlbuilder.UserID = "root";
            ////Program.mysqlbuilder.Password = "Minecraft36";
            ////Program.mysqlbuilder.Server = "localhost";
            ////Program.mysqlbuilder.Port = 3306;
            Program.mysqlbuilder.AllowUserVariables = true;
            Program.mysqlbuilder.ConnectionTimeout = Int32.MaxValue;
            Program.mysqlbuilder.ConnectionLifeTime = Int32.MaxValue;
            Program.mysqlbuilder.ConnectionReset = false;
            Program.mysqlbuilder.DefaultCommandTimeout = 100000; 
                    Program.mysqlconnection = new MySqlConnection(Program.mysqlbuilder.ToString());
                    try
                    {
                        Program.mysqlconnection.Open();
                        MainForm mainform = new MainForm();
                        this.Hide();
                        mainform.ShowDialog();
                        this.Show();
                        this.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message,"Ошибка подключения");
                    }
        }

        

        private void Types_network_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            cheak();
            if (cheak_only_number() == true)
            {
                if (!String.IsNullOrEmpty(portword.Text))
                {
                    Program.mysqlbuilder.Port = Convert.ToUInt32(portword.Text);
                }
                label2.Visible = false;
            }
            else if (cheak_only_number() == false)
            {
                label2.Visible = true;
            }
            
        }

        private void Login_TextChanged(object sender, EventArgs e)
        {
            cheak();
            Program.mysqlbuilder.UserID = Login.Text;
        }

        private void Password_TextChanged(object sender, EventArgs e)
        {
            cheak();
            Program.mysqlbuilder.Password = Password.Text;
        }

        private void ipword_TextChanged(object sender, EventArgs e)
        {
            cheak();
            Program.mysqlbuilder.Server = ipword.Text;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void cheak()
        {
            if(!String.IsNullOrEmpty(portword.Text) && !String.IsNullOrEmpty(Login.Text) && !String.IsNullOrEmpty(Password.Text) && !String.IsNullOrEmpty(ipword.Text))
            {
                Connector.Enabled = true;
            }else if (String.IsNullOrEmpty(portword.Text) || String.IsNullOrEmpty(Login.Text) || String.IsNullOrEmpty(Password.Text) || String.IsNullOrEmpty(ipword.Text))
            {
                Connector.Enabled = false;
            }
        }

        private bool cheak_only_number()
        {
            for (int i = 0; i < portword.Text.Length; i++)
            {
                if (portword.Text[i] < '0' || portword.Text[i] > '9')
                {
                    return false;
                }
            }
            return true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
