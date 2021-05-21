using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDV
{
    public partial class ucQuanLyFilecs : UserControl
    {
         static string value;
         bool checkBtn1 = false;
         bool checkBtn2 = false;
        public ucQuanLyFilecs()
        {
            InitializeComponent();
        }
        private static ucQuanLyFilecs _instance;
        public static ucQuanLyFilecs Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ucQuanLyFilecs();
                }
                return _instance;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            formSenFile f = (formSenFile)Application.OpenForms["formSenFile"];
            if(f == null)
            {
                f = new formSenFile();
            }
            f.ShowDialog();
        }
        public void setValue()
        {
            if (radioButton1.Checked == true)
            {
                
                
                if (dataGridView1.Visible == true)
                {
                    saveForSendFile.value = "tkgui";
                }
                else if (dataGridView2.Visible == true)
                {
                    saveForSendFile.value = "tknhan";
                }
            }
            
            if (radioButton3.Checked == true)
            {

                

                saveForSendFile.value = "tieude";
                
            }
        }
        private void ucQuanLyFilecs_Load(object sender, EventArgs e)
        {

            connectDb con = new connectDb();
            
            radioButton1.Checked = true;

            checkBtn1 = true;
            
            dataGridView1.Visible = true;
            
            dataGridView2.Visible = false;
            
            loadata();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadata();
            checkBtn1 = true;
            checkBtn2 = false;
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            textBox1.Text = "";
            radioButton1.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadata1();
            checkBtn2 = true;
            checkBtn1 = false;
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            textBox1.Text = "";
            radioButton1.Checked = true;
        }

        public void setdtgview(DataGridView dtg)
        {
            dtg.Columns[0].Visible = false;
            dtg.Columns[1].HeaderText = "Người gửi";
            dtg.Columns[2].HeaderText = "Quyền người gửi";
            dtg.Columns[3].HeaderText = "Chi bộ";
            dtg.Columns[4].HeaderText = "Người nhận";
            dtg.Columns[5].HeaderText = "Quyền người nhận";
            dtg.Columns[6].HeaderText = "Chi bộ";
            dtg.Columns[7].HeaderText = "Tiêu đề";
            dtg.Columns[8].HeaderText = "Nội dung";
            dtg.Columns[9].HeaderText = "Tên file";
            dtg.Columns[10].HeaderText = "Ngày gửi";
            foreach (DataGridViewColumn col in dtg.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            
        }

        public void loadata()
        {
            //setValue();
            label2.Text = button1.Text;
            connectDb con = new connectDb();
            con.rfAccDataGrid4File(dataGridView1,  "guifile", null, userInfor.username,null);
            setdtgview(dataGridView1);
            
        }
        public void loadata1()
        {
            //setValue();
            label2.Text = button2.Text;
            connectDb con = new connectDb();
            con.rfAccDataGrid4File(dataGridView2, "guifile", userInfor.username, null,null);
            setdtgview(dataGridView2);
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(checkBtn1 == true)
            {
                if (dataGridView1.RowCount != 0)
                {
                    connectDb con = new connectDb();
                    con.downloadFileDtg1(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), null);
                    MessageBox.Show("Đã tải xong");
                }
            }
            if (checkBtn2 == true)
            {
                if (dataGridView2.RowCount != 0)
                {
                    connectDb con = new connectDb();
                    con.downloadFileDtg2(Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value), null);
                    MessageBox.Show("Đã tải xong");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(checkBtn1 == true)
            {
                if (dataGridView1.RowCount != 0)
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn tải tất cả không? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        Thread t = new Thread(t1);
                        t.Start();
                        t.Join();
                        MessageBox.Show("Đã tải xong");
                    }
                }
            }
            if(checkBtn2 == true)
            {
                if (dataGridView2.RowCount != 0)
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn tải tất cả không? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        Thread t = new Thread(t11);
                        t.Start();
                        t.Join();
                        MessageBox.Show("Đã tải xong");
                    }
                }
            }   
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(checkBtn1 == true)
            {
                if (dataGridView1.RowCount != 0)
                {
                    connectDb con = new connectDb();
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa file " + dataGridView1.CurrentRow.Cells[9].Value.ToString() + " không?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        con.deleteFile(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                        MessageBox.Show("Đã xóa file");
                    }
                    loadata();
                    loadata1();
                }
            }
            if(checkBtn2 == true)
            {
                if (dataGridView2.RowCount != 0)
                {
                    connectDb con = new connectDb();
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa file " + dataGridView1.CurrentRow.Cells[9].Value.ToString() + " không?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        con.deleteFile(Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value.ToString()));
                        MessageBox.Show("Đã xóa file");
                    }
                    loadata();
                    loadata1();
                }
            }
            
            
            
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if(checkBtn1 == true)
            {
                if (dataGridView1.RowCount != 0)
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa tất cả file không?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        Thread t = new Thread(t2);
                        t.Start();
                        t.Join();
                        MessageBox.Show("Đã xóa file");
                        loadata();
                        loadata1();
                    }
                }
            }
             if(checkBtn2 == true)
            {
                if (dataGridView2.RowCount != 0)
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa tất cả file không?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        Thread t = new Thread(t22);
                        t.Start();
                        t.Join();
                        MessageBox.Show("Đã xóa file");
                        loadata();
                        loadata1();
                    }
                }
            }        
        }
        public void t1()
        {
            
                connectDb con = new connectDb();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    con.downloadFileDtg1(Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value));
                }
            
        }
        public void t11()
        {

            connectDb con = new connectDb();
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                con.downloadFileDtg2(Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value));
            }

        }

        public void t2()
        {
            
                connectDb con = new connectDb();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    con.deleteFile(Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value));
                }
            
        }
        public void t22()
        {

            connectDb con = new connectDb();
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                con.deleteFile(Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value));
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            connectDb con = new connectDb();
            setValue();
            if(checkBtn1 == true)
            {
                con.dtgFilterForFile(dataGridView1, saveForSendFile.value, textBox1.Text,"guifile", null, userInfor.username, null);
            }
            else if (checkBtn2 == true)
            {
                con.dtgFilterForFile(dataGridView2, saveForSendFile.value, textBox1.Text, "guifile", userInfor.username, null, null);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }









        //public void setValue()
        //{
        //    if (comboBox1.SelectedIndex == 0)
        //    {
        //        ucQuanLyFilecs.value = "";
        //    }
        //    if (comboBox1.SelectedIndex == 1)
        //    {
        //        ucQuanLyFilecs.value = " DATEDIFF(day,ngaygui ,GETDATE()) = 0";
        //    }
        //    if (comboBox1.SelectedIndex == 2)
        //    {
        //        ucQuanLyFilecs.value = " DATEDIFF(day,ngaygui ,GETDATE()) = 1";
        //    }
        //    if (comboBox1.SelectedIndex == 3)
        //    {
        //        ucQuanLyFilecs.value = " DATEDIFF(day,ngaygui ,GETDATE()) = 2";
        //    }
        //    if (comboBox1.SelectedIndex == 4)
        //    {
        //        ucQuanLyFilecs.value = " (DATEDIFF(day,ngaygui ,GETDATE()) >= 3 and DATEDIFF(day,ngaygui ,GETDATE()) <= 5)";
        //    }
        //    if (comboBox1.SelectedIndex == 5)
        //    {
        //        ucQuanLyFilecs.value = " (DATEDIFF(day,ngaygui ,GETDATE()) >= 6 and DATEDIFF(day,ngaygui ,GETDATE()) <= 10)";
        //    }
        //    if (comboBox1.SelectedIndex == 6)
        //    {
        //        ucQuanLyFilecs.value = " (DATEDIFF(day,ngaygui ,GETDATE()) >= 11 and DATEDIFF(day,ngaygui ,GETDATE()) <= 15)";
        //    }
        //    if (comboBox1.SelectedIndex == 7)
        //    {
        //        ucQuanLyFilecs.value = "(DATEDIFF(day,ngaygui ,GETDATE()) >= 16 and DATEDIFF(day,ngaygui ,GETDATE()) <= 30)";
        //    }
        //    if (comboBox1.SelectedIndex == 8)
        //    {
        //        ucQuanLyFilecs.value = " (DATEDIFF(month,ngaygui ,GETDATE()) >= 1 and DATEDIFF(month,ngaygui ,GETDATE()) <= 2)";
        //    }
        //    if (comboBox1.SelectedIndex == 9)
        //    {
        //        ucQuanLyFilecs.value = " (DATEDIFF(month,ngaygui ,GETDATE()) >= 3 and DATEDIFF(month,ngaygui ,GETDATE()) <= 5)";
        //    }
        //    if (comboBox1.SelectedIndex == 10)
        //    {
        //        ucQuanLyFilecs.value = " (DATEDIFF(month,ngaygui ,GETDATE()) >= 6 and DATEDIFF(month,ngaygui ,GETDATE()) <=12)";
        //    }
        //    if (comboBox1.SelectedIndex == 11)
        //    {
        //        ucQuanLyFilecs.value = " DATEDIFF(Year,ngaygui ,GETDATE()) >1";
        //    }
        //}
    }
}
