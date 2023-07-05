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
    public partial class Form2 : Form
    {
        DataSet ds = new DataSet();
        int sayac = 0;
        
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source = DESKTOP-070QPQR; Initial Catalog = Sözlük; User ID = sa; Password=63405EMre");

        private void Button1_Click(object sender, EventArgs e) // Onaylama için
        {
            // 1. Türkish Mean kutusu açılsın
            // 2. ComboBox text değeri ile Türkish Mean kutusu eşleşiyormu Confirm buttonu ile olacak 
            // 3. Eğer doğru ise true box işaretlenecek değilse false işaretlenecek
            // Kaydet prosedürü çalıştırılacak.


            SaveData();


            
            baglanti.Close();
        }

        private void Button2_Click(object sender, EventArgs e) // Kayıt getir
        {
            BringData();
        }

        private void Form2_Load(object sender, EventArgs e) // İlk ekran açılırken Kayıt getir.
        {
            BringData();
        }

        

        private void BringData()
        {
            //checkBox1.Checked = true;
            SqlCommand cmd = new SqlCommand("SELECT * FROM Words WHERE WordId = @ID " +
                                            "SELECT Coalesce(MAX(SessionId),0) as SessionId FROM Test WHERE Date=@Date "+//, baglanti) ;
                                            "EXEC BringVerb 1", baglanti);
            sayac = sayac + 1;
            cmd.Parameters.AddWithValue("@ID", sayac);
            cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(DateTime.Now.ToShortDateString()));

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ds.Clear();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                textBox1.Text = ds.Tables[0].Rows[0]["Eng_Write"].ToString();
                //textBox2.Text = ds.Tables[0].Rows[0]["Turkish_Mean"].ToString();
                textBox3.Text = ds.Tables[0].Rows[0]["Eng_Read"].ToString();
                if (Convert.ToInt32(ds.Tables[1].Rows[0]["SessionId"]) > 0)
                    textBox4.Text = (Convert.ToInt32(ds.Tables[1].Rows[0]["SessionId"]) + 1).ToString();
                else
                    textBox4.Text = "1";

                // ComboBox Dolduruluyor...
                comboBox1.Items.Clear();
                Random random = new Random();
                int RndNumber = random.Next(4 + 1, 10);
                //int RndNumber = random.Next(Convert.ToInt32(ds.Tables[3].Rows[0]["FMin"]) + 1, Convert.ToInt32(ds.Tables[3].Rows[0]["FMax"]));

                int PreWordId, NowWordId;
                //comboBox1.Items.Add(ds.Tables[0].Rows[0]["Turkish_Mean"].ToString());
                for (int i = 0; i < (ds.Tables[2].Rows.Count); i++)
                {
                    if (i == 0) PreWordId = Convert.ToInt32(ds.Tables[2].Rows[0]["WordId"]);
                    else PreWordId = Convert.ToInt32(ds.Tables[2].Rows[i - 1]["WordId"]);
                    NowWordId = Convert.ToInt32(ds.Tables[2].Rows[i]["WordId"]);

                    if (RndNumber >= PreWordId && RndNumber <= NowWordId)
                    {
                        if (RndNumber != NowWordId)
                            comboBox1.Items.Add(ds.Tables[0].Rows[0]["Turkish_Mean"].ToString());

                        comboBox1.Items.Add(ds.Tables[2].Rows[i]["Turkish_Mean"].ToString());
                    }
                    else
                    {
                        comboBox1.Items.Add(ds.Tables[2].Rows[i]["Turkish_Mean"].ToString());
                    }
                }
            }
            else
            {
                sayac = 0;
            }
            comboBox1.Text = "";
            textBox2.Text = "";
        } //Verilerin getirlmesini sağlıyor.
        private void SaveData()
        {
            textBox2.Text = ds.Tables[0].Rows[0]["Turkish_Mean"].ToString();

            if (comboBox1.Text == textBox2.Text)
            {
                checkBox1.Checked = true;
                checkBox2.Checked = false;
            }
            else
            {
                checkBox2.Checked = true;
                checkBox1.Checked = false;
            }

            baglanti.Open();
            SqlCommand kayitek = new SqlCommand("insert into Test (WordId,Answer,Date,SessionID) values (@WordId,@Answer,@Date,@ID)", baglanti);
            kayitek.Parameters.AddWithValue("@ID", textBox4.Text);
            kayitek.Parameters.AddWithValue("@WordId", Convert.ToInt32(ds.Tables[0].Rows[0]["WordId"]).ToString());
            kayitek.Parameters.AddWithValue("@Date", Convert.ToDateTime(DateTime.Now.ToShortDateString()));
            if (checkBox1.Checked == true)
                kayitek.Parameters.AddWithValue("@Answer", "D");
            else if (checkBox2.Checked == true)
                kayitek.Parameters.AddWithValue("@Answer", "Y");

            try
            {
                if (kayitek.ExecuteNonQuery() > 0)
                    MessageBox.Show("Kayıt Edildi");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



            baglanti.Close();
        }
    }
}
