using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Hanssens.Net;
using Nhom14_KinhDoanhDienNuoc.Models;
using Nhom14_KinhDoanhDienNuoc.Services;
using Nhom14_KinhDoanhDienNuoc.Views;
using Quanlydiennuoc;

namespace Nhom14_KinhDoanhDienNuoc
{
    public partial class LayoutForm : Form
    {
        //Fields
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        private List<HoaDonXuatNhapTheoThang> hoaDonThuNhapTheoThangs = new List<HoaDonXuatNhapTheoThang>();
        private List<HoaDonXuatNhapTheoThang> hoaDonNhapKhoTheoThangs = new List<HoaDonXuatNhapTheoThang>();
        HangHoaService hangHoaService = new HangHoaService();
        LocalStorage storage = new LocalStorage();

        //Constructor
        public LayoutForm()
        {
            InitializeComponent();
            preData();
            
        }

        public void preData()
        {
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            //panelMenu.Controls.Add(leftBorderBtn);

            //Form
            this.Text = "Cửa Hàng Điện Nước";
            //this.ControlBox = false;
            //this.DoubleBuffered = true;
            //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            hoaDonThuNhapTheoThangs = hangHoaService.selectThuNhapTheoThang(DateTime.Now.Year);
            hoaDonNhapKhoTheoThangs = hangHoaService.selectNhapKhoTheoThang(DateTime.Now.Year);
            hoaDonThuNhapTheoThangs.ForEach(i =>
            {
                chartThuNhap.Series[0].Points.AddXY(i.thang, i.tong_thu_nhap);
            });
            hoaDonNhapKhoTheoThangs.ForEach((i) =>
            {
                chartThuNhap.Series[1].Points.AddXY(i.thang, i.tong_thu_nhap);

            });

            List<HoaDonXuatNhapTheoTuan> listHoaDonTheoTuan = hangHoaService.getHoaDonByCreatedDateGroupDayOfWeek("5/9/2022", "5/17/2022");
            Console.WriteLine(new JavaScriptSerializer().Serialize(listHoaDonTheoTuan));
            chartThuNhapTheoTuan.DataSource=listHoaDonTheoTuan;
            chartThuNhapTheoTuan.Series["ThuNhapTheoTuan"].XValueMember = "day_of_week";
            chartThuNhapTheoTuan.Series["ThuNhapTheoTuan"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            chartThuNhapTheoTuan.Series["ThuNhapTheoTuan"].YValueMembers = "thanh_tien";
            chartThuNhapTheoTuan.Series["ThuNhapTheoTuan"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;

            lbChiTieu.Text = hangHoaService.getTongChiTieu(DateTime.Now.Month).tong.ToString();
            lbThuNhap.Text = hangHoaService.getTongThuNhap(DateTime.Now.Month).tong.ToString();

            lbThangChiTieu.Text = lbThangThuNhap.Text = "Trong Tháng " + DateTime.Now.Month;

            Console.WriteLine(LoginForm.currentUser.role);
            if (LoginForm.currentUser.role != "ROLE_MANAGER")
            {
                btQLNhanVien.Visible = false;
                btQLNhaCungCap.Visible = false;
            }
        }

        //Structs
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }

        //Methods
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                //Left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                //Current Child Form Icon
                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            //open only form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            //childForm.Font.Size = 7.8;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitleChildForm.Text = childForm.Text;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new BanHangView());
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
            OpenChildForm(new KhoHangView());
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            OpenChildForm(new BaoCaoBanHangView());
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new QLNhanVien());
        }

        private void btnMarketing_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);
            OpenChildForm(new QLNhaCungCap());
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            //OpenChildForm(new Forms.FormSetting());
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            Reset();
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = Color.MediumPurple;
            lblTitleChildForm.Text = "Home";
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void iconbtLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LoginForm().Show();
        }
    }
}
