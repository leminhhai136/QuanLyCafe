using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafe.DTO
{
    public class Supplier
    {
        public Supplier(string id, string name, string sdt)
        {
            this.ID = id;
            this.Name = name;
            this.SDT = sdt;
        }

        public Supplier(DataRow row)
        {
            this.Name = row["name"].ToString();
            this.ID = row["id"].ToString();
            this.SDT = row["Sdt"].ToString();

        }

        private string name;

        public string Name
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
        private string sdt;

        public string SDT
        {
            get { return sdt; }
            set { sdt = value; }
        }
    }
}

