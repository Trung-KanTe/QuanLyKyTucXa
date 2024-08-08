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
    public partial class frmDienNuoc : Form
    {
        public frmDienNuoc()
        {
            InitializeComponent();
            txtChiSoDau.TextChanged += txtChiSoDau_TextChanged;
            txtChiSoCuoi.TextChanged += txtChiSoCuoi_TextChanged;
            txtGia.TextChanged += txtGia_TextChanged;
        }

        public string s;

        private void ThanhToan()
        {
            if (!string.IsNullOrEmpty(txtChiSoDau.Text) && !string.IsNullOrEmpty(txtChiSoCuoi.Text) && !string.IsNullOrEmpty(txtGia.Text))
            {
                int chiSoDau = int.Parse(txtChiSoDau.Text);
                int chiSoCuoi = int.Parse(txtChiSoCuoi.Text);
                int gia = int.Parse(txtGia.Text);
                int soTienThanhToan = (chiSoCuoi - chiSoDau) * gia;

                txtSoTienThanhToan.Text = soTienThanhToan.ToString();
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            frmThanhToan tp = new frmThanhToan();
            tp.Show();
        }

        private void DieuChinhCot()
        {
            dgvDien.Columns["ID"].Width = 50;
            dgvDien.Columns["ChiSoDau"].Width = 100;
            dgvDien.Columns["ChiSoCuoi"].Width = 100;
            dgvDien.Columns["Gia"].Width = 100;
            dgvDien.Columns["NgayThanhToan"].Width = 140;
            dgvDien.Columns["Tien"].Width = 100;
            dgvDien.Columns["MaSinhVien"].Width = 100;

            dgvDien.Columns["ID"].HeaderText = "ID";
            dgvDien.Columns["ChiSoDau"].HeaderText = "Chỉ số đầu";
            dgvDien.Columns["ChiSoCuoi"].HeaderText = "Chỉ số cuối";
            dgvDien.Columns["Gia"].HeaderText = "Giá";
            dgvDien.Columns["NgayThanhToan"].HeaderText = "Ngày thanh toán";
            dgvDien.Columns["Tien"].HeaderText = "Tiền";
            dgvDien.Columns["MaSinhVien"].HeaderText = "Mã sinh viên";
        }

        private void frmDienNuoc_Load(object sender, EventArgs e)
        {
            txtMaSV.Focus();
            this.Location = new Point(488, 165);
            
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
                txtThanhToan.Visible=false;
            }    
        }

        private void LoadData()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var dienData = (from d in db.DIENs
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

                dgvDien.DataSource = dienData;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSV.Clear();
            txtChiSoDau.Clear();
            txtChiSoCuoi.Clear();
            txtMaSV.Focus();
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maSinhVien = txtTimKiem.Text;

            using (var db = new QLKyTucXaEntities())
            {
                var dien = db.DIENs.FirstOrDefault(d => d.MaSinhVien == maSinhVien);

                if (dien != null)
                {
                    txtChiSoDau.Text = dien.ChiSoDau.ToString();
                    txtChiSoCuoi.Text = dien.ChiSoCuoi.ToString();
                    txtGia.Text = dien.Gia.ToString();
                    dateNgayThanhToan.Value = dien.NgayThanhToan.Value;
                    txtSoTienThanhToan.Text = dien.Tien.ToString();
                    txtMaSV.Text = dien.MaSinhVien;
                }
                else
                {
                    MessageBox.Show("Sinh viên chưa thanh toán tiền điện.");
                    txtTimKiem.Clear();
                    txtTimKiem.Focus();
                }
            }
        }

        private void dgvDien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDien.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                DataGridViewRow row = dgvDien.Rows[e.RowIndex];

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
            if (txtChiSoDau.Text == "" || txtChiSoCuoi.Text == "" || txtMaSV.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                            DateTime ngayThanhToan = dateNgayThanhToan.Value;
                            var lastPayment = db.DIENs
                                .Where(d => d.MaSinhVien == maSinhVien && DbFunctions.TruncateTime(d.NgayThanhToan).Value.Month == ngayThanhToan.Month && DbFunctions.TruncateTime(d.NgayThanhToan).Value.Year == ngayThanhToan.Year)
                                .OrderByDescending(d => d.NgayThanhToan)
                                .FirstOrDefault();

                            if (lastPayment != null)
                            {
                                MessageBox.Show("Sinh viên đã thanh toán trong cùng một tháng trước đó!", "Thông tin", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                return;
                            }

                            DIEN dien = new DIEN
                            {
                                ChiSoDau = int.Parse(txtChiSoDau.Text),
                                ChiSoCuoi = int.Parse(txtChiSoCuoi.Text),
                                Gia = int.Parse(txtGia.Text),
                                NgayThanhToan = ngayThanhToan,
                                Tien = int.Parse(txtSoTienThanhToan.Text),
                                MaSinhVien = maSinhVien
                            };

                            db.DIENs.Add(dien);
                            db.SaveChanges();

                            LoadData();

                            MessageBox.Show("Thanh toán thành công!");
                            btnHoi.Enabled = true;
                            txtMaSV.Focus();
                            txtMaSV.Clear();
                            txtChiSoDau.Clear();
                            txtChiSoCuoi.Clear();
                            txtThanhToan.Clear();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi thanh toán: " + ex.Message);
                            txtMaSV.Focus();
                            txtMaSV.Clear();
                            txtChiSoDau.Clear();
                            txtChiSoCuoi.Clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã sinh viên không tồn tại!");
                        txtMaSV.Focus();
                        txtMaSV.Clear();
                        txtChiSoDau.Clear();
                        txtChiSoCuoi.Clear();
                    }
                }
            }
            else
            {
                MessageBox.Show("Số tiền thanh toán không khớp!");
                txtThanhToan.Clear();
                txtThanhToan.Focus();
            }
        }

        private void btnHoi_Click(object sender, EventArgs e)
        {
            using (var db = new QLKyTucXaEntities())
            {
                try
                {
                    var dienThanhToan = db.DIENs.OrderByDescending(d => d.ID).ToList();

                    if (dienThanhToan.Any())
                    {
                        var dienThanhToanMoiNhat = dienThanhToan.First();

                        db.DIENs.Remove(dienThanhToanMoiNhat);
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

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            btnTimKiem.Enabled = true;
        }

        private void txtThanhToan_TextChanged(object sender, EventArgs e)
        {
            btnThanhToan.Enabled = true;
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Save Excel File";
            saveFileDialog.FileName = "DanhSachDien"; 

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    exportExcel(dgvDien, filePath); 
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

            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dataGridView.Columns[i].HeaderText;
            }

            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells[j].Value.ToString();
                }
            }

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

            int rowCount = worksheet.UsedRange.Rows.Count;
            int columnCount = worksheet.UsedRange.Columns.Count;

            dgvDien.DataSource = null;

            dgvDien.Columns.Add("ID", "ID");
            dgvDien.Columns.Add("ChiSoDau", "Chỉ số đầu");
            dgvDien.Columns.Add("ChiSoCuoi", "Chỉ số cuối");
            dgvDien.Columns.Add("Gia", "Giá");
            dgvDien.Columns.Add("NgayThanhToan", "Ngày thanh toán");
            dgvDien.Columns.Add("Tien", "Tiền");
            dgvDien.Columns.Add("MaSinhVien", "Mã sinh viên");

            for (int i = 2; i <= rowCount; i++)
            {
                dgvDien.Rows.Add(worksheet.Cells[i, 1].Value,
                                 worksheet.Cells[i, 2].Value,
                                 worksheet.Cells[i, 3].Value,
                                 worksheet.Cells[i, 4].Value,
                                 worksheet.Cells[i, 5].Value,
                                 worksheet.Cells[i, 6].Value,
                                 worksheet.Cells[i, 7].Value);
            }

            workbook.Close();
            excelApp.Quit();

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
                var dienList = db.DIENs.OrderBy(nv => nv.ID).ToList();

                dataTable.Columns.Add("ID", typeof(Int64));

                dataTable.Columns.Add("ChiSoDau", typeof(Int64));
                dataTable.Columns.Add("ChiSoCuoi", typeof(Int64));
                dataTable.Columns.Add("Gia", typeof(Int64));
                dataTable.Columns.Add("NgayThanhToan", typeof(DateTime));
                dataTable.Columns.Add("Tien", typeof(Int64));
                dataTable.Columns.Add("MaSinhVien", typeof(string));

                foreach (var dien in dienList)
                {
                    dataTable.Rows.Add(dien.ID, dien.ChiSoDau, dien.ChiSoCuoi, dien.Gia, dien.NgayThanhToan, dien.Tien, dien.MaSinhVien);
                }
            }

            ReportDocument reportDocument = new ReportDocument();

            string reportPath = "D:\\Năm 3 C# LT Quản Lý\\QuanLyKyTucXa\\QuanLyKyTucXa\\Report\\rpt_Dien.rpt";

            reportDocument.Load(reportPath);

            reportDocument.SetDataSource(dataTable);

            frmReport r = new frmReport();

            r.crystalReportViewer1.ReportSource = reportDocument;

            r.ShowDialog();
        }
    }
}
