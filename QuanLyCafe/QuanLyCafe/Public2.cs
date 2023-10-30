using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCafe
{
    internal class Public2
    {
        public static void ExportToExcel(ListView lv)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)app.ActiveSheet;
                    app.Visible = false;
                    for (int j = 1; j <= lv.Columns.Count-1; j++)
                    {
                        var newWidth = Math.Min(255, lv.Columns[j - 1].Width / 2);
                        ws.Columns[j].ColumnWidth = newWidth;
                        ws.Cells[1, j] = lv.Columns[j - 1].Text;
                    }
                    int i = 2;
                    foreach (ListViewItem item in lv.Items)
                    {
                        for (int k = 1; k <= item.SubItems.Count; k++)
                        {
                            ws.Cells[i, k] = item.SubItems[k - 1].Text;
                        }
                        i++;
                    }
                    wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("In hoá đơn thành công");
                }
            }
        }
    }
}
