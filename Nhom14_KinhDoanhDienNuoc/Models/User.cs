using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom14_KinhDoanhDienNuoc.Models
{
    public class User
    {
        public int id { get; set; }
        public string ten { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string sdt { get; set; }
        public string diachi { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public bool isLogined { get; set; }
    }
}
