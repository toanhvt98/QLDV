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
    public partial class ucTaiKhoanCaNhan : UserControl
    {
        public ucTaiKhoanCaNhan()
        {
            InitializeComponent();
        }
        bool chiboAdmin = false;
        private void ucTaiKhoanCaNhan_Load(object sender, EventArgs e)
        {
            connectDb con = new connectDb();
            textBox1.Text = userInfor.username;
            textBox2.Text = userInfor.pwd;
            con.getNameChiBoOfAcc(userInfor.username,textBox3);
            con.getQuyen(userInfor.username,textBox4);
            textBox2.UseSystemPasswordChar = true;
            button2.Visible = false;
            button3.BackgroundImageLayout = ImageLayout.Stretch;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            if(textBox3.Text == "")
            {
                chiboAdmin = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(button1.Text == "Đổi mật khẩu")
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox1.UseSystemPasswordChar = true;
                textBox2.UseSystemPasswordChar = true;
                textBox3.UseSystemPasswordChar = true;
                label2.Text = "Mật khẩu cũ:";
                label3.Text = "Mật khẩu mới:";
                label4.Text = "Nhập lại mật khẩu mới:";
                button1.Text = "Xác nhận";
                button2.Visible = true;
                button3.Visible = false;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
            else if(button1.Text == "Xác nhận")
            {
                               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connectDb con = new connectDb();
            textBox1.Text = userInfor.username;
            textBox2.Text = userInfor.pwd;
            if(chiboAdmin == true)
            {
                textBox3.Text = "";
            }
            else
            {
                con.getNameChiBoOfAcc(userInfor.username, textBox3);
            }
            
            textBox2.UseSystemPasswordChar = true;
            textBox1.UseSystemPasswordChar = false;
            textBox3.UseSystemPasswordChar = false;
            label2.Text = "Tên tài khoản:";
            label3.Text = "Mật khẩu:";
            label4.Text = "Chi bộ:";
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            button2.Visible = false;
            button1.Text = "Đổi mật khẩu";
            button3.Visible = true;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(button1.Text == "Xác nhận")
            { 
                if(textBox1.Text != "")
                {
                    button4.Visible = true;
                }
                else
                    button4.Visible = false;
            }
            else
                button4.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (button1.Text == "Xác nhận")
            {
                if (textBox2.Text != "")
                {
                    button5.Visible = true;
                }
                else
                    button5.Visible = false;
            }
            else
                button5.Visible = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (button1.Text == "Xác nhận")
            {
                if (textBox3.Text != "")
                {
                    button6.Visible = true;
                }
                else
                    button6.Visible = false;
            }
            else
                button6.Visible = false;
        }
    }
}
