namespace ProyectoFormulario
{
    partial class btnSelectFile
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAccion = new System.Windows.Forms.Button();
            this.txtOrigen = new System.Windows.Forms.TextBox();
            this.txtDestino = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.rdbCoche = new System.Windows.Forms.RadioButton();
            this.rdbPaseo = new System.Windows.Forms.RadioButton();
            this.rdbBicileta = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnAccion
            // 
            this.btnAccion.Location = new System.Drawing.Point(70, 196);
            this.btnAccion.Name = "btnAccion";
            this.btnAccion.Size = new System.Drawing.Size(75, 23);
            this.btnAccion.TabIndex = 0;
            this.btnAccion.Text = "Accion";
            this.btnAccion.UseVisualStyleBackColor = true;
            this.btnAccion.Click += new System.EventHandler(this.btnAccion_Click);
            // 
            // txtOrigen
            // 
            this.txtOrigen.Location = new System.Drawing.Point(70, 129);
            this.txtOrigen.Name = "txtOrigen";
            this.txtOrigen.Size = new System.Drawing.Size(172, 20);
            this.txtOrigen.TabIndex = 1;
            // 
            // txtDestino
            // 
            this.txtDestino.Location = new System.Drawing.Point(70, 155);
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.Size = new System.Drawing.Size(172, 20);
            this.txtDestino.TabIndex = 2;
            this.txtDestino.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtDestino_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Origen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Destino";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(260, 42);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(506, 537);
            this.listBox1.TabIndex = 6;
            // 
            // rdbCoche
            // 
            this.rdbCoche.AutoSize = true;
            this.rdbCoche.Location = new System.Drawing.Point(23, 42);
            this.rdbCoche.Name = "rdbCoche";
            this.rdbCoche.Size = new System.Drawing.Size(56, 17);
            this.rdbCoche.TabIndex = 7;
            this.rdbCoche.TabStop = true;
            this.rdbCoche.Text = "Coche";
            this.rdbCoche.UseVisualStyleBackColor = true;
            // 
            // rdbPaseo
            // 
            this.rdbPaseo.AutoSize = true;
            this.rdbPaseo.Location = new System.Drawing.Point(23, 65);
            this.rdbPaseo.Name = "rdbPaseo";
            this.rdbPaseo.Size = new System.Drawing.Size(55, 17);
            this.rdbPaseo.TabIndex = 8;
            this.rdbPaseo.TabStop = true;
            this.rdbPaseo.Text = "Paseo";
            this.rdbPaseo.UseVisualStyleBackColor = true;
            // 
            // rdbBicileta
            // 
            this.rdbBicileta.AutoSize = true;
            this.rdbBicileta.Location = new System.Drawing.Point(23, 88);
            this.rdbBicileta.Name = "rdbBicileta";
            this.rdbBicileta.Size = new System.Drawing.Size(65, 17);
            this.rdbBicileta.TabIndex = 9;
            this.rdbBicileta.TabStop = true;
            this.rdbBicileta.Text = "Bicicleta";
            this.rdbBicileta.UseVisualStyleBackColor = true;
            // 
            // btnSelectFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 623);
            this.Controls.Add(this.rdbBicileta);
            this.Controls.Add(this.rdbPaseo);
            this.Controls.Add(this.rdbCoche);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDestino);
            this.Controls.Add(this.txtOrigen);
            this.Controls.Add(this.btnAccion);
            this.Name = "btnSelectFile";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.btnSelectFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAccion;
        private System.Windows.Forms.TextBox txtOrigen;
        private System.Windows.Forms.TextBox txtDestino;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.RadioButton rdbCoche;
        private System.Windows.Forms.RadioButton rdbPaseo;
        private System.Windows.Forms.RadioButton rdbBicileta;
    }
}

