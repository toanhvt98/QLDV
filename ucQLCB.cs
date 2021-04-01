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
    public partial class ucQLCB : UserControl
    {
        public ucQLCB()
        {
            InitializeComponent();
        }
        private static ucQLCB _instance;
        public static ucQLCB Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ucQLCB();
                }
                return _instance;
            }
        }
        public void loaddata()
        {
            connectDb con = new connectDb();
            con.rfAccDataGrid(dataGridView1, "chibo");
            setViewDtg();
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void ucQLCB_Load(object sender, EventArgs e)
        {
            
            radioButton1.Checked = true;
            loaddata();
        }

        private void setViewDtg()
        {
            dataGridView1.Columns[0].HeaderText = "STT";
            dataGridView1.Columns[1].HeaderText = "Mã chi bộ";
            dataGridView1.Columns[2].HeaderText = "Tên chi bộ";
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[2].Width = 250;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formAddChiBo facb = (formAddChiBo)Application.OpenForms["formAddChiBo"];
            if(facb == null)
            {
                facb = new formAddChiBo(this);
            }
            facb.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formUpdateChiBo facb = (formUpdateChiBo)Application.OpenForms["formAddChiBo"];
            if (facb == null)
            {
                facb = new formUpdateChiBo(this);
                facb.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                facb.textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                facb.textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            }
            facb.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string tenchibo = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            connectDb con = new connectDb();
            DialogResult dr = MessageBox.Show("Bạn có muốn xóa chi bộ: " + tenchibo + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                con.del(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), "chibo");
                MessageBox.Show("Xóa thành công chi bộ: " + tenchibo, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            loaddata();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            connectDb con = new connectDb();
            string col = "";
            if (radioButton1.Checked)
            {
                col = "ten";
            }
            else
            {
                col = "ma";
            }
            setViewDtg();
            con.dtgFilter(dataGridView1,col,textBox1.Text,"chibo");
        }
    }
}
