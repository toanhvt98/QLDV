using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDV
{
    public partial class formSenFile : Form
    {
        public formSenFile()
        {
            InitializeComponent();
        }
        private static formSenFile _instance;
        public static formSenFile Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new formSenFile();
                }
                return _instance;
            }
        }
        private void SendFile_Load(object sender, EventArgs e)
        {
            SendFile.Instance.Dock = DockStyle.Fill;
            SendFile.Instance.BringToFront();
            panel1.Controls.Add(SendFile.Instance);
        }

        private void formSenFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach(Control c in panel1.Controls)
            {
                foreach(Control c1 in c.Controls)
                {
                    if(c1 is TextBox)
                    {
                        TextBox t = (TextBox)c1;
                        t.Text = "";
                    }
                    if (c1 is RichTextBox)
                    {
                        RichTextBox t = (RichTextBox)c1;
                        t.Text = "";
                    }
                }
            }
        }
    }
}
