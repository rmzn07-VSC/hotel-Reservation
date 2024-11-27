using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OtelRezerved_Network.giris_kayit_Form;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace OtelRezerved_Network
{
    public partial class yonetici_merkez_Form : Form
    {
        public yonetici_merkez_Form()
        {
            InitializeComponent();



            // Kullanıcının formun boyutunu değiştirmesini engeller
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // TextBox'ınıza placeholder metni ekleyin
            onecikan_oteladi_textBox.Text = "Otel adını giriniz...";
            onecikan_oteladi_textBox.ForeColor = Color.Gray;

            // TextBox'a odaklandığında placeholder metni silin
            onecikan_oteladi_textBox.GotFocus += RemovePlaceholderAd;
            // TextBox'tan odak çıktığında ve metin boşsa placeholder metni geri getirin
            onecikan_oteladi_textBox.LostFocus += AddPlaceholderAd;

            onecikan_aciklama_richTextBox.Text = "Otel açıklamasını giriniz...";
            onecikan_aciklama_richTextBox.ForeColor = Color.Gray;

            // TextBox'a odaklandığında placeholder metni silin
            onecikan_aciklama_richTextBox.GotFocus += RemovePlaceholderAciklama;
            // TextBox'tan odak çıktığında ve metin boşsa placeholder metni geri getirin
            onecikan_aciklama_richTextBox.LostFocus += AddPlaceholderAciklama;

            
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb");
        OleDbCommand komut = new OleDbCommand();

        private void temizle()
        {
            onecikan_aciklama_richTextBox.Clear();
            onecikan_oteladi_textBox.Clear();
            onecikan_fiyat_textBox.Clear();
            onecikan_yildizsayisi_textBox.Clear();
            onecikan_konum_comboBox.SelectedIndex = -1;
            onecikan_bolgeselkonum_comboBox.SelectedIndex = -1;
            onecikan_1yildiz_radioButton.Checked = false;
            onecikan_2yildiz_radioButton.Checked = false;
            onecikan_3yildiz_radioButton.Checked = false;
            onecikan_4yildiz_radioButton.Checked = false;
            onecikan_5yildiz_radioButton.Checked = false;
            onecikan_otelresim_pictureBox.Image = null;

        }



        public void RemovePlaceholderAd(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox tb = (System.Windows.Forms.TextBox)sender;
            if (tb.Text == "Otel adını giriniz...")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        public void AddPlaceholderAd(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox tb = (System.Windows.Forms.TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Otel adını giriniz...";
                tb.ForeColor = Color.Gray;
            }
        }

        public void RemovePlaceholderAciklama(object sender, EventArgs e)
        {
            System.Windows.Forms.RichTextBox rtb = (System.Windows.Forms.RichTextBox)sender;
            if (rtb.Text == "Otel açıklamasını giriniz...")
            {
                rtb.Text = "";
                rtb.ForeColor = Color.Black;
            }
        }

        public void AddPlaceholderAciklama(object sender, EventArgs e)
        {
            System.Windows.Forms.RichTextBox rtb = (System.Windows.Forms.RichTextBox)sender;
            if (string.IsNullOrWhiteSpace(rtb.Text))
            {
                rtb.Text = "Otel açıklamasını giriniz...";
                rtb.ForeColor = Color.Gray;
            }
        }

        public void RemovePlaceholderAra(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox rtb = (System.Windows.Forms.TextBox)sender;
            if (rtb.Text == "Aratılacak Oteli Giriniz...")
            {
                rtb.Text = "";
                rtb.ForeColor = Color.Black;
            }
        }

        public void AddPlaceholderAra(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox rtb = (System.Windows.Forms.TextBox)sender;
            if (string.IsNullOrWhiteSpace(rtb.Text))
            {
                rtb.Text = "Aratılacak Oteli Giriniz...";
                rtb.ForeColor = Color.Gray;
            }
        }





        private void goster_onecikan()
        {

            onecikan_oteller_listView.Items.Clear();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = ("Select * From otel_onecikan");
            OleDbDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {

                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["oteladi"].ToString();
                ekle.SubItems.Add(oku["aciklama"].ToString());
                ekle.SubItems.Add(oku["fiyat"].ToString());
                ekle.SubItems.Add(oku["konum"].ToString());
                ekle.SubItems.Add(oku["bolgekonum"].ToString());
                ekle.SubItems.Add(oku["yildiz"].ToString());
                ekle.SubItems.Add(oku["ImagePath"].ToString());

                onecikan_oteller_listView.Items.Add(ekle);
            }
            baglanti.Close();

        }

        private void goster_musteri()
        {

            musteri_musterigöster_listView.Items.Clear();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = ("Select * From musteri");
            OleDbDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {

                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["mail"].ToString();
                ekle.SubItems.Add(oku["ad"].ToString());
                ekle.SubItems.Add(oku["sifre"].ToString());
                ekle.SubItems.Add(oku["telno"].ToString());
                ekle.SubItems.Add(oku["konum"].ToString());


                musteri_musterigöster_listView.Items.Add(ekle);
            }
            baglanti.Close();

        }

        private void goster_musteri_rezer()
        {

            rezervs_musteri_listView.Items.Clear();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = ("Select * From musteri");
            OleDbDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {

                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["mail"].ToString();
                ekle.SubItems.Add(oku["ad"].ToString());
                ekle.SubItems.Add(oku["sifre"].ToString());
                ekle.SubItems.Add(oku["telno"].ToString());
                ekle.SubItems.Add(oku["konum"].ToString());


                rezervs_musteri_listView.Items.Add(ekle);
            }
            baglanti.Close();

        }

        private void goster_musteri_sonziyaret()
        {

            son_musteri_listView.Items.Clear();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = ("Select * From musteri");
            OleDbDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {

                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["mail"].ToString();
                ekle.SubItems.Add(oku["ad"].ToString());
                ekle.SubItems.Add(oku["sifre"].ToString());
                ekle.SubItems.Add(oku["telno"].ToString());
                ekle.SubItems.Add(oku["konum"].ToString());


                son_musteri_listView.Items.Add(ekle);
            }
            baglanti.Close();

        }

        private void goster_musteri_yorum()
        {

            yorum_musteri_listView.Items.Clear();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = ("Select * From musteri");
            OleDbDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {

                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["mail"].ToString();
                ekle.SubItems.Add(oku["ad"].ToString());
                ekle.SubItems.Add(oku["sifre"].ToString());
                ekle.SubItems.Add(oku["telno"].ToString());
                ekle.SubItems.Add(oku["konum"].ToString());


                yorum_musteri_listView.Items.Add(ekle);
            }
            baglanti.Close();

        }

        private void goster_musteri_fav()
        {

            favotel_musteri_listView.Items.Clear();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = ("Select * From musteri");
            OleDbDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {

                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["mail"].ToString();
                ekle.SubItems.Add(oku["ad"].ToString());
                ekle.SubItems.Add(oku["sifre"].ToString());
                ekle.SubItems.Add(oku["telno"].ToString());
                ekle.SubItems.Add(oku["konum"].ToString());


                favotel_musteri_listView.Items.Add(ekle);
            }
            baglanti.Close();

        }
        // ListView'deki tüm öğeleri saklayan bir liste oluştur
        List<ListViewItem> tumOgeler = new List<ListViewItem>();


        private void Form3_Load(object sender, EventArgs e)
        {
            goster_onecikan();
            goster_musteri();
            goster_musteri_rezer();
            goster_musteri_sonziyaret();
            goster_musteri_yorum();
            goster_musteri_fav();
            goster_yonetici();
            // Form yüklendiğinde, ListView'deki tüm öğeleri listeye ekleyin
            foreach (ListViewItem item in onecikan_oteller_listView.Items)
            {
                tumOgeler.Add((ListViewItem)item.Clone());
            }
        }











        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (onecikan_5yildiz_radioButton.Checked)
            {
                onecikan_yildizsayisi_textBox.Text = "5";
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (onecikan_4yildiz_radioButton.Checked)
            {
                onecikan_yildizsayisi_textBox.Text = "4";
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (onecikan_3yildiz_radioButton.Checked)
            {
                onecikan_yildizsayisi_textBox.Text = "3";
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (onecikan_2yildiz_radioButton.Checked)
            {
                onecikan_yildizsayisi_textBox.Text = "2";
            }
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (onecikan_1yildiz_radioButton.Checked)
            {
                onecikan_yildizsayisi_textBox.Text = "1";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Kontrollerin doğru bir şekilde doldurulup doldurulmadığını kontrol et
                if (string.IsNullOrWhiteSpace(onecikan_oteladi_textBox.Text))
                {
                    MessageBox.Show("Lütfen otel adını giriniz.");
                }
                else if (string.IsNullOrWhiteSpace(onecikan_aciklama_richTextBox.Text))
                {
                    MessageBox.Show("Lütfen otel açıklamasını giriniz.");
                }
                else if (string.IsNullOrWhiteSpace(onecikan_konum_comboBox.Text))
                {
                    MessageBox.Show("Lütfen konumu seçiniz.");
                }
                else if (string.IsNullOrWhiteSpace(onecikan_bolgeselkonum_comboBox.Text))
                {
                    MessageBox.Show("Lütfen bölgesel konumu seçiniz.");
                }
                else if (string.IsNullOrWhiteSpace(onecikan_yildizsayisi_textBox.Text))
                {
                    MessageBox.Show("Lütfen yıldız sayısını giriniz.");
                }
                else if (!decimal.TryParse(onecikan_fiyat_textBox.Text, out _))
                {
                    MessageBox.Show("Lütfen fiyatı giriniz.");
                }

                else
                {
                    // Panel'in kaydırma konumunu sıfırla
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        onecikan_otelresim_pictureBox.Image = new Bitmap(openFileDialog.FileName);
                        onecikan_otelresim_pictureBox.ImageLocation = openFileDialog.FileName;

                        string diskLetter = Path.GetPathRoot(Environment.CurrentDirectory)[0].ToString();
                        string imagePath = diskLetter + @":\Special Disk\VeriTabani\Kullanılan\Masa üstü rezerv Network\OtelRezerved Network\OtelRezerved Network\bin\Debug\resim\" + Path.GetFileName(openFileDialog.FileName);
                        onecikan_yazi_label.Visible = false;
                        DialogResult dialogResult = MessageBox.Show("Otel eklensin mi?", "Onay", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            File.Copy(openFileDialog.FileName, imagePath, true);

                            baglanti.Open();
                            OleDbCommand komut = new OleDbCommand("INSERT into otel_onecikan (oteladi, aciklama, fiyat, konum, bolgekonum, yildiz, ImagePath) values ('"
                            + onecikan_oteladi_textBox.Text + "','" + onecikan_aciklama_richTextBox.Text + "','" + onecikan_fiyat_textBox.Text +
                            "','" + onecikan_konum_comboBox.Text + "','" + onecikan_bolgeselkonum_comboBox.Text + "','" + onecikan_yildizsayisi_textBox.Text + "','" + imagePath + "')", baglanti);
                            komut.ExecuteNonQuery();
                            baglanti.Close();
                            goster_onecikan();
                            temizle();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Resim gösterilemiyor!");
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void onecikan_fiyat_label_Click(object sender, EventArgs e)
        {

        }


        private void onecikan_fiyat_textBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            // Boşlukları ve harfleri engelle
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void onecikan_fiyat_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ozelteklif_listView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void onecikan_oteller_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (onecikan_oteller_listView.SelectedItems.Count > 0)
                {
                    // Seçili öğenin otel adını al
                    string otelAdi = onecikan_oteller_listView.SelectedItems[0].Text;

                    // Veritabanı bağlantısını oluştur
                    string connnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
                    using (OleDbConnection connn = new OleDbConnection(connnString))
                    {
                        connn.Open();

                        // Veritabanından veriyi al
                        string query = "SELECT * FROM otel_onecikan WHERE oteladi = @OtelAdi";
                        OleDbCommand cmd = new OleDbCommand(query, connn);
                        cmd.Parameters.AddWithValue("@OtelAdi", otelAdi);

                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            // Veriyi label ve richTextBox'a yaz
                            onecikan_oteladi_textBox.Text = reader["oteladi"].ToString();
                            onecikan_aciklama_richTextBox.Text = reader["aciklama"].ToString();
                            onecikan_fiyat_textBox.Text = reader["fiyat"].ToString();
                            onecikan_konum_comboBox.Text = reader["konum"].ToString();
                            onecikan_bolgeselkonum_comboBox.Text = reader["bolgekonum"].ToString();
                            onecikan_yildizsayisi_textBox.Text = reader["yildiz"].ToString();
                            string imagePath = reader["ImagePath"].ToString();
                            onecikan_otelresim_pictureBox.Image = Image.FromFile(imagePath);
                            onecikan_yazi_label.Visible = false;
                        }
                    }
                }
                goster_onecikan();
            }
            catch
            {
                onecikan_otelresim_pictureBox.Image = null;
                MessageBox.Show("Resim Gösterilemiyor!");

            }
        }




        

        private void onecikan_sil_button_Click(object sender, EventArgs e)
        {
            // Silinecek otel adını al
            string silinecekOtelAdi = onecikan_oteladi_textBox.Text;

            // Eğer otel adı boş veya sadece boşluklardan oluşuyorsa, işlemi durdur ve hata mesajı göster
            if (String.IsNullOrWhiteSpace(silinecekOtelAdi))
            {
                MessageBox.Show("Lütfen geçerli bir otel adı girin.");
                return;
            }

            // Veritabanı bağlantısını oluştur
            string connnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
            using (OleDbConnection connn = new OleDbConnection(connnString))
            {
                connn.Open();

                // Otel adının veritabanında olup olmadığını kontrol et
                string checkQuery = "SELECT COUNT(*) FROM otel_onecikan WHERE oteladi = @OtelAdi";
                using (OleDbCommand checkCmd = new OleDbCommand(checkQuery, connn))
                {
                    checkCmd.Parameters.AddWithValue("@OtelAdi", silinecekOtelAdi);
                    int count = (int)checkCmd.ExecuteScalar();

                    // Eğer otel adı veritabanında yoksa, işlemi durdur ve hata mesajı göster
                    if (count == 0)
                    {
                        MessageBox.Show("Girilen otel adı veritabanında bulunamadı.");
                        return;
                    }
                }

                // Veritabanından veriyi sil
                string query = "DELETE FROM otel_onecikan WHERE oteladi = @OtelAdi";
                using (OleDbCommand cmd = new OleDbCommand(query, connn))
                {
                    cmd.Parameters.AddWithValue("@OtelAdi", silinecekOtelAdi);
                    cmd.ExecuteNonQuery();
                }
            }

            goster_onecikan();
            temizle();

        }



        private void onecikan_guncelle_button_Click(object sender, EventArgs e)
        {
            // Güncellenecek otel adını al
            string guncellenecekOtelAdi = onecikan_oteladi_textBox.Text;

            // Eğer otel adı boş veya sadece boşluklardan oluşuyorsa, işlemi durdur ve hata mesajı göster
            if (String.IsNullOrWhiteSpace(guncellenecekOtelAdi))
            {
                MessageBox.Show("Lütfen geçerli bir otel adı girin.");
                return;
            }

            // Veritabanı bağlantısını oluştur
            string connnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
            using (OleDbConnection connn = new OleDbConnection(connnString))
            {
                connn.Open();

                // Otel adının veritabanında olup olmadığını kontrol et
                string checkQuery = "SELECT COUNT(*) FROM otel_onecikan WHERE oteladi = @OtelAdi";
                using (OleDbCommand checkCmd = new OleDbCommand(checkQuery, connn))
                {
                    checkCmd.Parameters.AddWithValue("@OtelAdi", guncellenecekOtelAdi);
                    int count = (int)checkCmd.ExecuteScalar();

                    // Eğer otel adı veritabanında yoksa, işlemi durdur ve hata mesajı göster
                    if (count == 0)
                    {
                        MessageBox.Show("Girilen otel adı veritabanında bulunamadı.");
                        return;
                    }
                }

                // "Resmi değiştirmek ister misiniz?" diye sor
                DialogResult dialogResult = MessageBox.Show("Resmi değiştirmek ister misiniz?", "Resim Değiştirme", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    onecikan_yazi_label.Visible = false;
                    // OpenFileDialog oluştur
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|All files (*.*)|*.*";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Resmi PictureBox'a yükle
                        onecikan_otelresim_pictureBox.Image = new Bitmap(openFileDialog.FileName);

                        // Resmi belirtilen yola kaydet
                        string baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                        string imagePath = Path.Combine(baseDirectory, "resim", Path.GetFileName(openFileDialog.FileName));
                        onecikan_otelresim_pictureBox.Image.Save(imagePath, ImageFormat.Png);

                        // Veritabanında veriyi güncelle
                        string query = "UPDATE otel_onecikan SET aciklama = @aciklama, fiyat = @fiyat, konum = @konum, bolgekonum = @bolgekonum, yildiz = @yildiz, ImagePath = @ImagePath WHERE oteladi = @OtelAdi";
                        using (OleDbCommand cmd = new OleDbCommand(query, connn))
                        {
                            cmd.Parameters.AddWithValue("@aciklama", onecikan_aciklama_richTextBox.Text);
                            cmd.Parameters.AddWithValue("@fiyat", onecikan_fiyat_textBox.Text);
                            cmd.Parameters.AddWithValue("@konum", onecikan_konum_comboBox.Text);
                            cmd.Parameters.AddWithValue("@bolgekonum", onecikan_bolgeselkonum_comboBox.Text);
                            cmd.Parameters.AddWithValue("@yildiz", onecikan_yildizsayisi_textBox.Text);
                            cmd.Parameters.AddWithValue("@ImagePath", imagePath);
                            cmd.Parameters.AddWithValue("@OtelAdi", guncellenecekOtelAdi);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // Resim değiştirilmediyse, sadece diğer alanları güncelle
                    string query = "UPDATE otel_onecikan SET aciklama = @aciklama, fiyat = @fiyat, konum = @konum, bolgekonum = @bolgekonum, yildiz = @yildiz WHERE oteladi = @OtelAdi";
                    using (OleDbCommand cmd = new OleDbCommand(query, connn))
                    {
                        cmd.Parameters.AddWithValue("@aciklama", onecikan_aciklama_richTextBox.Text);
                        cmd.Parameters.AddWithValue("@fiyat", onecikan_fiyat_textBox.Text);
                        cmd.Parameters.AddWithValue("@konum", onecikan_konum_comboBox.Text);
                        cmd.Parameters.AddWithValue("@bolgekonum", onecikan_bolgeselkonum_comboBox.Text);
                        cmd.Parameters.AddWithValue("@yildiz", onecikan_yildizsayisi_textBox.Text);
                        cmd.Parameters.AddWithValue("@OtelAdi", guncellenecekOtelAdi);

                        cmd.ExecuteNonQuery();
                    }
                }

                // ListView'ı yenile
                goster_onecikan();
            }
        }

        private void onecikan_temizle_button_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void musteri_musterigöster_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                musteri_email_textBox.Text = musteri_musterigöster_listView.SelectedItems[0].SubItems[0].Text.ToString();
                musteri_adi_textBox.Text = musteri_musterigöster_listView.SelectedItems[0].SubItems[1].Text.ToString();
                musteri_sifre_textBox.Text = musteri_musterigöster_listView.SelectedItems[0].SubItems[2].Text.ToString();
                musteri_telno_textBox.Text = musteri_musterigöster_listView.SelectedItems[0].SubItems[3].Text.ToString();
                musteri_konum_comboBox.Text = musteri_musterigöster_listView.SelectedItems[0].SubItems[4].Text.ToString();
            }
            catch { }
        }

        private void rezervs_musteri_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rezervs_musteri_listView.SelectedItems.Count > 0)
            {
                string secilenMusteri = rezervs_musteri_listView.SelectedItems[0].SubItems[0].Text.ToString();

                // Veritabanı bağlantısını oluştur
                string connnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
                using (OleDbConnection connn = new OleDbConnection(connnString))
                {
                    connn.Open();

                    // Veritabanından veriyi al
                    string query = "SELECT * FROM musteri_rezerv WHERE musteri_adi = @MusteriAdi";
                    OleDbCommand cmd = new OleDbCommand(query, connn);
                    cmd.Parameters.AddWithValue("@MusteriAdi", secilenMusteri);

                    // Veriyi DataTable'a doldur
                    OleDbDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // rezervs_goster_listView'i temizle
                    rezervs_goster_listView.Items.Clear();

                    // DataTable'daki veriyi rezervs_goster_listView'e ekle
                    foreach (DataRow row in dt.Rows)
                    {
                        ListViewItem item = new ListViewItem(row["musteri_adi"].ToString());
                        item.SubItems.Add(row["otel_adi"].ToString());
                        item.SubItems.Add(row["tarih"].ToString());
                        item.SubItems.Add(row["ucret"].ToString());
                        rezervs_goster_listView.Items.Add(item);
                    }
                }
            }
        }

        private void rezervs_goster_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                rezerv_oteladi_textBox.Text = rezervs_goster_listView.SelectedItems[0].SubItems[1].Text.ToString();
                rezerv_tarih_textBox.Text = rezervs_goster_listView.SelectedItems[0].SubItems[2].Text.ToString();
                rezerv_ucret_textBox.Text = rezervs_goster_listView.SelectedItems[0].SubItems[3].Text.ToString();
            }
            catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // Veritabanı bağlantısını oluştur
                string connnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
                using (OleDbConnection connn = new OleDbConnection(connnString))
                {
                    connn.Open();

                    // Veritabanından veriyi sil
                    string query = "DELETE FROM musteri_rezerv WHERE otel_adi = ? AND tarih = ? AND ucret = ?";
                    OleDbCommand cmd = new OleDbCommand(query, connn);
                    cmd.Parameters.AddWithValue("@otel_adi", rezerv_oteladi_textBox.Text);
                    cmd.Parameters.AddWithValue("@tarih", rezerv_tarih_textBox.Text);
                    cmd.Parameters.AddWithValue("@ucret", rezerv_ucret_textBox.Text);

                    // Komutu çalıştır
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                MessageBox.Show("Müşteri seçiniz!");
            }
            finally
            {
                rezervs_goster_listView.Items.Clear();
                goster_onecikan();
                goster_musteri();
                goster_musteri_rezer();
                goster_musteri_sonziyaret();
                goster_musteri_yorum();
                goster_musteri_fav();
            }
        }

        private void son_musteri_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (son_musteri_listView.SelectedItems.Count > 0)
            {
                string secilenMusteri = son_musteri_listView.SelectedItems[0].SubItems[0].Text.ToString();

                // Veritabanı bağlantısını oluştur
                string connnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
                using (OleDbConnection connn = new OleDbConnection(connnString))
                {
                    connn.Open();

                    // Veritabanından veriyi al
                    string query = "SELECT * FROM musteri_son WHERE musteri_adi = @MusteriAdi";
                    OleDbCommand cmd = new OleDbCommand(query, connn);
                    cmd.Parameters.AddWithValue("@MusteriAdi", secilenMusteri);

                    // Veriyi DataTable'a doldur
                    OleDbDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // rezervs_goster_listView'i temizle
                    son_sonotel_listView.Items.Clear();

                    // DataTable'daki veriyi rezervs_goster_listView'e ekle
                    foreach (DataRow row in dt.Rows)
                    {
                        ListViewItem item = new ListViewItem(row["tarih"].ToString());
                        item.SubItems.Add(row["musteri_adi"].ToString());
                        item.SubItems.Add(row["otel_adi"].ToString());

                        son_sonotel_listView.Items.Add(item);
                    }
                }
            }
        }

        private void yorum_musteri_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (yorum_musteri_listView.SelectedItems.Count > 0)
            {
                string secilenMusteri = yorum_musteri_listView.SelectedItems[0].SubItems[0].Text.ToString();

                // Veritabanı bağlantısını oluştur
                string connnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
                using (OleDbConnection connn = new OleDbConnection(connnString))
                {
                    connn.Open();

                    // Veritabanından veriyi al
                    string query = "SELECT * FROM musteri_yorum WHERE musteri_adi = @MusteriAdi";
                    OleDbCommand cmd = new OleDbCommand(query, connn);
                    cmd.Parameters.AddWithValue("@MusteriAdi", secilenMusteri);

                    // Veriyi DataTable'a doldur
                    OleDbDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // rezervs_goster_listView'i temizle
                    yorum_goster_listView11.Items.Clear();

                    // DataTable'daki veriyi rezervs_goster_listView'e ekle
                    foreach (DataRow row in dt.Rows)
                    {
                        ListViewItem item = new ListViewItem(row["musteri_adi"].ToString());

                        item.SubItems.Add(row["otel_adi"].ToString());
                        item.SubItems.Add(row["yorum"].ToString());

                        yorum_goster_listView11.Items.Add(item);
                    }
                }
            }
        }

        private void yorum_goster_listView11_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                musteri_yorumu_richTextBox.Text = yorum_goster_listView11.SelectedItems[0].SubItems[2].Text.ToString();
            }
            catch { }
        }

        private void favotel_musteri_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (favotel_musteri_listView.SelectedItems.Count > 0)
            {
                string secilenMusteri = favotel_musteri_listView.SelectedItems[0].SubItems[0].Text.ToString();

                // Veritabanı bağlantısını oluştur
                string connnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
                using (OleDbConnection connn = new OleDbConnection(connnString))
                {
                    connn.Open();

                    // Veritabanından veriyi al
                    string query = "SELECT * FROM musteri_fav WHERE musteri_adi = @MusteriAdi";
                    OleDbCommand cmd = new OleDbCommand(query, connn);
                    cmd.Parameters.AddWithValue("@MusteriAdi", secilenMusteri);

                    // Veriyi DataTable'a doldur
                    OleDbDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // rezervs_goster_listView'i temizle
                    favotel_favs_listView.Items.Clear();

                    // DataTable'daki veriyi rezervs_goster_listView'e ekle
                    foreach (DataRow row in dt.Rows)
                    {
                        ListViewItem item = new ListViewItem(row["musteri_adi"].ToString());

                        item.SubItems.Add(row["fav_otel"].ToString());


                        favotel_favs_listView.Items.Add(item);
                    }
                }
            }
        }

        private void ekle_button_Click(object sender, EventArgs e)
        {
            


            string email = musteri_email_textBox.Text;
            string sifre = musteri_sifre_textBox.Text;
            
            string adsoyad = musteri_adi_textBox.Text;
            string telno = musteri_telno_textBox.Text;
            string konum = musteri_konum_comboBox.Text;



            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sifre) || string.IsNullOrEmpty(adsoyad) || string.IsNullOrEmpty(telno) || string.IsNullOrEmpty(konum))
            {
                MessageBox.Show("Gerekli alanları doldurun!");
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
                        OleDbCommand komut = new OleDbCommand("INSERT into musteri (mail, ad, sifre, telno, konum) values (@Email, @AdSoyad, @Sifre, @Telno, @Konum)", baglanti);
                        komut.Parameters.AddWithValue("@Email", email);
                        komut.Parameters.AddWithValue("@AdSoyad", adsoyad);
                        komut.Parameters.AddWithValue("@Sifre", sifre);
                        komut.Parameters.AddWithValue("@Telno", telno);
                        komut.Parameters.AddWithValue("@Konum", konum);
                        komut.ExecuteNonQuery();
                        goster_musteri();
                        musteriYonetimTemizle();



                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            goster_musteri();
        }

        private void guncelle_button_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string ad = musteri_adi_textBox.Text;
                    string mail = musteri_email_textBox.Text;
                    string sifre = musteri_sifre_textBox.Text;
                    string telno = musteri_telno_textBox.Text;
                    string konum = musteri_konum_comboBox.Text;

                    // musteri tablosunda güncelleme
                    string query = "UPDATE musteri SET mail = @mail, sifre = @sifre, telno = @telno, konum = @konum WHERE ad = @ad";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@mail", mail);
                        command.Parameters.AddWithValue("@sifre", sifre);
                        command.Parameters.AddWithValue("@telno", telno);
                        command.Parameters.AddWithValue("@konum", konum);
                        command.Parameters.AddWithValue("@ad", ad);

                        command.ExecuteNonQuery();
                    }

                    // musteri_fav tablosunda güncelleme
                    query = "UPDATE musteri_fav SET musteri_adi = @mail WHERE musteri_adi = @ad";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        
                        command.Parameters.AddWithValue("@ad", ad);

                        command.ExecuteNonQuery();
                    }

                    // musteri_rezerv tablosunda güncelleme
                    query = "UPDATE musteri_rezerv SET musteri_adi = @mail WHERE musteri_adi = @ad";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                       
                        command.Parameters.AddWithValue("@ad", ad);

                        command.ExecuteNonQuery();
                    }

                    // musteri_son tablosunda güncelleme
                    query = "UPDATE musteri_son SET musteri_adi = @mail WHERE musteri_adi = @ad";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        
                        command.Parameters.AddWithValue("@ad", ad);

                        command.ExecuteNonQuery();
                    }

                    // musteri_yorum tablosunda güncelleme
                    query = "UPDATE musteri_yorum SET musteri_adi = @mail WHERE musteri_adi = @ad";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                       
                        command.Parameters.AddWithValue("@ad", ad);

                        command.ExecuteNonQuery();
                    }

                    goster_musteri();
                }
            }
           

            finally
            {
                goster_musteri();
                goster_onecikan();
                musteriYonetimTemizle();
                goster_musteri_rezer();
                goster_musteri_sonziyaret();
                goster_musteri_yorum();
                goster_musteri_fav();
            }
        }

        private void sil_button_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string ad = musteri_adi_textBox.Text;

                    // musteri tablosundan silme
                    string query = "DELETE FROM musteri WHERE ad = @ad";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ad", ad);
                        command.ExecuteNonQuery();
                    }

                    // musteri_fav tablosundan silme
                    query = "DELETE FROM musteri_fav WHERE musteri_adi = @ad";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ad", ad);
                        command.ExecuteNonQuery();
                    }

                    // musteri_rezerv tablosundan silme
                    query = "DELETE FROM musteri_rezerv WHERE musteri_adi = @ad";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ad", ad);
                        command.ExecuteNonQuery();
                    }

                    // musteri_son tablosundan silme
                    query = "DELETE FROM musteri_son WHERE musteri_adi = @ad";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ad", ad);
                        command.ExecuteNonQuery();
                    }

                    // musteri_yorum tablosundan silme
                    query = "DELETE FROM musteri_yorum WHERE musteri_adi = @ad";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ad", ad);
                        command.ExecuteNonQuery();
                    }

                    goster_musteri();
                }
            }
            finally
            {
                goster_musteri();
                goster_onecikan();
                musteriYonetimTemizle();
                goster_musteri_rezer();
                goster_musteri_sonziyaret();
                goster_musteri_yorum();
                goster_musteri_fav();
            }

        }
        
        private void musteriYonetimTemizle()
        {
            musteri_adi_textBox.Clear();
            musteri_email_textBox.Clear();
            musteri_sifre_textBox.Clear();
            musteri_telno_textBox.Clear();
            musteri_konum_comboBox.SelectedIndex = -1;

        }

        private void musteri_telno_textBox_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void goster_yonetici()
        {

            yonetici_listView.Items.Clear();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = ("Select * From yonetici");
            OleDbDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {

                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["email"].ToString();
                ekle.SubItems.Add(oku["sifre"].ToString());



                yonetici_listView.Items.Add(ekle);
            }
            baglanti.Close();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            goster_yonetici();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string email = yonetici_email_textBox.Text;
                string sifre = yonetici_sifre_textBox.Text;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sifre))
                {
                    MessageBox.Show("E-mail ve Şifre alanları boş olamaz!");
                    return;
                }

                using (OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb"))
                {
                    baglanti.Open();

                    OleDbCommand kontrolKomut = new OleDbCommand("SELECT * FROM yonetici WHERE email = @Email", baglanti);
                    kontrolKomut.Parameters.AddWithValue("@Email", email);
                    kontrolKomut.Parameters.AddWithValue("@Sifre", sifre);
                    OleDbDataReader dr = kontrolKomut.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();
                        if (email == dr["mail"].ToString())
                        {
                            MessageBox.Show("E-mail zaten mevcut");
                        }
                        else if (email == "xx")
                        {
                            MessageBox.Show("Bu Mail'i kullanamazsınız!");
                        }
                    }
                    else
                    {
                        OleDbCommand komut = new OleDbCommand("INSERT into yonetici (email, sifre) values (@Email, @Sifre)", baglanti);
                        komut.Parameters.AddWithValue("@Email", email);
                        komut.Parameters.AddWithValue("@Sifre", sifre);
                        komut.ExecuteNonQuery();
                        goster_yonetici();
                    }
                }
            }
            finally
            {
                temizle_yonetici();
                goster_yonetici();
            }
        }

        private void temizle_yonetici()
        {
            yonetici_sifre_textBox.Clear();
            yonetici_email_textBox.Clear();



        }

        private void yonetici_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                yonetici_email_textBox.Text = yonetici_listView.SelectedItems[0].SubItems[0].Text.ToString();
                yonetici_sifre_textBox.Text = yonetici_listView.SelectedItems[0].SubItems[1].Text.ToString();
               
            }
            catch { }
        }

        private void yonetici_guncelle_button_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string email = yonetici_sifre_textBox.Text;
                    string sifre = yonetici_email_textBox.Text;

                    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sifre))
                    {
                        MessageBox.Show("E-mail ve Şifre alanları boş olamaz!");
                        return;
                    }

                    // yonetici tablosunda güncelleme
                    string query = "UPDATE yonetici SET sifre = @sifre WHERE email = @email";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@sifre", sifre);

                        int affectedRows = command.ExecuteNonQuery();
                        if (affectedRows > 0)
                        {
                            // Güncelleme başarılı
                            MessageBox.Show("Güncelleme başarılı!");
                        }
                        else
                        {
                            // Güncelleme başarısız, email bulunamadı
                            MessageBox.Show("Yönetici bulunamadı!");
                        }

                        temizle_yonetici();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Yönetici Bulunamadı!");

            }
            finally
            {
                temizle_yonetici();
                goster_yonetici();

            }
        }

        private void yonetici_sil_button_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string email = yonetici_email_textBox.Text;


                    if (string.IsNullOrEmpty(email))
                    {
                        MessageBox.Show("E-mail ve Şifre alanları boş olamaz!");
                        return;
                    }


                    // musteri tablosundan silme
                    string query = "DELETE FROM yonetici WHERE email = @mail";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@mail", email);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Yönetici Bulunamadı!");

            }
            finally

            {
                temizle_yonetici();
                goster_yonetici();

            }
        }
    }
}

//Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\bin\Debug\otel.mdb