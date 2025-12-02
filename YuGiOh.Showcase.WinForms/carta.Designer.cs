namespace YuGiOh.Showcase.WinForms
{
    partial class carta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pbcartas = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbcartas).BeginInit();
            SuspendLayout();
            // 
            // pbcartas
            // 
            pbcartas.Location = new Point(-9, -9);
            pbcartas.Name = "pbcartas";
            pbcartas.Size = new Size(366, 510);
            pbcartas.TabIndex = 0;
            pbcartas.TabStop = false;
            // 
            // carta
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(347, 488);
            ControlBox = false;
            Controls.Add(pbcartas);
            Margin = new Padding(3, 4, 3, 4);
            Name = "carta";
            ((System.ComponentModel.ISupportInitialize)pbcartas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public PictureBox pbcartas;
    }
}