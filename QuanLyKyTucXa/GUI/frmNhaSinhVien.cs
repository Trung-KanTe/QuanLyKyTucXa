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
    public partial class frmNhaSinhVien : Form
    {
        public frmNhaSinhVien()
        {
            InitializeComponent();
        }

        public string s;
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            Close();
        }

        private void frmNhaSinhVien_Load(object sender, EventArgs e)
        {
            this.Location = new Point(488, 165);
        }

        private void btnChauDoc_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSinhVien sv = new frmSinhVien();
            sv.u = s;
            sv.TenNha = "Châu Đốc";
            sv.Show();
        }

        private void btnChauThanh_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSinhVien sv = new frmSinhVien();
            sv.u = s;
            sv.TenNha = "Châu Thành";
            sv.Show();
        }

        private void btnTriTon_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSinhVien sv = new frmSinhVien();
            sv.u = s;
            sv.TenNha = "Tri Tôn";
            sv.Show();
        }

        private void btnAnPhu_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSinhVien sv = new frmSinhVien();
            sv.u = s;
            sv.TenNha = "An Phú";
            sv.Show();
        }

        private void btnTinhBien_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSinhVien sv = new frmSinhVien();
            sv.u = s;
            sv.TenNha = "Tịnh Biên";
            sv.Show();
        }

        private void btnTanChau_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSinhVien sv = new frmSinhVien();
            sv.u = s;
            sv.TenNha = "Tân Châu";
            sv.Show();
        }

        private void btnThoaiSon_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSinhVien sv = new frmSinhVien();
            sv.u = s;
            sv.TenNha = "Thoại Sơn";
            sv.Show();
        }

        private void btnChoMoi_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSinhVien sv = new frmSinhVien();
            sv.u = s;
            sv.TenNha = "Chợ Mới";
            sv.Show();
        }

        
    }
}
