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
    public partial class frmThongKe : Form
    {
        public frmThongKe()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            this.Location = new Point(488, 165);
        }

        private void btnSinhVien_Click(object sender, EventArgs e)
        {
            frmThongKeSinhVien sv = new frmThongKeSinhVien();
            sv.Show();
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            frmThongKePhong t = new frmThongKePhong();
            t.Show();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            frmThongKeNhanVien n = new frmThongKeNhanVien();
            n.Show();
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            frmThongKeTaiKhoan t = new frmThongKeTaiKhoan();
            t.Show();
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            frmHoaDon t = new frmHoaDon();
            t.Show();
        }
    }
}
