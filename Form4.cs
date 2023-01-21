using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace TelkariSon
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        static string constring = ("Data Source=DESKTOP-NPG5I0O\\SQLEXPRESS;Initial Catalog=Teklarison;Integrated Security=True");
        SqlConnection baglanti = new SqlConnection(constring);

        public void kayitGoruntule()
        {
            string getir = "select*from tblStok";
            SqlCommand komut = new SqlCommand(getir, baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            baglanti.Close();
        }

        private void btnKayit_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                    string ekle = "insert into tblStok(UrunAdii,UrunModeli,AlisFiyat,SatisFiyat,IslemTarihi,StokAdedi)values(@UrunAdii,@UrunModeli,@AlisFiyat,@SatisFiyat,@IslemTarihi,@StokAdedi)";
                    SqlCommand komut = new SqlCommand(ekle, baglanti);

                     
                   komut.Parameters.AddWithValue("@UrunAdii", comboBox1.SelectedIndex);
                    komut.Parameters.AddWithValue("@UrunModeli", textBox3.Text);
                    komut.Parameters.AddWithValue("@AlisFiyat", textBox4.Text);
                    komut.Parameters.AddWithValue("@SatisFiyat", textBox5.Text);
                    komut.Parameters.AddWithValue("@IslemTarihi", dateTimePicker1.Value);
                    komut.Parameters.AddWithValue("@StokAdedi", textBox7.Text);

                    komut.ExecuteNonQuery();

                    MessageBox.Show("kayıt eklendi");
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("bir hata var!" + hata.Message);
            }
          
            comboBox1.SelectedIndex = -1;
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox7.Clear();   

        }

        private void btnGoruntule_Click(object sender, EventArgs e)
        {
            kayitGoruntule();
        }
        int id = 0;
        

        private void btnSil_Click(object sender, EventArgs e)
        {foreach(DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                int id = Convert.ToInt32(drow.Cells[0].Value);
                urunSil(id);
                kayitGoruntule();

                comboBox1.SelectedIndex = -1;
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox7.Clear();

                MessageBox.Show("ürün başarıyla silindi");
            } 
            
        }

        public void urunSil(int Stokid)
        {
           
            SqlCommand komut = new SqlCommand("Delete  from tblStok where Stokid=@Stokid", baglanti);
            baglanti.Open();
            komut.Parameters.AddWithValue("@Stokid", Stokid);
            komut.ExecuteNonQuery();
            kayitGoruntule();
            baglanti.Close();
        

        }


        private void btnGeri_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.ShowDialog();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from tblStok where UrunModeli like '%" + textBox6.Text + "%'", baglanti);
            SqlDataAdapter ada = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
            textBox6.Clear();
        }
        private int i;
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
           /* string güncelle =*/ 
            SqlCommand komut = new SqlCommand("Update tblStok set UrunAdii=@UrunAdii, UrunModeli=@UrunModeli,AlisFiyat=@AlisFiyat,SatisFiyat=@SatisFiyat,StokAdedi=@StokAdedi where Stokid=@Stokid", baglanti);
          komut.Parameters.AddWithValue("@UrunAdii",SqlDbType.VarChar).Value=comboBox1.SelectedIndex;
            komut.Parameters.AddWithValue("@UrunModeli", SqlDbType.VarChar).Value= textBox3.Text;
            komut.Parameters.AddWithValue("@AlisFiyat", Convert.ToDecimal(textBox4.Text));
            komut.Parameters.AddWithValue("@SatisFiyat", Convert.ToDecimal(textBox5.Text));
            komut.Parameters.AddWithValue("@StokAdedi", SqlDbType.VarChar).Value=textBox7.Text;
            komut.Parameters.AddWithValue("@Stokid", dataGridView1.Rows[i].Cells[0].Value);


            komut.ExecuteNonQuery();
            
            MessageBox.Show("Kayıtlar başarıyla güncellendi");
            baglanti.Close();
            kayitGoruntule();

            comboBox1.SelectedIndex = -1;
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();   
            textBox7.Clear();   

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            i = e.RowIndex;
            //textBox1.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
           // comboBox1.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textBox3.Text= dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox4.Text= dataGridView1.Rows[i].Cells[3].Value.ToString();
            textBox5.Text= dataGridView1.Rows[i].Cells[4].Value.ToString();
            textBox7.Text= dataGridView1.Rows[i].Cells[6].Value.ToString();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select UrunAdi from tblUrunler", baglanti);
            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();

            while (dr.Read())
            {
                comboBox1.Items.Add(dr["UrunAdi"]);


            }
            baglanti.Close();
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand ekle = new SqlCommand("insert into tblUrunler (UrunAdi)values(@UrunAdi)", baglanti);
            ekle.Parameters.AddWithValue("@UrunAdi", textBox1.Text);
            comboBox1.Items.Add(textBox1.Text);
            ekle.ExecuteNonQuery();
            MessageBox.Show("Yeni Kategori Eklendi");
            baglanti.Close();
        }

      
    }
}
