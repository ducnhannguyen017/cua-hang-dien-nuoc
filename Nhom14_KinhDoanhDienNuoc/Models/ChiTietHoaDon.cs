using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom14_KinhDoanhDienNuoc.Models
{
    public class ChiTietHoaDon
    {
        public int id { get; set; }
        public string ten_HH { get; set; }
        public int id_hoa_don { get; set; }
        public int id_hang_hoa { get; set; }
        public int don_gia { get; set; }
        public int so_luong { get; set; }
        public int thanh_tien { get; set; }
        public DateTime ngay_tao { get; set; }
        public int gia_si { get; set; }
        public int gia_le { get; set; }
    }
}
