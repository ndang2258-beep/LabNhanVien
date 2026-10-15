
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LabNhanVien
{
    public class FormNhanVien : Form
    {
        private TextBox txtMSNV;
        private TextBox txtTenNV;
        private TextBox txtLuongCB;

        private Button btnDongY;
        private Button btnBoQua;

        public NhanVien NhanVien { get; private set; }

        public FormNhanVien()
        {
            InitializeForm();
            Text = "Nhân viên";
        }

        public FormNhanVien(NhanVien nv)
        {
            InitializeForm();

            Text = "Nhân viên";

            txtMSNV.Text = nv.MSNV;
            txtTenNV.Text = nv.TenNV;
            txtLuongCB.Text = nv.LuongCB.ToString("0.##");
        }

        private void InitializeForm()
        {
            Width = 380;
            Height = 220;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            Label lblMSNV = new Label();
            lblMSNV.Text = "MSNV:";
            lblMSNV.SetBounds(20, 20, 100, 25);

            txtMSNV = new TextBox();
            txtMSNV.SetBounds(125, 20, 210, 25);

            Label lblTenNV = new Label();
            lblTenNV.Text = "Tên nhân viên:";
            lblTenNV.SetBounds(20, 55, 100, 25);

            txtTenNV = new TextBox();
            txtTenNV.SetBounds(125, 55, 210, 25);

            Label lblLuongCB = new Label();
            lblLuongCB.Text = "Lương căn bản:";
            lblLuongCB.SetBounds(20, 90, 100, 25);

            txtLuongCB = new TextBox();
            txtLuongCB.SetBounds(125, 90, 210, 25);

            btnDongY = new Button();
            btnDongY.Text = "Đồng ý";
            btnDongY.SetBounds(125, 130, 90, 30);
            btnDongY.Click += BtnDongY_Click;

            btnBoQua = new Button();
            btnBoQua.Text = "Bỏ qua";
            btnBoQua.SetBounds(235, 130, 90, 30);
            btnBoQua.DialogResult = DialogResult.Cancel;

            Controls.Add(lblMSNV);
            Controls.Add(txtMSNV);
            Controls.Add(lblTenNV);
            Controls.Add(txtTenNV);
            Controls.Add(lblLuongCB);
            Controls.Add(txtLuongCB);
            Controls.Add(btnDongY);
            Controls.Add(btnBoQua);

            AcceptButton = btnDongY;
            CancelButton = btnBoQua;
        }

        private void BtnDongY_Click(object sender, EventArgs e)
        {
            string ma = txtMSNV.Text.Trim();
            string ten = txtTenNV.Text.Trim();
            string luongText = txtLuongCB.Text.Trim();

            if (ma == "" || ten == "" || luongText == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            decimal luong;

            if (!decimal.TryParse(luongText, out luong) || luong < 0)
            {
                MessageBox.Show("Lương phải là số không âm!");
                txtLuongCB.Focus();
                return;
            }

            NhanVien = new NhanVien
            {
                MSNV = ma,
                TenNV = ten,
                LuongCB = luong
            };

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
