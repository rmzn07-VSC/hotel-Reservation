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
            if (giris_email_textBox.Text == "xx" && giris_sifre_textBox.Text == "xx")
            {
                yonetici_merkez_Form form3 = new yonetici_merkez_Form();

                // Form2'yi gösterin
                form3.Show();

                // İsteğe bağlı olarak, mevcut formu gizleyebilirsiniz
                this.Hide();
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (kayit_sifre_textBox.Text == kayit_sifreonay_textBox.Text )
                {
                    if (kayit_sifre_textBox.Text != "" && kayit_sifreonay_textBox.Text != "" && kayit_adsoyad_textBox.Text != "" && kayit_email_textBox.Text != "" && kayit_konum_comboBox.Text != "" && kayit_telno_textBox.Text != "")
                    {


                        baglanti.Open();
                        OleDbCommand komut = new OleDbCommand("INSERT into musteri ( mail, adsoyad , sifre , telno , konum) values ('"
                        + kayit_email_textBox.Text + "','" + kayit_adsoyad_textBox.Text + "','" + kayit_sifre_textBox.Text +
                        "','" + kayit_telno_textBox.Text + "','" + kayit_konum_comboBox.Text + "')", baglanti);
                        komut.ExecuteNonQuery();
                        baglanti.Close();

                        kayit_adsoyad_textBox.Clear();

                        kayit_email_textBox.Clear();
                        kayit_sifre_textBox.Clear();
                        kayit_sifreonay_textBox.Clear();

                        kayit_telno_textBox.Clear();
                        kayit_konum_comboBox.Text = "";

                        // Form1'nin bir örneğini oluşturun
                        uygulama_ana_Form form1 = new uygulama_ana_Form();

                        // Form1'yi gösterin
                        form1.Show();

                        // İsteğe bağlı olarak, mevcut formu gizleyebilirsiniz
                        this.Hide();
                    }
                    else MessageBox.Show("Giriş yap eksik var");

                }
                else
                {
                    MessageBox.Show("Şifre Onayını Kontrol Edin!");
                }

            }
            catch
            {
                MessageBox.Show("Tüm Boşlukları Doldurun!");
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

