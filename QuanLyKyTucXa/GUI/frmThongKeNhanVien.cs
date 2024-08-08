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
    public partial class frmThongKeNhanVien : Form
    {
        public frmThongKeNhanVien()
        {
            InitializeComponent();
        }

        private void frmThongKeNhanVien_Load(object sender, EventArgs e)
        {
            this.Location = new Point(488, 165);
            fillChartCot();
            fillNhanVien();
        }

        private void fillNhanVien()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var TongNV = db.NHANVIENs.Count();
                var NhanVienLam = db.NHANVIENs.Count(sv => sv.TinhTrang == "Yes");
                var NhanVienNghi = TongNV - NhanVienLam;

                lblTatCa.Text = TongNV.ToString() + " nhân viên";
                lblDangLam.Text = NhanVienLam.ToString() + " nhân viên";
                lblNghiLam.Text = NhanVienNghi.ToString() + " nhân viên";
            }
        }

        private void fillChartCot()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var query = from nv in db.NHANVIENs
                            group nv by nv.ChucVu into g
                            select new
                            {
                                ChucVu = g.Key,
                                SoLuongNV = g.Count()
                            };

                foreach (var result in query)
                {
                    BDChucVu.Series["Nhân viên"].Points.AddXY(result.ChucVu, result.SoLuongNV);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            frmThongKe t = new frmThongKe();
            t.Show();
        }
    }
}
