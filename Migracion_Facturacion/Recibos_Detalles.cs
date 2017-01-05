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
    public partial class Recibos_Detalles : Form
    {
        string db_origen = "Simapag_09_01_2016";
        string db_destino = "Simapag_20161015"; //Simapag_Depurado

        public Recibos_Detalles()
        {
            InitializeComponent();
        }

        private void btn_regresar_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            this.Hide();
            obj.Show();
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            #region Importacion de datos...
            class_detalles obj = new class_detalles();
            if (rdb_1.Checked)
            {
                dtg_origen.DataSource = obj.Cargar_Sanicones_Activas(db_origen, db_destino);
                lbl_origen.Text = dtg_origen.Rows.Count.ToString();
                MessageBox.Show("Caso 1 Cargado", "Migración Recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_import.Enabled = false;
                btn_copy.Enabled = true;
            }
            if (rdb_2.Checked)
            {
                dtg_origen.DataSource = obj.Cargar_Sanicones_Reincidir(db_origen, db_destino);
                lbl_origen.Text = dtg_origen.Rows.Count.ToString();
                MessageBox.Show("Caso 2 Cargado", "Migración Recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_import.Enabled = false;
                btn_copy.Enabled = true;
            }
            #endregion
        }

        private void btn_copy_Click(object sender, EventArgs e)
        {
            if (rdb_1.Checked)
            {
                #region Escenarios 1 y 2 ...
                pBar1.Visible = true;
                pBar1.Minimum = 1;
                pBar1.Maximum = dtg_origen.Rows.Count * 2;
                pBar1.Value = 1;
                pBar1.Step = 1;
                btn_copy.Enabled = false;

                string[] rpu = new string[dtg_origen.Rows.Count];
                string[] Predio_ID = new string[dtg_origen.Rows.Count];
                string[] Giro_ID = new string[dtg_origen.Rows.Count];
                string[] Abrebiatura = new string[dtg_origen.Rows.Count];
                string[] inspeccion = new string[dtg_origen.Rows.Count];
                string[] Concepto_Cobro = new string[dtg_origen.Rows.Count];
                string[] folio = new string[dtg_origen.Rows.Count];
                string[] fecha_creo = new string[dtg_origen.Rows.Count];
                string[] Observaciones = new string[dtg_origen.Rows.Count];
                string[] auto_id = new string[dtg_origen.Rows.Count];


                int i = 0;
                foreach (DataGridViewRow row in dtg_origen.Rows)
                {
                    rpu[i] = row.Cells[0].Value.ToString();
                    Predio_ID[i] = row.Cells[1].Value.ToString();
                    Giro_ID[i] = row.Cells[2].Value.ToString();
                    Abrebiatura[i] = row.Cells[3].Value.ToString();
                    inspeccion[i] = row.Cells[4].Value.ToString();
                    i++;

                }

                for (int j = 0; j < dtg_origen.Rows.Count; j++)
                {
                    var divicion = inspeccion[j].Split(' ');
                    if (divicion.Length > 3)
                    {
                        int a = divicion[1].IndexOf("/");

                        if (a > 0)
                        {
                            fecha_creo[j] = divicion[1];
                            folio[j] = divicion[2];
                            for (int gg = 3; gg < divicion.Count(); gg++)
                            {
                                Observaciones[j] = Observaciones[j] + " " + divicion[gg];

                            }
                            if (Observaciones[j] == " RECONEXIÒN POR USUARIO" || Observaciones[j] == " RECONEXIÓN POR USUARIO")
                            {
                                Observaciones[j] = "RECONEXION";
                            }
                            if (Observaciones[j] == " DAÑOS A LAS INSTLACI")
                            {
                                Observaciones[j] = "DAÑOS A LAS INSTALACIONES";
                            }
                            if (Observaciones[j] == " MANIPULACION DE MED")
                            {
                                Observaciones[j] = "MANIPULACION-DAÑOS DE MEDIDOR";
                            }

                            if (Observaciones[j] == " MANIPU DE INSTA DRENAJE")
                            {
                                Observaciones[j] = "MANIPULACION DE INSTALACIONES SANITARIAS O DE DRENAJE";
                            }
                            if (Observaciones[j] == " MANIPULACION  DE MEDIDOR")
                            {
                                Observaciones[j] = "MANIPULACION-DAÑOS DE MEDIDOR";
                            }
                            if (Observaciones[j] == " REUBICACIÓN DE MEDIDOR")
                            {
                                Observaciones[j] = "REUBICACION DE MEDIDOR";
                            }
                            if (Observaciones[j] == " X PASAR AGUA")
                            {
                                Observaciones[j] = "PASAR AGUA";
                            }
                            if (Observaciones[j] == " DE MED")
                            {
                                Observaciones[j] = "MANIPULACION-DAÑOS DE MEDIDOR";
                            }
                            if (Observaciones[j] == " RECONEXI POR USUARIO")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " RECONEX POR USUARIO")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " RECONEX POR USUARIO")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " RECONEX POR USUARIO")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " RECONEXIÓN POR USUARIIO")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " MANIPULACIÓN DE MEDIDOR")
                            {
                                Observaciones[j] = "MANIPULACION-DAÑOS DE MEDIDOR";
                            }
                            if (Observaciones[j] == " INSTALAR MEC DE SUCCIÓN")
                            {
                                Observaciones[j] = "INSTALAR MECANISMOS DE SUCCION A LA RED DE AGUA POTABLE";
                            }
                            if (Observaciones[j] == " MANIPULACION DE MEDIDOR")
                            {
                                Observaciones[j] = "MANIPULACION-DAÑOS DE MEDIDOR";
                            }
                            if (Observaciones[j] == " DAÑOS A LAS INSTLACIO")
                            {
                                Observaciones[j] = "DAÑOS A LAS INSTALACIONES DE SIMAPAG (AGUA POTABLE)";
                            }
                            if (Observaciones[j] == " DERIVACIÓN EN MEDIDOR")
                            {
                                Observaciones[j] = "DERIVACIONES DE AGUA POTABLE";
                            }
                            if (Observaciones[j] == " RECONEX POR USUARIIO")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " DAÑOS A LS INSTALAC")
                            {
                                Observaciones[j] = "DAÑOS A LAS INSTALACIONES DE SIMAPAG (AGUA POTABLE)";
                            }
                            if (Observaciones[j] == " MANIPULACION DE INT SAN")
                            {
                                Observaciones[j] = "MANIPULACION DE INSTALACIONES SANITARIAS O DE DRENAJE";
                            }
                            if (Observaciones[j] == " MANIPULACION DE MEDI")
                            {
                                Observaciones[j] = "MANIPULACION-DAÑOS DE MEDIDOR";
                            }



                        }
                        if (a == 0)
                        {
                            folio[j] = divicion[1];
                            fecha_creo[j] = divicion[divicion.Count() - 1];
                            for (int gg = 2; gg < divicion.Count() - 1; gg++)
                            {
                                Observaciones[j] = Observaciones[j] + " " + divicion[gg];

                            }

                            if (Observaciones[j] == " RECONEXIÒN POR USUARIO" || Observaciones[j] == "  RECONEXIÓN POR USUARIO")
                            {
                                Observaciones[j] = "RECONEXION";
                            }
                            if (Observaciones[j] == " DAÑOS A LAS INSTLACI")
                            {
                                Observaciones[j] = "DAÑOS A LAS INSTALACIONES";
                            }
                            if (Observaciones[j] == " MANIPULACION DE MED")
                            {
                                Observaciones[j] = "MANIPULACION-DAÑOS DE MEDIDOR";
                            }
                            if (Observaciones[j] == " MANIPU DE INSTA DRENAJE")
                            {
                                Observaciones[j] = "MANIPULACION DE INSTALACIONES SANITARIAS O DE DRENAJE";
                            }
                            if (Observaciones[j] == " MANIPULACION  DE MEDIDOR")
                            {
                                Observaciones[j] = "MANIPULACION-DAÑOS DE MEDIDOR";
                            }
                            if (Observaciones[j] == " REUBICACIÓN DE MEDIDOR")
                            {
                                Observaciones[j] = "REUBICACION DE MEDIDOR";
                            }
                            if (Observaciones[j] == " X PASAR AGUA")
                            {
                                Observaciones[j] = "PASAR AGUA";
                            }
                            if (Observaciones[j] == " DE MED")
                            {
                                Observaciones[j] = "MANIPULACION-DAÑOS DE MEDIDOR";
                            }

                            if (Observaciones[j] == " RECONEXI POR USUARIO")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " RECONEX POR USUARIO")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " RECONEX POR USUARIO")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " RECONEX POR USUARIO")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " RECONEXIÓN POR USUARIIO")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " MANIPULACIÓN DE MEDIDOR")
                            {
                                Observaciones[j] = "MANIPULACION-DAÑOS DE MEDIDOR";
                            }
                            if (Observaciones[j] == " INSTALAR MEC DE SUCCIÓN")
                            {
                                Observaciones[j] = "INSTALAR MECANISMOS DE SUCCION A LA RED DE AGUA POTABLE";
                            }
                            if (Observaciones[j] == " MANIPULACION DE MEDIDOR")
                            {
                                Observaciones[j] = "MANIPULACION-DAÑOS DE MEDIDOR";
                            }
                            if (Observaciones[j] == " DAÑOS A LAS INSTLACIO")
                            {
                                Observaciones[j] = "DAÑOS A LAS INSTALACIONES DE SIMAPAG (AGUA POTABLE)";
                            }
                            if (Observaciones[j] == " DERIVACIÓN EN MEDIDOR")
                            {
                                Observaciones[j] = "DERIVACIONES DE AGUA POTABLE";
                            }
                            if (Observaciones[j] == " RECONEX POR USUARIIO")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " DAÑOS A LS INSTALAC")
                            {
                                Observaciones[j] = "DAÑOS A LAS INSTALACIONES DE SIMAPAG (AGUA POTABLE)";
                            }



                            if (Observaciones[j] == " MANIPULACION DE INT SAN")
                            {
                                Observaciones[j] = "MANIPULACION DE INSTALACIONES SANITARIAS O DE DRENAJE";
                            }
                            if (Observaciones[j] == " MANIPULACION DE MEDI")
                            {
                                Observaciones[j] = "MANIPULACION-DAÑOS DE MEDIDOR";
                            }






                        }

                    }

                }
                class_detalles obj = new class_detalles();
                DataTable aa;
                for (int a = 0; a < dtg_origen.Rows.Count; a++)
                {
                    if (Observaciones[a] != null)
                    {
                        aa = obj.Cargar_Concepto_Cobro(db_origen, db_destino, Observaciones[a].Trim());
                        if (aa != null)
                        {
                            foreach (DataRow row in aa.Rows)
                            {
                                Concepto_Cobro[a] = row["Concepto_ID"].ToString();
                            }
                        }
                    }
                }

                DataTable Dt_Datos = new DataTable();

                Dt_Datos.Columns.Add("Estatus");
                Dt_Datos.Columns.Add("Observaciones");
                Dt_Datos.Columns.Add("Fk_Predio");
                Dt_Datos.Columns.Add("Usuario_Creo");
                Dt_Datos.Columns.Add("Fecha_Creo");
                Dt_Datos.Columns.Add("Rpu");
                Dt_Datos.Columns.Add("Recibio");
                Dt_Datos.Columns.Add("Folio");
                Dt_Datos.Columns.Add("Dias_Descuento");
                Dt_Datos.Columns.Add("Tarifa_ID");
                Dt_Datos.Columns.Add("Abrebiatura");
                
                for (int indice = 0; indice < dtg_origen.Rows.Count; indice++)

                {
                    if (!string.IsNullOrEmpty(Concepto_Cobro[indice]))
                    {
                        DataRow Dr = Dt_Datos.NewRow();
                        Dr["Estatus"] = "ACTIVO";
                        Dr["Observaciones"] = Concepto_Cobro[indice];
                        Dr["Fk_Predio"] = Predio_ID[indice];
                        Dr["Usuario_Creo"] = "ROSE NO";
                        Dr["Fecha_Creo"] = fecha_creo[indice];
                        Dr["Rpu"] = rpu[indice];
                        Dr["Recibio"] = "N/A";
                        Dr["Folio"] = folio[indice];
                        Dr["Dias_Descuento"] = 3;
                        Dr["Tarifa_ID"] = Giro_ID[indice];
                        Dr["Abrebiatura"] = Abrebiatura[indice];
                        Dt_Datos.Rows.Add(Dr);
                    }
                }

                dtg_destino.DataSource = Dt_Datos;

               
          




                pBar1.PerformStep();
                

                lbl_destino.Text = dtg_destino.Rows.Count.ToString();
                if (rdb_1.Checked)
                    MessageBox.Show("Caso 1 Copiado!", "Migración Recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Caso 2 Copiado!", "Migración Recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_migrar.Enabled = true;
                pBar1.Visible = false;
                #endregion
            }
            if (rdb_2.Checked)
            {
                #region Escenarios 1 y 2 ...
                pBar1.Visible = true;
                pBar1.Minimum = 1;
                pBar1.Maximum = dtg_origen.Rows.Count * 2;
                pBar1.Value = 1;
                pBar1.Step = 1;
                btn_copy.Enabled = false;

                string[] rpu = new string[dtg_origen.Rows.Count];
                string[] Predio_ID = new string[dtg_origen.Rows.Count];
                string[] Giro_ID = new string[dtg_origen.Rows.Count];
                string[] Abrebiatura = new string[dtg_origen.Rows.Count];
                string[] inspeccion = new string[dtg_origen.Rows.Count];
                string[] Concepto_Cobro = new string[dtg_origen.Rows.Count];
                string[] folio = new string[dtg_origen.Rows.Count];
                string[] fecha_creo = new string[dtg_origen.Rows.Count];
                string[] Observaciones = new string[dtg_origen.Rows.Count];
                string[] auto_id = new string[dtg_origen.Rows.Count];


                int i = 0;
                foreach (DataGridViewRow row in dtg_origen.Rows)
                {
                    rpu[i] = row.Cells[0].Value.ToString();
                    Predio_ID[i] = row.Cells[1].Value.ToString();
                    Giro_ID[i] = row.Cells[2].Value.ToString();
                    Abrebiatura[i] = row.Cells[3].Value.ToString();
                    inspeccion[i] = row.Cells[4].Value.ToString();
                    i++;

                }

                for (int j = 0; j < dtg_origen.Rows.Count; j++)
                {
                    var divicion = inspeccion[j].Split(' ');
                    if (divicion.Length > 3)
                    {
                        int a = divicion[1].IndexOf("/");

                        if (a > 0)
                        {
                            fecha_creo[j] = divicion[1];
                            folio[j] = divicion[2];
                            for (int gg = 3; gg < divicion.Count(); gg++)
                            {
                                Observaciones[j] = Observaciones[j] + " " + divicion[gg];

                            }

                            if (Observaciones[j] == " REINCID EN RECONEXIÒN")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " REINCIDENCIA EN RECON")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " REINCID EN RECONEXIÓN")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " REINCIDENCIA EN REC")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " EN REC")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " REINCIDENCIA EN RECONEX")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " REINCIDENCIA EN RECO")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " REINC EN RECONEXIÒN")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " REINCIDENCIA EN RECONEX")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " REINCIDENCIA EN RECO")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " REINCID EN RECONEX")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " REINC EN MANIPU DE MED")
                            {
                                Observaciones[j] = "MANIPULACION-DAÑOS DE MEDIDOR";
                            }
                            if (Observaciones[j] == " REINCIDENA EN REC")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " REINC EN REC POR USUARIO")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " REINC EN RECONEXION")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " REINCIDENCIA EN RECONE")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }


                        }
                        if (a <= 0)
                        {
                            folio[j] = divicion[1];
                            fecha_creo[j] = divicion[divicion.Count() - 1];
                            for (int gg = 2; gg < divicion.Count() - 1; gg++)
                            {
                                Observaciones[j] = Observaciones[j] + " " + divicion[gg];

                            }

                           
                           if (Observaciones[j] == " REINCID EN RECONEXIÒN")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " REINCIDENCIA EN RECON")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                            if (Observaciones[j] == " REINCID EN RECONEXIÓN")
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                             if (Observaciones[j] == " REINCIDENCIA EN REC" )
                            {
                                Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                            }
                             if (Observaciones[j] == " EN REC")
                             {
                                 Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                             }

                             if (Observaciones[j] == " REINCIDENCIA EN RECONEX")
                             {
                                 Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                             }
                             if (Observaciones[j] == " REINCIDENCIA EN RECO")
                             {
                                 Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                             }
                             if (Observaciones[j] == " REINC EN RECONEXIÒN")
                             {
                                 Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                             }
                             if (Observaciones[j] == " REINCIDENCIA EN RECONEX")
                             {
                                 Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                             }
                             if (Observaciones[j] == " REINCIDENCIA EN RECO" )
                             {
                                 Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                             }
                             if (Observaciones[j] == " REINCID EN RECONEX")
                             {
                                 Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                             }
                            
                              if (Observaciones[j] == " REINC EN MANIPU DE MED" )
                             {
                                 Observaciones[j] = "MANIPULACION-DAÑOS DE MEDIDOR";
                             }
                             if (Observaciones[j] == " REINCIDENA EN REC")
                             {
                                 Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                             }
                              if (Observaciones[j] == " REINC EN REC POR USUARIO" )
                             {
                                 Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                             }
                             if (Observaciones[j] ==  " REINC EN RECONEXION" )
                             {
                                 Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                             }
                             if (Observaciones[j] == " REINCIDENCIA EN RECONE")
                             {
                                 Observaciones[j] = "RECONEXION DE SERVICIO POR PERSONA AJENA AL ORGANISMO";
                             }
                            


                        }

                    }

                }
                class_detalles obj = new class_detalles();
                DataTable aa;
                for (int a = 0; a < dtg_origen.Rows.Count; a++)
                {
                    if (Observaciones[a] != null)
                    {
                        aa = obj.Cargar_Concepto_Cobro(db_origen, db_destino, Observaciones[a].Trim());
                        if (aa != null)
                        {
                            foreach (DataRow row in aa.Rows)
                            {
                                Concepto_Cobro[a] = row["Concepto_ID"].ToString();
                            }
                        }
                    }
                }

                DataTable Dt_Datos = new DataTable();

                Dt_Datos.Columns.Add("Estatus");
                Dt_Datos.Columns.Add("Observaciones");
                Dt_Datos.Columns.Add("Fk_Predio");
                Dt_Datos.Columns.Add("Usuario_Creo");
                Dt_Datos.Columns.Add("Fecha_Creo");
                Dt_Datos.Columns.Add("Rpu");
                Dt_Datos.Columns.Add("Recibio");
                Dt_Datos.Columns.Add("Folio");
                Dt_Datos.Columns.Add("Dias_Descuento");
                Dt_Datos.Columns.Add("Tarifa_ID");
                Dt_Datos.Columns.Add("Abrebiatura");

                for (int indice = 0; indice < dtg_origen.Rows.Count; indice++)
                {
                    if (!string.IsNullOrEmpty(Concepto_Cobro[indice]))
                    {
                        DataRow Dr = Dt_Datos.NewRow();
                        Dr["Estatus"] = "ACTIVO";
                        Dr["Observaciones"] = Concepto_Cobro[indice];
                        Dr["Fk_Predio"] = Predio_ID[indice];
                        Dr["Usuario_Creo"] = "ROSE SI";
                        Dr["Fecha_Creo"] = fecha_creo[indice];
                        Dr["Rpu"] = rpu[indice];
                        Dr["Recibio"] = "N/A";
                        Dr["Folio"] = folio[indice];
                        Dr["Dias_Descuento"] = 3;
                        Dr["Tarifa_ID"] = Giro_ID[indice];
                        Dr["Abrebiatura"] = Abrebiatura[indice];

                        Dt_Datos.Rows.Add(Dr);
                    }
                }

                dtg_destino.DataSource = Dt_Datos;







                pBar1.PerformStep();


                lbl_destino.Text = dtg_destino.Rows.Count.ToString();
                if (rdb_1.Checked)
                    MessageBox.Show("Caso 1 Copiado!", "Migración Recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Caso 2 Copiado!", "Migración Recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_migrar.Enabled = true;
                pBar1.Visible = false;
                #endregion
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
            #region Migracion datos dtg_Destino...
            DialogResult result;
            result = MessageBox.Show("¿Seguro de querer continuar?", "Migración Recibos", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                pBar1.Visible = true;
                pBar1.Minimum = 1;
                pBar1.Maximum = dtg_destino.Rows.Count;
                pBar1.Value = 1;
                pBar1.Step = 1;
                SqlConnection conn = new SqlConnection("Data Source=172.16.0.115;Initial Catalog=" + db_destino + ";User ID=usrsimapag;Password=C0nt3l16");
                string query = "INSERT INTO Ope_Cor_Sanciones(Estatus,Observaciones,Fk_Predio,Usuario_Creo,Fecha_Creo,Rpu,Recibio,Folio,Dias_Descuento,RFC_S) "
                    + "VALUES(@estatus,@observaciones,@fk_predio,@usuario_creo,@fecha_creo,@rpu,@recibio,@folio,@dias_descuento,@giro)";
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    //int k = 0;
                    foreach (DataGridViewRow row in dtg_destino.Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@estatus", row.Cells[0].Value.ToString());
                        cmd.Parameters.AddWithValue("@observaciones", row.Cells[1].Value.ToString());
                        cmd.Parameters.AddWithValue("@fk_predio", row.Cells[2].Value.ToString());
                        cmd.Parameters.AddWithValue("@usuario_creo", row.Cells[3].Value.ToString());
                        cmd.Parameters.AddWithValue("@fecha_creo", row.Cells[4].Value.ToString());
                        cmd.Parameters.AddWithValue("@rpu", row.Cells[5].Value.ToString());
                        cmd.Parameters.AddWithValue("@recibio", row.Cells[6].Value.ToString());
                        cmd.Parameters.AddWithValue("@folio", row.Cells[7].Value.ToString());
                        cmd.Parameters.AddWithValue("@dias_descuento", int.Parse(row.Cells[8].Value.ToString()));
                        cmd.Parameters.AddWithValue("@giro", row.Cells[10].Value.ToString());
                        
                        cmd.ExecuteNonQuery();
                        pBar1.PerformStep();
                    }
                    conn.Close();
                    MessageBox.Show("Datos Migrados!!", "Migración Detalles", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error 404", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pBar1.Visible = false;
                }
            }
            #endregion
        }

        private void Recibos_Detalles_Load(object sender, EventArgs e)
        {
            rdb_1.Checked = true;
            btn_import.Enabled = true;
            btn_copy.Enabled = false;
            btn_migrar.Enabled = false;
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
