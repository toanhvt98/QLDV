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

        private void setviewDTG()
        {
            anhdangvienDataGridViewImageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dataGridView1.Columns[0].HeaderText = "STT";
            dataGridView1.Columns[1].HeaderText = "Ảnh Đảng viên";
            dataGridView1.Columns[2].HeaderText = "Số lý lịch";
            dataGridView1.Columns[3].HeaderText = "Số thẻ";
            dataGridView1.Columns[4].HeaderText = "Tên Đảng viên";

            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[1].Width = 170;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[4].Width = 150;

            dataGridView1.Columns[0].Visible = false;

        }

        public void loaddata()
        {
            connectDb con = new connectDb();
            
            con.selectDangVien(dataGridView1);
            setviewDTG();
            dataGridView1.Columns[0].Visible = false;
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

        }
    }
}
