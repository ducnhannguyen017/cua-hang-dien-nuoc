using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom14_KinhDoanhDienNuoc.Models
{
    public class NhapKho
    {
        public int id { get; set; }
        public int id_nha_cung_cap { get; set; }
        public int id_hang_hoa { get; set; }
        public string ten_HH { get; set; }
        public int don_gia { get; set; }
        public int so_luong { get; set; }   
        public int thanh_tien { get; set; }
        public DateTime ngay_tao { get; set; }
    }
}
