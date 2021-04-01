using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDV
{
    class usercontrolForm
    {
        public static void showcontrol(Control c, Control c1)
        {
            c1.Controls.Clear();
            c.Dock = DockStyle.Fill;
            c.BringToFront();
            c.Focus();
            c1.Controls.Add(c);
        }



        public static void clearcontrol(Control c, Control c1)
        {

            c.Controls.Remove(c1);
            c.Dispose();
        }
    }
}
