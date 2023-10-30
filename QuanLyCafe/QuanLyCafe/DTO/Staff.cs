using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafe.DTO
{
    public class Staff
    {
        public Staff(string id, string name, string sdt, string dateOfBirth, string position)
        {
            this.ID = id;
            this.Ten = name;
            this.ngaysinh = dateOfBirth;
            this.SDT = sdt;
            this.chucvu = position;
        }

        public Staff(DataRow row)
        {          
            this.ID = row["id"].ToString();
            this.Ten = row["tên"].ToString();
            this.ngaysinh = row["ngày sinh"].ToString();
            this.SDT = row["Sdt"].ToString();
            this.chucvu = row["chức vụ"].ToString();

        }

        

        private string name;

        public string Ten
        {
            get { return name; }
            set { name = value; }
        }

        private string iD;

        public string ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string dateOfBirth;

        public string ngaysinh
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }
        private string sdt;

        public string SDT
        {
            get { return sdt; }
            set { sdt = value; }
        }
        private string position;
        public string chucvu
        {
            get { return position; }
            set { position = value; }
        }
    }
}

