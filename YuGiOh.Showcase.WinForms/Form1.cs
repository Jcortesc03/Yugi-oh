using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace YuGiOh.Showcase.WinForms
{
    public partial class Form1 : Form
    {
        private List<Image> deck = new List<Image>();
        private Dictionary<PictureBox, Image> imagenOriginal = new Dictionary<PictureBox, Image>();
        private PictureBox cartaSeleccionada;

        
        private PictureBox carta1j1, carta2j1, carta3j1, carta4j1, carta5j1;
        private PictureBox carta6j1, carta7j1, carta8j1, carta9j1, carta10j1;
        private PictureBox carta1j2, carta2j2, carta3j2, carta4j2, carta5j2;
        private PictureBox carta6j2, carta7j2, carta8j2, carta9j2, carta10j2;
        private PictureBox Guardarcarta1, Guardarcarta2;
        private PictureBox ataquej1, defensaj1, ataquej2, defensaj2;
        private Button btbocaarriba, btnBocaabajo, btnDetalles, btnJugar, btnsalir;
        private Panel panel1;

        public Form1()
        {
            InitializeComponent();

            
            CrearPanel();
            CrearCartasJugador1();
            CrearCartasJugador2();
            CrearZonasEspeciales();
            CrearBotones();

            ConfigurarFormulario();
            ConfigurarClicks();
            CargarDeck();
            RepartirCartas();
        }

        
        
        
        private void CrearPanel()
        {
            panel1 = new Panel
            {
                BackgroundImage = Properties.Resources.fondo,
                BackgroundImageLayout = ImageLayout.Stretch,
                Dock = DockStyle.Fill
            };
            this.Controls.Add(panel1);
        }

        
        
        private void CrearCartasJugador1()
        {
            // Fila 1 - Mano J1 (arriba)
            carta1j1 = CrearCarta("carta1j1", 250, 15);
            carta2j1 = CrearCarta("carta2j1", 360, 15);
            carta3j1 = CrearCarta("carta3j1", 470, 15);
            carta4j1 = CrearCarta("carta4j1", 580, 15);
            carta5j1 = CrearCarta("carta5j1", 690, 15);

            // Fila 2 - Magias/Trampas J1
            carta6j1 = CrearCarta("carta6j1", 250, 150);
            carta7j1 = CrearCarta("carta7j1", 360, 150);
            carta8j1 = CrearCarta("carta8j1", 470, 150);
            carta9j1 = CrearCarta("carta9j1", 580, 150);
            carta10j1 = CrearCarta("carta10j1", 690, 150);
        }

        
        private void CrearCartasJugador2()
        {
            // Fila 1 - Magias/Trampas J2 (MÁS ARRIBA)
            carta6j2 = CrearCarta("carta6j2", 250, 420);
            carta7j2 = CrearCarta("carta7j2", 360, 420);
            carta8j2 = CrearCarta("carta8j2", 470, 420);
            carta9j2 = CrearCarta("carta9j2", 580, 420);
            carta10j2 = CrearCarta("carta10j2", 690, 420);

            // Fila 2 - Mano J2 (abajo)
            carta1j2 = CrearCarta("carta1j2", 250, 560);
            carta2j2 = CrearCarta("carta2j2", 360, 560);
            carta3j2 = CrearCarta("carta3j2", 470, 560);
            carta4j2 = CrearCarta("carta4j2", 580, 560);
            carta5j2 = CrearCarta("carta5j2", 690, 560);
        }

        
        private void CrearZonasEspeciales()
        {
            // Mazos (lado izquierdo)
            Guardarcarta1 = CrearCarta("Guardarcarta1", 80, 150);
            Guardarcarta2 = CrearCarta("Guardarcarta2", 80, 420);

            // Zonas de ataque/defensa (más a la derecha y separadas)
            ataquej1 = CrearCarta("ataquej1", 900, 260);
            defensaj1 = CrearCarta("defensaj1", 900, 400);
            ataquej2 = CrearCarta("ataquej2", 1020, 260);
            defensaj2 = CrearCarta("defensaj2", 1020, 400);
        }

        // ===============================================
        // CREAR BOTONES
        // ===============================================
        private void CrearBotones()
        {
            btbocaarriba = CrearBoton("Boca Arriba", 1150, 220, Color.White);
            btnBocaabajo = CrearBoton("Boca Abajo", 1150, 295, Color.White);
            btnDetalles = CrearBoton("Detalles", 1150, 380, Color.Gray);
            btnJugar = CrearBoton("Jugar", 1150, 470, Color.LawnGreen);
            btnsalir = CrearBoton("Salir del juego", 1150, 565, Color.OrangeRed);

            // Eventos
            btbocaarriba.Click += btbocaarriba_Click;
            btnBocaabajo.Click += btnBocaabajo_Click_1;
            btnDetalles.Click += btnDetalles_Click_1;
            btnJugar.Click += btnJugar_Click_1;
            btnsalir.Click += (s, e) => Application.Exit();
        }

        // ===============================================
        // MÉTODOS HELPER
        // ===============================================
        private PictureBox CrearCarta(string nombre, int x, int y)
        {
            var pb = new PictureBox
            {
                Name = nombre,
                Location = new Point(x, y),
                Size = new Size(90, 130),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent  // 🔥 SIN BORDES BLANCOS
            };
            panel1.Controls.Add(pb);
            return pb;
        }

        private Button CrearBoton(string texto, int x, int y, Color color)
        {
            var btn = new Button
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(130, 51),
                BackColor = color,
                FlatStyle = FlatStyle.Flat
            };
            this.Controls.Add(btn);
            btn.BringToFront();
            return btn;
        }

        private void ConfigurarFormulario()
        {
            this.ClientSize = new Size(1300, 730);  // 🔥 MÁS ANCHO
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Yu-Gi-Oh! Showcase";
        }

        private void ConfigurarClicks()
        {
            foreach (var pb in panel1.Controls.OfType<PictureBox>())
            {
                pb.Click += (s, e) => { cartaSeleccionada = (PictureBox)s; };
            }
        }

        // ===============================================
        // CARGAR DECK
        // ===============================================
        private void CargarDeck()
        {
            string[] posiblesRutas = new[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "imagenes"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\imagenes"),
                Path.Combine(Application.StartupPath, "imagenes")
            };

            string path = posiblesRutas.FirstOrDefault(Directory.Exists);

            if (path == null)
            {
                MessageBox.Show("No se encontró la carpeta 'imagenes'", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] archivos = Directory.GetFiles(path)
                .Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (archivos.Length == 0)
            {
                MessageBox.Show($"No se encontraron imágenes en:\n{path}");
                return;
            }

            deck.Clear();
            foreach (var archivo in archivos)
            {
                try { deck.Add(Image.FromFile(archivo)); }
                catch { /* Ignorar archivos corruptos */ }
            }
        }

        // ===============================================
        // REPARTIR CARTAS
        // ===============================================
        private void RepartirCartas()
        {
            if (deck.Count == 0)
            {
                MessageBox.Show("⚠️ El deck está vacío. No se pueden repartir cartas.");
                return;
            }

            Random rnd = new Random();

            PictureBox[] mazoJ1 = { carta1j1, carta2j1, carta3j1, carta4j1, carta5j1,
                                    carta6j1, carta7j1, carta8j1, carta9j1, carta10j1 };

            PictureBox[] mazoJ2 = { carta1j2, carta2j2, carta3j2, carta4j2, carta5j2,
                                    carta6j2, carta7j2, carta8j2, carta9j2, carta10j2 };

            // Repartir J1
            var deckJ1 = deck.OrderBy(x => rnd.Next()).Take(10).ToList();
            for (int i = 0; i < mazoJ1.Length && i < deckJ1.Count; i++)
            {
                mazoJ1[i].Image = deckJ1[i];
                imagenOriginal[mazoJ1[i]] = deckJ1[i];
            }

            // Repartir J2
            var deckJ2 = deck.OrderBy(x => rnd.Next()).Take(10).ToList();
            for (int i = 0; i < mazoJ2.Length && i < deckJ2.Count; i++)
            {
                mazoJ2[i].Image = deckJ2[i];
                imagenOriginal[mazoJ2[i]] = deckJ2[i];
            }

            // Imágenes de mazos y zonas
            Guardarcarta1.Image = Properties.Resources.parte_atrasver;
            Guardarcarta2.Image = Properties.Resources.parte_atrasver;
            ataquej1.Image = Properties.Resources.parteatrasmor;
            defensaj1.Image = Properties.Resources.parteatrasgris;
            ataquej2.Image = Properties.Resources.parteatrasmor;
            defensaj2.Image = Properties.Resources.parteatrasgris;
        }

        // ===============================================
        // EVENTOS DE BOTONES
        // ===============================================
        private void btnBocaabajo_Click_1(object sender, EventArgs e)
        {
            if (cartaSeleccionada == null) return;
            cartaSeleccionada.Image = Properties.Resources.parte_atras;
        }

        private void btbocaarriba_Click(object sender, EventArgs e)
        {
            if (cartaSeleccionada == null || !imagenOriginal.ContainsKey(cartaSeleccionada))
                return;

            cartaSeleccionada.Image = imagenOriginal[cartaSeleccionada];
        }

        private void btnDetalles_Click_1(object sender, EventArgs e)
        {
            if (cartaSeleccionada == null || cartaSeleccionada.Image == null)
            {
                MessageBox.Show("Seleccione una carta primero", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            carta ver = new carta(cartaSeleccionada.Image);
            ver.Show();
        }

        private void btnJugar_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("¡Comienza el duelo! ⚔️", "Yu-Gi-Oh",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}