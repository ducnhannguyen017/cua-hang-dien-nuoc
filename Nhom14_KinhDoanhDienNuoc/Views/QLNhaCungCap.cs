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
    public partial class QLNhaCungCap : Form
    {
        HangHoaService hangHoaService = new HangHoaService();
        List<NhaCungCap> listNhaCungCap = new List<NhaCungCap>();
        int idClick;
        public QLNhaCungCap()
        {
            InitializeComponent();
            preData();
        }

        public void preData()
        {
            listNhaCungCap = hangHoaService.getAllNhaCungCap();
            dgvNhaCungCap.DataSource = listNhaCungCap;
            dgvNhaCungCap.Columns[0].Visible = false;
            dgvNhaCungCap.Columns[1].HeaderText = "Tên";
            dgvNhaCungCap.Columns[2].HeaderText = "SĐT";
            dgvNhaCungCap.Columns[3].HeaderText = "Email";
            dgvNhaCungCap.Columns[4].HeaderText = "Địa Chỉ";
        }

        private void dgvNhaCungCap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvNhaCungCap.Rows[e.RowIndex];
                tbTen.Text = row.Cells[1].Value.ToString();
                tbSDT.Text = row.Cells[2].Value.ToString();
                tbEmail.Text = row.Cells[3].Value.ToString();
                tbDiaChi.Text = row.Cells[4].Value.ToString();

                idClick = Int32.Parse(row.Cells[0].Value.ToString());
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            tbTen.Text = "";
            tbSDT.Text = "";
            tbEmail.Text = "";
            tbDiaChi.Text = "";
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            if (listNhaCungCap.FindIndex(i => i.ten == tbTen.Text) >= 0)
            {
                MessageBox.Show("Trùng Tên");
            }
            else
            {
                NhaCungCap nhaCungCap = new NhaCungCap()
                {
                    ten = tbTen.Text,
                    email = tbEmail.Text,
                    dia_chi = tbDiaChi.Text,
                    sdt = tbSDT.Text,
                };
                try
                {
                    hangHoaService.updateNhaCungCap(nhaCungCap, idClick);
                    MessageBox.Show("Đã Lưu");
                    listNhaCungCap = hangHoaService.getAllNhaCungCap();
                    dgvNhaCungCap.DataSource = listNhaCungCap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error");
                }

            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            if (listNhaCungCap.FindIndex(i => i.ten == tbTen.Text) >= 0)
            {
                MessageBox.Show("Trùng Tên");
            }
            else
            {
                NhaCungCap nhaCungCap = new NhaCungCap()
                {
                    ten = tbTen.Text,
                    email = tbEmail.Text,
                    dia_chi = tbDiaChi.Text,
                    sdt = tbSDT.Text,
                };
                try
                {
                    hangHoaService.insertNhaCungCap(nhaCungCap);
                    MessageBox.Show("Đã Lưu");
                    listNhaCungCap = hangHoaService.getAllNhaCungCap();
                    dgvNhaCungCap.DataSource = listNhaCungCap;
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
                hangHoaService.deleteNhaCungCap(idClick);
                MessageBox.Show("Đã Xóa");
                listNhaCungCap = hangHoaService.getAllNhaCungCap();
                dgvNhaCungCap.DataSource = listNhaCungCap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
            
        }
    }
}
