using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TelkariSon
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        static string constring = ("Data Source=DESKTOP-NPG5I0O\\SQLEXPRESS;Initial Catalog=Teklarison;Integrated Security=True");
        SqlConnection baglanti = new SqlConnection(constring);

        public void kayitGetir()
        {
            string getir = "select*from tblMusteri";
            SqlCommand komut = new SqlCommand(getir, baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            baglanti.Close();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int i = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();

        }

        private void btnMusteriEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                    string ekle = "insert into tblMusteri(MusteriAdıSoyadı,MusteriTelefonu,IslemTarihi,Aldıkları,Odenegi)values(@MusteriAdıSoyadı,@MusteriTelefonu,@IslemTarihi,@Aldıkları,@Odenegi)";
                    SqlCommand komut = new SqlCommand(ekle, baglanti);
                    komut.Parameters.AddWithValue("@MusteriAdıSoyadı", textBox1.Text);
                    komut.Parameters.AddWithValue("@MusteriTelefonu", textBox2.Text);
                    komut.Parameters.AddWithValue("@IslemTarihi", dateTimePicker1.Value);
                    komut.Parameters.AddWithValue("@Aldıkları", textBox3.Text);
                    komut.Parameters.AddWithValue("@Odenegi", textBox4.Text);




                    komut.ExecuteNonQuery();

                    MessageBox.Show("kayıt eklendi");
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("bir hata var!" + hata.Message);
            }

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void btnGoruntule_Click(object sender, EventArgs e)
        {
            kayitGetir();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.ShowDialog();
        }
    }
}
