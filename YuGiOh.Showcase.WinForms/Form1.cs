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

            // --- Aumentar ventana para que no corte cartas ---
            this.ClientSize = new Size(925, 950);


            btnDetalles.BringToFront();
            btnJugar.BringToFront();
            btnsalir.BringToFront();
            btnBocaabajo.BringToFront();
            btbocaarriba.BringToFront();

            AjustarTamanos();
            PosicionarCartas();
            ConfigurarClicks();
            CargarDeck();
            RepartirCartas();
        }


        private PictureBox cartaSeleccionada;

        private void ConfigurarClicks()
        {
            // 👉 Todos los PictureBox dentro del panel
            foreach (var pb in panel1.Controls.OfType<PictureBox>())
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
            // Carpeta "imagenes" al lado del .exe (funciona en cualquier PC)
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "imagenes");

            if (!Directory.Exists(path))
            {
                MessageBox.Show("No existe la carpeta 'imagenes' en:\n" + path);
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
                MessageBox.Show("No se encontraron imágenes en:\n" + path);
                return;
            }

            deck.Clear();

            foreach (string archivo in archivos)
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

            // Mazo del jugador 1 (10 cartas únicas)
            PictureBox[] cartasJ1 =
            {
        carta1j1, carta2j1, carta3j1, carta4j1, carta5j1,
        carta6j1, carta7j1, carta8j1, carta9j1, carta10j1
    };

            // Mazo del jugador 2 (10 cartas únicas)
            PictureBox[] cartasJ2 =
            {
        carta1j2, carta2j2, carta3j2, carta4j2, carta5j2,
        carta6j2, carta7j2, carta8j2, carta9j2, carta10j2
    };

            // ---- JUGADOR 1: cartas sin repetir ----
            List<Image> deckJ1 = new List<Image>(deck);

            foreach (PictureBox pb in cartasJ1)
            {
                int index = rnd.Next(deckJ1.Count);
                pb.Image = deckJ1[index];
                imagenOriginal[pb] = deckJ1[index];

                deckJ1.RemoveAt(index); // evita repetición en J1
            }

            // ---- JUGADOR 2: cartas sin repetir ----
            // Se usa un deck independiente para que J1 y J2 puedan compartir carta
            List<Image> deckJ2 = new List<Image>(deck);

            foreach (PictureBox pb in cartasJ2)
            {
                int index = rnd.Next(deckJ2.Count);
                pb.Image = deckJ2[index];
                imagenOriginal[pb] = deckJ2[index];

                deckJ2.RemoveAt(index); // evita repetición en J2
            }

            // Cartas especiales
            Guardarcarta1.Image = Properties.Resources.parte_atrasver;
            Guardarcarta2.Image = Properties.Resources.parte_atrasver;

            ataquej1.Image = Properties.Resources.parteatrasmor;
            defensaj1.Image = Properties.Resources.parteatrasgris;

            ataquej2.Image = Properties.Resources.parteatrasmor;
            defensaj2.Image = Properties.Resources.parteatrasgris;
        }

        private void PosicionarCartas()
        {
            int startX = 200;
            int espacio = 100;

            // Fila jugador 1
            PictureBox[] manoJ1 = { carta1j1, carta2j1, carta3j1, carta4j1, carta5j1 };

            for (int i = 0; i < manoJ1.Length; i++)
                manoJ1[i].Location = new Point(startX + i * espacio, 20);

            // Magias/trampas J1
            PictureBox[] magiasJ1 = { carta6j1, carta7j1, carta8j1, carta9j1, carta10j1 };

            for (int i = 0; i < magiasJ1.Length; i++)
                magiasJ1[i].Location = new Point(startX + i * espacio, 160);

            // Zonas ataque/defensa
            ataquej1.Location = new Point(350, 300);
            defensaj1.Location = new Point(450, 300);

            ataquej2.Location = new Point(350, 430);
            defensaj2.Location = new Point(450, 430);

            // Magias/trampas J2
            PictureBox[] magiasJ2 = { carta6j2, carta7j2, carta8j2, carta9j2, carta10j2 };

            for (int i = 0; i < magiasJ2.Length; i++)
                magiasJ2[i].Location = new Point(startX + i * espacio, 560);

            // Mano J2
            PictureBox[] manoJ2 = { carta1j2, carta2j2, carta3j2, carta4j2, carta5j2 };

            for (int i = 0; i < manoJ2.Length; i++)
                manoJ2[i].Location = new Point(startX + i * espacio, 700);
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
            foreach (var pb in panel1.Controls.OfType<PictureBox>())
            {
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Width = 86;
                pb.Height = 124;
            }
        }




        private void btnBocaabajo_Click_1(object sender, EventArgs e)
        {
            if (cartaSeleccionada == null) return;
            if (!imagenOriginal.ContainsKey(cartaSeleccionada)) return;

            cartaSeleccionada.Image = Properties.Resources.parte_atras;
        }



        private void btnDetalles_Click_1(object sender, EventArgs e)
        {
            if (cartaSeleccionada == null || cartaSeleccionada.Image == null)
            {
                MessageBox.Show("Seleccione una carta");
                return;
            }

            carta ver = new carta(cartaSeleccionada.Image);
            ver.Show();
        }


        private void btnJugar_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Comienza el duelo");
        }


        private void btbocaarriba_Click(object sender, EventArgs e)
        {
            if (cartaSeleccionada == null) return;
            if (!imagenOriginal.ContainsKey(cartaSeleccionada)) return;

            cartaSeleccionada.Image = imagenOriginal[cartaSeleccionada];
        }


    }
}
