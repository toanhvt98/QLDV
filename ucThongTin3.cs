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
    public partial class ucThongTin3 : UserControl
    {
        public List<string> l = new List<string>();
        string huyhieudang = "";
        public static bool check = false;

        public ucThongTin3()
        {
            InitializeComponent();
        }
        private static ucThongTin3 _instance;
        public static ucThongTin3 Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ucThongTin3();
                }
                return _instance;
            }
        }
        private void ucThongTin3cs_Load(object sender, EventArgs e)
        {
            connectDb con = new connectDb();
            if (check == true)
            {
                getInfor();
                string a = String.Concat(con.getTextCheckBox("daotaochuyenmon2", "huyhieu", dangvien.solylich).Where(c => !Char.IsWhiteSpace(c)));
                List<string> l = a.Split(',').ToList();
                for (int i = 0; i < l.Count; i++)
                {
                    if (l[i] == String.Concat(checkBox1.Text.Where(c => !Char.IsWhiteSpace(c))))
                    {
                        checkBox1.Checked = true;
                    }
                    if (l[i] == String.Concat(checkBox2.Text.Where(c => !Char.IsWhiteSpace(c))))
                    {
                        checkBox2.Checked = true;
                    }
                    if (l[i] == String.Concat(checkBox3.Text.Where(c => !Char.IsWhiteSpace(c))))
                    {
                        checkBox3.Checked = true;
                    }
                    if (l[i] == String.Concat(checkBox4.Text.Where(c => !Char.IsWhiteSpace(c))))
                    {
                        checkBox4.Checked = true;
                    }
                    if (l[i] == String.Concat(checkBox5.Text.Where(c => !Char.IsWhiteSpace(c))))
                    {
                        checkBox5.Checked = true;
                    }
                    if (l[i] == String.Concat(checkBox6.Text.Where(c => !Char.IsWhiteSpace(c))))
                    {
                        checkBox6.Checked = true;
                    }
                    if (l[i] == String.Concat(checkBox7.Text.Where(c => !Char.IsWhiteSpace(c))))
                    {
                        checkBox7.Checked = true;
                    }
                    if (l[i] == String.Concat(checkBox8.Text.Where(c => !Char.IsWhiteSpace(c))))
                    {
                        checkBox8.Checked = true;
                    }
                    if (l[i] == String.Concat(checkBox9.Text.Where(c => !Char.IsWhiteSpace(c))))
                    {
                        checkBox9.Checked = true;
                    }
                    if (l[i] == String.Concat(checkBox10.Text.Where(c => !Char.IsWhiteSpace(c))))
                    {
                        checkBox10.Checked = true;
                    }
                    if (l[i] == String.Concat(checkBox11.Text.Where(c => !Char.IsWhiteSpace(c))))
                    {
                        checkBox11.Checked = true;
                    }
                    if (l[i] == String.Concat(checkBox12.Text.Where(c => !Char.IsWhiteSpace(c))))
                    {
                        checkBox12.Checked = true;
                    }
                }
            }
            loaddata();
        }
        public void setInfor()
        {
            dangvien.khenthuong = ucThongTin3.Instance.richTextBox1.Text;
            List<string> checkedItems = (from Control c in Controls where c is CheckBox && ((CheckBox)c).Checked select c.Text).ToList();
            dangvien.huyhieudang = string.Join(", ", checkedItems.ToArray());

            dangvien.danhhieu = ucThongTin3.Instance.richTextBox2.Text;
            dangvien.kyluat = ucThongTin3.Instance.richTextBox3.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            formDaoTaoChung f = (formDaoTaoChung)Application.OpenForms["formDaoTaoChung"];
            if (f == null)
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
        public void getInfor()
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

    }
}
