using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom14_KinhDoanhDienNuoc.Models
{
    public class HoaDon
    {
        public int id { get; set; }
        public DateTime ngay_tao { get; set; }
        public string ten_KH { get; set; }
        public int thanh_tien { get; set; }
        public string loai_gia { get; set; }
        public string sdt_KH { get; set; }
        public string dia_chi_KH { get; set; }
        public int id_nhan_vien { get; set; }
        public string ten_nhan_vien { get; set; }

    }
    public class HoaDonXuatNhapTheoThang
    {
        public int thang { get; set; }
        public int tong_thu_nhap { get; set; }
    }
    public class HoaDonXuatNhapTheoTuan
    {
        public string day_of_week { get; set; }
        public int thanh_tien { get; set; }
    }
    public class ThuChi 
    {
        public int tong { get; set; }
    }
}
