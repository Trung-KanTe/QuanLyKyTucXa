using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyKyTucXa
{
    public partial class frmThongKeSinhVien : Form
    {
        public frmThongKeSinhVien()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            frmThongKe tk = new frmThongKe();
            tk.Show();
        }

        private void BDTenNha_Click(object sender, EventArgs e)
        {

        }

        private void frmThongKeSinhVien_Load(object sender, EventArgs e)
        {
            this.Location = new Point(488, 165);
            fillChartCot();
            fillChartTron();
            fillSinhVien();
        }

        private void fillSinhVien()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var TongSV = db.SINHVIENs.Count();
                var SinhVienO = db.SINHVIENs.Count(sv => sv.TinhTrangO == "Yes");
                var SinhVienDi = TongSV - SinhVienO;

                lblTatCa.Text = TongSV.ToString() + " sinh viên";
                lblDangO.Text = SinhVienO.ToString() + " sinh viên";
                lblTraPhong.Text = SinhVienDi.ToString() + " sinh viên";
            }
        }

        private void fillChartCot()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var query = from sv in db.SINHVIENs
                            join p in db.PHONGs on sv.MaPhong equals p.MaPhong
                            group sv by p.TenNha into g
                            select new
                            {
                                TenNha = g.Key,
                                SoLuongSinhVien = g.Count()
                            };

                foreach (var result in query)
                {
                    BDTenNha.Series["Sinh viên"].Points.AddXY(result.TenNha, result.SoLuongSinhVien);
                }
            }
        }

        private void fillChartTron()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var TongSV = db.SINHVIENs.Count();
                var SVNam = db.SINHVIENs.Count(sv => sv.GioiTinh == "Nam");
                var SVNu = TongSV - SVNam;

                double TiLeNam = (double)SVNam / TongSV * 100;
                double TiLeNu = (double)SVNu / TongSV * 100;

                BDGioiTinh.Series.Clear();
                BDGioiTinh.Legends.Clear();
                BDGioiTinh.Series.Add("Sinh viên");
                BDGioiTinh.Series["Sinh viên"].ChartType = SeriesChartType.Doughnut;

                BDGioiTinh.Series["Sinh viên"].Points.AddXY("Nam", TiLeNam);
                BDGioiTinh.Series["Sinh viên"].Points.AddXY("Nữ", TiLeNu);

                BDGioiTinh.Series["Sinh viên"].IsValueShownAsLabel = true;
                BDGioiTinh.Series["Sinh viên"].LabelFormat = "#.#'%'";

                BDGioiTinh.Legends.Add(new Legend("Legend1"));
                BDGioiTinh.Series["Sinh viên"].Legend = "Legend1";
                BDGioiTinh.Legends["Legend1"].Docking = Docking.Right;
            }
        }
        private void lblTatCa_Click(object sender, EventArgs e)
        {

        }
    }
}
