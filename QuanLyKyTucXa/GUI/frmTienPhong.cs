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
    public partial class frmTienPhong : Form
    {
        public frmTienPhong()
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

        private void frmTienPhong_Load(object sender, EventArgs e)
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

        private void DieuChinhCot()
        {
            dgvTienPhong.Columns["ID"].Width = 70;
            dgvTienPhong.Columns["Thang"].Width = 130;
            dgvTienPhong.Columns["Tien"].Width = 130;
            dgvTienPhong.Columns["MaSinhVien"].Width = 130;
            dgvTienPhong.Columns["MaPhong"].Width = 140;

            dgvTienPhong.Columns["ID"].HeaderText = "ID";
            dgvTienPhong.Columns["Thang"].HeaderText = "Ngày thanh toán";
            dgvTienPhong.Columns["Tien"].HeaderText = "Tiền phòng";
            dgvTienPhong.Columns["MaSinhVien"].HeaderText = "Mã sinh viên";
            dgvTienPhong.Columns["MaPhong"].HeaderText = "Mã phòng";
        }

        private void LoadData()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var phongData = (from l in db.THANHTOANs
                                 select new
                                 {
                                     l.ID,
                                     l.Thang,
                                     l.Tien,
                                     l.MaSinhVien,
                                     l.MaPhong
                                 }).ToList();

                dgvTienPhong.DataSource = phongData;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maSinhVienCanTim = txtTimKiem.Text.Trim();

            using (var db = new QLKyTucXaEntities())
            {
                var sinhVien = db.SINHVIENs.FirstOrDefault(sv => sv.MaSinhVien == maSinhVienCanTim);

                if (sinhVien != null)
                {
                    txtMaSV.Text = sinhVien.MaSinhVien;
                    txtMaPhong.Text = sinhVien.MaPhong;
                    MessageBox.Show("Đã tìm thấy sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên có mã số này. Vui lòng nhập lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtTimKiem.Clear();
                    txtTimKiem.Focus();
                    
                }
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            using (var db = new QLKyTucXaEntities())
            {
                try
                {
                    if (txtThanhToan.Text.Equals(txtTienPhong.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        string maSinhVien = txtMaSV.Text;

                        var sinhVien = db.SINHVIENs.FirstOrDefault(sv => sv.MaSinhVien == maSinhVien);

                        if (sinhVien != null)
                        {
                            DateTime ngayThanhToan = dateNgayThanhToan.Value;
                            var lastPayment = db.THANHTOANs
                                .Where(tt => tt.MaSinhVien == maSinhVien && DbFunctions.TruncateTime(tt.Thang).Value.Month == ngayThanhToan.Month && DbFunctions.TruncateTime(tt.Thang).Value.Year == ngayThanhToan.Year)
                                .OrderByDescending(tt => tt.Thang)
                                .FirstOrDefault();

                            if (lastPayment != null)
                            {
                                MessageBox.Show("Sinh viên đã thanh toán tiền phòng trong cùng một tháng trước đó!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            THANHTOAN thanhToan = new THANHTOAN
                            {
                                Thang = ngayThanhToan,
                                Tien = int.Parse(txtThanhToan.Text),
                                MaSinhVien = maSinhVien,
                                MaPhong = txtMaPhong.Text
                            };

                            db.THANHTOANs.Add(thanhToan);
                            db.SaveChanges();

                            LoadData();

                            MessageBox.Show("Thanh toán tiền phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            txtMaSV.Clear();
                            txtTimKiem.Focus();
                            txtTimKiem.Clear();
                            txtMaPhong.Clear();
                            btnHoi.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Mã sinh viên không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtMaSV.Clear();
                            txtMaSV.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Số tiền thanh toán không trùng khớp với tiền phòng.\nVui lòng thanh toán lại!!!", "Lỗi");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thanh toán tiền phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaSV.Clear();
                    txtMaSV.Focus();
                }
            }
        }

        private void btnHoi_Click(object sender, EventArgs e)
        {
            using (var db = new QLKyTucXaEntities())
            {
                try
                {
                    var phongThanhToan = db.THANHTOANs.OrderByDescending(l => l.ID).ToList();

                    if (phongThanhToan.Any())
                    {
                        var phongThanhToanMoiNhat = phongThanhToan.First();

                        db.THANHTOANs.Remove(phongThanhToanMoiNhat);
                        db.SaveChanges();

                        LoadData();

                        MessageBox.Show("Hủy thanh toán tiền phòng thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin thanh toán phòng để hủy!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi hủy thanh toán tiền phòng: " + ex.Message);
                }
            }
        }

        private void dgvTienPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvTienPhong.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                DataGridViewRow row = dgvTienPhong.Rows[e.RowIndex];

                txtMaSV.Text = row.Cells["MaSinhVien"].Value.ToString();
                txtMaPhong.Text = row.Cells["MaPhong"].Value.ToString();

                dateNgayThanhToan.Value = Convert.ToDateTime(row.Cells["Thang"].Value);
                txtTienPhong.Text = row.Cells["Tien"].Value.ToString();

            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Save Excel File";
            saveFileDialog.FileName = "DanhSachTienPhong"; // Tên mặc định cho tập tin Excel

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    exportExcel(dgvTienPhong, filePath); // Xuất dữ liệu từ DataGridView dgvDSPhong vào tập tin Excel tại đường dẫn đã chọn
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
            dgvTienPhong.DataSource = null;

            dgvTienPhong.Columns.Add("ID", "ID");
            dgvTienPhong.Columns.Add("Thang", "Ngày thanh toán");
            dgvTienPhong.Columns.Add("Tien", "Tiền phòng");
            dgvTienPhong.Columns.Add("MaSinhVien", "Mã sinh viên");
            dgvTienPhong.Columns.Add("MaPhong", "Mã phòng");

            // Đổ dữ liệu từ Excel vào DataGridView
            for (int i = 2; i <= rowCount; i++)
            {
                dgvTienPhong.Rows.Add(worksheet.Cells[i, 1].Value, // ID
                                 worksheet.Cells[i, 2].Value, // Chỉ số đầu
                                 worksheet.Cells[i, 3].Value, // Chỉ số cuối
                                 worksheet.Cells[i, 4].Value,
                                 worksheet.Cells[i, 5].Value); // Mã sinh viên
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

        private void txtThanhToan_TextChanged(object sender, EventArgs e)
        {
            btnThanhToan.Enabled = true;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();

            using (var db = new QLKyTucXaEntities())
            {
                var nuocList = db.THANHTOANs.OrderBy(nv => nv.ID).ToList();

                dataTable.Columns.Add("ID", typeof(Int64));

                dataTable.Columns.Add("Thang", typeof(DateTime));
                dataTable.Columns.Add("Tien", typeof(Int64));
                dataTable.Columns.Add("MaSinhVien", typeof(string));
                dataTable.Columns.Add("MaPhong", typeof(string));

                foreach (var dien in nuocList)
                {
                    dataTable.Rows.Add(dien.ID, dien.Thang, dien.Tien, dien.MaSinhVien, dien.MaPhong);
                }
            }

            ReportDocument reportDocument = new ReportDocument();

            string reportPath = "D:\\Năm 3 C# LT Quản Lý\\QuanLyKyTucXa\\QuanLyKyTucXa\\Report\\rpt_TienPhong.rpt";

            reportDocument.Load(reportPath);

            reportDocument.SetDataSource(dataTable);

            frmReport r = new frmReport();

            r.crystalReportViewer1.ReportSource = reportDocument;

            r.ShowDialog();
        }
    }
}
