using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardandoFotoTeste
{
    public partial class Form1 : Form
    {
        private string Caminho = @"C:\Users\fabio\Desktop\GuardandoFotoTeste\imagens\";
        private bool OutrosDispositivos;
        private FilterInfoCollection MeusDispositivos;
        private VideoCaptureDevice WebCam;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CarregarDispositivos();
        }
        public void CarregarDispositivos() 
        {
            MeusDispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (MeusDispositivos.Count > 0) 
            {
                OutrosDispositivos = true;
                for(int i = 0; i < MeusDispositivos.Count; i++) 
                    comboBox1.Items.Add(MeusDispositivos[i].Name.ToString());
                comboBox1.Text = MeusDispositivos[0].Name.ToString();
            }
            else 
            {
                OutrosDispositivos = false;
            }
        }
        private void EncerrarWebcam() 
        {
            if (WebCam!=null&&WebCam.IsRunning) 
            {
                WebCam.SignalToStop();
                WebCam = null;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            EncerrarWebcam();
            int i = comboBox1.SelectedIndex;
            string nomeVideo = MeusDispositivos[i].MonikerString;
            WebCam = new VideoCaptureDevice(nomeVideo);
            WebCam.NewFrame += new NewFrameEventHandler(Capturando);
            WebCam.Start();
        }
        private void Capturando(object sender,NewFrameEventArgs eventArgs) 
        {
            Bitmap Imagem = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = Imagem;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            EncerrarWebcam();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = pictureBox1.Image;
            pictureBox2.Image.Save(Caminho+"1.jpg",ImageFormat.Jpeg);
        }
    }
}
