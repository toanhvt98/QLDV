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
    public partial class formCreateAdminAcc : Form
    {
        public formCreateAdminAcc()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connectDb con = new connectDb();
            if(textBox1.Text != "")
            {
                if (textBox2.Text != "")
                {
                    con.createAdminAcc(textBox1.Text, textBox2.Text);
                    DialogResult dr = MessageBox.Show("Tạo thành công tài khoản: " + textBox1.Text + " với quyền Admin", "Bạn có muốn tiếp tục không?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                    MessageBox.Show("Mật khẩu không được dể trống!");
            }
            else
                MessageBox.Show("Tài khoản không được dể trống!");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
