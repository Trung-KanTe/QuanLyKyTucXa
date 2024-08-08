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
    public partial class frmThongKePhong : Form
    {
        public frmThongKePhong()
        {
            InitializeComponent();
        }

        private void frmThongKePhong_Load(object sender, EventArgs e)
        {
            this.Location = new Point(488, 165);
            fillChartCot();
            fillChartTron();
            fillPhong();
        }

        private void fillPhong()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var TongPhong = db.PHONGs.Count();
                var PhongO = db.PHONGs.Count(sv => sv.TinhTrang == "Có người");
                var PhongTrong = TongPhong - PhongO;

                // Đếm số lượng phòng đủ 4 sinh viên cùng một mã phòng
                var PhongDu = db.PHONGs.Count(p => p.SINHVIENs.Count() == 4);

                lblTatCa.Text = TongPhong.ToString() + " phòng";
                lblDangO.Text = PhongO.ToString() + " phòng";
                lblTrong.Text = PhongTrong.ToString() + " phòng";
                lblDu.Text = PhongDu.ToString() + " phòng";
            }
        }

        private void fillChartCot()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var query = from p in db.PHONGs
                            group p by p.TenNha into g
                            select new
                            {
                                TenNha = g.Key,
                                SoLuongPhong = g.Count()
                            };

                foreach (var result in query)
                {
                    BDTenNha.Series["Phòng"].Points.AddXY(result.TenNha, result.SoLuongPhong);
                }
            }
        }

        private void fillChartTron()
        {
            using (var db = new QLKyTucXaEntities())
            {
                // Đếm số lượng phòng theo từng trạng thái
                var TongPhong = db.PHONGs.Count();
                var PhongYes = db.PHONGs.Count(p => p.TrangThai == "Yes");
                var PhongNo = TongPhong - PhongYes;

                // Tính phần trăm của trạng thái Yes và No
                double TiLeYes = (double)PhongYes / TongPhong * 100;
                double TiLeNo = (double)PhongNo / TongPhong * 100;

                // Xóa dữ liệu cũ trong biểu đồ
                BDTrangThai.Series.Clear();
                BDTrangThai.Legends.Clear();
                BDTrangThai.Series.Add("phòng");
                BDTrangThai.Series["phòng"].ChartType = SeriesChartType.Doughnut;

                // Thêm dữ liệu vào biểu đồ Doughnut
                BDTrangThai.Series["phòng"].Points.AddXY("Hoạt động", TiLeYes);
                BDTrangThai.Series["phòng"].Points.AddXY("Tu sửa", TiLeNo);

                // Hiển thị phần trăm trên biểu đồ
                BDTrangThai.Series["phòng"].IsValueShownAsLabel = true;
                BDTrangThai.Series["phòng"].LabelFormat = "#.#'%'";

                // Thêm chú thích (Legend) cho biểu đồ
                BDTrangThai.Legends.Add(new Legend("Legend1"));
                BDTrangThai.Series["phòng"].Legend = "Legend1";
                BDTrangThai.Legends["Legend1"].Docking = Docking.Right;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            frmThongKe tk = new frmThongKe();
            tk.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
