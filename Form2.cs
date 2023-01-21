using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelkariSon
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
            
        }

        private void btnSatiş_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(); 
            this.Hide();
            form3.ShowDialog(); 
        }

        private void btnStok_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();  
            this.Hide();
            form4.ShowDialog(); 
        }

       
    }
}
