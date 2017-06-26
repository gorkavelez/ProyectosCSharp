namespace CapturaDatosPlantaWindowsForm
{
    partial class REFINOS
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
            this.lblSabana = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtHora = new System.Windows.Forms.TextBox();
            this.txtDia = new System.Windows.Forms.TextBox();
            this.btnSelecionFechas = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDownHora = new System.Windows.Forms.Button();
            this.btnUpHora = new System.Windows.Forms.Button();
            this.btnDownDia = new System.Windows.Forms.Button();
            this.btnUpDia = new System.Windows.Forms.Button();
            this.btnAtras = new System.Windows.Forms.Button();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtObservac = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSabana
            // 
            this.lblSabana.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSabana.AutoSize = true;
            this.lblSabana.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSabana.Location = new System.Drawing.Point(308, 35);
            this.lblSabana.Name = "lblSabana";
            this.lblSabana.Size = new System.Drawing.Size(52, 17);
            this.lblSabana.TabIndex = 45;
            this.lblSabana.Text = "label8";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(446, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 15);
            this.label2.TabIndex = 39;
            this.label2.Text = "Hora registro";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(446, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 15);
            this.label1.TabIndex = 38;
            this.label1.Text = "Fecha registro";
            // 
            // TxtHora
            // 
            this.TxtHora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtHora.Location = new System.Drawing.Point(551, 35);
            this.TxtHora.Name = "TxtHora";
            this.TxtHora.Size = new System.Drawing.Size(76, 20);
            this.TxtHora.TabIndex = 37;
            // 
            // txtDia
            // 
            this.txtDia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDia.Location = new System.Drawing.Point(551, 6);
            this.txtDia.Name = "txtDia";
            this.txtDia.Size = new System.Drawing.Size(76, 20);
            this.txtDia.TabIndex = 36;
            // 
            // btnSelecionFechas
            // 
            this.btnSelecionFechas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelecionFechas.BackgroundImage = global::CapturaDatosPlantaWindowsForm.Properties.Resources.scheduleFree;
            this.btnSelecionFechas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSelecionFechas.Location = new System.Drawing.Point(711, 4);
            this.btnSelecionFechas.Name = "btnSelecionFechas";
            this.btnSelecionFechas.Size = new System.Drawing.Size(68, 51);
            this.btnSelecionFechas.TabIndex = 46;
            this.btnSelecionFechas.UseVisualStyleBackColor = true;
            this.btnSelecionFechas.Click += new System.EventHandler(this.btnSelecionFechas_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::CapturaDatosPlantaWindowsForm.Properties.Resources.SK_Mark_Standard_Col_RGB;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(258, 36);
            this.pictureBox1.TabIndex = 44;
            this.pictureBox1.TabStop = false;
            // 
            // btnDownHora
            // 
            this.btnDownHora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownHora.BackgroundImage = global::CapturaDatosPlantaWindowsForm.Properties.Resources.FlechaAbajo;
            this.btnDownHora.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDownHora.Location = new System.Drawing.Point(672, 32);
            this.btnDownHora.Name = "btnDownHora";
            this.btnDownHora.Size = new System.Drawing.Size(33, 23);
            this.btnDownHora.TabIndex = 43;
            this.btnDownHora.UseVisualStyleBackColor = true;
            // 
            // btnUpHora
            // 
            this.btnUpHora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpHora.BackgroundImage = global::CapturaDatosPlantaWindowsForm.Properties.Resources.FlechaArriba;
            this.btnUpHora.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpHora.Location = new System.Drawing.Point(633, 32);
            this.btnUpHora.Name = "btnUpHora";
            this.btnUpHora.Size = new System.Drawing.Size(33, 23);
            this.btnUpHora.TabIndex = 42;
            this.btnUpHora.UseVisualStyleBackColor = true;
            // 
            // btnDownDia
            // 
            this.btnDownDia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownDia.BackgroundImage = global::CapturaDatosPlantaWindowsForm.Properties.Resources.FlechaAbajo;
            this.btnDownDia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDownDia.Location = new System.Drawing.Point(672, 6);
            this.btnDownDia.Name = "btnDownDia";
            this.btnDownDia.Size = new System.Drawing.Size(33, 23);
            this.btnDownDia.TabIndex = 41;
            this.btnDownDia.UseVisualStyleBackColor = true;
            // 
            // btnUpDia
            // 
            this.btnUpDia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpDia.BackgroundImage = global::CapturaDatosPlantaWindowsForm.Properties.Resources.FlechaArriba;
            this.btnUpDia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpDia.Location = new System.Drawing.Point(633, 6);
            this.btnUpDia.Name = "btnUpDia";
            this.btnUpDia.Size = new System.Drawing.Size(33, 23);
            this.btnUpDia.TabIndex = 40;
            this.btnUpDia.UseVisualStyleBackColor = true;
            // 
            // btnAtras
            // 
            this.btnAtras.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAtras.Location = new System.Drawing.Point(551, 391);
            this.btnAtras.Name = "btnAtras";
            this.btnAtras.Size = new System.Drawing.Size(75, 23);
            this.btnAtras.TabIndex = 49;
            this.btnAtras.Text = "Atras";
            this.btnAtras.UseVisualStyleBackColor = true;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(548, 436);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(53, 13);
            this.lblUsuario.TabIndex = 48;
            this.lblUsuario.Text = "lblUsuario";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Location = new System.Drawing.Point(643, 391);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 47;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            // 
            // txtObservac
            // 
            this.txtObservac.Location = new System.Drawing.Point(12, 306);
            this.txtObservac.Multiline = true;
            this.txtObservac.Name = "txtObservac";
            this.txtObservac.Size = new System.Drawing.Size(508, 108);
            this.txtObservac.TabIndex = 50;
            // 
            // REFINOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 517);
            this.Controls.Add(this.txtObservac);
            this.Controls.Add(this.btnAtras);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnSelecionFechas);
            this.Controls.Add(this.lblSabana);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnDownHora);
            this.Controls.Add(this.btnUpHora);
            this.Controls.Add(this.btnDownDia);
            this.Controls.Add(this.btnUpDia);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtHora);
            this.Controls.Add(this.txtDia);
            this.Name = "REFINOS";
            this.Text = "REFINOS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.REFINOS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelecionFechas;
        private System.Windows.Forms.Label lblSabana;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnDownHora;
        private System.Windows.Forms.Button btnUpHora;
        private System.Windows.Forms.Button btnDownDia;
        private System.Windows.Forms.Button btnUpDia;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtHora;
        private System.Windows.Forms.TextBox txtDia;
        private System.Windows.Forms.Button btnAtras;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtObservac;
    }
}