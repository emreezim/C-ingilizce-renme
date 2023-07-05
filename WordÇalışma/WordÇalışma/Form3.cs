using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordÇalışma
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

       

        private void Button1_Click(object sender, EventArgs e)
        {
            Form frmname = new Form1();
            frmname.ShowDialog();
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            Form frmname2 = new Form2();
            frmname2.ShowDialog();
        }

        

       

        private void Button3_Click(object sender, EventArgs e)
        {
            Form frmname4 = new Form4();
            frmname4.ShowDialog();
        }
    }
}
