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
    public partial class frmDangKy : Form
    {
        public frmDangKy()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            frmLoGin loGin = new frmLoGin();
            loGin.ShowDialog();
        }

        private void btnKichHoat_Click(object sender, EventArgs e)
        {
            using (var db = new QLKyTucXaEntities())
            {
                if (radSinhVien.Checked)
                {
                    var user = db.SINHVIENs.FirstOrDefault(u => u.MaSinhVien == txtMa.Text);
                    if (user != null)
                    {
                        if (txtMa.Text != "" || txtMatKhau.Text != "" || txtTaiKhoan.Text == "" || txtXacNhanMK.Text == "")
                        {
                            if(txtMatKhau.Text == txtXacNhanMK.Text)
                            {
                                TAIKHOAN newAccount = new TAIKHOAN
                                {
                                    TenNguoiDung = txtTaiKhoan.Text,
                                    MatKhau = txtXacNhanMK.Text,
                                    MaSinhVien = txtMa.Text,
                                    PhanQuyen = 2,
                                };
                                db.TAIKHOANs.Add(newAccount);
                                db.SaveChanges();

                                DialogResult result = MessageBox.Show("Đăng ký người dùng thành công!!!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                if (result == DialogResult.OK)
                                {
                                    this.Hide();
                                    frmLoGin loGin = new frmLoGin();
                                    loGin.ShowDialog();
                                }
                            }    
                            else
                            {
                                MessageBox.Show("Xác nhận mật khẩu không chính xác!", "Thông báo");
                                txtXacNhanMK.Clear();
                                txtXacNhanMK.Focus();
                            }    
                        }
                        else
                        {
                            MessageBox.Show("Vui lòng nhập đầy đủ thông tin !", "Thông báo");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã số sinh viên không tồn tại trong danh sách!", "Thông báo");
                    }
                }
                else
                {
                    var user = db.NHANVIENs.FirstOrDefault(u => u.MaNhanVien == txtMa.Text);
                    if (user != null)
                    {
                        if (txtMa.Text != "" || txtMatKhau.Text != "" || txtTaiKhoan.Text == "" || txtXacNhanMK.Text == "")
                        {
                            if (txtMatKhau.Text == txtXacNhanMK.Text)
                            {
                                TAIKHOAN newAccount = new TAIKHOAN
                                {
                                    TenNguoiDung = txtTaiKhoan.Text,
                                    MatKhau = txtXacNhanMK.Text,
                                    MaNhanVien = txtMa.Text,
                                    PhanQuyen = 2,
                                };
                                db.TAIKHOANs.Add(newAccount);
                                db.SaveChanges();

                                DialogResult result = MessageBox.Show("Đăng ký người dùng thành công!!!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                if (result == DialogResult.OK)
                                {
                                    this.Hide();
                                    frmLoGin loGin = new frmLoGin();
                                    loGin.ShowDialog();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Xác nhận mật khẩu không chính xác!", "Thông báo");
                                txtXacNhanMK.Clear();
                                txtXacNhanMK.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Vui lòng nhập đầy đủ thông tin !", "Thông báo");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã nhân viên không tồn tại trong danh sách!", "Thông báo");
                    }
                }
            }    
        }

        private bool HienMK = false;
        private void btnHienMK_Click(object sender, EventArgs e)
        {
            HienMK = !HienMK;
            txtMatKhau.PasswordChar = HienMK ? '\0' : '*';
        }

        private bool HienXNMK = false;
        private void btnHienXNMK_Click(object sender, EventArgs e)
        {
            HienXNMK = !HienXNMK;
            txtMatKhau.PasswordChar = HienXNMK ? '\0' : '*';
        }
    } 
}
