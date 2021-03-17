using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDV
{
    public partial class UCDaoTaoChung3 : UserControl
    {
        public List<string> l = new List<string>();

        public static bool check = false;
        public UCDaoTaoChung3()
        {
            InitializeComponent();
        }



        private static UCDaoTaoChung3 _instance;
        public static UCDaoTaoChung3 Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new UCDaoTaoChung3();
                }
                return _instance;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formThemVaThongTinDangVien dtvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
            dtvttdv.label1.Text = "II. TÓM TẮT QUÁ TRÌNH HOẠT ĐỘNG VÀ CÔNG TÁC";
            int x = (dtvttdv.panel2.Size.Width - dtvttdv.label1.Size.Width) / 2;
            dtvttdv.label1.Location = new Point(x, dtvttdv.label1.Location.Y);
            dtvttdv.panel1.Controls.Add(ucQuaTrinhHoatDongVaCongTac2.Instance);
            ucQuaTrinhHoatDongVaCongTac2.Instance.Dock = DockStyle.Fill;
            ucQuaTrinhHoatDongVaCongTac2.Instance.BringToFront();
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
         
            if (checkBox1.Checked)
            {
                l.Add(checkBox1.Text);
            }
            if (checkBox2.Checked)
            {
                l.Add(checkBox2.Text);
            }
            if (checkBox3.Checked)
            {
                l.Add(checkBox3.Text);
            }
            if (checkBox4.Checked)
            {
                l.Add(checkBox4.Text);
            }
            if (checkBox5.Checked)
            {
                l.Add(checkBox5.Text);
            }
            if (checkBox6.Checked)
            {
                l.Add(checkBox6.Text);
            }
            if (checkBox7.Checked)
            {
                l.Add(checkBox7.Text);
            }
            if (checkBox8.Checked)
            {
                l.Add(checkBox8.Text);
            }
            if (checkBox9.Checked)
            {
                l.Add(checkBox9.Text);
            }
            if (checkBox10.Checked)
            {
                l.Add(checkBox10.Text);
            }
            if (checkBox11.Checked)
            {
                l.Add(checkBox11.Text);
            }
            if (checkBox12.Checked)
            {
                l.Add(checkBox12.Text);
            }

            formThemVaThongTinDangVien dtvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
            dtvttdv.label1.Text = "IV. ĐẶC ĐIỂM LỊCH SỬ VÀ QUAN HỆ VỚI NƯỚC NGOÀI";
            int x = (dtvttdv.panel2.Size.Width - dtvttdv.label1.Size.Width) / 2;
            dtvttdv.label1.Location = new Point(x, dtvttdv.label1.Location.Y);
            dtvttdv.panel1.Controls.Add(ucDdlsVaQhng4.Instance);
            ucDdlsVaQhng4.Instance.Dock = DockStyle.Fill;
            ucDdlsVaQhng4.Instance.BringToFront();
            setInfor();
        }

        private void UCDaoTaoChung3_Load(object sender, EventArgs e)
        {
            if(check == true)
            {
                foreach (Control c in this.Controls)
                {
                    if (c is RichTextBox)
                    {
                        ((RichTextBox)(c)).Text = "";
                    }
                    else if (c is DataGridView)
                    {
                        ((DataGridView)c).DataSource = null;
                    }
                    else if(c is CheckBox)
                    {
                        ((CheckBox)c).Checked = false;
                    }
                }
            }
            loaddata();
            dataGridView1.Columns[0].Visible = false;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void setInfor()
        {
            dangvien.khenthuong = richTextBox1.Text;
            dangvien.huyhieudang = string.Join(", ",l.ToArray());

            dangvien.danhhieu = richTextBox2.Text;
            dangvien.kyluat = richTextBox3.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            formDaoTaoChung fadctc = (formDaoTaoChung)Application.OpenForms["formAddDaoTaoChung"];
            if(fadctc == null)
            {
                fadctc = new formDaoTaoChung(this);
            }
            fadctc.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            formDaoTaoChung fadctc = (formDaoTaoChung)Application.OpenForms["formAddDaoTaoChung"];
            
            if (fadctc == null)
            {
                fadctc = new formDaoTaoChung(this);
                fadctc.Text = "Sửa";
                fadctc.label9.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                fadctc.button1.Text = "Cập nhật";
                fadctc.textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                fadctc.textBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                fadctc.dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[5].Value);
                fadctc.dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[6].Value);
                fadctc.textBox3.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                fadctc.textBox4.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                fadctc.textBox5.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                fadctc.textBox6.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            }
            fadctc.Show();
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
            con.rfGridFormThemVaThongTin(dataGridView1, "daotaochuyenmon",dangvien.solylich,dangvien.sothedangvien);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
