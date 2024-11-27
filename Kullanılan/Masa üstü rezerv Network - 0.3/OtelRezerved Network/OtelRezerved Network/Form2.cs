using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;



namespace OtelRezerved_Network
{

    public partial class Form2 : Form
    {


        public Form2()
        {
            InitializeComponent();


        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // tabPage2'den tabPage1'ye geçiş yapar
            tabControl1.SelectedTab = tabPage1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // tabPage1'den tabPage2'ye geçiş yapar
            tabControl1.SelectedTab = tabPage2;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Form2'nin bir örneğini oluşturun
            Form1 form1 = new Form1();

            // Form2'yi gösterin
            form1.Show();

            // İsteğe bağlı olarak, mevcut formu gizleyebilirsiniz
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
