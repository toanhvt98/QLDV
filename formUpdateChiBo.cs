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
    public partial class formUpdateChiBo : Form
    {
        private static ucQLCB qlcb;
        public int id { get; set; }
        public formUpdateChiBo(ucQLCB qlcb1)
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
            if (textBox2.Text != qlcb.dataGridView1.CurrentRow.Cells[2].Value.ToString())
            {
                if (con.checkChiBoAlready("ten", textBox2.Text))
                {
                    con.updateChiBo(id, textBox2.Text);
                    MessageBox.Show("Cập nhật thành công chi bộ có mã: " + textBox1.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Đã tồn tại chi bộ có tên: " + textBox2.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                this.Close();
            qlcb.loaddata();
            this.Close();
        }

    }     
}
