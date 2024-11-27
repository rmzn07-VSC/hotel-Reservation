using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Text;
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










        private void button1_Click(object sender, EventArgs e)
        {

            // Panel'in kaydırma konumunu sıfırla
            uyg_onecikan_panel.AutoScrollPosition = new Point(0, 0);

            string searchText = string.IsNullOrEmpty(uyg_onecikan_oteladi_textBox.Text) ? null : uyg_onecikan_oteladi_textBox.Text;
            string selectedKonum = uyg_konum_comboBox.SelectedItem != null ? uyg_konum_comboBox.SelectedItem.ToString() : null;

            string selectedYildiz = null;
            if (uyg_onecikan_yildiz5_radioButton.Checked)
                selectedYildiz = "✩✩✩✩✩";
            else if (uyg_onecikan_yildiz4_radioButton.Checked)
                selectedYildiz = "✩✩✩✩";
            else if (uyg_onecikan_yildiz3_radioButton.Checked)
                selectedYildiz = "✩✩✩";
            else if (uyg_onecikan_yildiz2_radioButton.Checked)
                selectedYildiz = "✩✩";
            else if (uyg_onecikan_yildiz1_radioButton.Checked)
                selectedYildiz = "✩";

            List<Control> matchedControls = new List<Control>();  // matchedControls değişkenini burada tanımlıyoruz

            foreach (Control control in uyg_onecikan_panel.Controls)
            {
                if (control is GroupBox groupBox && (searchText == null || groupBox.Text.Contains(searchText)))
                {
                    bool match = false;
                    if (selectedKonum != null || selectedYildiz != null)
                    {
                        foreach (Control childControl in groupBox.Controls)
                        {
                            if (childControl is Label label)
                            {
                                if (!match && selectedKonum != null && label.Text == selectedKonum)
                                    match = true;
                                else if (!match && selectedYildiz != null && label.Text == selectedYildiz && label.Location == new Point(464, 235))
                                    match = true;
                            }
                        }
                    }
                    else
                    {
                        match = true;
                    }

                    if (match)
                        matchedControls.Add(control);
                }
            }

            // matchedControls listesindeki GroupBox'ları görünür yap, diğerlerini gizle
            foreach (Control control in uyg_onecikan_panel.Controls)
            {
                if (control is GroupBox groupBox)
                {
                    if (matchedControls.Contains(control))
                    {
                        groupBox.Visible = true;
                    }
                    else
                    {
                        groupBox.Visible = false;
                    }
                }
            }

            double minFiyat = 0, maxFiyat = double.MaxValue;

            // Min ve Max fiyatları çözümle
            if (!string.IsNullOrEmpty(uyg_onecikan_minfiyat_textBox.Text))
            {
                if (!double.TryParse(uyg_onecikan_minfiyat_textBox.Text, out minFiyat))
                {
                    MessageBox.Show("Lütfen geçerli bir minimum fiyat girin!");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(uyg_onecikan_maxfiyat_textBox.Text))
            {
                if (!double.TryParse(uyg_onecikan_maxfiyat_textBox.Text, out maxFiyat))
                {
                    MessageBox.Show("Lütfen geçerli bir maksimum fiyat girin!");
                    return;
                }
            }

            // Min fiyatın Max fiyattan büyük olup olmadığını kontrol et
            if (minFiyat > maxFiyat)
            {
                MessageBox.Show("Minimum fiyat maximum fiyattan yüksek olamaz!");
                return;
            }

            int yPosition = 0; // Başlangıç konumu
            foreach (Control control in matchedControls)
            {
                if (control is GroupBox groupBox && groupBox.Tag != null)
                {
                    double fiyat = (double)groupBox.Tag;
                    if (fiyat >= minFiyat && fiyat <= maxFiyat)
                    {
                        bool matchRadioButton = true;
                        if (uyg_onecikan_sehirmerkez_radioButton.Checked || uyg_onecikan_plajyakin_radioButton.Checked || uyg_onecikan_havaalaniyakin_radioButton.Checked)
                        {
                            matchRadioButton = false;
                            foreach (Control childControl in groupBox.Controls)
                            {
                                if (childControl is Label label && label.Location == new Point(649, 203))
                                {
                                    if ((uyg_onecikan_sehirmerkez_radioButton.Checked && label.Text == "Şehir Merkezinde") ||
                                        (uyg_onecikan_plajyakin_radioButton.Checked && label.Text == "Plaj Yakınında") ||
                                        (uyg_onecikan_havaalaniyakin_radioButton.Checked && label.Text == "Havaalanı Yakınında"))
                                    {
                                        matchRadioButton = true;
                                        break;
                                    }
                                }
                            }
                        }

                        if (matchRadioButton)
                        {
                            // Fiyat aralığına ve RadioButton durumlarına uygun olanları göster
                            groupBox.Visible = true;
                            groupBox.Location = new Point(groupBox.Location.X, yPosition);
                            yPosition += groupBox.Height + 10; // Her bir GroupBox arasında 10 piksel boşluk bırakır
                        }
                        else
                        {
                            // Fiyat aralığına ve RadioButton durumlarına uymayanları gizle
                            groupBox.Visible = false;
                        }
                    }
                    else
                    {
                        // Fiyat aralığına uymayanları gizle
                        groupBox.Visible = false;
                    }
                }
            }



            // groupBox4'ün görünürlüğünü false olarak ayarla
            groupBox4.Visible = false;


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

        }

        private void label50_Click(object sender, EventArgs e)
        {
            uyg_anasekmeler_tabControl.SelectedTab = uyg_yardimdestek_tabPage;
        }

        private void uygulama_ana_Form_Load(object sender, EventArgs e)
        {
            try
            {

                uyg_kullanici_kim_label.Text = TextYazi.Yazi;

                this.uyg_son_listView.SelectedIndexChanged += new System.EventHandler(this.uyg_son_listView_SelectedIndexChanged);

                string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    goster_musteri_yorumm();
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand("SELECT oteladi, ImagePath, yildiz, aciklama, fiyat, konum, bolgekonum FROM otel_onecikan", conn);
                    OleDbDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    while (reader.Read())
                    {
                        // GroupBox oluşturma kısmı
                        GroupBox groupBox = new GroupBox();
                        groupBox.Text = reader["oteladi"].ToString();
                        groupBox.Font = new Font(groupBox.Font.FontFamily, 15); // Yazı tipi boyutunu 15 yap
                        groupBox.Size = groupBox4.Size; // GroupBox4'ün boyutlarını kullan
                        groupBox.Location = new Point(10, i * groupBox4.Height); // Yerleşim için bir örnek


                        Label labelFiyat = new Label();
                        labelFiyat.Text = reader["fiyat"].ToString() + "$"; // Açıklamayı ayarla
                        labelFiyat.Location = new Point(599, 120); // LabelAciklama'nın konumunu ayarla
                        labelFiyat.AutoSize = true;
                        labelFiyat.Font = new Font(labelFiyat.Font.FontFamily, 15); // Yazı tipini kalın yap ve boyutunu 15 yap
                        groupBox.Controls.Add(labelFiyat); // LabelAciklama'yı GroupBox'a ekle

                        double fiyat = double.Parse(reader["fiyat"].ToString());
                        groupBox.Tag = fiyat; // Fiyatı groupBox.Tag özelliğine ata


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
                        string[] labelNames = { "Günlük Fiyat:", "Konumu:", "Bölgesel Konumu:" };
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
                        int openFormCount = 0;
                        // Butonun tıklama olayını ayarla
                        rezerveSayfasiButton.Click += (s, args) =>
                        {
                            goster_musteri_yorumm();
                            Global.KullaniciKimText = uyg_kullanici_kim_label.Text;
                            // Aynı anda sadece 3 form açılabilir
                            if (openFormCount >= 3)
                            {
                                MessageBox.Show("Önceki Rezerve Sayfalarını Kapatınız");
                                return;
                            }

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

                                    // GroupBox içindeki belirli konumdaki Label'ı bul
                                    Label gunlukFiyatLabel = groupBoxx.Controls.OfType<Label>().FirstOrDefault(l => l.Location == new Point(599, 120));

                                    rezervsayfa_Form rezervSayfaForm = new rezervsayfa_Form();
                                    rezervSayfaForm.rezervsayfa_oteladi_label.Text = otelAdi; // GroupBox'ın metnini ayarla

                                    // Eğer Label bulunduysa, metnini rezervsayfa_gunlukfiyat_label'a yazdır
                                    if (gunlukFiyatLabel != null)
                                    {
                                        // Label'ın metnini al ve sonundaki dolar işaretini kaldır
                                        string gunlukFiyat = gunlukFiyatLabel.Text.Replace("$", "");
                                        rezervSayfaForm.rezervsayfa_gunlukfiyat_label.Text = gunlukFiyat;
                                    }

                                    rezervSayfaForm.FormClosed += (senderr, a) => openFormCount--; // Form kapatıldığında sayaçtan bir düşür
                                    rezervSayfaForm.Show(); // Formu göster
                                    openFormCount++; // Açık form sayısını artır

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

                                            // Veritabanı bağlantısını kapat
                                            connn.Close();
                                        }

                                        // ListView'i güncelle
                                        UpdateListView();

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
                    goster_musteri_yorumm();




                }



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



            }

            finally
            {
                goster_musteri_rezerv();
                goster_musteri_yorumm();
                goster_musteri_fav();
            }


        }

        private void uygulama_ana_Form_Resize(object sender, EventArgs e)
        {


        }

        private void kullanici_kim_label_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM musteri_son WHERE otel_adi = @otelAdi";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@otelAdi", uyg_oteladibilgi_label.Text);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void uyg_seciliziyaret_button_Click(object sender, EventArgs e)
        {
            string otelAdi = uyg_oteladibilgi_label.Text;

            // kullanici_kim_label'deki metni al
            string musteriAdi = uyg_kullanici_kim_label.Text;

            rezervsayfa_Form rezervSayfaForm = new rezervsayfa_Form();
            rezervSayfaForm.rezervsayfa_oteladi_label.Text = otelAdi; // GroupBox'ın metnini ayarla
            rezervSayfaForm.Show(); // Formu göster
        }

        private void uyg_son_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uyg_son_listView.SelectedItems.Count > 0)
            {
                // Seçili öğenin otel adını al
                string otelAdi = uyg_son_listView.SelectedItems[0].Text;

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
                        uyg_oteladibilgi_label.Text = reader["oteladi"].ToString();
                        uyg_aciklamabilgi_richTextBox.Text = reader["aciklama"].ToString();
                        uyg_fiyatbilgi_label.Text = reader["fiyat"].ToString();
                        uyg_konumbilgi_label.Text = reader["konum"].ToString();
                        uyg_bolgekonumbilgi_label.Text = reader["bolgekonum"].ToString();
                        uyg_yildizbilgi_label.Text = reader["yildiz"].ToString();
                    }
                }
            }
        }


        void UpdateListView()
        {
            // Veritabanı bağlantısını oluştur
            string connnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
            using (OleDbConnection connn = new OleDbConnection(connnString))
            {
                connn.Open();

                // Veritabanından veriyi al
                string query = "SELECT otel_adi, tarih FROM musteri_son WHERE musteri_adi = @MusteriAdi";
                OleDbCommand cmd = new OleDbCommand(query, connn);
                cmd.Parameters.AddWithValue("@MusteriAdi", uyg_kullanici_kim_label.Text);

                // Veriyi DataTable'a doldur
                OleDbDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

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
        }


        private void uyg_gecmisisil_button_Click(object sender, EventArgs e)
        {
            // Veritabanı bağlantısını oluştur
            string connnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
            using (OleDbConnection connn = new OleDbConnection(connnString))
            {
                connn.Open();

                // Veritabanından ilgili kaydı sil
                string query = "DELETE FROM musteri_son WHERE musteri_adi = @musteriAdi";
                using (OleDbCommand cmd = new OleDbCommand(query, connn))
                {
                    cmd.Parameters.AddWithValue("@musteriAdi", uyg_kullanici_kim_label.Text);
                    cmd.ExecuteNonQuery();
                }
                // Veritabanından ilgili kaydı sil
                string queryy = "DELETE FROM musteri_son WHERE musteri_adi = @musteriAdi";
                using (OleDbCommand cmd = new OleDbCommand(queryy, connn))
                {
                    cmd.Parameters.AddWithValue("@musteriAdi", uyg_kullanici_kim_label.Text);
                    cmd.ExecuteNonQuery();
                }

                // ListView'i güncelle
                UpdateListView();
            }
        }

        private void uyg_onecikan_sifirla_button_Click(object sender, EventArgs e)
        {
            ResetControls(uyg_onecikan_filtreler_panel);

            // GroupBox4 hariç tüm GroupBox'ları göster
            int yPosition = 0; // Başlangıç konumu
            foreach (Control control in uyg_onecikan_panel.Controls)
            {
                if (control is GroupBox groupBox && groupBox != groupBox4)
                {
                    groupBox.Visible = true;
                    groupBox.Location = new Point(groupBox.Location.X, yPosition);
                    yPosition += groupBox.Height + 10; // Her bir GroupBox arasında 10 piksel boşluk bırakır
                }
            }
        }

        private void ResetControls(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                if (control is RadioButton)
                {
                    ((RadioButton)control).Checked = false;
                }
                else if (control is ComboBox)
                {
                    ((ComboBox)control).SelectedIndex = -1;
                }
                else if (control is TextBox)
                {
                    ((TextBox)control).Clear();
                }
                else if (control is GroupBox)
                {
                    ResetControls(control); // Recursive call for GroupBox
                }
            }
        }

        private void uyg_onecikan_maxfiyat_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Boşlukları ve harfleri engelle
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void uyg_onecikan_minfiyat_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Boşlukları ve harfleri engelle
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void uyg_rezerv_rezervlerim_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                uyg_rezerv_oteladi_textBox.Text = uyg_rezerv_rezervlerim_listView.SelectedItems[0].SubItems[0].Text.ToString();
                uyg_rezerv_tarih_textBox.Text = uyg_rezerv_rezervlerim_listView.SelectedItems[0].SubItems[1].Text.ToString();
                uyg_rezerv_fiyat_textBox.Text = uyg_rezerv_rezervlerim_listView.SelectedItems[0].SubItems[2].Text.ToString();
            } catch { }
        }


        public void goster_musteri_yorumm()
        {

            // Veritabanı bağlantısını oluştur
            string connnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
            using (OleDbConnection connn = new OleDbConnection(connnString))
            {
                connn.Open();

                // Veritabanından veriyi al
                string query = "SELECT otel_adi, yorum FROM musteri_yorum WHERE musteri_adi = @MusteriAdi";
                OleDbCommand cmd = new OleDbCommand(query, connn);
                cmd.Parameters.AddWithValue("@MusteriAdi", uyg_kullanici_kim_label.Text);

                // Veriyi DataTable'a doldur
                OleDbDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                // ListView'i temizle
                uyg_yorumlarım_listView.Items.Clear();

                // DataTable'daki veriyi ListView'e ekle
                foreach (DataRow row in dt.Rows)
                {
                    ListViewItem item = new ListViewItem(row["otel_adi"].ToString());
                    item.SubItems.Add(row["yorum"].ToString());
                    uyg_yorumlarım_listView.Items.Add(item);
                }
            }

        }

        public void goster_musteri_rezerv()
        {

            // Veritabanı bağlantısını oluştur
            string connnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
            using (OleDbConnection connn = new OleDbConnection(connnString))
            {
                connn.Open();

                // Veritabanından veriyi al
                string query = "SELECT otel_adi, tarih, ucret FROM musteri_rezerv WHERE musteri_adi = @MusteriAdi";
                OleDbCommand cmd = new OleDbCommand(query, connn);
                cmd.Parameters.AddWithValue("@MusteriAdi", uyg_kullanici_kim_label.Text);

                // Veriyi DataTable'a doldur
                OleDbDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                // ListView'i temizle
                uyg_rezerv_rezervlerim_listView.Items.Clear();

                // DataTable'daki veriyi ListView'e ekle
                foreach (DataRow row in dt.Rows)
                {
                    ListViewItem item = new ListViewItem(row["otel_adi"].ToString());
                    item.SubItems.Add(row["tarih"].ToString());
                    item.SubItems.Add(row["ucret"].ToString());
                    uyg_rezerv_rezervlerim_listView.Items.Add(item);
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            goster_musteri_yorumm();
        }

        private void uyg_yorumlarım_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                oteladine_label.Text = uyg_yorumlarım_listView.SelectedItems[0].SubItems[0].Text.ToString();
                uyg_yorumum_richTextBox.Text = uyg_yorumlarım_listView.SelectedItems[0].SubItems[1].Text.ToString();
                mevcutyorum_richTextBox.Text = uyg_yorumlarım_listView.SelectedItems[0].SubItems[1].Text.ToString();
            }
            catch { }

        }

        private void guncelle_button_Click(object sender, EventArgs e)
        {
            try
            {


                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE musteri_yorum SET yorum = ? WHERE musteri_adi = ? AND otel_adi = ? AND yorum = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@yorum", uyg_yorumum_richTextBox.Text);
                        command.Parameters.AddWithValue("@musteri_adi", uyg_kullanici_kim_label.Text);
                        command.Parameters.AddWithValue("@otel_adi", oteladine_label.Text);
                        command.Parameters.AddWithValue("@eski_yorum", mevcutyorum_richTextBox.Text);
                        command.ExecuteNonQuery();
                        goster_musteri_yorumm();
                        temizle_yorum();
                    }
                }


            }
            finally
            {
                goster_musteri_yorumm();
                temizle_yorum();
            }
        }
        public void temizle_yorum()
        {
            uyg_yorumum_richTextBox.Clear();
            mevcutyorum_richTextBox.Clear();
            oteladine_label.Text = "";


        }
        public void temizle_rezerve()
        {
            uyg_rezerv_fiyat_textBox.Clear();
            gunlukfiyat_textBox.Clear();
            uyg_rezerv_tarih_textBox.Clear();
            uyg_rezerv_oteladi_textBox.Clear();


        }

        private void sil_button_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM musteri_yorum WHERE musteri_adi = ? AND otel_adi = ? AND yorum = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@musteri_adi", uyg_kullanici_kim_label.Text);
                        command.Parameters.AddWithValue("@otel_adi", oteladine_label.Text);
                        command.Parameters.AddWithValue("@yorum", mevcutyorum_richTextBox.Text);
                        command.ExecuteNonQuery();
                        goster_musteri_yorumm();
                        temizle_yorum();
                    }
                }
            }
            finally
            {
                goster_musteri_yorumm();
                temizle_yorum();
            }
            
        }

       

       

        
       

        private void uyg_rezerv_oteladi_textBox_TextChanged(object sender, EventArgs e)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT fiyat FROM otel_onecikan WHERE oteladi = ?";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@oteladi", uyg_rezerv_oteladi_textBox.Text);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            gunlukfiyat_textBox.Text = reader["fiyat"].ToString();
                        }
                    }
                }
            }
        }

        private void rezervyinele_button_Click(object sender, EventArgs e)
        {
            goster_musteri_rezerv();
        }

        private void uyg_rezerv_sil_button_Click(object sender, EventArgs e)
        {
            try
            {
                // Veritabanı bağlantısını oluştur
                string connnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
                using (OleDbConnection connn = new OleDbConnection(connnString))
                {
                    connn.Open();

                    // Veritabanından veriyi sil
                    string query = "DELETE FROM musteri_rezerv WHERE otel_adi = ? AND tarih = ?";
                    OleDbCommand cmd = new OleDbCommand(query, connn);
                    cmd.Parameters.AddWithValue("@otel_adi", uyg_rezerv_oteladi_textBox.Text);
                    cmd.Parameters.AddWithValue("@tarih", uyg_rezerv_tarih_textBox.Text);

                    // Komutu çalıştır
                    cmd.ExecuteNonQuery();

                    // ListView'i temizle

                }
            }
            finally
            {
                uyg_rezerv_rezervlerim_listView.Items.Clear();
                goster_musteri_rezerv();
                temizle_rezerve();
            }
        }

        private void uyg_rezerv_myfav_tabPage_Click(object sender, EventArgs e)
        {

        }

        public void goster_musteri_fav()
        {

            // Veritabanı bağlantısını oluştur
            string connnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
            using (OleDbConnection connn = new OleDbConnection(connnString))
            {
                connn.Open();

                // Veritabanından veriyi al
                string query = "SELECT fav_otel FROM musteri_fav WHERE musteri_adi = @MusteriAdi";
                OleDbCommand cmd = new OleDbCommand(query, connn);
                cmd.Parameters.AddWithValue("@MusteriAdi", uyg_kullanici_kim_label.Text);

                // Veriyi DataTable'a doldur
                OleDbDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                // ListView'i temizle
                musteri_fav_listView.Items.Clear();

                // DataTable'daki veriyi ListView'e ekle
                foreach (DataRow row in dt.Rows)
                {
                    ListViewItem item = new ListViewItem(row["fav_otel"].ToString());
                    musteri_fav_listView.Items.Add(item);
                }
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            goster_musteri_fav();
        }

        private void musteri_fav_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                oteladi_fav_textBox.Text = musteri_fav_listView.SelectedItems[0].SubItems[0].Text.ToString();
              

            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Veritabanı bağlantısını oluştur
            string connnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\otel.mdb;";
            using (OleDbConnection connn = new OleDbConnection(connnString))
            {
                connn.Open();

                // Veritabanından veriyi sil
                string query = "DELETE FROM musteri_fav WHERE musteri_adi = ? AND fav_otel = ?";
                OleDbCommand cmd = new OleDbCommand(query, connn);
                cmd.Parameters.AddWithValue("@musteri_adi", uyg_kullanici_kim_label.Text);
                cmd.Parameters.AddWithValue("@fav_otel", oteladi_fav_textBox.Text);

                // Komutu çalıştır
                cmd.ExecuteNonQuery();
            }
        }

       
    }

}
