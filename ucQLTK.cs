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
    public partial class ucQLTK : UserControl
    {
        public ucQLTK()
        {
            InitializeComponent();
        }

        private static ucQLTK _instance;
        public static ucQLTK Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucQLTK();
                return _instance;
            }
        }

        public void loaddata()
        {
            connectDb con = new connectDb();
            con.rfAccDataGrid(dataGridView1,"taikhoan");
            setViewDtg();
            foreach(DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void setViewDtg()
        {
            dataGridView1.Columns[0].HeaderText = "STT";
            dataGridView1.Columns[1].HeaderText = "Tên tài khoản";
            dataGridView1.Columns[2].HeaderText = "Mật khẩu";
            dataGridView1.Columns[3].HeaderText = "Mã khoa";
            dataGridView1.Columns[4].HeaderText = "Quyền truy cập";
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 90;
            dataGridView1.Columns[4].Width = 120;
        }
        private void ucQLTK_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            loaddata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formAddAcc faa = (formAddAcc)Application.OpenForms["formAddAcc"];
            if(faa == null)
            {
                faa = new formAddAcc(this);
            }
            faa.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formUpdateAcc fua = (formUpdateAcc)Application.OpenForms["formUpdateAcc"];
            if (fua == null)
            {
                fua = new formUpdateAcc(this);
                fua.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                fua.textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                fua.textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            }
            fua.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string tentk = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            connectDb con = new connectDb();
            DialogResult dr = MessageBox.Show("Bạn có muốn xóa tài khoản: "+tentk+" không?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {
                con.del(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value),"taikhoan");
                MessageBox.Show("Xóa thành công tài khoản: " + tentk  , "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            loaddata();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            connectDb con = new connectDb();
            string col = "";
            if (radioButton1.Checked)
            {
                col = "tentk";
            }
            else
            {
                col = "chibo";
            }
            setViewDtg();
            con.dtgFilter(dataGridView1,col,textBox1.Text,"taikhoan");
        }
    }
}
