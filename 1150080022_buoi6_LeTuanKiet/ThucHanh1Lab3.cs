using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace _1150080022_buoi6_LeTuanKiet
{
    public partial class Form1 : Form
    {
        private DataTable dtOrder;
        private ComboBox cmbBan;
        private DataGridView dgvOrder;
        private Button btnOrder, btnXoa;

        public Form1()
        {
            InitializeComponent();
            KhoiTaoGiaoDien();
            KhoiTaoDataTable();
        }

        private void KhoiTaoGiaoDien()
        {
            // Form rộng + cao hơn một chút
            Text = "Quán ăn nhanh Hưng Thịnh";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(660, 560);
            Font = new Font("Microsoft Sans Serif", 9F);

            // HEADER
            var pnlHeader = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(640, 72),
                BorderStyle = BorderStyle.FixedSingle
            };

            var picLogo = new PictureBox
            {
                Location = new Point(8, 7),
                Size = new Size(70, 56),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White
            };
            picLogo.Load("https://img.lovepik.com/png/20231106/gourmet-burger-cheese-Hamburger-cheeseburger-A-meal_506563_wh860.png");

            var lblBanner = new Label
            {
                Text = "Quán ăn nhanh Hưng Thịnh",
                Location = new Point(92, 10),
                Size = new Size(540, 50),
                BackColor = Color.Green,
                ForeColor = Color.White,
                Font = new Font("Arial", 18, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlHeader.Controls.Add(picLogo);
            pnlHeader.Controls.Add(lblBanner);
            Controls.Add(pnlHeader);

            // DANH SÁCH MÓN ĂN (cao + rộng hơn)
            var grp = new GroupBox
            {
                Text = "Danh sách món ăn:",
                Location = new Point(10, 90),
                Size = new Size(640, 230) // tăng chiều cao
            };
            Controls.Add(grp);

            // helper tạo nút món – tăng chút kích thước & khoảng cách
            Button TaoButton(string text, int col, int row)
            {
                var btn = new Button
                {
                    Text = text,
                    Size = new Size(140, 36),
                    Location = new Point(12 + col * 156, 28 + row * 46)
                };
                btn.Click += BtnMonAn_Click;
                return btn;
            }

            // Hàng 1
            grp.Controls.Add(TaoButton("Cơm chiên trứng", 0, 0));
            grp.Controls.Add(TaoButton("Bánh mỳ ốp la", 1, 0));
            grp.Controls.Add(TaoButton("Coca", 2, 0));
            grp.Controls.Add(TaoButton("Lipton", 3, 0));

            // Hàng 2
            grp.Controls.Add(TaoButton("Ốc rang muối", 0, 1));
            grp.Controls.Add(TaoButton("Khoai tây chiên", 1, 1));
            grp.Controls.Add(TaoButton("7 up", 2, 1));
            grp.Controls.Add(TaoButton("Cam", 3, 1));

            // Hàng 3
            grp.Controls.Add(TaoButton("Mỳ xào hải sản", 0, 2));
            grp.Controls.Add(TaoButton("Cá viên chiên", 1, 2));
            grp.Controls.Add(TaoButton("Pepsi", 2, 2));
            grp.Controls.Add(TaoButton("Cafe", 3, 2));

            // Hàng 4
            grp.Controls.Add(TaoButton("Buger bò nướng", 0, 3));
            grp.Controls.Add(TaoButton("Đùi gà rán", 1, 3));
            grp.Controls.Add(TaoButton("Bún bò Huế", 2, 3));

            // DÒNG CHỨC NĂNG (dưới groupbox)
            int yFn = grp.Bottom + 8;

            btnXoa = new Button { Text = "Xóa", Location = new Point(22, yFn), Size = new Size(85, 30) };
            btnXoa.Click += BtnXoa_Click;
            Controls.Add(btnXoa);

            var lblChonBan = new Label { Text = "Chọn bàn:", Location = new Point(130, yFn + 6), AutoSize = true };
            Controls.Add(lblChonBan);

            cmbBan = new ComboBox
            {
                Location = new Point(200, yFn + 3),
                Size = new Size(210, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            for (int i = 1; i <= 10; i++) cmbBan.Items.Add($"Bàn {i}");
            cmbBan.SelectedIndex = 0;
            Controls.Add(cmbBan);

            btnOrder = new Button { Text = "Order", Location = new Point(430, yFn), Size = new Size(90, 30) };
            btnOrder.Click += BtnOrder_Click;
            Controls.Add(btnOrder);

            // LƯỚI ORDER (cao hơn)
            dgvOrder = new DataGridView
            {
                Location = new Point(10, yFn + 36),
                Size = new Size(640, 180), // tăng chiều cao
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.Gainsboro,
                RowHeadersVisible = false
            };
            dgvOrder.RowTemplate.Height = 26;
            dgvOrder.ColumnHeadersHeight = 28;
            Controls.Add(dgvOrder);
        }

        private void KhoiTaoDataTable()
        {
            dtOrder = new DataTable();
            dtOrder.Columns.Add("Tên món", typeof(string));
            dtOrder.Columns.Add("Số lượng", typeof(int));
            dgvOrder.DataSource = dtOrder;
        }

        private void BtnMonAn_Click(object sender, EventArgs e)
        {
            var tenMon = ((Button)sender).Text;
            var rows = dtOrder.Select($"[Tên món] = '{tenMon.Replace("'", "''")}'");
            if (rows.Length > 0) rows[0]["Số lượng"] = (int)rows[0]["Số lượng"] + 1;
            else dtOrder.Rows.Add(tenMon, 1);
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvOrder.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn món cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dtOrder.Rows[dgvOrder.SelectedRows[0].Index].Delete();
        }

        private void BtnOrder_Click(object sender, EventArgs e)
        {
            if (dtOrder.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có món nào được chọn!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenBan = cmbBan.SelectedItem.ToString();
            string fileName = $"Order_{tenBan}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

            try
            {
                using (var sw = new StreamWriter(fileName))
                {
                    sw.WriteLine("=====================================");
                    sw.WriteLine("     QUÁN ĂN NHANH HƯNG THỊNH");
                    sw.WriteLine("=====================================");
                    sw.WriteLine($"Bàn: {tenBan}");
                    sw.WriteLine($"Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                    sw.WriteLine("-------------------------------------");
                    sw.WriteLine("TÊN MÓN\t\t\tSỐ LƯỢNG");
                    sw.WriteLine("-------------------------------------");
                    foreach (DataRow row in dtOrder.Rows)
                        sw.WriteLine($"{row["Tên món"]}\t\t{row["Số lượng"]}");
                    sw.WriteLine("=====================================");
                }

                MessageBox.Show($"Order thành công!\nĐã lưu vào file: {fileName}",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtOrder.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi ghi file: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent() { }
    }
}
