namespace Migracion_Facturacion
{
    partial class Movimientos_Cobros
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Movimientos_Cobros));
            this.dtg_origen = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dtg_destino = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_copy = new System.Windows.Forms.Button();
            this.btn_migrar = new System.Windows.Forms.Button();
            this.btn_regresar = new System.Windows.Forms.Button();
            this.btn_import = new System.Windows.Forms.Button();
            this.pBar1 = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.gb_1 = new System.Windows.Forms.GroupBox();
            this.rdb_5 = new System.Windows.Forms.RadioButton();
            this.rdb_4 = new System.Windows.Forms.RadioButton();
            this.rdb_3 = new System.Windows.Forms.RadioButton();
            this.rdb_2 = new System.Windows.Forms.RadioButton();
            this.rdb_1 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_destino = new System.Windows.Forms.Label();
            this.lbl_origen = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_origen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_destino)).BeginInit();
            this.gb_1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtg_origen
            // 
            this.dtg_origen.AllowUserToAddRows = false;
            this.dtg_origen.AllowUserToDeleteRows = false;
            this.dtg_origen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtg_origen.Location = new System.Drawing.Point(12, 122);
            this.dtg_origen.Name = "dtg_origen";
            this.dtg_origen.ReadOnly = true;
            this.dtg_origen.Size = new System.Drawing.Size(493, 292);
            this.dtg_origen.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(15, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Origen";
            // 
            // dtg_destino
            // 
            this.dtg_destino.AllowUserToAddRows = false;
            this.dtg_destino.AllowUserToDeleteRows = false;
            this.dtg_destino.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtg_destino.Location = new System.Drawing.Point(661, 122);
            this.dtg_destino.Name = "dtg_destino";
            this.dtg_destino.ReadOnly = true;
            this.dtg_destino.Size = new System.Drawing.Size(493, 292);
            this.dtg_destino.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Crimson;
            this.label2.Location = new System.Drawing.Point(657, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Destino";
            // 
            // btn_copy
            // 
            this.btn_copy.Enabled = false;
            this.btn_copy.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_copy.ForeColor = System.Drawing.Color.DarkRed;
            this.btn_copy.Location = new System.Drawing.Point(530, 247);
            this.btn_copy.Name = "btn_copy";
            this.btn_copy.Size = new System.Drawing.Size(104, 43);
            this.btn_copy.TabIndex = 5;
            this.btn_copy.Text = "Copy";
            this.btn_copy.UseVisualStyleBackColor = true;
            this.btn_copy.Click += new System.EventHandler(this.btn_copy_Click);
            // 
            // btn_migrar
            // 
            this.btn_migrar.Enabled = false;
            this.btn_migrar.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_migrar.ForeColor = System.Drawing.Color.DarkRed;
            this.btn_migrar.Location = new System.Drawing.Point(530, 320);
            this.btn_migrar.Name = "btn_migrar";
            this.btn_migrar.Size = new System.Drawing.Size(104, 43);
            this.btn_migrar.TabIndex = 6;
            this.btn_migrar.Text = "Migrate";
            this.btn_migrar.UseVisualStyleBackColor = true;
            this.btn_migrar.Click += new System.EventHandler(this.btn_migrar_Click);
            // 
            // btn_regresar
            // 
            this.btn_regresar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_regresar.BackgroundImage")));
            this.btn_regresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_regresar.Location = new System.Drawing.Point(12, 430);
            this.btn_regresar.Name = "btn_regresar";
            this.btn_regresar.Size = new System.Drawing.Size(75, 58);
            this.btn_regresar.TabIndex = 9;
            this.btn_regresar.UseVisualStyleBackColor = true;
            this.btn_regresar.Click += new System.EventHandler(this.btn_regresar_Click);
            // 
            // btn_import
            // 
            this.btn_import.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_import.ForeColor = System.Drawing.Color.DarkRed;
            this.btn_import.Location = new System.Drawing.Point(530, 176);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(104, 43);
            this.btn_import.TabIndex = 10;
            this.btn_import.Text = "Import";
            this.btn_import.UseVisualStyleBackColor = true;
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // pBar1
            // 
            this.pBar1.Location = new System.Drawing.Point(385, 441);
            this.pBar1.Name = "pBar1";
            this.pBar1.Size = new System.Drawing.Size(399, 23);
            this.pBar1.TabIndex = 11;
            this.pBar1.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Mistral", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gold;
            this.label4.Location = new System.Drawing.Point(959, 459);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(201, 29);
            this.label4.TabIndex = 14;
            this.label4.Text = "Migración By Flowers";
            // 
            // gb_1
            // 
            this.gb_1.Controls.Add(this.rdb_5);
            this.gb_1.Controls.Add(this.rdb_4);
            this.gb_1.Controls.Add(this.rdb_3);
            this.gb_1.Controls.Add(this.rdb_2);
            this.gb_1.Controls.Add(this.rdb_1);
            this.gb_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_1.ForeColor = System.Drawing.Color.Crimson;
            this.gb_1.Location = new System.Drawing.Point(708, 12);
            this.gb_1.Name = "gb_1";
            this.gb_1.Size = new System.Drawing.Size(446, 52);
            this.gb_1.TabIndex = 19;
            this.gb_1.TabStop = false;
            this.gb_1.Text = "Escenarios";
            // 
            // rdb_5
            // 
            this.rdb_5.AutoSize = true;
            this.rdb_5.ForeColor = System.Drawing.Color.Gold;
            this.rdb_5.Location = new System.Drawing.Point(367, 21);
            this.rdb_5.Name = "rdb_5";
            this.rdb_5.Size = new System.Drawing.Size(72, 20);
            this.rdb_5.TabIndex = 4;
            this.rdb_5.TabStop = true;
            this.rdb_5.Text = "case 5";
            this.rdb_5.UseVisualStyleBackColor = true;
            this.rdb_5.CheckedChanged += new System.EventHandler(this.rdb_1_CheckedChanged_1);
            // 
            // rdb_4
            // 
            this.rdb_4.AutoSize = true;
            this.rdb_4.ForeColor = System.Drawing.Color.Gold;
            this.rdb_4.Location = new System.Drawing.Point(283, 21);
            this.rdb_4.Name = "rdb_4";
            this.rdb_4.Size = new System.Drawing.Size(72, 20);
            this.rdb_4.TabIndex = 3;
            this.rdb_4.TabStop = true;
            this.rdb_4.Text = "case 4";
            this.rdb_4.UseVisualStyleBackColor = true;
            this.rdb_4.CheckedChanged += new System.EventHandler(this.rdb_1_CheckedChanged_1);
            // 
            // rdb_3
            // 
            this.rdb_3.AutoSize = true;
            this.rdb_3.ForeColor = System.Drawing.Color.Gold;
            this.rdb_3.Location = new System.Drawing.Point(192, 21);
            this.rdb_3.Name = "rdb_3";
            this.rdb_3.Size = new System.Drawing.Size(72, 20);
            this.rdb_3.TabIndex = 2;
            this.rdb_3.TabStop = true;
            this.rdb_3.Text = "case 3";
            this.rdb_3.UseVisualStyleBackColor = true;
            this.rdb_3.CheckedChanged += new System.EventHandler(this.rdb_1_CheckedChanged_1);
            // 
            // rdb_2
            // 
            this.rdb_2.AutoSize = true;
            this.rdb_2.ForeColor = System.Drawing.Color.Gold;
            this.rdb_2.Location = new System.Drawing.Point(102, 21);
            this.rdb_2.Name = "rdb_2";
            this.rdb_2.Size = new System.Drawing.Size(72, 20);
            this.rdb_2.TabIndex = 1;
            this.rdb_2.TabStop = true;
            this.rdb_2.Text = "case 2";
            this.rdb_2.UseVisualStyleBackColor = true;
            this.rdb_2.CheckedChanged += new System.EventHandler(this.rdb_1_CheckedChanged_1);
            // 
            // rdb_1
            // 
            this.rdb_1.AutoSize = true;
            this.rdb_1.ForeColor = System.Drawing.Color.Gold;
            this.rdb_1.Location = new System.Drawing.Point(15, 21);
            this.rdb_1.Name = "rdb_1";
            this.rdb_1.Size = new System.Drawing.Size(72, 20);
            this.rdb_1.TabIndex = 0;
            this.rdb_1.TabStop = true;
            this.rdb_1.Text = "case 1";
            this.rdb_1.UseVisualStyleBackColor = true;
            this.rdb_1.CheckedChanged += new System.EventHandler(this.rdb_1_CheckedChanged_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Modern No. 20", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Crimson;
            this.label5.Location = new System.Drawing.Point(55, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(450, 38);
            this.label5.TabIndex = 18;
            this.label5.Text = "Facturación Recibos Master ";
            // 
            // lbl_destino
            // 
            this.lbl_destino.AutoSize = true;
            this.lbl_destino.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_destino.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_destino.Location = new System.Drawing.Point(1067, 89);
            this.lbl_destino.Name = "lbl_destino";
            this.lbl_destino.Size = new System.Drawing.Size(18, 18);
            this.lbl_destino.TabIndex = 29;
            this.lbl_destino.Text = "0";
            // 
            // lbl_origen
            // 
            this.lbl_origen.AutoSize = true;
            this.lbl_origen.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_origen.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_origen.Location = new System.Drawing.Point(406, 89);
            this.lbl_origen.Name = "lbl_origen";
            this.lbl_origen.Size = new System.Drawing.Size(18, 18);
            this.lbl_origen.TabIndex = 28;
            this.lbl_origen.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(935, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 18);
            this.label6.TabIndex = 27;
            this.label6.Text = "No. Registros:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(274, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 18);
            this.label7.TabIndex = 26;
            this.label7.Text = "No. Registros:";
            // 
            // Movimientos_Cobros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1172, 500);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_destino);
            this.Controls.Add(this.lbl_origen);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.gb_1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pBar1);
            this.Controls.Add(this.btn_import);
            this.Controls.Add(this.btn_regresar);
            this.Controls.Add(this.btn_migrar);
            this.Controls.Add(this.btn_copy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtg_destino);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtg_origen);
            this.Name = "Movimientos_Cobros";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movimientos_cobro";
            this.Load += new System.EventHandler(this.Movimientos_Cobros_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg_origen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_destino)).EndInit();
            this.gb_1.ResumeLayout(false);
            this.gb_1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtg_origen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtg_destino;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_copy;
        private System.Windows.Forms.Button btn_migrar;
        private System.Windows.Forms.Button btn_regresar;
        private System.Windows.Forms.Button btn_import;
        private System.Windows.Forms.ProgressBar pBar1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gb_1;
        private System.Windows.Forms.RadioButton rdb_5;
        private System.Windows.Forms.RadioButton rdb_4;
        private System.Windows.Forms.RadioButton rdb_3;
        private System.Windows.Forms.RadioButton rdb_2;
        private System.Windows.Forms.RadioButton rdb_1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_destino;
        private System.Windows.Forms.Label lbl_origen;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}