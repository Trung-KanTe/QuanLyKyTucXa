using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKyTucXa
{
    public partial class frmLoGin : Form
    {
        public frmLoGin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên đăng nhập hoặc mật khẩu!", "Thông báo");
                return;
            }

            using (var db = new QLKyTucXaEntities())
            {
                var user = db.TAIKHOANs.FirstOrDefault(u => u.TenNguoiDung == txtUsername.Text && u.MatKhau == txtPassword.Text);
                if (user != null)
                {
                    frmTrangChu tc = new frmTrangChu();
                    tc.TK = user.TenNguoiDung;
                    tc.role = user.PhanQuyen ?? 0;
                    tc.Show();
                    this.Hide();                 
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo");
                }
            }
        }

        private void frmLoGin_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.txtUsername.Focus();
        }

        private void btnQuenMK_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmQuenMatKhau qmk = new frmQuenMatKhau();
            qmk.Show();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDangKy dk = new frmDangKy();
            dk.Show();
        }

        private bool HienMK = false;
        private void btnHienMK_Click(object sender, EventArgs e)
        {
            HienMK = !HienMK;
            txtPassword.PasswordChar = HienMK ? '\0' : '*';
        }

        private void btnHoTro_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("* Liên hệ với Admin thông qua:\n\n -SĐT:0386880977\n\n -Email: trungkante@gmail.com\n\n _ĐỂ ĐƯỢC HỔ TRỢ NHANH CHÓNG VÀ KỊP THỜI_", "Thông tin", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                frmLoGin loGin = new frmLoGin();
                loGin.ShowDialog();
            }
        }
    }
}
