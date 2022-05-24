using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom14_KinhDoanhDienNuoc.Models
{
    public class HangHoa
    {
        public int id { get; set; }
        public string ten { get; set; }
        public int gia_nhap { get; set; }
        public int gia_si { get; set; }
        public int gia_le { get; set; }
        public string don_vi { get; set; }
        public int so_luong { get; set; }
        public int id_danh_muc { get; set; }
        public int so_luong_ton_kho { get; set; }
        public string ten_danh_muc { get; set; }
        public string ma_hang_hoa { get; set; }
        public string ten_hien_thi => ten +"("+ ma_hang_hoa+")";
    }
}
