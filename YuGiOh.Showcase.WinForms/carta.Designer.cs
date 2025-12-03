namespace YuGiOh.Showcase.WinForms
{
    partial class carta
    {
        private PictureBox pictureBox1;

        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.BackColor = Color.Black;
            // 
            // carta
            // 
            ClientSize = new Size(400, 600);
            Controls.Add(pictureBox1);
            Name = "carta";
            Text = "Vista de Carta";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }
    }
}
