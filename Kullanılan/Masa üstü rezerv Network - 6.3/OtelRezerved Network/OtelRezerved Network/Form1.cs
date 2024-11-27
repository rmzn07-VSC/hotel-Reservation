using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelRezerved_Network
{
    public partial class uygulama_ana_Form : Form
    {
        public uygulama_ana_Form()
        {
            InitializeComponent();
        }

            // Formunuzun Resize olayına bir olay işleyici ekleyin
           
            

           


        private void button1_Click(object sender, EventArgs e)
        {
            // Panel'in kaydırma konumunu sıfırla
            uyg_onecikan_panel.AutoScrollPosition = new Point(0, 0);

            string searchText = uyg_onecikan_oteladi_textBox.Text;
            List<Control> matchedControls = new List<Control>();

            foreach (Control control in uyg_onecikan_panel.Controls)
            {
                if (control is GroupBox && ((GroupBox)control).Text.Contains(searchText))
                {
                    matchedControls.Add(control);
                }
            }

            int yPosition = 10; // Başlangıç konumu

            foreach (Control control in uyg_onecikan_panel.Controls)
            {
                if (control is GroupBox)
                {
                    if (matchedControls.Contains(control))
                    {
                        control.Visible = true;
                        control.Location = new Point(control.Location.X, yPosition);
                        yPosition += control.Height + 10; // Her bir GroupBox arasında 10 piksel boşluk bırakır
                    }
                    else
                    {
                        control.Visible = false;
                    }
                }
            }

            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Panel'in kaydırma konumunu sıfırla
            uyg_ozelteklif_panel.AutoScrollPosition = new Point(0, 0);

            string searchText = uyg_ozelteklif_oteladi_textBox.Text;
            List<Control> matchedControls = new List<Control>();

            foreach (Control control in uyg_ozelteklif_panel.Controls)
            {
                if (control is GroupBox && ((GroupBox)control).Text.Contains(searchText))
                {
                    matchedControls.Add(control);
                }
            }

            int yPosition = 10; // Başlangıç konumu

            foreach (Control control in uyg_ozelteklif_panel.Controls)
            {
                if (control is GroupBox)
                {
                    if (matchedControls.Contains(control))
                    {
                        control.Visible = true;
                        control.Location = new Point(control.Location.X, yPosition);
                        yPosition += control.Height + 10; // Her bir GroupBox arasında 10 piksel boşluk bırakır
                    }
                    else
                    {
                        control.Visible = false;
                    }
                }
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            
        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void label47_Click(object sender, EventArgs e)
        {
            uyg_anasekmeler_tabControl.SelectedTab = uyg_anasayfa_tabPage;
            uyg_anasayfa_altsekme_tabControl.SelectedTab = uyg_ozelteklif_tabPage;
        }

        private void label50_Click(object sender, EventArgs e)
        {
            uyg_anasekmeler_tabControl.SelectedTab = uyg_yardimdestek_tabPage;
            
        }

        private void uygulama_ana_Form_Load(object sender, EventArgs e)
        {
            


            string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT oteladi, ImagePath, yildiz, aciklama, fiyat, konum, bolgekonum FROM otel_onecikan", conn);
                OleDbDataReader reader = cmd.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    GroupBox groupBox = new GroupBox();
                    groupBox.Text = reader["oteladi"].ToString();
                    groupBox.Font = new Font(groupBox.Font.FontFamily, 15); // Yazı tipi boyutunu 15 yap
                    groupBox.Size = groupBox4.Size; // GroupBox4'ün boyutlarını kullan
                    groupBox.Location = new Point(10, i * groupBox4.Height); // Yerleşim için bir örnek

                    // Resmi oluştur ve GroupBox'a ekle
                    PictureBox pictureBox = new PictureBox();
                    string imagePath = Path.Combine(Application.StartupPath, reader["ImagePath"].ToString());
                    pictureBox.ImageLocation = imagePath;
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Resmi PictureBox'a sığdır
                    pictureBox.Size = pictureBox2.Size; // PictureBox2'nin boyutlarını kullan
                    pictureBox.Location = pictureBox2.Location; // PictureBox2'nin konumunu kullan
                    groupBox.Controls.Add(pictureBox);




                    // Yıldız sayısına göre bir Label oluştur
                    Label labelYildiz = new Label();
                    labelYildiz.Text = new String('✩', Convert.ToInt32(reader["yildiz"])); // Yıldız sayısına göre metni ayarla
                    labelYildiz.Location = new Point(464, 235); // LabelYildiz'ın konumunu ayarla
                    labelYildiz.AutoSize = true;
                    labelYildiz.Font = new Font(labelYildiz.Font.FontFamily, 20);
                    // LabelYildiz'ı GroupBox'a ekle
                    groupBox.Controls.Add(labelYildiz);



                    // Label isimleri ve konumları
                    string[] labelNames = { "Fiyat:", "Konumu:", "Bölgesel Konumu:" };
                    Point[] labelLocations = { new Point(465, 120), new Point(465, 158), new Point(465, 202) };

                    for (int j = 0; j < 3; j++)
                    {
                        // Yeni bir Label oluştur
                        Label label = new Label();
                        label.Text = labelNames[j]; // Label'ın metnini ayarla
                        label.Location = labelLocations[j]; // Label'ın konumunu ayarla
                        label.AutoSize = true;
                        label.Font = new Font(label.Font.FontFamily, 15, FontStyle.Bold);
                        // Label'ı GroupBox'a ekle
                        groupBox.Controls.Add(label);
                    }


                    Label labelAciklama = new Label();
                    labelAciklama.Text = reader["aciklama"].ToString(); // Açıklamayı ayarla
                    labelAciklama.Location = new Point(465, 26); // LabelAciklama'nın konumunu ayarla
                    labelAciklama.Size = new Size(738, 75);
                    
                    labelAciklama.Font = new Font(labelAciklama.Font.FontFamily, 15, FontStyle.Bold); // Yazı tipini kalın yap ve boyutunu 15 yap
                    // LabelAciklama'yı GroupBox'a ekle
                    groupBox.Controls.Add(labelAciklama);


                    // Açık olan rezervsayfa_Form form sayısını takip eden bir sayaç oluşturun
                    // Bu değişkeni form seviyesinde tanımlayın
                    int rezervSayfaFormCount = 0;

                    // Rezerve Sayfası butonunu oluştur
                    Button rezerveSayfasiButton = new Button();
                    rezerveSayfasiButton.Text = "Rezerve Sayfası ve Yorum";
                    rezerveSayfasiButton.Location = new Point(498, 279);
                    rezerveSayfasiButton.Size = new Size(693, 47);

                    // Butonun tıklama olayını ayarla
                    rezerveSayfasiButton.Click += (s, args) =>
                    {
                        // Eğer açık olan rezervsayfa_Form form sayısı 3'ten fazla ise
                        if (rezervSayfaFormCount >= 3)
                        {
                            // Kullanıcıya bir mesaj göster
                            MessageBox.Show("Lütfen diğer rezervasyon sayfalarınızdan birini kapatınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        }
                        else
                        {
                            // Yeni bir rezervsayfa_Form formu oluşturun
                            rezervsayfa_Form rezervSayfaForm = new rezervsayfa_Form();
                            rezervSayfaForm.rezervsayfa_oteladi_label.Text = groupBox.Text; // GroupBox'ın metnini ayarla

                            // Form kapatıldığında sayaçtan bir düşür
                            rezervSayfaForm.FormClosed += (bottom, b) => rezervSayfaFormCount--;

                            // giris_kayit_Form formunun bir örneğini oluşturun
                            giris_kayit_Form girisKayitForm = new giris_kayit_Form();

                            // Formu göster ve sayaçı bir artır
                            rezervSayfaForm.Show();
                            rezervSayfaFormCount++;

                            // Formun her zaman en üstte olmasını sağla
                            rezervSayfaForm.TopMost = true;

                            // Veritabanına bağlan
                            OleDbConnection connn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;");
                            connn.Open();

                            // En son giriş yapan müşterinin adını ve soyadını çeken SQL sorgusu
                            string queryy = "SELECT adsoyad FROM musteri WHERE mail = '" + girisKayitForm.EmailText + "'";

                            OleDbCommand cmdd = new OleDbCommand(queryy, connn);
                            OleDbDataReader readerr = cmdd.ExecuteReader();

                            if (readerr.Read()) // Eğer bir sonuç döndüyse
                            {
                                // Müşterinin adını ve soyadını al
                                string adsoyad = readerr["adsoyad"].ToString();

                                // Tarihi al
                                string tarih = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");

                                // Veritabanına yeni bir kayıt eklemek için SQL sorgusu
                                string insertQueryy = "INSERT INTO musteri_son (musteri_adi, otel_adi, tarih) VALUES ('" + adsoyad + "', '" + groupBox.Text + "', '" + tarih + "')";

                                // SQL sorgusunu çalıştır
                                OleDbCommand insertCmdd = new OleDbCommand(insertQueryy, connn);
                                insertCmdd.ExecuteNonQuery();
                            }

                            // Veritabanı bağlantısını kapat
                            connn.Close();
                        }
                    };

                    // Butonu GroupBox'a ekle
                    groupBox.Controls.Add(rezerveSayfasiButton);



                    Label labelFiyat = new Label();
                    labelFiyat.Text = reader["fiyat"].ToString(); // Açıklamayı ayarla
                    labelFiyat.Location = new Point(535, 120); // LabelAciklama'nın konumunu ayarla
                    labelFiyat.AutoSize = true;
                    labelFiyat.Font = new Font(labelFiyat.Font.FontFamily, 15); // Yazı tipini kalın yap ve boyutunu 15 yap
                    // LabelAciklama'yı GroupBox'a ekle
                    groupBox.Controls.Add(labelFiyat);

                    Label labelKonum = new Label();
                    labelKonum.Text = reader["konum"].ToString(); // Açıklamayı ayarla
                    labelKonum.Location = new Point(562, 158); // LabelAciklama'nın konumunu ayarla
                    labelKonum.AutoSize = true;
                    labelKonum.Font = new Font(labelKonum.Font.FontFamily, 15); // Yazı tipini kalın yap ve boyutunu 15 yap
                    // LabelAciklama'yı GroupBox'a ekle
                    groupBox.Controls.Add(labelKonum);

                    Label labelBolgeKonum = new Label();
                    labelBolgeKonum.Text = reader["bolgekonum"].ToString(); // Açıklamayı ayarla
                    labelBolgeKonum.Location = new Point(649, 203); // LabelAciklama'nın konumunu ayarla
                    labelBolgeKonum.AutoSize = true;
                    labelBolgeKonum.Font = new Font(labelBolgeKonum.Font.FontFamily, 15); // Yazı tipini kalın yap ve boyutunu 15 yap
                    // LabelAciklama'yı GroupBox'a ekle
                    groupBox.Controls.Add(labelBolgeKonum);

                    uyg_onecikan_panel.Controls.Add(groupBox);
                    i++;
                }


            }

        }

        private void uygulama_ana_Form_Resize(object sender, EventArgs e)
        {
            

        }
    }
}
