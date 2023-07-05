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
namespace WordÇalışma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sözlükDataSet.Words' table. You can move, or remove it, as needed.
            //this.wordsTableAdapter.Fill(this.sözlükDataSet.Words);
            Button1_Click(sender,e);
        }
        SqlConnection baglanti = new SqlConnection("Data Source = DESKTOP-070QPQR; Initial Catalog = Sözlük; User ID = sa; Password=63405EMre");

        private void Button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from Words",baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kayitekle = new SqlCommand("Declare @ID int Select @ID = Max(WordId) + 1 from Words insert into Words (WordId,Eng_Write,Eng_Read,Turkish_Mean) values  (@ID,@e1,@e2,@t1)",baglanti);
            kayitekle.Parameters.AddWithValue("@e1",textBox2.Text);
            kayitekle.Parameters.AddWithValue("@e2", textBox3.Text);
            kayitekle.Parameters.AddWithValue("@t1", textBox4.Text);
            try
            {
                if (kayitekle.ExecuteNonQuery() > 0)
                    MessageBox.Show("Kayıt Edildi");
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            baglanti.Close();
           
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kayitsil = new SqlCommand("Delete from Words where Eng_Write=@adi", baglanti);
            kayitsil.Parameters.AddWithValue("@adi", textBox2.Text);
            kayitsil.ExecuteNonQuery();
            baglanti.Close();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox1.Clear();

        }
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            string numara = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            string yazılış = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            string okunuş = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            string anlam = dataGridView1.Rows[secilen].Cells[3].Value.ToString();

            textBox2.Text = yazılış;
            textBox3.Text = okunuş;
            textBox4.Text = anlam;
            textBox1.Text = numara;
            
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            
            baglanti.Open();
            SqlCommand komutgüncel = new SqlCommand(" Update Words set WordId=@p4, Eng_Write=@p1,Eng_Read=@p2,Turkish_Mean=@p3 where WordId=@p4", baglanti);
            komutgüncel.Parameters.AddWithValue("@p1",textBox2.Text);
            komutgüncel.Parameters.AddWithValue("@p2", textBox3.Text);
            komutgüncel.Parameters.AddWithValue("@p3", textBox4.Text);
            komutgüncel.Parameters.AddWithValue("@p4", textBox1.Text);
            komutgüncel.ExecuteNonQuery();
            baglanti.Close();
        
    }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
