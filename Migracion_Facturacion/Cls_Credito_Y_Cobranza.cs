using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;

namespace Migracion_Facturacion
{
    class Cls_Credito_Y_Cobranza
    {

        public DataTable Consultar_No_Diverso(string Rpu) //----- caso 5: RECIBOS SIN HOSTORIAL -----
        {
            DataTable Dt_Consulta = null;
            String strSql = "";
            DataSet ds;
            SqlDataAdapter da;


            using (SqlConnection conexion = new SqlConnection(Cls_Conexion.Str_Conexion_CreditoC_Contel))
            {
                conexion.Open();

                using (SqlCommand comando = conexion.CreateCommand())
                {

                    strSql = "SELECT "+
                                " TOP 1 D.No_Diverso  ";

                    //******************************************************************************************************************************
                    //******************************************************************************************************************************
                    //    Seccion from    ******************************************************************************************************************
                    strSql += " from Ope_Cor_Diversos d " +
                                " join Ope_Cor_Diversos_Detalles dd on dd.No_Diverso = d.No_Diverso" ;

                    //******************************************************************************************************************************
                    //******************************************************************************************************************************
                    //******************************************************************************************************************************
                    //  seccion where
                    strSql += " where d.RPU = '" + Rpu + "'  and d.Estatus = 'PENDIENTE'";
                    strSql += "and (dd.Concepto_ID = '00104' or dd.Concepto_ID = '00105') ";


                    //  order by ****************************************************************************************************
                    //***************************************************************************************************************
                    strSql += " ORDER BY d.No_Diverso DESC ";


                    comando.CommandText = strSql;
                    comando.CommandTimeout = 600;
                    da = new SqlDataAdapter(comando);
                    ds = new DataSet();
                    da.Fill(ds);

                    Dt_Consulta = ds.Tables[0];


                } // comando

            } // conexion




            return Dt_Consulta;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db_destino_Siac"></param>
        /// <param name="db_origen_simapag"></param>
        /// <returns></returns>
        public DataTable Consultar_Cuentas_A_Suspender() //----- caso 5: RECIBOS SIN HOSTORIAL -----
        {
            DataTable Dt_Consulta = null;
            String strSql = "";
            String strPeriodo = "";
            String porcentajeCorte = "0";

            DataSet ds;
            SqlDataAdapter da;


            using (SqlConnection conexion = new SqlConnection(Cls_Conexion.Str_Conexion_CreditoC_Contel))
            {
                conexion.Open();

                using (SqlCommand comando = conexion.CreateCommand())
                {

                    strSql = "SELECT COUNT(DISTINCT (f.No_Factura_Recibo)) AS Meses_Adeudo " +
                           " ,SUM(fd.Total_Saldo) AS Monto_Adeudo " +
                           " , '' as No_Suspension " +
                           " ,p.Region_ID " +
                           " ,p.Colonia_ID " +
                           " ,f.Predio_ID " +
                           " ,p.no_cuenta AS No_Cuenta " +
                           " ,p.RPU AS RPU " +
                           //", '' as Orden_Trabajo_Id" +
                           ", '' as no_diverso";

                    strSql += ", respaldo.fcorte as Fecha_Notificacion ";

                    //******************************************************************************************************************************
                    //******************************************************************************************************************************
                    //    Seccion from    ******************************************************************************************************************
                    strSql += " FROM Cat_Cor_Predios p " +
                          " JOIN Cat_Cor_Usuarios u ON p.USUARIO_ID = u.USUARIO_ID " +
                          " JOIN Cat_Cor_Tarifas t ON t.Tarifa_ID = p.Tarifa_ID " +
                          " JOIN Ope_Cor_Facturacion_Recibos f ON f.Predio_ID = p.Predio_ID " +
                          " JOIN Ope_Cor_Facturacion_Recibos_Detalles fd ON fd.No_Factura_Recibo = f.No_Factura_Recibo " +
                          " JOIN Cat_Cor_Regiones r ON r.Region_ID = p.Region_ID " +
                          " left outer  JOIN Cat_Cor_Colonias c ON c.COLONIA_ID = p.Colonia_ID " +
                          " JOIN CAT_COR_TIPOS_CUOTAS cu ON cu.CUOTA_ID = t.Cuota_ID " +
                          " left outer JOIN Cat_Cor_Calles ca ON ca.CALLE_ID = p.Calle_ID " +
                          " JOIN Cat_Cor_Giros_Actividades ga ON ga.Actividad_Giro_ID = p.Giro_Actividad_ID " +
                          " JOIN Cat_Cor_Giros g ON g.GIRO_ID = ga.Giro_ID ";


                    //  Se Obtienen los valores de la base de datos del respaldo del simapag
                    String Bd_Simapag = ConfigurationManager.ConnectionStrings["Credito_Cobranza_BDSimapag"].ConnectionString;
                    var Obj_Base_Simapag = new System.Data.SqlClient.SqlConnectionStringBuilder(Cls_Conexion.Str_Conexion_CreditoC_Simapag);

                    strSql += " join " + Obj_Base_Simapag.InitialCatalog + ".dbo.recibos respaldo on respaldo.rpu = p.RPU";


                    //******************************************************************************************************************************
                    //******************************************************************************************************************************
                    //******************************************************************************************************************************
                    //  seccion where
                    strSql += " WHERE f.Estatus_Recibo IN ( " +
                      "       'PENDIENTE' " +
                      "       ,'PARCIAL' " +
                      "       ) ";

                    //strSql += " and (cu.CLAVE='CF' or cu.CLAVE='SM')"


                    strSql += " and ((P.Cobranza = 'SI'  OR P.Requerido = 'SI' OR P.Aplica_Notificacion = 'SI') )";

                    //strSql += " and p.rpu = '000870200161'";

                    //******************************************************************************************************************************
                    //******************************************************************************************************************************
                    //******************************************************************************************************************************
                    //  Group by
                    strSql += " GROUP BY p.RPU , p.no_cuenta, p.Region_ID ,p.Colonia_ID,f.Predio_ID, p.cortado,r.Numero_Region" +
                           ",c.NOMBRE,t.Clave,cu.CLAVE,ca.NOMBRE,u.NOMBRE,u.APELLIDO_PATERNO,u.APELLIDO_MATERNO,t.Nombre,g.Nombre_Giro,p.Numero_Exterior,p.No_Orden_Reparto ";

                    strSql += ", respaldo.fcorte ";
                    strSql += ", p.predio_id";

                    //******************************************************************************************************************************
                    //******************************************************************************************************************************
                    //******************************************************************************************************************************
                    //  having 
                    //if (Datos.P_Operacion == "CORTE" || Convert.ToInt64(Datos.P_Meses_Adeudo) > 0 || Convert.ToInt64(Datos.P_Adeudo) > 0 || Convert.ToInt64(Datos.P_Adeudo_Fin) > 0)
                    //{
                    strSql += " HAVING  LEN(MAX(f.No_Factura_Recibo)) >0   ";

                    strSql += "  AND COUNT(DISTINCT (f.No_Factura_Recibo)) >= 1 ";
                    strSql += " and SUM(fd.Total_Saldo) >  0";

                    //  order by ****************************************************************************************************
                    //***************************************************************************************************************
                    strSql += " order by p.predio_id asc";


                    comando.CommandText = strSql;
                    comando.CommandTimeout = 600;
                    da = new SqlDataAdapter(comando);
                    ds = new DataSet();
                    da.Fill(ds);

                    Dt_Consulta = ds.Tables[0];


                } // comando

            } // conexion




            return Dt_Consulta;

        }// fin de consulta
        
        //****************************************************************************************
        //NOMBRE_FUNCION: Insertar_Notificacion_SIMAPAG
        //DESCRIPCION: Insertar un registro de Notificacion a la base de datos           
        //PARAMETROS : El objeto Datos que se encarga con la comunicación de la clase de Negocio
        //CREO       : Sergio Ulises Durán Hernández
        //FECHA_CREO : 10-Octubre-2011
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //****************************************************************************************
        public void Insertar_Notificacion_SIMAPAG(DataTable Dt_Notificacion, ProgressBar Pro_Bar_1, ProgressBar Pro_Bar_2)
        {
            //Declaración de las variables
            SqlTransaction Obj_Transaccion = null;
            SqlConnection Obj_Conexion = new SqlConnection(Cls_Conexion.Str_Conexion_CreditoC_Contel);
            SqlCommand Obj_Comando = new SqlCommand();
            String Mi_SQL;
            Int64 Consecutivo_Eventos = 0;

            try
            {
                Obj_Conexion.Open();
                Obj_Transaccion = Obj_Conexion.BeginTransaction();
                Obj_Comando.Transaction = Obj_Transaccion;
                Obj_Comando.Connection = Obj_Conexion;

                Pro_Bar_1.Visible = true;
                Pro_Bar_1.Minimum = 1;
                Pro_Bar_1.Maximum = 2;
                Pro_Bar_1.Value = 1;
                Pro_Bar_1.Step = 1;

                Stopwatch watch = new Stopwatch();
                watch.Start();


                Pro_Bar_2.Visible = true;
                Pro_Bar_2.Minimum = 1;
                Pro_Bar_2.Maximum = Dt_Notificacion.Rows.Count;
                Pro_Bar_2.Value = 1;
                Pro_Bar_2.Step = 1;

                Stopwatch watch2 = new Stopwatch();
                watch2.Start();
              

                
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //Notificacion  ***********************************************************************************

                #region Notificacion

                foreach (DataRow Registro in Dt_Notificacion.Rows)
                {
                    // ******************************************************************************************
                    // ******************************************************************************************
                    //Query para la inserción de una Notificacion
                    Mi_SQL = "INSERT INTO Ope_Cor_Notificaciones (" +
                        " No_Notificacion" +        //  1  
                        ", Region_ID" +             //  2
                        ", Sector_ID" +             //  3
                        ", Colonia_ID" +            //  4
                        ", Predio_ID" +             //  5
                        ", Elaboro_ID" +            //  6
                        ", Folio" +                 //  7
                        ", No_Cuenta" +             //  8
                        ", RPU" +                   //  9
                        ", Estatus" +               //  10
                        ", Fecha_Notificacion" +    //  11
                        ", Usuario_Creo" +          //  12
                        ", Fecha_Creo" +            //  13
                        ", No_Factura_Recibo" +     //  14
                        ", Suspension_Id " +        //  15
                        ", Adeudo" +                //  16
                        ", meses_adeudo" +          //  17
                        ", Agua" +                  //  18
                        ", Alcantarillado" +        //  19
                        ", Saneamiento" +           //  20
                        ", Iva" +                   //  21
                        ", Recargos" +              //  22
                        ", Otros_cargos" +          //  23
                        ", Fecha_Facturacion" +     //  24
                        ", rezago" +                //  25
                        ", Recargo_Agua" +          //  26
                        ", Recargo_Drenaje" +       //  27
                        ", Recargo_Saneamiento" +   //  28
                        ", entrego_id" +            //  29
                        ", Fecha_Asignacion" +      //  30
                        ")";
                    // ******************************************************************************************
                    // ******************************************************************************************
                    Mi_SQL += " VALUES ('";
                    // ******************************************************************************************
                    // ******************************************************************************************
                    Mi_SQL += Registro["No_notificacion"].ToString() + "'" +                        //  1
                         " , '" +  Registro["Region_Id"].ToString()  + "'" +                        //  2
                         " , '" + Registro["sector_id"].ToString() + "'" +                          //  3
                         " , '" + Registro["colonia_id"].ToString() + "'" +                         //  4
                         " , '" + Registro["predio_id"].ToString() + "'" +                          //  5
                         " , '0000000028'" +                                                        //  6
                         " , '" + Registro["No_notificacion"].ToString() + "'" +                    //  7
                         " , '" + Registro["no_cuenta"].ToString() + "'" +                          //  8
                         " , '" + Registro["Rpu"].ToString() + "'" +                                //  9
                         " , 'CERRADA'";                                                           //  10

                    //  Filtro para la fecha ************ (11) *******                              //  11**********
                    Mi_SQL += " , '" + Convert.ToDateTime(Registro["Fecha_Notificacion"].ToString()).ToString("yyyy-dd-MM") + "'";
                    
                    //***************

                    Mi_SQL += ", 'MIGRACION'" +                                                     //  12
                            " , '" + Convert.ToDateTime(Registro["Fecha_Notificacion"].ToString()).ToString("yyyy-dd-MM") + "'";                                                         //  13

                    Mi_SQL += ", '" + Registro["No_Recibo"].ToString() + "'";                       //  14

                    Mi_SQL += ", '" + Registro["No_Suspension"].ToString() + "'";                   //  15

                    Mi_SQL += ", '" + Registro["Total"].ToString() + "'";                           //  16
                    Mi_SQL += ", '" + Registro["Meses_Adeudo_Origen"].ToString() + "'";                    //  17
                    Mi_SQL += ", '" + Registro["agua"].ToString() + "'";                            //  18
                    Mi_SQL += ", '" + Registro["Alcantarillado"].ToString() + "'";                  //  19
                    Mi_SQL += ", '" + Registro["Saneamiento"].ToString() + "'";                     //  20
                    Mi_SQL += ", '" + Registro["Iva"].ToString() + "'";                             //  21
                    Mi_SQL += ", '" + Registro["Recargo"].ToString() + "'";                         //  22
                    Mi_SQL += ", '" + Registro["Otros_Cargos"].ToString() + "'";                    //  23
                    Mi_SQL += ", '" + Convert.ToDateTime(Registro["Ultimo_Periodo_Facturado"].ToString()).ToString("dd-MM-yyyy") + "'";            //  24
                    Mi_SQL += ", '" + Registro["Rezago"].ToString() + "'";                          //  25
                    Mi_SQL += ", '" + Registro["Recargo_Agua"].ToString() + "'";                    //  26
                    Mi_SQL += ", '" + Registro["Recargo_Drenaje"].ToString() + "'";                 //  27
                    Mi_SQL += ", '" + Registro["Recargo_Saneamiento"].ToString() + "'";             //  28

                    Mi_SQL += ", '0000000028'";                                                     //  29
                    Mi_SQL += ", '" + Convert.ToDateTime(Registro["Fecha_Notificacion"].ToString()).ToString("yyyy-dd-MM") + "'";                                                       //  30


                    Mi_SQL += ")";


                    Obj_Comando.CommandText = Mi_SQL;
                    Obj_Comando.ExecuteNonQuery();

                #endregion Fin Notificacion


                    //***********************************************************************************************************************
                    //***********************************************************************************************************************
                    //***********************************************************************************************************************
                    //***********************************************************************************************************************
                    //se actualiza la informacion del predio ***********************************************************************************

                    #region Suspension

                    Mi_SQL = "UPDATE Ope_Cor_Suspensiones_Servicios SET " +
                        " No_Notificacion = '" + Registro["no_notificacion"].ToString() + "'" +
                        " WHERE No_Suspension = '" + Registro["No_Suspension"].ToString() + "'";

                    Obj_Comando.CommandText = Mi_SQL;
                    Obj_Comando.ExecuteNonQuery();




                    Pro_Bar_2.PerformStep();
                }


                Mi_SQL = " update  Ope_Cor_Notificaciones  set " +
                                " Ope_Cor_Notificaciones.Cobranza = (SELECT p.Cobranza from Cat_Cor_Predios p where p.RPU = Ope_Cor_Notificaciones.RPU) " +
                                " , Ope_Cor_Notificaciones.Requerido = (SELECT p.Requerido from Cat_Cor_Predios p where p.RPU = Ope_Cor_Notificaciones.RPU) ";

                Obj_Comando.CommandText = Mi_SQL;
                Obj_Comando.ExecuteNonQuery();


                #endregion Fin Suspension


                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //ejecucion de la transaccion    ***********************************************************************************
                Obj_Transaccion.Commit();

                watch2.Stop();
                Pro_Bar_1.PerformStep();
                watch.Stop();

                //*******************************************************************
                //*******************************************************************
                //*******************************************************************
            }
            catch (SqlException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (DBConcurrencyException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            finally
            {
                Obj_Conexion.Close();
            }
        }



        //****************************************************************************************
        //NOMBRE_FUNCION: Insertar_Notificacion_SIMAPAG
        //DESCRIPCION: Insertar un registro de Notificacion a la base de datos           
        //PARAMETROS : El objeto Datos que se encarga con la comunicación de la clase de Negocio
        //CREO       : Sergio Ulises Durán Hernández
        //FECHA_CREO : 10-Octubre-2011
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //****************************************************************************************
        public void Actualizar_Saldos_Notificaciones(DataTable Dt_Notificacion, ProgressBar Pro_Bar_1, ProgressBar Pro_Bar_2)
        {
            //Declaración de las variables
            SqlTransaction Obj_Transaccion = null;
            SqlConnection Obj_Conexion = new SqlConnection(Cls_Conexion.Str_Conexion_CreditoC_Ajuste_Final);
            SqlCommand Obj_Comando = new SqlCommand();
            String Mi_SQL;


            DataTable Dt_Consulta = new DataTable();
            SqlDataReader Dr_Lector;

            try
            {
                Obj_Conexion.Open();
                Obj_Transaccion = Obj_Conexion.BeginTransaction();
                Obj_Comando.Transaction = Obj_Transaccion;
                Obj_Comando.Connection = Obj_Conexion;

                Pro_Bar_1.Visible = true;
                Pro_Bar_1.Minimum = 1;
                Pro_Bar_1.Maximum = 2;
                Pro_Bar_1.Value = 1;
                Pro_Bar_1.Step = 1;

                Stopwatch watch = new Stopwatch();
                watch.Start();


                Pro_Bar_2.Visible = true;
                Pro_Bar_2.Minimum = 1;
                Pro_Bar_2.Maximum = Dt_Notificacion.Rows.Count;
                Pro_Bar_2.Value = 1;
                Pro_Bar_2.Step = 1;

                Stopwatch watch2 = new Stopwatch();
                watch2.Start();



                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //Notificacion  ***********************************************************************************

                #region Notificacion

                foreach (DataRow Registro in Dt_Notificacion.Rows)
                {

                    Mi_SQL = "select estatus from Ope_Cor_Notificaciones " +
                            " where rpu = '" + Registro["Rpu"].ToString() + "'";
                    
                    Obj_Comando.CommandText = Mi_SQL.ToString();
                    Obj_Comando.CommandType = CommandType.Text;
                    Obj_Comando.CommandTimeout = 300;
                    Dr_Lector = Obj_Comando.ExecuteReader();
                    Dt_Consulta = new DataTable();
                    Dt_Consulta.Load(Dr_Lector);
                    Dt_Consulta.AcceptChanges();
                    Dr_Lector.Close();


                    foreach (DataRow Registro_Estatus in Dt_Consulta.Rows)
                    {
                        if (Registro_Estatus["Estatus"].ToString() == "CERRADA" || Registro_Estatus["Estatus"].ToString() == "FILTRADA")
                        {

                            // ******************************************************************************************
                            // ******************************************************************************************
                            //Query para la inserción de una Notificacion
                            Mi_SQL = "update Ope_Cor_Notificaciones set";

                            Mi_SQL += " agua = '" + Registro["agua"].ToString() + "'";
                            Mi_SQL += ", Alcantarillado = '" + Registro["Alcantarillado"].ToString() + "'";
                            Mi_SQL += ", Saneamiento = '" + Registro["Saneamiento"].ToString() + "'";
                            Mi_SQL += ", Iva = '" + Registro["Iva"].ToString() + "'";
                            Mi_SQL += ", Rezago = '" + Registro["Rezago"].ToString() + "'";
                            Mi_SQL += ", Recargos = '" + Registro["Recargos"].ToString() + "'";
                            Mi_SQL += ", Otros_Cargos = '" + Registro["Otros_Cargos"].ToString() + "'";
                            Mi_SQL += ", Recargo_Agua = '" + Registro["Recargo_Agua"].ToString() + "'";
                            Mi_SQL += ", Recargo_Drenaje = '" + Registro["Recargo_Drenaje"].ToString() + "'";
                            Mi_SQL += ", Recargo_Saneamiento = '" + Registro["Recargo_Saneamiento"].ToString() + "'";
                            Mi_SQL += ", adeudo = '" + Registro["adeudo"].ToString() + "'";

                            //  where
                            Mi_SQL += " where rpu = '" + Registro["Rpu"].ToString() + "'";


                            Obj_Comando.CommandText = Mi_SQL;
                            Obj_Comando.ExecuteNonQuery();

                        }

                    }

                    Pro_Bar_2.PerformStep();
                }

                #endregion Fin Notificacion

                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //ejecucion de la transaccion    ***********************************************************************************
                Obj_Transaccion.Commit();

                watch2.Stop();
                Pro_Bar_1.PerformStep();
                watch.Stop();

                //*******************************************************************
                //*******************************************************************
                //*******************************************************************
            }
            catch (SqlException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (DBConcurrencyException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            finally
            {
                Obj_Conexion.Close();
            }
        }


        //****************************************************************************************
        //NOMBRE_FUNCION: Insertar_Suspension_Servicio
        //DESCRIPCION: Insertar un registro de Suspension de Servicio en la Base de Datos
        //PARAMETROS : El objeto Datos que se encarga con la comunicación de la clase de Negocio
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 24-Marzo-2016
        //MODIFICO   : 
        //FECHA_MODIFICO: 
        //CAUSA_MODIFICO: 
        //****************************************************************************************
        public void Insertar_Suspension_Servicio(DataTable Dt_Suspensiones, ProgressBar Pro_Bar_1, ProgressBar Pro_Bar_2)
        {
            //Declaración de las variables
            SqlTransaction Obj_Transaccion;
            SqlConnection Obj_Conexion = new SqlConnection(Cls_Conexion.Str_Conexion_CreditoC_Contel);
            SqlCommand Obj_Comando = new SqlCommand();
            String Mi_SQL = "";
            Obj_Conexion.Open();
            Obj_Transaccion = Obj_Conexion.BeginTransaction();
            Obj_Comando.Transaction = Obj_Transaccion;
            Obj_Comando.Connection = Obj_Conexion;

            String No_Suspension_Servicio = "";
            String Str_Sql = "";
            DataTable Dt_Parametro_Reconexion = new DataTable();
            DataTable Dt_Tipo_Falla = new DataTable();
            DataTable Dt_Datos_Usuario = new DataTable();

            try
            {

                Pro_Bar_1.Visible = true;
                Pro_Bar_1.Minimum = 1;
                Pro_Bar_1.Maximum = 2;
                Pro_Bar_1.Value = 1;
                Pro_Bar_1.Step = 1;

                Stopwatch watch = new Stopwatch();
                watch.Start();


                Pro_Bar_2.Visible = true;
                Pro_Bar_2.Minimum = 1;
                Pro_Bar_2.Maximum = Dt_Suspensiones.Rows.Count;
                Pro_Bar_2.Value = 1;
                Pro_Bar_2.Step = 1;

                Stopwatch watch2 = new Stopwatch();
                watch2.Start();
              

                foreach (DataRow Registro in Dt_Suspensiones.Rows)
                {
                    //*******************************************************************
                    //Query para la inserción de una Notificacion
                    Mi_SQL = "INSERT INTO Ope_Cor_Suspensiones_Servicios (";
                    Mi_SQL += "No_Suspension,";



                    Mi_SQL += " Region_ID,";
                    Mi_SQL += "Sector_ID,";
                    Mi_SQL += "Colonia_ID,";
                    Mi_SQL += "Predio_ID";

                    Mi_SQL += " ,Elaboro_ID,";
                    //Mi_SQL +="Entrego_ID,";
                    Mi_SQL += "Folio" +
                                ",Estatus,";
                    Mi_SQL += "Fecha_Corte,";
                    Mi_SQL += "No_Cuenta,";
                    Mi_SQL += "Adeudo,";
                    Mi_SQL += "Fecha_Adeudo,";
                    Mi_SQL += "Recibio,";
                    Mi_SQL += "Observaciones,";
                    Mi_SQL += "Usuario_Creo,";
                    Mi_SQL += "Fecha_Creo,";
                    Mi_SQL += "rpu,";
                    Mi_SQL += " Meses_Adeudo";

                    //*******************************************************************
                    Mi_SQL += ") VALUES ('";
                    //*******************************************************************    
                    Mi_SQL += Registro["No_Suspension"].ToString() + "'";



                    Mi_SQL += " , '" + Registro["Region_ID"].ToString() +"'"
                    + " , ''"
                    + " , '" + Registro["Colonia_ID"].ToString() + "'"
                    + " , '" + Registro["Predio_ID"].ToString() + "'"
                    + " , '0000000028'"
                        //+ " , NULL"
                    + " , '" + Registro["No_Suspension"].ToString() + "'"
                    + " , 'EJECUTADA'";


                    Mi_SQL += " , '" + Convert.ToDateTime(Registro["Fecha_Notificacion"].ToString()).ToString("yyyy-dd-MM") + "'";


                    Mi_SQL += " , '" + Registro["No_Cuenta"].ToString() + "'"
                        + " , " + Registro["Monto_Adeudo"].ToString()
                        + " , ''"
                        + " , ''"
                        + " , ''"
                        + " , 'MIGRACION'"
                        + " , '" + Convert.ToDateTime(Registro["Fecha_Notificacion"].ToString()).ToString("yyyy-dd-MM") + "'";

                    Mi_SQL += ", '" + Registro["Rpu"].ToString() + "'";
                    Mi_SQL += ", " + Registro["Meses_Adeudo"].ToString();
                    Mi_SQL += ")";
                    //*******************************************************************


                    Obj_Comando.CommandText = Mi_SQL;
                    Obj_Comando.ExecuteNonQuery();



                    if (!String.IsNullOrEmpty(Registro["no_diverso"].ToString()))
                    {
                        Mi_SQL = "update Ope_Cor_Diversos " +
                                    " SET" +
                                    " No_Suspension =  '" + Registro["No_Suspension"].ToString() + "'" +
                                    " where No_Diverso = '" + Registro["no_diverso"].ToString() + "'";

                        Obj_Comando.CommandText = Mi_SQL;
                        Obj_Comando.ExecuteNonQuery();
                    }

                    Pro_Bar_2.PerformStep();

                }

                

                //*******************************************************************
                //*******************************************************************
                //*******************************************************************
                //  se ejecuta la transaccion
                Obj_Transaccion.Commit();
                
                watch2.Stop();
                Pro_Bar_1.PerformStep();
                watch.Stop();

                //*******************************************************************
                //*******************************************************************
                //*******************************************************************

            }
            catch (SqlException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (DBConcurrencyException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }

            finally { Obj_Conexion.Close(); }
        }



        //*******************************************************************************
        //NOMBRE_FUNCION: Consultar_Predios_Notificacion_Simapag
        //DESCRIPCION: Realiza la consulta de los predios que seran notificados
        //PARAMETROS : 
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 30/Marzo/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public  DataTable Consultar_Predios_Notificacion_Simapag()
        {
            DataTable Dt_Consulta = new DataTable();
            StringBuilder Str_My_Sql = new StringBuilder();

            SqlConnection Obj_Conexion = null;
            SqlCommand Obj_Comando = null;
            SqlDataReader Dr_Lector;

            try
            {
                Obj_Conexion = new SqlConnection(Cls_Conexion.Str_Conexion_CreditoC_Contel);
                Obj_Conexion.Open();
                Obj_Comando = Obj_Conexion.CreateCommand();

                Str_My_Sql.Append("SELECT ");
                Str_My_Sql.Append(" COUNT(DISTINCT (f.Periodo_Facturacion)) AS Meses_Adeudo");
                Str_My_Sql.Append(", respaldo.mesadeudo as Meses_Adeudo_Origen ");

                Str_My_Sql.Append(", respaldo.fcorte as Fecha_Notificacion ");
                Str_My_Sql.Append(" ,( " +
                                        " select top(1) fs.Fecha_Emision " +
                                        " from Ope_Cor_Facturacion_Recibos fs" +
                                        " where fs.Predio_ID = p.Predio_ID" +
                                        " order by fs.No_Factura_Recibo desc" +
                                    " ) as Ultimo_Periodo_Facturado");

                Str_My_Sql.Append(", isnull((" +
                                    " SELECT sum(frd.Total_Saldo)" +
                                    " FROM Ope_Cor_Facturacion_Recibos fr" +
                                    " JOIN Ope_Cor_Facturacion_Recibos_Detalles frd ON fr.No_Factura_Recibo = frd.No_Factura_Recibo" +
                                    " WHERE fr.Estatus_Recibo IN (" +
                                            " 'PENDIENTE'" +
                                            " ,'PARCIAL'" +
                                            " )" +
                                        " AND fr.RPU = p.RPU" +
                                    ") , 0) AS Total");

                Str_My_Sql.Append(", (ISNULL((" +
                                        " SELECT SUM(frd.Importe_Saldo)" +
                                        " FROM Ope_Cor_Facturacion_Recibos fr" +
                                        " JOIN Ope_Cor_Facturacion_Recibos_Detalles frd ON fr.No_Factura_Recibo = frd.No_Factura_Recibo" +
                                        " JOIN Cat_Cor_Conceptos_Cobros co ON frd.Concepto_ID = co.Concepto_ID" +
                                        " WHERE fr.Estatus_Recibo IN (" +
                                                " 'PENDIENTE'" +
                                                " ,'PARCIAL'" +
                                                " )" +
                                                " AND frd.estatus IN ('PENDIENTE', 'PARCIAL') " +
                                            " AND fr.RPU = p.RPU" +
                                            " AND  co.Concepto_ID = (SELECT CONCEPTO_AGUA from Cat_Cor_Parametros)" +
                                        " ), 0) " +


                                        " + " +
                                            " ISNULL((" +
                                            " SELECT SUM(frd.Importe_Saldo)" +
                                            " FROM Ope_Cor_Facturacion_Recibos fr" +
                                            " JOIN Ope_Cor_Facturacion_Recibos_Detalles frd ON fr.No_Factura_Recibo = frd.No_Factura_Recibo" +
                                            " JOIN Cat_Cor_Conceptos_Cobros co ON frd.Concepto_ID = co.Concepto_ID" +
                                            " WHERE fr.Estatus_Recibo IN (" +
                                                    "'PENDIENTE'" +
                                                    ",'PARCIAL'" +
                                                    ")" +
                                                    " AND frd.estatus IN ('PENDIENTE', 'PARCIAL') " +
                                                " AND fr.RPU = p.RPU" +
                                                " AND  co.Concepto_ID = (SELECT Concepto_Agua_Comercial from Cat_Cor_Parametros)" +
                                            "), 0)" +


                                        " + " +
                                            " ISNULL((" +
                                            " SELECT SUM(frd.Importe_Saldo)" +
                                            " FROM Ope_Cor_Facturacion_Recibos fr" +
                                            " JOIN Ope_Cor_Facturacion_Recibos_Detalles frd ON fr.No_Factura_Recibo = frd.No_Factura_Recibo" +
                                            " JOIN Cat_Cor_Conceptos_Cobros co ON frd.Concepto_ID = co.Concepto_ID" +
                                            " WHERE fr.Estatus_Recibo IN (" +
                                                    "'PENDIENTE'" +
                                                    ",'PARCIAL'" +
                                                    ")" +
                                                    " AND frd.estatus IN ('PENDIENTE', 'PARCIAL') " +
                                                " AND fr.RPU = p.RPU" +
                                                " AND  co.Concepto_ID = (SELECT Concepto_Rezago_Agua_Id from Cat_Cor_Parametros)" +
                                            "), 0))" +

                                        " AS [Agua]");

                Str_My_Sql.Append(", (ISNULL((" +
                                       " SELECT SUM(frd.Importe_Saldo)" +
                                       " FROM Ope_Cor_Facturacion_Recibos fr" +
                                       " JOIN Ope_Cor_Facturacion_Recibos_Detalles frd ON fr.No_Factura_Recibo = frd.No_Factura_Recibo" +
                                       " JOIN Cat_Cor_Conceptos_Cobros co ON frd.Concepto_ID = co.Concepto_ID" +
                                       " WHERE fr.Estatus_Recibo IN (" +
                                               " 'PENDIENTE'" +
                                               " ,'PARCIAL'" +
                                               " )" +
                                           " AND frd.estatus IN ('PENDIENTE', 'PARCIAL') " +
                                           " AND fr.RPU = p.RPU" +
                                           " AND co.Concepto_ID = (SELECT CONCEPTO_DRENAJE from Cat_Cor_Parametros)" +
                                       " ), 0) " +

                                         " + " +
                                            " ISNULL((" +
                                            " SELECT SUM(frd.Importe_Saldo)" +
                                            " FROM Ope_Cor_Facturacion_Recibos fr" +
                                            " JOIN Ope_Cor_Facturacion_Recibos_Detalles frd ON fr.No_Factura_Recibo = frd.No_Factura_Recibo" +
                                            " JOIN Cat_Cor_Conceptos_Cobros co ON frd.Concepto_ID = co.Concepto_ID" +
                                            " WHERE fr.Estatus_Recibo IN (" +
                                                    "'PENDIENTE'" +
                                                    ",'PARCIAL'" +
                                                    ")" +
                                                    " AND frd.estatus IN ('PENDIENTE', 'PARCIAL') " +
                                                " AND fr.RPU = p.RPU" +
                                                " AND co.Concepto_ID = (SELECT Concepto_Rezago_Drenaje_Id from Cat_Cor_Parametros)" +
                                            "), 0))" +

                                       "AS [Alcantarillado]");


                Str_My_Sql.Append(", (ISNULL((" +
                                     " SELECT SUM(frd.Importe_Saldo)" +
                                     " FROM Ope_Cor_Facturacion_Recibos fr" +
                                     " JOIN Ope_Cor_Facturacion_Recibos_Detalles frd ON fr.No_Factura_Recibo = frd.No_Factura_Recibo" +
                                     " JOIN Cat_Cor_Conceptos_Cobros co ON frd.Concepto_ID = co.Concepto_ID" +
                                     " WHERE fr.Estatus_Recibo IN (" +
                                             " 'PENDIENTE'" +
                                             " ,'PARCIAL'" +
                                             " )" +
                                             " AND frd.estatus IN ('PENDIENTE', 'PARCIAL') " +
                                         " AND fr.RPU = p.RPU" +
                                         " AND co.Concepto_ID = (SELECT CONCEPTO_SANAMIENTO from Cat_Cor_Parametros)" +
                                     " ), 0) " +
                                       " + " +
                                            " ISNULL((" +
                                            " SELECT SUM(frd.Importe_Saldo)" +
                                            " FROM Ope_Cor_Facturacion_Recibos fr" +
                                            " JOIN Ope_Cor_Facturacion_Recibos_Detalles frd ON fr.No_Factura_Recibo = frd.No_Factura_Recibo" +
                                            " JOIN Cat_Cor_Conceptos_Cobros co ON frd.Concepto_ID = co.Concepto_ID" +
                                            " WHERE fr.Estatus_Recibo IN (" +
                                                    "'PENDIENTE'" +
                                                    ",'PARCIAL'" +
                                                    ")" +
                                            " AND frd.estatus IN ('PENDIENTE', 'PARCIAL') " +
                                            " AND fr.RPU = p.RPU" +

                                                " AND co.Concepto_ID = (SELECT Concepto_Rezago_Saneamiento_Id from Cat_Cor_Parametros)" +
                                            "), 0))" +


                                     " AS [Saneamiento]");

                Str_My_Sql.Append(",ISNULL((" +
                                       " SELECT SUM(frd.Impuesto_Saldo)" +
                                       " FROM Ope_Cor_Facturacion_Recibos fr" +
                                       " JOIN Ope_Cor_Facturacion_Recibos_Detalles frd ON fr.No_Factura_Recibo = frd.No_Factura_Recibo" +
                                       " JOIN Cat_Cor_Conceptos_Cobros co ON frd.Concepto_ID = co.Concepto_ID" +
                                       " WHERE fr.Estatus_Recibo IN (" +
                                               " 'PENDIENTE'" +
                                               " ,'PARCIAL'" +
                                               " )" +
                                               " AND frd.estatus IN ('PENDIENTE', 'PARCIAL') " +
                                           " AND fr.RPU = p.RPU" +
                                       " ), 0) AS [IVA]");

                Str_My_Sql.Append(",ISNULL((" +
                                      " SELECT SUM(frd.Importe_Saldo)" +
                                      " FROM Ope_Cor_Facturacion_Recibos fr" +
                                      " JOIN Ope_Cor_Facturacion_Recibos_Detalles frd ON fr.No_Factura_Recibo = frd.No_Factura_Recibo" +
                                      " JOIN Cat_Cor_Conceptos_Cobros co ON frd.Concepto_ID = co.Concepto_ID" +
                                      " WHERE fr.Estatus_Recibo IN (" +
                                              " 'PENDIENTE'" +
                                              " ,'PARCIAL'" +
                                              " )" +
                                              " AND frd.estatus IN ('PENDIENTE', 'PARCIAL') " +
                                          " AND fr.RPU = p.RPU" +
                                          " AND (co.Concepto_ID in (SELECT Concepto_Recargo_Agua_Id from Cat_Cor_Parametros)" +
                                                    " or co.Concepto_ID in (SELECT Concepto_Recargo_Drenaje_Id from Cat_Cor_Parametros)" +
                                                    " or co.Concepto_ID in (SELECT Concepto_Recargo_Saneamiento_Id from Cat_Cor_Parametros)" +
                                                ")" +
                                      " ), 0) AS [Recargo]");


                Str_My_Sql.Append(",ISNULL((" +
                                   " SELECT SUM(frd.Importe_Saldo)" +
                                   " FROM Ope_Cor_Facturacion_Recibos fr" +
                                   " JOIN Ope_Cor_Facturacion_Recibos_Detalles frd ON fr.No_Factura_Recibo = frd.No_Factura_Recibo" +
                                   " JOIN Cat_Cor_Conceptos_Cobros co ON frd.Concepto_ID = co.Concepto_ID" +
                                   " WHERE fr.Estatus_Recibo IN (" +
                                           " 'PENDIENTE'" +
                                           " ,'PARCIAL'" +
                                           " )" +
                                           " AND frd.estatus IN ('PENDIENTE', 'PARCIAL') " +
                                       " AND fr.RPU = p.RPU" +
                                       " AND co.Concepto_ID in (SELECT Concepto_Recargo_Agua_Id from Cat_Cor_Parametros) " +
                                   " ), 0) AS [Recargo_Agua]");

                Str_My_Sql.Append(",ISNULL((" +
                                 " SELECT SUM(frd.Importe_Saldo)" +
                                 " FROM Ope_Cor_Facturacion_Recibos fr" +
                                 " JOIN Ope_Cor_Facturacion_Recibos_Detalles frd ON fr.No_Factura_Recibo = frd.No_Factura_Recibo" +
                                 " JOIN Cat_Cor_Conceptos_Cobros co ON frd.Concepto_ID = co.Concepto_ID" +
                                 " WHERE fr.Estatus_Recibo IN (" +
                                         " 'PENDIENTE'" +
                                         " ,'PARCIAL'" +
                                         " )" +
                                         " AND frd.estatus IN ('PENDIENTE', 'PARCIAL') " +
                                     " AND fr.RPU = p.RPU" +
                                     " AND co.Concepto_ID in (SELECT Concepto_Recargo_Drenaje_Id from Cat_Cor_Parametros)" +
                                 " ), 0) AS [Recargo_Drenaje]");

                Str_My_Sql.Append(",ISNULL((" +
                                " SELECT SUM(frd.Importe_Saldo)" +
                                " FROM Ope_Cor_Facturacion_Recibos fr" +
                                " JOIN Ope_Cor_Facturacion_Recibos_Detalles frd ON fr.No_Factura_Recibo = frd.No_Factura_Recibo" +
                                " JOIN Cat_Cor_Conceptos_Cobros co ON frd.Concepto_ID = co.Concepto_ID" +
                                " WHERE fr.Estatus_Recibo IN (" +
                                        " 'PENDIENTE'" +
                                        " ,'PARCIAL'" +
                                        " )" +
                                        " AND frd.estatus IN ('PENDIENTE', 'PARCIAL') " +
                                    " AND fr.RPU = p.RPU" +
                                    " AND co.Concepto_ID in (SELECT Concepto_Recargo_Saneamiento_Id from Cat_Cor_Parametros)" +
                                " ), 0) AS [Recargo_Saneamiento]");

                Str_My_Sql.Append(",ISNULL((" +
                                   " SELECT SUM(frd.Importe_Saldo)" +
                                   " FROM Ope_Cor_Facturacion_Recibos fr" +
                                   " JOIN Ope_Cor_Facturacion_Recibos_Detalles frd ON fr.No_Factura_Recibo = frd.No_Factura_Recibo" +
                                   " JOIN Cat_Cor_Conceptos_Cobros co ON frd.Concepto_ID = co.Concepto_ID" +
                                   " WHERE fr.Estatus_Recibo IN (" +
                                           " 'PENDIENTE'" +
                                           " ,'PARCIAL'" +
                                           " )" +
                                           " AND frd.estatus IN ('PENDIENTE', 'PARCIAL') " +
                                       " AND fr.RPU = p.RPU" +
                                       " AND (" +
                                                "co.Concepto_ID in (SELECT Concepto_Rezago_Agua_Id from Cat_Cor_Parametros)" +
                                                " or co.Concepto_ID in (SELECT Concepto_Rezago_Drenaje_Id from Cat_Cor_Parametros)" +
                                                " or co.Concepto_ID in (SELECT Concepto_Rezago_Saneamiento_Id from Cat_Cor_Parametros)" +
                                            " )" +
                                   " ), 0) AS [Rezago]");


                Str_My_Sql.Append(", ISNULL((" +
                                        " SELECT SUM(frd.Importe_Saldo)" +
                                        " FROM Ope_Cor_Facturacion_Recibos fr" +
                                        " JOIN Ope_Cor_Facturacion_Recibos_Detalles frd ON fr.No_Factura_Recibo = frd.No_Factura_Recibo" +
                                        " JOIN Cat_Cor_Conceptos_Cobros co ON frd.Concepto_ID = co.Concepto_ID" +
                                        " WHERE fr.Estatus_Recibo IN (" +
                                                " 'PENDIENTE'" +
                                                " ,'PARCIAL'" +
                                                ")" +
                                                " AND frd.estatus IN ('PENDIENTE', 'PARCIAL') " +
                                            " AND fr.RPU = p.RPU" +
                                            " AND co.Nombre NOT LIKE '%recargo%'" +
                                            " AND co.Nombre NOT LIKE '%rezago%'" +
                                            " AND co.Nombre NOT LIKE '%agua' " +
                                            " AND co.Nombre NOT LIKE '%AGUA COMERCIAL' " +
                                            " AND co.Nombre NOT LIKE '%drenaje'" +
                                            " AND co.Nombre NOT LIKE '%saneamiento'" +
                                            " AND co.Nombre NOT LIKE '%iva'" +
                    //" AND (co.Concepto_ID in (SELECT Concepto_Cruz_Roja from Cat_Cor_Parametros) " +
                    //        " or co.Concepto_ID in (SELECT Concepto_Bomberos from Cat_Cor_Parametros))" +
                                        "), 0) AS [Otros_Cargos]");


                Str_My_Sql.Append(", p.Region_ID");
                Str_My_Sql.Append(",'' as Sector_ID");
                Str_My_Sql.Append(",p.Colonia_ID");
                Str_My_Sql.Append(",p.Predio_ID");
                Str_My_Sql.Append(",p.No_Cuenta AS No_Cuenta");
                Str_My_Sql.Append(",p.RPU AS Rpu");
                Str_My_Sql.Append(",r.Numero_Region as Region");
                //Str_My_Sql.Append(",s.Numero_Sector as Sector");
                Str_My_Sql.Append(",'' as Sector");
                Str_My_Sql.Append(",p.No_Orden_Reparto");
                Str_My_Sql.Append(",c.NOMBRE AS Colonia");
                Str_My_Sql.Append(",ca.NOMBRE AS Calle");
                Str_My_Sql.Append(",p.Numero_Exterior");
                Str_My_Sql.Append(",(ca.Nombre + ' Ext.' + P.Numero_Exterior) AS Domicilio");
                Str_My_Sql.Append(",(ISNULL(u.NOMBRE, '') + ' ' + ISNULL(u.APELLIDO_PATERNO, '') + ' ' + ISNULL(u.APELLIDO_MATERNO, '')) AS Usuario");
                Str_My_Sql.Append(",(t.Nombre + ' [' + t.Clave + ']') AS Tarifa");
                Str_My_Sql.Append(", 'NO' as Estatus_Proceso");


                Str_My_Sql.Append(", '' as No_Recibo");
                Str_My_Sql.Append(", ss.No_Suspension as No_Suspension");

                //*****************************************************************************************************************************************
                //*****************************************************************************************************************************************
                //  from **********************************************************************************************************************************
                Str_My_Sql.Append(" FROM Ope_Cor_Suspensiones_Servicios ss ");
                Str_My_Sql.Append(" join Cat_Cor_Predios p on p.predio_id = ss.predio_Id");
                Str_My_Sql.Append(" JOIN Ope_Cor_Facturacion_Recibos f ON f.Predio_ID = p.Predio_ID ");
                Str_My_Sql.Append(" JOIN Ope_Cor_Facturacion_Recibos_Detalles fd ON fd.No_Factura_Recibo = f.No_Factura_Recibo");
                Str_My_Sql.Append(" JOIN Cat_Cor_Usuarios u ON  p.Usuario_ID = u.USUARIO_ID");
                Str_My_Sql.Append(" JOIN Cat_Cor_Tarifas t ON t.Tarifa_ID = p.Tarifa_ID");
                Str_My_Sql.Append(" JOIN Cat_Cor_Regiones r ON r.Region_ID = p.Region_ID");
                //Str_My_Sql.Append(" JOIN Cat_Cor_Sectores s ON s.Sector_ID = p.Sector_ID");
                Str_My_Sql.Append(" left outer JOIN Cat_Cor_Colonias c ON c.COLONIA_ID = p.Colonia_ID");
                Str_My_Sql.Append(" left outer JOIN Cat_Cor_Calles ca ON ca.CALLE_ID = p.Calle_ID");
                //Str_My_Sql.Append(" LEFT OUTER JOIN Ope_Cor_Notificaciones n ON n.Predio_ID = p.Predio_ID " +
                //                        " and n.Estatus != 'CANCELADA' ");

                //  Se Obtienen los valores de la base de datos del respaldo del simapag
                String Bd_Simapag = ConfigurationManager.ConnectionStrings["Credito_Cobranza_BDSimapag"].ConnectionString;
                var Obj_Base_Simapag = new System.Data.SqlClient.SqlConnectionStringBuilder(Cls_Conexion.Str_Conexion_CreditoC_Simapag);



                Str_My_Sql.Append(" join " + Obj_Base_Simapag.InitialCatalog + ".dbo.recibos respaldo on respaldo.rpu = p.RPU");

                //*****************************************************************************************************************************************
                //*****************************************************************************************************************************************
                //  where  

                //  notificacion tomando estatus *****************************************
                //Str_My_Sql.Append(" where  (" +
                //                        " n.No_Notificacion is null" +
                //                        " OR" +
                //                            " (n.Estatus not in ('PAGADA', 'PAGADO', 'CERRADA', 'FILTRADA', 'PENDIENTE', 'ESPERA'))" +
                //                    " )");


                //Str_My_Sql.Append(" AND p.Cortado = 'SI'");
                //Str_My_Sql.Append(" AND p.Bloqueado = 'SI'");
                //Str_My_Sql.Append("AND ( P.Cobranza = 'SI'	OR p.Requerido = 'SI' )");
                //********************************************************************************************************************************************************************

                Str_My_Sql.Append(" where ss.Estatus = 'EJECUTADA' " +
                                        " and ss.No_Notificacion is null");


                Str_My_Sql.Append(" and f.Estatus_Recibo IN (" +
                                        "'PENDIENTE'" +
                                        ", 'PARCIAL'" +
                                        ")");


                //*****************************************************************************************************************************************
                //*****************************************************************************************************************************************
                //  group by
                Str_My_Sql.Append(" GROUP BY p.Region_ID");
                Str_My_Sql.Append(", ss.No_Suspension");
                //Str_My_Sql.Append(",p.Sector_ID");
                Str_My_Sql.Append(", respaldo.mesadeudo "); 
                Str_My_Sql.Append(",p.Colonia_ID");
                Str_My_Sql.Append(",p.Predio_ID");
                Str_My_Sql.Append(",p.No_Cuenta");
                Str_My_Sql.Append(",p.RPU");
                Str_My_Sql.Append(",r.Region_ID");
                Str_My_Sql.Append(",r.Numero_Region");
                // Str_My_Sql.Append(",s.Numero_Sector");
                Str_My_Sql.Append(",p.No_Orden_Reparto");
                Str_My_Sql.Append(",c.NOMBRE");
                Str_My_Sql.Append(",ca.NOMBRE");
                Str_My_Sql.Append(",p.Numero_Exterior");
                Str_My_Sql.Append(",p.Numero_Interior");
                Str_My_Sql.Append(",u.NOMBRE");
                Str_My_Sql.Append(",u.APELLIDO_PATERNO");
                Str_My_Sql.Append(",u.APELLIDO_MATERNO");
                Str_My_Sql.Append(",t.Nombre");
                Str_My_Sql.Append(",t.Clave");
                Str_My_Sql.Append(", respaldo.fcorte");

                //*****************************************************************************************************************************************
                //*****************************************************************************************************************************************
                //  order by
                Str_My_Sql.Append(" order by p.no_cuenta asc");

                //*****************************************************************************************************************************************
                //*****************************************************************************************************************************************

                Obj_Comando.CommandText = Str_My_Sql.ToString();
                Obj_Comando.CommandType = CommandType.Text;
                Obj_Comando.CommandTimeout = 600;
                Dr_Lector = Obj_Comando.ExecuteReader();
                Dt_Consulta.Load(Dr_Lector);
                Dt_Consulta.AcceptChanges();
                Dr_Lector.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;
        }




        //*******************************************************************************
        //NOMBRE_FUNCION: Consultar_Predios_Notificacion_Simapag
        //DESCRIPCION: Realiza la consulta de los predios que seran notificados
        //PARAMETROS : 
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 30/Marzo/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public DataTable Consultar_Nuevos_Meses(String Rpu)
        {
            DataTable Dt_Consulta = new DataTable();
            StringBuilder Str_My_Sql = new StringBuilder();

            SqlConnection Obj_Conexion = null;
            SqlCommand Obj_Comando = null;
            SqlDataReader Dr_Lector;

            try
            {
                Obj_Conexion = new SqlConnection(Cls_Conexion.Str_Conexion_CreditoC_Ajuste_Final);
                Obj_Conexion.Open();
                Obj_Comando = Obj_Conexion.CreateCommand();

                Str_My_Sql.Append("select COUNT(*) as Mes" +
                                    " ,(SELECT top 1 Fecha_Emision from Ope_Cor_Facturacion_Recibos where RPU = '" + Rpu + "' order by No_Factura_Recibo desc) as Fecha" +
                                " from Ope_Cor_Facturacion_Recibos" +
                                " where rpu = '" + Rpu + "'" +
                                " and Estatus_Recibo in ('PENDIENTE','PARCIAL')");

                Obj_Comando.CommandText = Str_My_Sql.ToString();
                Obj_Comando.CommandType = CommandType.Text;
                Obj_Comando.CommandTimeout = 600;
                Dr_Lector = Obj_Comando.ExecuteReader();
                Dt_Consulta.Load(Dr_Lector);
                Dt_Consulta.AcceptChanges();
                Dr_Lector.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;
        }



        //*******************************************************************************
        //NOMBRE_FUNCION: Consultar_Predios_Notificacion_Simapag
        //DESCRIPCION: Realiza la consulta de los predios que seran notificados
        //PARAMETROS : 
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 30/Marzo/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public DataTable Consultar_Nuevos_Meses_Sicap(String Rpu)
        {
            DataTable Dt_Consulta = new DataTable();
            StringBuilder Str_My_Sql = new StringBuilder();

            SqlConnection Obj_Conexion = null;
            SqlCommand Obj_Comando = null;
            SqlDataReader Dr_Lector;

            try
            {
                Obj_Conexion = new SqlConnection(Cls_Conexion.Str_Conexion_CreditoC_Simapag);
                Obj_Conexion.Open();
                Obj_Comando = Obj_Conexion.CreateCommand();

                Str_My_Sql.Append("select mesadeudo from recibos " +
                                " where rpu = '" + Rpu + "'");

                Obj_Comando.CommandText = Str_My_Sql.ToString();
                Obj_Comando.CommandType = CommandType.Text;
                Obj_Comando.CommandTimeout = 600;
                Dr_Lector = Obj_Comando.ExecuteReader();
                Dt_Consulta.Load(Dr_Lector);
                Dt_Consulta.AcceptChanges();
                Dr_Lector.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;
        }

        


        //*******************************************************************************
        //NOMBRE_FUNCION: Consultar_Predios_Notificacion_Simapag
        //DESCRIPCION: Realiza la consulta de los predios que seran notificados
        //PARAMETROS : 
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 30/Marzo/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public  DataTable Consultar_Notificaciones_Meses_Adeudo()
        {
            DataTable Dt_Consulta = new DataTable();
            StringBuilder Str_My_Sql = new StringBuilder();

            SqlConnection Obj_Conexion = null;
            SqlCommand Obj_Comando = null;
            SqlDataReader Dr_Lector;

            try
            {
                Obj_Conexion = new SqlConnection(Cls_Conexion.Str_Conexion_CreditoC_Ajuste_Final);
                Obj_Conexion.Open();
                Obj_Comando = Obj_Conexion.CreateCommand();

                Str_My_Sql.Append("Select  RPU , Meses_Adeudo , Fecha_Facturacion  from Ope_Cor_Notificaciones where Estatus IN ('FILTRADA')");

                Obj_Comando.CommandText = Str_My_Sql.ToString();
                Obj_Comando.CommandType = CommandType.Text;
                Obj_Comando.CommandTimeout = 600;
                Dr_Lector = Obj_Comando.ExecuteReader();
                Dt_Consulta.Load(Dr_Lector);
                Dt_Consulta.AcceptChanges();
                Dr_Lector.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;
        }

        //*******************************************************************************
        //NOMBRE_FUNCION: Consultar_Predios_Notificacion_Simapag
        //DESCRIPCION: Realiza la consulta de los predios que seran notificados
        //PARAMETROS : 
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 30/Marzo/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public DataTable Consultar_Fecha_Asignacion(string Rpu)
        {
            DataTable Dt_Consulta = new DataTable();
            StringBuilder Str_My_Sql = new StringBuilder();

            SqlConnection Obj_Conexion = null;
            SqlCommand Obj_Comando = null;
            SqlDataReader Dr_Lector;

            try
            {
                Obj_Conexion = new SqlConnection(Cls_Conexion.Str_Conexion_CreditoC_Contel);
                Obj_Conexion.Open();
                Obj_Comando = Obj_Conexion.CreateCommand();

                //*****************************************************************************************************************************************
                //*****************************************************************************************************************************************

                Str_My_Sql.Append("SELECT fcorte as Corte from recibos "+
                                    "where rpu ='" + Rpu +"'");

                Obj_Comando.CommandText = Str_My_Sql.ToString();
                Obj_Comando.CommandType = CommandType.Text;
                Obj_Comando.CommandTimeout = 6000;
                Dr_Lector = Obj_Comando.ExecuteReader();
                Dt_Consulta.Load(Dr_Lector);
                Dt_Consulta.AcceptChanges();
                Dr_Lector.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;
        }



        public DataTable Consultar_Notificaciones_Saldos(String Str_Rpu) 
        {
            DataTable Dt_Consulta = null;
            String strSql = "";
            
            DataSet ds;
            SqlDataAdapter da;


            using (SqlConnection conexion = new SqlConnection(Cls_Conexion.Str_Conexion_CreditoC_Ajuste_Saldos))
            {
                conexion.Open();

                using (SqlCommand comando = conexion.CreateCommand())
                {

                    strSql = "SELECT * from  Ope_Cor_Notificaciones";

                    if (!String.IsNullOrEmpty(Str_Rpu))
                    {
                        strSql += " where rpu = '" + Str_Rpu + "'";
                    }


                    comando.CommandText = strSql;
                    comando.CommandTimeout = 600;
                    da = new SqlDataAdapter(comando);
                    ds = new DataSet();
                    da.Fill(ds);

                    Dt_Consulta = ds.Tables[0];


                } // comando

            } // conexion




            return Dt_Consulta;

        }// fin de consulta
        
    }
}
