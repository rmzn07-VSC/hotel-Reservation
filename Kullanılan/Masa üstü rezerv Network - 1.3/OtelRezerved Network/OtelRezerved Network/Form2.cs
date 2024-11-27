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

    public partial class Form2 : Form
    {


        public Form2()
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
            if (textBox1.Text == "plusXreserved@gmail.com" && textBox2.Text == "147896325")
            {
                Form3 form3 = new Form3();

                // Form2'yi gösterin
                form3.Show();

                // İsteğe bağlı olarak, mevcut formu gizleyebilirsiniz
                this.Hide();
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
                if (textBox3.Text == textBox6.Text && textBox3.Text != null && textBox6.Text != null && textBox5.Text != null && textBox4.Text != null && comboBox1.Text != null)
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
                    comboBox1.Text = "  ";

                    // Form1'nin bir örneğini oluşturun
                    Form1 form1 = new Form1();

                    // Form1'yi gösterin
                    form1.Show();

                    // İsteğe bağlı olarak, mevcut formu gizleyebilirsiniz
                    this.Hide();


                }
                 else 
                { 
                    MessageBox.Show("Şifre Onayını Kontrol edin"); 
                }
            
        }
    }
}

