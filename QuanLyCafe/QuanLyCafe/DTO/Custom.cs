using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafe.DTO
{
    public class Custom
    {
        public Custom(string id, string name, string sdt, string table)
        {
            this.ID = id;
            this.Name = name;
            this.SDT = sdt;
            this.Table = table;
        }

        public Custom(DataRow row)
        {
            this.ID = row["id"].ToString();
            this.Name = row["name"].ToString();
            this.Table = row["table"].ToString();
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

        private string table;

        public string Table
        {
            get { return table; }
            set { table = value; }
        }
        private string sdt;

        public string SDT
        {
            get { return sdt; }
            set { sdt = value; }
        }
    }
}
