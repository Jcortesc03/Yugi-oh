using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using System;
using System.IO;
using System.Windows.Forms;

namespace YuGiOh.Showcase.WinForms
{
    public partial class Form2 : Form
    {
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;

        public Form2()
        {
            InitializeComponent();
            Core.Initialize();

            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);

            videoView1.MediaPlayer = _mediaPlayer;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string videoPath = Path.Combine(Application.StartupPath, "imagenes", "intro.mp4");

            if (!File.Exists(videoPath))
            {
                MessageBox.Show($"Archivo de video no encontrado:\n{videoPath}\n\nAsegúrate de que el archivo esté en la carpeta de salida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var media = new Media(_libVLC, videoPath, FromType.FromPath);
            _mediaPlayer.Play(media);

            _mediaPlayer.EndReached += (s, args) =>
            {
                this.Invoke(new Action(() =>
                {
                    new Form1().Show();
                    this.Hide();
                }));
            };
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            _mediaPlayer?.Stop();
            _mediaPlayer?.Dispose();
            _libVLC?.Dispose();
        }
    }
}