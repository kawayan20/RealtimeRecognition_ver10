using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace accelview_classes
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            label1.ForeColor = Color.YellowGreen;
            label2.ForeColor = Color.DarkTurquoise;
            label3.ForeColor = Color.Orange;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            pictureBox.Invalidate();
        }
    }
}
