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
    public partial class formChonNguoiNhanFile : Form
    {
        public static bool taikhoan = true;
        public static bool chibo = false;
        public formChonNguoiNhanFile()
        {
            InitializeComponent();
        }

        private static formChonNguoiNhanFile _instance;
        public static formChonNguoiNhanFile Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new formChonNguoiNhanFile();
                }
                return _instance;
            }
        }

        private void formChonNguoiNhanFile_Load(object sender, EventArgs e)
        {
            formChonNguoiNhanFile.taikhoan = true;
            connectDb con = new connectDb();
            con.getallAccToRickTextBox(checkedListBox1);
            checkedListBox1.CheckOnClick = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            connectDb con = new connectDb();
            checkedListBox1.Items.Clear();
            con.getallAccToRickTextBox(checkedListBox1);
            checkedListBox1.CheckOnClick = true;
            formChonNguoiNhanFile.taikhoan = true;
            formChonNguoiNhanFile.chibo = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connectDb con = new connectDb();
            checkedListBox1.Items.Clear();
            con.getallCBToRickTextBox(checkedListBox1);
            checkedListBox1.CheckOnClick = true;
            formChonNguoiNhanFile.taikhoan = false;
            formChonNguoiNhanFile.chibo = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (formChonNguoiNhanFile.taikhoan == true)
            {
                SendFile.Instance.label5.Text = checkedListBox1.CheckedItems.Count.ToString() + " người";
                SendFile.Instance.comboBox1.Items.Clear();
                foreach (string s in checkedListBox1.CheckedItems)
                {
                    SendFile.Instance.comboBox1.Items.Add(s);
                }
            }
            if (formChonNguoiNhanFile.chibo == true)
            {
                SendFile.Instance.comboBox1.Items.Clear();
                SendFile.Instance.label5.Text = checkedListBox1.CheckedItems.Count.ToString() + " chi bộ";
                foreach (string s in checkedListBox1.CheckedItems)
                {
                    SendFile.Instance.comboBox1.Items.Add(s);
                }
            }
            this.Close();
        }

        private void formChonNguoiNhanFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach(Control c in this.Controls)
            {
                if(c is CheckedListBox)
                {
                    CheckedListBox cb = (CheckedListBox)c;
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        checkedListBox1.SetItemChecked(i, false);
                    }
                }
            }
        }
    }
}
