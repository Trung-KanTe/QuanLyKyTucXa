using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;



namespace QuanLyKyTucXa
{
    public partial class frmPhong : Form
    {
        public frmPhong()
        {
            InitializeComponent();
        }

        public string TenNha;
        public string q; 

        private void frmPhong_Load(object sender, EventArgs e)
        {           
            txtMaPhong.Focus();
            this.Location = new Point(488, 165);
            
            txtTenNha.Text = TenNha;
            LoadData();
            DieuChinhCot();

            cbLoc.Items.Insert(0, "--Chọn--");
            cbLoc.SelectedIndex = 0;

            if (q == "User")
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            frmTenPhong tp = new frmTenPhong();
            tp.Show();
        }

        private void DieuChinhCot()
        {
            dgvDSPhong.Columns["MaPhong"].Width = 100;
            dgvDSPhong.Columns["TenNha"].Width = 120;
            dgvDSPhong.Columns["TinhTrang"].Width = 120;
            dgvDSPhong.Columns["TrangThai"].Width = 120;
            dgvDSPhong.Columns["DayNha"].Width = 100;

            dgvDSPhong.Columns["MaPhong"].HeaderText = "Mã phòng";
            dgvDSPhong.Columns["TenNha"].HeaderText = "Tên nhà";
            dgvDSPhong.Columns["TinhTrang"].HeaderText = "Tình trạng";
            dgvDSPhong.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvDSPhong.Columns["DayNha"].HeaderText = "Dãy nhà";

            dgvDSPhong.Columns["SINHVIENs"].Visible = false;
            dgvDSPhong.Columns["THANHTOANs"].Visible = false;
        }


