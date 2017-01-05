﻿namespace Migracion_Facturacion
{
    partial class Recibos_Cobros
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recibos_Cobros));
            this.label1 = new System.Windows.Forms.Label();
            this.btn_copy = new System.Windows.Forms.Button();
            this.btn_migrar = new System.Windows.Forms.Button();
            this.dtg_destino = new System.Windows.Forms.DataGridView();
            this.rpu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recibos_cobrar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_recibos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pago_efectivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pago_cheque = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pago_tarjeta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cambio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usuario_creo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_creo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado_recibo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importe_cobrado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iva_cobrado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tasa_iva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_cobrado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.no_factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.pBar1 = new System.Windows.Forms.ProgressBar();
            this.btn_regresar = new System.Windows.Forms.Button();
            this.dtg_origen = new System.Windows.Forms.DataGridView();
            this.btn_import = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.dtg_destino)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_origen)).BeginInit();
            this.gb_1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Lavender;
            this.label1.Location = new System.Drawing.Point(16, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Origen";
            // 
            // btn_copy
            // 
            this.btn_copy.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_copy.Location = new System.Drawing.Point(537, 239);
            this.btn_copy.Name = "btn_copy";
            this.btn_copy.Size = new System.Drawing.Size(104, 43);
            this.btn_copy.TabIndex = 2;
            this.btn_copy.Text = "Copy";
            this.btn_copy.UseVisualStyleBackColor = true;
            this.btn_copy.Click += new System.EventHandler(this.btn_copy_Click);
            // 
            // btn_migrar
            // 
            this.btn_migrar.Enabled = false;
            this.btn_migrar.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_migrar.Location = new System.Drawing.Point(537, 333);
            this.btn_migrar.Name = "btn_migrar";
            this.btn_migrar.Size = new System.Drawing.Size(104, 43);
            this.btn_migrar.TabIndex = 3;
            this.btn_migrar.Text = "Migrate";
            this.btn_migrar.UseVisualStyleBackColor = true;
            this.btn_migrar.Click += new System.EventHandler(this.btn_migrar_Click);
            // 
            // dtg_destino
            // 
            this.dtg_destino.AllowUserToAddRows = false;
            this.dtg_destino.AllowUserToDeleteRows = false;
            this.dtg_destino.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtg_destino.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rpu,
            this.recibos_cobrar,
            this.total_recibos,
            this.transferencia,
            this.pago_efectivo,
            this.pago_cheque,
            this.pago_tarjeta,
            this.saldo,
            this.cambio,
            this.fecha,
            this.usuario_creo,
            this.fecha_creo,
            this.estado_recibo,
            this.importe_cobrado,
            this.iva_cobrado,
            this.tasa_iva,
            this.total_cobrado,
            this.no_factura});
            this.dtg_destino.Location = new System.Drawing.Point(663, 120);
            this.dtg_destino.Name = "dtg_destino";
            this.dtg_destino.ReadOnly = true;
            this.dtg_destino.Size = new System.Drawing.Size(493, 292);
            this.dtg_destino.TabIndex = 4;
            // 
            // rpu
            // 
            this.rpu.HeaderText = "RPU";
            this.rpu.Name = "rpu";
            this.rpu.ReadOnly = true;
            // 
            // recibos_cobrar
            // 
            this.recibos_cobrar.HeaderText = "Recibos_cobrar";
            this.recibos_cobrar.Name = "recibos_cobrar";
            this.recibos_cobrar.ReadOnly = true;
            // 
            // total_recibos
            // 
            this.total_recibos.HeaderText = "Total_pagar";
            this.total_recibos.Name = "total_recibos";
            this.total_recibos.ReadOnly = true;
            // 
            // transferencia
            // 
            this.transferencia.HeaderText = "Transferencia";
            this.transferencia.Name = "transferencia";
            this.transferencia.ReadOnly = true;
            // 
            // pago_efectivo
            // 
            this.pago_efectivo.HeaderText = "Pago_efectivo";
            this.pago_efectivo.Name = "pago_efectivo";
            this.pago_efectivo.ReadOnly = true;
            // 
            // pago_cheque
            // 
            this.pago_cheque.HeaderText = "Pago_cheque";
            this.pago_cheque.Name = "pago_cheque";
            this.pago_cheque.ReadOnly = true;
            // 
            // pago_tarjeta
            // 
            this.pago_tarjeta.HeaderText = "Pago_tarjeta";
            this.pago_tarjeta.Name = "pago_tarjeta";
            this.pago_tarjeta.ReadOnly = true;
            // 
            // saldo
            // 
            this.saldo.HeaderText = "Saldo";
            this.saldo.Name = "saldo";
            this.saldo.ReadOnly = true;
            // 
            // cambio
            // 
            this.cambio.HeaderText = "Cambio";
            this.cambio.Name = "cambio";
            this.cambio.ReadOnly = true;
            // 
            // fecha
            // 
            this.fecha.HeaderText = "Fecha_pago";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            // 
            // usuario_creo
            // 
            this.usuario_creo.HeaderText = "Usuario_Creo";
            this.usuario_creo.Name = "usuario_creo";
            this.usuario_creo.ReadOnly = true;
            // 
            // fecha_creo
            // 
            this.fecha_creo.HeaderText = "Fecha_Creo";
            this.fecha_creo.Name = "fecha_creo";
            this.fecha_creo.ReadOnly = true;
            // 
            // estado_recibo
            // 
            this.estado_recibo.HeaderText = "Estado_Recibo";
            this.estado_recibo.Name = "estado_recibo";
            this.estado_recibo.ReadOnly = true;
            // 
            // importe_cobrado
            // 
            this.importe_cobrado.HeaderText = "Importe_Cobrado";
            this.importe_cobrado.Name = "importe_cobrado";
            this.importe_cobrado.ReadOnly = true;
            // 
            // iva_cobrado
            // 
            this.iva_cobrado.HeaderText = "IVA_Cobrado";
            this.iva_cobrado.Name = "iva_cobrado";
            this.iva_cobrado.ReadOnly = true;
            // 
            // tasa_iva
            // 
            this.tasa_iva.HeaderText = "Tasa_IVA";
            this.tasa_iva.Name = "tasa_iva";
            this.tasa_iva.ReadOnly = true;
            // 
            // total_cobrado
            // 
            this.total_cobrado.HeaderText = "Total_Cobrado";
            this.total_cobrado.Name = "total_cobrado";
            this.total_cobrado.ReadOnly = true;
            // 
            // no_factura
            // 
            this.no_factura.HeaderText = "No_Factura";
            this.no_factura.Name = "no_factura";
            this.no_factura.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Lavender;
            this.label2.Location = new System.Drawing.Point(658, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Destino";
            // 
            // pBar1
            // 
            this.pBar1.Location = new System.Drawing.Point(367, 442);
            this.pBar1.Name = "pBar1";
            this.pBar1.Size = new System.Drawing.Size(446, 23);
            this.pBar1.TabIndex = 6;
            this.pBar1.Visible = false;
            // 
            // btn_regresar
            // 
            this.btn_regresar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_regresar.BackgroundImage")));
            this.btn_regresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_regresar.Location = new System.Drawing.Point(21, 428);
            this.btn_regresar.Name = "btn_regresar";
            this.btn_regresar.Size = new System.Drawing.Size(75, 58);
            this.btn_regresar.TabIndex = 8;
            this.btn_regresar.UseVisualStyleBackColor = true;
            this.btn_regresar.Click += new System.EventHandler(this.btn_regresar_Click);
            // 
            // dtg_origen
            // 
            this.dtg_origen.AllowUserToAddRows = false;
            this.dtg_origen.AllowUserToDeleteRows = false;
            this.dtg_origen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtg_origen.Location = new System.Drawing.Point(21, 120);
            this.dtg_origen.Name = "dtg_origen";
            this.dtg_origen.ReadOnly = true;
            this.dtg_origen.Size = new System.Drawing.Size(493, 292);
            this.dtg_origen.TabIndex = 0;
            // 
            // btn_import
            // 
            this.btn_import.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_import.Location = new System.Drawing.Point(537, 142);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(104, 43);
            this.btn_import.TabIndex = 9;
            this.btn_import.Text = "Import";
            this.btn_import.UseVisualStyleBackColor = true;
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Mistral", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.GhostWhite;
            this.label4.Location = new System.Drawing.Point(972, 460);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(201, 29);
            this.label4.TabIndex = 15;
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
            this.gb_1.ForeColor = System.Drawing.Color.Lavender;
            this.gb_1.Location = new System.Drawing.Point(710, 12);
            this.gb_1.Name = "gb_1";
            this.gb_1.Size = new System.Drawing.Size(446, 52);
            this.gb_1.TabIndex = 19;
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
            this.label3.ForeColor = System.Drawing.Color.Lavender;
            this.label3.Location = new System.Drawing.Point(98, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(244, 38);
            this.label3.TabIndex = 18;
            this.label3.Text = "Recibos Cobros";
            // 
            // lbl_destino
            // 
            this.lbl_destino.AutoSize = true;
            this.lbl_destino.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_destino.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_destino.Location = new System.Drawing.Point(1066, 88);
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
            this.lbl_origen.Location = new System.Drawing.Point(405, 88);
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
            this.label6.Location = new System.Drawing.Point(934, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 18);
            this.label6.TabIndex = 27;
            this.label6.Text = "No. Registros:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(273, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 18);
            this.label5.TabIndex = 26;
            this.label5.Text = "No. Registros:";
            // 
            // Recibos_Cobros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1182, 498);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_destino);
            this.Controls.Add(this.lbl_origen);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gb_1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_import);
            this.Controls.Add(this.btn_regresar);
            this.Controls.Add(this.pBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtg_destino);
            this.Controls.Add(this.btn_migrar);
            this.Controls.Add(this.btn_copy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtg_origen);
            this.Name = "Recibos_Cobros";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recibos_Cobros";
            this.Load += new System.EventHandler(this.Recibos_Cobros_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg_destino)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_origen)).EndInit();
            this.gb_1.ResumeLayout(false);
            this.gb_1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_copy;
        private System.Windows.Forms.Button btn_migrar;
        private System.Windows.Forms.DataGridView dtg_destino;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar pBar1;
        private System.Windows.Forms.Button btn_regresar;
        private System.Windows.Forms.DataGridView dtg_origen;
        private System.Windows.Forms.Button btn_import;
        private System.Windows.Forms.Label label4;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn rpu;
        private System.Windows.Forms.DataGridViewTextBoxColumn recibos_cobrar;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_recibos;
        private System.Windows.Forms.DataGridViewTextBoxColumn transferencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn pago_efectivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn pago_cheque;
        private System.Windows.Forms.DataGridViewTextBoxColumn pago_tarjeta;
        private System.Windows.Forms.DataGridViewTextBoxColumn saldo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cambio;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn usuario_creo;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_creo;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado_recibo;
        private System.Windows.Forms.DataGridViewTextBoxColumn importe_cobrado;
        private System.Windows.Forms.DataGridViewTextBoxColumn iva_cobrado;
        private System.Windows.Forms.DataGridViewTextBoxColumn tasa_iva;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_cobrado;
        private System.Windows.Forms.DataGridViewTextBoxColumn no_factura;
    }
}