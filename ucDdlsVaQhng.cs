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
    public partial class ucDdlsVaQhng : UserControl
    {
        public ucDdlsVaQhng()
        {
            InitializeComponent();
        }
        private static ucDdlsVaQhng _instance;
        public static ucDdlsVaQhng Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ucDdlsVaQhng();
                }
                return _instance;
            }
        }
        private void ucDdlsVaQhng_Load(object sender, EventArgs e)
        {

        }

    }
}
