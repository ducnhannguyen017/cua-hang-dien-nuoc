using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom14_KinhDoanhDienNuoc.Models
{
    public class HangHoaInSingleHoaDon
    {
        public string ma_hang_hoa { get; set; }
        public string ten { get; set; }
        public string ten_danh_muc { get; set; }
        public string don_vi { get; set; }
        public int don_gia { get; set; }
        public int so_luong { get; set; }
        public int thanh_tien { get; set; }
        public DateTime ngay_tao { get; set; }
    }
}
