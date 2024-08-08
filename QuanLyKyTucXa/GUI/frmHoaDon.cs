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
    public partial class frmHoaDon : Form
    {
        public frmHoaDon()
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

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            this.Location = new Point(488, 165);
            fillBDDien();
            fillBDNuoc();
            fillBDLuong();
            fillBDPhong();
        }

        private void fillBDDien()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var DaThanhToan = db.DIENs
                                        .Where(d => d.NgayThanhToan != null)
                                        .Join(db.SINHVIENs.Where(s => s.TinhTrangO == "Yes"),
                                              dien => dien.MaSinhVien,
                                              sv => sv.MaSinhVien,
                                              (dien, sv) => dien.MaSinhVien)
                                        .Distinct()
                                        .ToList();

                int svDaThanhToan = DaThanhToan.Count;

                int svChuaThanhToan = db.SINHVIENs.Count(s => s.TinhTrangO == "Yes") - svDaThanhToan;

                BDDien.Series["Sinh viên"].Points.AddXY("Đã thanh toán", svDaThanhToan);
                BDDien.Series["Sinh viên"].Points.AddXY("Chưa thanh toán", svChuaThanhToan);

                BDDien.Series["Sinh viên"].Points[0].Color = System.Drawing.Color.Orange; 
                BDDien.Series["Sinh viên"].Points[1].Color = System.Drawing.Color.Orange;
                BDDien.Series["Sinh viên"].Color = System.Drawing.Color.Orange;
            }
        }

        private void fillBDNuoc()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var DaThanhToan = db.NUOCs
                                        .Where(d => d.NgayThanhToan != null)
                                        .Join(db.SINHVIENs.Where(s => s.TinhTrangO == "Yes"),
                                              nuoc => nuoc.MaSinhVien,
                                              sv => sv.MaSinhVien,
                                              (nuoc, sv) => nuoc.MaSinhVien)
                                        .Distinct()
                                        .ToList();

                int svDaThanhToan = DaThanhToan.Count;

                int svChuaThanhToan = db.SINHVIENs.Count(s => s.TinhTrangO == "Yes") - svDaThanhToan;

                BDNuoc.Series["Sinh viên"].Points.AddXY("Đã thanh toán", svDaThanhToan);
                BDNuoc.Series["Sinh viên"].Points.AddXY("Chưa thanh toán", svChuaThanhToan);

                BDNuoc.Series["Sinh viên"].Points[0].Color = System.Drawing.Color.Red; 
                BDNuoc.Series["Sinh viên"].Points[1].Color = System.Drawing.Color.Red;
                BDNuoc.Series["Sinh viên"].Color = System.Drawing.Color.Red;
            }
        }

        private void fillBDLuong()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var DaThanhToan = db.LUONGs
                                        .Where(d => d.Thang != null)
                                        .Join(db.NHANVIENs.Where(s => s.TinhTrang == "Yes"),
                                              luong => luong.MaNhanVien,
                                              nv => nv.MaNhanVien,
                                              (luong, sv) => luong.MaNhanVien)
                                        .Distinct()
                                        .ToList();

                int nvDaThanhToan = DaThanhToan.Count;

                int nvChuaThanhToan = db.NHANVIENs.Count(s => s.TinhTrang == "Yes") - nvDaThanhToan;

                BDLuong.Series["Nhân viên"].Points.AddXY("Đã thanh toán", nvDaThanhToan);
                BDLuong.Series["Nhân viên"].Points.AddXY("Chưa thanh toán", nvChuaThanhToan);
            }
        }


        private void fillBDPhong()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var DaThanhToan = db.THANHTOANs
                                        .Where(d => d.Thang != null)
                                        .Join(db.SINHVIENs.Where(s => s.TinhTrangO == "Yes"),
                                              phong => phong.MaSinhVien,
                                              sv => sv.MaSinhVien,
                                              (phong, sv) => phong.MaSinhVien)
                                        .Distinct()
                                        .ToList();

                int svDaThanhToan = DaThanhToan.Count;

                int svChuaThanhToan = db.SINHVIENs.Count(s => s.TinhTrangO == "Yes") - svDaThanhToan;

                BDPhong.Series["Sinh viên"].Points.AddXY("Đã thanh toán", svDaThanhToan);
                BDPhong.Series["Sinh viên"].Points.AddXY("Chưa thanh toán", svChuaThanhToan);

                BDPhong.Series["Sinh viên"].Points[0].Color = System.Drawing.Color.GreenYellow; 
                BDPhong.Series["Sinh viên"].Points[1].Color = System.Drawing.Color.GreenYellow;
                BDPhong.Series["Sinh viên"].Color = System.Drawing.Color.GreenYellow;
            }           
        }

        private void cbThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            int thang = cbThang.SelectedIndex + 1; // Tháng bắt đầu từ 1

            
            if (thang > 0)
            {
                using (var db = new QLKyTucXaEntities())
                {
                    //Điện
                    var TTDien = db.DIENs
                        .Where(d => d.NgayThanhToan != null && d.NgayThanhToan.Value.Month == thang)
                        .Select(d => d.MaSinhVien)
                        .Distinct()
                        .ToList();

                    int svTTDien = TTDien.Count;

                    int svChuaTTDien = db.SINHVIENs
                        .Count(s => s.TinhTrangO == "Yes" && !TTDien.Contains(s.MaSinhVien));

                    UpdateBDDien(svTTDien, svChuaTTDien);

                    //Nước
                    var TTNuoc = db.NUOCs
                        .Where(d => d.NgayThanhToan != null && d.NgayThanhToan.Value.Month == thang)
                        .Select(d => d.MaSinhVien)
                        .Distinct()
                        .ToList();

                    int svTTNuoc = TTNuoc.Count;

                    int svChuaTTNuoc = db.SINHVIENs
                        .Count(s => s.TinhTrangO == "Yes" && !TTDien.Contains(s.MaSinhVien));

                    UpdateBDNuoc(svTTNuoc, svChuaTTNuoc);

                    //Phòng
                    var TTPhong = db.THANHTOANs
                        .Where(d => d.Thang != null && d.Thang.Value.Month == thang)
                        .Select(d => d.MaSinhVien)
                        .Distinct()
                        .ToList();

                    int svTTPhong = TTPhong.Count;

                    int svChuaTTPhong = db.SINHVIENs
                        .Count(s => s.TinhTrangO == "Yes" && !TTPhong.Contains(s.MaSinhVien));

                    UpdateBDPhong(svTTPhong, svChuaTTPhong);

                    //Lương
                    var TTLuong = db.LUONGs
                       .Where(d => d.Thang != null && d.Thang.Value.Month == thang)
                       .Select(d => d.MaNhanVien)
                       .Distinct()
                       .ToList();

                    int nvTTLuong = TTLuong.Count;

                    int nvChuaTTLuong = db.NHANVIENs
                        .Count(s => s.TinhTrang == "Yes" && !TTLuong.Contains(s.MaNhanVien));

                    UpdateBDLuong(nvTTLuong, nvChuaTTLuong);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tháng từ combobox.");
            }
        }

        private void UpdateBDDien(int svTTDien, int svChuaTTDien)
        {
            BDDien.Series["Sinh viên"].Points.Clear();
            BDDien.Series["Sinh viên"].Points.AddXY("Đã thanh toán", svTTDien);
            BDDien.Series["Sinh viên"].Points.AddXY("Chưa thanh toán", svChuaTTDien);
        }

        private void UpdateBDNuoc(int svTTNuoc, int svChuaTTNuoc)
        {
            BDNuoc.Series["Sinh viên"].Points.Clear();
            BDNuoc.Series["Sinh viên"].Points.AddXY("Đã thanh toán", svTTNuoc);
            BDNuoc.Series["Sinh viên"].Points.AddXY("Chưa thanh toán", svChuaTTNuoc);
        }

        private void UpdateBDPhong(int svTTPhong, int svChuaTTPhong)
        {
            BDPhong.Series["Sinh viên"].Points.Clear();
            BDPhong.Series["Sinh viên"].Points.AddXY("Đã thanh toán", svTTPhong);
            BDPhong.Series["Sinh viên"].Points.AddXY("Chưa thanh toán", svChuaTTPhong);
        }

        private void UpdateBDLuong(int nvTTLuong, int nvChuaTTLuong)
        {
            BDLuong.Series["Nhân viên"].Points.Clear();
            BDLuong.Series["Nhân viên"].Points.AddXY("Đã thanh toán", nvTTLuong);
            BDLuong.Series["Nhân viên"].Points.AddXY("Chưa thanh toán", nvChuaTTLuong);
        }
    }
}
