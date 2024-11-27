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
using static OtelRezerved_Network.giris_kayit_Form;
using static System.Windows.Forms.LinkLabel;

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
            uyg_kullanici_kim_label.Text =TextYazi.Yazi;

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

                    // Rezerve Sayfası butonunu oluştur
                    Button rezerveSayfasiButton = new Button();
                    rezerveSayfasiButton.Text = "Rezerve Sayfası";
                    rezerveSayfasiButton.Location = new Point(498, 279);
                    rezerveSayfasiButton.Size = new Size(693, 47);

                    // Butonun tıklama olayını ayarla
                    rezerveSayfasiButton.Click += (s, args) =>
                    {
                        // Sender'ın bir Button olduğunu kontrol et
                        if (s is Button button)
                        {
                            // Butonun hangi GroupBox'un içinde olduğunu bul
                            GroupBox groupBoxx = button.Parent as GroupBox;

                            if (groupBoxx != null)
                            {
                                // GroupBox'un text'ini al
                                string otelAdi = groupBoxx.Text;

                                // kullanici_kim_label'deki metni al
                                string musteriAdi = uyg_kullanici_kim_label.Text;

                                rezervsayfa_Form rezervSayfaForm = new rezervsayfa_Form();
                                rezervSayfaForm.rezervsayfa_oteladi_label.Text = otelAdi; // GroupBox'ın metnini ayarla
                                rezervSayfaForm.Show(); // Formu göster

                                try
                                {
                                    // Veritabanı bağlantısını oluştur
                                    string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
                                    using (OleDbConnection connn = new OleDbConnection(connString))
                                    {
                                        connn.Open();

                                        // Veritabanına kaydet
                                        string query = "INSERT INTO musteri_son (otel_adi, musteri_adi, tarih) VALUES (@otelAdi, @musteriAdi, @tarih)";
                                        using (OleDbCommand cmdd = new OleDbCommand(query, connn))
                                        {
                                            cmdd.Parameters.AddWithValue("@otelAdi", otelAdi);
                                            cmdd.Parameters.AddWithValue("@musteriAdi", musteriAdi);
                                            cmdd.Parameters.AddWithValue("@tarih", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                            cmdd.ExecuteNonQuery();
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    // Hata mesajını görüntüle
                                    MessageBox.Show("Veritabanına veri eklenirken bir hata oluştu: " + ex.Message);
                                }
                            }
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


            // Timer kontrolünü oluştur
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

            // Timer kontrolünün Tick olayını ayarla
            timer1.Tick += (s, args) =>
            {
                // Veritabanı bağlantısını oluştur
                string connnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
                using (OleDbConnection connn = new OleDbConnection(connnString))
                {
                    connn.Open();

                    // Veritabanından veriyi al
                    string query = "SELECT * FROM musteri_son WHERE musteri_adi = @MusteriAdi";
                    OleDbDataAdapter da = new OleDbDataAdapter(query, connn);
                    da.SelectCommand.Parameters.AddWithValue("@MusteriAdi", uyg_kullanici_kim_label.Text);

                    // Veriyi DataTable'a doldur
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // ListView'i temizle
                    uyg_son_listView.Items.Clear();

                    // DataTable'daki veriyi ListView'e ekle
                    foreach (DataRow row in dt.Rows)
                    {
                        ListViewItem item = new ListViewItem(row["otel_adi"].ToString());
                        item.SubItems.Add(row["tarih"].ToString());
                        uyg_son_listView.Items.Add(item);
                    }
                }
            };

            // Timer kontrolünün aralığını ayarla (örneğin, her 5 saniyede bir güncelle)
            timer1.Interval = 5000;

            // Timer kontrolünü başlat
            timer1.Start();


        }

        private void uygulama_ana_Form_Resize(object sender, EventArgs e)
        {
            

        }

        private void kullanici_kim_label_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void uyg_seciliziyaret_button_Click(object sender, EventArgs e)
        {
          
        }
    }

}
