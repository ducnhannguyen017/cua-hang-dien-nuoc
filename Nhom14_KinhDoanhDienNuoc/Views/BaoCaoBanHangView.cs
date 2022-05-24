using Nhom14_KinhDoanhDienNuoc.Models;
using Nhom14_KinhDoanhDienNuoc.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Nhom14_KinhDoanhDienNuoc.Views
{
    public partial class BaoCaoBanHangView : Form
    {
        HangHoaService hangHoaService = new HangHoaService();
        UserService userService = new UserService();
        List<HoaDon> listHoaDon = new List<HoaDon>();
        List<HangHoaInSingleHoaDon> listHangHoaInSingleHoaDon = new List<HangHoaInSingleHoaDon>();
        DataTable dtHangHoaInSingleHoaDon = new DataTable();
        List<User> listNhanVien = new List<User>();
        public BaoCaoBanHangView()
        {
            InitializeComponent();
            preData();
        }
        public void preData()
        {
            listHoaDon = hangHoaService.getHoaDonByCreatedDate("","");

            dgvHoaDonBanHang.DataSource = listHoaDon;
            dgvHoaDonBanHang.Columns[0].Visible = false;
            dgvHoaDonBanHang.Columns[1].HeaderText = "Ngày Tạo";
            //dgvHoaDonBanHang.Columns[1].Width = 200;
            dgvHoaDonBanHang.Columns[2].HeaderText = "Tên Khách Hàng";
            dgvHoaDonBanHang.Columns[3].HeaderText = "Tổng Tiền";
            dgvHoaDonBanHang.Columns[4].HeaderText = "Loại Giá";
            dgvHoaDonBanHang.Columns[5].HeaderText = "SĐT Khách";
            dgvHoaDonBanHang.Columns[6].HeaderText = "Địa Chỉ";
            dgvHoaDonBanHang.Columns[7].Visible = false;
            dgvHoaDonBanHang.Columns[8].HeaderText = "Nhân Viên Bán";
            dgvHoaDonBanHang.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dtHangHoaInSingleHoaDon = new DataTable();
            dtHangHoaInSingleHoaDon.Columns.Add("Mã HH", typeof(string));
            dtHangHoaInSingleHoaDon.Columns.Add("Tên", typeof(string));
            dtHangHoaInSingleHoaDon.Columns.Add("Loại", typeof(string));
            dtHangHoaInSingleHoaDon.Columns.Add("Đơn Vị", typeof(string));
            dtHangHoaInSingleHoaDon.Columns.Add("Đơn Giá", typeof(string));
            dtHangHoaInSingleHoaDon.Columns.Add("Số Lượng", typeof(string));
            dtHangHoaInSingleHoaDon.Columns.Add("Thành Tiền", typeof(string));
            dgvHangHoaTrongHoaDon.DataSource = dtHangHoaInSingleHoaDon;

            tbNgayTao.Text = "";
            tbTenKH.Text = "";
            tbTongTien.Text = "";
            tbLoaiGia.Text = "";
            tbSDT.Text = "";
            tbDiaChi.Text ="";
            tbNhanVien.Text ="";
        }

        private void dgvHoaDonBanHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine(new JavaScriptSerializer().Serialize(e));
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvHoaDonBanHang.Rows[e.RowIndex];
                tbNgayTao.Text = row.Cells[1].Value.ToString();
                tbTenKH.Text = row.Cells[2].Value.ToString();
                tbTongTien.Text = row.Cells[3].Value.ToString();
                tbLoaiGia.Text = row.Cells[4].Value.ToString();
                tbSDT.Text = row.Cells[5].Value.ToString();
                tbDiaChi.Text = row.Cells[6].Value.ToString();
                tbNhanVien.Text = row.Cells[8].Value.ToString();

                listHangHoaInSingleHoaDon = hangHoaService.getChiTietHoaDonByCreatedDateAndIdHoaDon(Int32.Parse(row.Cells[0].Value.ToString()));
                dtHangHoaInSingleHoaDon.Rows.Clear();
                listHangHoaInSingleHoaDon.ForEach(i =>
                {
                    dtHangHoaInSingleHoaDon.Rows.Add(i.ma_hang_hoa, i.ten, i.ten_danh_muc, i.don_vi.ToString(), i.don_gia, i.so_luong, i.thanh_tien);
                });
                dgvHangHoaTrongHoaDon.DataSource = dtHangHoaInSingleHoaDon;

            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            preData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] to = dtpDenNgay.Value.ToString().Split(' ')[0].Split('/');
            int toDate = Int32.Parse(to[1]) + 1;
            listHoaDon = hangHoaService.getHoaDonByCreatedDate(dtpTuNgay.Value.ToString().Split(' ')[0],to[0]+"/"+toDate+"/"+to[2]);
            dgvHoaDonBanHang.DataSource = listHoaDon;
            Console.WriteLine(new JavaScriptSerializer().Serialize(listHoaDon));
        }
    }
}
