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
    public partial class formDaoTaoChung : Form
    {
        private readonly formThemVaThongTinDangVien3 ucDTC3;

        public static bool dt1 = false;
        public static bool dt2 = false;

        public formDaoTaoChung(formThemVaThongTinDangVien3 ucDTC3)
        {
            InitializeComponent();
            this.ucDTC3 = ucDTC3;
            dateTimePicker1.MaxDate = DateTime.Today;
            dateTimePicker2.MaxDate = DateTime.Now;
            dateTimePicker1.Value = DateTime.Today.AddDays(-1);
            dateTimePicker2.Value = DateTime.Today;
        }
        public formDaoTaoChung()
        {
            InitializeComponent();

            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker2.MaxDate = DateTime.Now;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            connectDb con = new connectDb();
            if(button1.Text == "Thêm")
            {
                if (dt1 == true && dt2 == true)
                {
                    con.DaoTaoChung1("insert",0, dangvien.solylich, dangvien.sothedangvien, textBox1.Text
                    , textBox2.Text, dateTimePicker1.Value, dateTimePicker2.Value, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text);

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    dateTimePicker1.Value = DateTime.Today;
                    dateTimePicker2.Value = DateTime.Today;

                }
                else
                {
                    MessageBox.Show(
                        "'Từ ngày' hoặc 'Đến ngày 'chưa được chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                int id = Convert.ToInt32(label9.Text);
                con.DaoTaoChung1("update",id, dangvien.solylich, dangvien.sothedangvien, textBox1.Text
                    , textBox2.Text, dateTimePicker1.Value, dateTimePicker2.Value, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text);

                MessageBox.Show(
                        "Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            
            ucDTC3.loaddata();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dt1 = true;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dt2 = true;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
