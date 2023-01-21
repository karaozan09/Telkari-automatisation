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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Configuration;

namespace TelkariSon
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        static string constring = ("Data Source=DESKTOP-NPG5I0O\\SQLEXPRESS;Initial Catalog=Teklarison;Integrated Security=True");
        SqlConnection baglanti=new SqlConnection(constring);
        
        public void kayitGetir()
        {
            string getir = "select*from tblSatis";
            SqlCommand komut = new SqlCommand(getir, baglanti);
            SqlDataAdapter adapter= new SqlDataAdapter(komut);
            DataTable table=new DataTable();    
            adapter.Fill(table);    
            dataGridView1.DataSource = table;   
            baglanti.Close();
        }
        //public void stokAzalt()
        //{
           
        //    for (int j = 0; j > dataGridView1.Rows.Count-1 ; j++)
        //    {
        //        baglanti.Open();
        //        string ekle = "insert into tblSatis(SatılanUrunler,Odenek,Kâr,MusteriAdiSoyadi,MusteriTelefon)values(@SatılanUrunler,@Odenek,@Kâr,@MusteriAdiSoyadi,@MusteriTelefon)";
        //        SqlCommand komut = new SqlCommand(ekle, baglanti);


        //       // komut.Parameters.AddWithValue("@UrunAdii", comboBox1.SelectedIndex);
        //        komut.Parameters.AddWithValue("@SatılanUrunler", dataGridView1.Rows[j].Cells["SatılanUrunler"].Value.ToString());
        //        komut.Parameters.AddWithValue("@Odenek", dataGridView1.Rows[j].Cells["Odenek"].Value.ToString());
        //        komut.Parameters.AddWithValue("@Kâr", dataGridView1.Rows[j].Cells["Kâr"].Value.ToString());
        //        komut.Parameters.AddWithValue("@MusteriAdiSoyadi", dataGridView1.Rows[j].Cells["MusteriAdiSoyadi"].Value.ToString());
        //        komut.Parameters.AddWithValue("@MusteriTelefon", dataGridView1.Rows[j].Cells["MusteriTelefon"].Value.ToString());

        //        komut.ExecuteNonQuery();

        //        SqlCommand komut2 = new SqlCommand("update tblStok set StokAdedi=StokAdedi-'" + dataGridView1.Rows[j].Cells["StokAdedi"].Value.ToString()+"where UrunModeli='" + dataGridView1.Rows[j].Cells["SatılanUrunler"].Value.ToString() + "'",baglanti);
        //        komut2.ExecuteNonQuery();
        //        baglanti.Close();


        //    }
        //}
        private void btnEkle_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                   
                    baglanti.Open();
                    string ekle = "insert into tblSatis(SatısTarihi,SatılanUrunler,Odenek,Kâr,MusteriAdiSoyadi,MusteriTelefon)values(@SatısTarihi,@SatılanUrunler," +
                        "@Odenek,@Kâr,@MusteriAdiSoyadi,@MusteriTelefon)";
                    SqlCommand komut = new SqlCommand(ekle, baglanti);
                   
                    komut.Parameters.AddWithValue("@SatısTarihi", dateTimePicker1.Value);
                    komut.Parameters.AddWithValue("@SatılanUrunler",textBox2.Text);
                    komut.Parameters.AddWithValue("@Odenek", Convert.ToDecimal(textBox3.Text));
                    komut.Parameters.AddWithValue("@Kâr", Convert.ToDecimal(textBox4.Text));
                    komut.Parameters.AddWithValue("@MusteriAdiSoyadi", textBox5.Text);
                    komut.Parameters.AddWithValue("@MusteriTelefon",Convert.ToInt64( textBox6.Text));
                   

                    komut.ExecuteNonQuery();

                    MessageBox.Show("kayıt eklendi");

                    //stokAzalt();

                }
            }

            
            catch (Exception hata)
            {
                MessageBox.Show("bir hata var!"+hata.Message);
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kayitGetir();
            
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2=new Form2();
            this.Hide();
            form2.ShowDialog();
            
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select UrunAdi from tblUrunler",baglanti);
            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();

            while (dr.Read())
            {
                comboBox1.Items.Add(dr["UrunAdi"]);
                

            }
            baglanti.Close();
        }
