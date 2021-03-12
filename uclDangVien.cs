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
    public partial class uclDangVien : UserControl
    {
        public uclDangVien()
        {
            InitializeComponent();
        }

        private static uclDangVien _instance;
        
        public static uclDangVien Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new uclDangVien();
                }
                return _instance;
            }
        }

        private void uclDangVien_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"; ;
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                pictureBox1.BackgroundImage = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (textBox2.Text != "")
                {
                    connectDb con = new connectDb();
                    if (con.checkSllVaSt("solylich"))
                    {
                        if (con.checkSllVaSt("sothe"))
                        {
                            Image img = pictureBox1.Image;
                            byte[] arrImg;
                            ImageConverter converter = new ImageConverter();
                            arrImg = (byte[])converter.ConvertTo(img, typeof(byte[]));
                            con.addSllVaSt(arrImg, textBox1.Text, textBox2.Text);
                            formThemVaThongTinDangVien ftvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
                            if (ftvttdv == null)
                            {
                                ftvttdv = new formThemVaThongTinDangVien();
                                ftvttdv.AutoScroll = true;
                            }
                            ftvttdv.ShowDialog();
                        }
                        else
                            MessageBox.Show("Mã số thẻ Đảng viên: " + textBox2.Text + " đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Mã số lý lịch Đảng viên: " + textBox1.Text + " đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Mã số thẻ Đảng viên không được bỏ trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Mã số lý lịch không được bỏ trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


    }
}
