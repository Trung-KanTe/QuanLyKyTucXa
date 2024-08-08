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
    public partial class frmTaiKhoan : Form
    {
        public frmTaiKhoan()
        {
            InitializeComponent();
        }

        public string t;
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void frmTaiKhoan_Load(object sender, EventArgs e)
        {
            this.Location = new Point(488, 165);
            LoadData();
            DieuChinhCot();
            cbLoc.Items.Insert(0, "--Chọn--");
            cbLoc.SelectedIndex = 0;
            if (t == "User")
            {
                btnIn.Visible = false;
                btnLamMoi.Visible = false;
                btnSua.Visible = false;
                btnXoa.Visible = false;
                btnImport.Visible = false;
                btnXuatExcel.Visible = false;
            }    
        }

        private void DieuChinhCot() 
        {
            dgvDSNguoiDung.Columns["TenNguoiDung"].HeaderText = "Tên người dùng";
            dgvDSNguoiDung.Columns["MatKhau"].HeaderText = "Mật khẩu";
            dgvDSNguoiDung.Columns["MaSinhVien"].HeaderText = "Mã sinh viên";
            dgvDSNguoiDung.Columns["MaNhanVien"].HeaderText = "Mã nhân viên";

        }

        private void LoadData()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var data = db.TAIKHOANs
                    .Where(t => t.PhanQuyen != 1)
                    .Select(t => new
                    {
                        t.TenNguoiDung,
                        t.MatKhau,
                        t.MaSinhVien,
                        t.MaNhanVien
                    })
                    .ToList();
                dgvDSNguoiDung.DataSource = data;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string maSinhVien = txtMaSV.Text.Trim();
                string maNhanVien = txtMaNV.Text.Trim();

                // Kiểm tra xem người dùng đã nhập mã sinh viên hoặc mã nhân viên hay chưa
                if (!string.IsNullOrEmpty(maSinhVien) || !string.IsNullOrEmpty(maNhanVien))
                {
                    using (var db = new QLKyTucXaEntities())
                    {
                        // Tìm tài khoản dựa trên mã sinh viên hoặc mã nhân viên
                        var taiKhoan = db.TAIKHOANs
                            .FirstOrDefault(t => t.MaSinhVien == maSinhVien || t.MaNhanVien == maNhanVien);

                        // Nếu tìm thấy tài khoản, xóa nó
                        if (taiKhoan != null)
                        {
                            db.TAIKHOANs.Remove(taiKhoan);
                            db.SaveChanges();

                            MessageBox.Show("Xóa tài khoản thành công.");
                            // Sau khi xóa thành công, bạn có thể gọi lại phương thức LoadData để tải lại dữ liệu
                            LoadData();
                            clearAll();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy tài khoản với mã sinh viên hoặc mã nhân viên đã nhập.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập mã sinh viên hoặc mã nhân viên để xóa tài khoản.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa tài khoản: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string maSinhVien = txtMaSV.Text.Trim();
                string maNhanVien = txtMaNV.Text.Trim();
                string tenNguoiDungMoi = txtUsername.Text.Trim();
                string matKhauMoi = txtPassword.Text.Trim();

                
                // Kiểm tra xem người dùng đã nhập mã sinh viên hoặc mã nhân viên và tên người dùng mới và mật khẩu mới hay chưa
                if ((!string.IsNullOrEmpty(maSinhVien) || !string.IsNullOrEmpty(maNhanVien)))
                {
                    using (var db = new QLKyTucXaEntities())
                    {
                        

                        var taiKhoan = db.TAIKHOANs
                            .FirstOrDefault(t => t.MaSinhVien == maSinhVien || t.MaNhanVien == maNhanVien);

                        if (taiKhoan != null)
                        {
                            if (!string.IsNullOrEmpty(tenNguoiDungMoi) && !string.IsNullOrEmpty(matKhauMoi))
                            {
                                var newTaiKhoan = new TAIKHOAN
                                {
                                    TenNguoiDung = tenNguoiDungMoi,
                                    MatKhau = matKhauMoi,
                                    MaSinhVien = !string.IsNullOrEmpty(maSinhVien) ? maSinhVien : taiKhoan.MaSinhVien,
                                    MaNhanVien = !string.IsNullOrEmpty(maNhanVien) ? maNhanVien : taiKhoan.MaNhanVien
                                };
                                db.TAIKHOANs.Remove(taiKhoan);

                                db.TAIKHOANs.Add(newTaiKhoan);
                            }
                            else if (!string.IsNullOrEmpty(matKhauMoi))
                            {
                                taiKhoan.MatKhau = matKhauMoi;
                            }

                            db.SaveChanges();

                            MessageBox.Show("Cập nhật tài khoản thành công.");
                            clearAll();
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy tài khoản với mã sinh viên hoặc mã nhân viên đã nhập.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin để cập nhật tài khoản.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật tài khoản: " + ex.Message);
            }

        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void clearAll()
        {
            txtPassword.Clear();
            txtUsername.Clear();
            txtTimKiem.Clear();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTimKiem.Text.Trim();

                // Kiểm tra xem người dùng đã nhập từ khóa tìm kiếm hay chưa
                if (!string.IsNullOrEmpty(keyword))
                {
                    using (var db = new QLKyTucXaEntities())
                    {
                        // Tìm tài khoản dựa trên mã sinh viên hoặc mã nhân viên
                        var taiKhoan = db.TAIKHOANs
                            .FirstOrDefault(t => t.MaSinhVien == keyword || t.MaNhanVien == keyword);

                        // Nếu tìm thấy tài khoản, hiển thị thông tin
                        if (taiKhoan != null)
                        {
                            txtMaNV.Text = taiKhoan.MaNhanVien;
                            txtMaSV.Text = taiKhoan.MaSinhVien;
                            txtPassword.Text = taiKhoan.MatKhau;
                            txtUsername.Text = taiKhoan.TenNguoiDung;
                            MessageBox.Show("Đã tìm thấy thông tin người dùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy tài khoản với mã sinh viên hoặc mã nhân viên đã nhập.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập mã sinh viên hoặc mã nhân viên để tìm kiếm.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tìm kiếm tài khoản: " + ex.Message);
            }
        }

        private void dgvDSNguoiDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDSNguoiDung.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvDSNguoiDung.Rows[e.RowIndex];

                if (selectedRow.Cells["MaSinhVien"].Value != null)
                    txtMaSV.Text = selectedRow.Cells["MaSinhVien"].Value.ToString();
                else
                    txtMaSV.Text = string.Empty;

                if (selectedRow.Cells["MaNhanVien"].Value != null)
                    txtMaNV.Text = selectedRow.Cells["MaNhanVien"].Value.ToString();
                else
                    txtMaNV.Text = string.Empty;

                if (selectedRow.Cells["TenNguoiDung"].Value != null)
                    txtUsername.Text = selectedRow.Cells["TenNguoiDung"].Value.ToString();
                else
                    txtUsername.Text = string.Empty;

                if (selectedRow.Cells["MatKhau"].Value != null)
                    txtPassword.Text = selectedRow.Cells["MatKhau"].Value.ToString();
                else
                    txtPassword.Text = string.Empty;
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            try
            {
                string loc = cbLoc.SelectedItem.ToString();

                List<TAIKHOAN> taikhoan = new List<TAIKHOAN>();

                using (var db = new QLKyTucXaEntities())
                {
                    if (loc == "Sinh viên ")
                    {
                        taikhoan = db.TAIKHOANs
                             .Where(t => t.MaSinhVien != null && t.PhanQuyen != 1)
                             .Include(t => t.NHANVIEN)
                             .Include(t => t.SINHVIEN)
                             .ToList();
                    }
                    else if (loc == "Nhân viên")
                    {
                        taikhoan = db.TAIKHOANs
                        .Where(t => t.MaNhanVien != null && t.PhanQuyen != 1)
                        .Include(t => t.NHANVIEN)
                        .ToList();
                    }
                    else if (loc == "Tổng hợp")
                    {
                        taikhoan = db.TAIKHOANs
                            .Where(t => t.MaNhanVien != null || t.MaSinhVien != null && t.PhanQuyen != 1)
                            .Include(t => t.NHANVIEN)
                            .Include(t => t.SINHVIEN)
                            .ToList();
                    }

                }

                // Hiển thị danh sách tài khoản lên DataGridView
                dgvDSNguoiDung.DataSource = taikhoan;
                DieuChinhCotLoc();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi lọc tài khoản: " + ex.Message);
            }
        }

        private void DieuChinhCotLoc()
        {
            dgvDSNguoiDung.Columns["TenNguoiDung"].HeaderText = "Tên người dùng";
            dgvDSNguoiDung.Columns["MatKhau"].HeaderText = "Mật khẩu";
            dgvDSNguoiDung.Columns["MaSinhVien"].HeaderText = "Mã sinh viên";
            dgvDSNguoiDung.Columns["MaNhanVien"].HeaderText = "Mã nhân viên";

            dgvDSNguoiDung.Columns["PhanQuyen"].Visible = false;
            dgvDSNguoiDung.Columns["NHANVIEN"].Visible = false;
            dgvDSNguoiDung.Columns["SINHVIEN"].Visible = false;

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            btnLamMoi.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            btnTimKiem.Enabled = true;
        }

        private void cbLoc_DropDown(object sender, EventArgs e)
        {
            cbLoc.Items.Remove("--Chọn--");
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Save Excel File";
            saveFileDialog.FileName = "DanhSachTaiKhoan";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    exportExcel(dgvDSNguoiDung, filePath);
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
                    if (dataGridView.Columns[j].HeaderText == "MaSinhVienOrMaNhanVien")
                    {
                        // Kiểm tra xem MaSinhVienOrMaNhanVien có giá trị là MaSinhVien hay MaNhanVien
                        object value = dataGridView.Rows[i].Cells[j].Value;
                        if (value != null)
                        {
                            string valueString = value.ToString();
                            if (!string.IsNullOrEmpty(valueString))
                            {
                                // Nếu giá trị không rỗng, điền vào cột MaSinhVien
                                worksheet.Cells[i + 2, j + 1] = valueString;
                            }
                            else
                            {
                                // Nếu giá trị rỗng, điền vào cột MaNhanVien
                                worksheet.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells["MaNhanVien"].Value.ToString();
                            }
                        }
                        else
                        {
                            // Nếu giá trị null, điền vào cột MaNhanVien
                            worksheet.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells["MaNhanVien"].Value.ToString();
                        }
                    }
                    else
                    {
                        object cellValue = dataGridView.Rows[i].Cells[j].Value;
                        if (cellValue != null)
                        {
                            worksheet.Cells[i + 2, j + 1] = cellValue.ToString();
                        }
                        else
                        {
                            worksheet.Cells[i + 2, j + 1] = string.Empty;
                        }
                    }
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
            dgvDSNguoiDung.DataSource = null;

            dgvDSNguoiDung.Columns.Add("TenNguoiDung", "Tên người dùng");
            dgvDSNguoiDung.Columns.Add("MatKhau", "Mật khẩu");
            dgvDSNguoiDung.Columns.Add("MaSinhVien", "Mã sinh viên");
            dgvDSNguoiDung.Columns.Add("MaNhanVien", "Mã nhân viên");

            // Đổ dữ liệu từ Excel vào DataGridView
            for (int i = 2; i <= rowCount; i++)
            {
                dgvDSNguoiDung.Rows.Add(worksheet.Cells[i, 1].Value, // ID
                                 worksheet.Cells[i, 2].Value, // Chỉ số đầu
                                 worksheet.Cells[i, 3].Value, // Chỉ số cuối
                                 worksheet.Cells[i, 4].Value); // Mã sinh viên
            }

            // Đóng Excel
            workbook.Close();
            excelApp.Quit();

            // Giải phóng tài nguyên
            ReleaseObject(worksheet);
            ReleaseObject(workbook);
            ReleaseObject(excelApp);

            MessageBox.Show("Import file Excel thành công!");
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();

            using (var db = new QLKyTucXaEntities())
            {
                var luongList = db.TAIKHOANs.Where(p => p.MaNhanVien != "ADMIN").OrderBy(p => p.MaSinhVien).ToList();

                dataTable.Columns.Add("TenNguoiDung", typeof(string));
                dataTable.Columns.Add("MatKhau", typeof(string));
                dataTable.Columns.Add("MaSinhVien", typeof(string));
                dataTable.Columns.Add("MaNhanVien", typeof(string));


                foreach (var luong in luongList)
                {
                    dataTable.Rows.Add(luong.TenNguoiDung, luong.MatKhau, luong.MaSinhVien, luong.MaNhanVien);
                }
            }

            ReportDocument reportDocument = new ReportDocument();

            string reportPath = "D:\\Năm 3 C# LT Quản Lý\\QuanLyKyTucXa\\QuanLyKyTucXa\\Report\\rpt_TaiKhoan.rpt";

            reportDocument.Load(reportPath);

            reportDocument.SetDataSource(dataTable);

            frmReport r = new frmReport();

            r.crystalReportViewer1.ReportSource = reportDocument;

            r.ShowDialog();
        }
    }
}
