using ClosedXML.Excel;
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
    public partial class KhoHangView : Form
    {
        HangHoaService hangHoaService = new HangHoaService();
        UserService userService = new UserService();
        List<HangHoa> listHangHoa = new List<HangHoa>();
        List<DanhMuc> listDanhMuc = new List<DanhMuc>();
        List<NhaCungCap> listNhaCungCap = new List<NhaCungCap>();
        List<User> listManager = new List<User>();
        NhaCungCap nhaCungCap = new NhaCungCap();
        public KhoHangView()
        {
            InitializeComponent();
            preData();
        }
        public void preData()
        {
            listHangHoa = hangHoaService.GetHangHoaByDanhMuc(-1);
            listDanhMuc = hangHoaService.GetAllDanhMuc();
            listNhaCungCap = hangHoaService .getAllNhaCungCap();
            listManager = userService.getUserByRole("ROLE_MANAGER");
            dgvKhoHang.DataSource = listHangHoa;
            dgvKhoHang.Columns[0].Visible = false;
            dgvKhoHang.Columns[1].HeaderText = "Tên";
            dgvKhoHang.Columns[2].HeaderText = "Giá Nhập";
            dgvKhoHang.Columns[3].HeaderText = "Giá Sỉ";
            dgvKhoHang.Columns[4].HeaderText = "Giá Lẻ";
            dgvKhoHang.Columns[5].HeaderText = "Đơn Vị";
            dgvKhoHang.Columns[8].HeaderText = "Tồn Kho";

            dgvKhoHang.Columns[7].Visible = false;
            dgvKhoHang.Columns[6].Visible = false;
            dgvKhoHang.Columns[11].Visible = false;
            dgvKhoHang.Columns[9].HeaderText = "Loại";
            dgvKhoHang.Columns[10].HeaderText = "Mã Hàng Hóa";
            cbLoaiHang.DataSource = listDanhMuc;
            cbLoaiHang.DisplayMember = "ten";
            cbLoaiHang.SelectedIndex = -1;
            cbMatHang.DataSource = listHangHoa;
            cbMatHang.DisplayMember = "ten";
            cbMatHang.SelectedIndex = -1;

            lbSoLuongMatHang.Text = listHangHoa.Count.ToString();

            cbNhaCungCap.DataSource = listNhaCungCap;
            cbNhaCungCap.DisplayMember = "ten";
            nhaCungCap = (NhaCungCap)cbNhaCungCap.SelectedItem;
            tbDiaChi.Text = nhaCungCap.dia_chi;
            tbSDT.Text = nhaCungCap.sdt;
            tbEmail.Text = nhaCungCap.email;

            tbMaPhieu.Text = "MPDN" + String.Join("", dtpNgayLap.Value.ToString().Split(' ')[0].Split('/'));

            cbNguoiLap.DataSource = listManager;
            cbNguoiLap.DisplayMember = "ten";
        }

        private void cbLoaiHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            DanhMuc danhMuc = (DanhMuc)comboBox.SelectedItem;

            if (cbLoaiHang.SelectedIndex >= 0)
            {
                listHangHoa = hangHoaService.GetHangHoaByDanhMuc(danhMuc.id);
            }
            else
            {
                listHangHoa = hangHoaService.GetHangHoaByDanhMuc(-1);
            }
            lbSoLuongMatHang.Text = listHangHoa.Count.ToString();

            dgvKhoHang.DataSource = listHangHoa;
            cbMatHang.DataSource = listHangHoa;
            cbMatHang.SelectedIndex = -1;
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            preData();
        }

        private void btImportExcel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    DataTable dt = new DataTable();
                    using (XLWorkbook workbook = new XLWorkbook(dialog.FileName))
                    {
                        bool isFirstRow = true;
                        var rows = workbook.Worksheet(1).RowsUsed();
                        //try
                        //{
                            foreach (var row in rows)
                            {
                                if (isFirstRow)
                                {
                                    isFirstRow = false;
                                }
                                else
                                {
                                    int i = 0;
                                    var cell = row.Cells().ToArray();
                                    int id_danh_muc = listDanhMuc.ToList().Find(x => x.ten == cell[6].Value.ToString()).id;
                                    HangHoa hangHoa = listHangHoa.ToList().Find(x => x.ma_hang_hoa == cell[7].Value.ToString());
                                    Console.WriteLine(new JavaScriptSerializer().Serialize(hangHoa));
                                    
                                    if (hangHoa!=null)
                                    {
                                        NhapKho nhapKho = new NhapKho()
                                        {
                                            id_hang_hoa = hangHoa.id,
                                            id_nha_cung_cap = nhaCungCap.id,
                                            ten_HH = cell[0].Value.ToString(),
                                            don_gia = Int32.Parse(cell[1].Value.ToString()),
                                            so_luong = Int32.Parse(cell[5].Value.ToString()),
                                            thanh_tien = Int32.Parse(cell[1].Value.ToString()) * Int32.Parse(cell[5].Value.ToString()),
                                            ngay_tao = dtpNgayLap.Value,
                                        };
                                        int so_luong_update = hangHoa.so_luong_ton_kho + Int32.Parse(cell[5].Value.ToString());
                                        hangHoaService.updateSoLuongTrongKho(so_luong_update, cell[7].Value.ToString(), nhapKho);
                                    }
                                    else
                                    {
                                        List<HangHoa> hangHoas = hangHoaService.GetHangHoaByDanhMuc(-1);
                                        int id_hang_hoa = hangHoas.ToArray()[0].id;
                                        NhapKho nhapKho = new NhapKho()
                                        {
                                            id_hang_hoa = id_hang_hoa,
                                            id_nha_cung_cap = nhaCungCap.id,
                                            ten_HH = cell[0].Value.ToString(),
                                            don_gia = Int32.Parse(cell[1].Value.ToString()),
                                            so_luong = Int32.Parse(cell[5].Value.ToString()),
                                            thanh_tien = Int32.Parse(cell[1].Value.ToString()) * Int32.Parse(cell[5].Value.ToString()),
                                            ngay_tao = dtpNgayLap.Value,
                                        };
                                        HangHoa insertHangHoa = new HangHoa()
                                        {
                                            ten = cell[0].Value.ToString(),
                                            gia_nhap = Int32.Parse(cell[1].Value.ToString()),
                                            gia_si = Int32.Parse(cell[2].Value.ToString()),
                                            gia_le = Int32.Parse(cell[3].Value.ToString()),
                                            don_vi = cell[4].Value.ToString(),
                                            so_luong_ton_kho = Int32.Parse(cell[5].Value.ToString()),
                                            id_danh_muc = id_danh_muc,
                                            ma_hang_hoa = cell[7].Value.ToString(),
                                        };
                                        hangHoaService.nhapHangHoa(insertHangHoa, nhapKho);
                                        //Console.WriteLine(new JavaScriptSerializer().Serialize(hangHoa));
                                    }
                                    

                                }
                            }
                            MessageBox.Show("Đã Lưu");
                            preData();
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show("Error");
                        //}
                    }
                }
            }
        }

        private void cbNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbNhaCungCap.DataSource = listNhaCungCap;
            cbNhaCungCap.DisplayMember = "ten";
            nhaCungCap = (NhaCungCap)cbNhaCungCap.SelectedItem;
            tbDiaChi.Text = nhaCungCap.dia_chi;
            tbSDT.Text = nhaCungCap.sdt;
            tbEmail.Text = nhaCungCap.email;
        }

        private void btExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Tên", typeof(string));
            dt.Columns.Add("Giá Nhập", typeof(string));
            dt.Columns.Add("Giá Sỉ", typeof(string));
            dt.Columns.Add("Giá Lẻ", typeof(string));
            dt.Columns.Add("Đơn Vị", typeof(string));
            dt.Columns.Add("Tồn Kho", typeof(string));
            dt.Columns.Add("Loại", typeof(string));
            dt.Columns.Add("Mã Hàng Hóa", typeof(string));
            listHangHoa.ForEach(hangHoa =>
            {
                string ten_danh_muc = listDanhMuc.Find(x => x.id == hangHoa.id_danh_muc).ten;
                dt.Rows.Add(hangHoa.ten, hangHoa.gia_nhap, hangHoa.gia_si, hangHoa.gia_le, hangHoa.don_vi, hangHoa.so_luong_ton_kho, ten_danh_muc, hangHoa.ma_hang_hoa);
            });
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            workbook.Worksheets.Add(dt, "Employees");
                            workbook.SaveAs(sfd.FileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

    }
}
