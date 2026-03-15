using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLSV
{
    public partial class form_lophoc : Form
    {
        string connectionString = "Data Source=.;Initial Catalog=QLSV;Integrated Security=True";

        public form_lophoc()
        {
            InitializeComponent();
        }

        // Load dữ liệu khi mở form
        private void form_lophoc_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // Hàm lấy dữ liệu từ DB
        void LoadData()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            string query = "SELECT * FROM lophoc";

            SqlDataAdapter da = new SqlDataAdapter(query, conn);

            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        // Thêm dữ liệu
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            string query = "INSERT INTO lophoc VALUES (@malop,@giangvien,@monhoc,@sosinhvien)";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@malop", textBox1.Text);
            cmd.Parameters.AddWithValue("@giangvien", textBox2.Text);
            cmd.Parameters.AddWithValue("@monhoc", textBox3.Text);
            cmd.Parameters.AddWithValue("@sosinhvien", textBox4.Text);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            LoadData();
        }

        // Sửa dữ liệu
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            string query = "UPDATE lophoc SET giangvien=@giangvien, monhoc=@monhoc, sosinhvien=@sosinhvien WHERE malop=@malop";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@malop", textBox1.Text);
            cmd.Parameters.AddWithValue("@giangvien", textBox2.Text);
            cmd.Parameters.AddWithValue("@monhoc", textBox3.Text);
            cmd.Parameters.AddWithValue("@sosinhvien", textBox4.Text);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            LoadData();
        }

        // Xóa dữ liệu
        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            string query = "DELETE FROM lophoc WHERE malop=@malop";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@malop", textBox1.Text);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            LoadData();
        }

        // Tìm kiếm
        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            string query = "SELECT * FROM lophoc WHERE malop LIKE @search OR monhoc LIKE @search";

            SqlDataAdapter da = new SqlDataAdapter(query, conn);

            da.SelectCommand.Parameters.AddWithValue("@search", "%" + textBox6.Text + "%");

            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        // Click vào bảng để hiển thị lên textbox
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;

            textBox1.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
        }
    }
}