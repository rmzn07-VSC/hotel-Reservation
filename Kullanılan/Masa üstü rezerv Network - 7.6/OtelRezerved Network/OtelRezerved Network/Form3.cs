using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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


        private void Form3_Load(object sender, EventArgs e)
        {
            goster_onecikan();
            goster_musteri();
            goster_musteri_rezer();
            goster_musteri_sonziyaret();
            goster_musteri_yorum();
            goster_musteri_fav();
        }

        

        

        

        

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (ozelteklif_5yildiz_radioButton.Checked)
            {
                ozelteklif_yildizsayisi_textBox.Text = "5";
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (ozelteklif_4yildiz_radioButton.Checked)
            {
                ozelteklif_yildizsayisi_textBox.Text = "4";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (ozelteklif_3yildiz_radioButton.Checked)
            {
                ozelteklif_yildizsayisi_textBox.Text = "3";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (ozelteklif_2yildiz_radioButton.Checked)
            {
                ozelteklif_yildizsayisi_textBox.Text = "2";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (ozelteklif_1yildiz_radioButton.Checked)
            {
                ozelteklif_yildizsayisi_textBox.Text = "1";
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
    }
}
//Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\bin\Debug\otel.mdb