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

    public partial class Form2 : Form
    {

        public Form1 Form1Obj; 

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void closebutton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            pictureBox3.Invalidate();
            pictureBox4.Invalidate();
        }
    }
}
