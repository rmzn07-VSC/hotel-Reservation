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
            tabControl1.SelectedTab = tabPage1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // tabPage1'den tabPage2'ye geçiş yapar
            tabControl1.SelectedTab = tabPage2;
        }





        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "xx" && textBox2.Text == "xx")
            {
                yonetici_merkez form3 = new yonetici_merkez();

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
                if (textBox3.Text == textBox6.Text )
                {
                    if (textBox3.Text != "" && textBox6.Text != "" && textBox5.Text != "" && textBox4.Text != "" && comboBox1.Text != "" && textBox9.Text != "")
                    {


                        baglanti.Open();
                        OleDbCommand komut = new OleDbCommand("INSERT into musteri ( mail, adsoyad , sifre , telno , konum) values ('"
                        + textBox4.Text + "','" + textBox5.Text + "','" + textBox3.Text +
                        "','" + textBox9.Text + "','" + comboBox1.Text + "')", baglanti);
                        komut.ExecuteNonQuery();
                        baglanti.Close();

                        textBox5.Clear();

                        textBox4.Clear();
                        textBox3.Clear();
                        textBox6.Clear();

                        textBox9.Clear();
                        comboBox1.Text = "";

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
            if (checkBox1.Checked)
            {
                // Checkbox işaretli olduğunda, TextBox'ın PasswordChar özelliğini devre dışı bırakır
                textBox2.PasswordChar = '\0';
            }
            else
            {
                // Checkbox işaretli değilken, TextBox'ın PasswordChar özelliğini etkinleştirir
                textBox2.PasswordChar = '*';
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                // Checkbox işaretli olduğunda, TextBox'ın PasswordChar özelliğini devre dışı bırakır
                textBox3.PasswordChar = '\0';
                textBox6.PasswordChar = '\0';
            }
            else
            {
                // Checkbox işaretli değilken, TextBox'ın PasswordChar özelliğini etkinleştirir
                textBox3.PasswordChar = '*';
                textBox6.PasswordChar = '*';
            }
        }
    }
}

