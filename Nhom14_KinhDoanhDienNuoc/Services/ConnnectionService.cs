using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom14_KinhDoanhDienNuoc.Services
{
    public class ConnnectionService
    {
        string myConnectionString = "server=localhost;database=nhom14_qlcuahangdiennuoc;uid=root;pwd=;";
        public MySqlDataReader excute<T>(string sqlCommand)
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sqlCommand, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {

                        return reader;
                    }
                }
            }
        }

    }
}
