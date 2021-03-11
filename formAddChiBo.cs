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
    public partial class formAddChiBo : Form
    {
        private readonly ucQLCB qlcb;
        public formAddChiBo(ucQLCB qlcb1)
        {
            InitializeComponent();
            qlcb = qlcb1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connectDb con = new connectDb();
            if(textBox1.Text != "")
            {
                if(textBox2.Text != "")
                {
                    if (con.checkChiBoAlready("ma", textBox1.Text))
                    {
                        if (con.checkChiBoAlready("ten", textBox2.Text))
                        {
                            con.addChiBo(textBox1.Text, textBox2.Text);
                            DialogResult dr = MessageBox.Show("Bạn đã thêm thành công tên chi bộ: " + textBox2.Text + ". Bạn có muốn tiếp tục thêm chi bộ không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr == DialogResult.Yes)
                            {
                                textBox1.Text = "";
                                textBox2.Text = "";
                            }
                            else
                                this.Close();
                        }
                        else
                            MessageBox.Show("Đã tồn tại chi bộ có tên: "+textBox2.Text,"Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Đã tồn tại chi bộ có mã: " + textBox1.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Tên chi bộ không được bỏ trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Mã chi bộ không được bỏ trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            qlcb.loaddata();
        }
    }
}
