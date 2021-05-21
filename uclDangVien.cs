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
            button4.Visible = false;
            button5.Visible = false;
            pictureBox1.Image = pictureBox1.InitialImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"; ;
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                pictureBox1.BackgroundImage = BackgroundImage;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = BackgroundImage;
            pictureBox1.Image = pictureBox1.InitialImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text != "")
            {
                if (textBox2.Text != "")
                {
                    connectDb con = new connectDb();
                    if (con.checkSllVaSt("solylich",textBox1.Text))
                    {
                        if (con.checkSllVaSt("sothe",textBox2.Text))
                        {
                            setInfor();
                            formThemVaThongTinDangVien ftvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
                            if (ftvttdv == null)
                            {
                                
                                ftvttdv = new formThemVaThongTinDangVien();                          
                                ftvttdv.AutoScroll = true;
                            }
                            ftvttdv.ShowDialog();
                            textBox1.Text = "";
                            textBox2.Text = "";
                            pictureBox1.Image = pictureBox1.InitialImage;
                            pictureBox1.BackgroundImage = BackgroundImage;
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

        private void setInfor()
        {
            dangvien.solylich = textBox1.Text;
            dangvien.sothedangvien = textBox2.Text;
            byte[] anh;
            ImageConverter converter = new ImageConverter();
            anh = (byte[])converter.ConvertTo(pictureBox1.Image, typeof(byte[]));
            dangvien.anh = anh;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            formMain fm = (formMain)Application.OpenForms["formMain"];
            UCThongTin uc = new UCThongTin();
            usercontrolForm.showcontrol(uc, fm.panel1);
        }
        

        private void button4_Click(object sender, EventArgs e)
        {
            

            formThemVaThongTinDangVien ftvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
            if (ftvttdv == null)
            {

                ftvttdv = new formThemVaThongTinDangVien();
                ftvttdv.AutoScroll = true;
                formThemVaThongTinDangVien.check = true;
                getinfor();
                ucThongTin1.check = true;
                
                


                ucThongTin3.check = true;
                ucThongTin4.check = true;
                ucThongTin6.check = true;
               
            }
            ftvttdv.ShowDialog();
        }

        private void getinfor()
        {
            dangvien.solylich = textBox1.Text;
            dangvien.sothedangvien = textBox2.Text;
            byte[] anh;
            ImageConverter converter = new ImageConverter();
            anh = (byte[])converter.ConvertTo(pictureBox1.Image, typeof(byte[]));
            dangvien.anh = anh;
        }
    }
}