private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select UrunModeli from tblStok where UrunAdii=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", comboBox1.SelectedIndex + 1);
            SqlDataReader dr;
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0]);
            }
            baglanti.Close();



        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            comboBox2.Items.Clear();
            textBox2.Text = comboBox2.Text;
            baglanti.Open();

         //   SqlCommand komut0=new SqlCommand("select AlisFiyat")

            SqlCommand komut = new SqlCommand("select SatisFiyat ,AlisFiyat from tblStok where UrunModeli=@UrunModeli", baglanti);
            komut.Parameters.AddWithValue("@UrunModeli", comboBox2.Text);
            SqlDataReader dr;
            dr = komut.ExecuteReader();

            while (dr.Read())
            {
                decimal AlisFiyat = (decimal)dr["AlisFiyat"];
               decimal SatisFiyat= (decimal)dr["SatisFiyat"];
                textBox3.Text = SatisFiyat.ToString();

                decimal Kâr = SatisFiyat- AlisFiyat ;
                 textBox4.Text=Kâr.ToString()  ;    
            }
            baglanti.Close();

        }

        //private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    textBox2.Text = comboBox3.Text;
        //    fiyatGetir();
        //}

        //public void fiyatGetir()
        //{


           
        //}
       // String MusteriAdiSoyadi = " ";
        private void btnSil_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                int Satisid = Convert.ToInt32(drow.Cells[0].Value);
                satisSil(Satisid);
                kayitGetir();

                comboBox1.SelectedIndex = -1;
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();


                MessageBox.Show("ürün başarıyla silindi");
            }


        }
        public void satisSil(int Satisid)
        {
            SqlCommand komut = new SqlCommand("Delete  from tblSatis where Satisid=@Satisid", baglanti);
            baglanti.Open();
            komut.Parameters.AddWithValue("@Satisid", Satisid);
            komut.ExecuteNonQuery();
            kayitGetir();
            baglanti.Close();
            

        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from tblSatis where MusteriAdiSoyadi like '%" + textBox8.Text+"%'" ,baglanti); 
            SqlDataAdapter ada = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
            textBox8.Clear();


        }
       private int i;

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update tblSatis set SatılanUrunler=@SatılanUrunler,Odenek=@Odenek,Kâr=@Kâr,MusteriAdiSoyadi=@MusteriAdiSoyadi,MusteriTelefon=@MusteriTelefon where Satisid=@Satisid", baglanti);
            komut.Parameters.AddWithValue("@SatılanUrunler", textBox2.Text);
            komut.Parameters.AddWithValue("@Odenek", Convert.ToDecimal(textBox3.Text));
            komut.Parameters.AddWithValue("@Kâr", Convert.ToDecimal(textBox4.Text));
            komut.Parameters.AddWithValue("@MusteriAdiSoyadi", textBox5.Text);
            komut.Parameters.AddWithValue("@MusteriTelefon", textBox6.Text);
            komut.Parameters.AddWithValue("@Satisid", dataGridView1.Rows[i].Cells[0].Value);
            komut.ExecuteNonQuery();


            MessageBox.Show("ürün başarıyla Güncellendi");
            baglanti.Close();
            kayitGetir();
          


            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();


        }

       

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            i= e.RowIndex;
         // dateTimePicker1.Value= dataGridView1.Rows[i].Cells[1].Value;
            textBox2.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
        }

       

        //private void Hesapla()
        //{
        //    try
        //    {
        //        baglanti.Open();
        //        SqlCommand komut=new SqlCommand("")
        //    }
        //}



        //private void Form3_Load(object sender, EventArgs e)
        //{
        //    // TODO: Bu kod satırı 'teklarisonDataSet2.tblSatis' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
        //    this.tblSatisTableAdapter.Fill(this.teklarisonDataSet2.tblSatis);

        //}



    }
    }

