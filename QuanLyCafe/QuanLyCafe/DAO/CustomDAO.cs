using QuanLyCafe.DTO;
using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafe.DAO
{
    public class CustomDAO
    {
        private static CustomDAO instance;

        public static CustomDAO Instance
        {
            get { if (instance == null) instance = new CustomDAO(); return CustomDAO.instance; }
            private set { CustomDAO.instance = value; }
        }

        private CustomDAO() { }

        public List<Custom> GetListCustom()
        {
            List<Custom> list = new List<Custom>();

            string query = "select * from Custom ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Custom custom = new Custom(item);
                list.Add(custom);
            }

            return list;
        }
        public static bool Kttrungma(string id)
        {
            string query = "select * from Custom where id = N'" + id + "'";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            if (data.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public List<Custom> SearchCustomByName(string name)
        {
            List<Custom> list = new List<Custom>();

            string query = string.Format("SELECT * FROM dbo.Custom WHERE dbo.fuConvertToUnsign1(name) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Custom custom = new Custom(item);
                list.Add(custom);
            }

            return list;
        }
        public bool AddCustom(string name, string id, string Sdt, string table)
        {
            string query = string.Format("INSERT dbo.Custom ( name, id, sdt, [table]) VALUES  ( N'{0}', N'{1}',N'{2}',N'{3}')", name, id, Sdt, table);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateCustom(string name, string id, string Sdt, string table, string macu)
        {
            string query = string.Format("UPDATE dbo.Custom SET name = N'{0}',id = N'{1}',sdt = N'{2}',[table] = N'{3}' WHERE id = N'{4}'", name, id, Sdt, table, macu);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteCustom(string id)
        {
            string query = string.Format("Delete Custom where id = N'{0}'", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
