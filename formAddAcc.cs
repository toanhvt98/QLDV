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
    public partial class formAddAcc : Form
    {
        private readonly ucQLTK qltk;
        public formAddAcc(ucQLTK qltk1)
        {
            InitializeComponent();
            qltk = qltk1;
        }

        

        private void formAddAcc_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'chiboDataSet.chibo' table. You can move, or remove it, as needed.
            this.chiboTableAdapter.Fill(this.chiboDataSet.chibo);
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            connectDb con = new connectDb();
            string macb = comboBox1.SelectedValue.ToString();
            if(textBox1.Text != "")
            {
                if (textBox2.Text != "")
                {
                    if (con.checkAccAlready(textBox1.Text))
                    {
                        con.addAcc(textBox1.Text, textBox2.Text, macb, "user");
                        DialogResult dr = MessageBox.Show("Thêm tài khoản: " + textBox1.Text + " thành công. Bạn có muốn thêm tiếp không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            textBox1.Text = "";
                            textBox2.Text = "";
                            comboBox1.SelectedIndex = 0;
                        }
                        else
                            this.Close();
                    }
                    else
                        MessageBox.Show("Đã tồn tại tài khoản: " + textBox1.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Mật khẩu không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Tài khoản không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            qltk.loaddata();
        }


    }
}
