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
                OleDbCommand cmd = new OleDbCommand("SELECT oteladi, ImagePath FROM otel_onecikan", conn);
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
                    pictureBox.ImageLocation = reader["ImagePath"].ToString();
                    pictureBox.Size = pictureBox2.Size; // PictureBox4'ün boyutlarını kullan
                    pictureBox.Location = pictureBox2.Location; // PictureBox4'ün konumunu kullan
                    groupBox.Controls.Add(pictureBox);


                    // Yıldız sayısına göre bir Label oluştur
                    Label labelYildiz = new Label();
                    labelYildiz.Text = new String('✩', Convert.ToInt16(reader["yildiz"])); // Yıldız sayısına göre metni ayarla
                    labelYildiz.Location = new Point(464, 235); // LabelYildiz'ın konumunu ayarla
                    labelYildiz.AutoSize = true;

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

                        // Label'ı GroupBox'a ekle
                        groupBox.Controls.Add(label);
                    }





                    uyg_onecikan_panel.Controls.Add(groupBox);
                    i++;
                }


            }

        }
    }
}
