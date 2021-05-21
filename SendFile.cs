using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace QLDV
{
    public partial class SendFile : UserControl
    {
        public SendFile()
        {
            InitializeComponent();
        }
        
        private static SendFile _instance;
        public static SendFile Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SendFile();
                }
                return _instance;
            }
        }
        private void SendFile_Load(object sender, EventArgs e)
        {
            linkLabel1.Text = string.Empty;
            button2.Enabled = false;
            label5.Text = "";
            comboBox1.Visible = false;
            connectDb con = new connectDb();
            if (con.getQuyen4File(userInfor.username) == "admin")
            {
                button3.Visible = true;
            }
            else
            {
                button3.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            linkLabel1.Text = string.Empty;

                button2.Enabled = false;
        }
        string filename = "";
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = @"All Files|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {              
                linkLabel1.Text = Path.GetFileName(openFileDialog1.FileName);
                filename = openFileDialog1.FileName;
            }
            if(linkLabel1.Text != string.Empty)
            {
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            
            if (textBox1.Text != string.Empty)
            {
                if (linkLabel1.Text != string.Empty)
                {
                    connectDb con = new connectDb();
                    saveForSendFile.ngaygui = DateTime.Now;
                    saveForSendFile.noidung = SendFile.Instance.richTextBox1.Text;
                    saveForSendFile.chibotkgui = con.getNameChiBoOfAcc1(userInfor.username);
                    saveForSendFile.quyennguoigui = con.getQuyen4File(userInfor.username);
                    if (con.getQuyen4File(userInfor.username) == "admin")
                    {
                        if (formChonNguoiNhanFile.taikhoan == true)
                        {
                            Thread t = new Thread(t1);
                            t.Start();
                            t.Join();
                            MessageBox.Show("Đã gửi");
                            ucQuanLyFilecs.Instance.loadata();
                            
                        }
                        else if (formChonNguoiNhanFile.chibo == true)
                        {
                            Thread t = new Thread(t2);
                            t.Start();
                            t.Join();
                            MessageBox.Show("Đã gửi");
                            ucQuanLyFilecs.Instance.loadata();
                            
                        }
                    }
                    else if (con.getQuyen4File(userInfor.username) != "admin")
                    {
                        Stream st = File.OpenRead(filename);
                        byte[] f = new byte[st.Length];
                        st.Read(f, 0, f.Length);
                        string tieude = textBox1.Text;
                        string noidung = saveForSendFile.noidung;

                        string tenfile = linkLabel1.Text;
                                  
                    }
                    ((Form)this.TopLevelControl).Close();
                }

            }
            ucQuanLyFilecs.Instance.loadata();ucQuanLyFilecs.Instance.loadata1();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formChonNguoiNhanFile f = (formChonNguoiNhanFile)Application.OpenForms["formChonNguoiNhanFile"];
            if(f == null)
            {
                f = new formChonNguoiNhanFile();
            }
            f.Show();
        }
        public  void t1()
        {
            connectDb con = new connectDb();
            Stream st = File.OpenRead(filename);
            byte[] f = new byte[st.Length];
            st.Read(f, 0, f.Length);
            string tieude = textBox1.Text;
            string noidung = saveForSendFile.noidung;
            
            string tenfile = linkLabel1.Text;
            foreach (string s in comboBox1.Items)
            {
                
                string quyennguoinhan = con.getQuyen4File(s);
                string nguoinhan = s;
                string chibo = con.getNameChiBoOfAcc1(s);
                
                con.sendfile(userInfor.username, chibo, saveForSendFile.quyennguoigui, saveForSendFile.ngaygui, quyennguoinhan, nguoinhan, tieude, noidung, f, tenfile,saveForSendFile.chibotkgui);
            }
        }
        public void t2()
        {
            connectDb con = new connectDb();
            Stream st = File.OpenRead(filename);
            byte[] f = new byte[st.Length];
            st.Read(f, 0, f.Length);
            string tieude = textBox1.Text;
            string noidung = saveForSendFile.noidung;

            string tenfile = linkLabel1.Text;
            foreach (string s in comboBox1.Items)
            {
                foreach(string i in con.getaccOfChiBo(s))
                {
                    
                    string quyennguoinhan = con.getQuyen4File(i);
                    string nguoinhan = i;
                    string chibo = s;
                    
                    con.sendfile(userInfor.username, chibo, saveForSendFile.quyennguoigui, saveForSendFile.ngaygui, quyennguoinhan, nguoinhan, tieude, noidung, f, tenfile,saveForSendFile.chibotkgui);
                    
                }
            }
        }
    }
}
