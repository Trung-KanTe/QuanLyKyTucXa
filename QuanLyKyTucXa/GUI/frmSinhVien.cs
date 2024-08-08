using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace QuanLyKyTucXa
{
    public partial class frmSinhVien : Form
    {
        public frmSinhVien()
        {
            InitializeComponent();
        }

        public string TenNha;
        public string u;
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            frmNhaSinhVien nsv = new frmNhaSinhVien();
            nsv.Show();
        }

        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            this.Location = new Point(488, 165);
            txtMaSV.Focus();
            txtTenNha.Text = TenNha;
            LoadMaPhong();
            LoadData();
            DieuChinhCot();
            cbLoc.Items.Insert(0, "--Chọn--");
            cbLoc.SelectedIndex = 0;
            if (u == "User")
            {
                btnThem.Visible = false;
                btnSua.Visible = false;
                btnXoa.Visible = false;
                btnLamMoi.Visible = false;
                btnImport.Visible = false;
                btnXuatExcel.Visible = false;
                btnIn.Visible = false;
            }    
        }

        private void LoadMaPhong()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var maPhongList = db.PHONGs
                                    .Where(p => p.TenNha == TenNha && p.TrangThai == "yes")
                                    .Where(p => p.SINHVIENs.Count < 4)
                                    .Select(p => p.MaPhong)
                                    .ToList();

                cbMaPhong.DataSource = maPhongList;
            }
        }

        private void DieuChinhCot()
        {
            dgvDSSinhVien.Columns["MaSinhVien"].Width = 70;
            dgvDSSinhVien.Columns["Ten"].Width = 110;
            dgvDSSinhVien.Columns["Email"].Width = 110;
            dgvDSSinhVien.Columns["DiaChi"].Width = 80;
            dgvDSSinhVien.Columns["GioiTinh"].Width = 70;
            dgvDSSinhVien.Columns["Nha"].Width = 80;
            dgvDSSinhVien.Columns["MaPhong"].Width = 80;
            dgvDSSinhVien.Columns["TinhTrangO"].Width = 80;

            dgvDSSinhVien.Columns["MaSinhVien"].HeaderText = "Mã SV";
            dgvDSSinhVien.Columns["Ten"].HeaderText = "Tên sinh viên";
            dgvDSSinhVien.Columns["Email"].HeaderText = "Email";
            dgvDSSinhVien.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvDSSinhVien.Columns["GioiTinh"].HeaderText = "Giới tính";
            dgvDSSinhVien.Columns["Nha"].HeaderText = "Dãy nhà";
            dgvDSSinhVien.Columns["MaPhong"].HeaderText = "Mã phòng";
            dgvDSSinhVien.Columns["TinhTrangO"].HeaderText = "Tình trạng ở";

            dgvDSSinhVien.Columns["DIENs"].Visible = false;
            dgvDSSinhVien.Columns["TAIKHOANs"].Visible = false;
            dgvDSSinhVien.Columns["PHONG"].Visible = false;
            dgvDSSinhVien.Columns["NUOCs"].Visible = false;
            dgvDSSinhVien.Columns["THANHTOANs"].Visible = false;
        }

        private void LoadData()
        {
            using (var db = new QLKyTucXaEntities())
            {
                var ListSinhVien = db.SINHVIENs.Include("PHONG").Include("DIENs").Include("NUOCs").Include("THANHTOANs").Include("TAIKHOANs").Where(p => p.Nha == TenNha).ToList();
                dgvDSSinhVien.DataSource = ListSinhVien;
                dgvDSSinhVien.Refresh();
            }
        }

        private bool KTMaPhong(string maPhong)
        {
            Regex regex;

            if (TenNha == "Châu Đốc")
            {
                regex = radNam.Checked ? new Regex(@"^CD\d{2}Nam$") : new Regex(@"^CD\d{2}Nu$");
            }
            else if (TenNha == "Tịnh Biên")
            {
                regex = radNam.Checked ? new Regex(@"^TB\d{2}Nam$") : new Regex(@"^TB\d{2}Nu$");
            }
            else if (TenNha == "Tri Tôn")
            {
                regex = radNam.Checked ? new Regex(@"^TT\d{2}Nam$") : new Regex(@"^TT\d{2}Nu$");
            }
            else if (TenNha == "An Phú")
            {
                regex = radNam.Checked ? new Regex(@"^AP\d{2}Nam$") : new Regex(@"^AP\d{2}Nu$");
            }
            else if (TenNha == "Tân Châu")
            {
                regex = radNam.Checked ? new Regex(@"^TC\d{2}Nam$") : new Regex(@"^TC\d{2}Nu$");
            }
            else if (TenNha == "Thoại Sơn")
            {
                regex = radNam.Checked ? new Regex(@"^TS\d{2}Nam$") : new Regex(@"^TS\d{2}Nu$");
            }
            else if (TenNha == "Chợ Mới")
            {
                regex = radNam.Checked ? new Regex(@"^CM\d{2}Nam$") : new Regex(@"^CM\d{2}Nu$");
            }
            else
            {
                regex = radNam.Checked ? new Regex(@"^CT\d{2}Nam$") : new Regex(@"^CT\d{2}Nu$");
            }

            return regex.IsMatch(maPhong);
        }


        private void btnThem_Click(object sender, EventArgs e)
        {      
            using (var db = new QLKyTucXaEntities())
            {
                string maSinhVien = txtMaSV.Text;

                var sinhVienTonTai = db.SINHVIENs.Any(sv => sv.MaSinhVien == maSinhVien);
                if (sinhVienTonTai)
                {
                    MessageBox.Show("Sinh viên đã tồn tại trong cơ sở dữ liệu, không thể thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Clear();
                    txtMaSV.Focus();
                    return;
                }

                string maPhong = cbMaPhong.SelectedItem.ToString();

                var soLuongSinhVienTrongPhong = db.SINHVIENs.Count(sv => sv.MaPhong == maPhong);

                if (soLuongSinhVienTrongPhong >= 4)
                {
                    MessageBox.Show("Phòng đã đầy, không thể thêm sinh viên mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!KTMaPhong(maPhong))
                {
                    MessageBox.Show("Giới tính không phù hợp với mã phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!KTEmail(txtEmail.Text))
                {
                    MessageBox.Show("Địa chỉ email không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Clear();
                    txtEmail.Focus();
                    return;
                }

                if (radNo.Checked)
                {
                    MessageBox.Show("Tình trạng ở phải là Yes!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }    

                SINHVIEN sinhVien = new SINHVIEN
                {
                    MaSinhVien = txtMaSV.Text,
                    Ten = txtTenSV.Text,
                    Email = txtEmail.Text,
                    DiaChi = txtDiaChi.Text,
                    GioiTinh = radNam.Checked ? "Nam" : "Nữ",
                    Nha = TenNha,
                    MaPhong = cbMaPhong.SelectedItem.ToString(),
                    TinhTrangO = radYes.Checked ? "Yes" : "No"
                };

                db.SINHVIENs.Add(sinhVien);
                db.SaveChanges();

                var phongUpdate = db.PHONGs.FirstOrDefault(p => p.MaPhong == maPhong);
                if (phongUpdate != null)
                {
                    phongUpdate.TinhTrang = "Có người";
                    db.SaveChanges();
                }

                MessageBox.Show("Đã thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                LoadData(); 
            }
        }

        private bool KTEmail(string email)
        {
            // Biểu thức chính quy để kiểm tra định dạng email
            string n = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Kiểm tra email có khớp với biểu thức chính quy không
            return Regex.IsMatch(email, n);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDSSinhVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để xóa!");
                return;
            }

            string maSinhVien = txtMaSV.Text;

            // Xác nhận với người dùng trước khi xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này không? \nKhi xóa xong thì sẽ không thể khôi phục được dữ liệu của sinh viên này !!", "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Xóa dữ liệu liên quan
                using (var db = new QLKyTucXaEntities())
                {
                    // Xóa dữ liệu từ bảng THANHTOAN
                    var thanhToan = db.THANHTOANs.Where(t => t.MaSinhVien == maSinhVien).ToList();
                    db.THANHTOANs.RemoveRange(thanhToan);

                    // Xóa dữ liệu từ bảng DIEN
                    var dien = db.DIENs.Where(d => d.MaSinhVien == maSinhVien).ToList();
                    db.DIENs.RemoveRange(dien);

                    // Xóa dữ liệu từ bảng NUOC
                    var nuoc = db.NUOCs.Where(n => n.MaSinhVien == maSinhVien).ToList();
                    db.NUOCs.RemoveRange(nuoc);

                    var taikhoan = db.TAIKHOANs.Where(n => n.MaSinhVien == maSinhVien).ToList();
                    db.TAIKHOANs.RemoveRange(taikhoan);

                    // Lưu thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();
                }

                // Xóa sinh viên
                using (var db = new QLKyTucXaEntities())
                {
                    var sinhVien = db.SINHVIENs.FirstOrDefault(s => s.MaSinhVien == maSinhVien);
                    if (sinhVien != null)
                    {
                        // Lấy phòng chứa sinh viên này
                        var phong = sinhVien.PHONG;

                        if (phong != null)
                        {
                            // Xóa sinh viên khỏi phòng
                            db.SINHVIENs.Remove(sinhVien);
                            db.SaveChanges();
                            MessageBox.Show("Xóa sinh viên thành công!");

                            // Kiểm tra nếu không còn sinh viên nào trong phòng
                            if (phong.SINHVIENs.Count == 0)
                            {
                                phong.TinhTrang = "Trống";
                                db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy phòng chứa sinh viên!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sinh viên cần xóa!");
                    }
                }
                Clear();
                LoadData();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDSSinhVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để sửa!");
                return;
            }

            // Lấy Mã Sinh Viên của sinh viên được chọn trong DataGridView
            string maSinhVien = txtMaSV.Text;

            using (var db = new QLKyTucXaEntities())
            {
                // Lấy thông tin sinh viên từ cơ sở dữ liệu
                var sinhVien = db.SINHVIENs.FirstOrDefault(s => s.MaSinhVien == maSinhVien);
                if (sinhVien != null)
                {
                    string MaPhong = cbMaPhong.SelectedItem.ToString();

                    if (!KTMaPhong(MaPhong))
                    {
                        MessageBox.Show("Giới tính không phù hợp với mã phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!KTEmail(txtEmail.Text))
                    {
                        MessageBox.Show("Địa chỉ email không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    sinhVien.Ten = txtTenSV.Text;
                    sinhVien.Email = txtEmail.Text;
                    sinhVien.DiaChi = txtDiaChi.Text;
                    sinhVien.GioiTinh = radNam.Checked ? "Nam" : "Nữ";
                    sinhVien.Nha = TenNha;
                    sinhVien.MaPhong = cbMaPhong.SelectedItem.ToString();
                    sinhVien.TinhTrangO = radYes.Checked ? "Yes" : "No";

                    // Lưu các thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();
                    MessageBox.Show("Cập nhật thông tin sinh viên thành công!");

                    // Kiểm tra xem tất cả sinh viên trong phòng có TinhTrangO đều là "No" không
                    var maPhong = sinhVien.MaPhong;
                    var sinhVienTrongPhong = db.SINHVIENs.Where(s => s.MaPhong == maPhong).ToList();
                    var tatCaSinhVienNo = sinhVienTrongPhong.All(s => s.TinhTrangO == "No");

                    if (tatCaSinhVienNo)
                    {
                        // Nếu tất cả sinh viên trong phòng đều có TinhTrangO là "No", cập nhật TinhTrang của phòng thành "Trống"
                        var phong = db.PHONGs.FirstOrDefault(p => p.MaPhong == maPhong);
                        if (phong != null)
                        {
                            phong.TinhTrang = "Trống";
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var phong = db.PHONGs.FirstOrDefault(p => p.MaPhong == maPhong);
                        if (phong != null)
                        {
                            phong.TinhTrang = "Có người";
                            db.SaveChanges();
                        }
                    }

                    LoadData(); // Gọi lại phương thức LoadData hoặc cập nhật DataGridView theo cách khác nếu cần
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên cần sửa!");
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maSinhVienCanTim = txtTimKiem.Text.Trim();

            if (!string.IsNullOrEmpty(maSinhVienCanTim))
            {
                using (var db = new QLKyTucXaEntities())
                {
                    var sinhVien = db.SINHVIENs.FirstOrDefault(s => s.MaSinhVien == maSinhVienCanTim);

                    if (sinhVien != null)
                    {
                        txtMaSV.Text = sinhVien.MaSinhVien;
                        txtTenSV.Text = sinhVien.Ten;
                        txtEmail.Text = sinhVien.Email;
                        txtDiaChi.Text = sinhVien.DiaChi;
                        cbMaPhong.SelectedItem = sinhVien.MaPhong;

                        if (sinhVien.GioiTinh == "Nam")
                            radNam.Checked = true;
                        else
                            radNu.Checked = true;

                        if (sinhVien.TinhTrangO == "Yes")
                            radYes.Checked = true;
                        else
                            radNo.Checked = true;

                        MessageBox.Show($"Đã tìm thấy sinh viên có mã {maSinhVienCanTim}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Không tìm thấy sinh viên có mã {maSinhVienCanTim}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên cần tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            txtTimKiem.Clear();
        }

        private void Clear()
        {
            txtMaSV.Clear();
            txtTenSV.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();

            cbMaPhong.SelectedIndex = -1;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            Clear();
            MessageBox.Show("Đã làm mới thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDSTong_Click(object sender, EventArgs e)
        {
            using (var db = new QLKyTucXaEntities())
            {
                var listSinhVien = db.SINHVIENs.Include("TAIKHOANs").Include("DIENs").Include("NUOCs").Include("THANHTOANs").ToList();

                dgvDSSinhVien.DataSource = listSinhVien;
                txtTenNha.Clear();
                txtTenNha.Enabled = true;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            btnTimKiem.Enabled = true;
        }

        private void txtMaSV_TextChanged(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLamMoi.Enabled = true;
        }

        private void dgvDSSinhVien_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDSSinhVien.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvDSSinhVien.SelectedRows[0];

                txtMaSV.Text = selectedRow.Cells["MaSinhVien"].Value.ToString();
                txtTenSV.Text = selectedRow.Cells["Ten"].Value.ToString();
                txtEmail.Text = selectedRow.Cells["Email"].Value.ToString();
                txtDiaChi.Text = selectedRow.Cells["DiaChi"].Value.ToString();
                cbMaPhong.SelectedItem = selectedRow.Cells["MaPhong"].Value.ToString();

                string gioiTinh = selectedRow.Cells["GioiTinh"].Value.ToString();
                radNam.Checked = gioiTinh == "Nam";
                radNu.Checked = gioiTinh == "Nữ";

                string tinhTrangO = selectedRow.Cells["TinhTrangO"].Value.ToString();
                radYes.Checked = tinhTrangO == "Yes";
                radNo.Checked = tinhTrangO == "No";
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (cbLoc.SelectedItem != null)
            {
                // Lấy giới tính được chọn từ combobox
                string gioiTinhLoc = cbLoc.SelectedItem.ToString();

                using (var db = new QLKyTucXaEntities())
                {
                    if (gioiTinhLoc == "Tổng hợp")
                    {
                        var listSinhVienLoc = db.SINHVIENs.Include("TAIKHOANs").Include("DIENs").Include("NUOCs").Include("THANHTOANs")
                                                   .Where(s => s.Nha == TenNha)
                                                    .ToList();
                        dgvDSSinhVien.DataSource = listSinhVienLoc;
                    }
                    else
                    {
                        var listSinhVienLoc = db.SINHVIENs.Include("TAIKHOANs").Include("DIENs").Include("NUOCs").Include("THANHTOANs")
                                                   .Where(s => s.GioiTinh == gioiTinhLoc && s.Nha == TenNha)
                                                   .ToList();
                        dgvDSSinhVien.DataSource = listSinhVienLoc;
                    }
                   
                }
            }
            else
            {
                // Nếu combobox chưa được chọn, thông báo cho người dùng
                MessageBox.Show("Vui lòng chọn giới tính cần lọc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Save Excel File";
            saveFileDialog.FileName = "DanhSachSinhVien"; // Tên mặc định cho tập tin Excel

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    exportExcel(dgvDSSinhVien, filePath); 
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
            dgvDSSinhVien.DataSource = null;

            dgvDSSinhVien.Columns.Add("MaSinhVien", "Mã SV");
            dgvDSSinhVien.Columns.Add("Ten", "Tên sinh viên");
            dgvDSSinhVien.Columns.Add("Email", "Email");
            dgvDSSinhVien.Columns.Add("DiaChi", "Địa chỉ");
            dgvDSSinhVien.Columns.Add("GioiTinh", "Giới tính");
            dgvDSSinhVien.Columns.Add("Nha", "Dãy nhà");
            dgvDSSinhVien.Columns.Add("MaPhong", "Mã phòng");
            dgvDSSinhVien.Columns.Add("TinhTrangO", "Tình trạng ở");

           

            // Đổ dữ liệu từ Excel vào DataGridView
            for (int i = 2; i <= rowCount; i++)
            {
                dgvDSSinhVien.Rows.Add(worksheet.Cells[i, 1].Value, // ID
                                 worksheet.Cells[i, 2].Value, // Chỉ số đầu
                                 worksheet.Cells[i, 3].Value, // Chỉ số cuối
                                 worksheet.Cells[i, 4].Value, // Giá
                                 worksheet.Cells[i, 5].Value,
                                 worksheet.Cells[i, 6].Value,
                                 worksheet.Cells[i, 7].Value,
                                 worksheet.Cells[i, 8].Value); // Mã sinh viên
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

        private void cbLoc_DropDown(object sender, EventArgs e)
        {
            cbLoc.Items.Remove("--Chọn--");
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();

            using (var db = new QLKyTucXaEntities())
            {
                var svList = db.SINHVIENs.Where(p => p.Nha == TenNha).OrderBy(p => p.TinhTrangO).ToList();

                dataTable.Columns.Add("MaSinhVien", typeof(string));
                dataTable.Columns.Add("Ten", typeof(string));
                dataTable.Columns.Add("Email", typeof(string));
                dataTable.Columns.Add("DiaChi", typeof(string));
                dataTable.Columns.Add("GioiTinh", typeof(string));
                dataTable.Columns.Add("Nha", typeof(string));
                dataTable.Columns.Add("MaPhong", typeof(string));
                dataTable.Columns.Add("TinhTrangO",typeof(string)); 

                foreach (var phong in svList)
                {
                    dataTable.Rows.Add(phong.MaPhong, phong.Ten, phong.Email, phong.DiaChi, phong.GioiTinh, phong.Nha, phong.MaPhong, phong.TinhTrangO);
                }
            }

            ReportDocument reportDocument = new ReportDocument();

            string reportPath = "D:\\Năm 3 C# LT Quản Lý\\QuanLyKyTucXa\\QuanLyKyTucXa\\Report\\rpt_SinhVien.rpt";

            reportDocument.Load(reportPath);

            reportDocument.SetDataSource(dataTable);

            frmReport r = new frmReport();

            r.crystalReportViewer1.ReportSource = reportDocument;

            r.ShowDialog();
        }

        private void btnImport_Click_1(object sender, EventArgs e)
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
    }
}

