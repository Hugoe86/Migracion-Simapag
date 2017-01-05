using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Migracion_Facturacion
{
    public partial class Movimientos_Cobros : Form
    {
        string db_origen = "Simapag_30_05_16";
        string db_destino = "Simapag_Pruebas"; //Simapag_Depurado


        public Movimientos_Cobros()
        {
            InitializeComponent();
        }

        private void Movimientos_Cobros_Load(object sender, EventArgs e)
        {
            rdb_1.Checked = true;
            btn_import.Enabled = true;
            btn_copy.Enabled = false;
            btn_migrar.Enabled = false;
        }

        private void btn_regresar_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            this.Hide();
            obj.Show();
        }

        private void btn_import_Click(object sender, EventArgs e)  // ---------------- Llenado del Datagrid Origen segun el escenario --------------- \\
        {
            #region Importacion de datos...
            class_movimientos obj = new class_movimientos();
            if (rdb_1.Checked)
            {
                dtg_origen.DataSource = obj.CargarRecibos_Caso1(db_origen, db_destino);
                lbl_origen.Text = dtg_origen.Rows.Count.ToString();
                MessageBox.Show("Caso 1 Cargado", "Migración Recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_import.Enabled = false;
                btn_copy.Enabled = true;
            }
            if (rdb_3.Checked)
            {
                dtg_origen.DataSource = obj.CargarRecibos_Caso3(db_origen, db_destino);
                lbl_origen.Text = dtg_origen.Rows.Count.ToString();
                MessageBox.Show("Caso 3 Cargado", "Migración Recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_import.Enabled = false;
                btn_copy.Enabled = true;
            }
            if (rdb_5.Checked)
            {
                dtg_origen.DataSource = obj.CargarRecibos_Caso5(db_origen, db_destino);
                lbl_origen.Text = dtg_origen.Rows.Count.ToString();
                MessageBox.Show("Caso 5 Cargado", "Migración Recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_import.Enabled = false;
                btn_copy.Enabled = true;
            }
            #endregion
        }

        private void btn_copy_Click(object sender, EventArgs e)  // ------------------ Copiado de datos, operaciones y llenado del datagrid Destino --------------- \\
        {
            class_movimientos obj = new class_movimientos();
            DataTable dt_tarifas = new DataTable();
            dt_tarifas = obj.CargarTarifasDetalles(db_destino);

            DataTable Dt_Datos = new DataTable();
            Dt_Datos.Columns.Add("no_factura");
            Dt_Datos.Columns.Add("no_cuenta");
            Dt_Datos.Columns.Add("no_recibo");
            Dt_Datos.Columns.Add("region_id");
            Dt_Datos.Columns.Add("predio_id");
            Dt_Datos.Columns.Add("usuario_id");
            Dt_Datos.Columns.Add("medidor_id");
            Dt_Datos.Columns.Add("tarifa_id");
            Dt_Datos.Columns.Add("lec_anterior");
            Dt_Datos.Columns.Add("lectura_actual");
            Dt_Datos.Columns.Add("consumo");
            Dt_Datos.Columns.Add("cuota_base");
            Dt_Datos.Columns.Add("cuata_consumo");
            Dt_Datos.Columns.Add("precio_m3");
            Dt_Datos.Columns.Add("fecha_inicio");
            Dt_Datos.Columns.Add("fecha_termino");
            Dt_Datos.Columns.Add("fecha_limite");
            Dt_Datos.Columns.Add("fecha_emision");
            Dt_Datos.Columns.Add("periodo");
            Dt_Datos.Columns.Add("tasa_iva");
            Dt_Datos.Columns.Add("total_importe");
            Dt_Datos.Columns.Add("total_iva");
            Dt_Datos.Columns.Add("total_pagar");
            Dt_Datos.Columns.Add("total_abono");
            Dt_Datos.Columns.Add("saldo");
            Dt_Datos.Columns.Add("estado");
            Dt_Datos.Columns.Add("anio");
            Dt_Datos.Columns.Add("bimestre");
            Dt_Datos.Columns.Add("rpu");
            Dt_Datos.Columns.Add("pagua");
            Dt_Datos.Columns.Add("palcan");
            Dt_Datos.Columns.Add("psanea");
            Dt_Datos.Columns.Add("recagua");
            Dt_Datos.Columns.Add("recalcan");
            Dt_Datos.Columns.Add("recsanea");
            Dt_Datos.Columns.Add("crbomb");
            Dt_Datos.Columns.Add("iva_agua");
            Dt_Datos.Columns.Add("iva_alcan");
            Dt_Datos.Columns.Add("iva_sanea");
            Dt_Datos.Columns.Add("abono_agua");
            Dt_Datos.Columns.Add("abono_alcan");
            Dt_Datos.Columns.Add("abono_sanea");
            Dt_Datos.Columns.Add("abono_recagua");
            Dt_Datos.Columns.Add("abono_recalcan");
            Dt_Datos.Columns.Add("abono_recsanea");
            Dt_Datos.Columns.Add("abono_crbomb");
            Dt_Datos.Columns.Add("abono_IVA_agua");
            Dt_Datos.Columns.Add("abono_IVA_alcan");
            Dt_Datos.Columns.Add("abono_IVA_sanea");
            Dt_Datos.Columns.Add("anticipo");
            Dt_Datos.Columns.Add("codigo_barras");
            Dt_Datos.Columns.Add("fecha_pago");

            if (rdb_1.Checked)  // ---- Inicia el escenario 1 ---- 
            {
                #region Escenario 1 ...
            //    btn_copy.Enabled = false;
            //    pBar1.Visible = true;
            //    pBar1.Minimum = 1;
            //    pBar1.Maximum = dtg_origen.Rows.Count;
            //    pBar1.Value = 1;
            //    pBar1.Step = 1;

            //    //---------------------------------------------------------------------------------------------------------------------------------

            //    double pagua, palcan, psanea, crbomb;
            //    double iva_agua, iva_alcan, iva_sanea;
            //    double pago_agua, pago_alcan, pago_sanea, pago_crbomb;
            //    double pago_iva_agua, pago_iva_alcan, pago_iva_sanea;
            //    double total_importe, total_iva, total_pagar = 0;
            //    double total_abonado = 0;
            //    double total_saldo = 0;
            //    double ppagua, ppalcan, ppsanea, pcrbomb, piva, panticipo;
            //    double ajuste_iva;
            //    double cuota_base = 0;
            //    double cuota_consumo = 0;
            //    double precio_m3 = 0;
            //    string fecha;
            //    int no_reg = obj.Consulta_id(db_destino);
            //    string automatic_id;
            //    int x = 1;

            //    DataTable dt_origen = (DataTable)dtg_origen.DataSource;

            //    foreach (DataRow encabezado in dt_origen.Rows)
            //    {
            //        DataRow Dr = Dt_Datos.NewRow();
            //        automatic_id = "0000000000" + (no_reg + x).ToString();
            //        automatic_id = automatic_id.Substring(automatic_id.Length - 10, 10);
            //        x++;
            //        Dr["no_factura"] = automatic_id;
            //        Dr["no_cuenta"] = encabezado["cuenta"];
            //        Dr["no_recibo"] = encabezado["foliorecib"];
            //        Dr["region_id"] = encabezado["region_id"];
            //        Dr["predio_id"] = encabezado["predio_id"];
            //        Dr["usuario_id"] = encabezado["usuario_id"];
            //        Dr["medidor_id"] = encabezado["nummedidor"];
            //        Dr["tarifa_id"] = encabezado["tarifa_id"];
            //        Dr["lec_anterior"] = encabezado["lecanterior"];
            //        Dr["lec_actual"] = encabezado["lecactual"];
            //        Dr["consumo"] = encabezado["consumo"];
            //        Dr["fecha_inicio"] = encabezado["fecha_inicio"];
            //        Dr["fecha_termino"] = encabezado["fecha_termino"];
            //        Dr["fecha_limite"] = encabezado["vencimient"];
            //        Dr["fecha_emision"] = encabezado["facturacion"];
            //        Dr["periodo"] = encabezado["periodo"];
            //        if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //        {
            //            Dr["tasa_iva"] = "16";
            //        }
            //        else
            //        {
            //            Dr["tasa_iva"] = "0";
            //        }

            //        pagua = double.Parse(encabezado["pagua"].ToString());
            //        palcan = double.Parse(encabezado["palcan"].ToString());
            //        psanea = double.Parse(encabezado["psanea"].ToString());
            //        crbomb = double.Parse(encabezado["crbomb"].ToString());
            //        pago_agua = 0;
            //        pago_alcan = 0;
            //        pago_sanea = 0;
            //        pago_iva_agua = 0;
            //        pago_iva_alcan = 0;
            //        pago_iva_sanea = 0;
            //        pago_crbomb = 0;
            //        total_abonado = 0;
            //        ppagua = double.Parse(encabezado["ppagua"].ToString());
            //        ppalcan = double.Parse(encabezado["ppalcan"].ToString());
            //        ppsanea = double.Parse(encabezado["ppsanea"].ToString());
            //        pcrbomb = double.Parse(encabezado["pcrbomb"].ToString());
            //        piva = double.Parse(encabezado["piva"].ToString());
            //        panticipo = double.Parse(encabezado["panticipo"].ToString());

            //        // ------------------------------- CALCULANDO IVA ---------------------------------- \\
            //        if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //        {
            //            iva_agua = pagua * 0.16;
            //        }
            //        else
            //        {
            //            iva_agua = 0;
            //        }
            //        iva_alcan = palcan * 0.16;
            //        iva_sanea = psanea * 0.16;
            //        ajuste_iva = double.Parse(encabezado["iva"].ToString()) - (iva_agua + iva_alcan + iva_sanea);
            //        if (double.Parse(encabezado["iva"].ToString()) > 0)
            //        {
            //            iva_sanea += ajuste_iva;
            //            if (iva_sanea < 0)
            //            {
            //                iva_alcan += iva_sanea;
            //                iva_sanea = 0;
            //                if (iva_alcan < 0)
            //                {
            //                    iva_agua += iva_alcan;
            //                    iva_alcan = 0;
            //                    if (iva_agua < 0)
            //                    {
            //                        iva_agua = 0;
            //                    }
            //                }
            //            }
            //        }
            //        // ------------------------------ FIN CALCULANDO IVA ---------------------------------

            //        total_importe = pagua + palcan + psanea + crbomb;
            //        total_iva = iva_agua + iva_alcan + iva_sanea;
            //        total_pagar = total_importe + total_iva;

            //        // ---------- * * * * - APLICACION DE PAGOS - * * * * --------------
            //        if (pagua <= ppagua)
            //        {
            //            pago_agua = pagua;
            //            total_abonado += pagua;
            //            ppagua -= pagua;
            //        }
            //        else
            //        {
            //            pago_agua = ppagua;
            //            total_abonado += ppagua;
            //            ppagua = 0;
            //        }
            //        if (palcan <= ppalcan)
            //        {
            //            pago_alcan = palcan;
            //            total_abonado += palcan;
            //            ppalcan -= palcan;
            //        }
            //        else
            //        {
            //            pago_alcan = ppalcan;
            //            total_abonado += ppalcan;
            //            ppalcan = 0;
            //        }
            //        if (psanea <= ppsanea)
            //        {
            //            pago_sanea = psanea;
            //            total_abonado += psanea;
            //            ppsanea -= psanea;
            //        }
            //        else
            //        {
            //            pago_sanea = ppsanea;
            //            total_abonado += ppsanea;
            //            ppsanea = 0;
            //        }
            //        if (crbomb <= pcrbomb)
            //        {
            //            pago_crbomb = crbomb;
            //            total_abonado += crbomb;
            //            pcrbomb -= crbomb;
            //        }
            //        else
            //        {
            //            pago_crbomb = pcrbomb;
            //            total_abonado += pcrbomb;
            //            pcrbomb = 0;
            //        }
            //        if (iva_agua <= piva)
            //        {
            //            pago_iva_agua = iva_agua;
            //            total_abonado += iva_agua;
            //            piva -= iva_agua;
            //        }
            //        else
            //        {
            //            pago_iva_agua = piva;
            //            total_abonado = piva;
            //            piva = 0;
            //        }
            //        if (iva_alcan <= piva)
            //        {
            //            pago_iva_alcan = iva_alcan;
            //            total_abonado += iva_alcan;
            //            piva -= iva_alcan;
            //        }
            //        else
            //        {
            //            pago_iva_alcan = piva;
            //            total_abonado += piva;
            //            piva = 0;
            //        }
            //        if (iva_sanea <= piva)
            //        {
            //            pago_iva_sanea = iva_sanea;
            //            total_abonado += iva_sanea;
            //            piva -= iva_sanea;
            //        }
            //        else
            //        {
            //            pago_sanea = piva;
            //            total_abonado += piva;
            //            piva = 0;
            //        }
            //        total_saldo = total_pagar - total_abonado;

            //        Dr["total_importe"] = Math.Round(total_importe, 2);
            //        Dr["total_iva"] = Math.Round(total_iva, 2);
            //        Dr["total_pagar"] = Math.Round(total_pagar, 2);
            //        Dr["total_abono"] = Math.Round(total_abonado, 2);
            //        Dr["fac_Saldo"] = Math.Round(total_saldo, 2);
            //        if (total_saldo == 0 || total_saldo - crbomb == 0)
            //        {
            //            Dr["estado"] = "PAGADO";
            //        }
            //        else
            //        {
            //            Dr["estado"] = "PENDIENTE";
            //        }
            //        Dr["anio"] = encabezado["anio"];
            //        Dr["bimestre"] = encabezado["bimestre"];
            //        Dr["rpu"] = encabezado["rpu"];
            //        Dr["pagua"] = Math.Round(pagua, 2);
            //        Dr["palcan"] = Math.Round(palcan, 2);
            //        Dr["psanea"] = Math.Round(psanea, 2);
            //        Dr["recagua"] = 0;
            //        Dr["recalcan"] = 0;
            //        Dr["recsanea"] = 0;
            //        Dr["crbomb"] = Math.Round(crbomb, 2);
            //        Dr["iva_agua"] = Math.Round(iva_agua, 2);
            //        Dr["iva_alcan"] = Math.Round(iva_alcan, 2);
            //        Dr["iva_sanea"] = Math.Round(iva_sanea, 2);
            //        Dr["abono_agua"] = Math.Round(pago_agua, 2);
            //        Dr["abono_alcan"] = Math.Round(pago_alcan, 2);
            //        Dr["abonosanea"] = Math.Round(pago_sanea, 2);
            //        Dr["abono_recagua"] = 0;
            //        Dr["abono_recalcan"] = 0;
            //        Dr["abono_recsanea"] = 0;
            //        Dr["abono_crbomb"] = Math.Round(pago_crbomb, 2);
            //        Dr["abono_IVA_agua"] = Math.Round(pago_iva_agua, 2);
            //        Dr["abono_IVA_alcan"] = Math.Round(pago_iva_alcan, 2);
            //        Dr["abono_IVA_sanea"] = Math.Round(pago_iva_sanea, 2);
            //        Dr["anticipo"] = Math.Round(panticipo, 2);
            //        Dr["Codigo_Barras"] = automatic_id + "F";
            //        fecha = encabezado["fechapago"].ToString().Substring(0, 10);
            //        Dr["Fecha_Pago"] = DateTime.Parse(fecha);

            //        //cuota_base = dt_tarifas.Select();

            //        Dr["fac_Cuota_Base"] = Math.Round(cuota_base, 2);
            //        Dr["fac_Cuata_Consumo"] = Math.Round(cuota_consumo, 2);
            //        Dr["fac_Precio_M3"] = Math.Round(precio_m3, 2);

            //        Dt_Datos.Rows.Add(Dr);

            //        pBar1.PerformStep();

            //    }//--- End foreach

            //    dtg_destino.DataSource = Dt_Datos;

            //    lbl_destino.Text = dtg_destino.Rows.Count.ToString();
            //    if (rdb_1.Checked)
            //        MessageBox.Show("Caso 1 Copiado!", "Migración Recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    else
            //        MessageBox.Show("Caso 2 Copiado!", "Migración Recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    btn_migrar.Enabled = true;
            //    pBar1.Visible = false;
            //    #endregion
            //    //---------------------------------------------------------------------------------------------------------------------------------
            //}
            //if (rdb_3.Checked)  // ----------------- Recibos con adeudos -----------------
            //{
            //    #region Escenario 3...
            //    btn_copy.Enabled = false;
            //    pBar1.Visible = true;
            //    pBar1.Minimum = 1;
            //    pBar1.Maximum = dtg_origen.Rows.Count;  // -------------- 2 ciclos
            //    pBar1.Value = 1;
            //    pBar1.Step = 1;

            //    Stopwatch watch = new Stopwatch();
            //    watch.Start();

            //    float[] tasa_iva = new float[dtg_origen.Rows.Count];
            //    float cuota_base = 0;
            //    float cuota_consumo = 0;
            //    float precio_m3 = 0;
            //    string[] estado_mes = new string[dtg_origen.Rows.Count];

            //    string[] his_foliorecib;
            //    string[] his_lecactual;
            //    string[] his_lecanterior;
            //    string[] his_consumo;
            //    string[] his_fecha_inicio;
            //    string[] his_fecha_termino;
            //    string[] his_vencimient;
            //    string[] his_facturacion;
            //    string[] his_periodo;
            //    string[] his_pagua;
            //    string[] his_palcan;
            //    string[] his_psanea;
            //    string[] his_rezagua;
            //    string[] his_rezalcan;
            //    string[] his_rezsanea;
            //    string[] his_recagua;
            //    string[] his_recalcan;
            //    string[] his_recsanea;
            //    string[] his_crbomb;
            //    string[] his_crbomb_aux;
            //    string[] his_iva;
            //    string[] his_importepago;
            //    string[] his_ppagua;
            //    string[] his_ppalcan;
            //    string[] his_ppsanea;
            //    string[] his_prezagua;
            //    string[] his_prezalcan;
            //    string[] his_prezsanea;
            //    string[] his_precagua;
            //    string[] his_precalcan;
            //    string[] his_precsanea;
            //    string[] his_pcrbomb;
            //    string[] his_piva;
            //    string[] his_estado;
            //    string[] his_anio;
            //    string[] his_bimestre;
            //    string[] his_rpu;
            //    string[] his_fechapago;

            //    float[] his_tasa_iva;
            //    float[] his_cuota_base;
            //    float[] his_cuota_consumo;
            //    float[] his_precio_m3;
            //    string[] his_auto_id;
            //    string Estatus = null;
            //    //string Gu_Fecha_Facturacion = null;
            //    //string Gu_Anio = null;
            //    //string Gu_Bimestre = null;
            //    int aux_i;
            //    string[] fac_No_Factura;
            //    string[] fac_No_Cuenta;
            //    string[] fac_No_Recibo;
            //    string[] fac_Region_ID;
            //    string[] fac_Predio_ID;
            //    string[] fac_Usuario_ID;
            //    string[] fac_Medidor_ID;
            //    string[] fac_Tarifa_ID;
            //    string[] fac_Lectura_Anterior;
            //    string[] fac_Lectura_Actual;
            //    string[] fac_Consumo;
            //    string[] fac_Cuota_Base;
            //    string[] fac_Cuata_Consumo;
            //    string[] fac_Precio_M3;
            //    string[] fac_Fecha_Inicio;
            //    string[] fac_Fecha_Termino;
            //    string[] fac_Fecha_Limite;
            //    string[] fac_Fecha_Emicio;
            //    string[] fac_Periodo;
            //    string[] fac_Tasa_IVA;
            //    double[] fac_Total_Importe;
            //    double[] fac_Total_IVA;
            //    double[] fac_Total_Pagado;
            //    double[] fac_Total_Abono;
            //    double[] fac_Saldo;
            //    string[] fac_Estado;
            //    string[] fac_Usuario_Creo;
            //    string[] fac_Fecha_Creo;
            //    string[] fac_Anio;
            //    string[] fac_Bimestre;
            //    string[] fac_RPU;
            //    double crbomb;
            //    double iva;
            //    double ajuste_iva = 0;

            //    double prezagua = 0;
            //    double prezalcan = 0;
            //    double prezsanea = 0;
            //    double precagua = 0;
            //    double precalcan = 0;
            //    double precsanea = 0;
            //    double pcrbomb = 0;
            //    double piva = 0;

            //    double[] pago_rezagua;
            //    double[] pago_rezalcan;
            //    double[] pago_rezsanea;
            //    double[] pago_recagua;
            //    double[] pago_recalcan;
            //    double[] pago_recsanea;
            //    double[] pago_crbomb;
            //    double[] pago_iva;
            //    double[] pago_iva_agua;
            //    double[] pago_iva_alcan;
            //    double[] pago_iva_sanea;

            //    double[] aux_pagua;
            //    double[] aux_palcan;
            //    double[] aux_psanea;
            //    double[] aux_recagua;
            //    double[] aux_recalcan;
            //    double[] aux_recsanea;
            //    double[] aux_crbomb;
            //    double[] aux_iva_agua;
            //    double[] aux_iva_alcan;
            //    double[] aux_iva_sanea;

            //    double[] his_iva_agua;
            //    double[] his_iva_alcan;
            //    double[] his_iva_sanea;
            //    double[] his_recagua_aux;
            //    double[] his_recalcan_aux;
            //    double[] his_recsanea_aux;
            //    double iva_acumulado;
            //    DataTable dt_recibos = (DataTable)dtg_origen.DataSource;
            //    DataTable dt_fechas;
            //    DataTable dt_historico = null;
            //    double actual_pagua = 0;
            //    double actual_palcan = 0;
            //    double actual_psanea = 0;
            //    double actual_recagua = 0;
            //    double actual_recalcan = 0;
            //    double actual_recsanea = 0;
            //    double actual_iva = 0;
            //    double actual_crbomb = 0;
            //    double actual_ppagua;
            //    double actual_ppalcan;
            //    double actual_ppsanea;
            //    double actual_prezagua;
            //    double actual_prezalcan;
            //    double actual_prezsanea;
            //    double actual_iva_agua;
            //    double actual_iva_alcan;
            //    double actual_iva_sanea;
            //    double actual_piva;
            //    double actual_pcrbomb;
            //    double actual_pago_agua;
            //    double actual_pago_alcan;
            //    double actual_pago_sanea;
            //    double actual_pago_iva_agua;
            //    double actual_pago_iva_alcan;
            //    double actual_pago_iva_sanea;
            //    double actual_pago_crbomb;
            //    double actual_total_abonado;
            //    string automatic_id;
            //    bool bandera;
            //    bool sinp = false;
            //    int no_Reg = obj.Consulta_id(db_destino);
            //    int x = 1;
                
            //    foreach (DataRow encabezado in dt_recibos.Rows)
            //    {
            //        //pBar1.PerformStep();
            //        Estatus = "NA";
            //        bandera = true;
            //        crbomb = 0;
            //        iva = 0;
            //        iva_acumulado = 0;
            //        actual_total_abonado = 0;

            //        //el dato de fechas ya se trae en la consulta inicial
            //        //dt_fechas = obj.Consulta_Rango_fechas(encabezado["rpu"].ToString(), db_origen);
            //        //if (dt_fechas.Rows.Count == 0)
            //        if (string.IsNullOrEmpty(encabezado["fecha_con"].ToString()))
            //        {
            //            ///eso es porque no hay fechas; no se encuentra ningun pagado en el historial

            //            //DataRow[] dr_Historicos = dt_historico_completo.Select("rpu='" + encabezado["rpu"].ToString() + "'" + " and estado <> 'NO MODIF'");
            //            //if (dr_Historicos.Length > 0)
            //            //{
            //            //    dt_historico = dr_Historicos.CopyToDataTable();
            //            //}

            //            //dt_historico = obj.Consulta_historico(encabezado["rpu"].ToString(), db_origen);
            //            sinp = true;

            //        }
            //        else
            //        {
            //            ///se tiene el dato de las fechas; si existen es porque hay un pagado en el historial
            //            ///
            //            //DataRow[] dr_Historicos = dt_historico_completo.Select("rpu='" + encabezado["rpu"].ToString() + "'"
            //            //                                                        + " and " + "facturacion BETWEEN '" + encabezado["fecha_con"].ToString() + "' and '" + encabezado["fecha_act"].ToString() + "'" + " and estado <> 'NO MODIF'");
            //            //if (dr_Historicos.Length > 0)
            //            //{
            //            //    dt_historico = dr_Historicos.CopyToDataTable();
            //            //}

            //            //dt_historico = obj.Consulta_historico_confechas(encabezado["rpu"].ToString(), encabezado["fecha_con"].ToString(), encabezado["fecha_act"].ToString(), db_origen);

            //            //foreach (DataRow fila_Fecha in dt_fechas.Rows)
            //            //{
            //            //    dt_historico = obj.Consulta_historico_confechas(encabezado["rpu"].ToString(), fila_Fecha["fecha_con"].ToString(), fila_Fecha["fecha_act"].ToString(), db_origen);
            //            //}  

            //        }




            //        if (dt_historico == null)
            //        {
            //            #region 2 facturitas!!...

            //            double anterior_rezagua = double.Parse(encabezado["rezagua"].ToString());
            //            double anterior_rezalcan = double.Parse(encabezado["rezalcan"].ToString());
            //            double anterior_rezsanea = double.Parse(encabezado["rezsanea"].ToString());
            //            double anterior_recagua = double.Parse(encabezado["recagua"].ToString());
            //            double anterior_recalcan = double.Parse(encabezado["recalcan"].ToString());
            //            double anterior_recsanea = double.Parse(encabezado["recsanea"].ToString());

            //            double anterior_iva_agua;
            //            if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //            {
            //                anterior_iva_agua = anterior_rezagua * 0.16;
            //            }
            //            else
            //            {
            //                anterior_iva_agua = 0;
            //            }
            //            double anterior_iva_alcan = anterior_rezalcan * 0.16;
            //            double anterior_iva_sanea = anterior_rezsanea * 0.16;
            //            double anterior_pago_agua = 0;
            //            double anterior_pago_alcan = 0;
            //            double anterior_pago_sanea = 0;
            //            double anterior_pago_recagua = 0;
            //            double anterior_pago_recalcan = 0;
            //            double anterior_pago_recsanea = 0;
            //            double anterior_pago_iva_agua = 0;
            //            double anterior_pago_iva_alcan = 0;
            //            double anterior_pago_iva_sanea = 0;
            //            double anterior_total_importe = anterior_rezagua + anterior_rezalcan + anterior_rezsanea + anterior_recagua + anterior_recalcan + anterior_recsanea;
            //            double anterior_total_iva = 0;

            //            if (double.Parse(encabezado["iva"].ToString()) > 0)
            //            {
            //                anterior_total_iva = anterior_iva_agua + anterior_iva_alcan + anterior_iva_sanea;
            //            }
            //            double anterior_total_pagado = anterior_total_importe + anterior_total_iva;
            //            double anterior_total_abonado = 0;
            //            double anterior_total_Saldo = 0;
            //            double anterior_prezagua = double.Parse(encabezado["prezagua"].ToString());
            //            double anterior_prezalcan = double.Parse(encabezado["prezalcan"].ToString());
            //            double anterior_prezsanea = double.Parse(encabezado["prezsanea"].ToString());
            //            double anterior_precagua = double.Parse(encabezado["precagua"].ToString());
            //            double anterior_precalcan = double.Parse(encabezado["precalcan"].ToString());
            //            double anterior_precsanea = double.Parse(encabezado["precsanea"].ToString());

            //            actual_pagua = double.Parse(encabezado["pagua"].ToString());
            //            actual_palcan = double.Parse(encabezado["palcan"].ToString());
            //            actual_psanea = double.Parse(encabezado["psanea"].ToString());
            //            //actual_recagua = double.Parse(encabezado["recagua"].ToString());
            //            //actual_recalcan = double.Parse(encabezado["recalcan"].ToString());
            //            //actual_recsanea = double.Parse(encabezado["recsanea"].ToString());
            //            actual_iva = double.Parse(encabezado["iva"].ToString());
            //            if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //            {
            //                actual_iva_agua = double.Parse(encabezado["pagua"].ToString()) * 0.16;
            //            }
            //            else
            //            {
            //                actual_iva_agua = 0;
            //            }
            //            actual_iva_alcan = double.Parse(encabezado["palcan"].ToString()) * 0.16;
            //            actual_iva_sanea = double.Parse(encabezado["psanea"].ToString()) * 0.16;
            //            if (actual_iva == 0)
            //            {
            //                //actual_iva = actual_iva_agua + actual_iva_alcan + actual_iva_sanea;
            //                actual_iva = 0;
            //            }
            //            else
            //            {
            //                ajuste_iva = actual_iva - (actual_iva_agua + actual_iva_alcan + actual_iva_sanea + anterior_iva_agua + anterior_iva_alcan + anterior_iva_sanea);
            //                //anterior_iva_sanea += ajuste_iva;
            //                actual_iva_sanea += ajuste_iva;
            //                if (actual_iva_sanea < 0)
            //                {
            //                    //anterior_iva_alcan += anterior_iva_sanea;
            //                    //anterior_iva_sanea = 0;
            //                    actual_iva_alcan += actual_iva_sanea;
            //                    actual_iva_sanea = 0;
            //                }
            //                if (actual_iva_alcan < 0)
            //                {
            //                    //anterior_iva_agua += anterior_iva_alcan;
            //                    //anterior_iva_alcan = 0;
            //                    actual_iva_agua += actual_iva_alcan;
            //                    actual_iva_alcan = 0;
            //                }
            //                actual_iva = actual_iva_agua + actual_iva_alcan + actual_iva_sanea;
            //            }


            //            actual_crbomb = double.Parse(encabezado["crbomb"].ToString());
            //            actual_ppagua = double.Parse(encabezado["ppagua"].ToString());
            //            actual_ppalcan = double.Parse(encabezado["ppalcan"].ToString());
            //            actual_ppsanea = double.Parse(encabezado["ppsanea"].ToString());
            //            //actual_precagua = double.Parse(encabezado["precagua"].ToString());
            //            //actual_precalcan = double.Parse(encabezado["precalcan"].ToString());
            //            //actual_precsanea = double.Parse(encabezado["precsanea"].ToString());
            //            actual_piva = double.Parse(encabezado["piva"].ToString());
            //            actual_pcrbomb = double.Parse(encabezado["pcrbomb"].ToString());

            //            //double ppagua = actual_ppagua;
            //            //double ppalcan = actual_ppalcan;
            //            //double ppsanea = actual_ppsanea;
            //            //prezagua = actual_prezagua;
            //            //prezalcan = actual_prezalcan;
            //            //prezsanea = actual_prezsanea;
            //            //precagua = actual_precagua;
            //            //precalcan = actual_precalcan;
            //            //precsanea = actual_precsanea;
            //            //pcrbomb = actual_pcrbomb;
            //            piva = actual_piva;

            //            // ************** Pagoas Factura Anterior ************ \\
            //            if (anterior_rezagua <= anterior_prezagua)
            //            {
            //                anterior_pago_agua = anterior_rezagua;
            //                anterior_total_abonado += anterior_rezagua;
            //                anterior_prezagua -= anterior_rezagua;
            //            }
            //            else
            //            {
            //                anterior_pago_agua = anterior_prezagua;
            //                anterior_total_abonado += anterior_prezagua;
            //                anterior_prezagua = 0;
            //            }
            //            if (anterior_rezalcan <= anterior_prezalcan)
            //            {
            //                anterior_pago_alcan = anterior_rezalcan;
            //                anterior_total_abonado += anterior_rezalcan;
            //                anterior_prezalcan -= anterior_rezalcan;
            //            }
            //            else
            //            {
            //                anterior_pago_alcan = anterior_prezalcan;
            //                anterior_total_abonado += anterior_prezalcan;
            //                anterior_prezalcan = 0;
            //            }
            //            if (anterior_rezsanea <= anterior_prezsanea)
            //            {
            //                anterior_pago_sanea = anterior_rezsanea;
            //                anterior_total_abonado += anterior_rezsanea;
            //                anterior_prezsanea -= anterior_rezsanea;
            //            }
            //            else
            //            {
            //                anterior_pago_sanea = anterior_prezsanea;
            //                anterior_total_abonado += anterior_prezsanea;
            //                anterior_prezsanea = 0;
            //            }
            //            if (anterior_recagua <= anterior_precagua)
            //            {
            //                anterior_pago_recagua = anterior_recagua;
            //                anterior_total_abonado += anterior_recagua;
            //                anterior_precagua -= anterior_recagua;
            //            }
            //            else
            //            {
            //                anterior_pago_recagua = anterior_precagua;
            //                anterior_total_abonado += anterior_precagua;
            //                anterior_precagua = 0;
            //            }
            //            if (anterior_recalcan <= anterior_precalcan)
            //            {
            //                anterior_pago_recalcan = anterior_recalcan;
            //                anterior_total_abonado += anterior_recalcan;
            //                anterior_precalcan -= anterior_recalcan;
            //            }
            //            else
            //            {
            //                anterior_pago_recalcan = anterior_precalcan;
            //                anterior_total_abonado += anterior_precalcan;
            //                anterior_precalcan = 0;
            //            }
            //            if (anterior_recsanea <= anterior_precsanea)
            //            {
            //                anterior_pago_recsanea = anterior_recsanea;
            //                anterior_total_abonado += anterior_recsanea;
            //                anterior_precsanea -= anterior_recsanea;
            //            }
            //            else
            //            {
            //                anterior_pago_recsanea = anterior_precsanea;
            //                anterior_total_abonado += anterior_precsanea;
            //                anterior_precsanea = 0;
            //            }
            //            if (anterior_iva_agua <= actual_piva)
            //            {
            //                anterior_pago_iva_agua = anterior_iva_agua;
            //                anterior_total_abonado += anterior_iva_agua;
            //                actual_piva -= anterior_iva_agua;
            //            }
            //            else
            //            {
            //                anterior_pago_iva_agua = actual_piva;
            //                anterior_total_abonado += actual_piva;
            //                actual_piva = 0;
            //            }
            //            if (anterior_iva_alcan <= actual_piva)
            //            {
            //                anterior_pago_iva_alcan = anterior_iva_alcan;
            //                anterior_total_abonado += anterior_iva_alcan;
            //                actual_piva -= anterior_iva_alcan;
            //            }
            //            else
            //            {
            //                anterior_pago_iva_alcan = actual_piva;
            //                anterior_total_abonado += actual_piva;
            //                actual_piva = 0;
            //            }
            //            if (anterior_iva_sanea <= actual_piva)
            //            {
            //                anterior_pago_iva_sanea = anterior_iva_sanea;
            //                anterior_total_abonado += anterior_iva_sanea;
            //                actual_piva -= anterior_iva_sanea;
            //            }
            //            else
            //            {
            //                anterior_pago_iva_sanea = actual_piva;
            //                anterior_total_abonado += actual_piva;
            //                actual_piva = 0;
            //            }
            //            anterior_total_abonado = Math.Round(anterior_total_abonado, 2);

            //            // ******************** Pagos Factura Actual **************** \\
            //            if (actual_pagua <= actual_ppagua)
            //            {
            //                actual_pago_agua = actual_pagua;
            //                actual_total_abonado += actual_pagua;
            //                actual_ppagua -= actual_pagua;
            //            }
            //            else
            //            {
            //                actual_pago_agua = actual_ppagua;
            //                actual_total_abonado += actual_ppagua;
            //                actual_ppagua = 0;
            //            }
            //            if (actual_palcan <= actual_ppalcan)
            //            {
            //                actual_pago_alcan = actual_palcan;
            //                actual_total_abonado += actual_palcan;
            //                actual_ppalcan -= actual_palcan;
            //            }
            //            else
            //            {
            //                actual_pago_alcan = actual_ppalcan;
            //                actual_total_abonado += actual_ppalcan;
            //                actual_ppalcan = 0;
            //            }
            //            if (actual_psanea <= actual_ppsanea)
            //            {
            //                actual_pago_sanea = actual_psanea;
            //                actual_total_abonado += actual_psanea;
            //                actual_ppsanea -= actual_psanea;
            //            }
            //            else
            //            {
            //                actual_pago_sanea = actual_ppsanea;
            //                actual_total_abonado += actual_ppsanea;
            //                actual_ppsanea = 0;
            //            }
            //            //if (actual_recagua <= precagua)
            //            //{
            //            //    actual_pago_recagua = actual_recagua;
            //            //    actual_total_abonado += actual_recagua;
            //            //    precagua -= actual_recagua;
            //            //}
            //            //else
            //            //{
            //            //    actual_pago_recagua = precagua;
            //            //    actual_total_abonado += precagua;
            //            //    precagua = 0;
            //            //}
            //            //if (actual_recalcan <= precalcan)
            //            //{
            //            //    actual_pago_recalcan = actual_recalcan;
            //            //    actual_total_abonado += actual_recalcan;
            //            //    precalcan -= actual_recalcan;
            //            //}
            //            //else
            //            //{
            //            //    actual_pago_recalcan = precalcan;
            //            //    actual_total_abonado += precalcan;
            //            //    precalcan = 0;
            //            //}
            //            //if (actual_recsanea <= precsanea)
            //            //{
            //            //    actual_pago_recsanea = actual_recsanea;
            //            //    actual_total_abonado += actual_recsanea;
            //            //    precsanea -= actual_recsanea;
            //            //}
            //            //else
            //            //{
            //            //    actual_pago_recsanea = precsanea;
            //            //    actual_total_abonado += precsanea;
            //            //    precsanea = 0;
            //            //}
            //            if (actual_iva_agua <= piva)
            //            {
            //                actual_pago_iva_agua = actual_iva_agua;
            //                actual_total_abonado += actual_iva_agua;
            //                piva -= actual_iva_agua;
            //            }
            //            else
            //            {
            //                actual_pago_iva_agua = piva;
            //                actual_total_abonado += piva;
            //                piva = 0;
            //            }
            //            if (actual_iva_alcan <= piva)
            //            {
            //                actual_pago_iva_alcan = actual_iva_alcan;
            //                actual_total_abonado += actual_iva_alcan;
            //                piva -= actual_iva_alcan;
            //            }
            //            else
            //            {
            //                actual_pago_iva_alcan = piva;
            //                actual_total_abonado += piva;
            //                piva = 0;
            //            }
            //            if (actual_iva_sanea <= piva)
            //            {
            //                actual_pago_iva_sanea = actual_iva_sanea;
            //                actual_total_abonado += actual_iva_sanea;
            //                piva -= actual_iva_sanea;
            //            }
            //            else
            //            {
            //                actual_pago_iva_sanea = piva;
            //                actual_total_abonado += piva;
            //                piva = 0;
            //            }
            //            if (actual_crbomb <= actual_pcrbomb)
            //            {
            //                actual_pago_crbomb = actual_crbomb;
            //                actual_total_abonado += actual_crbomb;
            //                actual_pcrbomb -= actual_crbomb;
            //            }
            //            else
            //            {
            //                actual_pago_crbomb = actual_pcrbomb;
            //                actual_total_abonado += actual_pcrbomb;
            //                actual_pcrbomb = 0;
            //            }
            //            actual_total_abonado = Math.Round(actual_total_abonado, 2);


            //            // -- Aqui inicia el llenado de las columnas "cuota_base", "cuota_consumo", "precio_m3" -- \\
                        
            //            DataRow Dr;

            //            // ************** Factura Anterior ************** \\
            //            if (float.Parse(encabezado["rezagua"].ToString()) != 0 || float.Parse(encabezado["rezalcan"].ToString()) != 0 || float.Parse(encabezado["rezsanea"].ToString()) != 0)
            //            {
            //                Dr = Dt_Datos.NewRow();
            //                automatic_id = "0000000000" + (no_Reg + x).ToString();
            //                automatic_id = automatic_id.Substring(automatic_id.Length - 10, 10);
            //                x++;

            //                Dr["no_factura"] = automatic_id;
            //                Dr["Codigo_Barras"] = automatic_id + "F";
            //                Dr["no_cuenta"] = encabezado["cuenta"];
            //                Dr["no_recibo"] = encabezado["foliorecib"];
            //                Dr["region_id"] = encabezado["region_ID"];
            //                Dr["predio_id"] = encabezado["predio_ID"];
            //                Dr["usuario_id"] = encabezado["usuario_ID"];
            //                Dr["medidor_id"] = encabezado["nummedidor"];
            //                Dr["tarifa_id"] = encabezado["tarifa_ID"];
            //                Dr["lec_anterior"] = encabezado["lecanterior"];
            //                Dr["lec_actual"] = encabezado["lecactual"];
            //                Dr["consumo"] = encabezado["consumo"];
            //                Dr["fac_Cuota_Base"] = 0;
            //                Dr["fac_Cuata_Consumo"] = 0;
            //                Dr["fac_Precio_M3"] = 0;
            //                Dr["fecha_inicio"] = encabezado["fecha_inicio"];
            //                Dr["fecha_termino"] = encabezado["fecha_termino"];
            //                Dr["fecha_limite"] = encabezado["vencimient"];
            //                Dr["fecha_emision"] = encabezado["facturacion"];
            //                Dr["periodo"] = encabezado["periodo"];
            //                if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //                {
            //                    Dr["tasa_iva"] = "16";
            //                }
            //                else
            //                {
            //                    Dr["tasa_iva"] = "0.0";
            //                }
            //                Dr["iva_agua"] = Math.Round(anterior_iva_agua, 2);
            //                Dr["iva_alcan"] = Math.Round(anterior_iva_alcan, 2);
            //                Dr["iva_sanea"] = Math.Round(anterior_iva_sanea, 2);

            //                Dr["total_importe"] = Math.Round(anterior_total_importe, 2);
            //                Dr["total_iva"] = Math.Round(anterior_total_iva, 2);

            //                anterior_total_pagado = Math.Round(anterior_total_pagado, 2);
            //                Dr["total_pagar"] = Math.Round(anterior_total_pagado, 2);
            //                Dr["total_abono"] = Math.Round(anterior_total_abonado, 2);
            //                anterior_total_Saldo = anterior_total_pagado - anterior_total_abonado;
            //                anterior_total_Saldo = Math.Round(anterior_total_Saldo, 2);
            //                Dr["fac_Saldo"] = anterior_total_Saldo;
            //                if (anterior_total_Saldo == 0)
            //                {
            //                    Dr["estado"] = "PAGADO";
            //                }
            //                else
            //                {
            //                    Dr["estado"] = "PENDIENTE";
            //                }
            //                if (encabezado["bimestre"].ToString() == "1")
            //                {
            //                    Dr["anio"] = int.Parse(encabezado["anio"].ToString()) + 11;
            //                    Dr["bimestre"] = int.Parse(encabezado["bimestre"].ToString()) - 1;
            //                }
            //                else
            //                {
            //                    Dr["anio"] = encabezado["anio"];
            //                    Dr["bimestre"] = int.Parse(encabezado["bimestre"].ToString()) - 1;
            //                }
            //                Dr["rpu"] = encabezado["rpu"];
            //                Dr["pagua"] = encabezado["rezagua"];
            //                Dr["palcan"] = encabezado["rezalcan"];
            //                Dr["psanea"] = encabezado["rezsanea"];
            //                Dr["recagua"] = Math.Round(anterior_recagua, 2);
            //                Dr["recalcan"] = Math.Round(anterior_recalcan, 2);
            //                Dr["recsanea"] = Math.Round(anterior_recsanea, 2);
            //                Dr["crbomb"] = 0;
            //                Dr["abono_agua"] = Math.Round(anterior_pago_agua, 2);
            //                Dr["abono_alcan"] = Math.Round(anterior_pago_alcan, 2);
            //                Dr["abonosanea"] = Math.Round(anterior_pago_sanea, 2);
            //                Dr["abono_recagua"] = Math.Round(anterior_pago_recagua, 2);
            //                Dr["abono_recalcan"] = Math.Round(anterior_pago_recalcan, 2);
            //                Dr["abono_recsanea"] = Math.Round(anterior_pago_recsanea, 2);
            //                Dr["abono_crbomb"] = 0;
            //                Dr["abono_IVA_agua"] = Math.Round(anterior_pago_iva_agua, 2);
            //                Dr["abono_IVA_alcan"] = Math.Round(anterior_pago_iva_alcan, 2);
            //                Dr["abono_IVA_sanea"] = Math.Round(anterior_pago_iva_sanea, 2);
            //                Dr["anticipo"] = 0;
            //                Dr["Fecha_Pago"] = encabezado["fechapago"];
            //                Dt_Datos.Rows.Add(Dr);
            //            }


            //            // ******************** Factura Actual ***************** \\
            //            Dr = Dt_Datos.NewRow();
            //            automatic_id = "0000000000" + (no_Reg + x).ToString();
            //            automatic_id = automatic_id.Substring(automatic_id.Length - 10, 10);
            //            x++;

            //            Dr["no_factura"] = automatic_id;
            //            Dr["Codigo_Barras"] = automatic_id + "F";
            //            Dr["no_cuenta"] = encabezado["cuenta"].ToString();
            //            Dr["no_recibo"] = encabezado["foliorecib"];
            //            Dr["region_id"] = encabezado["region_ID"];
            //            Dr["predio_id"] = encabezado["predio_ID"];
            //            Dr["usuario_id"] = encabezado["usuario_ID"];
            //            Dr["medidor_id"] = encabezado["nummedidor"];
            //            Dr["tarifa_id"] = encabezado["tarifa_ID"];
            //            Dr["lec_anterior"] = encabezado["lecanterior"];
            //            Dr["lec_actual"] = encabezado["lecactual"];
            //            Dr["consumo"] = encabezado["consumo"];
            //            Dr["fac_Cuota_Base"] = cuota_base;
            //            Dr["fac_Cuata_Consumo"] = cuota_consumo;
            //            Dr["fac_Precio_M3"] = precio_m3;
            //            Dr["fecha_inicio"] = encabezado["fecha_inicio"];
            //            Dr["fecha_termino"] = encabezado["fecha_termino"];
            //            Dr["fecha_limite"] = encabezado["vencimient"];
            //            Dr["fecha_emision"] = encabezado["facturacion"];
            //            Dr["periodo"] = encabezado["periodo"];
            //            if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //            {
            //                Dr["tasa_iva"] = "16";
            //            }
            //            else
            //            {
            //                Dr["tasa_iva"] = "0.0";
            //            }
            //            Dr["iva_agua"] = Math.Round(actual_iva_agua, 2);
            //            Dr["iva_alcan"] = Math.Round(actual_iva_alcan, 2);
            //            Dr["iva_sanea"] = Math.Round(actual_iva_sanea, 2);

            //            Dr["total_importe"] = Math.Round(double.Parse(encabezado["pagua"].ToString()) + double.Parse(encabezado["palcan"].ToString()) + double.Parse(encabezado["psanea"].ToString()) + actual_recagua + actual_recalcan + actual_recsanea + actual_crbomb, 2);
            //            Dr["total_iva"] = Math.Round(actual_iva, 2);
            //            double Total_Pagado = float.Parse(encabezado["pagua"].ToString()) + float.Parse(encabezado["palcan"].ToString()) + float.Parse(encabezado["psanea"].ToString()) + actual_recagua + actual_recalcan + actual_recsanea + actual_crbomb + actual_iva;

            //            Total_Pagado = Math.Round(Total_Pagado, 2);
            //            Dr["total_pagar"] = Total_Pagado;
            //            Dr["total_abono"] = Math.Round(actual_total_abonado, 2);
            //            double Saldo = float.Parse(encabezado["pagua"].ToString()) + float.Parse(encabezado["palcan"].ToString()) + float.Parse(encabezado["psanea"].ToString()) + actual_recagua + actual_recalcan + actual_recsanea + actual_crbomb + actual_iva - actual_total_abonado;
            //            Saldo = Math.Round(Saldo, 2);
            //            Dr["fac_Saldo"] = Saldo;
            //            if (Saldo == 0)
            //            {
            //                Dr["estado"] = "PAGADO";
            //            }
            //            else
            //            {
            //                Dr["estado"] = "PENDIENTE";
            //            }
            //            Dr["anio"] = encabezado["anio"];
            //            Dr["bimestre"] = encabezado["bimestre"];
            //            Dr["rpu"] = encabezado["rpu"];
            //            Dr["pagua"] = encabezado["pagua"];
            //            Dr["palcan"] = encabezado["palcan"];
            //            Dr["psanea"] = encabezado["psanea"];
            //            Dr["recagua"] = 0;
            //            Dr["recalcan"] = 0;
            //            Dr["recsanea"] = 0;
            //            Dr["crbomb"] = actual_crbomb;

            //            Dr["abono_agua"] = Math.Round(actual_pago_agua, 2);
            //            Dr["abono_alcan"] = Math.Round(actual_pago_alcan, 2);
            //            Dr["abonosanea"] = Math.Round(actual_pago_sanea, 2);
            //            Dr["abono_recagua"] = 0;
            //            Dr["abono_recalcan"] = 0;
            //            Dr["abono_recsanea"] = 0;
            //            Dr["abono_crbomb"] = actual_pago_crbomb;
            //            Dr["abono_IVA_agua"] = Math.Round(actual_pago_iva_agua, 2);
            //            Dr["abono_IVA_alcan"] = Math.Round(actual_pago_iva_alcan, 2);
            //            Dr["abono_IVA_sanea"] = Math.Round(actual_pago_iva_sanea, 2);
            //            Dr["anticipo"] = Math.Round(actual_ppagua + actual_ppalcan + actual_ppsanea + precagua + precalcan + precsanea + piva, 2);
            //            Dr["Fecha_Pago"] = encabezado["fechapago"];

            //            Dt_Datos.Rows.Add(Dr);

            //            ajuste_iva = 0;

            //            pBar1.PerformStep();

            //            #endregion
            //        }
            //        else
            //        {
            //            double Total_Pagado, Saldo;
            //            his_foliorecib = new string[dt_historico.Rows.Count];
            //            his_lecactual = new string[dt_historico.Rows.Count];
            //            his_lecanterior = new string[dt_historico.Rows.Count];
            //            his_consumo = new string[dt_historico.Rows.Count];
            //            his_fecha_inicio = new string[dt_historico.Rows.Count];
            //            his_fecha_termino = new string[dt_historico.Rows.Count];
            //            his_vencimient = new string[dt_historico.Rows.Count];
            //            his_facturacion = new string[dt_historico.Rows.Count];
            //            his_periodo = new string[dt_historico.Rows.Count];
            //            his_pagua = new string[dt_historico.Rows.Count];
            //            his_palcan = new string[dt_historico.Rows.Count];
            //            his_psanea = new string[dt_historico.Rows.Count];
            //            his_rezagua = new string[dt_historico.Rows.Count];
            //            his_rezalcan = new string[dt_historico.Rows.Count];
            //            his_rezsanea = new string[dt_historico.Rows.Count];
            //            his_recagua = new string[dt_historico.Rows.Count];
            //            his_recalcan = new string[dt_historico.Rows.Count];
            //            his_recsanea = new string[dt_historico.Rows.Count];
            //            his_crbomb = new string[dt_historico.Rows.Count];
            //            his_iva = new string[dt_historico.Rows.Count];
            //            his_importepago = new string[dt_historico.Rows.Count];
            //            his_ppagua = new string[dt_historico.Rows.Count];
            //            his_ppalcan = new string[dt_historico.Rows.Count];
            //            his_ppsanea = new string[dt_historico.Rows.Count];
            //            his_prezagua = new string[dt_historico.Rows.Count];
            //            his_prezalcan = new string[dt_historico.Rows.Count];
            //            his_prezsanea = new string[dt_historico.Rows.Count];
            //            his_precagua = new string[dt_historico.Rows.Count];
            //            his_precalcan = new string[dt_historico.Rows.Count];
            //            his_precsanea = new string[dt_historico.Rows.Count];
            //            his_pcrbomb = new string[dt_historico.Rows.Count];
            //            his_crbomb_aux = new string[dt_historico.Rows.Count];
            //            his_piva = new string[dt_historico.Rows.Count];
            //            his_estado = new string[dt_historico.Rows.Count];
            //            his_anio = new string[dt_historico.Rows.Count];
            //            his_bimestre = new string[dt_historico.Rows.Count];
            //            his_rpu = new string[dt_historico.Rows.Count];
            //            his_fechapago = new string[dt_historico.Rows.Count];

            //            his_tasa_iva = new float[dt_historico.Rows.Count];
            //            his_cuota_base = new float[dt_historico.Rows.Count];
            //            his_cuota_consumo = new float[dt_historico.Rows.Count];
            //            his_precio_m3 = new float[dt_historico.Rows.Count];
            //            his_auto_id = new string[dt_historico.Rows.Count];
            //            fac_No_Factura = new string[dt_historico.Rows.Count];
            //            fac_No_Cuenta = new string[dt_historico.Rows.Count];
            //            fac_No_Recibo = new string[dt_historico.Rows.Count];
            //            fac_Region_ID = new string[dt_historico.Rows.Count];
            //            fac_Predio_ID = new string[dt_historico.Rows.Count];
            //            fac_Usuario_ID = new string[dt_historico.Rows.Count];
            //            fac_Medidor_ID = new string[dt_historico.Rows.Count];
            //            fac_Tarifa_ID = new string[dt_historico.Rows.Count];
            //            fac_Lectura_Anterior = new string[dt_historico.Rows.Count];
            //            fac_Lectura_Actual = new string[dt_historico.Rows.Count];
            //            fac_Consumo = new string[dt_historico.Rows.Count];
            //            fac_Cuota_Base = new string[dt_historico.Rows.Count];
            //            fac_Cuata_Consumo = new string[dt_historico.Rows.Count];
            //            fac_Precio_M3 = new string[dt_historico.Rows.Count];
            //            fac_Fecha_Inicio = new string[dt_historico.Rows.Count];
            //            fac_Fecha_Termino = new string[dt_historico.Rows.Count];
            //            fac_Fecha_Limite = new string[dt_historico.Rows.Count];
            //            fac_Fecha_Emicio = new string[dt_historico.Rows.Count];
            //            fac_Periodo = new string[dt_historico.Rows.Count];
            //            fac_Tasa_IVA = new string[dt_historico.Rows.Count];
            //            fac_Total_Importe = new double[dt_historico.Rows.Count];
            //            fac_Total_IVA = new double[dt_historico.Rows.Count];
            //            fac_Total_Pagado = new double[dt_historico.Rows.Count];
            //            fac_Total_Abono = new double[dt_historico.Rows.Count];
            //            fac_Saldo = new double[dt_historico.Rows.Count];
            //            fac_Estado = new string[dt_historico.Rows.Count];
            //            fac_Usuario_Creo = new string[dt_historico.Rows.Count];
            //            fac_Fecha_Creo = new string[dt_historico.Rows.Count];
            //            fac_Anio = new string[dt_historico.Rows.Count];
            //            fac_Bimestre = new string[dt_historico.Rows.Count];
            //            fac_RPU = new string[dt_historico.Rows.Count];

            //            pago_rezagua = new double[dt_historico.Rows.Count];
            //            pago_rezalcan = new double[dt_historico.Rows.Count];
            //            pago_rezsanea = new double[dt_historico.Rows.Count];
            //            pago_recagua = new double[dt_historico.Rows.Count];
            //            pago_recalcan = new double[dt_historico.Rows.Count];
            //            pago_recsanea = new double[dt_historico.Rows.Count];
            //            pago_crbomb = new double[dt_historico.Rows.Count];
            //            pago_iva = new double[dt_historico.Rows.Count];
            //            aux_pagua = new double[dt_historico.Rows.Count];
            //            aux_palcan = new double[dt_historico.Rows.Count];
            //            aux_psanea = new double[dt_historico.Rows.Count];
            //            aux_recagua = new double[dt_historico.Rows.Count];
            //            aux_recalcan = new double[dt_historico.Rows.Count];
            //            aux_recsanea = new double[dt_historico.Rows.Count];
            //            aux_crbomb = new double[dt_historico.Rows.Count];

            //            aux_iva_agua = new double[dt_historico.Rows.Count];
            //            aux_iva_alcan = new double[dt_historico.Rows.Count];
            //            aux_iva_sanea = new double[dt_historico.Rows.Count];
            //            pago_iva_agua = new double[dt_historico.Rows.Count];
            //            pago_iva_alcan = new double[dt_historico.Rows.Count];
            //            pago_iva_sanea = new double[dt_historico.Rows.Count];
            //            his_iva_agua = new double[dt_historico.Rows.Count];
            //            his_iva_alcan = new double[dt_historico.Rows.Count];
            //            his_iva_sanea = new double[dt_historico.Rows.Count];
            //            his_recagua_aux = new double[dt_historico.Rows.Count];
            //            his_recalcan_aux = new double[dt_historico.Rows.Count];
            //            his_recsanea_aux = new double[dt_historico.Rows.Count];

            //            int indice = 0;
            //            foreach (DataRow Regitrso_Historico in dt_historico.Rows)
            //            {
            //                his_foliorecib[indice] = Regitrso_Historico["foliorecib"].ToString();

            //                his_lecactual[indice] = Regitrso_Historico["lecactual"].ToString();
            //                his_lecanterior[indice] = Regitrso_Historico["lecanterior"].ToString();
            //                his_consumo[indice] = Regitrso_Historico["consumo"].ToString();
            //                his_fecha_inicio[indice] = Regitrso_Historico["fecha_inicio"].ToString();
            //                his_fecha_termino[indice] = Regitrso_Historico["fecha_termino"].ToString();
            //                his_vencimient[indice] = Regitrso_Historico["vencimient"].ToString();
            //                his_facturacion[indice] = Regitrso_Historico["facturacion"].ToString();
            //                his_periodo[indice] = Regitrso_Historico["periodo"].ToString();
            //                his_pagua[indice] = Regitrso_Historico["pagua"].ToString();
            //                his_palcan[indice] = Regitrso_Historico["palcan"].ToString();
            //                his_psanea[indice] = Regitrso_Historico["psanea"].ToString();
            //                his_rezagua[indice] = Regitrso_Historico["rezagua"].ToString();
            //                his_rezalcan[indice] = Regitrso_Historico["rezalcan"].ToString();
            //                his_rezsanea[indice] = Regitrso_Historico["rezsanea"].ToString();
            //                his_recagua[indice] = Regitrso_Historico["recagua"].ToString();
            //                his_recalcan[indice] = Regitrso_Historico["recalcan"].ToString();
            //                his_recsanea[indice] = Regitrso_Historico["recsanea"].ToString();
            //                his_crbomb[indice] = Regitrso_Historico["crbomb"].ToString();
            //                his_iva[indice] = Regitrso_Historico["iva"].ToString();
            //                his_importepago[indice] = Regitrso_Historico["importepago"].ToString();
            //                his_ppagua[indice] = Regitrso_Historico["ppagua"].ToString();
            //                his_ppalcan[indice] = Regitrso_Historico["ppalcan"].ToString();
            //                his_ppsanea[indice] = Regitrso_Historico["ppsanea"].ToString();
            //                his_prezagua[indice] = Regitrso_Historico["prezagua"].ToString();
            //                his_prezalcan[indice] = Regitrso_Historico["prezalcan"].ToString();
            //                his_prezsanea[indice] = Regitrso_Historico["prezsanea"].ToString();
            //                his_precagua[indice] = Regitrso_Historico["precagua"].ToString();
            //                his_precalcan[indice] = Regitrso_Historico["precalcan"].ToString();
            //                his_precsanea[indice] = Regitrso_Historico["precsanea"].ToString();
            //                his_pcrbomb[indice] = Regitrso_Historico["pcrbomb"].ToString();
            //                his_piva[indice] = Regitrso_Historico["piva"].ToString();
            //                his_estado[indice] = Regitrso_Historico["estado"].ToString();
            //                his_anio[indice] = Regitrso_Historico["anio"].ToString();
            //                his_bimestre[indice] = Regitrso_Historico["bimestre"].ToString();
            //                his_fechapago[indice] = Regitrso_Historico["fechapago"].ToString();

            //                /////////////////////////////////////////////////
            //                aux_pagua[indice] = double.Parse(his_pagua[indice]);
            //                aux_palcan[indice] = double.Parse(his_palcan[indice]);
            //                aux_psanea[indice] = double.Parse(his_psanea[indice]);

            //                indice++;

            //            }


            //            int auxiliar_indice = indice - 1;
            //            for (; auxiliar_indice >= 0; auxiliar_indice--)
            //            {
            //                switch (his_estado[auxiliar_indice].Trim())
            //                {
            //                    case "REZAGADO":
            //                        Estatus = his_estado[auxiliar_indice].Trim();
            //                        if (auxiliar_indice > 0)
            //                        {
            //                            automatic_id = "0000000000" + (no_Reg + x).ToString();
            //                            fac_No_Factura[auxiliar_indice] = automatic_id.Substring(automatic_id.Length - 10, 10);
            //                            x++;
            //                            fac_No_Cuenta[auxiliar_indice] = encabezado["cuenta"].ToString();
            //                            fac_No_Recibo[auxiliar_indice] = his_foliorecib[auxiliar_indice];
            //                            fac_Region_ID[auxiliar_indice] = encabezado["Region_ID"].ToString();
            //                            fac_Predio_ID[auxiliar_indice] = encabezado["Predio_ID"].ToString();
            //                            fac_Usuario_ID[auxiliar_indice] = encabezado["Usuario_ID"].ToString();
            //                            fac_Medidor_ID[auxiliar_indice] = encabezado["nummedidor"].ToString();
            //                            fac_Tarifa_ID[auxiliar_indice] = encabezado["Tarifa_ID"].ToString();
            //                            fac_Lectura_Anterior[auxiliar_indice] = his_lecanterior[auxiliar_indice];
            //                            fac_Lectura_Actual[auxiliar_indice] = his_lecactual[auxiliar_indice];
            //                            fac_Consumo[auxiliar_indice] = his_consumo[auxiliar_indice]; ;
            //                            fac_Fecha_Inicio[auxiliar_indice] = his_fecha_inicio[auxiliar_indice];
            //                            fac_Fecha_Termino[auxiliar_indice] = his_fecha_termino[auxiliar_indice];
            //                            fac_Fecha_Limite[auxiliar_indice] = his_vencimient[auxiliar_indice];
            //                            fac_Fecha_Emicio[auxiliar_indice] = his_facturacion[auxiliar_indice];
            //                            fac_Periodo[auxiliar_indice] = his_periodo[auxiliar_indice];

            //                            if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //                                fac_Tasa_IVA[auxiliar_indice] = "16";
            //                            else
            //                                fac_Tasa_IVA[auxiliar_indice] = "0.0";
            //                            fac_Estado[auxiliar_indice] = his_estado[auxiliar_indice];
            //                            fac_Anio[auxiliar_indice] = his_anio[auxiliar_indice];
            //                            fac_Bimestre[auxiliar_indice] = his_bimestre[auxiliar_indice];
            //                            fac_RPU[auxiliar_indice] = encabezado["rpu"].ToString();

            //                            if (sinp)
            //                            {
            //                                his_recagua_aux[auxiliar_indice] = (double.Parse(his_recagua[auxiliar_indice - 1]) - double.Parse(his_recagua[auxiliar_indice])) + double.Parse(his_recagua[auxiliar_indice]);
            //                                his_recalcan_aux[auxiliar_indice] = (double.Parse(his_recalcan[auxiliar_indice - 1]) - double.Parse(his_recalcan[auxiliar_indice])) + double.Parse(his_recalcan[auxiliar_indice]);
            //                                his_recsanea_aux[auxiliar_indice] = (double.Parse(his_recsanea[auxiliar_indice - 1]) - double.Parse(his_recsanea[auxiliar_indice])) + double.Parse(his_recsanea[auxiliar_indice]);
            //                                his_pagua[auxiliar_indice] = (double.Parse(his_pagua[auxiliar_indice]) + double.Parse(his_rezagua[auxiliar_indice])).ToString();
            //                                his_palcan[auxiliar_indice] = (double.Parse(his_palcan[auxiliar_indice]) + double.Parse(his_rezalcan[auxiliar_indice])).ToString();
            //                                his_psanea[auxiliar_indice] = (double.Parse(his_psanea[auxiliar_indice]) + double.Parse(his_rezsanea[auxiliar_indice])).ToString();
            //                                sinp = false;
            //                            }
            //                            else
            //                            {
            //                                his_recagua_aux[auxiliar_indice] = (double.Parse(his_recagua[auxiliar_indice - 1]) - double.Parse(his_recagua[auxiliar_indice]));
            //                                his_recalcan_aux[auxiliar_indice] = (double.Parse(his_recalcan[auxiliar_indice - 1]) - double.Parse(his_recalcan[auxiliar_indice]));
            //                                his_recsanea_aux[auxiliar_indice] = (double.Parse(his_recsanea[auxiliar_indice - 1]) - double.Parse(his_recsanea[auxiliar_indice]));
            //                            }

            //                            if (his_recagua_aux[auxiliar_indice] < 0)
            //                            {
            //                                for (int a = auxiliar_indice - 1; a < his_recagua_aux.Length - 1; )
            //                                {
            //                                    if (double.Parse(his_precagua[a]) == 0)
            //                                    {
            //                                        a++;
            //                                    }
            //                                    else
            //                                    {
            //                                        his_recagua_aux[auxiliar_indice] += double.Parse(his_precagua[a]);
            //                                        break;
            //                                    }
            //                                }
            //                            }
            //                            if (his_recalcan_aux[auxiliar_indice] < 0)
            //                            {
            //                                for (int a = auxiliar_indice; a < his_recalcan_aux.Length - 1; )
            //                                {
            //                                    if (double.Parse(his_precalcan[a]) == 0)
            //                                    {
            //                                        a++;
            //                                    }
            //                                    else
            //                                    {
            //                                        his_recalcan_aux[auxiliar_indice] += double.Parse(his_precalcan[a]);
            //                                        break;
            //                                    }
            //                                }
            //                            }
            //                            if (his_recsanea_aux[auxiliar_indice] < 0)
            //                            {
            //                                for (int a = auxiliar_indice; a < his_recsanea_aux.Length - 1; )
            //                                {
            //                                    if (double.Parse(his_precsanea[a]) == 0)
            //                                    {
            //                                        a++;
            //                                    }
            //                                    else
            //                                    {
            //                                        his_recsanea_aux[auxiliar_indice] += double.Parse(his_precsanea[a]);
            //                                        break;
            //                                    }
            //                                }
            //                            }

            //                            aux_recagua[auxiliar_indice] = his_recagua_aux[auxiliar_indice];
            //                            aux_recalcan[auxiliar_indice] = his_recalcan_aux[auxiliar_indice];
            //                            aux_recsanea[auxiliar_indice] = his_recsanea_aux[auxiliar_indice];

            //                            his_crbomb_aux[auxiliar_indice] = (double.Parse(his_crbomb[auxiliar_indice]) - crbomb).ToString();
            //                            aux_crbomb[auxiliar_indice] = double.Parse(his_crbomb_aux[auxiliar_indice]);
            //                            crbomb = float.Parse(his_crbomb[auxiliar_indice]);

            //                            fac_Total_Importe[auxiliar_indice] = double.Parse(his_pagua[auxiliar_indice]) + double.Parse(his_palcan[auxiliar_indice]) + double.Parse(his_psanea[auxiliar_indice]) + double.Parse(his_crbomb_aux[auxiliar_indice]) + his_recagua_aux[auxiliar_indice] + his_recalcan_aux[auxiliar_indice] + his_recsanea_aux[auxiliar_indice];


            //                            // ----------------------------------- CALCULAR IVA ------------------------------------

            //                            if (float.Parse(his_iva[auxiliar_indice]) == 0) //no tiene datos en recivos
            //                            {
            //                                aux_iva_agua[auxiliar_indice] = 0;
            //                                aux_iva_alcan[auxiliar_indice] = 0;
            //                                aux_iva_sanea[auxiliar_indice] = 0;

            //                            }
            //                            else
            //                            {
            //                                if (bandera)
            //                                {
            //                                    aux_iva_agua[auxiliar_indice] = 0;
            //                                    aux_iva_alcan[auxiliar_indice] = double.Parse(his_iva[auxiliar_indice]) / 2;
            //                                    aux_iva_sanea[auxiliar_indice] = double.Parse(his_iva[auxiliar_indice]) / 2;
            //                                    iva_acumulado = double.Parse(his_iva[auxiliar_indice]);
            //                                    bandera = false;
            //                                }
            //                                else
            //                                {
            //                                    if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //                                    {
            //                                        aux_iva_agua[auxiliar_indice] = double.Parse(his_pagua[auxiliar_indice]) * 0.16;
            //                                        aux_iva_alcan[auxiliar_indice] = double.Parse(his_palcan[auxiliar_indice]) * 0.16;
            //                                        aux_iva_sanea[auxiliar_indice] = double.Parse(his_psanea[auxiliar_indice]) * 0.16;
            //                                    }
            //                                    else
            //                                    {
            //                                        aux_iva_agua[auxiliar_indice] = 0;
            //                                        aux_iva_alcan[auxiliar_indice] = double.Parse(his_palcan[auxiliar_indice]) * 0.16;
            //                                        aux_iva_sanea[auxiliar_indice] = double.Parse(his_psanea[auxiliar_indice]) * 0.16;
            //                                    }
            //                                    if (double.Parse(his_iva[auxiliar_indice]) <= iva_acumulado)
            //                                    {
            //                                        for (int a = auxiliar_indice + 1; a < his_piva.Length; )
            //                                        {
            //                                            if (float.Parse(his_piva[a]) == 0)
            //                                            {
            //                                                a++;
            //                                            }
            //                                            else
            //                                            {
            //                                                ajuste_iva = double.Parse(his_piva[a]);
            //                                                break;
            //                                            }
            //                                        }
            //                                        ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - iva_acumulado + ajuste_iva;
            //                                    }
            //                                    else
            //                                    {
            //                                        ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - iva_acumulado;

            //                                    }
            //                                    ajuste_iva = ajuste_iva - ((aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice]));
            //                                    aux_iva_sanea[auxiliar_indice] += ajuste_iva;
            //                                    if (aux_iva_sanea[auxiliar_indice] < 0)
            //                                    {
            //                                        ajuste_iva = aux_iva_sanea[auxiliar_indice];
            //                                        aux_iva_sanea[auxiliar_indice] = 0;
            //                                        aux_iva_alcan[auxiliar_indice] += ajuste_iva;

            //                                    }
            //                                    if (aux_iva_alcan[auxiliar_indice] < 0)
            //                                    {
            //                                        ajuste_iva = aux_iva_alcan[auxiliar_indice];
            //                                        aux_iva_alcan[auxiliar_indice] = 0;
            //                                        aux_iva_agua[auxiliar_indice] += ajuste_iva;
            //                                    }
            //                                    if (aux_iva_agua[auxiliar_indice] < 0)
            //                                    {
            //                                        aux_iva_agua[auxiliar_indice] = 0;
            //                                    }
            //                                    iva_acumulado = double.Parse(his_iva[auxiliar_indice]);

            //                                }

            //                            }











            //                            //if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //                            //    {
            //                            //        aux_iva_agua[auxiliar_indice] = double.Parse(his_pagua[auxiliar_indice]) * 0.16;
            //                            //        aux_iva_alcan[auxiliar_indice] = double.Parse(his_palcan[auxiliar_indice]) * 0.16;
            //                            //        aux_iva_sanea[auxiliar_indice] = double.Parse(his_psanea[auxiliar_indice]) * 0.16;
            //                            //    }
            //                            //    else
            //                            //    {
            //                            //        aux_iva_agua[auxiliar_indice] = 0;
            //                            //        aux_iva_alcan[auxiliar_indice] = double.Parse(his_palcan[auxiliar_indice]) * 0.16;
            //                            //        aux_iva_sanea[auxiliar_indice] = double.Parse(his_psanea[auxiliar_indice]) * 0.16;
            //                            //    }


            //                            //    if (float.Parse(his_iva[auxiliar_indice]) == 0) //no tiene datos en recivos 
            //                            //    {
            //                            //        iva_acumulado += aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice];
            //                            //        bandera = false;
            //                            //    }
            //                            //    else
            //                            //    {

            //                            //        if (iva_acumulado != 0) // tiene Historial pero aun 
            //                            //        {
            //                            //            bandera = false;

            //                            //            ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - iva_acumulado;
            //                            //            ajuste_iva = ajuste_iva - ((aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice]));
            //                            //            aux_iva_sanea[auxiliar_indice] += ajuste_iva;
            //                            //            if (aux_iva_sanea[auxiliar_indice] < 0)
            //                            //            {
            //                            //                ajuste_iva = aux_iva_sanea[auxiliar_indice];
            //                            //                aux_iva_sanea[auxiliar_indice] = 0;
            //                            //                aux_iva_alcan[auxiliar_indice] += ajuste_iva;

            //                            //            }
            //                            //            if (aux_iva_alcan[auxiliar_indice] < 0)
            //                            //            {
            //                            //                ajuste_iva = aux_iva_alcan[auxiliar_indice];
            //                            //                aux_iva_alcan[auxiliar_indice] = 0;
            //                            //                aux_iva_agua[auxiliar_indice] += ajuste_iva;
            //                            //            }
            //                            //            if (aux_iva_agua[auxiliar_indice] < 0)
            //                            //            {
            //                            //                aux_iva_agua[auxiliar_indice] = 0;
            //                            //            }
            //                            //            iva_acumulado = double.Parse(his_iva[auxiliar_indice]);
            //                            //        }
            //                            //        else
            //                            //        {
            //                            //            if (bandera)
            //                            //            {
            //                            //                bandera = false;
            //                            //                ajuste_iva = double.Parse(his_iva[auxiliar_indice]);
            //                            //                ajuste_iva = ajuste_iva - ((aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice]));
            //                            //                aux_iva_sanea[auxiliar_indice] += ajuste_iva;
            //                            //                if (aux_iva_sanea[auxiliar_indice] < 0)
            //                            //                {
            //                            //                    ajuste_iva = aux_iva_sanea[auxiliar_indice];
            //                            //                    aux_iva_sanea[auxiliar_indice] = 0;
            //                            //                    aux_iva_alcan[auxiliar_indice] += ajuste_iva;

            //                            //                }
            //                            //                if (aux_iva_alcan[auxiliar_indice] < 0)
            //                            //                {
            //                            //                    ajuste_iva = aux_iva_alcan[auxiliar_indice];
            //                            //                    aux_iva_alcan[auxiliar_indice] = 0;
            //                            //                    aux_iva_agua[auxiliar_indice] += ajuste_iva;
            //                            //                }
            //                            //                if (aux_iva_agua[auxiliar_indice] < 0)
            //                            //                {
            //                            //                    aux_iva_agua[auxiliar_indice] = 0;
            //                            //                }
            //                            //                iva_acumulado = double.Parse(his_iva[auxiliar_indice]);

            //                            //            }

            //                            //            else
            //                            //            {
            //                            //                for (int a = auxiliar_indice + 1; a < his_piva.Length; )
            //                            //                {
            //                            //                    if (float.Parse(his_piva[a]) == 0)
            //                            //                    {
            //                            //                        a++;
            //                            //                    }
            //                            //                    else
            //                            //                    {
            //                            //                        ajuste_iva = double.Parse(his_piva[a]);
            //                            //                        break;
            //                            //                    }
            //                            //                }
            //                            //                if (double.Parse(his_iva[auxiliar_indice])>=double.Parse(his_iva[auxiliar_indice + 1])){
            //                            //                ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - double.Parse(his_iva[auxiliar_indice + 1]) ;
            //                            //                }else{
            //                            //                ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - double.Parse(his_iva[auxiliar_indice + 1]) + ajuste_iva;
            //                            //                }
            //                            //                ajuste_iva = ajuste_iva - ((aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice]));
            //                            //                aux_iva_sanea[auxiliar_indice] += ajuste_iva;
            //                            //                if (aux_iva_sanea[auxiliar_indice] < 0)
            //                            //                {
            //                            //                    ajuste_iva = aux_iva_sanea[auxiliar_indice];
            //                            //                    aux_iva_sanea[auxiliar_indice] = 0;
            //                            //                    aux_iva_alcan[auxiliar_indice] += ajuste_iva;

            //                            //                }
            //                            //                if (aux_iva_alcan[auxiliar_indice] < 0)
            //                            //                {
            //                            //                    ajuste_iva = aux_iva_alcan[auxiliar_indice];
            //                            //                    aux_iva_alcan[auxiliar_indice] = 0;
            //                            //                    aux_iva_agua[auxiliar_indice] += ajuste_iva;
            //                            //                }
            //                            //                if (aux_iva_agua[auxiliar_indice] < 0)
            //                            //                {
            //                            //                    aux_iva_agua[auxiliar_indice] = 0;
            //                            //                }
            //                            //                iva_acumulado = double.Parse(his_iva[auxiliar_indice]);
            //                            //            }
            //                            //        }

            //                            //    }




            //                            fac_Total_IVA[auxiliar_indice] = aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice];
            //                            iva = double.Parse(his_iva[auxiliar_indice]);
            //                            his_iva_agua[auxiliar_indice] = aux_iva_agua[auxiliar_indice];
            //                            his_iva_alcan[auxiliar_indice] = aux_iva_alcan[auxiliar_indice];
            //                            his_iva_sanea[auxiliar_indice] = aux_iva_sanea[auxiliar_indice];

            //                            fac_Total_Pagado[auxiliar_indice] = fac_Total_Importe[auxiliar_indice] + fac_Total_IVA[auxiliar_indice];
            //                            fac_Saldo[auxiliar_indice] = fac_Total_Pagado[auxiliar_indice] - fac_Total_Abono[auxiliar_indice];
            //                            // -------------------------------- FIN CALCULAR IVA --------------------------------------

            //                            //}
            //                        }
            //                        else
            //                        {
            //                            automatic_id = "0000000000" + (no_Reg + x).ToString();
            //                            fac_No_Factura[auxiliar_indice] = automatic_id.Substring(automatic_id.Length - 10, 10);
            //                            x++;
            //                            fac_No_Cuenta[auxiliar_indice] = encabezado["cuenta"].ToString();
            //                            fac_No_Recibo[auxiliar_indice] = his_foliorecib[auxiliar_indice];
            //                            fac_Region_ID[auxiliar_indice] = encabezado["Region_ID"].ToString();
            //                            fac_Predio_ID[auxiliar_indice] = encabezado["Predio_ID"].ToString();
            //                            fac_Usuario_ID[auxiliar_indice] = encabezado["Usuario_ID"].ToString();
            //                            fac_Medidor_ID[auxiliar_indice] = encabezado["nummedidor"].ToString();
            //                            fac_Tarifa_ID[auxiliar_indice] = encabezado["Tarifa_ID"].ToString();
            //                            fac_Lectura_Anterior[auxiliar_indice] = his_lecanterior[auxiliar_indice];
            //                            fac_Lectura_Actual[auxiliar_indice] = his_lecactual[auxiliar_indice];
            //                            fac_Consumo[auxiliar_indice] = his_consumo[auxiliar_indice]; ;

            //                            fac_Fecha_Inicio[auxiliar_indice] = his_fecha_inicio[auxiliar_indice];
            //                            fac_Fecha_Termino[auxiliar_indice] = his_fecha_termino[auxiliar_indice];
            //                            fac_Fecha_Limite[auxiliar_indice] = his_vencimient[auxiliar_indice];
            //                            fac_Fecha_Emicio[auxiliar_indice] = his_facturacion[auxiliar_indice];
            //                            fac_Periodo[auxiliar_indice] = his_periodo[auxiliar_indice];

            //                            if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //                                fac_Tasa_IVA[auxiliar_indice] = "16";
            //                            else
            //                                fac_Tasa_IVA[auxiliar_indice] = "0.0";
            //                            fac_Estado[auxiliar_indice] = his_estado[auxiliar_indice];
            //                            fac_Anio[auxiliar_indice] = his_anio[auxiliar_indice];
            //                            fac_Bimestre[auxiliar_indice] = his_bimestre[auxiliar_indice];
            //                            fac_RPU[auxiliar_indice] = encabezado["rpu"].ToString();


            //                            if (sinp)
            //                            {
            //                                his_recagua_aux[auxiliar_indice] = (double.Parse(encabezado["recagua"].ToString()) - double.Parse(his_recagua[auxiliar_indice])) + double.Parse(his_recagua[auxiliar_indice]);
            //                                his_recalcan_aux[auxiliar_indice] = (double.Parse(encabezado["recalcan"].ToString()) - double.Parse(his_recalcan[auxiliar_indice])) + double.Parse(his_recalcan[auxiliar_indice]);
            //                                his_recsanea_aux[auxiliar_indice] = (double.Parse(encabezado["recsanea"].ToString()) - double.Parse(his_recsanea[auxiliar_indice])) + double.Parse(his_recsanea[auxiliar_indice]);
            //                                his_pagua[auxiliar_indice] = (double.Parse(his_pagua[auxiliar_indice]) + double.Parse(his_rezagua[auxiliar_indice])).ToString();
            //                                his_palcan[auxiliar_indice] = (double.Parse(his_palcan[auxiliar_indice]) + double.Parse(his_rezalcan[auxiliar_indice])).ToString();
            //                                his_psanea[auxiliar_indice] = (double.Parse(his_psanea[auxiliar_indice]) + double.Parse(his_rezsanea[auxiliar_indice])).ToString();
            //                                sinp = false;
            //                            }
            //                            else
            //                            {
            //                                his_recagua_aux[auxiliar_indice] = (double.Parse(encabezado["recagua"].ToString()) - double.Parse(his_recagua[auxiliar_indice]));
            //                                his_recalcan_aux[auxiliar_indice] = (double.Parse(encabezado["recalcan"].ToString()) - double.Parse(his_recalcan[auxiliar_indice]));
            //                                his_recsanea_aux[auxiliar_indice] = (double.Parse(encabezado["recsanea"].ToString()) - double.Parse(his_recsanea[auxiliar_indice]));
            //                            }



            //                            if (his_recagua_aux[auxiliar_indice] < 0)
            //                            {
            //                                for (int a = auxiliar_indice; a < his_recagua_aux.Length - 1; )
            //                                {
            //                                    if (double.Parse(his_precagua[a]) == 0)
            //                                    {
            //                                        a++;
            //                                    }
            //                                    else
            //                                    {
            //                                        his_recagua_aux[auxiliar_indice] += double.Parse(his_precagua[a]);
            //                                        break;
            //                                    }
            //                                }
            //                            }
            //                            if (his_recalcan_aux[auxiliar_indice] < 0)
            //                            {
            //                                for (int a = auxiliar_indice; a < his_recalcan_aux.Length - 1; )
            //                                {
            //                                    if (double.Parse(his_precalcan[a]) == 0)
            //                                    {
            //                                        a++;
            //                                    }
            //                                    else
            //                                    {
            //                                        his_recalcan_aux[auxiliar_indice] += double.Parse(his_precalcan[a]);
            //                                        break;
            //                                    }
            //                                }
            //                            }
            //                            if (his_recsanea_aux[auxiliar_indice] < 0)
            //                            {
            //                                for (int a = auxiliar_indice; a < his_recsanea_aux.Length - 1; )
            //                                {
            //                                    if (double.Parse(his_precsanea[a]) == 0)
            //                                    {
            //                                        a++;
            //                                    }
            //                                    else
            //                                    {
            //                                        his_recsanea_aux[auxiliar_indice] += double.Parse(his_precsanea[a]);
            //                                        break;
            //                                    }
            //                                }
            //                            }

            //                            aux_recagua[auxiliar_indice] = his_recagua_aux[auxiliar_indice];
            //                            aux_recalcan[auxiliar_indice] = his_recalcan_aux[auxiliar_indice];
            //                            aux_recsanea[auxiliar_indice] = his_recsanea_aux[auxiliar_indice];

            //                            his_crbomb_aux[auxiliar_indice] = (float.Parse(his_crbomb[auxiliar_indice]) - crbomb).ToString();
            //                            aux_crbomb[auxiliar_indice] = float.Parse(his_crbomb_aux[auxiliar_indice]);
            //                            crbomb = float.Parse(his_crbomb[auxiliar_indice]);

            //                            fac_Total_Importe[auxiliar_indice] = float.Parse(his_pagua[auxiliar_indice]) + float.Parse(his_palcan[auxiliar_indice]) + float.Parse(his_psanea[auxiliar_indice]) + float.Parse(his_crbomb_aux[auxiliar_indice]) + his_recagua_aux[auxiliar_indice] + his_recalcan_aux[auxiliar_indice] + his_recsanea_aux[auxiliar_indice];

            //                            if (float.Parse(his_iva[auxiliar_indice]) == 0) //no tiene datos en recivos
            //                            {
            //                                aux_iva_agua[auxiliar_indice] = 0;
            //                                aux_iva_alcan[auxiliar_indice] = 0;
            //                                aux_iva_sanea[auxiliar_indice] = 0;

            //                            }
            //                            else
            //                            {
            //                                if (bandera)
            //                                {
            //                                    aux_iva_agua[auxiliar_indice] = 0;
            //                                    aux_iva_alcan[auxiliar_indice] = double.Parse(his_iva[auxiliar_indice]) / 2;
            //                                    aux_iva_sanea[auxiliar_indice] = double.Parse(his_iva[auxiliar_indice]) / 2;
            //                                    iva_acumulado = double.Parse(his_iva[auxiliar_indice]);
            //                                    bandera = false;
            //                                }
            //                                else
            //                                {
            //                                    if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //                                    {
            //                                        aux_iva_agua[auxiliar_indice] = double.Parse(his_pagua[auxiliar_indice]) * 0.16;
            //                                        aux_iva_alcan[auxiliar_indice] = double.Parse(his_palcan[auxiliar_indice]) * 0.16;
            //                                        aux_iva_sanea[auxiliar_indice] = double.Parse(his_psanea[auxiliar_indice]) * 0.16;
            //                                    }
            //                                    else
            //                                    {
            //                                        aux_iva_agua[auxiliar_indice] = 0;
            //                                        aux_iva_alcan[auxiliar_indice] = double.Parse(his_palcan[auxiliar_indice]) * 0.16;
            //                                        aux_iva_sanea[auxiliar_indice] = double.Parse(his_psanea[auxiliar_indice]) * 0.16;
            //                                    }
            //                                    if (double.Parse(his_iva[auxiliar_indice]) <= iva_acumulado)
            //                                    {
            //                                        for (int a = auxiliar_indice + 1; a < his_piva.Length; )
            //                                        {
            //                                            if (float.Parse(his_piva[a]) == 0)
            //                                            {
            //                                                a++;
            //                                            }
            //                                            else
            //                                            {
            //                                                ajuste_iva = double.Parse(his_piva[a]);
            //                                                break;
            //                                            }
            //                                        }
            //                                        ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - iva_acumulado + ajuste_iva;
            //                                    }
            //                                    else
            //                                    {
            //                                        ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - iva_acumulado;

            //                                    }
            //                                    ajuste_iva = ajuste_iva - ((aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice]));
            //                                    aux_iva_sanea[auxiliar_indice] += ajuste_iva;
            //                                    if (aux_iva_sanea[auxiliar_indice] < 0)
            //                                    {
            //                                        ajuste_iva = aux_iva_sanea[auxiliar_indice];
            //                                        aux_iva_sanea[auxiliar_indice] = 0;
            //                                        aux_iva_alcan[auxiliar_indice] += ajuste_iva;

            //                                    }
            //                                    if (aux_iva_alcan[auxiliar_indice] < 0)
            //                                    {
            //                                        ajuste_iva = aux_iva_alcan[auxiliar_indice];
            //                                        aux_iva_alcan[auxiliar_indice] = 0;
            //                                        aux_iva_agua[auxiliar_indice] += ajuste_iva;
            //                                    }
            //                                    if (aux_iva_agua[auxiliar_indice] < 0)
            //                                    {
            //                                        aux_iva_agua[auxiliar_indice] = 0;
            //                                    }
            //                                    iva_acumulado = double.Parse(his_iva[auxiliar_indice]);

            //                                }

            //                            }
            //                            // ----------------------------------- CALCULAR IVA ------------------------------------
            //                            //if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //                            //{
            //                            //    aux_iva_agua[auxiliar_indice] = double.Parse(his_pagua[auxiliar_indice]) * 0.16;
            //                            //    aux_iva_alcan[auxiliar_indice] = double.Parse(his_palcan[auxiliar_indice]) * 0.16;
            //                            //    aux_iva_sanea[auxiliar_indice] = double.Parse(his_psanea[auxiliar_indice]) * 0.16;
            //                            //}
            //                            //else
            //                            //{
            //                            //    aux_iva_agua[auxiliar_indice] = 0f;
            //                            //    aux_iva_alcan[auxiliar_indice] = double.Parse(his_palcan[auxiliar_indice]) * 0.16;
            //                            //    aux_iva_sanea[auxiliar_indice] = double.Parse(his_psanea[auxiliar_indice]) * 0.16;
            //                            //}
            //                            //if (float.Parse(his_iva[auxiliar_indice]) == 0)
            //                            //{
            //                            //    iva_acumulado += aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice];
            //                            //    bandera = false;
            //                            //}
            //                            //else
            //                            //{
            //                            //    if (iva_acumulado != 0)
            //                            //    {
            //                            //        bandera = false;
            //                            //        ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - iva_acumulado;
            //                            //        ajuste_iva = ajuste_iva - ((aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice]));
            //                            //        aux_iva_sanea[auxiliar_indice] += ajuste_iva;
            //                            //        if (aux_iva_sanea[auxiliar_indice] < 0)
            //                            //        {
            //                            //            ajuste_iva = aux_iva_sanea[auxiliar_indice];
            //                            //            aux_iva_sanea[auxiliar_indice] = 0;
            //                            //            aux_iva_alcan[auxiliar_indice] += ajuste_iva;

            //                            //        }
            //                            //        if (aux_iva_alcan[auxiliar_indice] < 0)
            //                            //        {
            //                            //            ajuste_iva = aux_iva_alcan[auxiliar_indice];
            //                            //            aux_iva_alcan[auxiliar_indice] = 0;
            //                            //            aux_iva_agua[auxiliar_indice] += ajuste_iva;
            //                            //        }
            //                            //        if (aux_iva_agua[auxiliar_indice] < 0)
            //                            //        {
            //                            //            aux_iva_agua[auxiliar_indice] = 0;
            //                            //        }
            //                            //        iva_acumulado = double.Parse(his_iva[auxiliar_indice]);
            //                            //    }
            //                            //    else
            //                            //    {
            //                            //        if (bandera)
            //                            //        {
            //                            //            bandera = false;
            //                            //            ajuste_iva = double.Parse(his_iva[auxiliar_indice]);
            //                            //            ajuste_iva = ajuste_iva - ((aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice]));
            //                            //            aux_iva_sanea[auxiliar_indice] += ajuste_iva;
            //                            //            if (aux_iva_sanea[auxiliar_indice] < 0)
            //                            //            {
            //                            //                ajuste_iva = aux_iva_sanea[auxiliar_indice];
            //                            //                aux_iva_sanea[auxiliar_indice] = 0;
            //                            //                aux_iva_alcan[auxiliar_indice] += ajuste_iva;

            //                            //            }
            //                            //            if (aux_iva_alcan[auxiliar_indice] < 0)
            //                            //            {
            //                            //                ajuste_iva = aux_iva_alcan[auxiliar_indice];
            //                            //                aux_iva_alcan[auxiliar_indice] = 0;
            //                            //                aux_iva_agua[auxiliar_indice] += ajuste_iva;
            //                            //            }
            //                            //            if (aux_iva_agua[auxiliar_indice] < 0)
            //                            //            {
            //                            //                aux_iva_agua[auxiliar_indice] = 0;
            //                            //            }
            //                            //            iva_acumulado = double.Parse(his_iva[auxiliar_indice]);

            //                            //        }

            //                            //        else
            //                            //        {
            //                            //            for (int a = auxiliar_indice + 1; a < his_piva.Length; )
            //                            //            {
            //                            //                if (float.Parse(his_piva[a]) == 0)
            //                            //                {
            //                            //                    a++;
            //                            //                }
            //                            //                else
            //                            //                {
            //                            //                    ajuste_iva = double.Parse(his_piva[a]);
            //                            //                    break;
            //                            //                }
            //                            //            }

            //                            //            if (double.Parse(his_iva[auxiliar_indice])>=double.Parse(his_iva[auxiliar_indice + 1])){
            //                            //                ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - double.Parse(his_iva[auxiliar_indice + 1]) ;
            //                            //                }else{
            //                            //                ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - double.Parse(his_iva[auxiliar_indice + 1]) + ajuste_iva;
            //                            //                }
            //                            //            ajuste_iva = ajuste_iva - ((aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice]));
            //                            //            aux_iva_sanea[auxiliar_indice] += ajuste_iva;
            //                            //            if (aux_iva_sanea[auxiliar_indice] < 0)
            //                            //            {
            //                            //                ajuste_iva = aux_iva_sanea[auxiliar_indice];
            //                            //                aux_iva_sanea[auxiliar_indice] = 0;
            //                            //                aux_iva_alcan[auxiliar_indice] += ajuste_iva;

            //                            //            }
            //                            //            if (aux_iva_alcan[auxiliar_indice] < 0)
            //                            //            {
            //                            //                ajuste_iva = aux_iva_alcan[auxiliar_indice];
            //                            //                aux_iva_alcan[auxiliar_indice] = 0;
            //                            //                aux_iva_agua[auxiliar_indice] += ajuste_iva;
            //                            //            }
            //                            //            if (aux_iva_agua[auxiliar_indice] < 0)
            //                            //            {
            //                            //                aux_iva_agua[auxiliar_indice] = 0;
            //                            //            }
            //                            //            iva_acumulado = double.Parse(his_iva[auxiliar_indice]);
            //                            //        }
            //                            //    }

            //                            //}
            //                            fac_Total_IVA[auxiliar_indice] = aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice];
            //                            //ajuste_iva = float.Parse(his_iva[auxiliar_indice]) - float.Parse(fac_Total_IVA[auxiliar_indice]) - iva;
            //                            iva = double.Parse(his_iva[auxiliar_indice]);
            //                            //fac_Total_IVA[auxiliar_indice] = (float.Parse(fac_Total_IVA[auxiliar_indice]) + ajuste_iva).ToString();
            //                            // aux_iva_sanea[auxiliar_indice] += ajuste_iva;
            //                            his_iva_agua[auxiliar_indice] = aux_iva_agua[auxiliar_indice];
            //                            his_iva_alcan[auxiliar_indice] = aux_iva_alcan[auxiliar_indice];
            //                            his_iva_sanea[auxiliar_indice] = aux_iva_sanea[auxiliar_indice];

            //                            fac_Total_Pagado[auxiliar_indice] = fac_Total_Importe[auxiliar_indice] + fac_Total_IVA[auxiliar_indice];
            //                            fac_Saldo[auxiliar_indice] = fac_Total_Pagado[auxiliar_indice] - fac_Total_Abono[auxiliar_indice];
            //                            // -------------------------------- FIN CALCULAR IVA --------------------------------------

            //                        }

            //                        break;
            //                    case "PAGADO":
            //                        Estatus = "PAGADO";
            //                        break;
            //                    case "A FAVOR":
            //                        Estatus = "A FAVOR";
            //                        break;
            //                    case "Conv Admvo":
            //                        Estatus = "Conv Admvo";
            //                        break;
            //                    case "Conv Inter":
            //                        Estatus = "Conv Inter";
            //                        break;
            //                    case "Conv PEC":
            //                        Estatus = "Conv PEC";
            //                        break;
            //                    case "Ren pagare":
            //                        Estatus = "Ren pagare";
            //                        break;
            //                    case "SUBSIDIO":
            //                        Estatus = "SUBSIDIO";
            //                        break;
            //                    case "NO MODIF":
            //                        Estatus = "No modificado";
            //                        break;

            //                    case "PARCIAL":
            //                        Estatus = his_estado[auxiliar_indice].Trim();
            //                        if (auxiliar_indice > 0)
            //                        {
            //                            automatic_id = "0000000000" + (no_Reg + x).ToString();
            //                            fac_No_Factura[auxiliar_indice] = automatic_id.Substring(automatic_id.Length - 10, 10);
            //                            x++;
            //                            fac_No_Cuenta[auxiliar_indice] = encabezado["cuenta"].ToString();
            //                            fac_No_Recibo[auxiliar_indice] = his_foliorecib[auxiliar_indice];
            //                            fac_Region_ID[auxiliar_indice] = encabezado["Region_ID"].ToString();
            //                            fac_Predio_ID[auxiliar_indice] = encabezado["Predio_ID"].ToString();
            //                            fac_Usuario_ID[auxiliar_indice] = encabezado["Usuario_ID"].ToString();
            //                            fac_Medidor_ID[auxiliar_indice] = encabezado["nummedidor"].ToString();
            //                            fac_Tarifa_ID[auxiliar_indice] = encabezado["Tarifa_ID"].ToString();
            //                            fac_Lectura_Anterior[auxiliar_indice] = his_lecanterior[auxiliar_indice];
            //                            fac_Lectura_Actual[auxiliar_indice] = his_lecactual[auxiliar_indice];
            //                            fac_Consumo[auxiliar_indice] = his_consumo[auxiliar_indice]; ;
            //                            fac_Fecha_Inicio[auxiliar_indice] = his_fecha_inicio[auxiliar_indice];
            //                            fac_Fecha_Termino[auxiliar_indice] = his_fecha_termino[auxiliar_indice];
            //                            fac_Fecha_Limite[auxiliar_indice] = his_vencimient[auxiliar_indice];
            //                            fac_Fecha_Emicio[auxiliar_indice] = his_facturacion[auxiliar_indice];
            //                            fac_Periodo[auxiliar_indice] = his_periodo[auxiliar_indice];

            //                            if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //                                fac_Tasa_IVA[auxiliar_indice] = "16";
            //                            else
            //                                fac_Tasa_IVA[auxiliar_indice] = "0.0";
            //                            fac_Estado[auxiliar_indice] = his_estado[auxiliar_indice];
            //                            fac_Anio[auxiliar_indice] = his_anio[auxiliar_indice];
            //                            fac_Bimestre[auxiliar_indice] = his_bimestre[auxiliar_indice];
            //                            fac_RPU[auxiliar_indice] = encabezado["rpu"].ToString();


            //                            if (sinp)
            //                            {
            //                                his_recagua_aux[auxiliar_indice] = (double.Parse(his_recagua[auxiliar_indice - 1]) - double.Parse(his_recagua[auxiliar_indice])) + double.Parse(his_recagua[auxiliar_indice]);
            //                                his_recalcan_aux[auxiliar_indice] = (double.Parse(his_recalcan[auxiliar_indice - 1]) - double.Parse(his_recalcan[auxiliar_indice])) + double.Parse(his_recalcan[auxiliar_indice]);
            //                                his_recsanea_aux[auxiliar_indice] = (double.Parse(his_recsanea[auxiliar_indice - 1]) - double.Parse(his_recsanea[auxiliar_indice])) + double.Parse(his_recsanea[auxiliar_indice]);
            //                                his_pagua[auxiliar_indice] = (double.Parse(his_pagua[auxiliar_indice]) + double.Parse(his_rezagua[auxiliar_indice])).ToString();
            //                                his_palcan[auxiliar_indice] = (double.Parse(his_palcan[auxiliar_indice]) + double.Parse(his_rezalcan[auxiliar_indice])).ToString();
            //                                his_psanea[auxiliar_indice] = (double.Parse(his_psanea[auxiliar_indice]) + double.Parse(his_rezsanea[auxiliar_indice])).ToString();
            //                                sinp = false;
            //                            }
            //                            else
            //                            {
            //                                his_recagua_aux[auxiliar_indice] = (double.Parse(his_recagua[auxiliar_indice - 1]) - double.Parse(his_recagua[auxiliar_indice]));
            //                                his_recalcan_aux[auxiliar_indice] = (double.Parse(his_recalcan[auxiliar_indice - 1]) - double.Parse(his_recalcan[auxiliar_indice]));
            //                                his_recsanea_aux[auxiliar_indice] = (double.Parse(his_recsanea[auxiliar_indice - 1]) - double.Parse(his_recsanea[auxiliar_indice]));
            //                            }

            //                            if (his_recagua_aux[auxiliar_indice] < 0)
            //                            {
            //                                for (int a = auxiliar_indice; a < his_recagua_aux.Length - 1; )
            //                                {
            //                                    if (double.Parse(his_precagua[a]) == 0)
            //                                    {
            //                                        a++;
            //                                    }
            //                                    else
            //                                    {
            //                                        his_recagua_aux[auxiliar_indice] += double.Parse(his_precagua[a]);
            //                                        break;
            //                                    }
            //                                }
            //                            }
            //                            if (his_recalcan_aux[auxiliar_indice] < 0)
            //                            {
            //                                for (int a = auxiliar_indice; a < his_recalcan_aux.Length - 1; )
            //                                {
            //                                    if (double.Parse(his_precalcan[a]) == 0)
            //                                    {
            //                                        a++;
            //                                    }
            //                                    else
            //                                    {
            //                                        his_recalcan_aux[auxiliar_indice] += double.Parse(his_precalcan[a]);
            //                                        break;
            //                                    }
            //                                }
            //                            }
            //                            if (his_recsanea_aux[auxiliar_indice] < 0)
            //                            {
            //                                for (int a = auxiliar_indice; a < his_recsanea_aux.Length - 1; )
            //                                {
            //                                    if (double.Parse(his_precsanea[a]) == 0)
            //                                    {
            //                                        a++;
            //                                    }
            //                                    else
            //                                    {
            //                                        his_recsanea_aux[auxiliar_indice] += double.Parse(his_precsanea[a]);
            //                                        break;
            //                                    }
            //                                }
            //                            }

            //                            aux_recagua[auxiliar_indice] = his_recagua_aux[auxiliar_indice];
            //                            aux_recalcan[auxiliar_indice] = his_recalcan_aux[auxiliar_indice];
            //                            aux_recsanea[auxiliar_indice] = his_recsanea_aux[auxiliar_indice];
            //                            his_crbomb_aux[auxiliar_indice] = (float.Parse(his_crbomb[auxiliar_indice]) - crbomb).ToString();
            //                            aux_crbomb[auxiliar_indice] = float.Parse(his_crbomb_aux[auxiliar_indice]);
            //                            crbomb = float.Parse(his_crbomb[auxiliar_indice]);


            //                            fac_Total_Importe[auxiliar_indice] = float.Parse(his_pagua[auxiliar_indice]) + float.Parse(his_palcan[auxiliar_indice]) + float.Parse(his_psanea[auxiliar_indice]) + float.Parse(his_crbomb_aux[auxiliar_indice]) + his_recagua_aux[auxiliar_indice] + his_recalcan_aux[auxiliar_indice] + his_recsanea_aux[auxiliar_indice];
            //                            if (float.Parse(his_iva[auxiliar_indice]) == 0) //no tiene datos en recivos
            //                            {
            //                                aux_iva_agua[auxiliar_indice] = 0;
            //                                aux_iva_alcan[auxiliar_indice] = 0;
            //                                aux_iva_sanea[auxiliar_indice] = 0;

            //                            }
            //                            else
            //                            {
            //                                if (bandera)
            //                                {
            //                                    aux_iva_agua[auxiliar_indice] = 0;
            //                                    aux_iva_alcan[auxiliar_indice] = double.Parse(his_iva[auxiliar_indice]) / 2;
            //                                    aux_iva_sanea[auxiliar_indice] = double.Parse(his_iva[auxiliar_indice]) / 2;
            //                                    iva_acumulado = double.Parse(his_iva[auxiliar_indice]);
            //                                    bandera = false;
            //                                }
            //                                else
            //                                {
            //                                    if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //                                    {
            //                                        aux_iva_agua[auxiliar_indice] = double.Parse(his_pagua[auxiliar_indice]) * 0.16;
            //                                        aux_iva_alcan[auxiliar_indice] = double.Parse(his_palcan[auxiliar_indice]) * 0.16;
            //                                        aux_iva_sanea[auxiliar_indice] = double.Parse(his_psanea[auxiliar_indice]) * 0.16;
            //                                    }
            //                                    else
            //                                    {
            //                                        aux_iva_agua[auxiliar_indice] = 0;
            //                                        aux_iva_alcan[auxiliar_indice] = double.Parse(his_palcan[auxiliar_indice]) * 0.16;
            //                                        aux_iva_sanea[auxiliar_indice] = double.Parse(his_psanea[auxiliar_indice]) * 0.16;
            //                                    }
            //                                    if (double.Parse(his_iva[auxiliar_indice]) <= iva_acumulado)
            //                                    {
            //                                        for (int a = auxiliar_indice + 1; a < his_piva.Length; )
            //                                        {
            //                                            if (float.Parse(his_piva[a]) == 0)
            //                                            {
            //                                                a++;
            //                                            }
            //                                            else
            //                                            {
            //                                                ajuste_iva = double.Parse(his_piva[a]);
            //                                                break;
            //                                            }
            //                                        }
            //                                        ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - iva_acumulado + ajuste_iva;
            //                                    }
            //                                    else
            //                                    {
            //                                        ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - iva_acumulado;

            //                                    }
            //                                    ajuste_iva = ajuste_iva - ((aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice]));
            //                                    aux_iva_sanea[auxiliar_indice] += ajuste_iva;
            //                                    if (aux_iva_sanea[auxiliar_indice] < 0)
            //                                    {
            //                                        ajuste_iva = aux_iva_sanea[auxiliar_indice];
            //                                        aux_iva_sanea[auxiliar_indice] = 0;
            //                                        aux_iva_alcan[auxiliar_indice] += ajuste_iva;

            //                                    }
            //                                    if (aux_iva_alcan[auxiliar_indice] < 0)
            //                                    {
            //                                        ajuste_iva = aux_iva_alcan[auxiliar_indice];
            //                                        aux_iva_alcan[auxiliar_indice] = 0;
            //                                        aux_iva_agua[auxiliar_indice] += ajuste_iva;
            //                                    }
            //                                    if (aux_iva_agua[auxiliar_indice] < 0)
            //                                    {
            //                                        aux_iva_agua[auxiliar_indice] = 0;
            //                                    }
            //                                    iva_acumulado = double.Parse(his_iva[auxiliar_indice]);

            //                                }

            //                            }
            //                            //// ----------------------------------- CALCULAR IVA ------------------------------------
            //                            //if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //                            //{
            //                            //    aux_iva_agua[auxiliar_indice] = double.Parse(his_pagua[auxiliar_indice]) * 0.16;
            //                            //}
            //                            //else
            //                            //{
            //                            //    aux_iva_agua[auxiliar_indice] = 0f;
            //                            //}
            //                            //aux_iva_alcan[auxiliar_indice] = double.Parse(his_palcan[auxiliar_indice]) * 0.16;
            //                            //aux_iva_sanea[auxiliar_indice] = double.Parse(his_psanea[auxiliar_indice]) * 0.16;

            //                            //if (float.Parse(his_iva[auxiliar_indice]) == 0)
            //                            //{
            //                            //    iva_acumulado += aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice]-double.Parse( his_piva[auxiliar_indice]);
            //                            //}
            //                            //else
            //                            //{
            //                            //    if (iva_acumulado != 0)
            //                            //    {
            //                            //        ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - iva_acumulado + double.Parse(his_piva[auxiliar_indice]); ;
            //                            //        ajuste_iva = ajuste_iva - ((aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice]));
            //                            //        aux_iva_sanea[auxiliar_indice] += ajuste_iva;
            //                            //        if (aux_iva_sanea[auxiliar_indice] < 0)
            //                            //        {
            //                            //            ajuste_iva = aux_iva_sanea[auxiliar_indice];
            //                            //            aux_iva_sanea[auxiliar_indice] = 0;
            //                            //            aux_iva_alcan[auxiliar_indice] += ajuste_iva;

            //                            //        }
            //                            //        if (aux_iva_alcan[auxiliar_indice] < 0)
            //                            //        {
            //                            //            ajuste_iva = aux_iva_alcan[auxiliar_indice];
            //                            //            aux_iva_alcan[auxiliar_indice] = 0;
            //                            //            aux_iva_agua[auxiliar_indice] += ajuste_iva;
            //                            //        }
            //                            //        if (aux_iva_agua[auxiliar_indice] < 0)
            //                            //        {
            //                            //            aux_iva_agua[auxiliar_indice] = 0;
            //                            //        }
            //                            //        iva_acumulado = 0;
            //                            //    }
            //                            //    else
            //                            //    {
            //                            //        if (bandera)
            //                            //        {
            //                            //            bandera = false;
            //                            //            ajuste_iva = double.Parse(his_iva[auxiliar_indice]);
            //                            //            ajuste_iva = ajuste_iva - ((aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice]));
            //                            //            aux_iva_sanea[auxiliar_indice] += ajuste_iva;
            //                            //            if (aux_iva_sanea[auxiliar_indice] < 0)
            //                            //            {
            //                            //                ajuste_iva = aux_iva_sanea[auxiliar_indice];
            //                            //                aux_iva_sanea[auxiliar_indice] = 0;
            //                            //                aux_iva_alcan[auxiliar_indice] += ajuste_iva;

            //                            //            }
            //                            //            if (aux_iva_alcan[auxiliar_indice] < 0)
            //                            //            {
            //                            //                ajuste_iva = aux_iva_alcan[auxiliar_indice];
            //                            //                aux_iva_alcan[auxiliar_indice] = 0;
            //                            //                aux_iva_agua[auxiliar_indice] += ajuste_iva;
            //                            //            }
            //                            //            if (aux_iva_agua[auxiliar_indice] < 0)
            //                            //            {
            //                            //                aux_iva_agua[auxiliar_indice] = 0;
            //                            //            }
            //                            //            iva_acumulado = double.Parse(his_iva[auxiliar_indice]);

            //                            //        }

            //                            //        else
            //                            //        {
            //                            //            for (int a = auxiliar_indice + 1; a<his_piva.Length; )
            //                            //            {
            //                            //                if (float.Parse(his_piva[a]) == 0)
            //                            //                {
            //                            //                    a++;
            //                            //                }
            //                            //                else
            //                            //                {
            //                            //                    ajuste_iva = double.Parse(his_piva[a]);
            //                            //                    break;
            //                            //                }
            //                            //            }
            //                            //            if (double.Parse(his_iva[auxiliar_indice])>=double.Parse(his_iva[auxiliar_indice + 1])){
            //                            //            ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - double.Parse(his_iva[auxiliar_indice + 1]) ;
            //                            //            }else{
            //                            //            ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - double.Parse(his_iva[auxiliar_indice + 1]) + ajuste_iva;
            //                            //            }

            //                            //            ajuste_iva = ajuste_iva - ((aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice]));
            //                            //            aux_iva_sanea[auxiliar_indice] += ajuste_iva;
            //                            //            if (aux_iva_sanea[auxiliar_indice] < 0)
            //                            //            {
            //                            //                ajuste_iva = aux_iva_sanea[auxiliar_indice];
            //                            //                aux_iva_sanea[auxiliar_indice] = 0;
            //                            //                aux_iva_alcan[auxiliar_indice] += ajuste_iva;

            //                            //            }
            //                            //            if (aux_iva_alcan[auxiliar_indice] < 0)
            //                            //            {
            //                            //                ajuste_iva = aux_iva_alcan[auxiliar_indice];
            //                            //                aux_iva_alcan[auxiliar_indice] = 0;
            //                            //                aux_iva_agua[auxiliar_indice] += ajuste_iva;
            //                            //            }
            //                            //            if (aux_iva_agua[auxiliar_indice] < 0)
            //                            //            {
            //                            //                aux_iva_agua[auxiliar_indice] = 0;
            //                            //            }
            //                            //            iva_acumulado = double.Parse(his_iva[auxiliar_indice]);
            //                            //        }
            //                            //    }

            //                            //}
            //                            fac_Total_IVA[auxiliar_indice] = aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice];
            //                            iva = double.Parse(his_iva[auxiliar_indice]);
            //                            his_iva_agua[auxiliar_indice] = aux_iva_agua[auxiliar_indice];
            //                            his_iva_alcan[auxiliar_indice] = aux_iva_alcan[auxiliar_indice];
            //                            his_iva_sanea[auxiliar_indice] = aux_iva_sanea[auxiliar_indice];
            //                            // -------------------------------- FIN CALCULAR IVA --------------------------------------
            //                            fac_Total_Pagado[auxiliar_indice] = fac_Total_Importe[auxiliar_indice] + fac_Total_IVA[auxiliar_indice];
            //                            fac_Saldo[auxiliar_indice] = fac_Total_Pagado[auxiliar_indice] - fac_Total_Abono[auxiliar_indice];

            //                            prezagua = double.Parse(his_prezagua[auxiliar_indice].ToString()) + double.Parse(his_ppagua[auxiliar_indice]);
            //                            prezalcan = double.Parse(his_prezalcan[auxiliar_indice].ToString()) + double.Parse(his_ppalcan[auxiliar_indice]);
            //                            prezsanea = double.Parse(his_prezsanea[auxiliar_indice].ToString()) + double.Parse(his_ppsanea[auxiliar_indice]);
            //                            precagua = double.Parse(his_precagua[auxiliar_indice].ToString());
            //                            precalcan = double.Parse(his_precalcan[auxiliar_indice].ToString());
            //                            precsanea = double.Parse(his_precsanea[auxiliar_indice].ToString());
            //                            pcrbomb = float.Parse(his_pcrbomb[auxiliar_indice].ToString());
            //                            piva = double.Parse(his_piva[auxiliar_indice].ToString());
            //                            aux_crbomb[auxiliar_indice] = float.Parse(his_crbomb[auxiliar_indice]) - crbomb;
            //                            crbomb = 0;

            //                            aux_i = indice - 1;
            //                            for (; aux_i >= 0; aux_i--)
            //                            {
            //                                if (prezagua == 0 && prezalcan == 0 && prezsanea == 0 && precagua == 0 && precalcan == 0 && precsanea == 0 && pcrbomb == 0 && piva == 0)
            //                                {
            //                                    break;
            //                                }

            //                                if (!string.IsNullOrEmpty(fac_Estado[aux_i]))
            //                                {
            //                                    if (fac_Estado[aux_i].Trim() == "REZAGADO" || fac_Estado[aux_i].Trim() == "PARCIAL")
            //                                    {

            //                                        /////////////////////INICIA_PAGUA///////////////////////
            //                                        if (aux_pagua[aux_i] != 0)
            //                                        {
            //                                            if (aux_pagua[aux_i] <= prezagua)
            //                                            {
            //                                                pago_rezagua[aux_i] += aux_pagua[aux_i];
            //                                                prezagua -= aux_pagua[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_pagua[aux_i];
            //                                                aux_pagua[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {
            //                                                pago_rezagua[aux_i] += prezagua;
            //                                                aux_pagua[aux_i] = aux_pagua[aux_i] - prezagua;
            //                                                fac_Total_Abono[aux_i] += prezagua;
            //                                                prezagua = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////Fin_PAGUA///////////////////////
            //                                        /////////////////////INICIA_Palcan///////////////////////
            //                                        if (aux_palcan[aux_i] != 0)
            //                                        {
            //                                            if (aux_palcan[aux_i] <= prezalcan)
            //                                            {
            //                                                pago_rezalcan[aux_i] += aux_palcan[aux_i];
            //                                                prezalcan -= aux_palcan[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_palcan[aux_i];
            //                                                aux_palcan[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {

            //                                                pago_rezalcan[aux_i] += prezalcan;
            //                                                aux_palcan[aux_i] -= prezalcan;
            //                                                fac_Total_Abono[aux_i] += prezalcan;
            //                                                prezalcan = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////Fin_PAlcan///////////////////////
            //                                        /////////////////////INICIA_Psanea///////////////////////
            //                                        if (aux_psanea[aux_i] != 0)
            //                                        {
            //                                            if (aux_psanea[aux_i] <= prezsanea)
            //                                            {
            //                                                pago_rezsanea[aux_i] += aux_psanea[aux_i];
            //                                                prezsanea -= aux_psanea[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_psanea[aux_i];
            //                                                aux_psanea[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {
            //                                                pago_rezsanea[aux_i] += prezsanea;
            //                                                aux_psanea[aux_i] -= prezsanea;
            //                                                fac_Total_Abono[aux_i] += prezsanea;
            //                                                prezsanea = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////Fin_Psanea///////////////////////
            //                                        /////////////////////INICIA_recagua///////////////////////
            //                                        if (aux_recagua[aux_i] != 0)
            //                                        {
            //                                            if (aux_recagua[aux_i] <= precagua)
            //                                            {
            //                                                pago_recagua[aux_i] += aux_recagua[aux_i];
            //                                                precagua -= aux_recagua[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_recagua[aux_i];
            //                                                aux_recagua[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {
            //                                                pago_recagua[aux_i] += precagua;
            //                                                aux_recagua[aux_i] -= precagua;
            //                                                fac_Total_Abono[aux_i] += precagua;
            //                                                precagua = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////FIN_rec_agua///////////////////////
            //                                        /////////////////////INICIA_recalcan///////////////////////
            //                                        if (aux_recalcan[aux_i] != 0)
            //                                        {
            //                                            if (aux_recalcan[aux_i] <= precalcan)
            //                                            {
            //                                                pago_recalcan[aux_i] += aux_recalcan[aux_i];
            //                                                precalcan -= aux_recalcan[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_recalcan[aux_i];
            //                                                aux_recalcan[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {
            //                                                pago_recalcan[aux_i] += precalcan;
            //                                                aux_recalcan[aux_i] -= precalcan;
            //                                                fac_Total_Abono[aux_i] += precalcan;
            //                                                precalcan = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////fin_recalcan///////////////////////
            //                                        /////////////////////fin_recsanea///////////////////////
            //                                        if (aux_recsanea[aux_i] != 0)
            //                                        {
            //                                            if (aux_recsanea[aux_i] <= precsanea)
            //                                            {
            //                                                pago_recsanea[aux_i] += aux_recsanea[aux_i];
            //                                                precsanea -= aux_recsanea[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_recsanea[aux_i];
            //                                                aux_recsanea[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {
            //                                                pago_recsanea[aux_i] += precsanea;
            //                                                aux_recsanea[aux_i] -= precsanea;
            //                                                fac_Total_Abono[aux_i] += precsanea;
            //                                                precsanea = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////fin_recsanea///////////////////////
            //                                        /////////////////////inicia_iva_agua///////////////////////
            //                                        if (aux_iva_agua[aux_i] != 0)
            //                                        {
            //                                            if (aux_iva_agua[aux_i] <= piva)
            //                                            {
            //                                                pago_iva_agua[aux_i] += aux_iva_agua[aux_i];
            //                                                piva -= aux_iva_agua[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_iva_agua[aux_i];
            //                                                aux_iva_agua[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {
            //                                                pago_iva_agua[aux_i] += piva;
            //                                                aux_iva_agua[aux_i] -= piva;
            //                                                fac_Total_Abono[aux_i] += piva;
            //                                                piva = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////fin_iva_agua///////////////////////
            //                                        /////////////////////inicia_iva_alcan///////////////////////
            //                                        if (aux_iva_alcan[aux_i] != 0)
            //                                        {
            //                                            if (aux_iva_alcan[aux_i] <= piva)
            //                                            {
            //                                                pago_iva_alcan[aux_i] += aux_iva_alcan[aux_i];
            //                                                piva -= aux_iva_alcan[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_iva_alcan[aux_i];
            //                                                aux_iva_alcan[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {
            //                                                pago_iva_alcan[aux_i] += piva;
            //                                                aux_iva_alcan[aux_i] -= piva;
            //                                                fac_Total_Abono[aux_i] += piva;
            //                                                piva = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////fin_iva_alcan///////////////////////
            //                                        /////////////////////inicia_iva_sanea///////////////////////
            //                                        if (aux_iva_sanea[aux_i] != 0)
            //                                        {
            //                                            if (aux_iva_sanea[aux_i] <= piva)
            //                                            {
            //                                                pago_iva_sanea[aux_i] += aux_iva_sanea[aux_i];
            //                                                piva -= aux_iva_sanea[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_iva_sanea[aux_i];
            //                                                aux_iva_sanea[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {
            //                                                pago_iva_sanea[aux_i] += piva;
            //                                                aux_iva_sanea[aux_i] -= piva;
            //                                                fac_Total_Abono[aux_i] += piva;
            //                                                piva = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////fin_iva_sanea///////////////////////
            //                                        /////////////////////inicia_pcrbomb///////////////////////
            //                                        if (aux_crbomb[aux_i] != 0)
            //                                        {
            //                                            if (aux_crbomb[aux_i] <= pcrbomb)
            //                                            {
            //                                                pago_crbomb[aux_i] += aux_crbomb[aux_i];
            //                                                pcrbomb -= aux_crbomb[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_crbomb[aux_i];
            //                                                aux_crbomb[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {
            //                                                pago_crbomb[aux_i] += pcrbomb;
            //                                                aux_crbomb[aux_i] -= pcrbomb;
            //                                                fac_Total_Abono[aux_i] += pcrbomb;
            //                                                pcrbomb = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////fin_pcrbomb///////////////////////
            //                                    }
            //                                }
            //                                fac_Saldo[aux_i] = fac_Total_Pagado[aux_i] - fac_Total_Abono[aux_i];
            //                                if (!string.IsNullOrEmpty(fac_Estado[aux_i]))
            //                                {
            //                                    if ((aux_pagua[aux_i] + aux_palcan[aux_i] + aux_psanea[aux_i] + aux_recagua[aux_i] + aux_recalcan[aux_i] + aux_recsanea[aux_i]
            //                                                     + aux_iva_agua[aux_i] + aux_iva_alcan[aux_i] + aux_iva_sanea[aux_i]) == 0)
            //                                    {
            //                                        fac_Estado[aux_i] = "PAGADO";
            //                                    }
            //                                }
            //                            }/////////////////////fin del for para pagar
            //                            //}

            //                        }
            //                        else
            //                        {
            //                            automatic_id = "0000000000" + (no_Reg + x).ToString();
            //                            fac_No_Factura[auxiliar_indice] = automatic_id.Substring(automatic_id.Length - 10, 10);
            //                            x++;
            //                            fac_No_Cuenta[auxiliar_indice] = encabezado["cuenta"].ToString();
            //                            fac_No_Recibo[auxiliar_indice] = his_foliorecib[auxiliar_indice];
            //                            fac_Region_ID[auxiliar_indice] = encabezado["Region_ID"].ToString();
            //                            fac_Predio_ID[auxiliar_indice] = encabezado["Predio_ID"].ToString();
            //                            fac_Usuario_ID[auxiliar_indice] = encabezado["Usuario_ID"].ToString();
            //                            fac_Medidor_ID[auxiliar_indice] = encabezado["nummedidor"].ToString();
            //                            fac_Tarifa_ID[auxiliar_indice] = encabezado["Tarifa_ID"].ToString();
            //                            fac_Lectura_Anterior[auxiliar_indice] = his_lecanterior[auxiliar_indice];
            //                            fac_Lectura_Actual[auxiliar_indice] = his_lecactual[auxiliar_indice];
            //                            fac_Consumo[auxiliar_indice] = his_consumo[auxiliar_indice]; ;
            //                            fac_Fecha_Inicio[auxiliar_indice] = his_fecha_inicio[auxiliar_indice];
            //                            fac_Fecha_Termino[auxiliar_indice] = his_fecha_termino[auxiliar_indice];
            //                            fac_Fecha_Limite[auxiliar_indice] = his_vencimient[auxiliar_indice];
            //                            fac_Fecha_Emicio[auxiliar_indice] = his_facturacion[auxiliar_indice];
            //                            fac_Periodo[auxiliar_indice] = his_periodo[auxiliar_indice];

            //                            if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //                                fac_Tasa_IVA[auxiliar_indice] = "16";
            //                            else
            //                                fac_Tasa_IVA[auxiliar_indice] = "0.0";
            //                            fac_Estado[auxiliar_indice] = his_estado[auxiliar_indice];
            //                            fac_Anio[auxiliar_indice] = his_anio[auxiliar_indice];
            //                            fac_Bimestre[auxiliar_indice] = his_bimestre[auxiliar_indice];
            //                            fac_RPU[auxiliar_indice] = encabezado["rpu"].ToString();

            //                            if (sinp)
            //                            {
            //                                his_recagua_aux[auxiliar_indice] = (double.Parse(encabezado["recagua"].ToString()) - double.Parse(his_recagua[auxiliar_indice])) + double.Parse(his_recagua[auxiliar_indice]);
            //                                his_recalcan_aux[auxiliar_indice] = (double.Parse(encabezado["recalcan"].ToString()) - double.Parse(his_recalcan[auxiliar_indice])) + double.Parse(his_recalcan[auxiliar_indice]);
            //                                his_recsanea_aux[auxiliar_indice] = (double.Parse(encabezado["recsanea"].ToString()) - double.Parse(his_recsanea[auxiliar_indice])) + double.Parse(his_recsanea[auxiliar_indice]);
            //                                his_pagua[auxiliar_indice] = (double.Parse(his_pagua[auxiliar_indice]) + double.Parse(his_rezagua[auxiliar_indice])).ToString();
            //                                his_palcan[auxiliar_indice] = (double.Parse(his_palcan[auxiliar_indice]) + double.Parse(his_rezalcan[auxiliar_indice])).ToString();
            //                                his_psanea[auxiliar_indice] = (double.Parse(his_psanea[auxiliar_indice]) + double.Parse(his_rezsanea[auxiliar_indice])).ToString();
            //                                sinp = false;
            //                            }
            //                            else
            //                            {
            //                                his_recagua_aux[auxiliar_indice] = (double.Parse(encabezado["recagua"].ToString()) - double.Parse(his_recagua[auxiliar_indice]));
            //                                his_recalcan_aux[auxiliar_indice] = (double.Parse(encabezado["recalcan"].ToString()) - double.Parse(his_recalcan[auxiliar_indice]));
            //                                his_recsanea_aux[auxiliar_indice] = (double.Parse(encabezado["recsanea"].ToString()) - double.Parse(his_recsanea[auxiliar_indice]));
            //                            }

            //                            if (his_recagua_aux[auxiliar_indice] < 0)
            //                            {
            //                                for (int a = auxiliar_indice; a < his_recagua_aux.Length - 1; )
            //                                {
            //                                    if (double.Parse(his_precagua[a]) == 0)
            //                                    {
            //                                        a++;
            //                                    }
            //                                    else
            //                                    {
            //                                        his_recagua_aux[auxiliar_indice] += double.Parse(his_precagua[a]);
            //                                        break;
            //                                    }
            //                                }
            //                            }
            //                            if (his_recalcan_aux[auxiliar_indice] < 0)
            //                            {
            //                                for (int a = auxiliar_indice; a < his_recalcan_aux.Length - 1; )
            //                                {
            //                                    if (double.Parse(his_precalcan[a]) == 0)
            //                                    {
            //                                        a++;
            //                                    }
            //                                    else
            //                                    {
            //                                        his_recalcan_aux[auxiliar_indice] += double.Parse(his_precalcan[a]);
            //                                        break;
            //                                    }
            //                                }
            //                            }
            //                            if (his_recsanea_aux[auxiliar_indice] < 0)
            //                            {
            //                                for (int a = auxiliar_indice; a < his_recsanea_aux.Length - 1; )
            //                                {
            //                                    if (double.Parse(his_precsanea[a]) == 0)
            //                                    {
            //                                        a++;
            //                                    }
            //                                    else
            //                                    {
            //                                        his_recsanea_aux[auxiliar_indice] += double.Parse(his_precsanea[a]);
            //                                        break;
            //                                    }
            //                                }
            //                            }

            //                            aux_recagua[auxiliar_indice] = his_recagua_aux[auxiliar_indice];
            //                            aux_recalcan[auxiliar_indice] = his_recalcan_aux[auxiliar_indice];
            //                            aux_recsanea[auxiliar_indice] = his_recsanea_aux[auxiliar_indice];
            //                            his_crbomb_aux[auxiliar_indice] = (float.Parse(his_crbomb[auxiliar_indice]) - crbomb).ToString();
            //                            aux_crbomb[auxiliar_indice] = float.Parse(his_crbomb_aux[auxiliar_indice]);
            //                            crbomb = float.Parse(his_crbomb[auxiliar_indice]);


            //                            fac_Total_Importe[auxiliar_indice] = float.Parse(his_pagua[auxiliar_indice]) + float.Parse(his_palcan[auxiliar_indice]) + float.Parse(his_psanea[auxiliar_indice]) + float.Parse(his_crbomb_aux[auxiliar_indice]) + his_recagua_aux[auxiliar_indice] + his_recalcan_aux[auxiliar_indice] + his_recsanea_aux[auxiliar_indice];
            //                            if (float.Parse(his_iva[auxiliar_indice]) == 0) //no tiene datos en recivos
            //                            {
            //                                aux_iva_agua[auxiliar_indice] = 0;
            //                                aux_iva_alcan[auxiliar_indice] = 0;
            //                                aux_iva_sanea[auxiliar_indice] = 0;

            //                            }
            //                            else
            //                            {
            //                                if (bandera)
            //                                {
            //                                    aux_iva_agua[auxiliar_indice] = 0;
            //                                    aux_iva_alcan[auxiliar_indice] = double.Parse(his_iva[auxiliar_indice]) / 2;
            //                                    aux_iva_sanea[auxiliar_indice] = double.Parse(his_iva[auxiliar_indice]) / 2;
            //                                    iva_acumulado = double.Parse(his_iva[auxiliar_indice]);
            //                                    bandera = false;
            //                                }
            //                                else
            //                                {
            //                                    if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //                                    {
            //                                        aux_iva_agua[auxiliar_indice] = double.Parse(his_pagua[auxiliar_indice]) * 0.16;
            //                                        aux_iva_alcan[auxiliar_indice] = double.Parse(his_palcan[auxiliar_indice]) * 0.16;
            //                                        aux_iva_sanea[auxiliar_indice] = double.Parse(his_psanea[auxiliar_indice]) * 0.16;
            //                                    }
            //                                    else
            //                                    {
            //                                        aux_iva_agua[auxiliar_indice] = 0;
            //                                        aux_iva_alcan[auxiliar_indice] = double.Parse(his_palcan[auxiliar_indice]) * 0.16;
            //                                        aux_iva_sanea[auxiliar_indice] = double.Parse(his_psanea[auxiliar_indice]) * 0.16;
            //                                    }
            //                                    if (double.Parse(his_iva[auxiliar_indice]) <= iva_acumulado)
            //                                    {
            //                                        for (int a = auxiliar_indice + 1; a < his_piva.Length; )
            //                                        {
            //                                            if (float.Parse(his_piva[a]) == 0)
            //                                            {
            //                                                a++;
            //                                            }
            //                                            else
            //                                            {
            //                                                ajuste_iva = double.Parse(his_piva[a]);
            //                                                break;
            //                                            }
            //                                        }
            //                                        ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - iva_acumulado + ajuste_iva;
            //                                    }
            //                                    else
            //                                    {
            //                                        ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - iva_acumulado;

            //                                    }
            //                                    ajuste_iva = ajuste_iva - ((aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice]));
            //                                    aux_iva_sanea[auxiliar_indice] += ajuste_iva;
            //                                    if (aux_iva_sanea[auxiliar_indice] < 0)
            //                                    {
            //                                        ajuste_iva = aux_iva_sanea[auxiliar_indice];
            //                                        aux_iva_sanea[auxiliar_indice] = 0;
            //                                        aux_iva_alcan[auxiliar_indice] += ajuste_iva;

            //                                    }
            //                                    if (aux_iva_alcan[auxiliar_indice] < 0)
            //                                    {
            //                                        ajuste_iva = aux_iva_alcan[auxiliar_indice];
            //                                        aux_iva_alcan[auxiliar_indice] = 0;
            //                                        aux_iva_agua[auxiliar_indice] += ajuste_iva;
            //                                    }
            //                                    if (aux_iva_agua[auxiliar_indice] < 0)
            //                                    {
            //                                        aux_iva_agua[auxiliar_indice] = 0;
            //                                    }
            //                                    iva_acumulado = double.Parse(his_iva[auxiliar_indice]);

            //                                }

            //                            }

            //                            //// ----------------------------------- CALCULAR IVA ------------------------------------
            //                            //if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //                            //{
            //                            //    aux_iva_agua[auxiliar_indice] = double.Parse(his_pagua[auxiliar_indice]) * 0.16;
            //                            //}
            //                            //else
            //                            //{
            //                            //    aux_iva_agua[auxiliar_indice] = 0f;
            //                            //}
            //                            //aux_iva_alcan[auxiliar_indice] = double.Parse(his_palcan[auxiliar_indice]) * 0.16;
            //                            //aux_iva_sanea[auxiliar_indice] = double.Parse(his_psanea[auxiliar_indice]) * 0.16;

            //                            //if (float.Parse(his_iva[auxiliar_indice]) == 0)
            //                            //{
            //                            //    iva_acumulado += aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice] - double.Parse(his_piva[auxiliar_indice]);
            //                            //    bandera = false;
            //                            //}
            //                            //else
            //                            //{
            //                            //    if (iva_acumulado != 0)
            //                            //    {
            //                            //        bandera = false;
            //                            //        ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - iva_acumulado + double.Parse(his_piva[auxiliar_indice]); ;
            //                            //        ajuste_iva = ajuste_iva - ((aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice]));
            //                            //        aux_iva_sanea[auxiliar_indice] += ajuste_iva;
            //                            //        if (aux_iva_sanea[auxiliar_indice] < 0)
            //                            //        {
            //                            //            ajuste_iva = aux_iva_sanea[auxiliar_indice];
            //                            //            aux_iva_sanea[auxiliar_indice] = 0;
            //                            //            aux_iva_alcan[auxiliar_indice] += ajuste_iva;

            //                            //        }
            //                            //        if (aux_iva_alcan[auxiliar_indice] < 0)
            //                            //        {
            //                            //            ajuste_iva = aux_iva_alcan[auxiliar_indice];
            //                            //            aux_iva_alcan[auxiliar_indice] = 0;
            //                            //            aux_iva_agua[auxiliar_indice] += ajuste_iva;
            //                            //        }
            //                            //        if (aux_iva_agua[auxiliar_indice] < 0)
            //                            //        {
            //                            //            aux_iva_agua[auxiliar_indice] = 0;
            //                            //        }
            //                            //        iva_acumulado = 0;
            //                            //    }
            //                            //    else
            //                            //    {
            //                            //        if (bandera)
            //                            //        {
            //                            //            bandera = false;
            //                            //            ajuste_iva = double.Parse(his_iva[auxiliar_indice]);
            //                            //            ajuste_iva = ajuste_iva - ((aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice]));
            //                            //            aux_iva_sanea[auxiliar_indice] += ajuste_iva;
            //                            //            if (aux_iva_sanea[auxiliar_indice] < 0)
            //                            //            {
            //                            //                ajuste_iva = aux_iva_sanea[auxiliar_indice];
            //                            //                aux_iva_sanea[auxiliar_indice] = 0;
            //                            //                aux_iva_alcan[auxiliar_indice] += ajuste_iva;

            //                            //            }
            //                            //            if (aux_iva_alcan[auxiliar_indice] < 0)
            //                            //            {
            //                            //                ajuste_iva = aux_iva_alcan[auxiliar_indice];
            //                            //                aux_iva_alcan[auxiliar_indice] = 0;
            //                            //                aux_iva_agua[auxiliar_indice] += ajuste_iva;
            //                            //            }
            //                            //            if (aux_iva_agua[auxiliar_indice] < 0)
            //                            //            {
            //                            //                aux_iva_agua[auxiliar_indice] = 0;
            //                            //            }
            //                            //            iva_acumulado = double.Parse(his_iva[auxiliar_indice]);

            //                            //        }

            //                            //        else
            //                            //        {
            //                            //            for (int a = auxiliar_indice + 1; a < his_piva.Length; )
            //                            //            {
            //                            //                if (float.Parse(his_piva[a]) == 0)
            //                            //                {
            //                            //                    a++;
            //                            //                }
            //                            //                else
            //                            //                {
            //                            //                    ajuste_iva = double.Parse(his_piva[a]);
            //                            //                    break;
            //                            //                }
            //                            //            }

            //                            //            if (double.Parse(his_iva[auxiliar_indice])>=double.Parse(his_iva[auxiliar_indice + 1])){
            //                            //                ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - double.Parse(his_iva[auxiliar_indice + 1]) ;
            //                            //                }else{
            //                            //                ajuste_iva = double.Parse(his_iva[auxiliar_indice]) - double.Parse(his_iva[auxiliar_indice + 1]) + ajuste_iva;
            //                            //                }
            //                            //            ajuste_iva = ajuste_iva - ((aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice]));
            //                            //            aux_iva_sanea[auxiliar_indice] += ajuste_iva;
            //                            //            if (aux_iva_sanea[auxiliar_indice] < 0)
            //                            //            {
            //                            //                ajuste_iva = aux_iva_sanea[auxiliar_indice];
            //                            //                aux_iva_sanea[auxiliar_indice] = 0;
            //                            //                aux_iva_alcan[auxiliar_indice] += ajuste_iva;

            //                            //            }
            //                            //            if (aux_iva_alcan[auxiliar_indice] < 0)
            //                            //            {
            //                            //                ajuste_iva = aux_iva_alcan[auxiliar_indice];
            //                            //                aux_iva_alcan[auxiliar_indice] = 0;
            //                            //                aux_iva_agua[auxiliar_indice] += ajuste_iva;
            //                            //            }
            //                            //            if (aux_iva_agua[auxiliar_indice] < 0)
            //                            //            {
            //                            //                aux_iva_agua[auxiliar_indice] = 0;
            //                            //            }
            //                            //            iva_acumulado = double.Parse(his_iva[auxiliar_indice]);
            //                            //        }
            //                            //    }

            //                            //}
            //                            fac_Total_IVA[auxiliar_indice] = aux_iva_agua[auxiliar_indice] + aux_iva_alcan[auxiliar_indice] + aux_iva_sanea[auxiliar_indice];
            //                            iva = double.Parse(his_iva[auxiliar_indice]);
            //                            his_iva_agua[auxiliar_indice] = aux_iva_agua[auxiliar_indice];
            //                            his_iva_alcan[auxiliar_indice] = aux_iva_alcan[auxiliar_indice];
            //                            his_iva_sanea[auxiliar_indice] = aux_iva_sanea[auxiliar_indice];
            //                            // -------------------------------- FIN CALCULAR IVA --------------------------------------

            //                            fac_Total_Pagado[auxiliar_indice] = fac_Total_Importe[auxiliar_indice] + fac_Total_IVA[auxiliar_indice];
            //                            fac_Saldo[auxiliar_indice] = fac_Total_Pagado[auxiliar_indice] - fac_Total_Abono[auxiliar_indice];

            //                            prezagua = double.Parse(his_prezagua[auxiliar_indice].ToString()) + double.Parse(his_ppagua[auxiliar_indice]);
            //                            prezalcan = double.Parse(his_prezalcan[auxiliar_indice].ToString()) + double.Parse(his_ppalcan[auxiliar_indice]);
            //                            prezsanea = double.Parse(his_prezsanea[auxiliar_indice].ToString()) + double.Parse(his_ppsanea[auxiliar_indice]);
            //                            precagua = double.Parse(his_precagua[auxiliar_indice].ToString());
            //                            precalcan = double.Parse(his_precalcan[auxiliar_indice].ToString());
            //                            precsanea = double.Parse(his_precsanea[auxiliar_indice].ToString());
            //                            pcrbomb = float.Parse(his_pcrbomb[auxiliar_indice].ToString());
            //                            piva = double.Parse(his_piva[auxiliar_indice].ToString());
            //                            aux_crbomb[auxiliar_indice] = float.Parse(his_crbomb[auxiliar_indice]) - crbomb;
            //                            crbomb = 0;

            //                            aux_i = indice - 1;
            //                            for (; aux_i >= 0; aux_i--)
            //                            {
            //                                if (prezagua == 0 && prezalcan == 0 && prezsanea == 0 && precagua == 0 && precalcan == 0 && precsanea == 0 && pcrbomb == 0 && piva == 0)
            //                                {
            //                                    break;
            //                                }

            //                                if (!string.IsNullOrEmpty(fac_Estado[aux_i]))
            //                                {
            //                                    if (fac_Estado[aux_i].Trim() == "REZAGADO" || fac_Estado[aux_i].Trim() == "PARCIAL")
            //                                    {

            //                                        /////////////////////INICIA_PAGUA///////////////////////
            //                                        if (aux_pagua[aux_i] != 0)
            //                                        {
            //                                            if (aux_pagua[aux_i] <= prezagua)
            //                                            {
            //                                                pago_rezagua[aux_i] += aux_pagua[aux_i];
            //                                                prezagua -= aux_pagua[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_pagua[aux_i];
            //                                                aux_pagua[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {
            //                                                pago_rezagua[aux_i] += prezagua;
            //                                                aux_pagua[aux_i] = aux_pagua[aux_i] - prezagua;
            //                                                fac_Total_Abono[aux_i] += prezagua;
            //                                                prezagua = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////Fin_PAGUA///////////////////////
            //                                        /////////////////////INICIA_Palcan///////////////////////
            //                                        if (aux_palcan[aux_i] != 0)
            //                                        {
            //                                            if (aux_palcan[aux_i] <= prezalcan)
            //                                            {
            //                                                pago_rezalcan[aux_i] += aux_palcan[aux_i];
            //                                                prezalcan -= aux_palcan[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_palcan[aux_i];
            //                                                aux_palcan[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {

            //                                                pago_rezalcan[aux_i] += prezalcan;
            //                                                aux_palcan[aux_i] -= prezalcan;
            //                                                fac_Total_Abono[aux_i] += prezalcan;
            //                                                prezalcan = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////Fin_PAlcan///////////////////////
            //                                        /////////////////////INICIA_Psanea///////////////////////
            //                                        if (aux_psanea[aux_i] != 0)
            //                                        {
            //                                            if (aux_psanea[aux_i] <= prezsanea)
            //                                            {
            //                                                pago_rezsanea[aux_i] += aux_psanea[aux_i];
            //                                                prezsanea -= aux_psanea[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_psanea[aux_i];
            //                                                aux_psanea[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {
            //                                                pago_rezsanea[aux_i] += prezsanea;
            //                                                aux_psanea[aux_i] -= prezsanea;
            //                                                fac_Total_Abono[aux_i] += prezsanea;
            //                                                prezsanea = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////Fin_Psanea///////////////////////
            //                                        /////////////////////INICIA_recagua///////////////////////
            //                                        if (aux_recagua[aux_i] != 0)
            //                                        {
            //                                            if (aux_recagua[aux_i] <= precagua)
            //                                            {
            //                                                pago_recagua[aux_i] += aux_recagua[aux_i];
            //                                                precagua -= aux_recagua[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_recagua[aux_i];
            //                                                aux_recagua[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {
            //                                                pago_recagua[aux_i] += precagua;
            //                                                aux_recagua[aux_i] -= precagua;
            //                                                fac_Total_Abono[aux_i] += precagua;
            //                                                precagua = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////FIN_rec_agua///////////////////////
            //                                        /////////////////////INICIA_recalcan///////////////////////
            //                                        if (aux_recalcan[aux_i] != 0)
            //                                        {
            //                                            if (aux_recalcan[aux_i] <= precalcan)
            //                                            {
            //                                                pago_recalcan[aux_i] += aux_recalcan[aux_i];
            //                                                precalcan -= aux_recalcan[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_recalcan[aux_i];
            //                                                aux_recalcan[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {
            //                                                pago_recalcan[aux_i] += precalcan;
            //                                                aux_recalcan[aux_i] -= precalcan;
            //                                                fac_Total_Abono[aux_i] += precalcan;
            //                                                precalcan = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////fin_recalcan///////////////////////
            //                                        /////////////////////fin_recsanea///////////////////////
            //                                        if (aux_recsanea[aux_i] != 0)
            //                                        {
            //                                            if (aux_recsanea[aux_i] <= precsanea)
            //                                            {
            //                                                pago_recsanea[aux_i] += aux_recsanea[aux_i];
            //                                                precsanea -= aux_recsanea[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_recsanea[aux_i];
            //                                                aux_recsanea[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {
            //                                                pago_recsanea[aux_i] += precsanea;
            //                                                aux_recsanea[aux_i] -= precsanea;
            //                                                fac_Total_Abono[aux_i] += precsanea;
            //                                                precsanea = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////fin_recsanea///////////////////////
            //                                        /////////////////////inicia_iva_agua///////////////////////
            //                                        if (aux_iva_agua[aux_i] != 0)
            //                                        {
            //                                            if (aux_iva_agua[aux_i] <= piva)
            //                                            {
            //                                                pago_iva_agua[aux_i] += aux_iva_agua[aux_i];
            //                                                piva -= aux_iva_agua[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_iva_agua[aux_i];
            //                                                aux_iva_agua[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {
            //                                                pago_iva_agua[aux_i] += piva;
            //                                                aux_iva_agua[aux_i] -= piva;
            //                                                fac_Total_Abono[aux_i] += piva;
            //                                                piva = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////fin_iva_agua///////////////////////
            //                                        /////////////////////inicia_iva_alcan///////////////////////
            //                                        if (aux_iva_alcan[aux_i] != 0)
            //                                        {
            //                                            if (aux_iva_alcan[aux_i] <= piva)
            //                                            {
            //                                                pago_iva_alcan[aux_i] += aux_iva_alcan[aux_i];
            //                                                piva -= aux_iva_alcan[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_iva_alcan[aux_i];
            //                                                aux_iva_alcan[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {
            //                                                pago_iva_alcan[aux_i] += piva;
            //                                                aux_iva_alcan[aux_i] -= piva;
            //                                                fac_Total_Abono[aux_i] += piva;
            //                                                piva = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////fin_iva_alcan///////////////////////
            //                                        /////////////////////inicia_iva_sanea///////////////////////
            //                                        if (aux_iva_sanea[aux_i] != 0)
            //                                        {
            //                                            if (aux_iva_sanea[aux_i] <= piva)
            //                                            {
            //                                                pago_iva_sanea[aux_i] += aux_iva_sanea[aux_i];
            //                                                piva -= aux_iva_sanea[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_iva_sanea[aux_i];
            //                                                aux_iva_sanea[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {
            //                                                pago_iva_sanea[aux_i] += piva;
            //                                                aux_iva_sanea[aux_i] -= piva;
            //                                                fac_Total_Abono[aux_i] += piva;
            //                                                piva = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////fin_iva_sanea///////////////////////
            //                                        /////////////////////inicia_pcrbomb///////////////////////
            //                                        if (aux_crbomb[aux_i] != 0)
            //                                        {
            //                                            if (aux_crbomb[aux_i] <= pcrbomb)
            //                                            {
            //                                                pago_crbomb[aux_i] += aux_crbomb[aux_i];
            //                                                pcrbomb -= aux_crbomb[aux_i];
            //                                                fac_Total_Abono[aux_i] += aux_crbomb[aux_i];
            //                                                aux_crbomb[aux_i] = 0;
            //                                            }
            //                                            else
            //                                            {
            //                                                pago_crbomb[aux_i] += pcrbomb;
            //                                                aux_crbomb[aux_i] -= pcrbomb;
            //                                                fac_Total_Abono[aux_i] += pcrbomb;
            //                                                pcrbomb = 0;
            //                                            }
            //                                        }
            //                                        /////////////////////fin_pcrbomb///////////////////////
            //                                    }
            //                                }
            //                                fac_Saldo[aux_i] = fac_Total_Pagado[aux_i] - fac_Total_Abono[aux_i];
            //                                if (!string.IsNullOrEmpty(fac_Estado[aux_i]))
            //                                {
            //                                    if ((aux_pagua[aux_i] + aux_palcan[aux_i] + aux_psanea[aux_i] + aux_recagua[aux_i] + aux_recalcan[aux_i] + aux_recsanea[aux_i]
            //                                                     + aux_iva_agua[aux_i] + aux_iva_alcan[aux_i] + aux_iva_sanea[aux_i]) == 0)
            //                                    {
            //                                        fac_Estado[aux_i] = "PAGADO";
            //                                    }
            //                                }
            //                            }/////////////////////fin del for para pagar
            //                        }
            //                        break;
            //                }////////////fin del switch
            //            }

            //            actual_pagua = float.Parse(encabezado["pagua"].ToString());
            //            actual_palcan = float.Parse(encabezado["palcan"].ToString());
            //            actual_psanea = float.Parse(encabezado["psanea"].ToString());
            //            if (Estatus != "PARCIAL" && Estatus != "REZAGADO")
            //            {
            //                actual_iva = double.Parse(encabezado["iva"].ToString());
            //            }
            //            else
            //            {

            //                actual_iva = double.Parse(encabezado["iva"].ToString()) - double.Parse(his_iva[0]);
            //            }
            //            if (actual_iva < 0)
            //            {
            //                for (int a = 0; a < his_piva.Length - 1; )
            //                {
            //                    if (double.Parse(his_piva[a]) == 0)
            //                    {
            //                        a++;
            //                    }
            //                    else
            //                    {
            //                        actual_iva += double.Parse(his_piva[a]);
            //                        break;
            //                    }
            //                }
            //            }
            //            if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //            {
            //                actual_iva_agua = double.Parse(encabezado["pagua"].ToString()) * 0.16;
            //            }
            //            else
            //            {
            //                actual_iva_agua = 0;
            //            }
            //            actual_iva_alcan = double.Parse(encabezado["palcan"].ToString()) * 0.16;
            //            actual_iva_sanea = double.Parse(encabezado["psanea"].ToString()) * 0.16;

            //            if (actual_iva == 0)
            //            {
            //                actual_iva = actual_iva_agua + actual_iva_alcan + actual_iva_sanea;
            //            }
            //            else
            //            {
            //                ajuste_iva = actual_iva - (actual_iva_agua + actual_iva_alcan + actual_iva_sanea);
            //                actual_iva_sanea += ajuste_iva;

            //                if (actual_iva_sanea < 0)
            //                {
            //                    actual_iva_alcan += actual_iva_sanea;
            //                    actual_iva_sanea = 0;
            //                }
            //                if (actual_iva_alcan < 0)
            //                {
            //                    actual_iva_agua += actual_iva_alcan;
            //                    actual_iva_alcan = 0;
            //                }
            //                if (actual_iva_agua < 0)
            //                {
            //                    actual_iva_agua = 0;
            //                }
            //            }
            //            if (Estatus != "PARCIAL" && Estatus != "REZAGADO")
            //            {
            //                actual_crbomb = double.Parse(encabezado["crbomb"].ToString());
            //            }
            //            else
            //            {
            //                actual_crbomb = float.Parse(encabezado["crbomb"].ToString()) - float.Parse(his_crbomb[0]);
            //                if (actual_crbomb <= 0)
            //                {
            //                    for (int a = 0; a < his_pcrbomb.Length; )
            //                    {
            //                        if (float.Parse(his_pcrbomb[a]) == 0)
            //                        {
            //                            a++;
            //                        }
            //                        else
            //                        {
            //                            actual_crbomb += float.Parse(his_pcrbomb[a]);
            //                            break;
            //                        }
            //                    }
            //                }
            //            }



            //            actual_ppagua = float.Parse(encabezado["ppagua"].ToString());
            //            actual_ppalcan = float.Parse(encabezado["ppalcan"].ToString());
            //            actual_ppsanea = float.Parse(encabezado["ppsanea"].ToString());
            //            actual_prezagua = double.Parse(encabezado["prezagua"].ToString());
            //            actual_prezalcan = double.Parse(encabezado["prezalcan"].ToString());
            //            actual_prezsanea = double.Parse(encabezado["prezsanea"].ToString());
            //            actual_piva = double.Parse(encabezado["piva"].ToString());
            //            actual_pcrbomb = float.Parse(encabezado["pcrbomb"].ToString());

            //            prezagua = actual_prezagua;
            //            prezalcan = actual_prezalcan;
            //            prezsanea = actual_prezsanea;
            //            precagua = double.Parse(encabezado["precagua"].ToString());
            //            precalcan = double.Parse(encabezado["precalcan"].ToString());
            //            precsanea = double.Parse(encabezado["precsanea"].ToString());
            //            pcrbomb = actual_pcrbomb;
            //            piva = actual_piva;

            //            aux_i = indice - 1;
            //            for (; aux_i >= 0; aux_i--)
            //            {
            //                if (prezagua == 0 && prezalcan == 0 && prezsanea == 0 && precagua == 0 && precalcan == 0 && precsanea == 0 && pcrbomb == 0 && piva == 0)
            //                {
            //                    break;
            //                }

            //                if (!string.IsNullOrEmpty(fac_Estado[aux_i]))
            //                {
            //                    if (fac_Estado[aux_i].Trim() == "REZAGADO" || fac_Estado[aux_i].Trim() == "PARCIAL")
            //                    {

            //                        /////////////////////INICIA_PAGUA///////////////////////
            //                        if (aux_pagua[aux_i] != 0)
            //                        {
            //                            if (aux_pagua[aux_i] <= prezagua)
            //                            {
            //                                pago_rezagua[aux_i] += aux_pagua[aux_i];
            //                                prezagua -= aux_pagua[aux_i];
            //                                fac_Total_Abono[aux_i] += aux_pagua[aux_i];
            //                                aux_pagua[aux_i] = 0;
            //                            }
            //                            else
            //                            {
            //                                pago_rezagua[aux_i] += prezagua;
            //                                aux_pagua[aux_i] = aux_pagua[aux_i] - prezagua;
            //                                fac_Total_Abono[aux_i] += prezagua;
            //                                prezagua = 0;
            //                            }
            //                        }
            //                        /////////////////////Fin_PAGUA///////////////////////
            //                        /////////////////////INICIA_Palcan///////////////////////
            //                        if (aux_palcan[aux_i] != 0)
            //                        {
            //                            if (aux_palcan[aux_i] <= prezalcan)
            //                            {
            //                                pago_rezalcan[aux_i] += aux_palcan[aux_i];
            //                                prezalcan -= aux_palcan[aux_i];
            //                                fac_Total_Abono[aux_i] += aux_palcan[aux_i];
            //                                aux_palcan[aux_i] = 0;
            //                            }
            //                            else
            //                            {

            //                                pago_rezalcan[aux_i] += prezalcan;
            //                                aux_palcan[aux_i] -= prezalcan;
            //                                fac_Total_Abono[aux_i] += prezalcan;
            //                                prezalcan = 0;
            //                            }
            //                        }
            //                        /////////////////////Fin_PAlcan///////////////////////
            //                        /////////////////////INICIA_Psanea///////////////////////
            //                        if (aux_psanea[aux_i] != 0)
            //                        {
            //                            if (aux_psanea[aux_i] <= prezsanea)
            //                            {
            //                                pago_rezsanea[aux_i] += aux_psanea[aux_i];
            //                                prezsanea -= aux_psanea[aux_i];
            //                                fac_Total_Abono[aux_i] += aux_psanea[aux_i];
            //                                aux_psanea[aux_i] = 0;
            //                            }
            //                            else
            //                            {
            //                                pago_rezsanea[aux_i] += prezsanea;
            //                                aux_psanea[aux_i] -= prezsanea;
            //                                fac_Total_Abono[aux_i] += prezsanea;
            //                                prezsanea = 0;
            //                            }
            //                        }
            //                        /////////////////////Fin_Psanea///////////////////////
            //                        /////////////////////INICIA_recagua///////////////////////
            //                        if (aux_recagua[aux_i] != 0)
            //                        {
            //                            if (aux_recagua[aux_i] <= precagua)
            //                            {
            //                                pago_recagua[aux_i] += aux_recagua[aux_i];
            //                                precagua -= aux_recagua[aux_i];
            //                                fac_Total_Abono[aux_i] += aux_recagua[aux_i];
            //                                aux_recagua[aux_i] = 0;
            //                            }
            //                            else
            //                            {
            //                                pago_recagua[aux_i] += precagua;
            //                                aux_recagua[aux_i] -= precagua;
            //                                fac_Total_Abono[aux_i] += precagua;
            //                                precagua = 0;
            //                            }
            //                        }
            //                        /////////////////////FIN_rec_agua///////////////////////
            //                        /////////////////////INICIA_recalcan///////////////////////
            //                        if (aux_recalcan[aux_i] != 0)
            //                        {
            //                            if (aux_recalcan[aux_i] <= precalcan)
            //                            {
            //                                pago_recalcan[aux_i] += aux_recalcan[aux_i];
            //                                precalcan -= aux_recalcan[aux_i];
            //                                fac_Total_Abono[aux_i] += aux_recalcan[aux_i];
            //                                aux_recalcan[aux_i] = 0;
            //                            }
            //                            else
            //                            {
            //                                pago_recalcan[aux_i] += precalcan;
            //                                aux_recalcan[aux_i] -= precalcan;
            //                                fac_Total_Abono[aux_i] += precalcan;
            //                                precalcan = 0;
            //                            }
            //                        }
            //                        /////////////////////fin_recalcan///////////////////////
            //                        /////////////////////fin_recsanea///////////////////////
            //                        if (aux_recsanea[aux_i] != 0)
            //                        {
            //                            if (aux_recsanea[aux_i] <= precsanea)
            //                            {
            //                                pago_recsanea[aux_i] += aux_recsanea[aux_i];
            //                                precsanea -= aux_recsanea[aux_i];
            //                                fac_Total_Abono[aux_i] += aux_recsanea[aux_i];
            //                                aux_recsanea[aux_i] = 0;
            //                            }
            //                            else
            //                            {
            //                                pago_recsanea[aux_i] += precsanea;
            //                                aux_recsanea[aux_i] -= precsanea;
            //                                fac_Total_Abono[aux_i] += precsanea;
            //                                precsanea = 0;
            //                            }
            //                        }
            //                        /////////////////////fin_recsanea///////////////////////
            //                        /////////////////////inicia_iva_agua///////////////////////
            //                        if (aux_iva_agua[aux_i] != 0)
            //                        {
            //                            if (aux_iva_agua[aux_i] <= piva)
            //                            {
            //                                pago_iva_agua[aux_i] += aux_iva_agua[aux_i];
            //                                piva -= aux_iva_agua[aux_i];
            //                                fac_Total_Abono[aux_i] += aux_iva_agua[aux_i];
            //                                aux_iva_agua[aux_i] = 0;
            //                            }
            //                            else
            //                            {
            //                                pago_iva_agua[aux_i] += piva;
            //                                aux_iva_agua[aux_i] -= piva;
            //                                fac_Total_Abono[aux_i] += piva;
            //                                piva = 0;
            //                            }
            //                        }
            //                        /////////////////////fin_iva_agua///////////////////////
            //                        /////////////////////inicia_iva_alcan///////////////////////
            //                        if (aux_iva_alcan[aux_i] != 0)
            //                        {
            //                            if (aux_iva_alcan[aux_i] <= piva)
            //                            {
            //                                pago_iva_alcan[aux_i] += aux_iva_alcan[aux_i];
            //                                piva -= aux_iva_alcan[aux_i];
            //                                fac_Total_Abono[aux_i] += aux_iva_alcan[aux_i];
            //                                aux_iva_alcan[aux_i] = 0;
            //                            }
            //                            else
            //                            {
            //                                pago_iva_alcan[aux_i] += piva;
            //                                aux_iva_alcan[aux_i] -= piva;
            //                                fac_Total_Abono[aux_i] += piva;
            //                                piva = 0;
            //                            }
            //                        }
            //                        /////////////////////fin_iva_alcan///////////////////////
            //                        /////////////////////inicia_iva_sanea///////////////////////
            //                        if (aux_iva_sanea[aux_i] != 0)
            //                        {
            //                            if (aux_iva_sanea[aux_i] <= piva)
            //                            {
            //                                pago_iva_sanea[aux_i] += aux_iva_sanea[aux_i];
            //                                piva -= aux_iva_sanea[aux_i];
            //                                fac_Total_Abono[aux_i] += aux_iva_sanea[aux_i];
            //                                aux_iva_sanea[aux_i] = 0;
            //                            }
            //                            else
            //                            {
            //                                pago_iva_sanea[aux_i] += piva;
            //                                aux_iva_sanea[aux_i] -= piva;
            //                                fac_Total_Abono[aux_i] += piva;
            //                                piva = 0;
            //                            }
            //                        }
            //                        /////////////////////fin_iva_sanea///////////////////////
            //                        /////////////////////inicia_pcrbomb///////////////////////
            //                        if (aux_crbomb[aux_i] != 0)
            //                        {
            //                            if (aux_crbomb[aux_i] <= pcrbomb)
            //                            {
            //                                pago_crbomb[aux_i] += aux_crbomb[aux_i];
            //                                pcrbomb -= aux_crbomb[aux_i];
            //                                fac_Total_Abono[aux_i] += aux_crbomb[aux_i];
            //                                aux_crbomb[aux_i] = 0;
            //                            }
            //                            else
            //                            {
            //                                pago_crbomb[aux_i] += pcrbomb;
            //                                aux_crbomb[aux_i] -= pcrbomb;
            //                                fac_Total_Abono[aux_i] += pcrbomb;
            //                                pcrbomb = 0;
            //                            }
            //                        }
            //                        /////////////////////fin_pcrbomb///////////////////////
            //                    }
            //                }
            //                fac_Saldo[aux_i] = fac_Total_Pagado[aux_i] - fac_Total_Abono[aux_i];
            //                if (!string.IsNullOrEmpty(fac_Estado[aux_i]))
            //                {
            //                    if ((aux_pagua[aux_i] + aux_palcan[aux_i] + aux_psanea[aux_i] + aux_recagua[aux_i] + aux_recalcan[aux_i] + aux_recsanea[aux_i]
            //                                     + aux_iva_agua[aux_i] + aux_iva_alcan[aux_i] + aux_iva_sanea[aux_i]) == 0)
            //                    {
            //                        fac_Estado[aux_i] = "PAGADO";
            //                    }
            //                }
            //            }/////////////////////fin del for para pagar



            //            if (actual_pagua <= actual_ppagua)
            //            {
            //                actual_pago_agua = actual_pagua;
            //                actual_total_abonado += actual_pagua;
            //                actual_ppagua -= actual_pagua;
            //            }
            //            else
            //            {
            //                actual_pago_agua = actual_ppagua;
            //                actual_total_abonado += actual_ppagua;
            //                actual_ppagua = 0;
            //            }
            //            if (actual_palcan <= actual_ppalcan)
            //            {
            //                actual_pago_alcan = actual_palcan;
            //                actual_total_abonado += actual_palcan;
            //                actual_ppalcan -= actual_palcan;
            //            }
            //            else
            //            {
            //                actual_pago_alcan = actual_ppalcan;
            //                actual_total_abonado += actual_ppalcan;
            //                actual_ppalcan = 0;
            //            }
            //            if (actual_psanea <= actual_ppsanea)
            //            {
            //                actual_pago_sanea = actual_psanea;
            //                actual_total_abonado += actual_psanea;
            //                actual_ppsanea -= actual_psanea;
            //            }
            //            else
            //            {
            //                actual_pago_sanea = actual_ppsanea;
            //                actual_total_abonado += actual_ppsanea;
            //                actual_ppsanea = 0;
            //            }
            //            if (actual_iva_agua <= piva)
            //            {
            //                actual_pago_iva_agua = actual_iva_agua;
            //                actual_total_abonado += actual_iva_agua;
            //                piva -= actual_iva_agua;
            //            }
            //            else
            //            {
            //                actual_pago_iva_agua = piva;
            //                actual_total_abonado += piva;
            //                piva = 0;
            //            }
            //            if (actual_iva_alcan <= piva)
            //            {
            //                actual_pago_iva_alcan = actual_iva_alcan;
            //                actual_total_abonado += actual_iva_alcan;
            //                piva -= actual_iva_alcan;
            //            }
            //            else
            //            {
            //                actual_pago_iva_alcan = piva;
            //                actual_total_abonado += piva;
            //                piva = 0;
            //            }
            //            if (actual_iva_sanea <= piva)
            //            {
            //                actual_pago_iva_sanea = actual_iva_sanea;
            //                actual_total_abonado += actual_iva_sanea;
            //                piva -= actual_iva_sanea;
            //            }
            //            else
            //            {
            //                actual_pago_iva_sanea = piva;
            //                actual_total_abonado += piva;
            //                piva = 0;
            //            }
            //            if (actual_crbomb <= pcrbomb)
            //            {
            //                actual_pago_crbomb = actual_crbomb;
            //                actual_total_abonado += actual_crbomb;
            //                pcrbomb -= actual_crbomb;
            //            }
            //            else
            //            {
            //                actual_pago_crbomb = pcrbomb;
            //                actual_total_abonado += pcrbomb;
            //                pcrbomb = 0;
            //            }
            //            actual_total_abonado = Math.Round(actual_total_abonado, 2);

            //            DataRow Dr;
            //            indice--;
            //            for (; indice >= 0; indice--)
            //            {
            //                if (!string.IsNullOrEmpty(fac_Estado[indice]))
            //                {
            //                    Dr = Dt_Datos.NewRow();

            //                    Dr["no_factura"] = fac_No_Factura[indice];
            //                    Dr["Codigo_Barras"] = fac_No_Factura[indice] + "F";
            //                    Dr["no_cuenta"] = fac_No_Cuenta[indice];
            //                    Dr["no_recibo"] = fac_No_Recibo[indice];
            //                    Dr["region_id"] = fac_Region_ID[indice];
            //                    Dr["predio_id"] = fac_Predio_ID[indice];
            //                    Dr["usuario_id"] = fac_Usuario_ID[indice];
            //                    Dr["medidor_id"] = fac_Medidor_ID[indice];
            //                    Dr["tarifa_id"] = fac_Tarifa_ID[indice];
            //                    Dr["lec_anterior"] = fac_Lectura_Anterior[indice];
            //                    Dr["lec_actual"] = fac_Lectura_Actual[indice];
            //                    Dr["consumo"] = fac_Consumo[indice];
            //                    Dr["fac_Cuota_Base"] = fac_Cuota_Base[indice];
            //                    Dr["fac_Cuata_Consumo"] = fac_Cuata_Consumo[indice];
            //                    Dr["fac_Precio_M3"] = fac_Precio_M3[indice];
            //                    Dr["fecha_inicio"] = fac_Fecha_Inicio[indice];
            //                    Dr["fecha_termino"] = fac_Fecha_Termino[indice];
            //                    Dr["fecha_limite"] = fac_Fecha_Limite[indice];
            //                    Dr["fecha_emision"] = fac_Fecha_Emicio[indice];
            //                    Dr["periodo"] = fac_Periodo[indice];
            //                    Dr["tasa_iva"] = fac_Tasa_IVA[indice];

            //                    Dr["total_importe"] = Math.Round(fac_Total_Importe[indice], 2);
            //                    Dr["total_iva"] = Math.Round(fac_Total_IVA[indice], 2);
            //                    Dr["total_pagar"] = Math.Round(fac_Total_Pagado[indice], 2);
            //                    Dr["total_abono"] = Math.Round(fac_Total_Abono[indice], 2);
            //                    Dr["fac_Saldo"] = Math.Round(fac_Saldo[indice], 2);
            //                    if (fac_Estado[indice].Trim() == "REZAGADO" || fac_Estado[indice].Trim() == "PARCIAL")
            //                    {
            //                        Dr["estado"] = "PENDIENTE";
            //                    }
            //                    else
            //                    {
            //                        Dr["estado"] = fac_Estado[indice];
            //                    }

            //                    Dr["anio"] = fac_Anio[indice];
            //                    Dr["bimestre"] = fac_Bimestre[indice];
            //                    Dr["rpu"] = fac_RPU[indice];
            //                    Dr["pagua"] = his_pagua[indice];
            //                    Dr["palcan"] = his_palcan[indice];
            //                    Dr["psanea"] = his_psanea[indice];
            //                    Dr["recagua"] = Math.Round(his_recagua_aux[indice], 2);
            //                    Dr["recalcan"] = Math.Round(his_recalcan_aux[indice], 2);
            //                    Dr["recsanea"] = Math.Round(his_recsanea_aux[indice], 2);
            //                    Dr["crbomb"] = his_crbomb_aux[indice];
            //                    Dr["iva_agua"] = Math.Round(his_iva_agua[indice], 2);
            //                    Dr["iva_alcan"] = Math.Round(his_iva_alcan[indice], 2);
            //                    Dr["iva_sanea"] = Math.Round(his_iva_sanea[indice], 2);
            //                    Dr["abono_agua"] = Math.Round(pago_rezagua[indice], 2);
            //                    Dr["abono_alcan"] = Math.Round(pago_rezalcan[indice], 2);
            //                    Dr["abonosanea"] = Math.Round(pago_rezsanea[indice], 2);
            //                    Dr["abono_recagua"] = Math.Round(pago_recagua[indice], 2);
            //                    Dr["abono_recalcan"] = Math.Round(pago_recalcan[indice], 2);
            //                    Dr["abono_recsanea"] = Math.Round(pago_recsanea[indice], 2);
            //                    Dr["abono_crbomb"] = Math.Round(pago_crbomb[indice], 2);
            //                    Dr["abono_IVA_agua"] = Math.Round(pago_iva_agua[indice], 2);
            //                    Dr["abono_IVA_alcan"] = Math.Round(pago_iva_alcan[indice], 2);
            //                    Dr["abono_IVA_sanea"] = Math.Round(pago_iva_sanea[indice], 2);
            //                    Dr["anticipo"] = 0;
            //                    Dr["Fecha_Pago"] = his_fechapago[indice];

            //                    Dt_Datos.Rows.Add(Dr);
            //                }
            //            }

            //            // -- Aqui inicia el llenado de las columnas "cuota_base", "cuota_consumo", "precio_m3" -- \\
                        


            //            Dr = Dt_Datos.NewRow();
            //            automatic_id = "0000000000" + (no_Reg + x).ToString();
            //            automatic_id = automatic_id.Substring(automatic_id.Length - 10, 10);
            //            x++;

            //            Dr["no_factura"] = automatic_id;
            //            Dr["Codigo_Barras"] = automatic_id + "F";
            //            Dr["no_cuenta"] = encabezado["cuenta"].ToString();
            //            Dr["no_recibo"] = encabezado["foliorecib"];
            //            Dr["region_id"] = encabezado["region_ID"];
            //            Dr["predio_id"] = encabezado["predio_ID"];
            //            Dr["usuario_id"] = encabezado["usuario_ID"];
            //            Dr["medidor_id"] = encabezado["nummedidor"];
            //            Dr["tarifa_id"] = encabezado["tarifa_ID"];
            //            Dr["lec_anterior"] = encabezado["lecanterior"];
            //            Dr["lec_actual"] = encabezado["lecactual"];
            //            Dr["consumo"] = encabezado["consumo"];
            //            Dr["fac_Cuota_Base"] = cuota_base;
            //            Dr["fac_Cuata_Consumo"] = cuota_consumo;
            //            Dr["fac_Precio_M3"] = precio_m3;
            //            Dr["fecha_inicio"] = encabezado["fecha_inicio"];
            //            Dr["fecha_termino"] = encabezado["fecha_termino"];
            //            Dr["fecha_limite"] = encabezado["vencimient"];
            //            Dr["fecha_emision"] = encabezado["facturacion"];
            //            Dr["periodo"] = encabezado["periodo"];
            //            if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //            {
            //                Dr["tasa_iva"] = "16";
            //            }
            //            else
            //            {
            //                Dr["tasa_iva"] = "0.0";
            //            }
            //            Dr["iva_agua"] = Math.Round(actual_iva_agua, 2);
            //            Dr["iva_alcan"] = Math.Round(actual_iva_alcan, 2);
            //            Dr["iva_sanea"] = Math.Round(actual_iva_sanea, 2);

            //            Dr["total_importe"] = Math.Round(double.Parse(encabezado["pagua"].ToString()) + double.Parse(encabezado["palcan"].ToString()) + double.Parse(encabezado["psanea"].ToString()) + actual_recagua + actual_recalcan + actual_recsanea + actual_crbomb, 2);
            //            Dr["total_iva"] = Math.Round(actual_iva, 2);
            //            Total_Pagado = float.Parse(encabezado["pagua"].ToString()) + float.Parse(encabezado["palcan"].ToString()) + float.Parse(encabezado["psanea"].ToString()) + actual_recagua + actual_recalcan + actual_recsanea + actual_crbomb + actual_iva;

            //            Total_Pagado = Math.Round(Total_Pagado, 2);
            //            Dr["total_pagar"] = Total_Pagado;
            //            Dr["total_abono"] = Math.Round(actual_total_abonado, 2);
            //            Saldo = float.Parse(encabezado["pagua"].ToString()) + float.Parse(encabezado["palcan"].ToString()) + float.Parse(encabezado["psanea"].ToString()) + actual_recagua + actual_recalcan + actual_recsanea + actual_crbomb + actual_iva - actual_total_abonado;
            //            Saldo = Math.Round(Saldo, 2);
            //            Dr["fac_Saldo"] = Saldo;
            //            if (Saldo == 0)
            //            {
            //                Dr["estado"] = "PAGADO";
            //            }
            //            else
            //            {
            //                Dr["estado"] = "PENDIENTE";
            //            }
            //            Dr["anio"] = encabezado["anio"];
            //            Dr["bimestre"] = encabezado["bimestre"];
            //            Dr["rpu"] = encabezado["rpu"];
            //            Dr["pagua"] = encabezado["pagua"];
            //            Dr["palcan"] = encabezado["palcan"];
            //            Dr["psanea"] = encabezado["psanea"];
            //            Dr["recagua"] = 0;
            //            Dr["recalcan"] = 0;
            //            Dr["recsanea"] = 0;
            //            Dr["crbomb"] = actual_crbomb;

            //            Dr["abono_agua"] = Math.Round(actual_pago_agua, 2);
            //            Dr["abono_alcan"] = Math.Round(actual_pago_alcan, 2);
            //            Dr["abonosanea"] = Math.Round(actual_pago_sanea, 2);
            //            Dr["abono_recagua"] = 0;
            //            Dr["abono_recalcan"] = 0;
            //            Dr["abono_recsanea"] = 0;
            //            Dr["abono_crbomb"] = Math.Round(actual_pago_crbomb, 2);
            //            Dr["abono_IVA_agua"] = Math.Round(actual_pago_iva_agua, 2);
            //            Dr["abono_IVA_alcan"] = Math.Round(actual_pago_iva_alcan, 2);
            //            Dr["abono_IVA_sanea"] = Math.Round(actual_pago_iva_sanea, 2);
            //            Dr["anticipo"] = Math.Round(actual_ppagua + actual_ppalcan + actual_ppsanea + piva, 2);
            //            Dr["Fecha_Pago"] = encabezado["fechapago"];

            //            Dt_Datos.Rows.Add(Dr);

            //            ajuste_iva = 0;

            //            pBar1.PerformStep();

            //        }//end foreach encabezado
            //    }
            //    dtg_destino.DataSource = Dt_Datos;
            //    lbl_destino.Text = (dtg_destino.Rows.Count).ToString();
            //    watch.Stop();
            //    TimeSpan ts = watch.Elapsed;
            //    string elapsedtime = string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            //    MessageBox.Show("Datos Copiados!" + "\n" + "Tiempo: " + elapsedtime, "Migración Recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    pBar1.Visible = false;
            //    btn_migrar.Enabled = true;

            //    #endregion
            //}
            //if (rdb_5.Checked)
            //{
            //    #region Escenario 5...

            //    btn_copy.Enabled = false;
            //    pBar1.Visible = true;
            //    pBar1.Minimum = 1;
            //    pBar1.Maximum = dtg_origen.Rows.Count;
            //    pBar1.Value = 1;
            //    pBar1.Step = 1;

            //    Stopwatch watch = new Stopwatch();
            //    watch.Start();

            //    double pagua;
            //    double palcan;
            //    double psanea;
            //    double rezagua;
            //    double rezalcan;
            //    double rezsanea;
            //    double recagua;
            //    double recalcan;
            //    double recsanea;
            //    double iva;
            //    double crbomb;
            //    double ppagua;
            //    double ppalcan;
            //    double ppsanea;
            //    double prezagua;
            //    double prezalcan;
            //    double prezsanea;
            //    double precagua;
            //    double precalcan;
            //    double precsanea;
            //    double piva;
            //    double pcrbomb;
            //    double panticipo;
            //    double actual_iva_agua;
            //    double actual_iva_alcan;
            //    double actual_iva_sanea;
            //    double anterior_iva_agua;
            //    double anterior_iva_alcan;
            //    double anterior_iva_sanea;

            //    double pago_agua;
            //    double pago_alcan;
            //    double pago_sanea;
            //    double pago_rezagua;
            //    double pago_rezalcan;
            //    double pago_rezsanea;
            //    double pago_recagua;
            //    double pago_recalcan;
            //    double pago_recsanea;
            //    double pago_actual_iva_agua;
            //    double pago_actual_iva_alcan;
            //    double pago_actual_iva_sanea;
            //    double pago_anterior_iva_agua;
            //    double pago_anterior_iva_alcan;
            //    double pago_anterior_iva_sanea;
            //    double pago_crbomb;

            //    double actual_total_importe;
            //    double actual_total_iva;
            //    double actual_total_pagar;
            //    double actual_total_abono;
            //    double actual_total_saldo;
            //    double anterior_total_importe;
            //    double anterior_total_iva;
            //    double anterior_total_pagar;
            //    double anterior_total_abono;
            //    double anterior_total_saldo;
            //    double ajuste_iva;

            //    float cuota_base = 0;
            //    float cuota_consumo = 0;
            //    float precio_m3 = 0;

            //    string automatic_id;
            //    int no_Reg = obj.Consulta_id(db_destino);
            //    int x = 1;

            //    DataTable Dt_Datos = new DataTable();
            //    Dt_Datos.Columns.Add("fac_No_Factura");
            //    Dt_Datos.Columns.Add("fac_No_Cuenta");
            //    Dt_Datos.Columns.Add("fac_No_Recibo");
            //    Dt_Datos.Columns.Add("fac_Region_ID");
            //    Dt_Datos.Columns.Add("fac_Predio_ID");
            //    Dt_Datos.Columns.Add("fac_Usuario_ID");
            //    Dt_Datos.Columns.Add("fac_Medidor_ID");
            //    Dt_Datos.Columns.Add("fac_Tarifa_ID");
            //    Dt_Datos.Columns.Add("fac_Lectura_Anterior");
            //    Dt_Datos.Columns.Add("fac_Lectura_Actual");
            //    Dt_Datos.Columns.Add("fac_Consumo");
            //    Dt_Datos.Columns.Add("fac_Cuota_Base");
            //    Dt_Datos.Columns.Add("fac_Cuata_Consumo");
            //    Dt_Datos.Columns.Add("fac_Precio_M3");
            //    Dt_Datos.Columns.Add("fac_Fecha_Inicio");
            //    Dt_Datos.Columns.Add("fac_Fecha_Termino");
            //    Dt_Datos.Columns.Add("fac_Fecha_Limite");
            //    Dt_Datos.Columns.Add("fac_Fecha_Emicio");
            //    Dt_Datos.Columns.Add("periodo");
            //    Dt_Datos.Columns.Add("fac_Tasa_IVA");
            //    Dt_Datos.Columns.Add("fac_Total_Importe");
            //    Dt_Datos.Columns.Add("fac_Total_IVA");
            //    Dt_Datos.Columns.Add("fac_Total_Pagado");
            //    Dt_Datos.Columns.Add("fac_Total_Abono");
            //    Dt_Datos.Columns.Add("fac_Saldo");
            //    Dt_Datos.Columns.Add("fac_Estado");
            //    Dt_Datos.Columns.Add("fac_Anio");
            //    Dt_Datos.Columns.Add("fac_Bimestre");
            //    Dt_Datos.Columns.Add("fac_RPU");
            //    Dt_Datos.Columns.Add("Pagua");
            //    Dt_Datos.Columns.Add("Palcan");
            //    Dt_Datos.Columns.Add("Psanea");
            //    Dt_Datos.Columns.Add("recagua");
            //    Dt_Datos.Columns.Add("recalcan");
            //    Dt_Datos.Columns.Add("recsanea");
            //    Dt_Datos.Columns.Add("crbomb");
            //    Dt_Datos.Columns.Add("IVA_agua");
            //    Dt_Datos.Columns.Add("IVA_alcan");
            //    Dt_Datos.Columns.Add("IVA_sanea");
            //    Dt_Datos.Columns.Add("abono_agua");
            //    Dt_Datos.Columns.Add("abono_alcan");
            //    Dt_Datos.Columns.Add("abonosanea");
            //    Dt_Datos.Columns.Add("abono_recagua");
            //    Dt_Datos.Columns.Add("abono_recalcan");
            //    Dt_Datos.Columns.Add("abono_recsanea");
            //    Dt_Datos.Columns.Add("abono_crbomb");
            //    Dt_Datos.Columns.Add("abono_IVA_agua");
            //    Dt_Datos.Columns.Add("abono_IVA_alcan");
            //    Dt_Datos.Columns.Add("abono_IVA_sanea");
            //    Dt_Datos.Columns.Add("anticipo");
            //    Dt_Datos.Columns.Add("Codigo_Barras");
            //    Dt_Datos.Columns.Add("Fecha_Pago");

            //    DataTable dt_recibos = (DataTable)dtg_origen.DataSource;

            //    foreach (DataRow encabezado in dt_recibos.Rows)
            //    {
            //        pagua = double.Parse(encabezado["pagua"].ToString());
            //        palcan = double.Parse(encabezado["palcan"].ToString());
            //        psanea = double.Parse(encabezado["psanea"].ToString());
            //        rezagua = double.Parse(encabezado["rezagua"].ToString());
            //        rezalcan = double.Parse(encabezado["rezalcan"].ToString());
            //        rezsanea = double.Parse(encabezado["rezsanea"].ToString());
            //        recagua = double.Parse(encabezado["recagua"].ToString());
            //        recalcan = double.Parse(encabezado["recalcan"].ToString());
            //        recsanea = double.Parse(encabezado["recsanea"].ToString());
            //        iva = double.Parse(encabezado["iva"].ToString());
            //        crbomb = double.Parse(encabezado["crbomb"].ToString());
            //        ppagua = double.Parse(encabezado["ppagua"].ToString());
            //        ppalcan = double.Parse(encabezado["ppalcan"].ToString());
            //        ppsanea = double.Parse(encabezado["ppsanea"].ToString());
            //        prezagua = double.Parse(encabezado["prezagua"].ToString());
            //        prezalcan = double.Parse(encabezado["prezalcan"].ToString());
            //        prezsanea = double.Parse(encabezado["prezsanea"].ToString());
            //        precagua = double.Parse(encabezado["precagua"].ToString());
            //        precalcan = double.Parse(encabezado["precalcan"].ToString());
            //        precsanea = double.Parse(encabezado["precsanea"].ToString());
            //        piva = double.Parse(encabezado["piva"].ToString());
            //        pcrbomb = double.Parse(encabezado["pcrbomb"].ToString());
            //        panticipo = double.Parse(encabezado["panticipo"].ToString());

            //        if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //        {
            //            anterior_iva_agua = rezagua * 0.16;
            //            actual_iva_agua = pagua * 0.16;
            //        }
            //        else
            //        {
            //            anterior_iva_agua = 0;
            //            actual_iva_agua = 0;
            //        }
            //        anterior_iva_alcan = rezalcan * 0.16;
            //        anterior_iva_sanea = rezsanea * 0.16;
            //        actual_iva_alcan = palcan * 0.16;
            //        actual_iva_sanea = psanea * 0.16;

            //        anterior_total_iva = anterior_iva_agua + anterior_iva_alcan + anterior_iva_sanea;
            //        actual_total_iva = actual_iva_agua + actual_iva_alcan + actual_iva_sanea;

            //        if (iva > 0)
            //        {
            //            ajuste_iva = iva - (anterior_total_iva + actual_total_iva);
            //            actual_iva_sanea += ajuste_iva;
            //            if (actual_iva_sanea < 0)
            //            {
            //                actual_iva_alcan += actual_iva_sanea;
            //                actual_iva_sanea = 0;
            //                if (actual_iva_alcan < 0)
            //                {
            //                    anterior_iva_sanea += actual_iva_alcan;
            //                    actual_iva_alcan = 0;
            //                    if (anterior_iva_sanea < 0)
            //                    {
            //                        anterior_iva_alcan += anterior_iva_sanea;
            //                        anterior_iva_sanea = 0;
            //                    }
            //                }
            //            }
            //            anterior_total_iva = anterior_iva_agua + anterior_iva_alcan + anterior_iva_sanea;
            //            actual_total_iva = actual_iva_agua + actual_iva_alcan + actual_iva_sanea;
            //        }
            //        else
            //        {
            //            actual_iva_agua = 0;
            //            actual_iva_alcan = 0;
            //            actual_iva_sanea = 0;
            //            actual_total_iva = 0;

            //            anterior_iva_agua = 0;
            //            anterior_iva_alcan = 0;
            //            anterior_iva_sanea = 0;
            //            anterior_total_iva = 0;
            //        }

            //        anterior_total_importe = rezagua + rezalcan + rezsanea + recagua + recalcan + recsanea;
            //        actual_total_importe = pagua + palcan + psanea + crbomb;

            //        anterior_total_pagar = anterior_total_importe + anterior_total_iva;
            //        actual_total_pagar = actual_total_importe + actual_total_iva;

            //        pago_agua = 0;
            //        pago_alcan = 0;
            //        pago_sanea = 0;
            //        pago_rezagua = 0;
            //        pago_rezalcan = 0;
            //        pago_rezsanea = 0;
            //        pago_recagua = 0;
            //        pago_recalcan = 0;
            //        pago_recsanea = 0;
            //        pago_actual_iva_agua = 0;
            //        pago_actual_iva_alcan = 0;
            //        pago_actual_iva_sanea = 0;
            //        pago_anterior_iva_agua = 0;
            //        pago_anterior_iva_alcan = 0;
            //        pago_anterior_iva_sanea = 0;
            //        pago_crbomb = 0;

            //        actual_total_abono = 0;
            //        anterior_total_abono = 0;
            //        actual_total_saldo = 0;
            //        anterior_total_saldo = 0;


            //        // ************** PAGOS RECIBO ANTERIOR ************ \\
            //        if (rezagua <= prezagua)
            //        {
            //            pago_rezagua = rezagua;
            //            anterior_total_abono += rezagua;
            //            prezagua -= rezagua;
            //        }
            //        else
            //        {
            //            pago_rezagua = prezagua;
            //            anterior_total_abono += prezagua;
            //            prezagua = 0;
            //        }
            //        if (rezalcan <= prezalcan)
            //        {
            //            pago_rezalcan = rezalcan;
            //            anterior_total_abono += rezalcan;
            //            prezalcan -= rezalcan;
            //        }
            //        else
            //        {
            //            pago_rezalcan = prezalcan;
            //            anterior_total_abono += prezalcan;
            //            prezalcan = 0;
            //        }
            //        if (rezsanea <= prezsanea)
            //        {
            //            pago_rezsanea = rezsanea;
            //            anterior_total_abono += rezsanea;
            //            prezsanea -= rezsanea;
            //        }
            //        else
            //        {
            //            pago_rezsanea = prezsanea;
            //            anterior_total_abono += prezsanea;
            //            prezsanea = 0;
            //        }
            //        if (recagua <= precagua)
            //        {
            //            pago_recagua = recagua;
            //            anterior_total_abono += recagua;
            //            precagua -= recagua;
            //        }
            //        else
            //        {
            //            pago_recagua = precagua;
            //            anterior_total_abono += precagua;
            //            precagua = 0;
            //        }
            //        if (recalcan <= precalcan)
            //        {
            //            pago_recalcan = recalcan;
            //            anterior_total_abono += recalcan;
            //            precalcan -= recalcan;
            //        }
            //        else
            //        {
            //            pago_recalcan = precalcan;
            //            anterior_total_abono += precalcan;
            //            precalcan = 0;
            //        }
            //        if (recsanea <= precsanea)
            //        {
            //            pago_recsanea = recsanea;
            //            anterior_total_abono += recsanea;
            //            precsanea -= recsanea;
            //        }
            //        else
            //        {
            //            pago_recsanea = precsanea;
            //            anterior_total_abono += precsanea;
            //            precsanea = 0;
            //        }
            //        if (anterior_iva_agua <= piva)
            //        {
            //            pago_anterior_iva_agua = anterior_iva_agua;
            //            anterior_total_abono += anterior_iva_agua;
            //            piva -= anterior_iva_agua;
            //        }
            //        else
            //        {
            //            pago_anterior_iva_agua = piva;
            //            anterior_total_abono += piva;
            //            piva = 0;
            //        }
            //        if (anterior_iva_alcan <= piva)
            //        {
            //            pago_anterior_iva_alcan = anterior_iva_alcan;
            //            anterior_total_abono += anterior_iva_alcan;
            //            piva -= anterior_iva_alcan;
            //        }
            //        else
            //        {
            //            pago_anterior_iva_alcan = piva;
            //            anterior_total_abono += piva;
            //            piva = 0;
            //        }
            //        if (anterior_iva_sanea <= piva)
            //        {
            //            pago_anterior_iva_sanea = anterior_iva_sanea;
            //            anterior_total_abono += anterior_iva_sanea;
            //            piva -= anterior_iva_sanea;
            //        }
            //        else
            //        {
            //            pago_anterior_iva_sanea = piva;
            //            anterior_total_abono += piva;
            //            piva = 0;
            //        }

            //        // ******************** PAGOS FACTURA ACTUAL **************** \\
            //        if (pagua <= ppagua)
            //        {
            //            pago_agua = pagua;
            //            actual_total_abono += pagua;
            //            ppagua -= pagua;
            //        }
            //        else
            //        {
            //            pago_agua = ppagua;
            //            actual_total_abono += ppagua;
            //            ppagua = 0;
            //        }
            //        if (palcan <= ppalcan)
            //        {
            //            pago_alcan = palcan;
            //            actual_total_abono += palcan;
            //            ppalcan -= palcan;
            //        }
            //        else
            //        {
            //            pago_alcan = ppalcan;
            //            actual_total_abono += ppalcan;
            //            ppalcan = 0;
            //        }
            //        if (psanea <= ppsanea)
            //        {
            //            pago_sanea = psanea;
            //            actual_total_abono += psanea;
            //            ppsanea -= psanea;
            //        }
            //        else
            //        {
            //            pago_sanea = ppsanea;
            //            actual_total_abono += ppsanea;
            //            ppsanea = 0;
            //        }
            //        if (actual_iva_agua <= piva)
            //        {
            //            pago_actual_iva_agua = actual_iva_agua;
            //            actual_total_abono += actual_iva_agua;
            //            piva -= actual_iva_agua;
            //        }
            //        else
            //        {
            //            pago_actual_iva_agua = piva;
            //            actual_total_abono += piva;
            //            piva = 0;
            //        }
            //        if (actual_iva_alcan <= piva)
            //        {
            //            pago_actual_iva_alcan = actual_iva_alcan;
            //            actual_total_abono += actual_iva_alcan;
            //            piva -= actual_iva_alcan;
            //        }
            //        else
            //        {
            //            pago_actual_iva_alcan = piva;
            //            actual_total_abono += piva;
            //            piva = 0;
            //        }
            //        if (actual_iva_sanea <= piva)
            //        {
            //            pago_actual_iva_sanea = actual_iva_sanea;
            //            actual_total_abono += actual_iva_sanea;
            //            piva -= actual_iva_sanea;
            //        }
            //        else
            //        {
            //            pago_actual_iva_sanea = piva;
            //            actual_total_abono += piva;
            //            piva = 0;
            //        }
            //        if (crbomb <= pcrbomb)
            //        {
            //            pago_crbomb = crbomb;
            //            actual_total_abono += crbomb;
            //            pcrbomb -= crbomb;
            //        }
            //        else
            //        {
            //            pago_crbomb = pcrbomb;
            //            actual_total_abono += pcrbomb;
            //            pcrbomb = 0;
            //        }

            //        anterior_total_saldo = anterior_total_pagar - anterior_total_abono;
            //        actual_total_saldo = actual_total_pagar - actual_total_abono;

            //        // -- Aqui inicia el llenado de las columnas "cuota_base", "cuota_consumo", "precio_m3" -- \\

            //        int cont;
            //        if (encabezado["tarifa_ID"].ToString() == "00003" || encabezado["tarifa_ID"].ToString() == "00004" || encabezado["tarifa_ID"].ToString() == "00008" || encabezado["tarifa_ID"].ToString() == "00009" || encabezado["tarifa_ID"].ToString() == "00010" || encabezado["tarifa_ID"].ToString() == "00011") // tarifas fijas
            //        {
            //            for (cont = 0; cont < dt_tarifadetalle.Rows.Count; cont++)
            //            {
            //                if (encabezado["tarifa_ID"].ToString() == colum_tarifa_id[cont] && colum_cantidad[cont] == "0")
            //                {
            //                    cuota_base = float.Parse(colum_enero[cont]);
            //                    cuota_consumo = 0.0f;
            //                    precio_m3 = 0.0f;
            //                    break;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            for (cont = 0; cont < dt_tarifadetalle.Rows.Count; cont++)
            //            {
            //                if (encabezado["tarifa_ID"].ToString() == colum_tarifa_id[cont] && colum_cantidad[cont] == "0")
            //                {
            //                    switch (encabezado["bimestre"].ToString())
            //                    {
            //                        case "1":
            //                            cuota_base = float.Parse(colum_enero[cont]);
            //                            break;
            //                        case "2":
            //                            cuota_base = float.Parse(colum_febrero[cont]);
            //                            break;
            //                        case "3":
            //                            cuota_base = float.Parse(colum_marzo[cont]);
            //                            break;
            //                        case "4":
            //                            cuota_base = float.Parse(colum_abril[cont]);
            //                            break;
            //                        case "5":
            //                            cuota_base = float.Parse(colum_mayo[cont]);
            //                            break;
            //                        case "6":
            //                            cuota_base = float.Parse(colum_junio[cont]);
            //                            break;
            //                        case "7":
            //                            cuota_base = float.Parse(colum_julio[cont]);
            //                            break;
            //                        case "8":
            //                            cuota_base = float.Parse(colum_agosto[cont]);
            //                            break;
            //                        case "9":
            //                            cuota_base = float.Parse(colum_septiembre[cont]);
            //                            break;
            //                        case "10":
            //                            cuota_base = float.Parse(colum_octubre[cont]);
            //                            break;
            //                        case "11":
            //                            cuota_base = float.Parse(colum_noviembre[cont]);
            //                            break;
            //                        case "12":
            //                            cuota_base = float.Parse(colum_diciembre[cont]);
            //                            break;
            //                    }//end switch
            //                    break;
            //                }//end if
            //            }//end for
            //            for (cont = 0; cont < dt_tarifadetalle.Rows.Count; cont++)
            //            {
            //                if (encabezado["tarifa_ID"].ToString() == colum_tarifa_id[cont] && colum_cantidad[cont] == "101")
            //                {
            //                    switch (encabezado["bimestre"].ToString())
            //                    {
            //                        case "1":
            //                            precio_m3 = float.Parse(colum_enero[cont]);
            //                            break;
            //                        case "2":
            //                            precio_m3 = float.Parse(colum_febrero[cont]);
            //                            break;
            //                        case "3":
            //                            precio_m3 = float.Parse(colum_marzo[cont]);
            //                            break;
            //                        case "4":
            //                            precio_m3 = float.Parse(colum_abril[cont]);
            //                            break;
            //                        case "5":
            //                            precio_m3 = float.Parse(colum_mayo[cont]);
            //                            break;
            //                        case "6":
            //                            precio_m3 = float.Parse(colum_junio[cont]);
            //                            break;
            //                        case "7":
            //                            precio_m3 = float.Parse(colum_julio[cont]);
            //                            break;
            //                        case "8":
            //                            precio_m3 = float.Parse(colum_agosto[cont]);
            //                            break;
            //                        case "9":
            //                            precio_m3 = float.Parse(colum_septiembre[cont]);
            //                            break;
            //                        case "10":
            //                            precio_m3 = float.Parse(colum_octubre[cont]);
            //                            break;
            //                        case "11":
            //                            precio_m3 = float.Parse(colum_noviembre[cont]);
            //                            break;
            //                        case "12":
            //                            precio_m3 = float.Parse(colum_diciembre[cont]);
            //                            break;
            //                    }//end switch
            //                    break;
            //                }//end if
            //            }//end for
            //            if (int.Parse(encabezado["consumo"].ToString()) <= 100)
            //            {
            //                for (cont = 0; cont < dt_tarifadetalle.Rows.Count; cont++)
            //                {
            //                    if (encabezado["tarifa_ID"].ToString() == colum_tarifa_id[cont] && encabezado["consumo"].ToString() == colum_cantidad[cont])
            //                    {
            //                        switch (encabezado["bimestre"].ToString())
            //                        {
            //                            case "1":
            //                                cuota_consumo = float.Parse(colum_enero[cont]);
            //                                break;
            //                            case "2":
            //                                cuota_consumo = float.Parse(colum_febrero[cont]);
            //                                break;
            //                            case "3":
            //                                cuota_consumo = float.Parse(colum_marzo[cont]);
            //                                break;
            //                            case "4":
            //                                cuota_consumo = float.Parse(colum_abril[cont]);
            //                                break;
            //                            case "5":
            //                                cuota_consumo = float.Parse(colum_mayo[cont]);
            //                                break;
            //                            case "6":
            //                                cuota_consumo = float.Parse(colum_junio[cont]);
            //                                break;
            //                            case "7":
            //                                cuota_consumo = float.Parse(colum_julio[cont]);
            //                                break;
            //                            case "8":
            //                                cuota_consumo = float.Parse(colum_agosto[cont]);
            //                                break;
            //                            case "9":
            //                                cuota_consumo = float.Parse(colum_septiembre[cont]);
            //                                break;
            //                            case "10":
            //                                cuota_consumo = float.Parse(colum_octubre[cont]);
            //                                break;
            //                            case "11":
            //                                cuota_consumo = float.Parse(colum_noviembre[cont]);
            //                                break;
            //                            case "12":
            //                                cuota_consumo = float.Parse(colum_diciembre[cont]);
            //                                break;
            //                        }//end switch
            //                        break;
            //                    }//end if
            //                }//end for
            //            }//end if
            //            else
            //            {
            //                int consumo_excedente = int.Parse(encabezado["consumo"].ToString()) - 100;
            //                for (cont = 0; cont < dt_tarifadetalle.Rows.Count; cont++)
            //                {
            //                    if (encabezado["tarifa_ID"].ToString() == colum_tarifa_id[cont] && colum_cantidad[cont] == "100")
            //                    {
            //                        switch (encabezado["bimestre"].ToString())
            //                        {
            //                            case "1":
            //                                cuota_consumo = float.Parse(colum_enero[cont]) + (consumo_excedente * precio_m3);
            //                                break;
            //                            case "2":
            //                                cuota_consumo = float.Parse(colum_febrero[cont]) + (consumo_excedente * precio_m3);
            //                                break;
            //                            case "3":
            //                                cuota_consumo = float.Parse(colum_marzo[cont]) + (consumo_excedente * precio_m3);
            //                                break;
            //                            case "4":
            //                                cuota_consumo = float.Parse(colum_abril[cont]) + (consumo_excedente * precio_m3);
            //                                break;
            //                            case "5":
            //                                cuota_consumo = float.Parse(colum_mayo[cont]) + (consumo_excedente * precio_m3);
            //                                break;
            //                            case "6":
            //                                cuota_consumo = float.Parse(colum_junio[cont]) + (consumo_excedente * precio_m3);
            //                                break;
            //                            case "7":
            //                                cuota_consumo = float.Parse(colum_julio[cont]) + (consumo_excedente * precio_m3);
            //                                break;
            //                            case "8":
            //                                cuota_consumo = float.Parse(colum_agosto[cont]) + (consumo_excedente * precio_m3);
            //                                break;
            //                            case "9":
            //                                cuota_consumo = float.Parse(colum_septiembre[cont]) + (consumo_excedente * precio_m3);
            //                                break;
            //                            case "10":
            //                                cuota_consumo = float.Parse(colum_octubre[cont]) + (consumo_excedente * precio_m3);
            //                                break;
            //                            case "11":
            //                                cuota_consumo = float.Parse(colum_noviembre[cont]) + (consumo_excedente * precio_m3);
            //                                break;
            //                            case "12":
            //                                cuota_consumo = float.Parse(colum_diciembre[cont]) + (consumo_excedente * precio_m3);
            //                                break;
            //                        }//end switch
            //                        break;
            //                    }//end if
            //                }//end for
            //            }
            //        }//end else (tarifas medidas)

            //        DataRow Dr;

            //        // ************** Factura Anterior ************** \\
            //        if (float.Parse(encabezado["rezagua"].ToString()) != 0 || float.Parse(encabezado["rezalcan"].ToString()) != 0 || float.Parse(encabezado["rezsanea"].ToString()) != 0)
            //        {
            //            Dr = Dt_Datos.NewRow();
            //            automatic_id = "0000000000" + (no_Reg + x).ToString();
            //            automatic_id = automatic_id.Substring(automatic_id.Length - 10, 10);
            //            x++;

            //            Dr["no_factura"] = automatic_id;
            //            Dr["Codigo_Barras"] = automatic_id + "F";
            //            Dr["no_cuenta"] = encabezado["cuenta"];
            //            Dr["no_recibo"] = encabezado["foliorecib"];
            //            Dr["region_id"] = encabezado["region_ID"];
            //            Dr["predio_id"] = encabezado["predio_ID"];
            //            Dr["usuario_id"] = encabezado["usuario_ID"];
            //            Dr["medidor_id"] = encabezado["nummedidor"];
            //            Dr["tarifa_id"] = encabezado["tarifa_ID"];
            //            Dr["lec_anterior"] = encabezado["lecanterior"];
            //            Dr["lec_actual"] = encabezado["lecactual"];
            //            Dr["consumo"] = encabezado["consumo"];
            //            //Dr["fac_Cuota_Base"] = 0;
            //            //Dr["fac_Cuata_Consumo"] = 0;
            //            //Dr["fac_Precio_M3"] = 0;
            //            Dr["fecha_inicio"] = DateTime.ParseExact(encabezado["fecha_inicio"].ToString(), "dd/MM/yyyy", null).AddMonths(-1);
            //            Dr["fecha_termino"] = DateTime.ParseExact(encabezado["fecha_termino"].ToString(), "dd/MM/yyyy", null).AddMonths(-1);
            //            Dr["fecha_limite"] = DateTime.ParseExact(encabezado["vencimient"].ToString().Substring(0, 10), "MM/dd/yyyy", null).AddMonths(-1);
            //            Dr["fecha_emision"] = DateTime.ParseExact(encabezado["facturacion"].ToString().Substring(0, 10), "MM/dd/yyyy", null).AddMonths(-1);
            //            Dr["periodo"] = encabezado["periodo"];
            //            if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //            {
            //                Dr["tasa_iva"] = "16";
            //            }
            //            else
            //            {
            //                Dr["tasa_iva"] = "0.0";
            //            }
            //            Dr["iva_agua"] = Math.Round(anterior_iva_agua, 2);
            //            Dr["iva_alcan"] = Math.Round(anterior_iva_alcan, 2);
            //            Dr["iva_sanea"] = Math.Round(anterior_iva_sanea, 2);
            //            Dr["total_importe"] = Math.Round(anterior_total_importe, 2);
            //            Dr["total_iva"] = Math.Round(anterior_total_iva, 2);
            //            Dr["total_pagar"] = Math.Round(anterior_total_pagar, 2);
            //            Dr["total_abono"] = Math.Round(anterior_total_abono, 2);
            //            Dr["fac_Saldo"] = anterior_total_saldo;
            //            if (anterior_total_saldo == 0)
            //            {
            //                Dr["estado"] = "PAGADO";
            //            }
            //            else
            //            {
            //                Dr["estado"] = "PENDIENTE";
            //            }
            //            if (encabezado["bimestre"].ToString() == "1")
            //            {
            //                Dr["anio"] = int.Parse(encabezado["anio"].ToString()) + 11;
            //                Dr["bimestre"] = int.Parse(encabezado["bimestre"].ToString()) - 1;
            //            }
            //            else
            //            {
            //                Dr["anio"] = encabezado["anio"];
            //                Dr["bimestre"] = int.Parse(encabezado["bimestre"].ToString()) - 1;
            //            }
            //            Dr["rpu"] = encabezado["rpu"];
            //            Dr["pagua"] = encabezado["rezagua"];
            //            Dr["palcan"] = encabezado["rezalcan"];
            //            Dr["psanea"] = encabezado["rezsanea"];
            //            Dr["recagua"] = Math.Round(recagua, 2);
            //            Dr["recalcan"] = Math.Round(recalcan, 2);
            //            Dr["recsanea"] = Math.Round(recsanea, 2);
            //            Dr["crbomb"] = 0;
            //            Dr["abono_agua"] = Math.Round(pago_rezagua, 2);
            //            Dr["abono_alcan"] = Math.Round(pago_rezalcan, 2);
            //            Dr["abonosanea"] = Math.Round(pago_rezsanea, 2);
            //            Dr["abono_recagua"] = Math.Round(pago_recagua, 2);
            //            Dr["abono_recalcan"] = Math.Round(pago_recalcan, 2);
            //            Dr["abono_recsanea"] = Math.Round(pago_recsanea, 2);
            //            Dr["abono_crbomb"] = 0;
            //            Dr["abono_IVA_agua"] = Math.Round(pago_anterior_iva_agua, 2);
            //            Dr["abono_IVA_alcan"] = Math.Round(pago_anterior_iva_alcan, 2);
            //            Dr["abono_IVA_sanea"] = Math.Round(pago_anterior_iva_sanea, 2);
            //            Dr["anticipo"] = 0;
            //            Dr["Fecha_Pago"] = encabezado["fechapago"];
            //            Dt_Datos.Rows.Add(Dr);
            //        }


            //        // ******************** Factura Actual ***************** \\
            //        Dr = Dt_Datos.NewRow();
            //        automatic_id = "0000000000" + (no_Reg + x).ToString();
            //        automatic_id = automatic_id.Substring(automatic_id.Length - 10, 10);
            //        x++;

            //        Dr["no_factura"] = automatic_id;
            //        Dr["Codigo_Barras"] = automatic_id + "F";
            //        Dr["no_cuenta"] = encabezado["cuenta"].ToString();
            //        Dr["no_recibo"] = encabezado["foliorecib"];
            //        Dr["region_id"] = encabezado["region_ID"];
            //        Dr["predio_id"] = encabezado["predio_ID"];
            //        Dr["usuario_id"] = encabezado["usuario_ID"];
            //        Dr["medidor_id"] = encabezado["nummedidor"];
            //        Dr["tarifa_id"] = encabezado["tarifa_ID"];
            //        Dr["lec_anterior"] = encabezado["lecanterior"];
            //        Dr["lec_actual"] = encabezado["lecactual"];
            //        Dr["consumo"] = encabezado["consumo"];
            //        Dr["fac_Cuota_Base"] = cuota_base;
            //        Dr["fac_Cuata_Consumo"] = cuota_consumo;
            //        Dr["fac_Precio_M3"] = precio_m3;
            //        Dr["fecha_inicio"] = DateTime.ParseExact(encabezado["fecha_inicio"].ToString(), "dd/MM/yyyy", null);
            //        Dr["fecha_termino"] = DateTime.ParseExact(encabezado["fecha_termino"].ToString(), "dd/MM/yyyy", null);
            //        Dr["fecha_limite"] = encabezado["vencimient"];
            //        Dr["fecha_emision"] = encabezado["facturacion"];
            //        Dr["periodo"] = encabezado["periodo"];
            //        if (Boolean.Parse(encabezado["ivaagua"].ToString()))
            //        {
            //            Dr["tasa_iva"] = "16";
            //        }
            //        else
            //        {
            //            Dr["tasa_iva"] = "0.0";
            //        }
            //        Dr["iva_agua"] = Math.Round(actual_iva_agua, 2);
            //        Dr["iva_alcan"] = Math.Round(actual_iva_alcan, 2);
            //        Dr["iva_sanea"] = Math.Round(actual_iva_sanea, 2);
            //        Dr["total_importe"] = Math.Round(actual_total_importe, 2);
            //        Dr["total_iva"] = Math.Round(actual_total_iva, 2);
            //        Dr["total_pagar"] = Math.Round(actual_total_pagar, 2);
            //        Dr["total_abono"] = Math.Round(actual_total_abono, 2);
            //        Dr["fac_Saldo"] = Math.Round(actual_total_saldo, 2);
            //        if (actual_total_saldo == 0)
            //        {
            //            Dr["estado"] = "PAGADO";
            //        }
            //        else
            //        {
            //            Dr["estado"] = "PENDIENTE";
            //        }
            //        Dr["anio"] = encabezado["anio"];
            //        Dr["bimestre"] = encabezado["bimestre"];
            //        Dr["rpu"] = encabezado["rpu"];
            //        Dr["pagua"] = encabezado["pagua"];
            //        Dr["palcan"] = encabezado["palcan"];
            //        Dr["psanea"] = encabezado["psanea"];
            //        Dr["recagua"] = 0;
            //        Dr["recalcan"] = 0;
            //        Dr["recsanea"] = 0;
            //        Dr["crbomb"] = crbomb;
            //        Dr["abono_agua"] = Math.Round(pago_agua, 2);
            //        Dr["abono_alcan"] = Math.Round(pago_alcan, 2);
            //        Dr["abonosanea"] = Math.Round(pago_sanea, 2);
            //        Dr["abono_recagua"] = 0;
            //        Dr["abono_recalcan"] = 0;
            //        Dr["abono_recsanea"] = 0;
            //        Dr["abono_crbomb"] = pago_crbomb;
            //        Dr["abono_IVA_agua"] = Math.Round(pago_actual_iva_agua, 2);
            //        Dr["abono_IVA_alcan"] = Math.Round(pago_actual_iva_alcan, 2);
            //        Dr["abono_IVA_sanea"] = Math.Round(pago_actual_iva_sanea, 2);
            //        Dr["anticipo"] = Math.Round(prezagua + prezalcan + prezsanea + ppagua + ppalcan + ppsanea + precagua + precalcan + precsanea + piva, 2);
            //        Dr["Fecha_Pago"] = encabezado["fechapago"];

            //        Dt_Datos.Rows.Add(Dr);

            //        ajuste_iva = 0;

            //        pBar1.PerformStep();
            //    }
            //    dtg_destino.DataSource = Dt_Datos;
            //    lbl_destino.Text = (dtg_destino.Rows.Count).ToString();
            //    watch.Stop();
            //    TimeSpan ts = watch.Elapsed;
            //    string elapsedtime = string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            //    MessageBox.Show("Datos Copiados!" + "\n" + "Tiempo: " + elapsedtime, "Migración Recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    pBar1.Visible = false;
            //    btn_migrar.Enabled = true;

                #endregion
            }
        }


        private void btn_migrar_Click(object sender, EventArgs e) // -------------------- Migracion de datos obtenidos ------------------- \\
        {
            #region Migracion datos dtg_Destino...

//            if (rdb_1.Checked || rdb_3.Checked || rdb_5.Checked)
//            {
//                DialogResult result;
//                result = MessageBox.Show("¿Seguro de querer continuar?", "Migración Recibos", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
//                if (result == System.Windows.Forms.DialogResult.Yes)
//                {
//                    string U_creo = "";
//                    if (rdb_1.Checked)
//                        U_creo = "MIGRACION CASO 1";
//                    if (rdb_3.Checked)
//                        U_creo = "MIGRACION CASO 3";
//                    if (rdb_5.Checked)
//                        U_creo = "MIGRACION CASO 5";

//                    pBar1.Visible = true;
//                    pBar1.Minimum = 1;
//                    pBar1.Maximum = dtg_destino.Rows.Count;
//                    pBar1.Value = 1;
//                    pBar1.Step = 1;
//                    Stopwatch watch = new Stopwatch();
//                    watch.Start();
//                    SqlConnection conn = new SqlConnection("Data Source=172.16.0.115;Initial Catalog=" + db_destino + ";User ID=usrsimapag;Password=C0nt3l16");
//                    string query1 = "INSERT INTO Ope_Cor_Facturacion_Recibos (No_Factura_Recibo,No_Cuenta,No_Recibo,Region_ID,Predio_ID,Usuario_ID,Medidor_ID,"
//                        + "Tarifa_ID,Lectura_Anterior,Lectura_Actual,Consumo,Cuota_Base,Cuota_Consumo,Precio_Metro_Cubico,Fecha_Inicio_Periodo,"
//                        + "Fecha_Termino_Periodo,Fecha_Limite_Pago,Fecha_Emision,Periodo_Facturacion,Tasa_IVA,Total_Importe,Total_IVA,Total_Pagar,"
//                        + "Total_Abono,Saldo,Estatus_Recibo,Usuario_Creo,Fecha_Creo,Anio,Bimestre,RPU,Codigo_Barras) "
//                        + "VALUES (@no_factura,@no_cuenta,@no_recibo,@region_id,@predio_id,@usuario_id,@medidor_id,@tarifa_id,@lec_anterior,@lec_actual,@consumo,"
//                        + "@cuota_base,@cuota_consumo,@precio_m3,@fe_in_pe,@fe_te_pe,@fe_limite,@fe_emision,@periodo,@tasa_iva,@total_importe,@total_iva,"
//                        + "@total_pagar,@total_abono,@saldo,@estatus,@usuario_creo,getdate(),@anio,@bimestre,@rpu,@codigo_barras)";

//                    string query2 = "INSERT INTO Ope_Cor_Facturacion_Recibos_Detalles (No_Factura_Recibo,Concepto_ID,Concepto,Importe,Impuesto,Total,"
//                    + "Importe_Abonado,Impuesto_Abonado,Total_Abonado,Importe_Saldo,Impuesto_Saldo,Total_Saldo,Anio,Bimestre,Estatus) "
//                    + "VALUES(@no_factura,@concepto_id,@concepto,@importe,@impuesto,@total,@importe_abonado,@impuesto_abonado,@total_abonado,"
//                    + "@importe_saldo,@impuesto_saldo,@total_saldo,@anio,@bimestre,@estatus)";

//                    string query3 = "INSERT INTO Ope_Cor_Pagos_Adelantados(Rpu,Importe,Saldo,Estatus) "
//                        + "VALUES(@rpu_2,@importe_2,@saldo_2,@estatus_2)";

//                    string query4 = "SET language español INSERT into Ope_Cor_Caj_Recibos_Cobros(RPU,CODIGO_BARRAS,RECIBOS_COBRAR,TOTAL_RECIBOS,PAGO_EFECTIVO,PAGO_CHEQUE,"
//                        + "PAGO_TARJETA,SALDO,CAMBIO,FECHA,USUARIO_CREO,FECHA_CREO,estado_recibo,importe_cobrado,total_iva_cobrado,"
//                        + "tasa_iva_cobrado,total_cobrado_ajuste,NO_FACTURA,periodo) "
//                        + "VALUES(@rpu,@codigo_barras,@recibos_cobrar,@total_recibos,@pago_efectivo,@pago_cheque,@pago_tarjeta,@saldo,@cambio,"
//                        + "@fecha,@usuario_creo,getdate(),@estado_recibo,@importe_cobrado,@total_iva_cobrado,@tasa,@total_cobrado,@no_factura,@periodo)";

//                    string query5 = "SET language español INSERT INTO Ope_Cor_Caj_Movimientos_Cobros(CONCEPTO_ID,IMPORTE,FECHA_MOVIMIENTO,Facturado,COMENTARIOS,"
//                        + "USUARIO_CREO,FECHA_CREO,estado_concepto_cobro,impuesto,total,RPU) "
//                        + "VALUES(@concepto_id,@importe,@fecha_movimiento,@facturado,@comentarios,@usuario_creo,getdate(),"
//                        + "@estado_concepto_cobro,@impuesto,@total,@rpu)";

//                    DateTime fecha;

//                    //try
//                    conn.Open();
//                    SqlCommand cmd_encabezado = new SqlCommand(query1, conn);
//                    SqlCommand cmd_detalle_agua = new SqlCommand(query2, conn);
//                    SqlCommand cmd_detalle_alcan = new SqlCommand(query2, conn);
//                    SqlCommand cmd_detalle_sanea = new SqlCommand(query2, conn);
//                    SqlCommand cmd_detalle_recagua = new SqlCommand(query2, conn);
//                    SqlCommand cmd_detalle_recalcan = new SqlCommand(query2, conn);
//                    SqlCommand cmd_detalle_recsanea = new SqlCommand(query2, conn);
//                    SqlCommand cmd_detalle_cruz_roja = new SqlCommand(query2, conn);
//                    SqlCommand cmd_detalle_bomberos = new SqlCommand(query2, conn);
//                    SqlCommand cmd_anticipo = new SqlCommand(query3, conn);
//                    SqlCommand cmd_recibos_cobros = new SqlCommand(query4, conn);
//                    SqlCommand cmd_movimientos_agua = new SqlCommand(query5, conn);
//                    SqlCommand cmd_movimientos_alcan = new SqlCommand(query5, conn);
//                    SqlCommand cmd_movimientos_sanea = new SqlCommand(query5, conn);
//                    SqlCommand cmd_movimientos_recagua = new SqlCommand(query5, conn);
//                    SqlCommand cmd_movimientos_recalcan = new SqlCommand(query5, conn);
//                    SqlCommand cmd_movimientos_recsanea = new SqlCommand(query5, conn);
//                    SqlCommand cmd_movimientos_cruz_roja = new SqlCommand(query5, conn);
//                    SqlCommand cmd_movimientos_bomberos = new SqlCommand(query5, conn);
//                    SqlCommand cmd_movimientos_iva = new SqlCommand(query5, conn);

//                    DataTable dt_datitos = (DataTable)dtg_destino.DataSource;
//                    //int k = 0;
//                    foreach (DataRow row in dt_datitos.Rows)
//                    {
//                        if (!string.IsNullOrEmpty(row["fac_Cuota_Base"].ToString()))
//                        {
//                            cmd_encabezado.Parameters.Clear();
//                            cmd_encabezado.Parameters.AddWithValue("@no_factura", row["no_factura"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@no_cuenta", row["no_cuenta"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@no_recibo", row["no_recibo"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@region_id", row["region_id"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@predio_id", row["predio_id"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@usuario_id", row["usuario_id"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@medidor_id", row["medidor_id"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@tarifa_id", row["tarifa_id"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@lec_anterior", int.Parse(row["lec_anterior"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@lec_actual", int.Parse(row["lec_actual"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@consumo", int.Parse(row["consumo"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@cuota_base", float.Parse(row["fac_Cuota_Base"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@cuota_consumo", float.Parse(row["fac_Cuata_Consumo"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@precio_m3", float.Parse(row["fac_Precio_M3"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@fe_in_pe", DateTime.ParseExact(row["fecha_inicio"].ToString().Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
//                            cmd_encabezado.Parameters.AddWithValue("@fe_te_pe", DateTime.ParseExact(row["fecha_termino"].ToString().Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
//                            cmd_encabezado.Parameters.AddWithValue("@fe_limite", DateTime.Parse(row["fecha_limite"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@fe_emision", DateTime.Parse(row["fecha_emision"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@periodo", row["periodo"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@tasa_iva", float.Parse(row["tasa_iva"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@total_importe", float.Parse(row["total_importe"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@total_iva", float.Parse(row["total_iva"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@total_pagar", float.Parse(row["total_pagar"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@total_abono", float.Parse(row["total_abono"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@saldo", float.Parse(row["fac_Saldo"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@estatus", row["estado"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@usuario_creo", U_creo);
//                            cmd_encabezado.Parameters.AddWithValue("@anio", int.Parse(row["anio"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@bimestre", int.Parse(row["bimestre"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@rpu", row["rpu"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@codigo_barras", row["Codigo_Barras"].ToString());
//                            cmd_encabezado.ExecuteNonQuery();
//                            cmd_encabezado.Parameters.Clear();

//                            if (float.Parse(row["tasa_iva"].ToString()) == 0)
//                            {
//                                cmd_detalle_agua.Parameters.Clear();
//                                cmd_detalle_agua.Parameters.AddWithValue("@no_factura", row["no_factura"].ToString());
//                                cmd_detalle_agua.Parameters.AddWithValue("@concepto_id", "00001");
//                                cmd_detalle_agua.Parameters.AddWithValue("@concepto", "CONSUMO AGUA");
//                                cmd_detalle_agua.Parameters.AddWithValue("@importe", float.Parse(row["pagua"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@impuesto", float.Parse(row["iva_agua"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@total", float.Parse(row["pagua"].ToString()) + float.Parse(row["iva_agua"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@importe_abonado", float.Parse(row["abono_agua"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@impuesto_abonado", float.Parse(row["abono_IVA_agua"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@total_abonado", float.Parse(row["abono_agua"].ToString()) + float.Parse(row["abono_IVA_agua"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@importe_saldo", float.Parse(row["pagua"].ToString()) - float.Parse(row["abono_agua"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@impuesto_saldo", float.Parse(row["iva_agua"].ToString()) - float.Parse(row["abono_IVA_agua"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@total_saldo", (float.Parse(row["pagua"].ToString()) + float.Parse(row["iva_agua"].ToString())) - (float.Parse(row["abono_agua"].ToString()) + float.Parse(row["abono_IVA_agua"].ToString())));
//                                cmd_detalle_agua.Parameters.AddWithValue("@anio", int.Parse(row["anio"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@bimestre", int.Parse(row["bimestre"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@estatus", row["estado"].ToString());
//                                cmd_detalle_agua.ExecuteNonQuery();
//                                cmd_detalle_agua.Parameters.Clear();
//                            }
//                            else
//                            {
//                                cmd_detalle_agua.Parameters.Clear();
//                                cmd_detalle_agua.Parameters.AddWithValue("@no_factura", row["no_factura"].ToString());
//                                cmd_detalle_agua.Parameters.AddWithValue("@concepto_id", "00010");
//                                cmd_detalle_agua.Parameters.AddWithValue("@concepto", "AGUA COMERCIAL");
//                                cmd_detalle_agua.Parameters.AddWithValue("@importe", float.Parse(row["pagua"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@impuesto", float.Parse(row["iva_agua"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@total", float.Parse(row["pagua"].ToString()) + float.Parse(row["iva_agua"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@importe_abonado", float.Parse(row["abono_agua"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@impuesto_abonado", float.Parse(row["abono_IVA_agua"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@total_abonado", float.Parse(row["abono_agua"].ToString()) + float.Parse(row["abono_IVA_agua"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@importe_saldo", float.Parse(row["pagua"].ToString()) - float.Parse(row["abono_agua"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@impuesto_saldo", float.Parse(row["iva_agua"].ToString()) - float.Parse(row["abono_IVA_agua"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@total_saldo", (float.Parse(row["pagua"].ToString()) + float.Parse(row["iva_agua"].ToString())) - (float.Parse(row["abono_agua"].ToString()) + float.Parse(row["abono_IVA_agua"].ToString())));
//                                cmd_detalle_agua.Parameters.AddWithValue("@anio", int.Parse(row["anio"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@bimestre", int.Parse(row["bimestre"].ToString()));
//                                cmd_detalle_agua.Parameters.AddWithValue("@estatus", row["estado"].ToString());
//                                cmd_detalle_agua.ExecuteNonQuery();
//                                cmd_detalle_agua.Parameters.Clear();
//                            }
//                            cmd_detalle_alcan.Parameters.Clear();
//                            cmd_detalle_alcan.Parameters.AddWithValue("@no_factura", row["no_factura"].ToString());
//                            cmd_detalle_alcan.Parameters.AddWithValue("@concepto_id", "00002");
//                            cmd_detalle_alcan.Parameters.AddWithValue("@concepto", "DRENAJE");
//                            cmd_detalle_alcan.Parameters.AddWithValue("@importe", float.Parse(row["palcan"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@impuesto", float.Parse(row["iva_alcan"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@total", float.Parse(row["palcan"].ToString()) + float.Parse(row["iva_alcan"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@importe_abonado", float.Parse(row["abono_alcan"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@impuesto_abonado", float.Parse(row["abono_IVA_alcan"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@total_abonado", float.Parse(row["abono_alcan"].ToString()) + float.Parse(row["abono_IVA_alcan"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@importe_saldo", float.Parse(row["palcan"].ToString()) - float.Parse(row["abono_alcan"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@impuesto_saldo", float.Parse(row["iva_alcan"].ToString()) - float.Parse(row["abono_IVA_alcan"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@total_saldo", (float.Parse(row["palcan"].ToString()) + float.Parse(row["iva_alcan"].ToString())) - (float.Parse(row["abono_alcan"].ToString()) + float.Parse(row["abono_IVA_alcan"].ToString())));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@anio", int.Parse(row["anio"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@bimestre", int.Parse(row["bimestre"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@estatus", row["estado"].ToString());
//                            cmd_detalle_alcan.ExecuteNonQuery();
//                            cmd_detalle_alcan.Parameters.Clear();

//                            cmd_detalle_sanea.Parameters.Clear();
//                            cmd_detalle_sanea.Parameters.AddWithValue("@no_factura", row["no_factura"].ToString());
//                            cmd_detalle_sanea.Parameters.AddWithValue("@concepto_id", "00003");
//                            cmd_detalle_sanea.Parameters.AddWithValue("@concepto", "SANEAMIENTO");
//                            cmd_detalle_sanea.Parameters.AddWithValue("@importe", float.Parse(row["psanea"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@impuesto", float.Parse(row["iva_sanea"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@total", float.Parse(row["psanea"].ToString()) + float.Parse(row["iva_sanea"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@importe_abonado", float.Parse(row["abonosanea"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@impuesto_abonado", float.Parse(row["abono_IVA_sanea"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@total_abonado", float.Parse(row["abonosanea"].ToString()) + float.Parse(row["abono_IVA_sanea"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@importe_saldo", float.Parse(row["psanea"].ToString()) - float.Parse(row["abonosanea"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@impuesto_saldo", float.Parse(row["iva_sanea"].ToString()) - float.Parse(row["abono_IVA_sanea"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@total_saldo", (float.Parse(row["psanea"].ToString()) + float.Parse(row["iva_sanea"].ToString())) - (float.Parse(row["abonosanea"].ToString()) + float.Parse(row["abono_IVA_sanea"].ToString())));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@anio", int.Parse(row["anio"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@bimestre", int.Parse(row["bimestre"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@estatus", row["estado"].ToString());
//                            cmd_detalle_sanea.ExecuteNonQuery();
//                            cmd_detalle_sanea.Parameters.Clear();

//                            if (float.Parse(row["total_abono"].ToString()) > 0)
//                            {
//                                cmd_recibos_cobros.Parameters.Clear();
//                                cmd_recibos_cobros.Parameters.AddWithValue("@rpu", row["rpu"].ToString());
//                                cmd_recibos_cobros.Parameters.AddWithValue("@codigo_barras", row["Codigo_Barras"].ToString());
//                                cmd_recibos_cobros.Parameters.AddWithValue("@recibos_cobrar", 1);
//                                cmd_recibos_cobros.Parameters.AddWithValue("@total_recibos", row["total_pagar"].ToString());
//                                cmd_recibos_cobros.Parameters.AddWithValue("@pago_efectivo", row["total_abono"].ToString());
//                                cmd_recibos_cobros.Parameters.AddWithValue("@pago_cheque", 0);
//                                cmd_recibos_cobros.Parameters.AddWithValue("@pago_tarjeta", 0);
//                                cmd_recibos_cobros.Parameters.AddWithValue("@saldo", row["fac_Saldo"].ToString());
//                                cmd_recibos_cobros.Parameters.AddWithValue("@cambio", 0);
//                                fecha = DateTime.Parse(row["fecha_pago"].ToString().Substring(0, 10));
//                                cmd_recibos_cobros.Parameters.AddWithValue("@fecha", fecha.Date);
//                                //cmd_recibos_cobros.Parameters.AddWithValue("@fecha", string.Format("{0:dd/MM/yyyy}", row["Fecha_Pago"].ToString()));
//                                cmd_recibos_cobros.Parameters.AddWithValue("@usuario_creo", U_creo);
//                                cmd_recibos_cobros.Parameters.AddWithValue("@estado_recibo", "ACTIVO");
//                                cmd_recibos_cobros.Parameters.AddWithValue("@importe_cobrado", float.Parse(row["abono_agua"].ToString()) + float.Parse(row["abono_alcan"].ToString()) + float.Parse(row["abonosanea"].ToString()) + float.Parse(row["abono_recagua"].ToString()) + float.Parse(row["abono_recalcan"].ToString()) + float.Parse(row["abono_recsanea"].ToString()) + float.Parse(row["abono_crbomb"].ToString()));
//                                cmd_recibos_cobros.Parameters.AddWithValue("@total_iva_cobrado", float.Parse(row["abono_IVA_agua"].ToString()) + float.Parse(row["abono_IVA_alcan"].ToString()) + float.Parse(row["abono_IVA_sanea"].ToString()));
//                                cmd_recibos_cobros.Parameters.AddWithValue("@tasa", row["tasa_iva"].ToString());
//                                cmd_recibos_cobros.Parameters.AddWithValue("@total_cobrado", row["total_abono"].ToString());
//                                cmd_recibos_cobros.Parameters.AddWithValue("@no_factura", row["no_recibo"].ToString());
//                                cmd_recibos_cobros.Parameters.AddWithValue("@periodo", row["fecha_emision"].ToString());
//                                cmd_recibos_cobros.ExecuteNonQuery();
//                                cmd_recibos_cobros.Parameters.Clear();

//                                cmd_movimientos_agua.Parameters.Clear();
//                                cmd_movimientos_agua.Parameters.AddWithValue("@concepto_id", "00001");
//                                cmd_movimientos_agua.Parameters.AddWithValue("@importe", row["abono_agua"].ToString());
//                                cmd_movimientos_agua.Parameters.AddWithValue("@fecha_movimiento", DateTime.Parse(row["fecha_pago"].ToString().Substring(0, 10)));
//                                cmd_movimientos_agua.Parameters.AddWithValue("@facturado", "N");
//                                cmd_movimientos_agua.Parameters.AddWithValue("@comentarios", row["no_recibo"].ToString());
//                                cmd_movimientos_agua.Parameters.AddWithValue("@usuario_creo", U_creo);
//                                cmd_movimientos_agua.Parameters.AddWithValue("@estado_concepto_cobro", "ACTIVO");
//                                cmd_movimientos_agua.Parameters.AddWithValue("@impuesto", row["abono_IVA_agua"].ToString());
//                                cmd_movimientos_agua.Parameters.AddWithValue("@total", float.Parse(row["abono_agua"].ToString()) + float.Parse(row["abono_IVA_agua"].ToString()));
//                                cmd_movimientos_agua.Parameters.AddWithValue("@rpu", row["rpu"].ToString());
//                                cmd_movimientos_agua.ExecuteNonQuery();
//                                cmd_movimientos_agua.Parameters.Clear();

//                                cmd_movimientos_alcan.Parameters.Clear();
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@concepto_id", "00002");
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@importe", row["abono_alcan"].ToString());
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@fecha_movimiento", DateTime.Parse(row["fecha_pago"].ToString().Substring(0, 10)));
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@facturado", "N");
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@comentarios", row["no_recibo"].ToString());
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@usuario_creo", U_creo);
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@estado_concepto_cobro", "ACTIVO");
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@impuesto", row["abono_IVA_alcan"].ToString());
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@total", float.Parse(row["abono_alcan"].ToString()) + float.Parse(row["abono_IVA_alcan"].ToString()));
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@rpu", row["rpu"].ToString());
//                                cmd_movimientos_alcan.ExecuteNonQuery();
//                                cmd_movimientos_alcan.Parameters.Clear();

//                                cmd_movimientos_sanea.Parameters.Clear();
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@concepto_id", "00003");
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@importe", row["abonosanea"].ToString());
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@fecha_movimiento", DateTime.Parse(row["fecha_pago"].ToString().Substring(0, 10)));
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@facturado", "N");
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@comentarios", row["no_recibo"].ToString());
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@usuario_creo", U_creo);
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@estado_concepto_cobro", "ACTIVO");
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@impuesto", row["abono_IVA_sanea"].ToString());
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@total", float.Parse(row["abonosanea"].ToString()) + float.Parse(row["abono_IVA_sanea"].ToString()));
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@rpu", row["rpu"].ToString());
//                                cmd_movimientos_sanea.ExecuteNonQuery();
//                                cmd_movimientos_sanea.Parameters.Clear();
//                            }
//                        }
//                        else
//                        {
//                            cmd_encabezado.Parameters.Clear();
//                            cmd_encabezado.Parameters.AddWithValue("@no_factura", row["no_factura"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@no_cuenta", row["no_cuenta"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@no_recibo", row["no_recibo"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@region_id", row["region_id"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@predio_id", row["predio_id"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@usuario_id", row["usuario_id"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@medidor_id", row["medidor_id"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@tarifa_id", row["tarifa_id"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@lec_anterior", int.Parse(row["lec_anterior"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@lec_actual", int.Parse(row["lec_actual"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@consumo", int.Parse(row["consumo"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@cuota_base", 0);
//                            cmd_encabezado.Parameters.AddWithValue("@cuota_consumo", 0);
//                            cmd_encabezado.Parameters.AddWithValue("@precio_m3", 0);
//                            cmd_encabezado.Parameters.AddWithValue("@fe_in_pe", DateTime.ParseExact(row["fecha_inicio"].ToString().Trim(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
//                            cmd_encabezado.Parameters.AddWithValue("@fe_te_pe", DateTime.ParseExact(row["fecha_termino"].ToString().Trim(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
//                            cmd_encabezado.Parameters.AddWithValue("@fe_limite", DateTime.Parse(row["fecha_limite"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@fe_emision", DateTime.Parse(row["fecha_emision"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@periodo", row["periodo"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@tasa_iva", float.Parse(row["tasa_iva"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@total_importe", float.Parse(row["total_importe"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@total_iva", float.Parse(row["total_iva"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@total_pagar", float.Parse(row["total_pagar"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@total_abono", float.Parse(row["total_abono"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@saldo", float.Parse(row["fac_Saldo"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@estatus", row["estado"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@usuario_creo", U_creo);
//                            cmd_encabezado.Parameters.AddWithValue("@anio", int.Parse(row["anio"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@bimestre", int.Parse(row["bimestre"].ToString()));
//                            cmd_encabezado.Parameters.AddWithValue("@rpu", row["rpu"].ToString());
//                            cmd_encabezado.Parameters.AddWithValue("@codigo_barras", row["Codigo_Barras"].ToString());
//                            cmd_encabezado.ExecuteNonQuery();
//                            cmd_encabezado.Parameters.Clear();

//                            cmd_detalle_agua.Parameters.Clear();
//                            cmd_detalle_agua.Parameters.AddWithValue("@no_factura", row["no_factura"].ToString());
//                            cmd_detalle_agua.Parameters.AddWithValue("@concepto_id", "00004");
//                            cmd_detalle_agua.Parameters.AddWithValue("@concepto", "REZAGO AGUA");
//                            cmd_detalle_agua.Parameters.AddWithValue("@importe", float.Parse(row["pagua"].ToString()));
//                            cmd_detalle_agua.Parameters.AddWithValue("@impuesto", float.Parse(row["iva_agua"].ToString()));
//                            cmd_detalle_agua.Parameters.AddWithValue("@total", float.Parse(row["pagua"].ToString()) + float.Parse(row["iva_agua"].ToString()));
//                            cmd_detalle_agua.Parameters.AddWithValue("@importe_abonado", float.Parse(row["abono_agua"].ToString()));
//                            cmd_detalle_agua.Parameters.AddWithValue("@impuesto_abonado", float.Parse(row["abono_IVA_agua"].ToString()));
//                            cmd_detalle_agua.Parameters.AddWithValue("@total_abonado", float.Parse(row["abono_agua"].ToString()) + float.Parse(row["abono_IVA_agua"].ToString()));
//                            cmd_detalle_agua.Parameters.AddWithValue("@importe_saldo", float.Parse(row["pagua"].ToString()) - float.Parse(row["abono_agua"].ToString()));
//                            cmd_detalle_agua.Parameters.AddWithValue("@impuesto_saldo", float.Parse(row["iva_agua"].ToString()) - float.Parse(row["abono_IVA_agua"].ToString()));
//                            cmd_detalle_agua.Parameters.AddWithValue("@total_saldo", (float.Parse(row["pagua"].ToString()) + float.Parse(row["iva_agua"].ToString())) - (float.Parse(row["abono_agua"].ToString()) + float.Parse(row["abono_IVA_agua"].ToString())));
//                            cmd_detalle_agua.Parameters.AddWithValue("@anio", int.Parse(row["anio"].ToString()));
//                            cmd_detalle_agua.Parameters.AddWithValue("@bimestre", int.Parse(row["bimestre"].ToString()));
//                            cmd_detalle_agua.Parameters.AddWithValue("@estatus", row["estado"].ToString());
//                            cmd_detalle_agua.ExecuteNonQuery();
//                            cmd_detalle_agua.Parameters.Clear();

//                            cmd_detalle_alcan.Parameters.Clear();
//                            cmd_detalle_alcan.Parameters.AddWithValue("@no_factura", row["no_factura"].ToString());
//                            cmd_detalle_alcan.Parameters.AddWithValue("@concepto_id", "00005");
//                            cmd_detalle_alcan.Parameters.AddWithValue("@concepto", "REZAGO DRENAJE");
//                            cmd_detalle_alcan.Parameters.AddWithValue("@importe", float.Parse(row["palcan"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@impuesto", float.Parse(row["iva_alcan"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@total", float.Parse(row["palcan"].ToString()) + float.Parse(row["iva_alcan"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@importe_abonado", float.Parse(row["abono_alcan"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@impuesto_abonado", float.Parse(row["abono_IVA_alcan"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@total_abonado", float.Parse(row["abono_alcan"].ToString()) + float.Parse(row["abono_IVA_alcan"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@importe_saldo", float.Parse(row["palcan"].ToString()) - float.Parse(row["abono_alcan"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@impuesto_saldo", float.Parse(row["iva_alcan"].ToString()) - float.Parse(row["abono_IVA_alcan"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@total_saldo", (float.Parse(row["palcan"].ToString()) + float.Parse(row["iva_alcan"].ToString())) - (float.Parse(row["abono_alcan"].ToString()) + float.Parse(row["abono_IVA_alcan"].ToString())));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@anio", int.Parse(row["anio"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@bimestre", int.Parse(row["bimestre"].ToString()));
//                            cmd_detalle_alcan.Parameters.AddWithValue("@estatus", row["estado"].ToString());
//                            cmd_detalle_alcan.ExecuteNonQuery();
//                            cmd_detalle_alcan.Parameters.Clear();

//                            cmd_detalle_sanea.Parameters.Clear();
//                            cmd_detalle_sanea.Parameters.AddWithValue("@no_factura", row["no_factura"].ToString());
//                            cmd_detalle_sanea.Parameters.AddWithValue("@concepto_id", "00006");
//                            cmd_detalle_sanea.Parameters.AddWithValue("@concepto", "REZAGO SANEAMIENTO");
//                            cmd_detalle_sanea.Parameters.AddWithValue("@importe", float.Parse(row["psanea"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@impuesto", float.Parse(row["iva_sanea"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@total", float.Parse(row["psanea"].ToString()) + float.Parse(row["iva_sanea"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@importe_abonado", float.Parse(row["abonosanea"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@impuesto_abonado", float.Parse(row["abono_IVA_sanea"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@total_abonado", float.Parse(row["abonosanea"].ToString()) + float.Parse(row["abono_IVA_sanea"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@importe_saldo", float.Parse(row["psanea"].ToString()) - float.Parse(row["abonosanea"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@impuesto_saldo", float.Parse(row["iva_sanea"].ToString()) - float.Parse(row["abono_IVA_sanea"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@total_saldo", (float.Parse(row["psanea"].ToString()) + float.Parse(row["iva_sanea"].ToString())) - (float.Parse(row["abonosanea"].ToString()) + float.Parse(row["abono_IVA_sanea"].ToString())));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@anio", int.Parse(row["anio"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@bimestre", int.Parse(row["bimestre"].ToString()));
//                            cmd_detalle_sanea.Parameters.AddWithValue("@estatus", row["estado"].ToString());
//                            cmd_detalle_sanea.ExecuteNonQuery();
//                            cmd_detalle_sanea.Parameters.Clear();

//                            if (float.Parse(row["total_abono"].ToString()) > 0)
//                            {
//                                cmd_recibos_cobros.Parameters.Clear();
//                                cmd_recibos_cobros.Parameters.AddWithValue("@rpu", row["rpu"].ToString());
//                                cmd_recibos_cobros.Parameters.AddWithValue("@codigo_barras", row["Codigo_Barras"].ToString());
//                                cmd_recibos_cobros.Parameters.AddWithValue("@recibos_cobrar", 1);
//                                cmd_recibos_cobros.Parameters.AddWithValue("@total_recibos", row["total_pagar"].ToString());
//                                cmd_recibos_cobros.Parameters.AddWithValue("@pago_efectivo", row["total_abono"].ToString());
//                                cmd_recibos_cobros.Parameters.AddWithValue("@pago_cheque", 0);
//                                cmd_recibos_cobros.Parameters.AddWithValue("@pago_tarjeta", 0);
//                                cmd_recibos_cobros.Parameters.AddWithValue("@saldo", row["fac_Saldo"].ToString());
//                                cmd_recibos_cobros.Parameters.AddWithValue("@cambio", 0);
//                                fecha = DateTime.Parse(string.Format("{0:dd/MM/yyyy}", row["Fecha_Pago"].ToString()));
//                                cmd_recibos_cobros.Parameters.AddWithValue("@fecha", fecha.Date);
//                                //cmd_recibos_cobros.Parameters.AddWithValue("@fecha", string.Format("{0:dd/MM/yyyy}", row["Fecha_Pago"].ToString()));
//                                cmd_recibos_cobros.Parameters.AddWithValue("@usuario_creo", U_creo);
//                                cmd_recibos_cobros.Parameters.AddWithValue("@estado_recibo", "ACTIVO");
//                                cmd_recibos_cobros.Parameters.AddWithValue("@importe_cobrado", float.Parse(row["abono_agua"].ToString()) + float.Parse(row["abono_alcan"].ToString()) + float.Parse(row["abonosanea"].ToString()) + float.Parse(row["abono_recagua"].ToString()) + float.Parse(row["abono_recalcan"].ToString()) + float.Parse(row["abono_recsanea"].ToString()) + float.Parse(row["abono_crbomb"].ToString()));
//                                cmd_recibos_cobros.Parameters.AddWithValue("@total_iva_cobrado", float.Parse(row["abono_IVA_agua"].ToString()) + float.Parse(row["abono_IVA_alcan"].ToString()) + float.Parse(row["abono_IVA_sanea"].ToString()));
//                                cmd_recibos_cobros.Parameters.AddWithValue("@tasa", row["tasa_iva"].ToString());
//                                cmd_recibos_cobros.Parameters.AddWithValue("@total_cobrado", row["total_abono"].ToString());
//                                cmd_recibos_cobros.Parameters.AddWithValue("@no_factura", row["no_recibo"].ToString());
//                                cmd_recibos_cobros.Parameters.AddWithValue("@periodo", row["fecha_emision"].ToString());
//                                cmd_recibos_cobros.ExecuteNonQuery();
//                                cmd_recibos_cobros.Parameters.Clear();

//                                cmd_movimientos_agua.Parameters.Clear();
//                                cmd_movimientos_agua.Parameters.AddWithValue("@concepto_id", "00004");
//                                cmd_movimientos_agua.Parameters.AddWithValue("@importe", row["abono_agua"].ToString());
//                                cmd_movimientos_agua.Parameters.AddWithValue("@fecha_movimiento", DateTime.Parse(string.Format("{0:dd/MM/yyyy}", row["Fecha_Pago"].ToString())));
//                                cmd_movimientos_agua.Parameters.AddWithValue("@facturado", "N");
//                                cmd_movimientos_agua.Parameters.AddWithValue("@comentarios", row["no_recibo"].ToString());
//                                cmd_movimientos_agua.Parameters.AddWithValue("@usuario_creo", U_creo);
//                                cmd_movimientos_agua.Parameters.AddWithValue("@estado_concepto_cobro", "ACTIVO");
//                                cmd_movimientos_agua.Parameters.AddWithValue("@impuesto", row["abono_IVA_agua"].ToString());
//                                cmd_movimientos_agua.Parameters.AddWithValue("@total", float.Parse(row["abono_agua"].ToString()) + float.Parse(row["abono_IVA_agua"].ToString()));
//                                cmd_movimientos_agua.Parameters.AddWithValue("@rpu", row["rpu"].ToString());
//                                cmd_movimientos_agua.ExecuteNonQuery();
//                                cmd_movimientos_agua.Parameters.Clear();

//                                cmd_movimientos_alcan.Parameters.Clear();
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@concepto_id", "00005");
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@importe", row["abono_alcan"].ToString());
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@fecha_movimiento", DateTime.Parse(string.Format("{0:dd/MM/yyyy}", row["Fecha_Pago"].ToString())));
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@facturado", "N");
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@comentarios", row["no_recibo"].ToString());
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@usuario_creo", U_creo);
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@estado_concepto_cobro", "ACTIVO");
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@impuesto", row["abono_IVA_alcan"].ToString());
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@total", float.Parse(row["abono_alcan"].ToString()) + float.Parse(row["abono_IVA_alcan"].ToString()));
//                                cmd_movimientos_alcan.Parameters.AddWithValue("@rpu", row["rpu"].ToString());
//                                cmd_movimientos_alcan.ExecuteNonQuery();
//                                cmd_movimientos_alcan.Parameters.Clear();

//                                cmd_movimientos_sanea.Parameters.Clear();
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@concepto_id", "00006");
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@importe", row["abonosanea"].ToString());
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@fecha_movimiento", DateTime.Parse(string.Format("{0:dd/MM/yyyy}", row["Fecha_Pago"].ToString())));
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@facturado", "N");
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@comentarios", row["no_recibo"].ToString());
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@usuario_creo", U_creo);
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@estado_concepto_cobro", "ACTIVO");
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@impuesto", row["abono_IVA_sanea"].ToString());
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@total", float.Parse(row["abonosanea"].ToString()) + float.Parse(row["abono_IVA_sanea"].ToString()));
//                                cmd_movimientos_sanea.Parameters.AddWithValue("@rpu", row["rpu"].ToString());
//                                cmd_movimientos_sanea.ExecuteNonQuery();
//                                cmd_movimientos_sanea.Parameters.Clear();
//                            }
//                        }

//                        if (float.Parse(row["recagua"].ToString()) > 0 || float.Parse(row["recalcan"].ToString()) > 0 || float.Parse(row["recsanea"].ToString()) > 0)
//                        {
//                            cmd_detalle_recagua.Parameters.Clear();
//                            cmd_detalle_recagua.Parameters.AddWithValue("@no_factura", row["no_factura"].ToString());
//                            cmd_detalle_recagua.Parameters.AddWithValue("@concepto_id", "00007");
//                            cmd_detalle_recagua.Parameters.AddWithValue("@concepto", "RECARGO AGUA");
//                            cmd_detalle_recagua.Parameters.AddWithValue("@importe", float.Parse(row["recagua"].ToString()));
//                            cmd_detalle_recagua.Parameters.AddWithValue("@impuesto", 0);
//                            cmd_detalle_recagua.Parameters.AddWithValue("@total", float.Parse(row["recagua"].ToString()));
//                            cmd_detalle_recagua.Parameters.AddWithValue("@importe_abonado", float.Parse(row["abono_recagua"].ToString()));
//                            cmd_detalle_recagua.Parameters.AddWithValue("@impuesto_abonado", 0);
//                            cmd_detalle_recagua.Parameters.AddWithValue("@total_abonado", float.Parse(row["abono_recagua"].ToString()));
//                            cmd_detalle_recagua.Parameters.AddWithValue("@importe_saldo", float.Parse(row["recagua"].ToString()) - float.Parse(row["abono_recagua"].ToString()));
//                            cmd_detalle_recagua.Parameters.AddWithValue("@impuesto_saldo", 0);
//                            cmd_detalle_recagua.Parameters.AddWithValue("@total_saldo", float.Parse(row["recagua"].ToString()) - float.Parse(row["abono_recagua"].ToString()));
//                            cmd_detalle_recagua.Parameters.AddWithValue("@anio", int.Parse(row["anio"].ToString()));
//                            cmd_detalle_recagua.Parameters.AddWithValue("@bimestre", int.Parse(row["bimestre"].ToString()));
//                            cmd_detalle_recagua.Parameters.AddWithValue("@estatus", row["estado"].ToString());
//                            cmd_detalle_recagua.ExecuteNonQuery();
//                            cmd_detalle_recagua.Parameters.Clear();

//                            cmd_detalle_recalcan.Parameters.Clear();
//                            cmd_detalle_recalcan.Parameters.AddWithValue("@no_factura", row["no_factura"].ToString());
//                            cmd_detalle_recalcan.Parameters.AddWithValue("@concepto_id", "00008");
//                            cmd_detalle_recalcan.Parameters.AddWithValue("@concepto", "RECARGO DRENAJE");
//                            cmd_detalle_recalcan.Parameters.AddWithValue("@importe", float.Parse(row["recalcan"].ToString()));
//                            cmd_detalle_recalcan.Parameters.AddWithValue("@impuesto", 0);
//                            cmd_detalle_recalcan.Parameters.AddWithValue("@total", float.Parse(row["recalcan"].ToString()));
//                            cmd_detalle_recalcan.Parameters.AddWithValue("@importe_abonado", float.Parse(row["abono_recalcan"].ToString()));
//                            cmd_detalle_recalcan.Parameters.AddWithValue("@impuesto_abonado", 0);
//                            cmd_detalle_recalcan.Parameters.AddWithValue("@total_abonado", float.Parse(row["abono_recalcan"].ToString()));
//                            cmd_detalle_recalcan.Parameters.AddWithValue("@importe_saldo", float.Parse(row["recalcan"].ToString()) - float.Parse(row["abono_recalcan"].ToString()));
//                            cmd_detalle_recalcan.Parameters.AddWithValue("@impuesto_saldo", 0);
//                            cmd_detalle_recalcan.Parameters.AddWithValue("@total_saldo", float.Parse(row["recalcan"].ToString()) - float.Parse(row["abono_recalcan"].ToString()));
//                            cmd_detalle_recalcan.Parameters.AddWithValue("@anio", int.Parse(row["anio"].ToString()));
//                            cmd_detalle_recalcan.Parameters.AddWithValue("@bimestre", int.Parse(row["bimestre"].ToString()));
//                            cmd_detalle_recalcan.Parameters.AddWithValue("@estatus", row["estado"].ToString());
//                            cmd_detalle_recalcan.ExecuteNonQuery();
//                            cmd_detalle_recalcan.Parameters.Clear();

//                            cmd_detalle_recsanea.Parameters.Clear();
//                            cmd_detalle_recsanea.Parameters.AddWithValue("@no_factura", row["no_factura"].ToString());
//                            cmd_detalle_recsanea.Parameters.AddWithValue("@concepto_id", "00009");
//                            cmd_detalle_recsanea.Parameters.AddWithValue("@concepto", "RECARGO SANEAMIENTO");
//                            cmd_detalle_recsanea.Parameters.AddWithValue("@importe", float.Parse(row["recsanea"].ToString()));
//                            cmd_detalle_recsanea.Parameters.AddWithValue("@impuesto", 0);
//                            cmd_detalle_recsanea.Parameters.AddWithValue("@total", float.Parse(row["recsanea"].ToString()));
//                            cmd_detalle_recsanea.Parameters.AddWithValue("@importe_abonado", float.Parse(row["abono_recsanea"].ToString()));
//                            cmd_detalle_recsanea.Parameters.AddWithValue("@impuesto_abonado", 0);
//                            cmd_detalle_recsanea.Parameters.AddWithValue("@total_abonado", float.Parse(row["abono_recsanea"].ToString()));
//                            cmd_detalle_recsanea.Parameters.AddWithValue("@importe_saldo", float.Parse(row["recsanea"].ToString()) - float.Parse(row["abono_recsanea"].ToString()));
//                            cmd_detalle_recsanea.Parameters.AddWithValue("@impuesto_saldo", 0);
//                            cmd_detalle_recsanea.Parameters.AddWithValue("@total_saldo", float.Parse(row["recsanea"].ToString()) - float.Parse(row["abono_recsanea"].ToString()));
//                            cmd_detalle_recsanea.Parameters.AddWithValue("@anio", int.Parse(row["anio"].ToString()));
//                            cmd_detalle_recsanea.Parameters.AddWithValue("@bimestre", int.Parse(row["bimestre"].ToString()));
//                            cmd_detalle_recsanea.Parameters.AddWithValue("@estatus", row["estado"].ToString());
//                            cmd_detalle_recsanea.ExecuteNonQuery();
//                            cmd_detalle_recsanea.Parameters.Clear();

//                            if (float.Parse(row["abono_recagua"].ToString()) > 0 || float.Parse(row["abono_recalcan"].ToString()) > 0 || float.Parse(row["abono_recsanea"].ToString()) > 0)
//                            {
//                                cmd_movimientos_recagua.Parameters.Clear();
//                                cmd_movimientos_recagua.Parameters.AddWithValue("@concepto_id", "00007");
//                                cmd_movimientos_recagua.Parameters.AddWithValue("@importe", row["abono_recagua"].ToString());
//                                string fe = row["fecha_pago"].ToString().Substring(0, 10);
//                                cmd_movimientos_recagua.Parameters.AddWithValue("@fecha_movimiento", DateTime.Parse(fe));
//                                cmd_movimientos_recagua.Parameters.AddWithValue("@facturado", "N");
//                                cmd_movimientos_recagua.Parameters.AddWithValue("@comentarios", row["no_recibo"].ToString());
//                                cmd_movimientos_recagua.Parameters.AddWithValue("@usuario_creo", U_creo);
//                                cmd_movimientos_recagua.Parameters.AddWithValue("@estado_concepto_cobro", "ACTIVO");
//                                cmd_movimientos_recagua.Parameters.AddWithValue("@impuesto", 0);
//                                cmd_movimientos_recagua.Parameters.AddWithValue("@total", float.Parse(row["abono_recagua"].ToString()));
//                                cmd_movimientos_recagua.Parameters.AddWithValue("@rpu", row["rpu"].ToString());
//                                cmd_movimientos_recagua.ExecuteNonQuery();
//                                cmd_movimientos_recagua.Parameters.Clear();

//                                cmd_movimientos_recalcan.Parameters.Clear();
//                                cmd_movimientos_recalcan.Parameters.AddWithValue("@concepto_id", "00008");
//                                cmd_movimientos_recalcan.Parameters.AddWithValue("@importe", row["abono_recalcan"].ToString());
//                                fe = row["fecha_pago"].ToString().Substring(0, 10);
//                                cmd_movimientos_recalcan.Parameters.AddWithValue("@fecha_movimiento", DateTime.Parse(fe));
//                                cmd_movimientos_recalcan.Parameters.AddWithValue("@facturado", "N");
//                                cmd_movimientos_recalcan.Parameters.AddWithValue("@comentarios", row["no_recibo"].ToString());
//                                cmd_movimientos_recalcan.Parameters.AddWithValue("@usuario_creo", U_creo);
//                                cmd_movimientos_recalcan.Parameters.AddWithValue("@estado_concepto_cobro", "ACTIVO");
//                                cmd_movimientos_recalcan.Parameters.AddWithValue("@impuesto", 0);
//                                cmd_movimientos_recalcan.Parameters.AddWithValue("@total", float.Parse(row["abono_recalcan"].ToString()));
//                                cmd_movimientos_recalcan.Parameters.AddWithValue("@rpu", row["rpu"].ToString());
//                                cmd_movimientos_recalcan.ExecuteNonQuery();
//                                cmd_movimientos_recalcan.Parameters.Clear();

//                                cmd_movimientos_recsanea.Parameters.Clear();
//                                cmd_movimientos_recsanea.Parameters.AddWithValue("@concepto_id", "00009");
//                                cmd_movimientos_recsanea.Parameters.AddWithValue("@importe", row["abono_recsanea"].ToString());
//                                fe = row["fecha_pago"].ToString().Substring(0, 10);
//                                cmd_movimientos_recsanea.Parameters.AddWithValue("@fecha_movimiento", DateTime.Parse(fe));
//                                cmd_movimientos_recsanea.Parameters.AddWithValue("@facturado", "N");
//                                cmd_movimientos_recsanea.Parameters.AddWithValue("@comentarios", row["no_recibo"].ToString());
//                                cmd_movimientos_recsanea.Parameters.AddWithValue("@usuario_creo", U_creo);
//                                cmd_movimientos_recsanea.Parameters.AddWithValue("@estado_concepto_cobro", "ACTIVO");
//                                cmd_movimientos_recsanea.Parameters.AddWithValue("@impuesto", 0);
//                                cmd_movimientos_recsanea.Parameters.AddWithValue("@total", float.Parse(row["abono_recsanea"].ToString()));
//                                cmd_movimientos_recsanea.Parameters.AddWithValue("@rpu", row["rpu"].ToString());
//                                cmd_movimientos_recsanea.ExecuteNonQuery();
//                                cmd_movimientos_recsanea.Parameters.Clear();
//                            }
//                        }
//                        if (float.Parse(row["crbomb"].ToString()) > 0)
//                        {
//                            cmd_detalle_cruz_roja.Parameters.Clear();
//                            cmd_detalle_cruz_roja.Parameters.AddWithValue("@no_factura", row["no_factura"].ToString());
//                            cmd_detalle_cruz_roja.Parameters.AddWithValue("@concepto_id", "00011");
//                            cmd_detalle_cruz_roja.Parameters.AddWithValue("@concepto", "CRUZ ROJA");
//                            cmd_detalle_cruz_roja.Parameters.AddWithValue("@importe", float.Parse(row["crbomb"].ToString()) / 2);
//                            cmd_detalle_cruz_roja.Parameters.AddWithValue("@impuesto", 0);
//                            cmd_detalle_cruz_roja.Parameters.AddWithValue("@total", float.Parse(row["crbomb"].ToString()) / 2);
//                            cmd_detalle_cruz_roja.Parameters.AddWithValue("@importe_abonado", float.Parse(row["abono_crbomb"].ToString()) / 2);
//                            cmd_detalle_cruz_roja.Parameters.AddWithValue("@impuesto_abonado", 0);
//                            cmd_detalle_cruz_roja.Parameters.AddWithValue("@total_abonado", float.Parse(row["abono_crbomb"].ToString()) / 2);
//                            cmd_detalle_cruz_roja.Parameters.AddWithValue("@importe_saldo", (float.Parse(row["crbomb"].ToString()) - float.Parse(row["abono_crbomb"].ToString())) / 2);
//                            cmd_detalle_cruz_roja.Parameters.AddWithValue("@impuesto_saldo", 0);
//                            cmd_detalle_cruz_roja.Parameters.AddWithValue("@total_saldo", (float.Parse(row["crbomb"].ToString()) - float.Parse(row["abono_crbomb"].ToString())) / 2);
//                            cmd_detalle_cruz_roja.Parameters.AddWithValue("@anio", int.Parse(row["anio"].ToString()));
//                            cmd_detalle_cruz_roja.Parameters.AddWithValue("@bimestre", int.Parse(row["bimestre"].ToString()));
//                            cmd_detalle_cruz_roja.Parameters.AddWithValue("@estatus", row["estado"].ToString());
//                            cmd_detalle_cruz_roja.ExecuteNonQuery();
//                            cmd_detalle_cruz_roja.Parameters.Clear();

//                            cmd_detalle_bomberos.Parameters.Clear();
//                            cmd_detalle_bomberos.Parameters.AddWithValue("@no_factura", row["no_factura"].ToString());
//                            cmd_detalle_bomberos.Parameters.AddWithValue("@concepto_id", "00012");
//                            cmd_detalle_bomberos.Parameters.AddWithValue("@concepto", "BOMBEROS");
//                            cmd_detalle_bomberos.Parameters.AddWithValue("@importe", float.Parse(row["crbomb"].ToString()) / 2);
//                            cmd_detalle_bomberos.Parameters.AddWithValue("@impuesto", 0);
//                            cmd_detalle_bomberos.Parameters.AddWithValue("@total", float.Parse(row["crbomb"].ToString()) / 2);
//                            cmd_detalle_bomberos.Parameters.AddWithValue("@importe_abonado", float.Parse(row["abono_crbomb"].ToString()) / 2);
//                            cmd_detalle_bomberos.Parameters.AddWithValue("@impuesto_abonado", 0);
//                            cmd_detalle_bomberos.Parameters.AddWithValue("@total_abonado", float.Parse(row["abono_crbomb"].ToString()) / 2);
//                            cmd_detalle_bomberos.Parameters.AddWithValue("@importe_saldo", (float.Parse(row["crbomb"].ToString()) - float.Parse(row["abono_crbomb"].ToString())) / 2);
//                            cmd_detalle_bomberos.Parameters.AddWithValue("@impuesto_saldo", 0);
//                            cmd_detalle_bomberos.Parameters.AddWithValue("@total_saldo", (float.Parse(row["crbomb"].ToString()) - float.Parse(row["abono_crbomb"].ToString())) / 2);
//                            cmd_detalle_bomberos.Parameters.AddWithValue("@anio", int.Parse(row["anio"].ToString()));
//                            cmd_detalle_bomberos.Parameters.AddWithValue("@bimestre", int.Parse(row["bimestre"].ToString()));
//                            cmd_detalle_bomberos.Parameters.AddWithValue("@estatus", row["estado"].ToString());
//                            cmd_detalle_bomberos.ExecuteNonQuery();
//                            cmd_detalle_bomberos.Parameters.Clear();

//                            if (float.Parse(row["abono_crbomb"].ToString()) > 0)
//                            {
//                                cmd_movimientos_cruz_roja.Parameters.Clear();
//                                cmd_movimientos_cruz_roja.Parameters.AddWithValue("@concepto_id", "00011");
//                                cmd_movimientos_cruz_roja.Parameters.AddWithValue("@importe", float.Parse(row["abono_crbomb"].ToString()) / 2);
//                                string a = row["fecha_pago"].ToString().Substring(0, 10);
//                                DateTime aa = DateTime.Parse(a);
//                                cmd_movimientos_cruz_roja.Parameters.AddWithValue("@fecha_movimiento", aa);
//                                cmd_movimientos_cruz_roja.Parameters.AddWithValue("@facturado", "N");
//                                cmd_movimientos_cruz_roja.Parameters.AddWithValue("@comentarios", row["no_recibo"].ToString());
//                                cmd_movimientos_cruz_roja.Parameters.AddWithValue("@usuario_creo", U_creo);
//                                cmd_movimientos_cruz_roja.Parameters.AddWithValue("@estado_concepto_cobro", "ACTIVO");
//                                cmd_movimientos_cruz_roja.Parameters.AddWithValue("@impuesto", 0);
//                                cmd_movimientos_cruz_roja.Parameters.AddWithValue("@total", float.Parse(row["abono_crbomb"].ToString()) / 2);
//                                cmd_movimientos_cruz_roja.Parameters.AddWithValue("@rpu", row["rpu"].ToString());
//                                cmd_movimientos_cruz_roja.ExecuteNonQuery();
//                                cmd_movimientos_cruz_roja.Parameters.Clear();

//                                cmd_movimientos_bomberos.Parameters.Clear();
//                                cmd_movimientos_bomberos.Parameters.AddWithValue("@concepto_id", "00012");
//                                cmd_movimientos_bomberos.Parameters.AddWithValue("@importe", float.Parse(row["abono_crbomb"].ToString()) / 2);
//                                string b = row["fecha_pago"].ToString().Substring(0, 10);
//                                DateTime bb = DateTime.Parse(b);
//                                cmd_movimientos_bomberos.Parameters.AddWithValue("@fecha_movimiento", bb);
//                                cmd_movimientos_bomberos.Parameters.AddWithValue("@facturado", "N");
//                                cmd_movimientos_bomberos.Parameters.AddWithValue("@comentarios", row["no_recibo"].ToString());
//                                cmd_movimientos_bomberos.Parameters.AddWithValue("@usuario_creo", U_creo);
//                                cmd_movimientos_bomberos.Parameters.AddWithValue("@estado_concepto_cobro", "ACTIVO");
//                                cmd_movimientos_bomberos.Parameters.AddWithValue("@impuesto", 0);
//                                cmd_movimientos_bomberos.Parameters.AddWithValue("@total", float.Parse(row["abono_crbomb"].ToString()) / 2);
//                                cmd_movimientos_bomberos.Parameters.AddWithValue("@rpu", row["rpu"].ToString());
//                                cmd_movimientos_bomberos.ExecuteNonQuery();
//                                cmd_movimientos_bomberos.Parameters.Clear();
//                            }
//                        }

//                        if (float.Parse(row["total_abono"].ToString()) > 0)
//                        {
//                            cmd_movimientos_iva.Parameters.Clear();
//                            cmd_movimientos_iva.Parameters.AddWithValue("@concepto_id", "00014");
//                            cmd_movimientos_iva.Parameters.AddWithValue("@importe", float.Parse(row["abono_IVA_agua"].ToString()) + float.Parse(row["abono_IVA_alcan"].ToString()) + float.Parse(row["abono_IVA_sanea"].ToString()));
//                            DateTime c = DateTime.Parse(row["Fecha_Pago"].ToString().Substring(0, 10));
//                            cmd_movimientos_iva.Parameters.AddWithValue("@fecha_movimiento", c);
//                            cmd_movimientos_iva.Parameters.AddWithValue("@facturado", "N");
//                            cmd_movimientos_iva.Parameters.AddWithValue("@comentarios", row["no_recibo"].ToString());
//                            cmd_movimientos_iva.Parameters.AddWithValue("@usuario_creo", U_creo);
//                            cmd_movimientos_iva.Parameters.AddWithValue("@estado_concepto_cobro", "ACTIVO");
//                            cmd_movimientos_iva.Parameters.AddWithValue("@impuesto", 0);
//                            cmd_movimientos_iva.Parameters.AddWithValue("@total", float.Parse(row["abono_IVA_agua"].ToString()) + float.Parse(row["abono_IVA_alcan"].ToString()) + float.Parse(row["abono_IVA_sanea"].ToString()));
//                            cmd_movimientos_iva.Parameters.AddWithValue("@rpu", row["rpu"].ToString());
//                            cmd_movimientos_iva.ExecuteNonQuery();
//                            cmd_movimientos_iva.Parameters.Clear();
//                        }

//                        if (double.Parse(row["anticipo"].ToString()) > 0)
//                        {
//                            cmd_anticipo.Parameters.Clear();
//                            cmd_anticipo.Parameters.AddWithValue("@rpu_2", row["rpu"].ToString());
//                            cmd_anticipo.Parameters.AddWithValue("@importe_2", double.Parse(row["anticipo"].ToString()));
//                            cmd_anticipo.Parameters.AddWithValue("@saldo_2", double.Parse(row["anticipo"].ToString()));
//                            cmd_anticipo.Parameters.AddWithValue("@estatus_2", "PORAPLICAR");
//                            cmd_anticipo.ExecuteNonQuery();
//                            cmd_anticipo.Parameters.Clear();
//                        }

//                        //cmd_encabezado.ExecuteNonQuery();
//                        //cmd_detalle_agua.ExecuteNonQuery();
//                        //cmd_detalle_alcan.ExecuteNonQuery();
//                        //cmd_detalle_sanea.ExecuteNonQuery();
//                        //if (float.Parse(row["recagua"].ToString()) > 0 || float.Parse(row["recalcan"].ToString()) > 0 || float.Parse(row["recsanea"].ToString()) > 0)
//                        //{
//                        //    cmd_detalle_recagua.ExecuteNonQuery();
//                        //    cmd_detalle_recalcan.ExecuteNonQuery();
//                        //    cmd_detalle_recsanea.ExecuteNonQuery();
//                        //}
//                        //if (float.Parse(row["crbomb"].ToString()) > 0)
//                        //{
//                        //    cmd_detalle_cruz_roja.ExecuteNonQuery();
//                        //    cmd_detalle_bomberos.ExecuteNonQuery();
//                        //}
//                        //if (double.Parse(row["anticipo"].ToString()) >= 1)
//                        //{
//                        //    cmd_anticipo.ExecuteNonQuery();
//                        //}
//                        //if (float.Parse(row["total_abono"].ToString()) > 0)
//                        //{
//                        //    cmd_recibos_cobros.ExecuteNonQuery();
//                        //    cmd_movimientos_agua.ExecuteNonQuery();
//                        //    cmd_movimientos_alcan.ExecuteNonQuery();
//                        //    cmd_movimientos_sanea.ExecuteNonQuery();
//                        //    if (float.Parse(row["abono_recagua"].ToString()) > 0 || float.Parse(row["abono_recalcan"].ToString()) > 0 || float.Parse(row["abono_recsanea"].ToString()) > 0)
//                        //    {
//                        //        cmd_movimientos_recagua.ExecuteNonQuery();
//                        //        cmd_movimientos_recalcan.ExecuteNonQuery();
//                        //        cmd_movimientos_recsanea.ExecuteNonQuery();
//                        //    }
//                        //    if (float.Parse(row["abono_crbomb"].ToString()) > 0)
//                        //    {
//                        //        cmd_movimientos_cruz_roja.ExecuteNonQuery();
//                        //        cmd_movimientos_bomberos.ExecuteNonQuery();
//                        //    }
//                        //    cmd_movimientos_iva.ExecuteNonQuery();
//                        //}

//                        pBar1.PerformStep();
//                    }
//                    conn.Close();
//                    watch.Stop();
//                    TimeSpan ts = watch.Elapsed;
//                    string elapsedtime = string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);

//                    if (rdb_1.Checked)
//                    {
//                        rdb_1.Text = "Migrado!!";
//                        rdb_1.Checked = false;
//                        rdb_1.Enabled = false;
//                    }
//                    if (rdb_3.Checked)
//                    {
//                        rdb_3.Text = "Migrado!!";
//                        rdb_3.Checked = false;
//                        rdb_3.Enabled = false;
//                    }

//                    MessageBox.Show("Datos Migrados!!" + "\n" + "Tiempo: " + elapsedtime, "Migración Recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    pBar1.Visible = false;
//                    btn_migrar.Enabled = false;
//                }
//            }
//            else
//            {
//                //No hace nada
//            }
            #endregion
        }

        private void rdb_1_CheckedChanged_1(object sender, EventArgs e)
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
