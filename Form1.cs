
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace LabNhanVien
{
    public class NhanVien
    {
        public string MSNV { get; set; }
        public string TenNV { get; set; }
        public decimal LuongCB { get; set; }
    }

    public class Form1 : Form
    {
        private DataGridView dgvNhanVien;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnDong;

        private BindingList<NhanVien> danhSach =
            new BindingList<NhanVien>();

        public Form1()
        {
            InitializeForm();
            LoadData();
        }

        private void InitializeForm()
        {
            Text = "List View";
            Width = 570;
            Height = 370;
            StartPosition = FormStartPosition.CenterScreen;

            dgvNhanVien = new DataGridView();
            dgvNhanVien.Location = new System.Drawing.Point(15, 20);
            dgvNhanVien.Size = new System.Drawing.Size(350, 260);
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.ReadOnly = true;
            dgvNhanVien.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvNhanVien.MultiSelect = false;
            dgvNhanVien.AutoGenerateColumns = false;
            dgvNhanVien.RowHeadersVisible = false;

            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "MSNV",
                DataPropertyName = "MSNV",
                Width = 75
            });

            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Tên NV",
                DataPropertyName = "TenNV",
                Width = 150
            });

            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Lương CB",
                DataPropertyName = "LuongCB",
                Width = 110,
                DefaultCellStyle =
                {
                    Format = "N0"
                }
            });

            btnThem = new Button();
            btnThem.Text = "Thêm";
            btnThem.SetBounds(390, 30, 120, 30);
            btnThem.Click += BtnThem_Click;

            btnSua = new Button();
            btnSua.Text = "Sửa";
            btnSua.SetBounds(390, 70, 120, 30);
            btnSua.Click += BtnSua_Click;

            btnXoa = new Button();
            btnXoa.Text = "Xóa";
            btnXoa.SetBounds(390, 110, 120, 30);
            btnXoa.Click += BtnXoa_Click;

            btnDong = new Button();
            btnDong.Text = "Đóng";
            btnDong.SetBounds(390, 150, 120, 30);
            btnDong.Click += (s, e) => Close();

            Controls.Add(dgvNhanVien);
            Controls.Add(btnThem);
            Controls.Add(btnSua);
            Controls.Add(btnXoa);
            Controls.Add(btnDong);
        }

        private void LoadData()
        {
            danhSach.Add(new NhanVien
            {
                MSNV = "NV001",
                TenNV = "Nguyễn Thị Thu Hiền",
                LuongCB = 8500000
            });

            dgvNhanVien.DataSource = danhSach;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            using (FormNhanVien form = new FormNhanVien())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (TimNhanVien(form.NhanVien.MSNV) != null)
                    {
                        MessageBox.Show("Mã nhân viên đã tồn tại!");
                        return;
                    }

                    danhSach.Add(form.NhanVien);
                }
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            NhanVien nv = GetSelectedNhanVien();

            if (nv == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!");
                return;
            }

            using (FormNhanVien form = new FormNhanVien(nv))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    NhanVien trung = TimNhanVien(form.NhanVien.MSNV);

                    if (trung != null && trung != nv)
                    {
                        MessageBox.Show("Mã nhân viên đã tồn tại!");
                        return;
                    }

                    nv.MSNV = form.NhanVien.MSNV;
                    nv.TenNV = form.NhanVien.TenNV;
                    nv.LuongCB = form.NhanVien.LuongCB;

                    dgvNhanVien.Refresh();
                }
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            NhanVien nv = GetSelectedNhanVien();

            if (nv == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa nhân viên này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                danhSach.Remove(nv);
            }
        }

        private NhanVien GetSelectedNhanVien()
        {
            if (dgvNhanVien.CurrentRow == null)
                return null;

            return dgvNhanVien.CurrentRow.DataBoundItem as NhanVien;
        }

        private NhanVien TimNhanVien(string ma)
        {
            foreach (NhanVien nv in danhSach)
            {
                if (string.Equals(
                    nv.MSNV, ma,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return nv;
                }
            }

            return null;
        }
    }
}
