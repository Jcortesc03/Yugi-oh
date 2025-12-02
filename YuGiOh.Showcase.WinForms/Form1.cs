using System;
using System.Windows.Forms;

namespace YuGiOh.Showcase.WinForms;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void Btbocaarriba_Click(object sender, EventArgs e)
    {
        // Lógica para mostrar cartas boca arriba
        MessageBox.Show("Mostrar cartas boca arriba");
    }

    private void BtnBocaabajo_Click(object sender, EventArgs e)
    {
        // Lógica para mostrar cartas boca abajo
        MessageBox.Show("Mostrar cartas boca abajo");
    }

    private void BtnDetalles_Click(object sender, EventArgs e)
    {
        // Lógica para ver detalles de la carta seleccionada
        MessageBox.Show("Mostrar detalles de la carta");
    }

    private void BtnJugar_Click(object sender, EventArgs e)
    {
        // Lógica para iniciar el juego
        MessageBox.Show("¡Juego iniciado!");
    }

    private void carta4j2_Click(object sender, EventArgs e)
    {
        // Lógica cuando se hace clic en carta4j2
        MessageBox.Show("Haz clic en carta4j2");
    }

    private void btnsalir_Click(object sender, EventArgs e)
    {
        this.Close();
    }
}