using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Data.OleDb;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace OtelRezerved_Network
{

    public partial class giris_kayit_Form : Form
    {


        public giris_kayit_Form()
        {
            InitializeComponent();


        }

        public string EmailText
        {
            get { return giris_email_textBox.Text; }
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb");
        OleDbCommand komut = new OleDbCommand();

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // tabPage2'den tabPage1'ye geçiş yapar
            giriskayit_tabControl.SelectedTab = giris_tabPage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // tabPage1'den tabPage2'ye geçiş yapar
            giriskayit_tabControl.SelectedTab = kayit_tabPage;
        }





        private void button3_Click(object sender, EventArgs e)
        {


            string email = giris_email_textBox.Text;
            string sifre = giris_sifre_textBox.Text;

            using (OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb"))
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("SELECT * FROM musteri WHERE mail = @Email", baglanti);
                komut.Parameters.AddWithValue("@Email", email);
                OleDbDataReader dr = komut.ExecuteReader();

                // Belirli bir e-posta ve şifre ile giriş yapılıyorsa
                if (giris_email_textBox.Text == "xx" && giris_sifre_textBox.Text == "xx")
                {
                    // Onay mesajını göster
                    DialogResult onay = MessageBox.Show("Yönetici hesabına girmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo);
                    if (onay == DialogResult.Yes)
                    {
                        // YoneticiMerkezForm'un bir örneğini oluşturun
                        yonetici_merkez_Form yoneticiMerkezForm = new yonetici_merkez_Form();

                        // YoneticiMerkezForm'u gösterin
                        yoneticiMerkezForm.Show();

                        // İsteğe bağlı olarak, mevcut formu gizleyebilirsiniz
                        this.Hide();
                    }
                }
                else if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sifre))
                {
                    MessageBox.Show("Gerekli alanları doldurun");
                }
                else if (!dr.HasRows)
                {
                    MessageBox.Show("Böyle bir kullanıcı yok");
                }
                else
                {
                    dr.Read();
                    if (sifre != dr["sifre"].ToString())
                    {
                        MessageBox.Show("Şifrenizi kontrol edin");
                    }
                    else
                    {
                        // uygulama_ana_Form formunu açın
                        uygulama_ana_Form anaForm = new uygulama_ana_Form();
                        anaForm.Show();
                        this.Hide();

                        // Müşterinin adını ve soyadını al
                        string adsoyad = dr["adsoyad"].ToString();

                        // Ana formdaki label'a müşterinin adını ve soyadını yaz
                        anaForm.uyg_merhaba_label.Text = "MERHABA " + adsoyad + "! DİLEDİĞİNİZ REZERVASYONU YAPABİLİRSİNİZ!";
                    }
                }
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            string email = kayit_email_textBox.Text;
            string sifre = kayit_sifre_textBox.Text;
            string sifreOnay = kayit_sifreonay_textBox.Text;
            string adsoyad = kayit_adsoyad_textBox.Text;
            string telno = kayit_telno_textBox.Text;
            string konum = kayit_konum_comboBox.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sifre) || string.IsNullOrEmpty(sifreOnay) || string.IsNullOrEmpty(adsoyad) || string.IsNullOrEmpty(telno) || string.IsNullOrEmpty(konum))
            {
                MessageBox.Show("Gerekli alanları doldurun!");
            }
            else if (sifre != sifreOnay)
            {
                MessageBox.Show("Şifre Onayını Kontrol Edin!");
            }
            else
            {
                using (OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb"))
                {
                    baglanti.Open();

                    OleDbCommand kontrolKomut = new OleDbCommand("SELECT * FROM musteri WHERE mail = @Email OR telno = @Telno", baglanti);
                    kontrolKomut.Parameters.AddWithValue("@Email", email);
                    kontrolKomut.Parameters.AddWithValue("@Telno", telno);
                    OleDbDataReader dr = kontrolKomut.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();
                        if (email == dr["mail"].ToString())
                        {
                            MessageBox.Show("E-mail zaten mevcut");
                        }
                        else if (telno == dr["telno"].ToString())
                        {
                            MessageBox.Show("Telefon numarası zaten mevcut");
                        }
                        else if (email == "xx")
                        {
                            MessageBox.Show("Bu Mail'i kulanamazsınız!");
                        }
                    }
                    else
                    {
                        OleDbCommand komut = new OleDbCommand("INSERT into musteri (mail, adsoyad, sifre, telno, konum) values (@Email, @AdSoyad, @Sifre, @Telno, @Konum)", baglanti);
                        komut.Parameters.AddWithValue("@Email", email);
                        komut.Parameters.AddWithValue("@AdSoyad", adsoyad);
                        komut.Parameters.AddWithValue("@Sifre", sifre);
                        komut.Parameters.AddWithValue("@Telno", telno);
                        komut.Parameters.AddWithValue("@Konum", konum);
                        komut.ExecuteNonQuery();

                        // Form1'nin bir örneğini oluşturun
                        uygulama_ana_Form form1 = new uygulama_ana_Form();

                        // Form1'yi gösterin
                        form1.Show();

                        // İsteğe bağlı olarak, mevcut formu gizleyebilirsiniz
                        this.Hide();
                    }
                }
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (giris_sifre_gorunurluk_checkBox.Checked)
            {
                // Checkbox işaretli olduğunda, TextBox'ın PasswordChar özelliğini devre dışı bırakır
                giris_sifre_textBox.PasswordChar = '\0';
            }
            else
            {
                // Checkbox işaretli değilken, TextBox'ın PasswordChar özelliğini etkinleştirir
                giris_sifre_textBox.PasswordChar = '*';
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (kayit_sifre_gorunurluk_checkBox.Checked)
            {
                // Checkbox işaretli olduğunda, TextBox'ın PasswordChar özelliğini devre dışı bırakır
                kayit_sifre_textBox.PasswordChar = '\0';
                kayit_sifreonay_textBox.PasswordChar = '\0';
            }
            else
            {
                // Checkbox işaretli değilken, TextBox'ın PasswordChar özelliğini etkinleştirir
                kayit_sifre_textBox.PasswordChar = '*';
                kayit_sifreonay_textBox.PasswordChar = '*';
            }
        }

        private void giris_slogan_label_Click(object sender, EventArgs e)
        {
            // Form1'nin bir örneğini oluşturun
            uygulama_ana_Form form1 = new uygulama_ana_Form();

            // Form1'yi gösterin
            form1.Show();

            // İsteğe bağlı olarak, mevcut formu gizleyebilirsiniz
            this.Hide();
        }

        private void kayit_slogan_label_Click(object sender, EventArgs e)
        {

        }

        private void giris_tabPage_Click(object sender, EventArgs e)
        {
            
        }
    }
}

