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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            // MaximizeBox'ı devre dışı bırakır
            this.MaximizeBox = false;

            // Kullanıcının formun boyutunu değiştirmesini engeller
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb");
        OleDbCommand komut = new OleDbCommand();


       






        private void goster_onecikan()
        {

            listView7.Items.Clear();
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

                listView7.Items.Add(ekle);
            }
            baglanti.Close();

        }

        private void goster_musteri()
        {

            listView2.Items.Clear();
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
              

                listView2.Items.Add(ekle);
            }
            baglanti.Close();

        }

        private void goster_musteri_rezer()
        {

            listView8.Items.Clear();
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


                listView8.Items.Add(ekle);
            }
            baglanti.Close();

        }

        private void goster_musteri_sonziyaret()
        {

            listView3.Items.Clear();
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


                listView3.Items.Add(ekle);
            }
            baglanti.Close();

        }

        private void goster_musteri_yorum()
        {

            listView4.Items.Clear();
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


                listView4.Items.Add(ekle);
            }
            baglanti.Close();

        }

        private void goster_musteri_fav()
        {

            listView5.Items.Clear();
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


                listView5.Items.Add(ekle);
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

        

        

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(openFileDialog.FileName);
                pictureBox1.ImageLocation = openFileDialog.FileName;
                // Dosya adını alın
                
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                textBox5.Text = "5";
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                textBox5.Text = "4";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                textBox5.Text = "3";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox5.Text = "2";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox5.Text = "1";
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                textBox6.Text = "5";
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
            {
                textBox6.Text = "4";
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                textBox6.Text = "3";
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                textBox6.Text = "2";
            }
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked)
            {
                textBox6.Text = "1";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog.FileName);
                pictureBox1.ImageLocation = openFileDialog.FileName;

                string diskLetter = Path.GetPathRoot(Environment.CurrentDirectory)[0].ToString();
                string imagePath = diskLetter + @":\Special Disk\VeriTabani\Kullanılan\Masa üstü rezerv Network\OtelRezerved Network\OtelRezerved Network\bin\Debug\resim\" + Path.GetFileName(openFileDialog.FileName);

                DialogResult dialogResult = MessageBox.Show("Otel eklensin mi?", "Onay", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    File.Copy(openFileDialog.FileName, imagePath, true);

                    baglanti.Open();
                    OleDbCommand komut = new OleDbCommand("INSERT into otel_onecikan (oteladi, aciklama, fiyat, konum, bolgekonum, yildiz, ImagePath) values ('"
                    + textBox1.Text + "','" + richTextBox2.Text + "','" + textBox3.Text +
                    "','" + comboBox1.Text + "','" + comboBox5.Text + "','" + textBox6.Text + "','" + imagePath + "')", baglanti);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    goster_onecikan();

                    richTextBox2.Clear();
                    textBox1.Clear();
                    textBox3.Clear();
                    textBox6.Clear();
                    comboBox1.SelectedIndex = -1;
                    comboBox5.SelectedIndex = -1;
                    radioButton10.Checked = false;
                    radioButton9.Checked = false;
                    radioButton8.Checked = false;
                    radioButton7.Checked = false;
                    radioButton6.Checked = false;
                    pictureBox1.Image = null;

                }
            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
//Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\bin\Debug\otel.mdb