using Guna.UI2.WinForms;
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
    public partial class frmTrangChu : Form
    {
        public frmTrangChu()
        {
            InitializeComponent();
        }
        public string TK;
        public long role;
        Guna2Button nutChon = null;

        private void ThayDoiNut(Guna.UI2.WinForms.Guna2Button button)
        {
            if (nutChon != null)
            {
                nutChon.FillColor = Color.FromArgb(94, 148, 255);
            }

            button.FillColor = Color.FromArgb(50, 90, 255); 
            nutChon = button;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            frmLoGin l = new frmLoGin();
            l.Show();
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            ThayDoiNut((Guna.UI2.WinForms.Guna2Button)sender);
            frmTrangChu_Load(sender, e);
        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            txtTaiKhoan.Text = TK;
            if (role == 1)
            {
                txtPhanQuyen.Text = "Admin";
            }
            else
            {
                txtPhanQuyen.Text = "User";  
                btnTaiKhoan.Enabled = false;
                btnQLTaiKhoan.Enabled = false;
            }
        }

        private void btnQuanLyPhong_Click(object sender, EventArgs e)
        {
            ThayDoiNut((Guna.UI2.WinForms.Guna2Button)sender);
            frmTenPhong tp = new frmTenPhong();
            tp.n = txtPhanQuyen.Text;
            tp.Show();
        }

        private void btnQuanLySV_Click(object sender, EventArgs e)
        {
            ThayDoiNut((Guna.UI2.WinForms.Guna2Button)sender);
            frmNhaSinhVien nsv = new frmNhaSinhVien();
            nsv.s = txtPhanQuyen.Text;
            nsv.Show();
        }

        private void btnQuanLyNV_Click(object sender, EventArgs e)
        {
            ThayDoiNut((Guna.UI2.WinForms.Guna2Button)sender);
            frmNhanVien nv = new frmNhanVien();
            nv.u = txtPhanQuyen.Text;
            nv.Show();
        }

        private void btnThanhToann_Click(object sender, EventArgs e)
        {
            ThayDoiNut((Guna.UI2.WinForms.Guna2Button)sender);
            frmThanhToan tt = new frmThanhToan();
            tt.t = txtPhanQuyen.Text;
            tt.Show();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            ThayDoiNut((Guna.UI2.WinForms.Guna2Button)sender);
            frmThongKe tk = new frmThongKe();
            tk.Show();
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            ThayDoiNut((Guna.UI2.WinForms.Guna2Button)sender);
            frmTaiKhoan tk = new frmTaiKhoan();
            tk.t = txtPhanQuyen.Text;
            tk.Show();
        }

        private void btnQLPhong_Click(object sender, EventArgs e)
        {
            frmTenPhong tp = new frmTenPhong();
            tp.n = txtPhanQuyen.Text;
            tp.Show();
            ThayDoiNut(btnQuanLyPhong);
        }

        private void btnQLSinhVien_Click(object sender, EventArgs e)
        {
            frmNhaSinhVien nsv = new frmNhaSinhVien();
            nsv.s = txtPhanQuyen.Text;
            nsv.Show();
            ThayDoiNut(btnQuanLySV);
        }

        private void btnQLNhanVien_Click(object sender, EventArgs e)
        {
            frmNhanVien nv = new frmNhanVien();
            nv.u = txtPhanQuyen.Text;
            nv.Show();
            ThayDoiNut(btnQuanLyNV);
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            frmThanhToan tt = new frmThanhToan();
            tt.t = txtPhanQuyen.Text;
            tt.Show();
            ThayDoiNut(btnThanhToann);
        }

        private void btnQLThongKe_Click(object sender, EventArgs e)
        {
            frmThongKe tk = new frmThongKe();
            tk.Show();
            ThayDoiNut(btnThongKe);
        }

        private void btnQLTaiKhoan_Click(object sender, EventArgs e)
        {
            frmTaiKhoan tk = new frmTaiKhoan();
            tk.Show();
            ThayDoiNut(btnTaiKhoan);
        }
    }
}
