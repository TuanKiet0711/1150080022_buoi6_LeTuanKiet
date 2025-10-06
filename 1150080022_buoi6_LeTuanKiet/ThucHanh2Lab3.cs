using System;
using System.Drawing;
using System.Windows.Forms;

namespace _1150080022_buoi6_LeTuanKiet
{
    public partial class Form2 : Form
    {
        TextBox txtHoTen, txtLop, txtDiaChi;
        DateTimePicker dtpNgaySinh;
        Button btnThem, btnSua, btnXoa, btnThoat;
        ListView lvSV;

        public Form2()
        {
            InitializeComponent();
            TaoGiaoDien();
        }

        private void TaoGiaoDien()
        {
            Text = "Danh sách sinh viên";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(780, 480);
            Font = new Font("Segoe UI", 9F);

            // Title
            var lblTitle = new Label
            {
                Text = "DANH MỤC SINH VIÊN",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.RoyalBlue,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 10),
                Size = new Size(760, 50)
            };
            Controls.Add(lblTitle);

            // Group thông tin
            var grpInfo = new GroupBox
            {
                Text = "Thông tin sinh viên:",
                Location = new Point(10, 70),
                Size = new Size(760, 140)
            };
            Controls.Add(grpInfo);

            int left1 = 20, left2 = 400, top = 25, h = 26, w1 = 280, w2 = 320;

            grpInfo.Controls.Add(new Label { Text = "Họ tên:", Location = new Point(left1, top + 4), AutoSize = true });
            txtHoTen = new TextBox { Location = new Point(left1 + 70, top), Size = new Size(w1, h) };
            grpInfo.Controls.Add(txtHoTen);

            grpInfo.Controls.Add(new Label { Text = "Lớp:", Location = new Point(left2, top + 4), AutoSize = true });
            txtLop = new TextBox { Location = new Point(left2 + 50, top), Size = new Size(w2 - 60, h) };
            grpInfo.Controls.Add(txtLop);

            grpInfo.Controls.Add(new Label { Text = "Ngày sinh:", Location = new Point(left1, top + 44), AutoSize = true });
            dtpNgaySinh = new DateTimePicker
            {
                Location = new Point(left1 + 90, top + 40),
                Size = new Size(w1 - 20, h),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy"
            };
            grpInfo.Controls.Add(dtpNgaySinh);

            grpInfo.Controls.Add(new Label { Text = "Địa chỉ:", Location = new Point(left2, top + 44), AutoSize = true });
            txtDiaChi = new TextBox { Location = new Point(left2 + 70, top + 40), Size = new Size(w2 - 80, h) };

            grpInfo.Controls.Add(txtDiaChi);

            // Chức năng
            var grpActions = new GroupBox
            {
                Text = "Chức năng:",
                Location = new Point(10, 215),
                Size = new Size(760, 65)
            };
            Controls.Add(grpActions);

            btnThem = new Button { Text = "Thêm", Location = new Point(40, 25), Size = new Size(90, 28) };
            btnSua = new Button { Text = "Sửa", Location = new Point(160, 25), Size = new Size(90, 28) };
            btnXoa = new Button { Text = "Xóa", Location = new Point(280, 25), Size = new Size(90, 28) };
            btnThoat = new Button { Text = "Thoát", Location = new Point(670, 25), Size = new Size(70, 28) };

            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnThoat.Click += (s, e) => Close();

            grpActions.Controls.AddRange(new Control[] { btnThem, btnSua, btnXoa, btnThoat });

            // Listview
            var grpList = new GroupBox
            {
                Text = "Thông tin chung sinh viên:",
                Location = new Point(10, 285),
                Size = new Size(760, 180)
            };
            Controls.Add(grpList);

            lvSV = new ListView
            {
                Location = new Point(10, 22),
                Size = new Size(740, 145),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true
            };
            lvSV.Columns.Add("Họ tên", 240);
            lvSV.Columns.Add("Ngày sinh", 130);
            lvSV.Columns.Add("Lớp", 120);
            lvSV.Columns.Add("Địa chỉ", 230);
            lvSV.SelectedIndexChanged += LvSV_SelectedIndexChanged;

            grpList.Controls.Add(lvSV);
        }

        // ====== HANDLERS ======
        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Họ tên không được rỗng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            var item = new ListViewItem(txtHoTen.Text.Trim());
            item.SubItems.Add(dtpNgaySinh.Value.ToString("dd/MM/yyyy"));
            item.SubItems.Add(txtLop.Text.Trim());
            item.SubItems.Add(txtDiaChi.Text.Trim());

            lvSV.Items.Add(item);
            ClearInputs();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (lvSV.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 dòng để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lvSV.Items.Remove(lvSV.SelectedItems[0]);
            ClearInputs();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (lvSV.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 dòng để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Họ tên không được rỗng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            var it = lvSV.SelectedItems[0];
            it.Text = txtHoTen.Text.Trim();
            it.SubItems[1].Text = dtpNgaySinh.Value.ToString("dd/MM/yyyy");
            it.SubItems[2].Text = txtLop.Text.Trim();
            it.SubItems[3].Text = txtDiaChi.Text.Trim();
        }

        private void LvSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSV.SelectedItems.Count == 0) return;

            var it = lvSV.SelectedItems[0];
            txtHoTen.Text = it.Text;
            DateTime d;
            if (DateTime.TryParseExact(it.SubItems[1].Text, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out d))
            {
                dtpNgaySinh.Value = d;
            }
            txtLop.Text = it.SubItems[2].Text;
            txtDiaChi.Text = it.SubItems[3].Text;
        }

        // ====== UTIL ======
        private void ClearInputs()
        {
            txtHoTen.Clear();
            txtLop.Clear();
            txtDiaChi.Clear();
            dtpNgaySinh.Value = DateTime.Today;
            txtHoTen.Focus();
        }

        // Designer stub
        private void InitializeComponent() { }
    }
}
