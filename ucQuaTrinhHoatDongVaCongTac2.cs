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
    public partial class ucQuaTrinhHoatDongVaCongTac2 : UserControl
    {

        private ucHoanCanhGiaDinh6 uc6;

        public static bool check = false;

       

        public bool dt1 = false;
        public bool dt2 = false;
        public ucQuaTrinhHoatDongVaCongTac2(ucHoanCanhGiaDinh6 uc6)
        {
            InitializeComponent();
            this.uc6 = uc6;
        }

        public ucQuaTrinhHoatDongVaCongTac2()
        {
            InitializeComponent();
        }


        private static ucQuaTrinhHoatDongVaCongTac2 _instance;
        public static ucQuaTrinhHoatDongVaCongTac2 Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ucQuaTrinhHoatDongVaCongTac2();
                }
                return _instance;
            }
        }
        private void ucQuaTrinhHoatDongVaCongTac_Load(object sender, EventArgs e)
        {
            dateTimePicker1.ValueChanged += new EventHandler(dateTimePicker1_ValueChanged);
            dateTimePicker2.ValueChanged += new EventHandler(dateTimePicker2_ValueChanged);
            dateTimePicker1.MaxDate = DateTime.Today;
            dateTimePicker2.MaxDate = DateTime.Today;           
            if (check == true)
            {
                foreach (Control c in groupBox1.Controls)
                {
                    if (c is TextBox)
                    {
                        ((TextBox)(c)).Text = "";
                    }
                    else if (c is DateTimePicker)
                    {
                        ((DateTimePicker)c).Value = DateTime.Today;
                    }

                }
                foreach (Control c in this.Controls)
                {
                    if (c is DataGridView)
                    {
                        ((DataGridView)c).DataSource = null;
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

            
            dateTimePicker1.Value = DateTime.Today.AddDays(-1);
            dateTimePicker2.Value = DateTime.Today;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            formThemVaThongTinDangVien dtvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
            dtvttdv.label1.Text = "I. THÔNG TIN CƠ BẢN";
            int x = (dtvttdv.panel2.Size.Width - dtvttdv.label1.Size.Width) / 2;
            dtvttdv.label1.Location = new Point(x, dtvttdv.label1.Location.Y);
            dtvttdv.panel1.Controls.Add(ucTTCBDangVien.Instance);
            ucTTCBDangVien.Instance.Dock = DockStyle.Fill;
            ucTTCBDangVien.Instance.BringToFront();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            formThemVaThongTinDangVien dtvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
            dtvttdv.label1.Text = "III. ĐÀO TẠO, BỒI DƯỠNG VỀ CHUYÊN MÔN, NGHIỆP VỤ, LÝ LUẬN CHÍNH TRỊ, NGOẠI NGỮ";
            int x = (dtvttdv.panel2.Size.Width - dtvttdv.label1.Size.Width) / 2;
            dtvttdv.label1.Location = new Point(x, dtvttdv.label1.Location.Y);
            dtvttdv.panel1.Controls.Add(UCDaoTaoChung3.Instance);
            UCDaoTaoChung3.Instance.Dock = DockStyle.Fill;
            UCDaoTaoChung3.Instance.BringToFront();
            
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
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            connectDb con = new connectDb();
                if (dt1 == true && dt2 == true)
                {
                    con.quatrinhhoatdong("insert",id, dangvien.solylich, dangvien.sothedangvien, dateTimePicker1.Value, dateTimePicker2.Value, textBox1.Text, textBox2.Text, textBox3.Text);
                    dateTimePicker1.Value = DateTime.Today.AddDays(-1);
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
                con.quatrinhhoatdong("update",id, dangvien.solylich, dangvien.sothedangvien, dateTimePicker1.Value, dateTimePicker2.Value, textBox1.Text, textBox2.Text, textBox3.Text);
                dateTimePicker1.Value = DateTime.Today.AddDays(-1);
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
                dateTimePicker1.Value = DateTime.Today.AddDays(-1);
                dateTimePicker2.Value = DateTime.Today;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                loaddata();

            
        }

        public void loaddata()
        {
            connectDb con = new connectDb();
            con.rfGridFormThemVaThongTin(dataGridView1, "quatrinhhoatdong",dangvien.solylich,dangvien.sothedangvien);
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
            if(e.RowIndex != -1)
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
