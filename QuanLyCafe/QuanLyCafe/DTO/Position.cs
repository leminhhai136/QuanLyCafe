using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafe.DTO
{
    public class Position
    {
        public Position(string name)
        {
            this.Name = name;
        }

        public Position(DataRow row)
        {
            this.Name = row["name"].ToString();
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
