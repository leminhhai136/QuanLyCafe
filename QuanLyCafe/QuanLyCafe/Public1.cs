using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCafe
{
    internal class Public1
    {
        public static void XuatFileExcel(DataGridView dataGridView1, string fileName)
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;
            try
            {
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                excel.DisplayAlerts = false;

                workbook = excel.Workbooks.Add(Type.Missing);
                worksheet = null;
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                        worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText; 
                }
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            worksheet.Cells[i + 2, j + 1] = "'" + dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                }
                excelCellrange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[dataGridView1.RowCount + 1, dataGridView1.ColumnCount]];
                excelCellrange.EntireColumn.AutoFit();
                Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border.Weight = 2d;
                excelCellrange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, dataGridView1.ColumnCount]];
                excelCellrange.Interior.Color = System.Drawing.Color.Blue;
                excelCellrange.Font.Color = System.Drawing.Color.White;

                string folderName = Application.StartupPath + "\\Export";
                if (!Directory.Exists(folderName)) Directory.CreateDirectory(folderName);

                workbook.SaveAs(folderName + "\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss_") + fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                worksheet = null;
                workbook = null;
            }
        }
    }
}
