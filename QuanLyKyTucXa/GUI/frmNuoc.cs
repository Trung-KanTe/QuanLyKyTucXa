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
    public partial class frmNuoc : Form
    {
        public frmNuoc()
        {
            InitializeComponent();
            txtChiSoDau.TextChanged += txtChiSoDau_TextChanged;
            txtChiSoCuoi.TextChanged += txtChiSoCuoi_TextChanged;
            txtGia.TextChanged += txtGia_TextChanged;
        }

        public string s;
        private void ThanhToan()
        {
            // Kiểm tra xem có đủ giá trị trong ba ô nhập liệu không
            if (!string.IsNullOrEmpty(txtChiSoDau.Text) && !string.IsNullOrEmpty(txtChiSoCuoi.Text) && !string.IsNullOrEmpty(txtGia.Text))
            {
                // Thực hiện tính toán
                int chiSoDau = int.Parse(txtChiSoDau.Text);
                int chiSoCuoi = int.Parse(txtChiSoCuoi.Text);
                int gia = int.Parse(txtGia.Text);
                int soTienThanhToan = (chiSoCuoi - chiSoDau) * gia;

                // Hiển thị kết quả tính toán trong ô txtSoTienThanhToan
                txtSoTienThanhToan.Text = soTienThanhToan.ToString();
            }
        }

        private void frmNuoc_Load(object sender, EventArgs e)
        {
            this.Location = new Point(488, 165);
            txtMaSV.Focus();
            LoadData();
            DieuChinhCot();

            if (s == "User")
            {
                btnHoi.Visible = false;
                btnThanhToan.Visible = false;
                btnLamMoi.Visible = false;
                btnIn.Visible = false;
                btnImport.Visible = false;
                btnXuatExcel.Visible = false;
                txtThanhToan.Visible = false;
            }
        }

        private void DieuChinhCot()
        {
            dgvNuoc.Columns["ID"].Width = 50;
            dgvNuoc.Columns["ChiSoDau"].Width = 100;
            dgvNuoc.Columns["ChiSoCuoi"].Width = 100;
            dgvNuoc.Columns["Gia"].Width = 100;
            dgvNuoc.Columns["NgayThanhToan"].Width = 140;
            dgvNuoc.Columns["Tien"].Width = 100;
            dgvNuoc.Columns["MaSinhVien"].Width = 100;

            dgvNuoc.Columns["ID"].HeaderText = "ID";
            dgvNuoc.Columns["ChiSoDau"].HeaderText = "Chỉ số đầu";
            dgvNuoc.Columns["ChiSoCuoi"].HeaderText = "Chỉ số cuối";
            dgvNuoc.Columns["Gia"].HeaderText = "Giá";
            dgvNuoc.Columns["NgayThanhToan"].HeaderText = "Ngày thanh toán";
            dgvNuoc.Columns["Tien"].HeaderText = "Tiền";
            dgvNuoc.Columns["MaSinhVien"].HeaderText = "Mã sinh viên";
        }

        private void LoadData()
        {
            using (var db = new QLKyTucXaEntities())
            {
                // Truy vấn dữ liệu từ bảng nuoc
                var nuocData = (from d in db.NUOCs
                                select new
                                {
                                    d.ID,
                                    d.ChiSoDau,
                                    d.ChiSoCuoi,
                                    d.Gia,
                                    d.NgayThanhToan,
                                    d.Tien,
                                    d.MaSinhVien
                                }).ToList();

                // Hiển thị dữ liệu lên DataGridView dgvNuoc
                dgvNuoc.DataSource = nuocData;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            frmThanhToan tp = new frmThanhToan();
            tp.Show();
        }

        private void clear()
        {
            txtMaSV.Clear();
            txtChiSoDau.Clear();
            txtChiSoCuoi.Clear();
            txtSoTienThanhToan.Clear();
            txtThanhToan.Clear();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maSinhVien = txtTimKiem.Text;

            using (var db = new QLKyTucXaEntities())
            {
                // Kiểm tra xem sinh viên có tồn tại trong bảng nuoc hay không
                var nuoc = db.NUOCs.FirstOrDefault(d => d.MaSinhVien == maSinhVien);

                if (nuoc != null)
                {
                    // Hiển thị thông tin điện đã thanh toán lên các TextBox
                    txtChiSoDau.Text = nuoc.ChiSoDau.ToString();
                    txtChiSoCuoi.Text = nuoc.ChiSoCuoi.ToString();
                    txtGia.Text = nuoc.Gia.ToString();
                    dateNgayThanhToan.Value = nuoc.NgayThanhToan.Value;
                    txtSoTienThanhToan.Text = nuoc.Tien.ToString();
                    txtMaSV.Text = nuoc.MaSinhVien;
                    MessageBox.Show("Tìm kiếm sinh viên thành công!", "Thông tin", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    // Thông báo nếu sinh viên chưa thanh toán tiền điện
                    MessageBox.Show("Sinh viên chưa thanh toán tiền nước.");
                }
            }
        }

        private void dgvNuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvNuoc.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                // Lấy dòng được chọn
                DataGridViewRow row = dgvNuoc.Rows[e.RowIndex];

                // Đổ dữ liệu từ dòng được chọn vào các TextBox và DateTimePicker
                txtChiSoDau.Text = row.Cells["ChiSoDau"].Value.ToString();
                txtChiSoCuoi.Text = row.Cells["ChiSoCuoi"].Value.ToString();
                txtGia.Text = row.Cells["Gia"].Value.ToString();
                dateNgayThanhToan.Value = Convert.ToDateTime(row.Cells["NgayThanhToan"].Value);
                txtSoTienThanhToan.Text = row.Cells["Tien"].Value.ToString();
                txtMaSV.Text = row.Cells["MaSinhVien"].Value.ToString();
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (txtSoTienThanhToan.Text == txtThanhToan.Text)
            {
                using (var db = new QLKyTucXaEntities())
                {
                    string maSinhVien = txtMaSV.Text;

                    var sinhVien = db.SINHVIENs.FirstOrDefault(s => s.MaSinhVien == maSinhVien);

                    if (sinhVien != null)
                    {
                        try
                        {
                            // Kiểm tra xem sinh viên đã thanh toán trong cùng một tháng chưa
                            DateTime ngayThanhToan = dateNgayThanhToan.Value;
                            var lastPayment = db.NUOCs
                                .Where(d => d.MaSinhVien == maSinhVien && DbFunctions.TruncateTime(d.NgayThanhToan).Value.Month == ngayThanhToan.Month && DbFunctions.TruncateTime(d.NgayThanhToan).Value.Year == ngayThanhToan.Year)
                                .OrderByDescending(d => d.NgayThanhToan)
                                .FirstOrDefault();

                            if (lastPayment != null)
                            {
                                MessageBox.Show("Sinh viên đã thanh toán trong cùng một tháng trước đó!", "Thông tin", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                return;
                            }

                            NUOC nuoc = new NUOC
                            {
                                ChiSoDau = int.Parse(txtChiSoDau.Text),
                                ChiSoCuoi = int.Parse(txtChiSoCuoi.Text),
                                Gia = int.Parse(txtGia.Text),
                                NgayThanhToan = ngayThanhToan,
                                Tien = int.Parse(txtSoTienThanhToan.Text),
                                MaSinhVien = maSinhVien
                            };

                            db.NUOCs.Add(nuoc);
                            db.SaveChanges();

                            LoadData();

                            MessageBox.Show("Thanh toán thành công!");
                            clear();
                            btnHoi.Enabled = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi thanh toán: " + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã sinh viên không tồn tại!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Số tiền thanh toán không khớp!");
            }
        }

        private void txtChiSoDau_TextChanged(object sender, EventArgs e)
        {
            ThanhToan();
        }

        private void txtChiSoCuoi_TextChanged(object sender, EventArgs e)
        {
            ThanhToan();
        }

        private void txtGia_TextChanged(object sender, EventArgs e)
        {
            ThanhToan();
        }

        private void btnHoi_Click(object sender, EventArgs e)
        {
            using (var db = new QLKyTucXaEntities())
            {
                try
                {
                    string maSinhVien = txtMaSV.Text;

                    // Lấy danh sách thông tin thanh toán của sinh viên có mã tương ứng, sắp xếp theo thời gian giảm dần
                    var nuocThanhToans = db.NUOCs.OrderByDescending(d => d.ID).ToList();

                    if (nuocThanhToans.Any())
                    {
                        // Lấy thông tin thanh toán mới nhất
                        var nuocThanhToanMoiNhat = nuocThanhToans.First();

                        // Xóa thông tin thanh toán mới nhất
                        db.NUOCs.Remove(nuocThanhToanMoiNhat);
                        db.SaveChanges();

                        LoadData();

                        MessageBox.Show("Hủy thanh toán thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin thanh toán để hủy!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi hủy thanh toán: " + ex.Message);
                }
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Save Excel File";
            saveFileDialog.FileName = "DanhSachNuoc"; // Tên mặc định cho tập tin Excel

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    exportExcel(dgvNuoc, filePath); // Xuất dữ liệu từ DataGridView dgvDSPhong vào tập tin Excel tại đường dẫn đã chọn
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
            dgvNuoc.DataSource = null;

            dgvNuoc.Columns.Add("ID", "ID");
            dgvNuoc.Columns.Add("ChiSoDau", "Chỉ số đầu");
            dgvNuoc.Columns.Add("ChiSoCuoi", "Chỉ số cuối");
            dgvNuoc.Columns.Add("Gia", "Giá");
            dgvNuoc.Columns.Add("NgayThanhToan", "Ngày thanh toán");
            dgvNuoc.Columns.Add("Tien", "Tiền");
            dgvNuoc.Columns.Add("MaSinhVien", "Mã sinh viên");

            // Đổ dữ liệu từ Excel vào DataGridView
            for (int i = 2; i <= rowCount; i++)
            {
                dgvNuoc.Rows.Add(worksheet.Cells[i, 1].Value, // ID
                                 worksheet.Cells[i, 2].Value, // Chỉ số đầu
                                 worksheet.Cells[i, 3].Value, // Chỉ số cuối
                                 worksheet.Cells[i, 4].Value, // Giá
                                 worksheet.Cells[i, 5].Value, // Ngày thanh toán
                                 worksheet.Cells[i, 6].Value, // Tiền
                                 worksheet.Cells[i, 7].Value); // Mã sinh viên
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

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            btnTimKiem.Enabled = true;
        }

        private void txtMaSV_TextChanged(object sender, EventArgs e)
        {
            btnLamMoi.Enabled = true;
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
                var nuocList = db.NUOCs.OrderBy(nv => nv.ID).ToList();

                dataTable.Columns.Add("ID", typeof(Int64));

                dataTable.Columns.Add("ChiSoDau", typeof(Int64));
                dataTable.Columns.Add("ChiSoCuoi", typeof(Int64));
                dataTable.Columns.Add("Gia", typeof(Int64));
                dataTable.Columns.Add("NgayThanhToan", typeof(DateTime));
                dataTable.Columns.Add("Tien", typeof(Int64));
                dataTable.Columns.Add("MaSinhVien", typeof(string));

                foreach (var dien in nuocList)
                {
                    dataTable.Rows.Add(dien.ID, dien.ChiSoDau, dien.ChiSoCuoi, dien.Gia, dien.NgayThanhToan, dien.Tien, dien.MaSinhVien);
                }
            }

            ReportDocument reportDocument = new ReportDocument();

            string reportPath = "D:\\Năm 3 C# LT Quản Lý\\QuanLyKyTucXa\\QuanLyKyTucXa\\Report\\rpt_Nuoc.rpt";

            reportDocument.Load(reportPath);

            reportDocument.SetDataSource(dataTable);

            frmReport r = new frmReport();

            r.crystalReportViewer1.ReportSource = reportDocument;

            r.ShowDialog();
        }
    }
}
