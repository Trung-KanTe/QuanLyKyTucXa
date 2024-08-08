namespace QuanLyKyTucXa
{
    partial class frmQuenMatKhau
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuenMatKhau));
            this.label10 = new System.Windows.Forms.Label();
            this.guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.radNhanVien = new Guna.UI2.WinForms.Guna2RadioButton();
            this.radSinhVien = new Guna.UI2.WinForms.Guna2RadioButton();
            this.txtMa = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtTaiKhoan = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2ImageButton3 = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnExit = new Guna.UI2.WinForms.Guna2Button();
            this.btnXacNhan = new Guna.UI2.WinForms.Guna2Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnThoat = new Guna.UI2.WinForms.Guna2Button();
            this.guna2GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label10.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(295, 744);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(568, 23);
            this.label10.TabIndex = 91;
            this.label10.Text = "© 2024  Copyrights by Trung KanTe | Liên hệ hỗ trợ: 0386880977";
            // 
            // guna2GroupBox1
            // 
            this.guna2GroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.guna2GroupBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.guna2GroupBox1.BorderThickness = 3;
            this.guna2GroupBox1.Controls.Add(this.radNhanVien);
            this.guna2GroupBox1.Controls.Add(this.radSinhVien);
            this.guna2GroupBox1.Controls.Add(this.txtMa);
            this.guna2GroupBox1.Controls.Add(this.txtTaiKhoan);
            this.guna2GroupBox1.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.guna2GroupBox1.Font = new System.Drawing.Font("Cambria", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2GroupBox1.ForeColor = System.Drawing.Color.Black;
            this.guna2GroupBox1.Location = new System.Drawing.Point(82, 281);
            this.guna2GroupBox1.Name = "guna2GroupBox1";
            this.guna2GroupBox1.Size = new System.Drawing.Size(1006, 302);
            this.guna2GroupBox1.TabIndex = 90;
            this.guna2GroupBox1.Text = "Thông tin tài khoản";
            // 
            // radNhanVien
            // 
            this.radNhanVien.AutoSize = true;
            this.radNhanVien.BackColor = System.Drawing.Color.White;
            this.radNhanVien.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radNhanVien.CheckedState.BorderThickness = 0;
            this.radNhanVien.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radNhanVien.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radNhanVien.CheckedState.InnerOffset = -4;
            this.radNhanVien.Font = new System.Drawing.Font("Cambria", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radNhanVien.Location = new System.Drawing.Point(560, 58);
            this.radNhanVien.Name = "radNhanVien";
            this.radNhanVien.Size = new System.Drawing.Size(166, 37);
            this.radNhanVien.TabIndex = 72;
            this.radNhanVien.Text = "Nhân viên";
            this.radNhanVien.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radNhanVien.UncheckedState.BorderThickness = 2;
            this.radNhanVien.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radNhanVien.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radNhanVien.UseVisualStyleBackColor = false;
            // 
            // radSinhVien
            // 
            this.radSinhVien.AutoSize = true;
            this.radSinhVien.BackColor = System.Drawing.Color.White;
            this.radSinhVien.Checked = true;
            this.radSinhVien.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radSinhVien.CheckedState.BorderThickness = 0;
            this.radSinhVien.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radSinhVien.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radSinhVien.CheckedState.InnerOffset = -4;
            this.radSinhVien.Font = new System.Drawing.Font("Cambria", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radSinhVien.Location = new System.Drawing.Point(272, 58);
            this.radSinhVien.Name = "radSinhVien";
            this.radSinhVien.Size = new System.Drawing.Size(155, 37);
            this.radSinhVien.TabIndex = 72;
            this.radSinhVien.TabStop = true;
            this.radSinhVien.Text = "Sinh viên";
            this.radSinhVien.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radSinhVien.UncheckedState.BorderThickness = 2;
            this.radSinhVien.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radSinhVien.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radSinhVien.UseVisualStyleBackColor = false;
            // 
            // txtMa
            // 
            this.txtMa.BackColor = System.Drawing.Color.White;
            this.txtMa.BorderColor = System.Drawing.Color.Black;
            this.txtMa.BorderRadius = 15;
            this.txtMa.BorderThickness = 2;
            this.txtMa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMa.DefaultText = "";
            this.txtMa.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMa.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txtMa.FocusedState.BorderColor = System.Drawing.Color.White;
            this.txtMa.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMa.ForeColor = System.Drawing.Color.Black;
            this.txtMa.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMa.Location = new System.Drawing.Point(117, 118);
            this.txtMa.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtMa.Name = "txtMa";
            this.txtMa.PasswordChar = '\0';
            this.txtMa.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtMa.PlaceholderText = "Mã số sinh viên / Mã nhân viên";
            this.txtMa.SelectedText = "";
            this.txtMa.Size = new System.Drawing.Size(784, 53);
            this.txtMa.TabIndex = 71;
            // 
            // txtTaiKhoan
            // 
            this.txtTaiKhoan.BackColor = System.Drawing.Color.White;
            this.txtTaiKhoan.BorderColor = System.Drawing.Color.Black;
            this.txtTaiKhoan.BorderRadius = 15;
            this.txtTaiKhoan.BorderThickness = 2;
            this.txtTaiKhoan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTaiKhoan.DefaultText = "";
            this.txtTaiKhoan.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTaiKhoan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTaiKhoan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTaiKhoan.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTaiKhoan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txtTaiKhoan.FocusedState.BorderColor = System.Drawing.Color.White;
            this.txtTaiKhoan.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaiKhoan.ForeColor = System.Drawing.Color.Black;
            this.txtTaiKhoan.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTaiKhoan.Location = new System.Drawing.Point(117, 203);
            this.txtTaiKhoan.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtTaiKhoan.Name = "txtTaiKhoan";
            this.txtTaiKhoan.PasswordChar = '\0';
            this.txtTaiKhoan.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtTaiKhoan.PlaceholderText = "Tên tài khoản";
            this.txtTaiKhoan.SelectedText = "";
            this.txtTaiKhoan.Size = new System.Drawing.Size(784, 53);
            this.txtTaiKhoan.TabIndex = 71;
            // 
            // guna2ImageButton3
            // 
            this.guna2ImageButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.guna2ImageButton3.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton3.HoverState.ImageSize = new System.Drawing.Size(290, 100);
            this.guna2ImageButton3.Image = ((System.Drawing.Image)(resources.GetObject("guna2ImageButton3.Image")));
            this.guna2ImageButton3.ImageOffset = new System.Drawing.Point(0, 0);
            this.guna2ImageButton3.ImageRotate = 0F;
            this.guna2ImageButton3.ImageSize = new System.Drawing.Size(300, 100);
            this.guna2ImageButton3.Location = new System.Drawing.Point(12, 12);
            this.guna2ImageButton3.Name = "guna2ImageButton3";
            this.guna2ImageButton3.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton3.Size = new System.Drawing.Size(400, 120);
            this.guna2ImageButton3.TabIndex = 89;
            // 
            // btnExit
            // 
            this.btnExit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExit.FillColor = System.Drawing.Color.Transparent;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageSize = new System.Drawing.Size(30, 30);
            this.btnExit.Location = new System.Drawing.Point(1101, -57);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(46, 46);
            this.btnExit.TabIndex = 88;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BorderColor = System.Drawing.Color.White;
            this.btnXacNhan.BorderRadius = 15;
            this.btnXacNhan.BorderThickness = 1;
            this.btnXacNhan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXacNhan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXacNhan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXacNhan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXacNhan.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnXacNhan.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhan.ForeColor = System.Drawing.Color.Black;
            this.btnXacNhan.HoverState.BorderColor = System.Drawing.Color.Cyan;
            this.btnXacNhan.HoverState.FillColor = System.Drawing.Color.Blue;
            this.btnXacNhan.Location = new System.Drawing.Point(485, 632);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(190, 65);
            this.btnXacNhan.TabIndex = 87;
            this.btnXacNhan.Text = "Xác Nhận";
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(424, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(334, 47);
            this.label2.TabIndex = 84;
            this.label2.Text = "QUÊN MẬT KHẨU";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(494, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(446, 35);
            this.label5.TabIndex = 85;
            this.label5.Text = "TRƯỜNG ĐẠI HỌC AN GIANG";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(458, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(506, 35);
            this.label1.TabIndex = 86;
            this.label1.Text = "HỆ THỐNG QUẢN LÝ KÝ TÚC XÁ ";
            // 
            // btnThoat
            // 
            this.btnThoat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThoat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThoat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThoat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThoat.FillColor = System.Drawing.Color.Transparent;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.ImageSize = new System.Drawing.Size(30, 30);
            this.btnThoat.Location = new System.Drawing.Point(1101, 2);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(46, 46);
            this.btnThoat.TabIndex = 92;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // frmQuenMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1149, 788);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.guna2GroupBox1);
            this.Controls.Add(this.guna2ImageButton3);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmQuenMatKhau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmQuenMatKhau";
            this.Load += new System.EventHandler(this.frmQuenMatKhau_Load);
            this.guna2GroupBox1.ResumeLayout(false);
            this.guna2GroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox1;
        private Guna.UI2.WinForms.Guna2RadioButton radNhanVien;
        private Guna.UI2.WinForms.Guna2RadioButton radSinhVien;
        private Guna.UI2.WinForms.Guna2TextBox txtMa;
        private Guna.UI2.WinForms.Guna2TextBox txtTaiKhoan;
        private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton3;
        private Guna.UI2.WinForms.Guna2Button btnExit;
        private Guna.UI2.WinForms.Guna2Button btnXacNhan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button btnThoat;
    }
}