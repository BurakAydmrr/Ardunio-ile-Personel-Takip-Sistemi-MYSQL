using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using MySql.Data.MySqlClient;

namespace personel_takip
{
    public partial class Form1 : Form
    {
        MySqlConnection bag = new MySqlConnection("Server=localhost;Database=ardunio;Uid=root;Pwd=;CharSet=utf8;");

        MySqlCommand kmt = new MySqlCommand();



        public static string portismi, banthizi;
      string[] ports = SerialPort.GetPortNames();

        public Form1()
        {
            InitializeComponent();
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach(string port in ports)
            {
                comboBox1.Items.Add(port);

            }
            comboBox2.Items.Add("2400");
            comboBox2.Items.Add("4500");
            comboBox2.Items.Add("9600");
            comboBox2.Items.Add("5000");
            comboBox2.Items.Add("2400");

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 2;

            label1.Text = "";

        }

     
        private void button1_Click(object sender, EventArgs e)
        {

          //  string kid;


            timer1.Start();
            portismi = comboBox1.Text;
            banthizi = comboBox2.Text;

            try
            {
                serialPort1.PortName = portismi;
                serialPort1.BaudRate = Convert.ToInt16(banthizi);
                serialPort1.Open();
                label1.Text = "Bağlantı Başarılı.";
                label1.ForeColor = Color.Green;
            }
            catch
            {
                serialPort1.Close();
                serialPort1.Open();
                MessageBox.Show("Bağlantı Zaten Açık");
            }
          

        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if(serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                label1.Text = "Bağlantı Kesildi.";
                label1.ForeColor = Color.Red;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string sonuc;
            sonuc = serialPort1.ReadExisting();
            if(sonuc != "")
            {
               kartid.Text = sonuc;

                bag.Open();
                kmt.Connection = bag;
                kmt.CommandText = "SELECT * FROM tablo WHERE kart_id='" + sonuc + "'";

                MySqlDataReader oku = kmt.ExecuteReader();

                if (oku.Read())
                {
                    
                    pictureBox1.Image = Image.FromFile("img\\" + oku["resim"].ToString());
                    label3.Text = oku["adsoyad"].ToString();
                    label4.Text = oku["numara"].ToString();
                    label5.Text = oku["tarih"].ToString();
                }


                bag.Close();
            }
            



        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            if(portismi==null || banthizi == null)
            {
                MessageBox.Show("Bağlantını Kontrol Et.");
            }
            else
            {
                timer1.Stop();
                serialPort1.Close();
                label1.Text = "Bağlantı Kapalı";
                label1.ForeColor = Color.Red;
                kayit kyt = new kayit();
                kyt.ShowDialog();



            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
               
            }
        }





    }
}
