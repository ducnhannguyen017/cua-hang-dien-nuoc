using DocumentFormat.OpenXml.CustomProperties;
using Hanssens.Net;
using Nhom14_KinhDoanhDienNuoc;
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

namespace Quanlydiennuoc
{
    public partial class LoginForm : Form
    {
        HangHoaService hangHoaService =new HangHoaService();
        LocalStorage storage = new LocalStorage();
        public static User currentUser = new User();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pnlLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try {
                if(tbUsername.Text == "")
                {
                    tbUsername.Text = "";
                    return;
                }
                tbUsername.ForeColor = Color.Black;
                panel15.Visible = false;


            }
            catch { }
                
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try

            {
                if (tbPassword.Text == "")
                {
                    tbPassword.Text = "";
                    return;
                }
                tbPassword.ForeColor = Color.Black;
                panel19.Visible = false;
            }
            catch { }
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            tbUsername.SelectAll();
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            tbPassword.SelectAll();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Black;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(tbUsername.Text== "") // on write you own Condition
            {
                panel15.Visible = true;
                tbUsername.Focus();
                return;
            }
            else if (tbPassword.Text == "") // on write you own Condition
            {
                panel19.Visible = true;
                tbPassword.Focus();
                return;
            }
            else
            {
                User user = hangHoaService.login(tbUsername.Text, tbPassword.Text);
                Console.WriteLine(user.username == null);
                if (user.username != null)
                {
                    currentUser = user;
                    storage.Store("role", user.role);


                    LayoutForm layoutForm = new LayoutForm();
                    layoutForm.Size = new Size(1420, 684);
                    layoutForm.Show();
                    this.Hide();


                }
                //if(hangHoaService.login(tbUsername.Text, tbPassword.Text))
                //{
                //    this.Hide();
                //    LayoutForm layoutForm = new LayoutForm();
                //    layoutForm.Size = new Size(1420, 684);
                //    layoutForm.Show();
                //}
                else
                {
                    MessageBox.Show("Sai Username hoặc Password");
                }
            }

        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
