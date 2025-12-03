using System;
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

        public Form1()
        {
            InitializeComponent();
            AjustarTamanos();
            PosicionarCartas();
            ConfigurarClicks();
            CargarDeck();
            RepartirCartas();
        }

        private PictureBox cartaSeleccionada;

        private void ConfigurarClicks()
        {
            foreach (var pb in this.Controls.OfType<PictureBox>())
            {
                pb.Click += (s, e) =>
                {
                    cartaSeleccionada = (PictureBox)s;
                };
            }

            btnsalir.Click += (s, e) => Application.Exit();
        }


        // -----------------------------------------
        // CARGA TODAS LAS CARTAS DE LA CARPETA
        // -----------------------------------------


        private void CargarDeck()
        {
            string path = Path.Combine(Application.StartupPath, "imagenes");

            if (!Directory.Exists(path))
            {
                MessageBox.Show("La carpeta 'imagenes' no existe:\n" + path);
                return;
            }

            string[] archivos = Directory.GetFiles(path)
                .Where(f =>
                    f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                    f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                    f.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (archivos.Length == 0)
            {
                MessageBox.Show("No se encontraron imágenes válidas en:\n" + path);
                return;
            }

            deck.Clear();

            foreach (var archivo in archivos)
            {
                try
                {
                    deck.Add(Image.FromFile(archivo));
                }
                catch { }
            }
        }






        // -----------------------------------------
        // REPARTE CARTAS ALEATORIAS AL TABLERO
        // -----------------------------------------
        private void RepartirCartas()
        {
            Random rnd = new Random();

            // SOLO cartas de jugador 1 y 2
            PictureBox[] cartasJugadores =
            {
        // Jugador 1
        carta1j1, carta2j1, carta3j1, carta4j1, carta5j1,
        carta6j1, carta7j1, carta8j1, carta9j1, carta10j1,

        // Jugador 2
        carta1j2, carta2j2, carta3j2, carta4j2, carta5j2,
        carta6j2, carta7j2, carta8j2, carta9j2, carta10j2
    };

            if (deck.Count == 0)
            {
                MessageBox.Show("El deck está vacío.");
                return;
            }

            foreach (PictureBox pb in cartasJugadores)
            {
                int index = rnd.Next(deck.Count);
                pb.Image = deck[index];

                // Guardar la imagen REAL
                imagenOriginal[pb] = deck[index];
            }


            // sEstos van vacíos o con parte de atrás:
            ataquej1.Image = Properties.Resources.parteatrasmor;
            defensaj1.Image = Properties.Resources.parteatrasgris;

            ataquej2.Image = Properties.Resources.parteatrasmor;
            defensaj2.Image = Properties.Resources.parteatrasgris;
        }


        private void PosicionarCartas()
        {
            int startX = 260;   // posición horizontal inicial
            int espacio = 110;  // espacio entre cartas

            // FILA: cartas en mano jugador 1
            PictureBox[] manoJ1 = { carta1j1, carta2j1, carta3j1, carta4j1, carta5j1 };

            // FILA: magias/trampas jugador 1
            PictureBox[] magiasJ1 = { carta6j1, carta7j1, carta8j1, carta9j1, carta10j1 };

            // FILA: magias/trampas jugador 2
            PictureBox[] magiasJ2 = { carta6j2, carta7j2, carta8j2, carta9j2, carta10j2 };

            // FILA: cartas en mano jugador 2
            PictureBox[] manoJ2 = { carta1j2, carta2j2, carta3j2, carta4j2, carta5j2 };

            // UBICAR MANO J1 (PRIMERA FILA)
            for (int i = 0; i < 5; i++)
                manoJ1[i].Location = new Point(startX + i * espacio, 20);

            // UBICAR MAGIAS/T J1 (SEGUNDA FILA)
            for (int i = 0; i < 5; i++)
                magiasJ1[i].Location = new Point(startX + i * espacio, 160);

            // UBICAR MAGIAS/T J2 (CUARTA FILA)
            for (int i = 0; i < 5; i++)
                magiasJ2[i].Location = new Point(startX + i * espacio, 680);

            // UBICAR MANO J2 (QUINTA FILA)
            for (int i = 0; i < 5; i++)
                manoJ2[i].Location = new Point(startX + i * espacio, 830);

            // CARTAS “MAZO”
            Guardarcarta1.Location = new Point(60, 160);
            Guardarcarta2.Location = new Point(60, 680);

            // ZONAS DE ATAQUE / DEFENSA
            ataquej1.Location = new Point(430, 330);
            defensaj1.Location = new Point(580, 330);

            ataquej2.Location = new Point(430, 530);
            defensaj2.Location = new Point(580, 530);

        }

        // -----------------------------------------
        // ABRIR CARTA AL HACER CLIC
        // -----------------------------------------
        private void AbrirCarta(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            if (pb != null && pb.Image != null)
            {
                carta vista = new carta(pb.Image);
                vista.Show();
            }
        }

        // Asignamos el mismo evento a todas las cartas
        private void Form1_Load(object sender, EventArgs e)
        {
            carta1j1.Click += AbrirCarta;
            carta2j1.Click += AbrirCarta;
            carta3j1.Click += AbrirCarta;

            carta1j2.Click += AbrirCarta;
            carta2j2.Click += AbrirCarta;
            carta3j2.Click += AbrirCarta;
        }
        private void AjustarTamanos()
        {
            foreach (var pb in Controls.OfType<PictureBox>())
            {
                if (pb.Name != "fondo") // 👉NO modificar el fondo
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }


        private void btnBocaabajo_Click_1(object sender, EventArgs e)
        {
            if (cartaSeleccionada != null)
                cartaSeleccionada.Image = Properties.Resources.parte_atras;
        }


        private void btnDetalles_Click_1(object sender, EventArgs e)
        {
            if (cartaSeleccionada != null && cartaSeleccionada.Image != null)
            {
                carta ver = new carta(cartaSeleccionada.Image);
                ver.Show();
            }
        }

        private void btnJugar_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Comienza el duelo");
        }

        private void fondo_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btbocaarriba_Click(object sender, EventArgs e)
        {
            if (cartaSeleccionada == null) return;

            if (imagenOriginal.ContainsKey(cartaSeleccionada))
            {
                cartaSeleccionada.Image = imagenOriginal[cartaSeleccionada];
            }
        }
    }
}
