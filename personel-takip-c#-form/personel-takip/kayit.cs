using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace personel_takip
{
    public partial class kayit : Form
    {
        MySqlConnection bag= new MySqlConnection("Server=localhost;Database=ardunio;Uid=root;Pwd=;CharSet=utf8;");

       MySqlCommand kmt = new MySqlCommand();


        public kayit()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string sonuc;
            sonuc = serialPort1.ReadExisting();
            if (sonuc != "")
            {
                kartid.Text = sonuc;
            }
        }

        private void kayitcs_Load(object sender, EventArgs e)
        {
            serialPort1.PortName = Form1.portismi;
            serialPort1.BaudRate = Convert.ToInt16(Form1.banthizi);


            if(serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.Open();
                    label6.Text = "Bağlantı Sağlandı";
                    label6.ForeColor = Color.Green;
                }
                catch
                {
                    label6.Text = "Bağlantı Sağlanamadı.";
                }
              

            }
            else
            {
                label6.Text = "Bağlantı Sağlanmadı";
                label6.ForeColor = Color.Red;

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Start();
            kartid.Text = "-------------------------------";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            label7.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyaları (jpg) | *.jpg|Tüm Dosyalar | *.*";
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\img";
            dosya.RestoreDirectory = true;

            if(dosya.ShowDialog() == DialogResult.OK)
            {
                string di = dosya.SafeFileName;
                textBox3.Text = di;
            }

    
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (kartid.Text == "-------------------------------" || textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == ""){
                label7.Text = "Bilgileri Doğru Girdiğine Emin Ol.";
                label7.ForeColor = Color.Red;
            }
            else
            {

                try
                {
                    bag.Open();

                    kmt.Connection = bag;
                    kmt.CommandText = "INSERT INTO tablo (kart_id,adsoyad,numara,resim) VALUES  ('" + kartid.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
                    kmt.ExecuteNonQuery();
                    label7.Text = "Kayıt Yapıldı";
                    label7.ForeColor = Color.Green;

                    bag.Close();
                }
                catch
                {
                    bag.Close();
                    MessageBox.Show("KART ZATEN KAYITLI");


                }
            
            }
                   
           
               
            
          

      


        }

        private void kayit_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            serialPort1.Close();

        }
    }
}
