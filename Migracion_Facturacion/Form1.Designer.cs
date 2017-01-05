namespace Migracion_Facturacion
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_recib = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_recib_cob = new System.Windows.Forms.Button();
            this.btn_salir = new System.Windows.Forms.Button();
            this.btn_movimient = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Sanciones = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_recib
            // 
            this.btn_recib.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_recib.Location = new System.Drawing.Point(71, 74);
            this.btn_recib.Name = "btn_recib";
            this.btn_recib.Size = new System.Drawing.Size(133, 61);
            this.btn_recib.TabIndex = 0;
            this.btn_recib.Text = "Recibos";
            this.btn_recib.UseVisualStyleBackColor = true;
            this.btn_recib.Click += new System.EventHandler(this.btn_recib_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Mistral", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(133, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 34);
            this.label1.TabIndex = 1;
            this.label1.Text = "Migración Facturación";
            // 
            // btn_recib_cob
            // 
            this.btn_recib_cob.Enabled = false;
            this.btn_recib_cob.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_recib_cob.Location = new System.Drawing.Point(71, 173);
            this.btn_recib_cob.Name = "btn_recib_cob";
            this.btn_recib_cob.Size = new System.Drawing.Size(133, 61);
            this.btn_recib_cob.TabIndex = 2;
            this.btn_recib_cob.Text = "Recibos Cobros";
            this.btn_recib_cob.UseVisualStyleBackColor = true;
            this.btn_recib_cob.Click += new System.EventHandler(this.btn_recib_cob_Click);
            // 
            // btn_salir
            // 
            this.btn_salir.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_salir.BackgroundImage")));
            this.btn_salir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_salir.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_salir.Location = new System.Drawing.Point(12, 268);
            this.btn_salir.Name = "btn_salir";
            this.btn_salir.Size = new System.Drawing.Size(75, 75);
            this.btn_salir.TabIndex = 4;
            this.btn_salir.UseVisualStyleBackColor = true;
            this.btn_salir.Click += new System.EventHandler(this.btn_salir_Click);
            // 
            // btn_movimient
            // 
            this.btn_movimient.Enabled = false;
            this.btn_movimient.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_movimient.Location = new System.Drawing.Point(314, 173);
            this.btn_movimient.Name = "btn_movimient";
            this.btn_movimient.Size = new System.Drawing.Size(133, 61);
            this.btn_movimient.TabIndex = 5;
            this.btn_movimient.Text = "Movimientos Cobros";
            this.btn_movimient.UseVisualStyleBackColor = true;
            this.btn_movimient.Click += new System.EventHandler(this.btn_movimient_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(362, 280);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 39);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(188, 268);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 61);
            this.button1.TabIndex = 7;
            this.button1.Text = "credito y cobranza";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Sanciones
            // 
            this.Sanciones.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sanciones.Location = new System.Drawing.Point(314, 74);
            this.Sanciones.Name = "Sanciones";
            this.Sanciones.Size = new System.Drawing.Size(133, 61);
            this.Sanciones.TabIndex = 8;
            this.Sanciones.Text = "Sanciones";
            this.Sanciones.UseVisualStyleBackColor = true;
            this.Sanciones.Click += new System.EventHandler(this.Sanciones_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(517, 355);
            this.ControlBox = false;
            this.Controls.Add(this.Sanciones);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_movimient);
            this.Controls.Add(this.btn_salir);
            this.Controls.Add(this.btn_recib_cob);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_recib);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu Principal Migración";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_recib;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_recib_cob;
        private System.Windows.Forms.Button btn_salir;
        private System.Windows.Forms.Button btn_movimient;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Sanciones;
    }
}

