using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace TelkariSon
{
    public partial class Form1 : Form


    {
        SqlConnection baglanti=new SqlConnection("Data Source=DESKTOP-NPG5I0O\\SQLEXPRESS;Initial Catalog=Teklarison;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }
        bool isThere;
        private void btnGiris_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            string Password=textBox2.Text;
          
             baglanti.Open();
            SqlCommand cmd = new SqlCommand("select*from tblGiris ",baglanti);
            SqlDataReader dr=cmd.ExecuteReader();

            while (dr.Read())
            {
                if (userName == dr["UserName"].ToString() && Password == dr["Password"].ToString())
                {
                    isThere = true;
                    break;
                }
                else
                {
                   isThere=false;   
                }
            }
            if (isThere)
            {
                MessageBox.Show("Giriş Başarılı");

                Form2 form2 = new Form2();
                this.Hide();
                form2.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("Giriş Yapılamadı");
                
            }

            textBox1.Clear();
            textBox2.Clear();

            baglanti.Close();   
        }

       

        
    }
}
