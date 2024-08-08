using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace QuanLyKyTucXa
{
    public partial class frmLuong : Form
    {
        public frmLuong()
        {
            InitializeComponent();
        }

        public string s;
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            frmThanhToan tp = new frmThanhToan();
            tp.Show();
        }

        private void DieuChinhCot()
        {
            dgvLuong.Columns["ID"].Width = 70;
            dgvLuong.Columns["Thang"].Width = 130;
            dgvLuong.Columns["Tien"].Width = 130;
            dgvLuong.Columns["MaNhanVien"].Width = 130;
            dgvLuong.Columns["ChucVu"].Width = 140;

            dgvLuong.Columns["ID"].HeaderText = "ID";
            dgvLuong.Columns["Thang"].HeaderText = "Ngày nhận lương";
            dgvLuong.Columns["Tien"].HeaderText = "Tiền lương";
            dgvLuong.Columns["MaNhanVien"].HeaderText = "Mã nhân viên";
            dgvLuong.Columns["ChucVu"].HeaderText = "Chức vụ";
        }

        private void frmLuong_Load(object sender, EventArgs e)
        {
            txtTimKiem.Focus();
            this.Location = new Point(488, 165);
            
            LoadData();
            DieuChinhCot();

            if (s == "User")
            {
                btnHoi.Visible = false;
                btnThanhToan.Visible = false;
                btnIn.Visible = false;
                btnImport.Visible = false;
                btnXuatExcel.Visible = false;
                txtThanhToan.Visible = false;
            }
        }

        private void LoadData()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var luongData = (from l in db.LUONGs
                                 join n in db.NHANVIENs on l.MaNhanVien equals n.MaNhanVien
                                 select new
                                 {
                                     l.ID,
                                     l.Thang,
                                     l.Tien,
                                     l.MaNhanVien,
                                     n.ChucVu 
                                 }).ToList();

                dgvLuong.DataSource = luongData;
            }
        }

        

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maNhanVienCanTim = txtTimKiem.Text.Trim();

            using (var db = new QLKyTucXaEntities())
            {
                var nhanVien = db.NHANVIENs.FirstOrDefault(nv => nv.MaNhanVien == maNhanVienCanTim);

                if (nhanVien != null)
                {
                    if (nhanVien.TinhTrang == "Yes")
                    {
                        txtMaNV.Text = nhanVien.MaNhanVien;
                        txtChucVu.Text = nhanVien.ChucVu;

                        if (nhanVien.ChucVu == "Bảo vệ")
                        {
                            txtLuong.Text = "7000000";
                        }
                        else if (nhanVien.ChucVu == "Trưởng nhà")
                        {
                            txtLuong.Text = "10000000";
                        }
                        else if (nhanVien.ChucVu == "Giám đốc")
                        {
                            txtLuong.Text = "12000000";
                        }
                        else if (nhanVien.ChucVu == "Lao công")
                        {
                            txtLuong.Text = "8500000";
                        }
                        else if (nhanVien.ChucVu == "Quản lý")
                        {
                            txtLuong.Text = "9500000";
                        }
                        else if (nhanVien.ChucVu == "Kế toán")
                        {
                            txtLuong.Text = "9000000";
                        }
                        else if (nhanVien.ChucVu == "Nhân viên thu phí")
                        {
                            txtLuong.Text = "7500000";
                        }
                        else if (nhanVien.ChucVu == "Nhân viên khác")
                        {
                            txtLuong.Text = "8000000";
                        }
                        else if (nhanVien.ChucVu == "Nhân viên văn phòng")
                        {
                            txtLuong.Text = "8500000";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nhân viên này đã nghỉ làm.");
                        txtMaNV.Clear();
                        txtChucVu.Clear();
                        txtLuong.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên có mã số này. Vui lòng nhập lại !!!");
                    txtTimKiem.Clear();
                    txtTimKiem.Focus();
                }
            }
            txtTimKiem.Clear();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (txtThanhToan.Text != txtLuong.Text)
            {
                MessageBox.Show("Số tiền thanh toán không trùng với số tiền lương của nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new QLKyTucXaEntities())
            {
                try
                {
                    string maNhanVien = txtMaNV.Text;

                    var nhanVien = db.NHANVIENs.FirstOrDefault(nv => nv.MaNhanVien == maNhanVien);

                    if (nhanVien != null)
                    {
                        DateTime ngayThanhToan = dateNgayThanhToan.Value;
                        var lastPayment = db.LUONGs
                            .Where(l => l.MaNhanVien == maNhanVien && DbFunctions.TruncateTime(l.Thang).Value.Month == ngayThanhToan.Month && DbFunctions.TruncateTime(l.Thang).Value.Year == ngayThanhToan.Year)
                            .OrderByDescending(l => l.Thang)
                            .FirstOrDefault();

                        if (lastPayment != null)
                        {
                            MessageBox.Show("Đã thanh toán lương cho nhân viên này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        LUONG luong = new LUONG
                        {
                            Thang = ngayThanhToan,
                            Tien = int.Parse(txtLuong.Text),
                            MaNhanVien = maNhanVien
                        };

                        db.LUONGs.Add(luong);
                        db.SaveChanges();

                        LoadData();

                        MessageBox.Show("Thanh toán lương thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtMaNV.Clear();
                        txtChucVu.Clear();
                        txtLuong.Clear();
                        txtTimKiem.Focus();
                        txtThanhToan.Clear();
                        btnHoi.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Mã nhân viên không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaNV.Clear();
                        txtMaNV.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thanh toán lương: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaNV.Clear();
                    txtMaNV.Focus();
                }
            }
        }

        private void btnHoi_Click(object sender, EventArgs e)
        {
            using (var db = new QLKyTucXaEntities())
            {
                try
                {
                    var luongThanhToan = db.LUONGs.OrderByDescending(l => l.ID).ToList();

                    if (luongThanhToan.Any())
                    {
                        // Lấy thông tin thanh toán lương mới nhất
                        var luongThanhToanMoiNhat = luongThanhToan.First();

                        // Xóa thông tin thanh toán lương mới nhất
                        db.LUONGs.Remove(luongThanhToanMoiNhat);
                        db.SaveChanges();

                        LoadData();

                        MessageBox.Show("Hủy thanh toán lương thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin thanh toán lương để hủy!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi hủy thanh toán lương: " + ex.Message);
                }
            }
        }

        private void dgvLuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvLuong.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                // Lấy dòng được chọn
                DataGridViewRow row = dgvLuong.Rows[e.RowIndex];

                // Đổ dữ liệu từ dòng được chọn vào các TextBox và DateTimePicker
                txtMaNV.Text = row.Cells["MaNhanVien"].Value.ToString();
                txtChucVu.Text = row.Cells["ChucVu"].Value.ToString();
                
                dateNgayThanhToan.Value = Convert.ToDateTime(row.Cells["Thang"].Value);
                txtLuong.Text = row.Cells["Tien"].Value.ToString();
                
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Save Excel File";
            saveFileDialog.FileName = "DanhSachLuong"; // Tên mặc định cho tập tin Excel

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    exportExcel(dgvLuong, filePath); // Xuất dữ liệu từ DataGridView dgvDSPhong vào tập tin Excel tại đường dẫn đã chọn
                    MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void exportExcel(DataGridView dataGridView, string filePath)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = false;
            excelApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)excelApp.ActiveSheet;

            // Lưu tiêu đề cột
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dataGridView.Columns[i].HeaderText;
            }

            // Lưu dữ liệu từ DataGridView
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells[j].Value.ToString();
                }
            }

            // Lưu tập tin Excel
            worksheet.SaveAs(filePath);
            excelApp.Quit();
            ReleaseObject(worksheet);
            ReleaseObject(excelApp);
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImportFromExcel(openFileDialog.FileName);
            }
        }

        private void ImportFromExcel(string filePath)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Open(filePath);
            Excel.Worksheet worksheet = workbook.ActiveSheet;

            // Xác định số hàng và số cột có dữ liệu
            int rowCount = worksheet.UsedRange.Rows.Count;
            int columnCount = worksheet.UsedRange.Columns.Count;

            // Xóa dữ liệu hiện tại trong DataGridView
            dgvLuong.DataSource = null;

            dgvLuong.Columns.Add("ID", "ID");
            dgvLuong.Columns.Add("Thang", "Ngày nhận lương");
            dgvLuong.Columns.Add("Tien", "Tiền");
            dgvLuong.Columns.Add("MaNhanVien", "Mã nhân viên");
            dgvLuong.Columns.Add("ChucVu", "Chức vụ");
            

            // Đổ dữ liệu từ Excel vào DataGridView
            for (int i = 2; i <= rowCount; i++)
            {
                dgvLuong.Rows.Add(worksheet.Cells[i, 1].Value, 
                                 worksheet.Cells[i, 2].Value, 
                                 worksheet.Cells[i, 3].Value, 
                                 worksheet.Cells[i, 4].Value,
                                 worksheet.Cells[i, 5].Value); 
                                 
            }

            workbook.Close();
            excelApp.Quit();

            // Giải phóng tài nguyên
            ReleaseObject(worksheet);
            ReleaseObject(workbook);
            ReleaseObject(excelApp);

            MessageBox.Show("Import file Excel thành công!");
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            btnTimKiem.Enabled = true;
        }

        private void txtThanhToan_TextChanged(object sender, EventArgs e)
        {
            btnThanhToan.Enabled = true;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            
            using (var db = new QLKyTucXaEntities())
            {
                var luongList = db.LUONGs.OrderBy(p => p.ID).ToList();

                dataTable.Columns.Add("ID", typeof(Int64));
                dataTable.Columns.Add("Thang", typeof(DateTime));
                dataTable.Columns.Add("Tien", typeof(long));
                dataTable.Columns.Add("MaNhanVien", typeof(string));
               

                foreach (var luong in luongList)
                {
                    dataTable.Rows.Add(luong.ID, luong.Thang, luong.Tien, luong.MaNhanVien);
                }
            }

            ReportDocument reportDocument = new ReportDocument();

            string reportPath = "D:\\Năm 3 C# LT Quản Lý\\QuanLyKyTucXa\\QuanLyKyTucXa\\Report\\rpt_Luong.rpt";

            reportDocument.Load(reportPath);

            reportDocument.SetDataSource(dataTable);

            frmReport r = new frmReport();

            r.crystalReportViewer1.ReportSource = reportDocument;

            r.ShowDialog();
        }
    }
}
