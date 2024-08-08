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
    public partial class frmThanhToan : Form
    {
        public frmThanhToan()
        {
            InitializeComponent();
        }

        public string t;

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            this.Location = new Point(488, 165);
        }

        private void btnDien_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            frmDienNuoc dn = new frmDienNuoc();
            dn.s = t;
            dn.Show();
        }

        private void btnNuoc_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            frmNuoc n = new frmNuoc();
            n.s = t;
            n.Show();
        }

        private void btnLuong_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            frmLuong l = new frmLuong();
            l.s = t;
            l.Show();
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            frmTienPhong p = new frmTienPhong();
            p.s = t;
            p.Show();
        }
    }
}
