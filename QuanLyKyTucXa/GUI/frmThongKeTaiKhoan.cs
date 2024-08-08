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
    public partial class frmThongKeTaiKhoan : Form
    {
        public frmThongKeTaiKhoan()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            frmThongKe t = new frmThongKe();
            t.Show();
        }

        private void frmThongKeTaiKhoan_Load(object sender, EventArgs e)
        {
            this.Location = new Point(488, 165);
            fillTaiKhoan();
            fillChartCot();
            fillChartTron();
        }

        private void fillTaiKhoan()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var TongTaiKhoan = db.TAIKHOANs.Count();

                var SinhVienChuaCoTK = db.SINHVIENs.Count(sv => sv.TAIKHOANs.Count == 0);

                var NhanVienChuaCoTK = db.NHANVIENs.Count(nv => nv.TAIKHOANs.Count == 0);

                lblTatCa.Text = TongTaiKhoan.ToString() + " user";
                lblSinhVien.Text = SinhVienChuaCoTK.ToString() + " sinh viên";
                lblNhanVien.Text = NhanVienChuaCoTK.ToString() + " nhân viên";
            }
        }

        private void fillChartCot()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var query = from tk in db.TAIKHOANs
                            join sv in db.SINHVIENs on tk.MaSinhVien equals sv.MaSinhVien into svGroup
                            join nv in db.NHANVIENs on tk.MaNhanVien equals nv.MaNhanVien into nvGroup
                            select new
                            {
                                SoLuongSinhVien = svGroup.Count(),
                                SoLuongNhanVien = nvGroup.Count()
                            };

                // Tính tổng số lượng sinh viên và nhân viên đã có tài khoản
                int TongSinhVien = query.Sum(q => q.SoLuongSinhVien);
                int TongNhanVien = query.Sum(q => q.SoLuongNhanVien);

                // Thêm dữ liệu vào biểu đồ cột
                BDDangKy.Series["User"].Points.AddXY("Sinh viên", TongSinhVien);
                BDDangKy.Series["User"].Points.AddXY("Nhân viên", TongNhanVien);
            }
        }

        private void fillChartTron()
        {
            using (var db = new QLKyTucXaEntities())
            {
                // Đếm tổng số lượng Tài khoản và nhân viên
                var TongSVNV = db.SINHVIENs.Count() + db.NHANVIENs.Count();

                // Đếm số lượng tài khoản hiện có
                var TongTaiKhoan = db.TAIKHOANs.Count();

                // Tính số lượng người chưa có tài khoản
                var ChuaCoTaiKhoan = TongSVNV - TongTaiKhoan;

                // Tính phần trăm của những người đã có tài khoản và chưa có tài khoản
                double TiLeCoTaiKhoan = (double)TongTaiKhoan / TongSVNV * 100;
                double TiLeChuaCoTaiKhoan = (double)ChuaCoTaiKhoan / TongSVNV * 100;

                // Xóa dữ liệu cũ trong biểu đồ
                BDTaiKhoan.Series.Clear();
                BDTaiKhoan.Legends.Clear();
                BDTaiKhoan.Series.Add("Tài khoản");
                BDTaiKhoan.Series["Tài khoản"].ChartType = SeriesChartType.Doughnut;

                // Thêm dữ liệu vào biểu đồ Doughnut và thiết lập màu sắc
                var series = BDTaiKhoan.Series["Tài khoản"];
                series.Points.AddXY("Đã có tài khoản", TiLeCoTaiKhoan);
                series.Points[0].Color = Color.Green; // Đã có tài khoản - màu xanh
                series.Points.AddXY("Chưa có tài khoản", TiLeChuaCoTaiKhoan);
                series.Points[1].Color = Color.Red; // Chưa có tài khoản - màu đỏ

                // Hiển thị phần trăm trên biểu đồ
                series.IsValueShownAsLabel = true;
                series.LabelFormat = "#.#'%";

                // Thêm chú thích (Legend) cho biểu đồ
                BDTaiKhoan.Legends.Add(new Legend("Legend1"));
                series.Legend = "Legend1";
                BDTaiKhoan.Legends["Legend1"].Docking = Docking.Right;
            }
        }


    }
}
