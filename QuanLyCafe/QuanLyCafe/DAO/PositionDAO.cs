using QuanLyCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuanLyCafe.DAO.PositionDAO;

namespace QuanLyCafe.DAO
{
    public class PositionDAO
    {
       

            private static PositionDAO instance;
            public static PositionDAO Instance
            {
                get { if (instance == null) instance = new PositionDAO(); return PositionDAO.instance; }
                private set { PositionDAO.instance = value; }
            }

            private PositionDAO() { }

            public List<Position> GetListPosition()
            {
                List<Position> list = new List<Position>();

                string query = "select name from Position";

                DataTable data = DataProvider.Instance.ExecuteQuery(query);

                foreach (DataRow item in data.Rows)
                {
                    Position position = new Position(item);
                    list.Add(position);
                }

                return list;
            }
        
    }
}
