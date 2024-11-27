using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelRezerved_Network
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Panel'in kaydırma konumunu sıfırla
            panel1.AutoScrollPosition = new Point(0, 0);

            string searchText = textBox1.Text;
            List<Control> matchedControls = new List<Control>();

            foreach (Control control in panel1.Controls)
            {
                if (control is GroupBox && ((GroupBox)control).Text.Contains(searchText))
                {
                    matchedControls.Add(control);
                }
            }

            int yPosition = 10; // Başlangıç konumu

            foreach (Control control in panel1.Controls)
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
            panel3.AutoScrollPosition = new Point(0, 0);

            string searchText = textBox2.Text;
            List<Control> matchedControls = new List<Control>();

            foreach (Control control in panel3.Controls)
            {
                if (control is GroupBox && ((GroupBox)control).Text.Contains(searchText))
                {
                    matchedControls.Add(control);
                }
            }

            int yPosition = 10; // Başlangıç konumu

            foreach (Control control in panel3.Controls)
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
            tabControl1.SelectedTab = tabPage2;
            
        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void label47_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            tabControl2.SelectedTab = tabPage7;
        }

        private void label50_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
            
        }
    }
}
