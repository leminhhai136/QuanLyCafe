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
    public class StaffDAO
    {
        private static StaffDAO instance;

        public static StaffDAO Instance
        {
            get { if (instance == null) instance = new StaffDAO(); return StaffDAO.instance; }
            private set { StaffDAO.instance = value; }
        }

        private StaffDAO() { }

        public List<Staff> GetListStaff()
        {
            List<Staff> list = new List<Staff>();

            string query = "select * from Staff ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Staff staff = new Staff(item);
                list.Add(staff);
            }

            return list;
        }
        public static bool Kttrungma(string id)
        {
            string query = "select * from Staff where id = N'"+id+"'";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            if (data.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public List<Staff> SearchStaffByName(string name)
        {
            List<Staff> list = new List<Staff>();

            string query = string.Format("SELECT * FROM dbo.Staff WHERE dbo.fuConvertToUnsign1(name) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Staff staff = new Staff(item);
                list.Add(staff);
            }

            return list;
        }
        public bool AddStaff(string name, string id, string dateOfBirth, string Sdt, string Position)
        {
            string query = string.Format("INSERT dbo.Staff ( name, id, dateOfBirth, Sdt, Position )VALUES  ( N'{0}',N'{1}',N'{2}',N'{3}',N'{4}')", name, id, dateOfBirth,Sdt,Position);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateStaff(string name, string id, string dateOfBirth, string Sdt, string Position, string macu)
        {
            string query = string.Format("UPDATE dbo.Staff SET name = N'{0}',id = N'{1}',dateOfBirth = N'{2}',Sdt = N'{3}',Position = N'{4}' WHERE id = N'{5}'", name, id, dateOfBirth, Sdt, Position, macu);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteStaff(string macu)
        {
            string query = string.Format("Delete Staff where id = N'{0}'", macu);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
