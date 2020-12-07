using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Excel = Microsoft.Office.Interop.Excel;

namespace Client_Csharp_MySQL
{
    public partial class report2 : Form
    {
        public report2()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            Program.command.CommandText = "use track; SELECT a.* FROM (SELECT ANY_VALUE(id) AS ID, imei_id, max(dt) AS DT FROM coordinates GROUP BY imei_id LIMIT "+ textBox2.Text +") AS a where TIMESTAMPDIFF(SECOND, a.DT, NOW()) > "+ textBox1.Text +";";
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

        private void button2_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            // Создаём экземпляр рабочий книги Excel
            Excel.Workbook workBook;
            // Создаём экземпляр листа Excel
            Excel.Worksheet workSheet;

            workBook = excelApp.Workbooks.Add();
            workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);



            for (int j = 1; j < dataGridView1.Columns.Count+1; j++)
            {
                workSheet.Cells[1, j] = dataGridView1.Columns[j-1].HeaderText;
            }

            for (int j = 1; j < dataGridView1.Columns.Count + 1; j++)
            {
                for ( int i = 2; i < dataGridView1.RowCount+2; i++)
                {
                    workSheet.Cells[i, j] = dataGridView1.Rows[i-2].Cells[j-1].Value;
                }
            }

            workBook.SaveAs(textBox3.Text);
            excelApp.Quit();
        }
    }

    
}
