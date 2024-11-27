using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelRezerved_Network
{
    

    public partial class rezervsayfa_Form : Form
    {
        public rezervsayfa_Form()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // Veritabanı bağlantısını oluştur
            string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                conn.Open();

                // Veritabanında sorgu çalıştır
                string query = "SELECT COUNT(*) FROM musteri_fav WHERE musteri_adi = @musteriAdi AND fav_otel = @favOtel";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@musteriAdi", Global.KullaniciKimText);
                    cmd.Parameters.AddWithValue("@favOtel", rezervsayfa_oteladi_label.Text);
                    int count = (int)cmd.ExecuteScalar();

                    // Eğer kullanıcı adı ve otel adı zaten musteri_fav tablosunda varsa, checkbox'ı seçili olarak ayarla
                    if (count > 0)
                    {
                        rezervsayfa_favekle_checkBox.Checked = true;
                    }
                }

                // Veritabanı bağlantısını kapat
                conn.Close();
            }
        }
        


        private void rezervsayfa_rezerveet_button_Click(object sender, EventArgs e)
        {

           

            // Eğer toplam fiyat boş veya "0,00$" ise, hata mesajı göster
            if (string.IsNullOrEmpty(rezervsayfa_toplamfiyat_label.Text) || rezervsayfa_toplamfiyat_label.Text == "0,00$")
            {
                MessageBox.Show("Geçerli bir tarih girin");
                return;
            }

            // Veritabanı bağlantısını oluştur
            string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                conn.Open();

                // Veritabanına kaydet
                string query = "INSERT INTO musteri_rezerv (musteri_adi, tarih, otel_adi, ucret) VALUES (@musteriAdi, @tarih, @otelAdi, @ucret)";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@musteriAdi", Global.KullaniciKimText);
                    cmd.Parameters.AddWithValue("@tarih", rezervsayfa_baslangicbitis_textBox.Text);
                    cmd.Parameters.AddWithValue("@otelAdi", rezervsayfa_oteladi_label.Text);
                    cmd.Parameters.AddWithValue("@ucret", rezervsayfa_toplamfiyat_label.Text.Replace("$", ""));
                    cmd.ExecuteNonQuery();
                    Temizlerezerv();
                }

                // Veritabanı bağlantısını kapat
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Veritabanı bağlantısını oluştur
            string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                conn.Open();

                // Veritabanına kaydet
                string query = "INSERT INTO musteri_yorum (musteri_adi, otel_adi, yorum) VALUES (@musteriAdi, @otelAdi, @yorum)";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@musteriAdi", Global.KullaniciKimText);
                    cmd.Parameters.AddWithValue("@otelAdi", rezervsayfa_oteladi_label.Text);
                    cmd.Parameters.AddWithValue("@yorum", rezervsayfa_kullaniciyorum_richTextBox.Text);
                    cmd.ExecuteNonQuery();
                    rezervsayfa_kullaniciyorum_richTextBox.Text = "";
                }

                // Veritabanı bağlantısını kapat
                conn.Close();
            }
        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rezervsayfa_baslangiçtarih_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (rezervsayfa_baslangiçtarih_dateTimePicker.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Geçmiş bir tarih seçemezsiniz. Lütfen bugünün tarihinden sonraki bir tarih seçin.");
                rezervsayfa_baslangiçtarih_dateTimePicker.Value = DateTime.Today;
            }
            else
            {
                rezervsayfa_baslangicbitis_textBox.Text = "Başlangıç Tarihi: " + rezervsayfa_baslangiçtarih_dateTimePicker.Value.ToShortDateString();
                // Bitiş tarihi de seçildiyse hesapla ve yazdır
                if (rezervsayfa_bitistarih_dateTimePicker.Value.Date >= rezervsayfa_baslangiçtarih_dateTimePicker.Value.Date)
                {
                    HesaplaVeYazdir();
                }
            }
        }

        private void rezervsayfa_bitistarih_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (rezervsayfa_bitistarih_dateTimePicker.Value.Date < rezervsayfa_baslangiçtarih_dateTimePicker.Value.Date)
            {
                MessageBox.Show("Bitiş tarihi başlangıç tarihinden önce olamaz!");
                rezervsayfa_bitistarih_dateTimePicker.Value = rezervsayfa_baslangiçtarih_dateTimePicker.Value;
            }
            else
            {
                rezervsayfa_baslangicbitis_textBox.Text = "Başlangıç: " + rezervsayfa_baslangiçtarih_dateTimePicker.Value.ToShortDateString() + " Bitiş: " + rezervsayfa_bitistarih_dateTimePicker.Value.ToShortDateString();
                // Başlangıç tarihi de seçildiyse hesapla ve yazdır
                if (rezervsayfa_baslangiçtarih_dateTimePicker.Value.Date <= rezervsayfa_bitistarih_dateTimePicker.Value.Date)
                {
                    HesaplaVeYazdir();
                }
            }
        }

        private void HesaplaVeYazdir()
        {
            // Başlangıç ve bitiş tarihleri arasındaki gün sayısını hesapla
            int gunSayisi = (rezervsayfa_bitistarih_dateTimePicker.Value.Date - rezervsayfa_baslangiçtarih_dateTimePicker.Value.Date).Days;

            // Günlük fiyatı al (sonundaki dolar işaretini kaldır)
            string gunlukFiyatText = rezervsayfa_gunlukfiyat_label.Text.Replace("$", "");
            double gunlukFiyat = double.Parse(gunlukFiyatText);

            // Toplam fiyatı hesapla
            double toplamFiyat = gunSayisi * gunlukFiyat;

            // Toplam fiyatı yazdır
            rezervsayfa_toplamfiyat_label.Text = toplamFiyat.ToString("0.00") + "$";
        }

        private void rezervsayfa_bilgiler_panel_Paint(object sender, PaintEventArgs e)
        {

        }
        void Temizlerezerv()
        {
            // DateTimePicker kontrollerini bugünün tarihine ayarla
            rezervsayfa_baslangiçtarih_dateTimePicker.Value = DateTime.Today;
            rezervsayfa_bitistarih_dateTimePicker.Value = DateTime.Today;

            // TextBox ve Label kontrollerinin metnini boş bir dize ile sıfırla
            rezervsayfa_baslangicbitis_textBox.Text = "";
            rezervsayfa_toplamfiyat_label.Text = "";
            rezervsayfa_gunlukfiyat_label.Text = "";

        }

        private void rezervsayfa_favekle_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            // Veritabanı bağlantısını oluştur
            string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                conn.Open();

                // Veritabanında sorgu çalıştır
                string query = "SELECT COUNT(*) FROM musteri_fav WHERE musteri_adi = @musteriAdi AND fav_otel = @favOtel";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@musteriAdi", Global.KullaniciKimText);
                    cmd.Parameters.AddWithValue("@favOtel", rezervsayfa_oteladi_label.Text);
                    int count = (int)cmd.ExecuteScalar();

                    if (rezervsayfa_favekle_checkBox.Checked == true)
                    {
                        // Checkbox checked olduğunda ve veritabanında kayıt yoksa, veritabanına veri ekle
                        if (count == 0)
                        {
                            query = "INSERT INTO musteri_fav (musteri_adi, fav_otel) VALUES (@musteriAdi, @favOtel)";
                            cmd.CommandText = query;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Checkbox checked değilse ve veritabanında kayıt varsa, veritabanından veri sil
                        if (count > 0)
                        {
                            query = "DELETE FROM musteri_fav WHERE musteri_adi = @musteriAdi AND fav_otel = @favOtel";
                            cmd.CommandText = query;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                // Veritabanı bağlantısını kapat
                conn.Close();
            }
        }
    }
}
