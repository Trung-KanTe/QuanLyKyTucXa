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
    public partial class frmTenPhong : Form
    {
        public frmTenPhong()
        {
            InitializeComponent();
        }

        public string n;

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void frmTenPhong_Load(object sender, EventArgs e)
        {
            this.Location = new Point(488, 165);
        }

        private void btnChauDoc_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPhong p = new frmPhong();
            p.q = n;
            p.TenNha = "Châu Đốc";
            p.Show();
        }

        private void btnChauThanh_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPhong p = new frmPhong();
            p.q = n;
            p.TenNha = "Châu Thành";
            p.Show();
        }

        private void btnTriTon_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPhong p = new frmPhong();
            p.q = n;
            p.TenNha = "Tri Tôn";
            p.Show();
        }

        private void btnAnPhu_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPhong p = new frmPhong();
            p.q = n;
            p.TenNha = "An Phú";
            p.Show();
        }

        private void btnTinhBien_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPhong p = new frmPhong();
            p.q = n;
            p.TenNha = "Tịnh Biên";
            p.Show();
        }

        private void btnTanChau_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPhong p = new frmPhong();
            p.q = n;
            p.TenNha = "Tân Châu";
            p.Show();
        }

        private void btnThoaiSon_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPhong p = new frmPhong();
            p.q = n;
            p.TenNha = "Thoại Sơn";
            p.Show();
        }

        private void btnChoMoi_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPhong p = new frmPhong();
            p.q = n;
            p.TenNha = "Chợ Mới";
            p.Show();
        }
    }
}
