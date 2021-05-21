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
    public partial class ucThongTin5 : UserControl
    {
        public ucThongTin5()
        {
            InitializeComponent();
        }
        private static ucThongTin5 _instance;
        public static ucThongTin5 Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ucThongTin5();
                }
                return _instance;
            }
        }
        private void ucThongTin5_Load(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(DateTime.Now.Year.ToString());
            while (i >= 1900)
            {
                comboBox1.Items.Add(i);
                i--;
            }
            comboBox1.SelectedIndex = 0;
            loaddata();
            dataGridView1.Columns[6].Visible = false;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {

            connectDb con = new connectDb();
            con.quanheGD("insert", 0, dangvien.solylich, dangvien.sothedangvien, textBox1.Text, textBox2.Text, Convert.ToInt32(comboBox1.Text), richTextBox1.Text);
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedIndex = 0;
            richTextBox1.Text = "";
            loaddata();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[6].Value);
            connectDb con = new connectDb();
            con.quanheGD("update", id, dangvien.solylich, dangvien.sothedangvien, textBox1.Text, textBox2.Text, Convert.ToInt32(comboBox1.Text), richTextBox1.Text);
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedIndex = 0;
            richTextBox1.Text = "";
            loaddata();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[6].Value);
            connectDb con = new connectDb();
            con.del(id, "qhgiadinh");
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedIndex = 0;
            richTextBox1.Text = "";
            loaddata();
        }


        public void loaddata()
        {
            connectDb con = new connectDb();
            con.rfGridFormThemVaThongTin(dataGridView1, "qhgiadinh", dangvien.solylich, dangvien.sothedangvien);
            setviewDtg();
        }

        public void setviewDtg()
        {
            dataGridView1.Columns[0].HeaderText = "Số lý lịch";
            dataGridView1.Columns[1].HeaderText = "Số thẻ";
            dataGridView1.Columns[2].HeaderText = "Quan hệ";
            dataGridView1.Columns[3].HeaderText = "Họ tên";
            dataGridView1.Columns[4].HeaderText = "Năm sinh";
            dataGridView1.Columns[5].HeaderText = "Thông tin khác";
            dataGridView1.Columns[6].HeaderText = "STT";

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 130;
            dataGridView1.Columns[4].Width = 130;
            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[6].Width = 70;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                comboBox1.SelectedItem = dataGridView1.CurrentRow.Cells[4].Value;
                richTextBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            }

        }
    }
}
