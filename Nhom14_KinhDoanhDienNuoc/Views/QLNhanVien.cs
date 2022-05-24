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
using System.Windows.Forms;

namespace Nhom14_KinhDoanhDienNuoc.Views
{
    public partial class QLNhanVien : Form
    {
        HangHoaService hangHoaService = new HangHoaService();
        UserService userService = new UserService();
        List<User> listNhanVien = new List<User>();
        int idClick;
        public QLNhanVien()
        {
            InitializeComponent();
            preData();
        }

        public void preData()
        {
            listNhanVien = userService.getUserByRole("ROLE_EMPLOYEE");
            dgvNhanVien.DataSource = listNhanVien;
            dgvNhanVien.Columns[0].Visible = false;
            dgvNhanVien.Columns[1].HeaderText = "Tên";
            dgvNhanVien.Columns[2].HeaderText = "Username";
            dgvNhanVien.Columns[3].HeaderText = "Password";
            dgvNhanVien.Columns[4].HeaderText = "SĐT";
            dgvNhanVien.Columns[5].HeaderText = "Địa Chỉ";
            dgvNhanVien.Columns[6].HeaderText = "Email";
            dgvNhanVien.Columns[7].Visible = false;

        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvNhanVien.Rows[e.RowIndex];
                tbTen.Text = row.Cells[1].Value.ToString();
                tbPassword.Text = row.Cells[3].Value.ToString();
                tbSDT.Text = row.Cells[4].Value.ToString();
                tbEmail.Text = row.Cells[6].Value.ToString();
                tbUsername.Text = row.Cells[2].Value.ToString();
                tbDiaChi.Text = row.Cells[5].Value.ToString();

                idClick = Int32.Parse(row.Cells[0].Value.ToString());
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            tbTen.Text = "";
            tbSDT.Text = "";
            tbEmail.Text = "";
            tbUsername.Text = "";
            tbDiaChi.Text = "";
            tbPassword.Text = "";
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            if (listNhanVien.FindIndex(i => i.username == tbUsername.Text) >= 0)
            {
                MessageBox.Show("Trùng Tên");
            }
            else
            {
                User user = new User()
                {
                    username = tbUsername.Text,
                    ten = tbTen.Text,
                    password = tbPassword.Text,
                    email = tbEmail.Text,
                    diachi = tbDiaChi.Text,
                    sdt = tbDiaChi.Text,
                };
                try
                {
                    userService.updateNhanVien(user, idClick);
                    MessageBox.Show("Đã Lưu");
                    listNhanVien = userService.getUserByRole("ROLE_EMPLOYEE");
                    dgvNhanVien.DataSource = listNhanVien;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error");
                }
                
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            try
            {
                userService.deleteNhanVien(idClick);
                MessageBox.Show("Đã Xóa");
                listNhanVien = userService.getUserByRole("ROLE_EMPLOYEE");
                dgvNhanVien.DataSource = listNhanVien;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
            
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            if(listNhanVien.FindIndex(i=>i.username == tbUsername.Text) >= 0)
            {
                MessageBox.Show("Trùng Tên");
            }
            else
            {
                User user = new User()
                {
                    username = tbUsername.Text,
                    ten = tbTen.Text,
                    password = tbPassword.Text,
                    email = tbEmail.Text,
                    diachi = tbDiaChi.Text,
                    sdt = tbDiaChi.Text,
                };
                try
                {
                    userService.insertNhanVien(user);
                    MessageBox.Show("Đã Lưu");
                    listNhanVien = userService.getUserByRole("ROLE_EMPLOYEE");
                    dgvNhanVien.DataSource = listNhanVien;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error");
                }
                
            }
            
        }
    }
}
