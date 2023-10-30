using QuanLyCafe.DTO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCafe.DAO
{
    public class SupplierDAO
    {
        private static SupplierDAO instance;

        public static SupplierDAO Instance
        {
            get { if (instance == null) instance = new SupplierDAO(); return SupplierDAO.instance; }
            private set { SupplierDAO.instance = value; }
        }

        private SupplierDAO() { }

        public List<Supplier> GetListSupplier()
        {
            List<Supplier> list = new List<Supplier>();

            string query = "select * from Supplier ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Supplier supplier = new Supplier(item);
                list.Add(supplier);
            }

            return list;
        }
        public static bool Kttrungma(string id)
        {
            string query = "select * from Supplier where id = N'" + id + "'";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            if (data.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public List<Supplier> SearchSupplierByName(string Sdt)
        {
            List<Supplier> list = new List<Supplier>();

            string query = string.Format("SELECT * FROM dbo.Supplier WHERE dbo.fuConvertToUnsign1(Sdt) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", Sdt );
            
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Supplier supplier = new Supplier(item);
                list.Add(supplier);
            }

            return list;
        }
        public static bool Kttrungten(string Sdt)
        {
            string query = "select * from Supplier where name = N'" + Sdt + "'";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            if (data.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public bool AddSupplier(string name, string id, string Sdt)
        {
            string query = string.Format("INSERT dbo.Supplier ( name, id, Sdt )VALUES  ( N'{0}',N'{1}',N'{2}')", name, id, Sdt);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateSupplier(string name, string id, string Sdt,string macu)
        {
            string query = string.Format("UPDATE dbo.Supplier SET name = N'{0}',id = N'{1}',Sdt = N'{2}' WHERE id = N'{3}'", name, id, Sdt, macu);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteSupplier(string macu)
        {
            string query = string.Format("Delete Supplier where id = N'{0}'", macu);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