        private void LoadData()
        {
            using (var db = new QLKyTucXaEntities())
            {
                if (TenNha == "Châu Đốc")
                {
                    var ListPhong = db.PHONGs.Include("SINHVIENs").Include("THANHTOANs").Where(p => p.TenNha == "Châu Đốc").ToList();
                    dgvDSPhong.DataSource = ListPhong;
                }
                else if (TenNha == "Châu Thành")
                {
                    var ListPhong = db.PHONGs.Include("SINHVIENs").Include("THANHTOANs").Where(p => p.TenNha == "Châu Thành").ToList();
                    dgvDSPhong.DataSource = ListPhong;
                }
                else if (TenNha == "Tri Tôn")
                {
                    var ListPhong = db.PHONGs.Include("SINHVIENs").Include("THANHTOANs").Where(p => p.TenNha == "Tri Tôn").ToList();
                    dgvDSPhong.DataSource = ListPhong;
                }
                else if (TenNha == "An Phú")
                {
                    var ListPhong = db.PHONGs.Include("SINHVIENs").Include("THANHTOANs").Where(p => p.TenNha == "An Phú").ToList();
                    dgvDSPhong.DataSource = ListPhong;
                }
                else if (TenNha == "Tịnh Biên")
                {
                    var ListPhong = db.PHONGs.Include("SINHVIENs").Include("THANHTOANs").Where(p => p.TenNha == "Tịnh Biên").ToList();
                    dgvDSPhong.DataSource = ListPhong;
                }
                else if (TenNha == "Tân Châu")
                {
                    var ListPhong = db.PHONGs.Include("SINHVIENs").Include("THANHTOANs").Where(p => p.TenNha == "Tân Châu").ToList();
                    dgvDSPhong.DataSource = ListPhong;
                }
                else if (TenNha == "Thoại Sơn")
                {
                    var ListPhong = db.PHONGs.Include("SINHVIENs").Include("THANHTOANs").Where(p => p.TenNha == "Thoại Sơn").ToList();
                    dgvDSPhong.DataSource = ListPhong;
                }
                else
                {
                    var ListPhong = db.PHONGs.Include("SINHVIENs").Include("THANHTOANs").Where(p => p.TenNha == "Chợ Mới").ToList();
                    dgvDSPhong.DataSource = ListPhong;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var db = new QLKyTucXaEntities())
            {
                string maPhong = txtMaPhong.Text;

                if (!KTMaPhong(maPhong))
                {
                    MessageBox.Show("Mã phòng không đúng với tên nhà hoặc dãy nhà!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var existingPhong = db.PHONGs.FirstOrDefault(p => p.MaPhong == maPhong);

                if (existingPhong != null)
                {
                    txtMaPhong.Focus();
                    MessageBox.Show($"Phòng với mã {maPhong} đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;  
                }

                string tinhTrang;
                string trangThai = radYes.Checked ? radYes.Text : radNo.Text;
                string dayNha = radNhaNam.Checked ? radNhaNam.Text : radNhaNu.Text;

                if (radCoNguoi.Checked)
                {
                    MessageBox.Show("Phòng mới thêm còn trống chưa có sinh viên ở. Vui lòng chọn lại Tình Trạng Ở = 'Trống' !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    tinhTrang = radTrong.Text;
                }

                var phongMoi = new PHONG
                {
                    MaPhong = txtMaPhong.Text,
                    TenNha = txtTenNha.Text,
                    TinhTrang = tinhTrang,
                    TrangThai = trangThai,
                    DayNha = dayNha,
                };

                db.PHONGs.Add(phongMoi);
                db.SaveChanges();
                LoadData();
                MessageBox.Show($"Đã thêm thành công phòng {phongMoi.MaPhong} vào nhà {phongMoi.TenNha} !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        
        }

        private bool KTMaPhong(string maPhong)
        {
            Regex regex;

            if (TenNha == "Châu Đốc")
            {
                regex = radNhaNam.Checked ? new Regex(@"^CD\d{2}Nam$") : new Regex(@"^CD\d{2}Nu$");
            }
            else if (TenNha == "Tịnh Biên")
            {
                regex = radNhaNam.Checked ? new Regex(@"^TB\d{2}Nam$") : new Regex(@"^TB\d{2}Nu$");
            }
            else if (TenNha == "Tri Tôn")
            {
                regex = radNhaNam.Checked ? new Regex(@"^TT\d{2}Nam$") : new Regex(@"^TT\d{2}Nu$");
            }
            else if (TenNha == "An Phú")
            {
                regex = radNhaNam.Checked ? new Regex(@"^AP\d{2}Nam$") : new Regex(@"^AP\d{2}Nu$");
            }
            else if (TenNha == "Tân Châu")
            {
                regex = radNhaNam.Checked ? new Regex(@"^TC\d{2}Nam$") : new Regex(@"^TC\d{2}Nu$");
            }
            else if (TenNha == "Thoại Sơn")
            {
                regex = radNhaNam.Checked ? new Regex(@"^TS\d{2}Nam$") : new Regex(@"^TS\d{2}Nu$");
            }
            else if (TenNha == "Chợ Mới")
            {
                regex = radNhaNam.Checked ? new Regex(@"^CM\d{2}Nam$") : new Regex(@"^CM\d{2}Nu$");
            }
            else 
            {
                regex = radNhaNam.Checked ? new Regex(@"^CT\d{2}Nam$") : new Regex(@"^CT\d{2}Nu$");
            }

            return regex.IsMatch(maPhong);
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDSPhong.SelectedRows.Count > 0)
            {
                string maPhong = txtMaPhong.Text;  
                using (var db = new QLKyTucXaEntities())
                {
                    var phong = db.PHONGs.Include("SINHVIENs").FirstOrDefault(p => p.MaPhong == maPhong);

                    if (phong != null)
                    {
                        if (phong.SINHVIENs.Any() || radCoNguoi.Checked)
                        {
                            MessageBox.Show($"Phòng có mã {maPhong} đang có sinh viên ở, không thể xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa phòng có mã {maPhong} không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                db.PHONGs.Remove(phong);
                                db.SaveChanges();

                                LoadData();

                                MessageBox.Show($"Đã xóa thành công phòng {maPhong}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Không tìm thấy phòng có mã {maPhong} trong danh sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvDSPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow selectedRow = dgvDSPhong.Rows[e.RowIndex];

                string maPhong = selectedRow.Cells["MaPhong"].Value.ToString();
                string tenNha = selectedRow.Cells["TenNha"].Value.ToString();
                string tinhTrang = selectedRow.Cells["TinhTrang"].Value.ToString();
                string trangThai = selectedRow.Cells["TrangThai"].Value.ToString();
                string dayNha = selectedRow.Cells["DayNha"].Value.ToString();

                txtMaPhong.Text = maPhong;
                txtTenNha.Text = tenNha;
                if (tinhTrang == "Có người")
                    radCoNguoi.Checked = true;
                else
                    radTrong.Checked = true;

                if (trangThai == "Yes")
                    radYes.Checked = true;
                else
                    radNo.Checked = true;

                if (dayNha == "Nam")
                    radNhaNam.Checked = true;
                else
                    radNhaNu.Checked = true;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDSPhong.SelectedRows.Count > 0)
            {
                string maPhong = txtMaPhong.Text;

                if (!KTMaPhong(maPhong))
                {
                    MessageBox.Show("Dãy nhà không phù hợp với mã phòng! Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var db = new QLKyTucXaEntities())
                {
                    var phong = db.PHONGs.FirstOrDefault(p => p.MaPhong == maPhong);

                    if (phong != null)
                    {
                        
                        if (radCoNguoi.Checked && radNo.Checked)
                        {
                            MessageBox.Show("Phòng đang có sinh viên ở, không thể chuyển sang trạng thái ngưng hoạt động. Vui lòng di chuyển các sinh viên sang phòng khác!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            phong.TinhTrang = radCoNguoi.Checked ? "Có người" : "Trống";
                            phong.TrangThai = radYes.Checked ? "Yes" : "No";
                        }                                                
                        phong.DayNha = radNhaNam.Checked ? "Nam" : "Nữ";

                        db.SaveChanges();
                        LoadData();

                        MessageBox.Show($"Đã cập nhật thông tin phòng có mã {maPhong}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Không tìm thấy phòng có mã {maPhong} trong danh sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng để sửa thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaPhong.Clear();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maPhongCanTim = txtTimKiem.Text.Trim();

            if (!string.IsNullOrEmpty(maPhongCanTim))
            {
                using (var db = new QLKyTucXaEntities())
                {
                    var phong = db.PHONGs.FirstOrDefault(p => p.MaPhong == maPhongCanTim);

                    if (phong != null)
                    {
                        txtMaPhong.Text = phong.MaPhong;
                        txtTenNha.Text = phong.TenNha;
                        if (phong.TinhTrang == "Có người")
                            radCoNguoi.Checked = true;
                        else
                            radTrong.Checked = true;

                        if (phong.TrangThai == "Yes")
                            radYes.Checked = true;
                        else
                            radNo.Checked = true;

                        if (phong.DayNha == "Nam")
                            radNhaNam.Checked = true;
                        else
                            radNhaNu.Checked = true;

                        MessageBox.Show($"Đã tìm thấy phòng có mã {maPhongCanTim}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Không tìm thấy phòng có mã {maPhongCanTim}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã phòng cần tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            txtTimKiem.Clear();
        }

        private void btnDSTong_Click(object sender, EventArgs e)
        {
            using (var db = new QLKyTucXaEntities())
            {
                var ListPhong = db.PHONGs.Include("SINHVIENs").Include("THANHTOANs").ToList();
                dgvDSPhong.DataSource = ListPhong;
                txtTenNha.Clear();
                txtTenNha.Enabled = true;
            }
        }

        private void btnDSNam_Click(object sender, EventArgs e)
        {
            using (var db = new QLKyTucXaEntities())
            {
                var phongNamList = db.PHONGs.Include("SINHVIENs").Include("THANHTOANs").Where(p => p.DayNha == "Nam").ToList();
                dgvDSPhong.DataSource = phongNamList;
            }
        }

        private void btnDSNu_Click(object sender, EventArgs e)
        {
            using (var db = new QLKyTucXaEntities())
            {
                var phongNuList = db.PHONGs.Include("SINHVIENs").Include("THANHTOANs").Where(p => p.DayNha == "Nữ").ToList();
                dgvDSPhong.DataSource = phongNuList;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            btnTimKiem.Enabled = true;
        }

        private void txtMaPhong_TextChanged(object sender, EventArgs e)
        {                 
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnLamMoi.Enabled = true;
               
        }

        private void cbLoc_DropDown(object sender, EventArgs e)
        {
            cbLoc.Items.Remove("--Chọn--");
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            string gioiTinh = cbLoc.SelectedItem.ToString();

            List<PHONG> danhSachPhong = new List<PHONG>();

            using (var db = new QLKyTucXaEntities())
            {
                if (gioiTinh == "Tổng hợp")
                {
                    danhSachPhong = db.PHONGs
                    .Where(p => p.TenNha == TenNha)
                    .ToList();
                }
                else
                {
                    danhSachPhong = db.PHONGs
                    .Where(p => p.DayNha == gioiTinh && p.TenNha == TenNha)
                    .ToList();
                }
            }
            dgvDSPhong.DataSource = danhSachPhong;
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Save Excel File";
            saveFileDialog.FileName = "DanhSachPhong";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    exportExcel(dgvDSPhong, filePath); 
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

            dgvDSPhong.DataSource = null;

            dgvDSPhong.Columns.Add("MaPhong", "Mã phòng");
            dgvDSPhong.Columns.Add("TenNha", "Tên nhà");
            dgvDSPhong.Columns.Add("TinhTrang", "Tình trạng");
            dgvDSPhong.Columns.Add("TrangThai", "Trạng thái");
            dgvDSPhong.Columns.Add("DayNha", "Dãy nhà");

            for (int i = 2; i <= rowCount; i++)
            {
                dgvDSPhong.Rows.Add(worksheet.Cells[i, 1].Value,
                                 worksheet.Cells[i, 2].Value, 
                                 worksheet.Cells[i, 3].Value,
                                 worksheet.Cells[i, 4].Value,
                                 worksheet.Cells[i, 5].Value); 
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
                var phongList = db.PHONGs.Where(p => p.TenNha == TenNha).OrderBy(p =>p.DayNha).ToList();

                dataTable.Columns.Add("MaPhong", typeof(string));
                dataTable.Columns.Add("TenNha", typeof(string));
                dataTable.Columns.Add("TinhTrang", typeof(string));
                dataTable.Columns.Add("TrangThai", typeof(string));
                dataTable.Columns.Add("DayNha", typeof(string));

                foreach (var phong in phongList)
                {
                    dataTable.Rows.Add(phong.MaPhong, phong.TenNha, phong.TinhTrang, phong.TrangThai, phong.DayNha);
                }
            }

            ReportDocument reportDocument = new ReportDocument();

            string reportPath = "D:\\Năm 3 C# LT Quản Lý\\QuanLyKyTucXa\\QuanLyKyTucXa\\Report\\rpt_Phong.rpt";

            reportDocument.Load(reportPath);

            reportDocument.SetDataSource(dataTable);

            frmReport r = new frmReport();

            r.crystalReportViewer1.ReportSource = reportDocument;

            r.ShowDialog();
        }
    }
}

