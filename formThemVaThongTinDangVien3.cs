using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDV
{
    public partial class formThemVaThongTinDangVien3 : Form
    {

        public List<string> l = new List<string>();
        string huyhieudang = "";
        public static bool check = false;
        public formThemVaThongTinDangVien3()
        {
            InitializeComponent();
        }



        private void formThemVaThongTinDangVien3_Load(object sender, EventArgs e)
        {
            int x = (panel2.Size.Width - label1.Size.Width) / 2;
            loaddata();
            label1.Location = new Point(x, label1.Location.Y);
            label1.Text = "III. ĐÀO TẠO, BỒI DƯỠNG VỀ CHUYÊN MÔN, NGHIỆP VỤ, LÝ LUẬN CHÍNH TRỊ, NGOẠI NGỮ";

            if(check == true)
            {
                getInfor();
                foreach(string text in huyhieudang.Split(','))
                {
                    if(checkBox1.Text == text)
                    {
                        checkBox1.Checked = true;
                    }
                    if (checkBox2.Text == text)
                    {
                        checkBox2.Checked = true;
                    }
                    if (checkBox3.Text == text)
                    {
                        checkBox3.Checked = true;
                    }
                    if (checkBox4.Text == text)
                    {
                        checkBox4.Checked = true;
                    }
                    if (checkBox5.Text == text)
                    {
                        checkBox5.Checked = true;
                    }
                    if (checkBox6.Text == text)
                    {
                        checkBox6.Checked = true;
                    }
                    if (checkBox7.Text == text)
                    {
                        checkBox7.Checked = true;
                    }
                    if (checkBox8.Text == text)
                    {
                        checkBox8.Checked = true;
                    }
                    if (checkBox9.Text == text)
                    {
                        checkBox9.Checked = true;
                    }
                    if (checkBox10.Text == text)
                    {
                        checkBox10.Checked = true;
                    }
                    if (checkBox11.Text == text)
                    {
                        checkBox11.Checked = true;
                    }
                    if (checkBox12.Text == text)
                    {
                        checkBox12.Checked = true;
                    }
                }
            }
        }




        private void setInfor()
        {
            dangvien.khenthuong = richTextBox1.Text;
            dangvien.huyhieudang = string.Join(", ", l.ToArray());

            dangvien.danhhieu = richTextBox2.Text;
            dangvien.kyluat = richTextBox3.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            formDaoTaoChung f = (formDaoTaoChung)Application.OpenForms["formDaoTaoChung"];
            if(f == null)
            {
                f = new formDaoTaoChung(this);        
            }
            f.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            formDaoTaoChung f = (formDaoTaoChung)Application.OpenForms["formDaoTaoChung"];
            if (f == null)
            {
                f = new formDaoTaoChung(this);
                f.Text = "Sửa";
                f.button1.Text = "Cập nhật";
                f.label9.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                f.textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                f.textBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                f.dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[5].Value);
                f.dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[6].Value);
                f.textBox3.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                f.textBox4.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                f.textBox5.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                f.textBox6.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            }
            f.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            connectDb con = new connectDb();
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            con.del(id, "daotaochuyenmon");
            loaddata();
        }

        public void loaddata()
        {
            connectDb con = new connectDb();
            con.rfGridFormThemVaThongTin(dataGridView1, "daotaochuyenmon", dangvien.solylich, dangvien.sothedangvien);
            setviewdtg();
        }

        private void setviewdtg()
        {
            dataGridView1.Columns[0].HeaderText = "STT";
            dataGridView1.Columns[1].HeaderText = "Số lý lịch";
            dataGridView1.Columns[2].HeaderText = "Số thẻ";
            dataGridView1.Columns[3].HeaderText = "Tên trường";
            dataGridView1.Columns[4].HeaderText = "Ngành hoặc tên lớp";
            dataGridView1.Columns[5].HeaderText = "Từ ngày";
            dataGridView1.Columns[6].HeaderText = "Đến ngày";
            dataGridView1.Columns[7].HeaderText = "Hình thức học";
            dataGridView1.Columns[8].HeaderText = "Văn bằng";
            dataGridView1.Columns[9].HeaderText = "Chứng chỉ";
            dataGridView1.Columns[10].HeaderText = "Trình độ";

            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 130;
            dataGridView1.Columns[4].Width = 130;
            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[6].Width = 150;
            dataGridView1.Columns[7].Width = 150;
            dataGridView1.Columns[8].Width = 150;
            dataGridView1.Columns[9].Width = 150;
            dataGridView1.Columns[10].Width = 150;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            formThemVaThongTinDangVien2 f = (formThemVaThongTinDangVien2)Application.OpenForms["formThemVaThongTinDangVien2"];
            if (f == null)
            {
                f = new formThemVaThongTinDangVien2();
                f.AutoScroll = true;
            }
            f.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setInfor();
            formThemVaThongTinDangVien4 f = (formThemVaThongTinDangVien4)Application.OpenForms["formThemVaThongTinDangVien4"];
            if (f == null)
            {
                f = new formThemVaThongTinDangVien4();
                f.AutoScroll = true;
            }

            f.Show();
            this.Hide();
        }

        private void formThemVaThongTinDangVien3_FormClosed(object sender, FormClosedEventArgs e)
        {
            usercontrolForm.closeForm();
        }

        private void getInfor()
        {
            connectDb con = new connectDb();
            con.con.Open();
            using (SqlCommand cmd = new SqlCommand("select * from daotaochuyenmon2 where solylich='" + dangvien.solylich + "'", con.con))
            {
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        richTextBox1.Text = read.GetString(2);
                        huyhieudang = read.GetString(3);
                        richTextBox2.Text = read.GetString(4);
                        richTextBox3.Text = read.GetString(5);
                    }
                }
            }
            con.con.Close();
        }

        private void formThemVaThongTinDangVien3_FormClosing(object sender, FormClosingEventArgs e)
        {
            usercontrolForm.closeForm();
        }
    }
}
