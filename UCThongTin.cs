using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDV
{
    public partial class UCThongTin : UserControl
    {
        public UCThongTin()
        {
            InitializeComponent();
        }
        private static UCThongTin _instance;
        public static UCThongTin Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new UCThongTin();
                }
                return _instance;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null) { 
            formMain fm = (formMain)Application.OpenForms["formMain"];
            fm.panel1.Controls.Add(uclDangVien.Instance);
            uclDangVien.Instance.Dock = DockStyle.Fill;
            uclDangVien.Instance.button3.Visible = false;
            uclDangVien.Instance.button4.Visible = true;
            uclDangVien.Instance.button5.Visible = true;
            var data = (Byte[])(dataGridView1.CurrentRow.Cells[1].Value);
            var stream = new MemoryStream(data);
            uclDangVien.Instance.pictureBox1.Image = Image.FromStream(stream);
            uclDangVien.Instance.pictureBox1.BackgroundImage = null;

            uclDangVien.Instance.textBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            uclDangVien.Instance.textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            uclDangVien.Instance.textBox1.ReadOnly = true;
            uclDangVien.Instance.textBox2.ReadOnly = true;
            uclDangVien.Instance.button1.Text = "Đổi ảnh";
            uclDangVien.Instance.BringToFront();
               
        }     
        }

        public void setviewDTG(DataGridView dtg)
        {

            dtg.Columns[0].Visible = false;
            ((DataGridViewImageColumn)dtg.Columns[1]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            
            dtg.Columns[0].HeaderText = "STT";
            dtg.Columns[1].HeaderText = "Ảnh Đảng viên";
            dtg.Columns[2].HeaderText = "Số lý lịch";
            dtg.Columns[3].HeaderText = "Số thẻ";
            dtg.Columns[4].HeaderText = "Tên Đảng viên";

            dtg.Columns[0].Width = 70;
            dtg.Columns[1].Width = 170;
            dtg.Columns[2].Width = 120;
            dtg.Columns[3].Width = 120;
            dtg.Columns[4].Width = 150;
        }

        public void loaddata()
        {
            connectDb con = new connectDb();
            radioButton1.Checked = true;
            con.selectDangVien(dataGridView1);
            //setviewDTG();
            
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void UCThongTin_Load(object sender, EventArgs e)
        {
            
            loaddata();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            optionExport f = (optionExport)Application.OpenForms["optionExport"];
            if(f == null)
            {
                f = new optionExport();
            }
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                connectDb con = new connectDb();
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa tất cả thông tin của Đảng viên này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    con.dellDV(dataGridView1.CurrentRow.Cells[2].Value.ToString(), dataGridView1.CurrentRow.Cells[3].Value.ToString());
                }
                loaddata();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            connectDb con = new connectDb();
            string col = "";
            if(radioButton1.Checked == true)
            {
                col = "solylich";
            }
            else if (radioButton2.Checked == true)
            {
                col = "sothe";
            }
            else
            {
                col = "tendangvien";
            }
            
            con.dtgFilter(dataGridView1,col,textBox1.Text,"dangvien");
            setviewDTG(dataGridView1);
        }
    }
}
