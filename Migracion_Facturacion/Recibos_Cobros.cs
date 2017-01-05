using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Migracion_Facturacion
{
    public partial class Recibos_Cobros : Form
    {
        string db_origen = "Simapag_30_05_16";
        string db_destino = "Simapag_Pruebas"; //Simapag_Depurado

        public Recibos_Cobros()
        {
            InitializeComponent();
        }

        private void Recibos_Cobros_Load(object sender, EventArgs e)
        {
            rdb_1.Checked = true;
            btn_import.Enabled = true;
            btn_copy.Enabled = false;
            btn_migrar.Enabled = false;
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            #region Importacion de datos...
            class_pagos obj = new class_pagos();
            if (rdb_1.Checked)
            {
                dtg_origen.DataSource = obj.CargarPagos_Caso1(db_origen);
                lbl_origen.Text = dtg_origen.Rows.Count.ToString();
                MessageBox.Show("Caso 1 Cargado", "Migración Recibos Cobros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_import.Enabled = false;
                btn_copy.Enabled = true;
            }
            if (rdb_2.Checked)
            {
                dtg_origen.DataSource = obj.CargarPagos_Caso2(db_origen);
                lbl_origen.Text = dtg_origen.Rows.Count.ToString();
                MessageBox.Show("Caso 2 Cargado", "Migración Recibos Cobros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_import.Enabled = false;
                btn_copy.Enabled = true;
            }
            if (rdb_3.Checked)
            {
                //dtg_origen.DataSource = obj.CargarDetalles_Caso3();
                lbl_origen.Text = dtg_origen.Rows.Count.ToString();
                MessageBox.Show("Caso 3 Cargado!!", "Migración Recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_import.Enabled = false;
                btn_copy.Enabled = true;
            }
            if (rdb_4.Checked)
            {
                //dtg_origen.DataSource = obj.CargarDetalles_Caso4();
                lbl_origen.Text = dtg_origen.Rows.Count.ToString();
                MessageBox.Show("Caso 4 Cargado!!", "Migración Recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_import.Enabled = false;
                btn_copy.Enabled = true;
            }
            if (rdb_5.Checked)
            {
                //dtg_origen.DataSource = obj.CargarDetalles_Caso5();
                lbl_origen.Text = dtg_origen.Rows.Count.ToString();
                MessageBox.Show("Caso 5 Cargado!!", "Migración Recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_import.Enabled = false;
                btn_copy.Enabled = true;
            }
            #endregion

        }

        private void btn_copy_Click(object sender, EventArgs e)
        {
            if (rdb_1.Checked || rdb_2.Checked)
            {
                pBar1.Visible = true;
                pBar1.Minimum = 1;
                pBar1.Maximum = dtg_origen.Rows.Count * 2;
                pBar1.Value = 1;
                pBar1.Step = 1;
                btn_copy.Enabled = false;

                string[] col_rpu = new string[dtg_origen.Rows.Count];
                string[] col_total_pagar = new string[dtg_origen.Rows.Count];
                string[] col_importe = new string[dtg_origen.Rows.Count];
                string[] col_efectivo = new string[dtg_origen.Rows.Count];
                string[] col_cheque = new string[dtg_origen.Rows.Count];
                string[] col_tdc = new string[dtg_origen.Rows.Count];
                string[] col_fecha_pago = new string[dtg_origen.Rows.Count];
                string[] col_foliorecib = new string[dtg_origen.Rows.Count];
                string[] col_importe_cobrado = new string[dtg_origen.Rows.Count];
                string[] col_iva_cobrado = new string[dtg_origen.Rows.Count];
                string[] col_tasa_iva = new string[dtg_origen.Rows.Count];
                string[] col_total_cobrado = new string[dtg_origen.Rows.Count];

                float[] saldo = new float[dtg_origen.Rows.Count];
                float[] tasa_iva = new float[dtg_origen.Rows.Count];
                string[] estado = new string[dtg_origen.Rows.Count];

                int i = 0;
                try
                {
                    foreach (DataGridViewRow row in dtg_origen.Rows)
                    {
                        col_rpu[i] = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : string.Empty;
                        col_total_pagar[i] = row.Cells[1].Value != null ? row.Cells[1].Value.ToString() : string.Empty;
                        col_importe[i] = row.Cells[2].Value != null ? row.Cells[2].Value.ToString() : string.Empty;
                        col_efectivo[i] = row.Cells[3].Value != null ? row.Cells[3].Value.ToString() : string.Empty;
                        col_cheque[i] = row.Cells[4].Value != null ? row.Cells[4].Value.ToString() : string.Empty;
                        col_tdc[i] = row.Cells[5].Value != null ? row.Cells[5].Value.ToString() : string.Empty;
                        col_fecha_pago[i] = row.Cells[6].Value != null ? row.Cells[6].Value.ToString() : string.Empty;
                        col_foliorecib[i] = row.Cells[7].Value != null ? row.Cells[7].Value.ToString() : string.Empty;
                        col_importe_cobrado[i] = row.Cells[8].Value != null ? row.Cells[8].Value.ToString() : string.Empty;
                        col_iva_cobrado[i] = row.Cells[9].Value != null ? row.Cells[9].Value.ToString() : string.Empty;
                        col_tasa_iva[i] = row.Cells[10].Value != null ? row.Cells[10].Value.ToString() : string.Empty;
                        col_total_cobrado[i] = row.Cells[11].Value != null ? row.Cells[11].Value.ToString() : string.Empty;
                        i++;
                    }
                    //MessageBox.Show("Copy Process Completed!!");

                    int tope = dtg_origen.Rows.Count;
                    for (int j = 0; j < tope; j++)
                    {
                        if (j > 0)
                        {
                            if (col_foliorecib[j] == col_foliorecib[j - 1])
                                saldo[j] = saldo[j - 1] - float.Parse(col_importe[j]);
                            else
                                saldo[j] = float.Parse(col_total_pagar[j]) - float.Parse(col_importe[j]);
                        }
                        if (Boolean.Parse(col_tasa_iva[j]))
                            tasa_iva[j] = 16;
                        else
                            tasa_iva[j] = 0;
                        if (saldo[j] < 0)
                            estado[j] = "A FAVOR";
                        if (saldo[j] == 0)
                            estado[j] = "PAGADO";
                        if (saldo[j] > 0)
                            estado[j] = "PENDIENTE";
                        pBar1.PerformStep();
                    }
                    dtg_destino.Rows.Clear();
                    for (int k = 0; k < tope; k++)
                    {
                        dtg_destino.Rows.Add();
                        dtg_destino.Rows[k].Cells[0].Value = col_rpu[k];
                        dtg_destino.Rows[k].Cells[1].Value = 1;
                        dtg_destino.Rows[k].Cells[2].Value = col_total_pagar[k];
                        dtg_destino.Rows[k].Cells[3].Value = 0;
                        dtg_destino.Rows[k].Cells[4].Value = col_efectivo[k];
                        dtg_destino.Rows[k].Cells[5].Value = col_cheque[k];
                        dtg_destino.Rows[k].Cells[6].Value = col_tdc[k];
                        dtg_destino.Rows[k].Cells[7].Value = saldo[k];
                        dtg_destino.Rows[k].Cells[8].Value = 0;
                        dtg_destino.Rows[k].Cells[9].Value = col_fecha_pago[k];
                        dtg_destino.Rows[k].Cells[10].Value = "MIGRACION";
                        dtg_destino.Rows[k].Cells[11].Value = DateTime.Today;
                        dtg_destino.Rows[k].Cells[12].Value = estado[k];
                        dtg_destino.Rows[k].Cells[13].Value = col_importe_cobrado[k];
                        dtg_destino.Rows[k].Cells[14].Value = col_iva_cobrado[k];
                        dtg_destino.Rows[k].Cells[15].Value = tasa_iva[k];
                        dtg_destino.Rows[k].Cells[16].Value = col_total_cobrado[k];
                        dtg_destino.Rows[k].Cells[17].Value = col_foliorecib[k];
                        pBar1.PerformStep();
                    }
                    lbl_destino.Text = dtg_destino.Rows.Count.ToString();
                    if (rdb_1.Checked)
                        MessageBox.Show("Caso 1 Copiado!", "Migración Recibos Cobros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Caso 2 Copiado!", "Migración Recibos Cobros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_migrar.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btn_copy.Enabled = true;
                }
            }
            if (rdb_3.Checked)
            { 
            
            }
            if (rdb_4.Checked)
            { 
            
            }
            if (rdb_5.Checked)
            { 
            
            }
        }

        private void btn_migrar_Click(object sender, EventArgs e)
        {
            #region Migracion Datos...
            DialogResult result;
            result = MessageBox.Show("¿Seguro de querer continuar?","Migración",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                pBar1.Visible = true;
                pBar1.Minimum = 1;
                pBar1.Maximum = dtg_destino.Rows.Count;
                pBar1.Value = 1;
                pBar1.Step = 1;
                SqlConnection conn = new SqlConnection("Data Source=172.16.0.115;Initial Catalog="+db_destino+";User ID=usrsimapag;Password=C0nt3l16");
                string query = "INSERT INTO Ope_Cor_Caj_Recibos_Cobros (RPU,RECIBOS_COBRAR,TOTAL_RECIBOS,TRANSFERENCIA,PAGO_EFECTIVO,PAGO_CHEQUE,PAGO_TARJETA,"
	                +"SALDO,CAMBIO,FECHA,USUARIO_CREO,FECHA_CREO,estado_recibo,importe_cobrado,total_iva_cobrado,tasa_iva_cobrado,"
	                +"total_cobrado_ajuste,NO_FACTURA) "
                    + "VALUES (@rpu,@recibos_cobrar,@total_recibos,@transferencia,@pago_efectivo,@pago_cheque,@pago_tarjeta,@saldo,"
                    + "@cambio,@fecha,@usuario_creo,@fecha_creo,@estado,@importe_cobrado,@iva_cobrado,@tasa_iva,@total_cobrado,@no_factura)";

                //try
                //{
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    foreach (DataGridViewRow row in dtg_destino.Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@rpu", row.Cells[0].Value.ToString());
                        cmd.Parameters.AddWithValue("@recibos_cobrar", int.Parse(row.Cells[1].Value.ToString()));
                        cmd.Parameters.AddWithValue("@total_recibos", float.Parse(row.Cells[2].Value.ToString()));
                        cmd.Parameters.AddWithValue("@transferencia", float.Parse(row.Cells[3].Value.ToString()));
                        cmd.Parameters.AddWithValue("@pago_efectivo", float.Parse(row.Cells[4].Value.ToString()));
                        cmd.Parameters.AddWithValue("@pago_cheque", float.Parse(row.Cells[5].Value.ToString()));
                        cmd.Parameters.AddWithValue("@pago_tarjeta", float.Parse(row.Cells[6].Value.ToString()));
                        cmd.Parameters.AddWithValue("@saldo", float.Parse(row.Cells[7].Value.ToString()));
                        cmd.Parameters.AddWithValue("@cambio", float.Parse(row.Cells[8].Value.ToString()));
                        cmd.Parameters.AddWithValue("@fecha", DateTime.Parse(row.Cells[9].Value.ToString()));
                        cmd.Parameters.AddWithValue("@usuario_creo", row.Cells[10].Value.ToString());
                        cmd.Parameters.AddWithValue("@fecha_creo", DateTime.Parse(row.Cells[11].Value.ToString()));
                        cmd.Parameters.AddWithValue("@estado", row.Cells[12].Value.ToString());
                        cmd.Parameters.AddWithValue("@importe_cobrado", float.Parse(row.Cells[13].Value.ToString()));
                        cmd.Parameters.AddWithValue("@iva_cobrado", float.Parse(row.Cells[14].Value.ToString()));
                        cmd.Parameters.AddWithValue("@tasa_iva", float.Parse(row.Cells[15].Value.ToString()));
                        cmd.Parameters.AddWithValue("@total_cobrado", float.Parse(row.Cells[16].Value.ToString()));
                        cmd.Parameters.AddWithValue("@no_factura", row.Cells[17].Value.ToString());
                        cmd.ExecuteNonQuery();
                        pBar1.PerformStep();
                    }
                    conn.Close();
                    MessageBox.Show("Datos Migrados!!", "Migración Recibos Cobros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pBar1.Visible = false;
                    btn_migrar.Enabled = false;
                    if (rdb_1.Checked)
                    {
                        rdb_1.Text = "X";
                        rdb_1.Enabled = false;
                        rdb_1.Checked = false;
                    }
                    if (rdb_2.Checked)
                    {
                        rdb_2.Text = "X";
                        rdb_2.Enabled = false;
                        rdb_2.Checked = false;
                    }
                    if (rdb_3.Checked)
                    {
                        rdb_3.Text = "X";
                        rdb_3.Enabled = false;
                        rdb_3.Checked = false;
                    }
                    if (rdb_4.Checked)
                    {
                        rdb_4.Text = "X";
                        rdb_4.Enabled = false;
                        rdb_4.Checked = false;
                    }
                    if (rdb_5.Checked)
                    {
                        rdb_5.Text = "X";
                        rdb_5.Enabled = false;
                        rdb_5.Checked = false;
                    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            #endregion
        }

        private void btn_regresar_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            this.Hide();
            obj.Show();
        }

        private void rdb_1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    // Only one radio button will be checked
                    //MessageBox.Show("Changed: " + rb.Name);
                    btn_import.Enabled = true;
                    btn_copy.Enabled = false;
                    btn_migrar.Enabled = false;
                }
            }
        }
    }
}
