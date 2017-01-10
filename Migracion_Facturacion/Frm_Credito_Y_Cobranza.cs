using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Migracion_Facturacion
{
    public partial class Frm_Credito_Y_Cobranza : Form
    {
        //string db_destino = "Simapag_20161015";  // <--------------------- Base de datos (escructura)
        //string db_Simapag_Origen = "Simapag_09_01_2016";  // <--------------------- Base de datos (lectura)
        

        ///*********************************************************************************
        /// <summary>
        /// 
        /// </summary>
        ///*********************************************************************************
        public Frm_Credito_Y_Cobranza()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;
        }

        private DataGridView Grid_Local;
        private DataTable Dt_Suspension;
        private DataTable Dt_Notificacion;

        ///*********************************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Grid_Local_Resultado"></param>
        ///*********************************************************************************
        private void Grid_Local_Resultado(DataGridView Grid_Resultado)
        {
            Grid_Local = Grid_Resultado;
        }

        ///*********************************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Dt_Suspension_Resultado"></param>
        ///*********************************************************************************
        private void Dt_Suspension_Resultado(DataTable Dt_)
        {
            Dt_Suspension = Dt_;
        }

        ///*********************************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Grid_Momias_Local"></param>
        ///*********************************************************************************
        private void Dt_Notificaciones_Resultado(DataTable Dt_)
        {
            Dt_Notificacion = Dt_;
        }
        ///*********************************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///*********************************************************************************
        private void btn_regresar_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            this.Hide();
            obj.Show();
        }

        ///*********************************************************************************
        /// <summary>
        /// 
        /// </summary>
        ///*********************************************************************************
        private void Consultar_Suspensiones()
        {
            Cls_Credito_Y_Cobranza Obj_Credito_Cobranza = new Cls_Credito_Y_Cobranza();
            DataTable Dt_Consulta = new DataTable();
            DataTable Dt_Consulta_Diverso = new DataTable();
            Double Db_Total = 0;
            Int64 Int_No_Suspension = 1;

            try
            {
                Btn_Consultar.Enabled = false;
                Btn_Guardar.Enabled = false;

                Pro_Bar_1.Visible = true;
                Pro_Bar_1.Minimum = 1;
                Pro_Bar_1.Maximum = 3;
                Pro_Bar_1.Value = 1;
                Pro_Bar_1.Step = 1;

                Stopwatch watch = new Stopwatch();
                watch.Start();

                Lbl_Proceso_General.Text = "Fase 1/2  Consultando cuentas a suspender";
                Lbl_Cont_Cuentas.Text = "";
                Lbl_Etiqueta_Diversos.Text = "";

                Pro_Bar_2.Visible = true;
                Pro_Bar_2.Minimum = 1;
                Pro_Bar_2.Maximum = 1;
                Pro_Bar_2.Value = 1;
                Pro_Bar_2.Step = 1;

                Stopwatch watch2 = new Stopwatch();
                watch2.Start();
                Dt_Consulta = Obj_Credito_Cobranza.Consultar_Cuentas_A_Suspender();
                Pro_Bar_2.PerformStep();
                watch2.Stop();



                Lbl_Cont_Cuentas.Text = "Cuentas (" + Dt_Consulta.Rows.Count.ToString() + ")";
                Grid_1.DataSource = Dt_Consulta;

                //*************************************************************************************
                //*************************************************************************************
                //*************************************************************************************
                Lbl_Proceso_General.Text = "Fase 2/2  Consultando diversos (cortes)";

                Pro_Bar_1.PerformStep();


                Pro_Bar_2.Visible = true;
                Pro_Bar_2.Minimum = 1;
                Pro_Bar_2.Maximum = Dt_Consulta.Rows.Count;
                Pro_Bar_2.Value = 1;
                Pro_Bar_2.Step = 1;



                for (int Cont_For = 0; Cont_For <= Dt_Consulta.Rows.Count - 1; Cont_For++)
                {
                    //  consultamos el diverso

                    Dt_Consulta_Diverso = Obj_Credito_Cobranza.Consultar_No_Diverso(Dt_Consulta.Rows[Cont_For]["Rpu"].ToString());
                    Dt_Consulta.Rows[Cont_For].BeginEdit();

                    Dt_Consulta.Rows[Cont_For]["No_Suspension"] = String.Format("{0:0000000000}", Int_No_Suspension);
                    Int_No_Suspension++;

                    foreach (DataRow Registro_Diverso in Dt_Consulta_Diverso.Rows)
                    {
                        Dt_Consulta.Rows[Cont_For]["no_diverso"] = Registro_Diverso["No_Diverso"].ToString();
                        Db_Total++;
                    }


                    Dt_Consulta.Rows[Cont_For].EndEdit();
                    Dt_Consulta.Rows[Cont_For].AcceptChanges();

                    Pro_Bar_2.PerformStep();
                }


               

                //*************************************************************************************
                //*************************************************************************************
                //*************************************************************************************
                
                Pro_Bar_1.PerformStep();
                Grid_Local.DataSource = Dt_Consulta;
                Dt_Suspension_Resultado(Dt_Consulta);
                Btn_Consultar.Enabled = true;
                Btn_Guardar.Enabled = true;

                watch.Stop();
                watch2.Stop();


                Lbl_Etiqueta_Diversos.Text = "Cuentas con no. diverso ("  +
                        Db_Total.ToString() +
                        " de " + 
                        Dt_Consulta.Rows.Count.ToString() + ")";

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error [Consultar_Suspensiones]: " + Ex.Message, "Error");
                throw new Exception("Error [Consultar_Suspensiones]: " + Ex.Message);
            }
        }



        ///*********************************************************************************
        /// <summary>
        /// 
        /// </summary>
        ///*********************************************************************************
        private void Consultar_Notificaciones()
        {
            Cls_Credito_Y_Cobranza Obj_Credito_Cobranza = new Cls_Credito_Y_Cobranza();
            DataTable Dt_Consulta = new DataTable();
            DataTable Dt_Consulta_Diverso = new DataTable();
            Double Db_Total = 0;
            Int64 Int_No_Notificacion = 1;

            try
            {
                Btn_Consultar.Enabled = false;
                Btn_Guardar.Enabled = false;

                Pro_Bar_1.Visible = true;
                Pro_Bar_1.Minimum = 1;
                Pro_Bar_1.Maximum = 3;
                Pro_Bar_1.Value = 1;
                Pro_Bar_1.Step = 1;

                Stopwatch watch = new Stopwatch();
                watch.Start();

                Lbl_Proceso_General.Text = "fase 1/2 Consultando cuentas a notificar (consultando cartera vencida)";
                Lbl_Cont_Cuentas.Text = "";
                Lbl_Etiqueta_Diversos.Text = "";

                Pro_Bar_2.Visible = true;
                Pro_Bar_2.Minimum = 1;
                Pro_Bar_2.Maximum = 2;
                Pro_Bar_2.Value = 1;
                Pro_Bar_2.Step = 1;

                Stopwatch watch2 = new Stopwatch();
                watch2.Start();
                Dt_Consulta = Obj_Credito_Cobranza.Consultar_Predios_Notificacion_Simapag();
                Pro_Bar_2.PerformStep();
                watch2.Stop();



                Lbl_Cont_Cuentas.Text = "Cuentas (" + Dt_Consulta.Rows.Count.ToString() + ")";
                Grid_1.DataSource = Dt_Consulta;

                //*************************************************************************************
                //*************************************************************************************
                //*************************************************************************************

                Lbl_Proceso_General.Text = "fase 2/2 Asignando ID";
                Pro_Bar_2.Visible = true;
                Pro_Bar_2.Minimum = 1;
                if (Dt_Consulta != null && Dt_Consulta.Rows.Count > 0)
                {
                    Pro_Bar_2.Maximum = Dt_Consulta.Rows.Count;
                }
                else
                {
                    Pro_Bar_2.Maximum = 1;
                }
                Pro_Bar_2.Value = 1;
                Pro_Bar_2.Step = 1;


                Dt_Consulta.Columns.Add("no_notificacion");
                Dt_Consulta.Columns["no_notificacion"].SetOrdinal(0);
                Dt_Consulta.AcceptChanges();

                foreach (DataColumn Columna in Dt_Consulta.Columns)
                {
                    Columna.ReadOnly = false;
                }

                foreach (DataRow Registro in Dt_Consulta.Rows)
                {
                    Registro.BeginEdit();

                    Registro["no_notificacion"] = string.Format("{0:0000000000}", Int_No_Notificacion);
                    Registro.EndEdit();
                    Registro.AcceptChanges();

                    Pro_Bar_2.PerformStep();
                    Int_No_Notificacion++;

                }
                
                Pro_Bar_1.PerformStep();
                

                //*************************************************************************************
                //*************************************************************************************
                //*************************************************************************************

                //Lbl_Proceso_General.Text = "fase 3/3 Buscando fecha asignacion";
                //Pro_Bar_2.Visible = true;
                //Pro_Bar_2.Minimum = 1;
                //if (Dt_Consulta != null && Dt_Consulta.Rows.Count > 0)
                //{
                //    Pro_Bar_2.Maximum = Dt_Consulta.Rows.Count;
                //}
                //else
                //{
                //    Pro_Bar_2.Maximum = 1;
                //}
                //Pro_Bar_2.Value = 1;
                //Pro_Bar_2.Step = 1;

                //DataTable Dt_Fecha = new DataTable();
                //Int_No_Notificacion = 0;
                //foreach (DataRow Registro in Dt_Consulta.Rows)
                //{
                //    Registro.BeginEdit();

                //    //if (Int_No_Notificacion == 157)
                //    //{

                //    Dt_Fecha = Obj_Credito_Cobranza.Consultar_Fecha_Asignacion(db_Simapag_Origen, Registro["Rpu"].ToString());

                //    foreach (DataRow Registro_Fecha in Dt_Fecha.Rows)
                //    {
                //        Registro["Fecha_Notificacion"] = Registro_Fecha["Corte"].ToString();
                //    }
                //    Registro.EndEdit();
                //    Registro.AcceptChanges();
                //    //}


                //    Pro_Bar_2.PerformStep();
                //    Int_No_Notificacion++;

                //    Lbl_Cont_Cuentas.Text = Int_No_Notificacion.ToString();

                //}


                //*************************************************************************************
                //*************************************************************************************
                //*************************************************************************************

                Pro_Bar_1.PerformStep();
                Grid_Local.DataSource = Dt_Consulta;
                Dt_Notificaciones_Resultado(Dt_Consulta);
                Btn_Consultar.Enabled = true;
                Btn_Guardar.Enabled = true;

                watch.Stop();
                watch2.Stop();


            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error : " + Ex.Message, "Error", MessageBoxButtons.OK);
                throw new Exception("Error [Consultar_Notificaciones]: " + Ex.Message);
            }
        }


        
        ///*********************************************************************************
        /// <summary>
        /// 
        /// </summary>
        ///*********************************************************************************
        private void Ingresar_Suspensiones()
        {
            Cls_Credito_Y_Cobranza Obj_Credito_Cobranza = new Cls_Credito_Y_Cobranza();
            DataTable Dt_Consulta = new DataTable();
            DataTable Dt_Consulta_Diverso = new DataTable();
            Double Db_Total = 0;
            try
            {
                Btn_Consultar.Enabled = false;
                Btn_Guardar.Enabled = false;

                Lbl_Proceso_General.Text = "Ingresando suspensiones";

                Obj_Credito_Cobranza.Insertar_Suspension_Servicio(Dt_Suspension, Pro_Bar_1, Pro_Bar_2);

                Btn_Consultar.Enabled = true;
                Btn_Guardar.Enabled = false;

                Grid_Local.DataSource = null;

                MessageBox.Show("Migración exitosa","Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error [Ingresar_Suspensiones] : " + Ex.Message, "Error", MessageBoxButtons.OK);
                throw new Exception("Error [Ingresar_Suspensiones]: " + Ex.Message);
            }
        }

        ///*********************************************************************************
        /// <summary>
        /// 
        /// </summary>
        ///*********************************************************************************
        private void Ingresar_Notificacion()
        {
            Cls_Credito_Y_Cobranza Obj_Credito_Cobranza = new Cls_Credito_Y_Cobranza();
            DataTable Dt_Consulta = new DataTable();
            DataTable Dt_Consulta_Diverso = new DataTable();
            Double Db_Total = 0;
            try
            {
                Btn_Consultar.Enabled = false;
                Btn_Guardar.Enabled = false;

                Lbl_Proceso_General.Text = "Ingresando notificaciones";

                Obj_Credito_Cobranza.Insertar_Notificacion_SIMAPAG(Dt_Notificacion, Pro_Bar_1, Pro_Bar_2);

                Btn_Consultar.Enabled = true;
                Btn_Guardar.Enabled = false;
                Grid_Local.DataSource = null;

                MessageBox.Show("Migración exitosa", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error [Ingresar_Notificacion] : " + Ex.Message, "Error", MessageBoxButtons.OK);
                throw new Exception("Error [Ingresar_Notificacion]: " + Ex.Message);
            }
        }


        ///*********************************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///*********************************************************************************
        private void Btn_Consultar_Click(object sender, EventArgs e)
        {
           

            try
            {

                if (Rbtn_Suspension_Servicio.Checked == true)
                {
                    Grid_Local_Resultado(Grid_1);

                    //Creamos el delegado 
                    ThreadStart Delegado = new ThreadStart(Consultar_Suspensiones);
                    //Creamos la instancia del hilo 
                    Thread hilo = new Thread(Delegado);
                    //Iniciamos el hilo 
                    hilo.Start(); 

                }
                else if (Rbtn_Notificaciones.Checked == true)
                {
                    Grid_Local_Resultado(Grid_1);

                    //Creamos el delegado 
                    ThreadStart Delegado = new ThreadStart(Consultar_Notificaciones);
                    //Creamos la instancia del hilo 
                    Thread hilo = new Thread(Delegado);
                    //Iniciamos el hilo 
                    hilo.Start();
                }

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message, Ex);
            }
        }

        ///*********************************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///*********************************************************************************
        private void Frm_Credito_Y_Cobranza_Load(object sender, EventArgs e)
        {
            Btn_Guardar.Enabled = false;
        }

        ///*********************************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///*********************************************************************************
        private void Btn_Guardar_Click(object sender, EventArgs e)
        {
            try
            {

                if (Rbtn_Suspension_Servicio.Checked == true)
                {
                    //Creamos el delegado 
                    ThreadStart Delegado = new ThreadStart(Ingresar_Suspensiones);
                    //Creamos la instancia del hilo 
                    Thread hilo = new Thread(Delegado);
                    //Iniciamos el hilo 
                    hilo.Start(); 
                }
                else
                {
                    //Creamos el delegado 
                    ThreadStart Delegado = new ThreadStart(Ingresar_Notificacion);
                    //Creamos la instancia del hilo 
                    Thread hilo = new Thread(Delegado);
                    //Iniciamos el hilo 
                    hilo.Start(); 
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message, Ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_Credito_Y_Cobranza_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 obj = new Form1();
            this.Hide();
            obj.Show();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Abrir_Archivo_Click(object sender, EventArgs e)
        {
            OpenFileDialog Sfd_Ruta_Archivo = new OpenFileDialog();
            String Str_Ruta_Almacenamiento = "C:\\Servicios_siac\\Cartera_Vencida.txt";
            StringBuilder Str_Texto = new StringBuilder();
            String Str_Linea = "";
            String[] Str_Matriz_Lectura = new String[100];
            Boolean Estatus = false;
            int Cont_Columnas = 0;
            int Cont_Punto = 0;
            int Cont_Datos = 0;
            Boolean Bol_Meses_Adeudo = false;

            try
            {

                Sfd_Ruta_Archivo.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                Sfd_Ruta_Archivo.FilterIndex = 0;
                Sfd_Ruta_Archivo.RestoreDirectory = true;

                if (Sfd_Ruta_Archivo.ShowDialog() == DialogResult.OK)
                {
                    //  se llenara el documento
                    StreamReader Sr_Archivo_Original = new StreamReader(Sfd_Ruta_Archivo.OpenFile());
                    StreamWriter Sw_Archivo_Final = new StreamWriter(Str_Ruta_Almacenamiento, true, Encoding.UTF8);


                    while ((Str_Linea = Sr_Archivo_Original.ReadLine()) != null)
                    {
                        Cont_Columnas = 0;
                        Cont_Punto = 0;
                        Cont_Datos = 0;
                        Str_Texto = new StringBuilder();

                        Str_Matriz_Lectura = Str_Linea.Split(' ');
                        if (Str_Linea.Contains("00090289268"))
                        {
                            string X = "";
                        }


                        if (!Str_Linea.Contains("Total"))
                        {
                            if (!Str_Linea.Contains("Meses"))
                            {
                                if (!Str_Linea.Contains("Ctas"))
                                {
                                    if (Str_Matriz_Lectura.Length > 40)
                                    {
                                        for (int Cont_For = 0; Cont_For < Str_Matriz_Lectura.Length; Cont_For++)
                                        {
                                            if (Str_Matriz_Lectura[Cont_For].ToString() != "")
                                            {
                                                Char[] Char_Arreglo = Str_Matriz_Lectura[Cont_For].ToCharArray();

                                                foreach (Char Valor in Char_Arreglo)
                                                {
                                                    if (Char.IsNumber(Valor))
                                                    {
                                                        if (Cont_Columnas == 0)
                                                        {
                                                            if (Char_Arreglo.Length == 12)
                                                            {

                                                            }
                                                            else
                                                            {
                                                                break;
                                                            }
                                                        }

                                                        Cont_Columnas++;

                                                        if (Cont_Columnas > 2)
                                                        {
                                                            //  validamos para los importes
                                                            foreach (Char Valor1 in Char_Arreglo)
                                                            {
                                                                if (Valor1 == '.')
                                                                {
                                                                    Cont_Punto++;
                                                                    Estatus = true;
                                                                    break;
                                                                }
                                                            }

                                                            if (Bol_Meses_Adeudo == false)
                                                            {
                                                                if (Cont_For == (Str_Matriz_Lectura.Length - 1))
                                                                {
                                                                    Estatus = true;
                                                                    Bol_Meses_Adeudo = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Estatus = true;
                                                        }
                                                    }

                                                    break;
                                                }

                                                if (Estatus == true)
                                                {
                                                    if (Cont_Columnas == 1 || Cont_Columnas == 2 || Cont_Punto == 5  || Bol_Meses_Adeudo == true)
                                                    {
                                                        Str_Texto.Append(Str_Matriz_Lectura[Cont_For].ToString() + ";");
                                                        Cont_Datos++;
                                                    }
                                                }

                                               
                                                        
                                                

                                                Estatus = false;
                                                Bol_Meses_Adeudo = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        

                        //  validacion para la informaicon que se ingresa al archivo
                        if (Str_Texto.ToString().Length > 0)
                        {
                            if (Cont_Datos == 3)//  3
                            {
                                Sw_Archivo_Final.WriteLine(Str_Texto);
                            }
                        }
                    }

                    Sw_Archivo_Final.Close();
                    Sr_Archivo_Original.Close();

                }

                MessageBox.Show("Proceso exitoso", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception Ex)
            {

            }
        }
        //*******************************************************************************
        //NOMBRE DE LA FUNCIÓN:Consultar_Informacion
        //DESCRIPCIÓN: Metodo que permite llenar el Grid con la informacion de la consulta
        //PARAMETROS: 
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 02/Noviembre/2016
        //MODIFICO:
        //FECHA_MODIFICO:
        //CAUSA_MODIFICACIÓN:
        //*******************************************************************************
        private void Btn_Ajuste_Saldos_Click(object sender, EventArgs e)
        {
            DataTable Dt_Consulta_Migracion = new DataTable();
            Cls_Credito_Y_Cobranza Obj_Credito_Cobranza = new Cls_Credito_Y_Cobranza();
            
            try
            {
                //Creamos el delegado 
                ThreadStart Delegado = new ThreadStart(Ajustar_Saldos);
                //Creamos la instancia del hilo 
                Thread hilo = new Thread(Delegado);
                //Iniciamos el hilo 
                hilo.Start(); 


            }
            catch (Exception Ex)
            {
                MessageBox.Show("[Error] :" + Ex.Message, "Error", MessageBoxButtons.OK); 
            }
        }


        ///*********************************************************************************
        /// <summary>
        /// 
        /// </summary>
        ///*********************************************************************************
        private void Ajustar_Saldos()
        {
            Cls_Credito_Y_Cobranza Obj_Credito_Cobranza = new Cls_Credito_Y_Cobranza();
            DataTable Dt_Consulta = new DataTable();
            DataTable Dt_Consulta_Diverso = new DataTable();
            Double Db_Total = 0;
            DataTable Dt_Consulta_Migracion = new DataTable();
            String Str_Rpu = "";

            try
            {

                Str_Rpu = Txt_Rpu.Text;

                Dt_Consulta_Migracion = Obj_Credito_Cobranza.Consultar_Notificaciones_Saldos(Str_Rpu);

                Lbl_Proceso_General.Text = "Ajustando saldos de notificaciones";
                Obj_Credito_Cobranza.Actualizar_Saldos_Notificaciones(Dt_Consulta_Migracion, Pro_Bar_1, Pro_Bar_2);
                Lbl_Proceso_General.Text = "********************Ajuste de saldos Exitoso********************";

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error [Ajustar_Saldos] : " + Ex.Message, "Error", MessageBoxButtons.OK);
                throw new Exception("Error [Ingresar_Suspensiones]: " + Ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Verificar_Click(object sender, EventArgs e)
        {
            Cls_Credito_Y_Cobranza Obj_Credito_Cobranza = new Cls_Credito_Y_Cobranza();
            DataTable Dt_Consulta = new DataTable();
            DataTable Dt_Auxiliar = new DataTable();

            try
            {
                Dt_Consulta = Obj_Credito_Cobranza.Consultar_Notificaciones_Meses_Adeudo();

                Dt_Consulta.Columns.Add("Meses_");
                Dt_Consulta.Columns.Add("Fecha_");
                Dt_Consulta.Columns.Add("Meses_Sicap");

                string Str_Rpu = "";

                foreach (DataRow Registro in Dt_Consulta.Rows)
                {
                    Dt_Auxiliar.Clear();
                    Str_Rpu = "";
                    Str_Rpu = Registro["Rpu"].ToString();

                    Dt_Auxiliar = Obj_Credito_Cobranza.Consultar_Nuevos_Meses(Str_Rpu);

                    foreach (DataRow Registro_Nuevo in Dt_Auxiliar.Rows)
                    {
                        Registro.BeginEdit();

                        Registro["Meses_"] = Registro_Nuevo["Mes"].ToString();
                        Registro["Fecha_"] = Registro_Nuevo["Fecha"].ToString();

                        Registro.EndEdit();
                        Registro.AcceptChanges();
                    }

                    Dt_Auxiliar.Clear();

                    Dt_Auxiliar = Obj_Credito_Cobranza.Consultar_Nuevos_Meses_Sicap(Str_Rpu);

                    foreach (DataRow Registro_Nuevo in Dt_Auxiliar.Rows)
                    {
                        Registro.BeginEdit();

                        Registro["Meses_Sicap"] = Registro_Nuevo["mesadeudo"].ToString();

                        Registro.EndEdit();
                        Registro.AcceptChanges();
                    }


                }
                
                Grid_1.DataSource = Dt_Consulta;
                //Credito_Cobranza_BDContel

            }
            catch (Exception Ex)
            {

            }
        }

    }
}
