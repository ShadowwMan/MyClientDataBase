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
using Excel = Microsoft.Office.Interop.Excel;

namespace Client_Csharp_MySQL
{
    public partial class report1 : Form
    {
        public report1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            Program.command.CommandText = "use track;SET @i:=0;SET @j:= 0;SELECT a.imei_id, a.dt , b.dt FROM (SELECT @i:= @i + 1 AS id, imei_id, dt, latitude, longitude FROM coordinates WHERE(DATE(dt) >= '"+textBox1.Text+ "' AND DATE(dt) <= '" + textBox2.Text + "') ORDER BY imei_id ) as a INNER JOIN (SELECT @j:= @j + 1 AS id, imei_id, dt, latitude, longitude FROM coordinates WHERE(DATE(dt) >= '" + textBox1.Text + "' AND DATE(dt) <= '" + textBox2.Text + "') ORDER BY imei_id) as b ON(((a.id - b.id) = 1) AND(TIMESTAMPDIFF(MINUTE, a.dt, b.dt) > " + textBox3.Text + ") AND (b.imei_id = a.imei_id)) LIMIT "+ textBox4.Text+";"; 
            Program.reader = Program.command.ExecuteReader();
            while (Program.reader.Read())
            {
                var index = dataGridView1.Rows.Add();
                for (int i = 0; i < 3; i++)
                {
                    dataGridView1.Rows[index].Cells[i].Value = Program.reader[i].ToString();
                }
            }
            Program.reader.Close();

            button2.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            // Создаём экземпляр рабочий книги Excel
            Excel.Workbook workBook;
            // Создаём экземпляр листа Excel
            Excel.Worksheet workSheet;

            workBook = excelApp.Workbooks.Add();
            workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);



            for (int j = 1; j < dataGridView1.Columns.Count + 1; j++)
            {
                workSheet.Cells[1, j] = dataGridView1.Columns[j - 1].HeaderText;
            }

            for (int j = 1; j < dataGridView1.Columns.Count + 1; j++)
            {
                for (int i = 2; i < dataGridView1.RowCount + 2; i++)
                {
                    workSheet.Cells[i, j] = dataGridView1.Rows[i - 2].Cells[j - 1].Value;
                }
            }

            workBook.SaveAs(textBox5.Text);
            excelApp.Quit();
        }
    }
}
