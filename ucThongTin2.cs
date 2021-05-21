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
    public partial class ucThongTin2 : UserControl
    {
        public bool dt1 = false;
        public bool dt2 = false;
        public ucThongTin2()
        {
            InitializeComponent();
        }
        private static ucThongTin2 _instance;
        public static ucThongTin2 Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ucThongTin2();
                }
                return _instance;
            }
        }

        private void ucThongTin2_Load(object sender, EventArgs e)
        {
            dateTimePicker1.ValueChanged += new EventHandler(dateTimePicker1_ValueChanged);
            dateTimePicker2.ValueChanged += new EventHandler(dateTimePicker2_ValueChanged);
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker2.MaxDate = DateTime.Now;
            loaddata();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dt1 = true;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dt2 = true;
        }
        private void button5_Click(object sender, EventArgs e)
        {

            connectDb con = new connectDb();
            if (dt1 == true && dt2 == true)
            {
                con.quatrinhhoatdong("insert", 0, dangvien.solylich, dangvien.sothedangvien, dateTimePicker1.Value, dateTimePicker2.Value, textBox1.Text, textBox2.Text, textBox3.Text);
                dateTimePicker1.Value = DateTime.Today;
                dateTimePicker2.Value = DateTime.Today;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                loaddata();
            }
            else
            {
                MessageBox.Show(
                    "'Từ ngày' hoặc 'Đến ngày 'chưa được chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button8_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            connectDb con = new connectDb();
            con.quatrinhhoatdong("update", id, dangvien.solylich, dangvien.sothedangvien, dateTimePicker1.Value, dateTimePicker2.Value, textBox1.Text, textBox2.Text, textBox3.Text);
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            loaddata();



        }

        private void button9_Click(object sender, EventArgs e)
        {

            connectDb con = new connectDb();
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            con.del(id, "quatrinhhoatdong");
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            loaddata();


        }

        public void loaddata()
        {
            connectDb con = new connectDb();
            con.rfGridFormThemVaThongTin(dataGridView1, "quatrinhhoatdong", dangvien.solylich, dangvien.sothedangvien);
            setviewdtg();
        }

        private void setviewdtg()
        {
            dataGridView1.Columns[0].HeaderText = "STT";
            dataGridView1.Columns[1].HeaderText = "Số lý lịch";
            dataGridView1.Columns[2].HeaderText = "Số thẻ";
            dataGridView1.Columns[3].HeaderText = "Từ ngày";
            dataGridView1.Columns[4].HeaderText = "Đến ngày";
            dataGridView1.Columns[5].HeaderText = "Làm";
            dataGridView1.Columns[6].HeaderText = "Chức vụ";
            dataGridView1.Columns[7].HeaderText = "Đơn vị";

            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 130;
            dataGridView1.Columns[4].Width = 130;
            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[6].Width = 150;
            dataGridView1.Columns[7].Width = 150;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[3].Value);
                dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[4].Value);
                textBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            }

        }
    }
}
