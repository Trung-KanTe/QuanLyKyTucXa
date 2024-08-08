using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace QuanLyKyTucXa
{
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }

        public string u;

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            this.Location = new Point(488, 165);
            txtMaNV.Focus();
            LoadData();
            DieuChinhCot();
            cbChucVu.Items.Insert(0, "-Chọn chức vụ-");
            cbChucVu.SelectedIndex = 0;
            if (u == "User")
            {
                btnLamMoi.Visible = false;
                btnThem.Visible = false;
                btnSua.Visible = false;
                btnXoa.Visible = false;
                btnIn.Visible = false;
                btnImport.Visible = false;
                btnXuatExcel.Visible = false;
            }    
        }

        private void DieuChinhCot()
        {
            dgvDSNhanVien.Columns["MaNhanVien"].Width = 70;
            dgvDSNhanVien.Columns["TenNhanVien"].Width = 130;
            dgvDSNhanVien.Columns["Email"].Width = 130;
            dgvDSNhanVien.Columns["DiaChi"].Width = 100;
            dgvDSNhanVien.Columns["SDT"].Width = 80;
            dgvDSNhanVien.Columns["ChucVu"].Width = 140;
            dgvDSNhanVien.Columns["TinhTrang"].Width = 100;

            dgvDSNhanVien.Columns["MaNhanVien"].HeaderText = "Mã NV";
            dgvDSNhanVien.Columns["TenNhanVien"].HeaderText = "Tên nhân viên";
            dgvDSNhanVien.Columns["Email"].HeaderText = "Email";
            dgvDSNhanVien.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvDSNhanVien.Columns["SDT"].HeaderText = "SĐT";
            dgvDSNhanVien.Columns["ChucVu"].HeaderText = "Chức vụ";
            dgvDSNhanVien.Columns["TinhTrang"].HeaderText = "Tình trạng";

            dgvDSNhanVien.Columns["LUONGs"].Visible = false;
            dgvDSNhanVien.Columns["TAIKHOANs"].Visible = false;
        }

        private void LoadData()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var listNhanVien = db.NHANVIENs
                                    .Include("LUONGs")
                                    .Include("TAIKHOANs")
                                    .Where(nv => nv.MaNhanVien != "ADMIN")
                                    .ToList();

                dgvDSNhanVien.DataSource = listNhanVien;               
            }
        }

        private bool KTEmail(string email)
        {
            // Biểu thức chính quy để kiểm tra định dạng email
            string n = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Kiểm tra email có khớp với biểu thức chính quy không
            return Regex.IsMatch(email, n);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var db = new QLKyTucXaEntities())
            {
                string maNV = txtMaNV.Text.Trim();
                string tenNV = txtTenNV.Text.Trim();
                string emailNV = txtEmail.Text.Trim();
                string diaChiNV = txtDiaChi.Text.Trim();
                long sdtNV;

                var sinhVienTonTai = db.NHANVIENs.Any(sv => sv.MaNhanVien == maNV);
                if (sinhVienTonTai)
                {
                    MessageBox.Show("Nhân viên đã tồn tại trong cơ sở dữ liệu, không thể thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Clear();
                    txtMaNV.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(tenNV) || string.IsNullOrEmpty(emailNV) || string.IsNullOrEmpty(diaChiNV))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!long.TryParse(txtSDT.Text.Trim(), out sdtNV))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!KTEmail(txtEmail.Text))
                {
                    MessageBox.Show("Địa chỉ email không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string chucVu = cbChucVu.SelectedItem?.ToString();
                string tinhTrang = radYes.Checked ? "Yes" : "No"; 

                if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(tenNV) || string.IsNullOrEmpty(emailNV) || string.IsNullOrEmpty(diaChiNV) || string.IsNullOrEmpty(chucVu))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                NHANVIEN newNV = new NHANVIEN()
                {
                    MaNhanVien = maNV,
                    TenNhanVien = tenNV,
                    Email = emailNV,
                    DiaChi = diaChiNV,
                    SDT = sdtNV,
                    ChucVu = chucVu,
                    TinhTrang = tinhTrang
                };

                db.NHANVIENs.Add(newNV);
                db.SaveChanges();

                MessageBox.Show("Thêm nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                LoadData(); 
            }

        }

        private void Clear()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            cbChucVu.SelectedIndex = -1;
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNhanVienCanXoa = txtMaNV.Text.Trim();

            if (!string.IsNullOrEmpty(maNhanVienCanXoa))
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?\nSau khi xóa sẽ không thể khôi phục dữ liệu.", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (var db = new QLKyTucXaEntities())
                    {
                        var nhanVienCanXoa = db.NHANVIENs.FirstOrDefault(nv => nv.MaNhanVien == maNhanVienCanXoa);

                        if (nhanVienCanXoa != null)
                        {
                            var luongs = db.LUONGs.Where(l => l.MaNhanVien == maNhanVienCanXoa).ToList();
                            db.LUONGs.RemoveRange(luongs);

                            var taikhoan = db.TAIKHOANs.Where(l => l.MaNhanVien == maNhanVienCanXoa).ToList();
                            db.TAIKHOANs.RemoveRange(taikhoan);

                            db.NHANVIENs.Remove(nhanVienCanXoa);
                            db.SaveChanges(); 
                            LoadData();
                            MessageBox.Show("Đã xóa nhân viên và dữ liệu liên quan thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy nhân viên có mã tương ứng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNhanVienCanCapNhat = txtMaNV.Text.Trim(); 

            if (!string.IsNullOrEmpty(maNhanVienCanCapNhat))
            {
                using (var db = new QLKyTucXaEntities())
                {
                    var nhanVienCanCapNhat = db.NHANVIENs.FirstOrDefault(nv => nv.MaNhanVien == maNhanVienCanCapNhat);

                    if (nhanVienCanCapNhat != null)
                    {
                        nhanVienCanCapNhat.TenNhanVien = txtTenNV.Text; 
                        nhanVienCanCapNhat.Email = txtEmail.Text; 
                        nhanVienCanCapNhat.DiaChi = txtDiaChi.Text; 
                        nhanVienCanCapNhat.ChucVu = cbChucVu.SelectedItem?.ToString(); 
                        nhanVienCanCapNhat.SDT = Convert.ToInt64(txtSDT.Text); 

                        nhanVienCanCapNhat.TinhTrang = radYes.Checked ? "Yes" : "No";

                        db.SaveChanges();
                        MessageBox.Show("Đã cập nhật thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên có mã tương ứng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maNhanVienCanTimKiem = txtTimKiem.Text.Trim();

            if (!string.IsNullOrEmpty(maNhanVienCanTimKiem))
            {
                using (var db = new QLKyTucXaEntities())
                {
                    var nhanVienCanTimKiem = db.NHANVIENs.FirstOrDefault(nv => nv.MaNhanVien == maNhanVienCanTimKiem);

                    if (nhanVienCanTimKiem != null)
                    {
                        txtMaNV.Text = nhanVienCanTimKiem.MaNhanVien;
                        txtTenNV.Text = nhanVienCanTimKiem.TenNhanVien;
                        txtSDT.Text = nhanVienCanTimKiem.SDT.ToString(); 
                        txtEmail.Text = nhanVienCanTimKiem.Email;
                        txtDiaChi.Text = nhanVienCanTimKiem.DiaChi;

                        var chucVu = db.NHANVIENs.Where(nv => nv.MaNhanVien == maNhanVienCanTimKiem)
                                                    .Select(nv => nv.ChucVu)
                                                    .FirstOrDefault();
                        if (chucVu != null)
                        {
                            cbChucVu.SelectedItem = chucVu;
                        }

                        if (nhanVienCanTimKiem.TinhTrang == "Yes")
                        {
                            radYes.Checked = true;
                        }
                        else
                        {
                            radNo.Checked = true;
                        }

                        MessageBox.Show("Đã tìm thấy nhân viên có mã "+ txtMaNV.Text +"!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên có mã tương ứng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên cần tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvDSNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) 
            {
                DataGridViewRow selectedRow = dgvDSNhanVien.Rows[e.RowIndex];

                txtMaNV.Text = selectedRow.Cells["MaNhanVien"].Value.ToString();
                txtTenNV.Text = selectedRow.Cells["TenNhanVien"].Value.ToString();
                txtSDT.Text = selectedRow.Cells["SDT"].Value.ToString();
                txtEmail.Text = selectedRow.Cells["Email"].Value.ToString();
                txtDiaChi.Text = selectedRow.Cells["DiaChi"].Value.ToString();

                string chucVu = selectedRow.Cells["ChucVu"].Value.ToString();
                if (cbChucVu.Items.Contains(chucVu))
                {
                    cbChucVu.SelectedItem = chucVu;
                }
                
                string tinhTrang = selectedRow.Cells["TinhTrang"].Value.ToString();
                if (tinhTrang == "Yes")
                {
                    radYes.Checked = true;
                }
                else
                {
                    radNo.Checked = true;
                }
            }
        }

        private void cbChucVu_DropDown(object sender, EventArgs e)
        {
            cbChucVu.Items.Remove("-Chọn chức vụ-");
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Save Excel File";
            saveFileDialog.FileName = "DanhSachNhanVien"; 

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    exportExcel(dgvDSNhanVien, filePath); // Xuất dữ liệu từ DataGridView dgvDSPhong vào tập tin Excel tại đường dẫn đã chọn
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

            // Xác định số hàng và số cột có dữ liệu
            int rowCount = worksheet.UsedRange.Rows.Count;
            int columnCount = worksheet.UsedRange.Columns.Count;

            // Xóa dữ liệu hiện tại trong DataGridView
            dgvDSNhanVien.DataSource = null;

            dgvDSNhanVien.Columns.Add("MaNhanVien", "Mã NV");
            dgvDSNhanVien.Columns.Add("TenNhanVien", "Tên nhân viên");
            dgvDSNhanVien.Columns.Add("Email", "Email");
            dgvDSNhanVien.Columns.Add("DiaChi", "Địa chỉ");
            dgvDSNhanVien.Columns.Add("SDT", "SĐT");
            dgvDSNhanVien.Columns.Add("ChucVu", "Chức vụ");
            dgvDSNhanVien.Columns.Add("TinhTrang", "Tình trạng");

            

            // Đổ dữ liệu từ Excel vào DataGridView
            for (int i = 2; i <= rowCount; i++)
            {
                dgvDSNhanVien.Rows.Add(worksheet.Cells[i, 1].Value, 
                                 worksheet.Cells[i, 2].Value, 
                                 worksheet.Cells[i, 3].Value, 
                                 worksheet.Cells[i, 4].Value, 
                                 worksheet.Cells[i, 5].Value,
                                 worksheet.Cells[i, 6].Value,
                                 worksheet.Cells[i, 7].Value);


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

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {
            
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLamMoi.Enabled = true;
                
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();

            using (var db = new QLKyTucXaEntities())
            {
                var nhanVienList = db.NHANVIENs.OrderBy(nv => nv.MaNhanVien).ToList();

                dataTable.Columns.Add("MaNhanVien", typeof(string));
                
                dataTable.Columns.Add("TenNhanVien", typeof(string));
                dataTable.Columns.Add("SDT", typeof(string));
                dataTable.Columns.Add("Email", typeof(string));
                dataTable.Columns.Add("DiaChi", typeof(string));
                dataTable.Columns.Add("ChucVu", typeof(string));
                dataTable.Columns.Add("TinhTrang", typeof(string));

                foreach (var nhanVien in nhanVienList)
                {
                    dataTable.Rows.Add(nhanVien.MaNhanVien, nhanVien.TenNhanVien, nhanVien.SDT, nhanVien.Email, nhanVien.DiaChi, nhanVien.ChucVu, nhanVien.TinhTrang);
                }
            }

            ReportDocument reportDocument = new ReportDocument();

            string reportPath = "D:\\Năm 3 C# LT Quản Lý\\QuanLyKyTucXa\\QuanLyKyTucXa\\Report\\rpt_NhanVien.rpt";

            reportDocument.Load(reportPath);

            reportDocument.SetDataSource(dataTable);

            frmReport r = new frmReport();

            r.crystalReportViewer1.ReportSource = reportDocument;

            r.ShowDialog();
        }
    }
}
