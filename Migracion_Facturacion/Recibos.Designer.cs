namespace Migracion_Facturacion
{
    partial class Recibos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recibos));
            this.dtg_destino = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_copy = new System.Windows.Forms.Button();
            this.btn_migrar = new System.Windows.Forms.Button();
            this.btn_regresar = new System.Windows.Forms.Button();
            this.dtg_origen = new System.Windows.Forms.DataGridView();
            this.btn_import = new System.Windows.Forms.Button();
            this.pBar1 = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gb_1 = new System.Windows.Forms.GroupBox();
            this.rdb_7 = new System.Windows.Forms.RadioButton();
            this.rdb_6 = new System.Windows.Forms.RadioButton();
            this.rdb_4 = new System.Windows.Forms.RadioButton();
            this.rdb_2 = new System.Windows.Forms.RadioButton();
            this.rdb_5 = new System.Windows.Forms.RadioButton();
            this.rdb_3 = new System.Windows.Forms.RadioButton();
            this.rdb_1 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_origen = new System.Windows.Forms.Label();
            this.lbl_destino = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.check_1 = new System.Windows.Forms.CheckBox();
            this.lbl_tiempos_copy = new System.Windows.Forms.Label();
            this.lbl_tiempos_migrate = new System.Windows.Forms.Label();
            this.btn_TODO = new System.Windows.Forms.Button();
            this.pic_1 = new System.Windows.Forms.PictureBox();
            this.lbl_smp_origen = new System.Windows.Forms.Label();
            this.lbl_smp_destino = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_destino)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_origen)).BeginInit();
            this.gb_1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtg_destino
            // 
            this.dtg_destino.AllowUserToAddRows = false;
            this.dtg_destino.AllowUserToDeleteRows = false;
            this.dtg_destino.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtg_destino.Location = new System.Drawing.Point(651, 144);
            this.dtg_destino.Name = "dtg_destino";
            this.dtg_destino.ReadOnly = true;
            this.dtg_destino.Size = new System.Drawing.Size(493, 292);
            this.dtg_destino.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(647, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Destino";
            // 
            // btn_copy
            // 
            this.btn_copy.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_copy.Location = new System.Drawing.Point(529, 193);
            this.btn_copy.Name = "btn_copy";
            this.btn_copy.Size = new System.Drawing.Size(104, 43);
            this.btn_copy.TabIndex = 4;
            this.btn_copy.Text = "Copy";
            this.btn_copy.UseVisualStyleBackColor = true;
            this.btn_copy.Click += new System.EventHandler(this.btn_copy_Click);
            // 
            // btn_migrar
            // 
            this.btn_migrar.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_migrar.Location = new System.Drawing.Point(529, 242);
            this.btn_migrar.Name = "btn_migrar";
            this.btn_migrar.Size = new System.Drawing.Size(104, 43);
            this.btn_migrar.TabIndex = 5;
            this.btn_migrar.Text = "Migrate";
            this.btn_migrar.UseVisualStyleBackColor = true;
            this.btn_migrar.Click += new System.EventHandler(this.btn_migrar_Click);
            // 
            // btn_regresar
            // 
            this.btn_regresar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_regresar.BackgroundImage")));
            this.btn_regresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_regresar.Location = new System.Drawing.Point(17, 486);
            this.btn_regresar.Name = "btn_regresar";
            this.btn_regresar.Size = new System.Drawing.Size(79, 60);
            this.btn_regresar.TabIndex = 6;
            this.btn_regresar.UseVisualStyleBackColor = true;
            this.btn_regresar.Click += new System.EventHandler(this.btn_regresar_Click);
            // 
            // dtg_origen
            // 
            this.dtg_origen.AllowUserToAddRows = false;
            this.dtg_origen.AllowUserToDeleteRows = false;
            this.dtg_origen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtg_origen.Location = new System.Drawing.Point(16, 144);
            this.dtg_origen.Name = "dtg_origen";
            this.dtg_origen.ReadOnly = true;
            this.dtg_origen.Size = new System.Drawing.Size(493, 292);
            this.dtg_origen.TabIndex = 0;
            // 
            // btn_import
            // 
            this.btn_import.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_import.Location = new System.Drawing.Point(529, 144);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(104, 43);
            this.btn_import.TabIndex = 7;
            this.btn_import.Text = "Import";
            this.btn_import.UseVisualStyleBackColor = true;
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // pBar1
            // 
            this.pBar1.Location = new System.Drawing.Point(258, 457);
            this.pBar1.Name = "pBar1";
            this.pBar1.Size = new System.Drawing.Size(650, 23);
            this.pBar1.TabIndex = 8;
            this.pBar1.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Mistral", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 29);
            this.label4.TabIndex = 15;
            this.label4.Text = "Flowers and Chispa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Origen";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(91, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(325, 38);
            this.label3.TabIndex = 16;
            this.label3.Text = "Facturación Recibos";
            // 
            // gb_1
            // 
            this.gb_1.Controls.Add(this.rdb_7);
            this.gb_1.Controls.Add(this.rdb_6);
            this.gb_1.Controls.Add(this.rdb_4);
            this.gb_1.Controls.Add(this.rdb_2);
            this.gb_1.Controls.Add(this.rdb_5);
            this.gb_1.Controls.Add(this.rdb_3);
            this.gb_1.Controls.Add(this.rdb_1);
            this.gb_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_1.Location = new System.Drawing.Point(589, 18);
            this.gb_1.Name = "gb_1";
            this.gb_1.Size = new System.Drawing.Size(555, 93);
            this.gb_1.TabIndex = 17;
            this.gb_1.TabStop = false;
            this.gb_1.Text = "Escenarios";
            // 
            // rdb_7
            // 
            this.rdb_7.AutoSize = true;
            this.rdb_7.Location = new System.Drawing.Point(199, 73);
            this.rdb_7.Name = "rdb_7";
            this.rdb_7.Size = new System.Drawing.Size(148, 20);
            this.rdb_7.TabIndex = 7;
            this.rdb_7.Text = "TODO el Historial";
            this.rdb_7.UseVisualStyleBackColor = true;
            // 
            // rdb_6
            // 
            this.rdb_6.AutoSize = true;
            this.rdb_6.BackColor = System.Drawing.SystemColors.Desktop;
            this.rdb_6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rdb_6.Location = new System.Drawing.Point(387, 47);
            this.rdb_6.Name = "rdb_6";
            this.rdb_6.Size = new System.Drawing.Size(156, 20);
            this.rdb_6.TabIndex = 6;
            this.rdb_6.Text = "Saldos incorrectos";
            this.rdb_6.UseVisualStyleBackColor = false;
            this.rdb_6.CheckedChanged += new System.EventHandler(this.rdb_1_CheckedChanged);
            // 
            // rdb_4
            // 
            this.rdb_4.AutoSize = true;
            this.rdb_4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rdb_4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rdb_4.Location = new System.Drawing.Point(199, 47);
            this.rdb_4.Name = "rdb_4";
            this.rdb_4.Size = new System.Drawing.Size(182, 20);
            this.rdb_4.TabIndex = 5;
            this.rdb_4.Text = "Con adeudo (Historial)";
            this.rdb_4.UseVisualStyleBackColor = false;
            this.rdb_4.CheckedChanged += new System.EventHandler(this.rdb_1_CheckedChanged);
            // 
            // rdb_2
            // 
            this.rdb_2.AutoSize = true;
            this.rdb_2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rdb_2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rdb_2.Location = new System.Drawing.Point(16, 47);
            this.rdb_2.Name = "rdb_2";
            this.rdb_2.Size = new System.Drawing.Size(177, 20);
            this.rdb_2.TabIndex = 4;
            this.rdb_2.Text = "Sin adeudo (Historial)";
            this.rdb_2.UseVisualStyleBackColor = false;
            this.rdb_2.CheckedChanged += new System.EventHandler(this.rdb_1_CheckedChanged);
            // 
            // rdb_5
            // 
            this.rdb_5.AutoSize = true;
            this.rdb_5.Location = new System.Drawing.Point(387, 21);
            this.rdb_5.Name = "rdb_5";
            this.rdb_5.Size = new System.Drawing.Size(107, 20);
            this.rdb_5.TabIndex = 3;
            this.rdb_5.Text = "Sin historial";
            this.rdb_5.UseVisualStyleBackColor = true;
            this.rdb_5.CheckedChanged += new System.EventHandler(this.rdb_1_CheckedChanged);
            // 
            // rdb_3
            // 
            this.rdb_3.AutoSize = true;
            this.rdb_3.Location = new System.Drawing.Point(199, 21);
            this.rdb_3.Name = "rdb_3";
            this.rdb_3.Size = new System.Drawing.Size(110, 20);
            this.rdb_3.TabIndex = 2;
            this.rdb_3.Text = "Con adeudo";
            this.rdb_3.UseVisualStyleBackColor = true;
            this.rdb_3.CheckedChanged += new System.EventHandler(this.rdb_1_CheckedChanged);
            // 
            // rdb_1
            // 
            this.rdb_1.AutoSize = true;
            this.rdb_1.Checked = true;
            this.rdb_1.Location = new System.Drawing.Point(16, 21);
            this.rdb_1.Name = "rdb_1";
            this.rdb_1.Size = new System.Drawing.Size(105, 20);
            this.rdb_1.TabIndex = 0;
            this.rdb_1.TabStop = true;
            this.rdb_1.Text = "Sin adeudo";
            this.rdb_1.UseVisualStyleBackColor = true;
            this.rdb_1.CheckedChanged += new System.EventHandler(this.rdb_1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(326, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 18);
            this.label5.TabIndex = 18;
            this.label5.Text = "No. Registros:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(958, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 18);
            this.label6.TabIndex = 19;
            this.label6.Text = "No. Registros:";
            // 
            // lbl_origen
            // 
            this.lbl_origen.AutoSize = true;
            this.lbl_origen.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_origen.Location = new System.Drawing.Point(458, 118);
            this.lbl_origen.Name = "lbl_origen";
            this.lbl_origen.Size = new System.Drawing.Size(18, 18);
            this.lbl_origen.TabIndex = 20;
            this.lbl_origen.Text = "0";
            // 
            // lbl_destino
            // 
            this.lbl_destino.AutoSize = true;
            this.lbl_destino.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_destino.Location = new System.Drawing.Point(1090, 118);
            this.lbl_destino.Name = "lbl_destino";
            this.lbl_destino.Size = new System.Drawing.Size(18, 18);
            this.lbl_destino.TabIndex = 21;
            this.lbl_destino.Text = "0";
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(977, 487);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 60);
            this.button1.TabIndex = 22;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // check_1
            // 
            this.check_1.AutoSize = true;
            this.check_1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_1.Location = new System.Drawing.Point(541, 395);
            this.check_1.Name = "check_1";
            this.check_1.Size = new System.Drawing.Size(82, 22);
            this.check_1.TabIndex = 23;
            this.check_1.Text = "1 Click";
            this.check_1.UseVisualStyleBackColor = true;
            this.check_1.Visible = false;
            // 
            // lbl_tiempos_copy
            // 
            this.lbl_tiempos_copy.AutoSize = true;
            this.lbl_tiempos_copy.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tiempos_copy.Location = new System.Drawing.Point(112, 16);
            this.lbl_tiempos_copy.Name = "lbl_tiempos_copy";
            this.lbl_tiempos_copy.Size = new System.Drawing.Size(600, 16);
            this.lbl_tiempos_copy.TabIndex = 24;
            this.lbl_tiempos_copy.Text = "C1: 00:00:00  C2: 00:00:00  C3: 00:00:00  C4: 00:00:00  C5: 00:00:00  C6: 00:00:0" +
    "0";
            // 
            // lbl_tiempos_migrate
            // 
            this.lbl_tiempos_migrate.AutoSize = true;
            this.lbl_tiempos_migrate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tiempos_migrate.Location = new System.Drawing.Point(112, 41);
            this.lbl_tiempos_migrate.Name = "lbl_tiempos_migrate";
            this.lbl_tiempos_migrate.Size = new System.Drawing.Size(600, 16);
            this.lbl_tiempos_migrate.TabIndex = 25;
            this.lbl_tiempos_migrate.Text = "C1: 00:00:00  C2: 00:00:00  C3: 00:00:00  C4: 00:00:00  C5: 00:00:00  C6: 00:00:0" +
    "0";
            // 
            // btn_TODO
            // 
            this.btn_TODO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_TODO.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TODO.ForeColor = System.Drawing.Color.White;
            this.btn_TODO.Location = new System.Drawing.Point(1044, 486);
            this.btn_TODO.Name = "btn_TODO";
            this.btn_TODO.Size = new System.Drawing.Size(88, 58);
            this.btn_TODO.TabIndex = 26;
            this.btn_TODO.Text = "Migrar HFC6";
            this.btn_TODO.UseVisualStyleBackColor = false;
            this.btn_TODO.Click += new System.EventHandler(this.btn_TODO_Click);
            // 
            // pic_1
            // 
            this.pic_1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_1.BackgroundImage")));
            this.pic_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_1.Location = new System.Drawing.Point(556, 330);
            this.pic_1.Name = "pic_1";
            this.pic_1.Size = new System.Drawing.Size(50, 50);
            this.pic_1.TabIndex = 27;
            this.pic_1.TabStop = false;
            this.pic_1.DoubleClick += new System.EventHandler(this.pic_1_DoubleClick);
            // 
            // lbl_smp_origen
            // 
            this.lbl_smp_origen.AutoSize = true;
            this.lbl_smp_origen.Font = new System.Drawing.Font("Lucida Calligraphy", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_smp_origen.ForeColor = System.Drawing.Color.DarkRed;
            this.lbl_smp_origen.Location = new System.Drawing.Point(94, 117);
            this.lbl_smp_origen.Name = "lbl_smp_origen";
            this.lbl_smp_origen.Size = new System.Drawing.Size(109, 21);
            this.lbl_smp_origen.TabIndex = 28;
            this.lbl_smp_origen.Text = "Simapag...";
            // 
            // lbl_smp_destino
            // 
            this.lbl_smp_destino.AutoSize = true;
            this.lbl_smp_destino.Font = new System.Drawing.Font("Lucida Calligraphy", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_smp_destino.ForeColor = System.Drawing.Color.DarkRed;
            this.lbl_smp_destino.Location = new System.Drawing.Point(745, 117);
            this.lbl_smp_destino.Name = "lbl_smp_destino";
            this.lbl_smp_destino.Size = new System.Drawing.Size(109, 21);
            this.lbl_smp_destino.TabIndex = 29;
            this.lbl_smp_destino.Text = "Simapag...";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(23, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 30;
            this.label7.Text = "Copiado:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 16);
            this.label8.TabIndex = 31;
            this.label8.Text = "Migración:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lbl_tiempos_migrate);
            this.groupBox1.Controls.Add(this.lbl_tiempos_copy);
            this.groupBox1.Location = new System.Drawing.Point(203, 486);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(758, 64);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tiempos:";
            // 
            // Recibos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1159, 562);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_smp_destino);
            this.Controls.Add(this.lbl_smp_origen);
            this.Controls.Add(this.pic_1);
            this.Controls.Add(this.btn_TODO);
            this.Controls.Add(this.check_1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_destino);
            this.Controls.Add(this.lbl_origen);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gb_1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pBar1);
            this.Controls.Add(this.btn_import);
            this.Controls.Add(this.btn_regresar);
            this.Controls.Add(this.btn_migrar);
            this.Controls.Add(this.btn_copy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtg_destino);
            this.Controls.Add(this.dtg_origen);
            this.Name = "Recibos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recibos";
            this.Load += new System.EventHandler(this.Recibos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg_destino)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_origen)).EndInit();
            this.gb_1.ResumeLayout(false);
            this.gb_1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtg_destino;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_copy;
        private System.Windows.Forms.Button btn_migrar;
        private System.Windows.Forms.Button btn_regresar;
        private System.Windows.Forms.DataGridView dtg_origen;
        private System.Windows.Forms.Button btn_import;
        private System.Windows.Forms.ProgressBar pBar1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gb_1;
        private System.Windows.Forms.RadioButton rdb_3;
        private System.Windows.Forms.RadioButton rdb_1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_origen;
        private System.Windows.Forms.Label lbl_destino;
        private System.Windows.Forms.RadioButton rdb_5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox check_1;
        private System.Windows.Forms.RadioButton rdb_2;
        private System.Windows.Forms.RadioButton rdb_4;
        private System.Windows.Forms.Label lbl_tiempos_copy;
        private System.Windows.Forms.Label lbl_tiempos_migrate;
        private System.Windows.Forms.Button btn_TODO;
        private System.Windows.Forms.PictureBox pic_1;
        private System.Windows.Forms.Label lbl_smp_origen;
        private System.Windows.Forms.Label lbl_smp_destino;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rdb_6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdb_7;
    }
}