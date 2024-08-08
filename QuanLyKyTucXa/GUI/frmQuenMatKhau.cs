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
    public partial class frmQuenMatKhau : Form
    {
        public frmQuenMatKhau()
        {
            InitializeComponent();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (txtMa.Text == "" || txtTaiKhoan.Text == "")
            {
                MessageBox.Show("Bạn nhập chưa đủ thông tin!", "Thông báo");
                return;
            }

            using (var db = new QLKyTucXaEntities())
            {
                if (radSinhVien.Checked)
                {
                    var user = db.TAIKHOANs.FirstOrDefault(u => u.TenNguoiDung == txtTaiKhoan.Text && u.MaSinhVien == txtMa.Text);
                    if (user != null)
                    {
                        DialogResult result = MessageBox.Show($"Mật khẩu của người dùng {user.TenNguoiDung} là: {user.MatKhau}", "Thông tin", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (result == DialogResult.OK)
                        {
                            this.Hide();
                            frmLoGin loGin = new frmLoGin();
                            loGin.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã số sinh viên hoặc Tên tài khoản không đúng!", "Thông báo");
                    }
                }
                else
                {
                    var user = db.TAIKHOANs.FirstOrDefault(u => u.TenNguoiDung == txtTaiKhoan.Text && u.MaNhanVien == txtMa.Text);
                    if (user != null)
                    {
                        DialogResult result = MessageBox.Show($"Mật khẩu của người dùng {user.TenNguoiDung} là: {user.MatKhau}", "Thông tin", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (result == DialogResult.OK)
                        {
                            this.Hide();
                            frmLoGin loGin = new frmLoGin();
                            loGin.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã nhân viên hoặc Tên tài khoản không đúng!", "Thông báo");
                    }
                }
            }
        }

        private void frmQuenMatKhau_Load(object sender, EventArgs e)
        {
            txtMa.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            frmLoGin loGin = new frmLoGin();
            loGin.ShowDialog();
        }
    }
}
