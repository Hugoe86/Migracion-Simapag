namespace Migracion_Facturacion
{
    partial class Recibos_Detalles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recibos_Detalles));
            this.btn_import = new System.Windows.Forms.Button();
            this.btn_regresar = new System.Windows.Forms.Button();
            this.pBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.dtg_destino = new System.Windows.Forms.DataGridView();
            this.btn_migrar = new System.Windows.Forms.Button();
            this.btn_copy = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtg_origen = new System.Windows.Forms.DataGridView();
            this.gb_1 = new System.Windows.Forms.GroupBox();
            this.rdb_5 = new System.Windows.Forms.RadioButton();
            this.rdb_4 = new System.Windows.Forms.RadioButton();
            this.rdb_3 = new System.Windows.Forms.RadioButton();
            this.rdb_2 = new System.Windows.Forms.RadioButton();
            this.rdb_1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_destino = new System.Windows.Forms.Label();
            this.lbl_origen = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_destino)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_origen)).BeginInit();
            this.gb_1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_import
            // 
            this.btn_import.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_import.Location = new System.Drawing.Point(542, 139);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(104, 43);
            this.btn_import.TabIndex = 18;
            this.btn_import.Text = "Import";
            this.btn_import.UseVisualStyleBackColor = true;
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // btn_regresar
            // 
            this.btn_regresar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_regresar.BackgroundImage")));
            this.btn_regresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_regresar.Location = new System.Drawing.Point(12, 428);
            this.btn_regresar.Name = "btn_regresar";
            this.btn_regresar.Size = new System.Drawing.Size(75, 58);
            this.btn_regresar.TabIndex = 17;
            this.btn_regresar.UseVisualStyleBackColor = true;
            this.btn_regresar.Click += new System.EventHandler(this.btn_regresar_Click);
            // 
            // pBar1
            // 
            this.pBar1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.pBar1.Location = new System.Drawing.Point(378, 441);
            this.pBar1.Name = "pBar1";
            this.pBar1.Size = new System.Drawing.Size(446, 23);
            this.pBar1.TabIndex = 16;
            this.pBar1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(671, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 24);
            this.label2.TabIndex = 15;
            this.label2.Text = "Destino";
            // 
            // dtg_destino
            // 
            this.dtg_destino.AllowUserToAddRows = false;
            this.dtg_destino.AllowUserToDeleteRows = false;
            this.dtg_destino.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtg_destino.Location = new System.Drawing.Point(675, 117);
            this.dtg_destino.Name = "dtg_destino";
            this.dtg_destino.ReadOnly = true;
            this.dtg_destino.Size = new System.Drawing.Size(493, 292);
            this.dtg_destino.TabIndex = 14;
            // 
            // btn_migrar
            // 
            this.btn_migrar.Enabled = false;
            this.btn_migrar.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_migrar.Location = new System.Drawing.Point(542, 330);
            this.btn_migrar.Name = "btn_migrar";
            this.btn_migrar.Size = new System.Drawing.Size(104, 43);
            this.btn_migrar.TabIndex = 13;
            this.btn_migrar.Text = "Migrate";
            this.btn_migrar.UseVisualStyleBackColor = true;
            this.btn_migrar.Click += new System.EventHandler(this.btn_migrar_Click);
            // 
            // btn_copy
            // 
            this.btn_copy.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_copy.Location = new System.Drawing.Point(542, 232);
            this.btn_copy.Name = "btn_copy";
            this.btn_copy.Size = new System.Drawing.Size(104, 43);
            this.btn_copy.TabIndex = 12;
            this.btn_copy.Text = "Copy";
            this.btn_copy.UseVisualStyleBackColor = true;
            this.btn_copy.Click += new System.EventHandler(this.btn_copy_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(15, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 24);
            this.label1.TabIndex = 11;
            this.label1.Text = "Origen";
            // 
            // dtg_origen
            // 
            this.dtg_origen.AllowUserToAddRows = false;
            this.dtg_origen.AllowUserToDeleteRows = false;
            this.dtg_origen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtg_origen.Location = new System.Drawing.Point(12, 117);
            this.dtg_origen.Name = "dtg_origen";
            this.dtg_origen.ReadOnly = true;
            this.dtg_origen.Size = new System.Drawing.Size(493, 292);
            this.dtg_origen.TabIndex = 10;
            // 
            // gb_1
            // 
            this.gb_1.Controls.Add(this.rdb_5);
            this.gb_1.Controls.Add(this.rdb_4);
            this.gb_1.Controls.Add(this.rdb_3);
            this.gb_1.Controls.Add(this.rdb_2);
            this.gb_1.Controls.Add(this.rdb_1);
            this.gb_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_1.Location = new System.Drawing.Point(722, 12);
            this.gb_1.Name = "gb_1";
            this.gb_1.Size = new System.Drawing.Size(446, 52);
            this.gb_1.TabIndex = 20;
            this.gb_1.TabStop = false;
            this.gb_1.Text = "Escenarios";
            // 
            // rdb_5
            // 
            this.rdb_5.AutoSize = true;
            this.rdb_5.Location = new System.Drawing.Point(367, 21);
            this.rdb_5.Name = "rdb_5";
            this.rdb_5.Size = new System.Drawing.Size(72, 20);
            this.rdb_5.TabIndex = 4;
            this.rdb_5.Text = "case 5";
            this.rdb_5.UseVisualStyleBackColor = true;
            this.rdb_5.CheckedChanged += new System.EventHandler(this.rdb_1_CheckedChanged);
            // 
            // rdb_4
            // 
            this.rdb_4.AutoSize = true;
            this.rdb_4.Location = new System.Drawing.Point(283, 21);
            this.rdb_4.Name = "rdb_4";
            this.rdb_4.Size = new System.Drawing.Size(72, 20);
            this.rdb_4.TabIndex = 3;
            this.rdb_4.Text = "case 4";
            this.rdb_4.UseVisualStyleBackColor = true;
            this.rdb_4.CheckedChanged += new System.EventHandler(this.rdb_1_CheckedChanged);
            // 
            // rdb_3
            // 
            this.rdb_3.AutoSize = true;
            this.rdb_3.Location = new System.Drawing.Point(192, 21);
            this.rdb_3.Name = "rdb_3";
            this.rdb_3.Size = new System.Drawing.Size(72, 20);
            this.rdb_3.TabIndex = 2;
            this.rdb_3.Text = "case 3";
            this.rdb_3.UseVisualStyleBackColor = true;
            this.rdb_3.CheckedChanged += new System.EventHandler(this.rdb_1_CheckedChanged);
            // 
            // rdb_2
            // 
            this.rdb_2.AutoSize = true;
            this.rdb_2.Location = new System.Drawing.Point(102, 21);
            this.rdb_2.Name = "rdb_2";
            this.rdb_2.Size = new System.Drawing.Size(72, 20);
            this.rdb_2.TabIndex = 1;
            this.rdb_2.Text = "case 2";
            this.rdb_2.UseVisualStyleBackColor = true;
            this.rdb_2.CheckedChanged += new System.EventHandler(this.rdb_1_CheckedChanged);
            // 
            // rdb_1
            // 
            this.rdb_1.AutoSize = true;
            this.rdb_1.Checked = true;
            this.rdb_1.Location = new System.Drawing.Point(15, 21);
            this.rdb_1.Name = "rdb_1";
            this.rdb_1.Size = new System.Drawing.Size(72, 20);
            this.rdb_1.TabIndex = 0;
            this.rdb_1.TabStop = true;
            this.rdb_1.Text = "case 1";
            this.rdb_1.UseVisualStyleBackColor = true;
            this.rdb_1.CheckedChanged += new System.EventHandler(this.rdb_1_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(78, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(266, 38);
            this.label3.TabIndex = 19;
            this.label3.Text = "Recibos Detalles";
            // 
            // lbl_destino
            // 
            this.lbl_destino.AutoSize = true;
            this.lbl_destino.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_destino.Location = new System.Drawing.Point(1095, 84);
            this.lbl_destino.Name = "lbl_destino";
            this.lbl_destino.Size = new System.Drawing.Size(18, 18);
            this.lbl_destino.TabIndex = 25;
            this.lbl_destino.Text = "0";
            // 
            // lbl_origen
            // 
            this.lbl_origen.AutoSize = true;
            this.lbl_origen.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_origen.Location = new System.Drawing.Point(434, 84);
            this.lbl_origen.Name = "lbl_origen";
            this.lbl_origen.Size = new System.Drawing.Size(18, 18);
            this.lbl_origen.TabIndex = 24;
            this.lbl_origen.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(963, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 18);
            this.label6.TabIndex = 23;
            this.label6.Text = "No. Registros:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(302, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 18);
            this.label5.TabIndex = 22;
            this.label5.Text = "No. Registros:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Mistral", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label4.Location = new System.Drawing.Point(975, 460);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(201, 29);
            this.label4.TabIndex = 26;
            this.label4.Text = "Migración By Flowers";
            // 
            // Recibos_Detalles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1188, 498);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbl_destino);
            this.Controls.Add(this.lbl_origen);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gb_1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_import);
            this.Controls.Add(this.btn_regresar);
            this.Controls.Add(this.pBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtg_destino);
            this.Controls.Add(this.btn_migrar);
            this.Controls.Add(this.btn_copy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtg_origen);
            this.Name = "Recibos_Detalles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recibos_Detalles";
            this.Load += new System.EventHandler(this.Recibos_Detalles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg_destino)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_origen)).EndInit();
            this.gb_1.ResumeLayout(false);
            this.gb_1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_import;
        private System.Windows.Forms.Button btn_regresar;
        private System.Windows.Forms.ProgressBar pBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dtg_destino;
        private System.Windows.Forms.Button btn_migrar;
        private System.Windows.Forms.Button btn_copy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtg_origen;
        private System.Windows.Forms.GroupBox gb_1;
        private System.Windows.Forms.RadioButton rdb_5;
        private System.Windows.Forms.RadioButton rdb_4;
        private System.Windows.Forms.RadioButton rdb_3;
        private System.Windows.Forms.RadioButton rdb_2;
        private System.Windows.Forms.RadioButton rdb_1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_destino;
        private System.Windows.Forms.Label lbl_origen;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}