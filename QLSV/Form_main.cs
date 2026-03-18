using System;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLSV
{
    public partial class Form_main : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        public Form_main()
        {
            InitializeComponent();

            // Gán sự kiện
            this.Load += Form_main_Load;
            dataGridView1.CellClick += dataGridView1_CellClick;

            btn_them.Click += button1_Click; // Thêm
            btn_sua.Click += button2_Click; // Sửa
            btn_xoa.Click += button4_Click; // Xóa
            btn_lammoi.Click += button3_Click; // Làm mới
        }

        // ================= LOAD DATA =================
        private void Form_main_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            var ds = from sv in db.tbl_sinhviens
                     select new
                     {
                         sv.id,
                         sv.hoten,
                         sv.gioitinh,
                         sv.ngaysinh,
                         sv.malop
                     };

            dataGridView1.DataSource = ds;
        }

        // ================= CLICK GRID =================
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0)
            {
                txt_mssv.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                txt_hoten.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                txt_gioitinh.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                txt_ngaysinh.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                txt_malop.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            }
        }

        // ================= THÊM =================
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                tbl_sinhvien sv = new tbl_sinhvien();

                sv.id = txt_mssv.Text;
                sv.hoten = txt_hoten.Text;
                sv.gioitinh = txt_gioitinh.Text;
                sv.ngaysinh = DateTime.Parse(txt_ngaysinh.Text);
                sv.malop = txt_malop.Text;

                db.tbl_sinhviens.InsertOnSubmit(sv);
                db.SubmitChanges();

                loadData();
                MessageBox.Show("Thêm thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm: " + ex.Message);
            }
        }

        // ================= SỬA =================
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var sv = db.tbl_sinhviens.FirstOrDefault(x => x.id == txt_mssv.Text);

                if (sv != null)
                {
                    sv.hoten = txt_hoten.Text;
                    sv.gioitinh = txt_gioitinh.Text;
                    sv.ngaysinh = DateTime.Parse(txt_ngaysinh.Text);
                    sv.malop = txt_malop.Text;

                    db.SubmitChanges();
                    loadData();

                    MessageBox.Show("Sửa thành công!");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa: " + ex.Message);
            }
        }

        // ================= XOÁ =================
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var sv = db.tbl_sinhviens.FirstOrDefault(x => x.id == txt_mssv.Text);

                if (sv != null)
                {
                    db.tbl_sinhviens.DeleteOnSubmit(sv);
                    db.SubmitChanges();

                    loadData();
                    MessageBox.Show("Xóa thành công!");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xoá: " + ex.Message);
            }
        }

        // ================= LÀM MỚI =================
        private void button3_Click(object sender, EventArgs e)
        {
            txt_mssv.Clear();
            txt_hoten.Clear();
            txt_gioitinh.Clear();
            txt_ngaysinh.Value = DateTime.Now;
            txt_malop.Clear();

            loadData();
        }

        // ================= TÌM KIẾM =================
        private void button5_Click(object sender, EventArgs e)
        {
            string key = btn_timkiem.Text.ToLower();

            var ds = from sv in db.tbl_sinhviens
                     where sv.id.Contains(key)
                        || sv.hoten.Contains(key)
                        || sv.malop.Contains(key)
                     select new
                     {
                         sv.id,
                         sv.hoten,
                         sv.gioitinh,
                         sv.ngaysinh,
                         sv.malop
                     };

            dataGridView1.DataSource = ds;
        }
        private void qllh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            form_lophoc main = new form_lophoc();
            main.Show();
            this.Close();
        }
    }
}