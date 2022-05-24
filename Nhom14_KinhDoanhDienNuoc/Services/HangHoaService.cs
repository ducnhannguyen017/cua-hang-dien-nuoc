using MySql.Data.MySqlClient;
using Nhom14_KinhDoanhDienNuoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Nhom14_KinhDoanhDienNuoc.Services
{
    public class HangHoaService
    {
        string myConnectionString = "server=localhost;database=nhom14_qlcuahangdiennuoc;uid=root;pwd=;";
        public List<DanhMuc> GetAllDanhMuc()
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("select * from danhmuc", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<DanhMuc> listDanhMuc = new List<DanhMuc>();
                        while (reader.Read())
                        {
                            listDanhMuc.Add(
                                new DanhMuc() { 
                                    id = Int32.Parse(reader.GetString("id")),
                                    ten= reader.GetString("ten") 
                                });
                        }
                        return listDanhMuc;
                    }
                }
                conn.Close();
            }
        }
        public List<HangHoa> GetHangHoaByDanhMuc(int idDanhMuc)
        {
            string sqlCmd;
            if(idDanhMuc > 0)
            {
                sqlCmd = "select hanghoa.id, hanghoa.ma_hang_hoa, hanghoa.ten, danhmuc.ten AS ten_danh_muc, hanghoa.gia_nhap, hanghoa.gia_si, hanghoa.gia_le, hanghoa.don_vi, hanghoa.so_luong_ton_kho,hanghoa.id_danh_muc from hanghoa, danhmuc where hanghoa.id_danh_muc = danhmuc.id AND danhmuc.id =" + idDanhMuc.ToString();
            }
            else
            {
                sqlCmd = "select hanghoa.id, hanghoa.ma_hang_hoa, hanghoa.ten, danhmuc.ten AS ten_danh_muc, hanghoa.gia_nhap, hanghoa.gia_si, hanghoa.gia_le, hanghoa.don_vi, hanghoa.so_luong_ton_kho,hanghoa.id_danh_muc from hanghoa, danhmuc where hanghoa.id_danh_muc = danhmuc.id";
            }
            using (var conn = new MySqlConnection(myConnectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sqlCmd, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<HangHoa> listHangHoa = new List<HangHoa>();
                        while (reader.Read())
                        {
                            listHangHoa.Add(
                                new HangHoa() { 
                                    id = Int32.Parse(reader.GetString("id")),
                                    ten= reader.GetString("ten"),
                                    ten_danh_muc = reader.GetString("ten_danh_muc"),
                                    gia_nhap = Int32.Parse(reader.GetString("gia_nhap")),
                                    gia_si = Int32.Parse(reader.GetString("gia_si")),
                                    gia_le = Int32.Parse(reader.GetString("gia_le")),
                                    don_vi = reader.GetString("don_vi"),
                                    so_luong_ton_kho = Int32.Parse(reader.GetString("so_luong_ton_kho")),
                                    id_danh_muc = Int32.Parse(reader.GetString("id_danh_muc")),
                                    ma_hang_hoa = reader.GetString("ma_hang_hoa"),
                                });
                        }
                        return listHangHoa;
                    }
                }
                conn.Close();
            }
        }
        public List<HangHoa> GetHangHoaConTrongKhoByDanhMuc(int idDanhMuc)
        {
            string sqlCmd;
            if(idDanhMuc > 0)
            {
                sqlCmd = "select hanghoa.id, hanghoa.ma_hang_hoa, hanghoa.ten, danhmuc.ten AS ten_danh_muc, hanghoa.gia_nhap, hanghoa.gia_si, hanghoa.gia_le, hanghoa.don_vi, hanghoa.so_luong_ton_kho,hanghoa.id_danh_muc from hanghoa, danhmuc where hanghoa.so_luong_ton_kho >0 AND hanghoa.id_danh_muc = danhmuc.id AND danhmuc.id =" + idDanhMuc.ToString();
            }
            else
            {
                sqlCmd = "select hanghoa.id, hanghoa.ma_hang_hoa, hanghoa.ten, danhmuc.ten AS ten_danh_muc, hanghoa.gia_nhap, hanghoa.gia_si, hanghoa.gia_le, hanghoa.don_vi, hanghoa.so_luong_ton_kho,hanghoa.id_danh_muc from hanghoa, danhmuc where hanghoa.so_luong_ton_kho >0 AND hanghoa.id_danh_muc = danhmuc.id";
            }
            using (var conn = new MySqlConnection(myConnectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sqlCmd, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<HangHoa> listHangHoa = new List<HangHoa>();
                        while (reader.Read())
                        {
                            listHangHoa.Add(
                                new HangHoa() { 
                                    id = Int32.Parse(reader.GetString("id")),
                                    ten= reader.GetString("ten"),
                                    ten_danh_muc = reader.GetString("ten_danh_muc"),
                                    gia_nhap = Int32.Parse(reader.GetString("gia_nhap")),
                                    gia_si = Int32.Parse(reader.GetString("gia_si")),
                                    gia_le = Int32.Parse(reader.GetString("gia_le")),
                                    don_vi = reader.GetString("don_vi"),
                                    so_luong_ton_kho = Int32.Parse(reader.GetString("so_luong_ton_kho")),
                                    id_danh_muc = Int32.Parse(reader.GetString("id_danh_muc")),
                                    ma_hang_hoa = reader.GetString("ma_hang_hoa"),
                                });
                        }
                        return listHangHoa;
                    }
                }
                conn.Close();
            }
        }

        public void createReceipt(HoaDon hoaDon, ChiTietHoaDon[] cthd)
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                var str = "insert into hoadon(ngay_tao, ten_KH, thanh_tien, loai_gia, sdt_KH, dia_chi_KH, id_nhan_vien) values (STR_TO_DATE('" + hoaDon.ngay_tao + "','%m/%d/%Y %h:%i:%s %p'), '" + hoaDon.ten_KH + "', '" + hoaDon.thanh_tien + "','" + hoaDon.loai_gia + "', '" + hoaDon.sdt_KH + "', '" + hoaDon.dia_chi_KH + "', '" + hoaDon.id_nhan_vien + "' )";
                var strSelect = "select * from hoadon";
                int id = -1;
                conn.Open();
                using (var cmd = new MySqlCommand(str, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        //MessageBox.Show("Save Data");
                    }
                }
                using (var cmd = new MySqlCommand(strSelect, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = Int32.Parse(reader.GetString("id"));
                        }
                    }
                }
                conn.Close();
                conn.Open();
                foreach (var item in cthd)
                {
                    conn.Close();
                    conn.Open();
                    var str1 = "insert into chitiethoadon(id_hoa_don, id_hang_hoa, ten_HH, don_gia, so_luong, thanh_tien, ngay_tao) values ('" + id + "','" + item.id_hang_hoa + "', '" + item.ten_HH + "', '" + item.don_gia + "', '" + item.so_luong + "', '" + item.so_luong * item.don_gia + "', STR_TO_DATE('" + hoaDon.ngay_tao + "', '%m/%d/%Y %h:%i:%s %p'))";
                    using (var cmd = new MySqlCommand(str1, conn))
                    {
                        cmd.ExecuteReader();
                    }
                    conn.Close();
                    conn.Open();
                    List<HangHoa> hangHoa =  GetHangHoaByIdHangHoa(item.id_hang_hoa);
                    if (hangHoa.ToArray()[0].so_luong_ton_kho > 0)
                    {
                        int soluongtrongkho = hangHoa.ToArray()[0].so_luong_ton_kho - item.so_luong;
                        using (var cmd = new MySqlCommand("update hanghoa set so_luong_ton_kho=" + soluongtrongkho + " where id =" + item.id_hang_hoa, conn))
                        {
                            cmd.ExecuteReader();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hết Hàng");
                    }
                    
                }

                conn.Close();
                MessageBox.Show("Đã Lưu");
            }
        }
        public List<HangHoa> GetHangHoaByIdHangHoa(int idHangHoa)
        {
            string sqlCmd;
            if (idHangHoa > 0)
            {
                sqlCmd = "select * from hanghoa where id =" + idHangHoa.ToString();
            }
            else
            {
                sqlCmd = "select * from hanghoa";
            }
            using (var conn = new MySqlConnection(myConnectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sqlCmd, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<HangHoa> listHangHoa = new List<HangHoa>();
                        while (reader.Read())
                        {
                            listHangHoa.Add(
                                new HangHoa()
                                {
                                    id = Int32.Parse(reader.GetString("id")),
                                    ten = reader.GetString("ten"),
                                    id_danh_muc = Int32.Parse(reader.GetString("id_danh_muc")),
                                    gia_nhap = Int32.Parse(reader.GetString("gia_nhap")),
                                    gia_si = Int32.Parse(reader.GetString("gia_si")),
                                    gia_le = Int32.Parse(reader.GetString("gia_le")),
                                    don_vi = reader.GetString("don_vi"),
                                    so_luong_ton_kho = Int32.Parse(reader.GetString("so_luong_ton_kho")),
                                    ma_hang_hoa = reader.GetString("ma_hang_hoa"),
                                });
                        }
                        return listHangHoa;
                    }
                }
                conn.Close();
            }
        }

        public void nhapHangHoa(HangHoa hangHoa, NhapKho nhapKho)
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                string str = "INSERT INTO hanghoa(ten,ma_hang_hoa, gia_nhap, gia_si, gia_le, don_vi,so_luong_ton_kho, id_danh_muc) VALUES ('"+hangHoa.ten+"','"+hangHoa.ma_hang_hoa+"', '"+hangHoa.gia_nhap+"','"+hangHoa.gia_si+"', '"+hangHoa.gia_le+"', '"+hangHoa.don_vi+"', '"+hangHoa.so_luong_ton_kho+"','"+hangHoa.id_danh_muc+"')";
                conn.Open();
                using (var cmd = new MySqlCommand(str, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        //MessageBox.Show("Save Data");
                    }
                }
                conn.Close();
            }
            using (var conn = new MySqlConnection(myConnectionString))
            {
                string str = "INSERT INTO nhapkho(id_nha_cung_cap, id_hang_hoa, ten_HH, don_gia, so_luong, thanh_tien, ngay_tao) VALUES ('"+nhapKho.id_nha_cung_cap+"','"+nhapKho.id_hang_hoa+"', '"+nhapKho.ten_HH+"','"+nhapKho.don_gia+"', '"+nhapKho.so_luong+"', '"+nhapKho.thanh_tien+"',STR_TO_DATE('"+nhapKho.ngay_tao+ "','%m/%d/%Y %h:%i:%s %p'))";
                Console.WriteLine(str);
                conn.Open();
                using (var cmd = new MySqlCommand(str, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        //MessageBox.Show("Save Data");
                    }
                }
                conn.Close();
            }
        }
        public void updateSoLuongTrongKho(int so_luong, string ma_hang_hoa, NhapKho nhapKho)
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                string str = "update hanghoa set so_luong_ton_kho='"+so_luong+"'where ma_hang_hoa = '"+ma_hang_hoa+"'";
                conn.Open();
                using (var cmd = new MySqlCommand(str, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        //MessageBox.Show("Save Data");
                    }
                }
                conn.Close();
            }
            using (var conn = new MySqlConnection(myConnectionString))
            {
                string str = "INSERT INTO nhapkho(id_nha_cung_cap, id_hang_hoa, ten_HH, don_gia, so_luong, thanh_tien, ngay_tao) VALUES ('" + nhapKho.id_nha_cung_cap + "','" + nhapKho.id_hang_hoa + "', '" + nhapKho.ten_HH + "','" + nhapKho.don_gia + "', '" + nhapKho.so_luong + "', '" + nhapKho.thanh_tien + "',STR_TO_DATE('" + nhapKho.ngay_tao + "','%m/%d/%Y %h:%i:%s %p'))";
                conn.Open();
                using (var cmd = new MySqlCommand(str, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        //MessageBox.Show("Save Data");
                    }
                }
                conn.Close();
            }
        }

        public List<NhaCungCap> getAllNhaCungCap()
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("select * from nhacungcap", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<NhaCungCap> listNhaCungCap = new List<NhaCungCap>();
                        while (reader.Read())
                        {
                            listNhaCungCap.Add(
                                new NhaCungCap()
                                {
                                    id = Int32.Parse(reader.GetString("id")),
                                    ten = reader.GetString("ten"),
                                    sdt = reader.GetString("sdt"),
                                    email = reader.GetString("email"),
                                    dia_chi= reader.GetString("diachi")
                                });
                        }
                        return listNhaCungCap;
                    }
                }
                conn.Close();
            }
        }
        public List<HoaDon> getHoaDonByCreatedDate(string from, string to )
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                string str;
                if(from == "" || to == "")
                {
                    str = "select hoadon.id, hoadon.ngay_tao, hoadon.ten_KH, hoadon.thanh_tien, hoadon.loai_gia, hoadon.sdt_KH, hoadon.dia_chi_KH, users.ten as ten_nhan_vien from hoadon, users where hoadon.id_nhan_vien = users.id and users.role='ROLE_EMPLOYEE'";
                }
                else
                {
                    str = "select hoadon.id, hoadon.ngay_tao, hoadon.ten_KH, hoadon.thanh_tien, hoadon.loai_gia, hoadon.sdt_KH, hoadon.dia_chi_KH, users.ten as ten_nhan_vien from hoadon, users where hoadon.id_nhan_vien = users.id and users.role='ROLE_EMPLOYEE' and hoadon.ngay_tao>=STR_TO_DATE('" + from + "', '%m/%d/%Y') and hoadon.ngay_tao<=STR_TO_DATE('" + to + "', '%m/%d/%Y')";
                }
                Console.WriteLine(str);
                conn.Open();
                using (var cmd = new MySqlCommand(str, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<HoaDon> listHoaDon = new List<HoaDon>();
                        while (reader.Read())
                        {
                            listHoaDon.Add(
                                new HoaDon()
                                {
                                    id = Int32.Parse(reader.GetString("id")),
                                    ngay_tao = DateTime.Parse(reader.GetString("ngay_tao").ToString()),
                                    ten_KH = reader.GetString("ten_KH"),
                                    thanh_tien = Int32.Parse(reader.GetString("thanh_tien").ToString()),
                                    loai_gia = reader.GetString("loai_gia"),
                                    sdt_KH = reader.GetString("sdt_KH"),
                                    dia_chi_KH = reader.GetString("dia_chi_KH"),
                                    ten_nhan_vien = reader.GetString("ten_nhan_vien").ToString(),
                                });
                        }
                        return listHoaDon;
                    }
                }
                conn.Close();
            }
        }
        public List<HangHoaInSingleHoaDon> getChiTietHoaDonByCreatedDateAndIdHoaDon(int idHoaDon)
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("select hanghoa.ma_hang_hoa, hanghoa.ten, danhmuc.ten AS ten_danh_muc, hanghoa.don_vi, chitiethoadon.don_gia, chitiethoadon.so_luong, chitiethoadon.thanh_tien, chitiethoadon.ngay_tao from hanghoa, danhmuc, chitiethoadon where hanghoa.id_danh_muc=danhmuc.id and chitiethoadon.id_hang_hoa =hanghoa.id and chitiethoadon.id_hoa_don=" + idHoaDon.ToString(), conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<HangHoaInSingleHoaDon> list = new List<HangHoaInSingleHoaDon>();
                        while (reader.Read())
                        {
                            list.Add(
                                new HangHoaInSingleHoaDon()
                                {
                                    ma_hang_hoa = reader.GetString("ma_hang_hoa"),
                                    ten = reader.GetString("ten").ToString(),
                                    ten_danh_muc = reader.GetString("ten_danh_muc"),
                                    don_vi = reader.GetString("don_vi").ToString(),
                                    don_gia = Int32.Parse(reader.GetString("don_gia")),
                                    so_luong = Int32.Parse(reader.GetString("so_luong")),
                                    thanh_tien = Int32.Parse(reader.GetString("thanh_tien")),
                                    ngay_tao = DateTime.Parse(reader.GetString("ngay_tao").ToString()),
                                });
                        }
                        return list;
                    }
                }
                conn.Close();
            }
        }

        public void insertNhaCungCap(NhaCungCap nhaCungCap)
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                string str = "insert into nhacungcap (ten, sdt, email, diachi) values ('" + nhaCungCap.ten + "', '" + nhaCungCap.sdt + "','" + nhaCungCap.email + "','" + nhaCungCap.dia_chi+"')";
                Console.WriteLine(str);
                conn.Open();
                using (var cmd = new MySqlCommand(str, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }
                conn.Close();
            }
        }
        public void updateNhaCungCap(NhaCungCap nhaCungCap, int id)
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                string str = "update nhacungcap set ten='" + nhaCungCap.ten + "',sdt='" + nhaCungCap.sdt + "', email='" + nhaCungCap.email + "',diachi='" + nhaCungCap.dia_chi + "' where id ='" + id + "'";
                Console.WriteLine(str);
                conn.Open();
                using (var cmd = new MySqlCommand(str, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }
                conn.Close();
            }
        }
        public void deleteNhaCungCap(int id)
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                string str = "delete from nhaCungCap where id ='" + id + "'";
                conn.Open();
                using (var cmd = new MySqlCommand(str, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }
                conn.Close();
            }
        }

        public List<HoaDonXuatNhapTheoThang> selectThuNhapTheoThang(int nam)
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                string str = "select MONTH(hoadon.ngay_tao) as thang, SUM(hoadon.thanh_tien) as tong_thu_nhap from hoadon where YEAR(hoadon.ngay_tao)="+nam+" GROUP BY thang ";
                conn.Open();
                using (var cmd = new MySqlCommand(str, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<HoaDonXuatNhapTheoThang> list = new List<HoaDonXuatNhapTheoThang>();
                        while (reader.Read())
                        {
                            list.Add(new HoaDonXuatNhapTheoThang()
                            {
                                thang=Int32.Parse(reader.GetString("thang")),
                                tong_thu_nhap=Int32.Parse(reader.GetString("tong_thu_nhap")),
                            });
                        }
                        return list;
                    }
                }
                conn.Close();
            }
        }
        public List<HoaDonXuatNhapTheoThang> selectNhapKhoTheoThang(int nam)
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                string str = "SELECT MONTH(nhapkho.ngay_tao) as thang, SUM(nhapkho.thanh_tien) as tong_thu_nhap FROM `nhapkho` where YEAR(nhapkho.ngay_tao)="+nam+" GROUP BY thang";
                conn.Open();
                using (var cmd = new MySqlCommand(str, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<HoaDonXuatNhapTheoThang> list = new List<HoaDonXuatNhapTheoThang>();
                        while (reader.Read())
                        {
                            list.Add(new HoaDonXuatNhapTheoThang()
                            {
                                thang=Int32.Parse(reader.GetString("thang")),
                                tong_thu_nhap=Int32.Parse(reader.GetString("tong_thu_nhap")),
                            });
                        }
                        return list;
                    }
                }
                conn.Close();
            }
        }

        public List<HoaDonXuatNhapTheoTuan> getHoaDonByCreatedDateGroupDayOfWeek(string from, string to)
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                string str;
                if (from == "" || to == "")
                {
                    str = "select hoadon.id, hoadon.ngay_tao, hoadon.ten_KH, hoadon.thanh_tien, hoadon.loai_gia, hoadon.sdt_KH, hoadon.dia_chi_KH, users.ten as ten_nhan_vien from hoadon, users where hoadon.id_nhan_vien = users.id and users.role='ROLE_EMPLOYEE'";
                }
                else
                {
                    str = "select DAYOFWEEK(hoadon.ngay_tao) AS day_of_week, hoadon.thanh_tien from hoadon where hoadon.ngay_tao>=STR_TO_DATE('" + from + "', '%m/%d/%Y') and hoadon.ngay_tao<=STR_TO_DATE('" + to + "', '%m/%d/%Y') group by day_of_week";
                }
                Console.WriteLine(str);
                conn.Open();
                using (var cmd = new MySqlCommand(str, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<HoaDonXuatNhapTheoTuan> list = new List<HoaDonXuatNhapTheoTuan>();
                        while (reader.Read())
                        {
                            int day_of_week = Int32.Parse(reader.GetString("day_of_week").ToString());
                            list.Add(
                                new HoaDonXuatNhapTheoTuan()
                                {
                                    day_of_week = day_of_week==1?"Chủ Nhập":"Thứ "+day_of_week,
                                    thanh_tien = Int32.Parse(reader.GetString("thanh_tien").ToString())
                                });
                        }
                        return list;
                    }
                }
                conn.Close();
            }
        }
        public ThuChi getTongThuNhap(int thang)
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                string str = "select SUM(thanh_tien) as tong from hoadon where MONTH(hoadon.ngay_tao)='" + thang + "'";
                conn.Open();
                using (var cmd = new MySqlCommand(str, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        ThuChi thuChi = new ThuChi();
                        while (reader.Read())
                        {
                            thuChi = new ThuChi()
                            {
                                tong = Int32.Parse(reader.GetString("tong").ToString())
                            };
                        }
                        return thuChi;
                    }
                }
                conn.Close();
            }
        }
        public ThuChi getTongChiTieu(int thang)
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                string str = "select SUM(thanh_tien) as tong from nhapkho where MONTH(nhapkho.ngay_tao)='" + thang + "'";
                conn.Open();
                using (var cmd = new MySqlCommand(str, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        ThuChi chiTieu = new ThuChi();
                        while (reader.Read())
                        {
                            chiTieu = new ThuChi()
                            {
                                tong = Int32.Parse(reader.GetString("tong").ToString())
                            };
                        }
                        return chiTieu;
                    }
                }
                conn.Close();
            }
        }
        public User login(string username, string password)
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                string str = "select * from users where username='"+username+"' and password='"+password+"'";
                conn.Open();
                using (var cmd = new MySqlCommand(str, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        User user = new User();
                        while (reader.Read())
                        {
                            user = new User()
                            {
                                username = reader.GetString("username").ToString(),
                                password = reader.GetString("password").ToString(),
                                role = reader.GetString("role").ToString()
                            };
                        }
                        return user;
                    }
                }
                conn.Close();
            }
        }
    }
}
