using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
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
    public partial class BanHangView : Form
    {
        HangHoaService hangHoaService = new HangHoaService();
        UserService userService = new UserService();
        List<HangHoa> listHangHoa;
        List<HangHoa> listBanHang;
        List<User> employees;
        //DataTable dtListHangHoa;
        DataGridViewComboBoxColumn dgvCmb;
        int rowClick = -1;
        int so_luong = 1;

        public BanHangView()
        {
            InitializeComponent();
            preData();
        }

        private void preData()
        {
            listHangHoa = hangHoaService.GetHangHoaConTrongKhoByDanhMuc(-1);
            cbHangHoa.DataSource = listHangHoa;
            cbHangHoa.DisplayMember = "ten_hien_thi";

            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "ID" });
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Hàng Hóa" });
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Mã Hàng Hóa" });
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Đơn vị tính" });
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Số Lượng" });
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Đơn Giá" });
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Thành tiền" });

            DataGridViewButtonColumn xoa = new DataGridViewButtonColumn();
            xoa.Text = "Xóa";
            xoa.UseColumnTextForButtonValue = true;
            dgvHangHoa.Columns.Add(xoa);

            //dgvHangHoa.Columns["xoaColumn"].DisplayIndex = 7;
            //dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn());
            //dgvHangHoa.Columns[1].Width = 300;
            //dgvHangHoa.Columns[0].Visible = false;

            //cmbobox người lập hóa đơn
            employees = userService.getUserByRole("ROlE_EMPLOYEE");
            cbNguoiLap.DataSource = employees;
            cbNguoiLap.DisplayMember = "ten";
            //cb loai gia
            cbLoaiGia.Items.Add("Giá Sỉ");
            cbLoaiGia.Items.Add("Giá Lẻ");
            cbLoaiGia.SelectedIndex = 0;

            tbMaPhieu.Text ="MPDN"+String.Join("", dtpNgayLap.Value.ToString().Split(' ')[0].Split('/')) ;
            Console.WriteLine(new JavaScriptSerializer().Serialize(listHangHoa));

        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is ComboBox)
            {
                ComboBox combo = (ComboBox)e.Control;
                combo.DropDownStyle = ComboBoxStyle.DropDown;
                combo.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            }
        }

        private void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            //Console.WriteLine(new JavaScriptSerializer().Serialize(sender));
            var row = listHangHoa.ToArray()[combo.SelectedIndex];
            Console.WriteLine(new JavaScriptSerializer().Serialize(row));
            //dtListHangHoa.Rows.Add( row.don_vi, "3", row.gia_nhap, row.gia_nhap * 3);

        }

        private void btNhap_Click(object sender, EventArgs e)
        {
            HangHoa hangHoa = (HangHoa)cbHangHoa.SelectedItem;
            int gia;
            if (cbLoaiGia.SelectedItem == "Giá Sỉ")
            {
                gia = hangHoa.gia_si;
            }
            else
            {
                gia = hangHoa.gia_le;
            }

            int i =0;
            System.Boolean exist = false;
            int index = 0;
            foreach (DataGridViewRow row in dgvHangHoa.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    Console.WriteLine( hangHoa.ma_hang_hoa);
                    if (Int32.Parse(row.Cells[0].Value.ToString()) == hangHoa.id)
                    {
                        exist = true;
                        index = i;
                    }
                }
                i++;
            }

            if (exist == false)
            {
                so_luong = 1;
                dgvHangHoa.Rows.Add(hangHoa.id, hangHoa.ten, hangHoa.ma_hang_hoa, hangHoa.don_vi, so_luong.ToString(), gia, gia* so_luong);

            }
            else
            {
                so_luong++;
                dgvHangHoa.Rows[index].Cells[4].Value = so_luong.ToString();
                dgvHangHoa.Rows[index].Cells[6].Value = so_luong* gia;
            }
            int thanh_tien = 0;
            foreach (DataGridViewRow row in dgvHangHoa.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    thanh_tien += Int32.Parse(row.Cells[6].Value.ToString());
                }
                //More code here
            }
            tbThanhTien.Text = thanh_tien.ToString();
            tbTongTien.Text = thanh_tien.ToString();

            Console.WriteLine("--------------------");
        }

        private void btThanhToan_Click(object sender, EventArgs e)
        {
            if (tbKhachHang.Text == "" || tbDiaChi.Text == "" || tbSDT.Text == "")
            {
                MessageBox.Show("Thông tin khách hàng chưa đủ");
            }
            else if (tbThanhTien.Text != "")
            {
                HoaDon hoaDon = new HoaDon()
                {
                    ngay_tao = dtpNgayLap.Value,
                    ten_KH = tbKhachHang.Text,
                    thanh_tien = Int32.Parse(tbThanhTien.Text),
                    loai_gia = cbLoaiGia.SelectedItem.ToString(),
                    sdt_KH = tbSDT.Text,
                    dia_chi_KH = tbDiaChi.Text,
                    id_nhan_vien = employees[cbNguoiLap.SelectedIndex].id

                };
                List<ChiTietHoaDon> cthd = new List<ChiTietHoaDon>();
                foreach (DataGridViewRow row in dgvHangHoa.Rows)
                {
                    if (row.Cells[4].Value != null)
                    {
                        cthd.Add(new ChiTietHoaDon()
                        {
                            id_hang_hoa = Int32.Parse(row.Cells[0].Value.ToString()),
                            ten_HH = row.Cells[1].Value.ToString(),
                            don_gia = Int32.Parse(row.Cells[5].Value.ToString()),
                            so_luong = Int32.Parse(row.Cells[4].Value.ToString()),
                            thanh_tien = Int32.Parse(row.Cells[6].Value.ToString()),
                            ngay_tao = dtpNgayLap.Value
                        });
                    }
                }
                tbDiaChi.Text = "";
                tbKhachHang.Text = "";
                tbMaPhieu.Text = "";
                tbSDT.Text = "";
                dgvHangHoa.Rows.Clear();
                hangHoaService.createReceipt(hoaDon, cthd.ToArray());
            }
            
            else
            {
                MessageBox.Show("Chưa nhập hàng bán");
            }
        }

        private void btNhap_Click_1(object sender, EventArgs e)
        {

        }

        private void dgvHangHoa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                rowClick = e.RowIndex;
                if (e.ColumnIndex == 7&& dgvHangHoa.RowCount-1!=e.RowIndex)
                {
                    Console.WriteLine("204");
                    dgvHangHoa.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void dgvHangHoa_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                rowClick = e.RowIndex;
            }
        }

        private void btGiam_Click(object sender, EventArgs e)
        {
            if(rowClick >= 0)
            {
                var row = dgvHangHoa.Rows[rowClick];
                var oldValue = row.Cells[4].Value;
                if (Int32.Parse(row.Cells[4].Value.ToString()) >= 1)
                {
                    so_luong--;
                    row.Cells[4].Value = so_luong;
                    row.Cells[6].Value = so_luong * Int32.Parse(row.Cells[5].Value.ToString());
                }
                else
                {
                    dgvHangHoa.Rows.RemoveAt(rowClick);
                }

            }
            else
            {
                MessageBox.Show("Chọn hàng để thao tác");
            }
        }
    }
}
