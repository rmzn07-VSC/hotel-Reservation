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

            // MaximizeBox'ı devre dışı bırakır
            this.MaximizeBox = false;

            // Kullanıcının formun boyutunu değiştirmesini engeller
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
                ekle.SubItems.Add(oku["adsoyad"].ToString());
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
                ekle.SubItems.Add(oku["adsoyad"].ToString());
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
                ekle.SubItems.Add(oku["adsoyad"].ToString());
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
                ekle.SubItems.Add(oku["adsoyad"].ToString());
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
                ekle.SubItems.Add(oku["adsoyad"].ToString());
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


            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                onecikan_otelresim_pictureBox.Image = new Bitmap(openFileDialog.FileName);
                onecikan_otelresim_pictureBox.ImageLocation = openFileDialog.FileName;

                // Uygulamanın çalıştığı dizini alırız.
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string imagePath = Path.Combine(appDirectory, "resim", Path.GetFileName(openFileDialog.FileName));
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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void onecikan_fiyat_label_Click(object sender, EventArgs e)
        {

        }
    }
}
//Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\bin\Debug\otel.mdb