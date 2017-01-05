using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Migracion_Facturacion
{
    class class_detalles
    {
        public DataTable Cargar_Sanicones_Activas(string db_origen, string db_destino) //----- caso 1: suspendido = 0 & rezagua = 0 (ACTUALES)-----
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnn = new SqlConnection("Data Source=172.16.0.115;Initial Catalog=" + db_origen + ";User ID=usrsimapag;Password=C0nt3l16"))
            {
                string ConsultaRecibos = "SELECT " +
                                                    "i.rpu,p.Predio_ID,g.Giro_ID,g.Descripcion,RTRIM(LTRIM(i.inspeccion)) as inspeccion " +
                                                "FROM inspeccion as i " +
                                                "JOIN " + db_destino + ".dbo.Cat_Cor_Predios as p on p.RPU=i.rpu " +
                                                "JOIN " + db_destino + ".dbo.Cat_Cor_Giros_Actividades as g on g.Actividad_Giro_ID = p.Giro_Actividad_ID " +
                                                "WHERE i.inspeccion LIKE 'SANCIO%' " +
                                                "AND YEAR(i.fecha) > 2014 " +
                                                "AND i.inspeccion NOT LIKE '%PAGA%' " +
                                                "AND i.inspeccion NOT LIKE '%REINC%' " +
                                                "AND i.inspeccion NOT LIKE '%Cond%' " +
                                                "AND i.inspeccion NOT LIKE '%Cance%'";
                SqlCommand cmd = new SqlCommand(ConsultaRecibos, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }

        public DataTable Cargar_Sanicones_Reincidir(string db_origen, string db_destino) //----- caso 1: suspendido = 0 & rezagua = 0 (ACTUALES)-----
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnn = new SqlConnection("Data Source=172.16.0.115;Initial Catalog=" + db_origen + ";User ID=usrsimapag;Password=C0nt3l16"))
            {
                string ConsultaRecibos = "SELECT " +
                                                    "i.rpu,p.Predio_ID,g.Giro_ID,g.Descripcion,RTRIM(LTRIM(i.inspeccion)) as inspeccion " +
                                                "FROM inspeccion as i " +
                                                "JOIN " + db_destino + ".dbo.Cat_Cor_Predios as p on p.RPU=i.rpu " +
                                                "JOIN " + db_destino + ".dbo.Cat_Cor_Giros_Actividades as g on g.Actividad_Giro_ID = p.Giro_Actividad_ID " +
                                                "WHERE i.inspeccion LIKE 'SANCIO%' " +
                                                "AND YEAR(i.fecha) > 2014 " +
                                                "AND i.inspeccion NOT LIKE '%PAGA%' " +
                                                "AND i.inspeccion  LIKE '%REINC%' " +
                                                "AND i.inspeccion NOT LIKE '%Cond%' " +
                                                "AND i.inspeccion NOT LIKE '%Cance%'";
                SqlCommand cmd = new SqlCommand(ConsultaRecibos, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }

        public DataTable Cargar_Concepto_Cobro(string db_origen, string db_destino,string Inspeccion) //----- caso 1: suspendido = 0 & rezagua = 0 (ACTUALES)-----
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnn = new SqlConnection("Data Source=172.16.0.115;Initial Catalog=" + db_destino + ";User ID=usrsimapag;Password=C0nt3l16"))
            {
                string ConsultaRecibos = "SELECT TOP 1 Concepto_ID from Cat_Cor_Conceptos_Cobros WHERE concepto_categoria_id ='00003' and Nombre like '%" + Inspeccion + "%' and Nombre NOT LIKE '%Descuento%'";
                SqlCommand cmd = new SqlCommand(ConsultaRecibos, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }

        public DataTable CargarDetalles_Caso1(string db_origen,string db_destino) //----- caso 1: suspendido = 0 & rezagua = 0 (ACTUALES)-----
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnn = new SqlConnection("Data Source=172.16.0.115;Initial Catalog="+db_origen+";User ID=usrsimapag;Password=C0nt3l16"))
            {
                string ConsultaRecibos = "SELECT (SELECT no_factura_recibo FROM "+db_destino+".dbo.Ope_Cor_Facturacion_Recibos WHERE R.foliorecib = No_Recibo)as no_factura,"
	                +"(SELECT iva from tarifas where codtarifa = R.tarifa and fecha like '2016%')as ivaagua,"
	                +"R.pagua,R.palcan,R.psanea,R.crbomb,R.ppagua,R.ppalcan,R.ppsanea,R.pcrbomb,"
	                +"R.iva,R.piva,YEAR(facturacion)as anio, "
	                +"(MONTH(facturacion)-1) as bimestre,R.rpu FROM recibos R "
	                +"FULL OUTER JOIN "+db_destino+".dbo.Ope_Cor_Facturacion_Recibos F ON F.RPU = R.rpu "
                    +"WHERE ISNUMERIC(R.rpu)=1 AND foliorecib > 0 AND sector<>99 "
	                +"AND R.suspendido = 0 AND R.rezagua = 0 AND YEAR(R.facturacion)=2016 AND MONTH(R.facturacion)=5";
                SqlCommand cmd = new SqlCommand(ConsultaRecibos, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }

        public DataTable CargarDetalles_Caso2(string db_origen, string db_destino) //----- caso 2: suspendido = 0 & rezagua = 0 (NO ACTUALES)-----
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnn = new SqlConnection("Data Source=172.16.0.115;Initial Catalog=" + db_origen + ";User ID=usrsimapag;Password=C0nt3l16"))
            {
                string ConsultaRecibos = "SELECT (SELECT no_factura_recibo FROM " + db_destino + ".dbo.Ope_Cor_Facturacion_Recibos WHERE R.foliorecib = No_Recibo)as no_factura,"
                    + "(SELECT iva from tarifas where codtarifa = R.tarifa and fecha like '2016%')as ivaagua,"
                    + "R.pagua,R.palcan,R.psanea,R.crbomb,R.ppagua,R.ppalcan,R.ppsanea,R.pcrbomb,"
                    + "R.iva,R.piva,YEAR(facturacion)as anio, "
                    + "(MONTH(facturacion)-1) as bimestre,R.rpu FROM recibos R "
                    + "FULL OUTER JOIN " + db_destino + ".dbo.Ope_Cor_Facturacion_Recibos F ON F.RPU = R.rpu "
                    + "WHERE (ISNUMERIC(R.rpu)=1 AND foliorecib > 0 AND sector<>99 "
                    + "AND R.suspendido = 0 AND R.rezagua = 0 AND YEAR(R.facturacion)<>2016) OR "
                    +"(ISNUMERIC(R.rpu)=1 AND foliorecib > 0 AND sector<>99 "
                    + "AND R.suspendido = 0 AND R.rezagua = 0 AND MONTH(R.facturacion)<>5)";
                SqlCommand cmd = new SqlCommand(ConsultaRecibos, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }

        public DataTable CargarDetalles_Caso3(string db_origen, string db_destino) //----- caso 3: suspendido = 0 & rezagua > 0 -----
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnn = new SqlConnection("Data Source=172.16.0.115;Initial Catalog=" + db_origen + ";User ID=usrsimapag;Password=C0nt3l16"))
            {
                string ConsultaRecibos = "SELECT F.No_Factura_Recibo,"
                    + "(SELECT iva from tarifas where codtarifa = R.tarifa and fecha like '2016%')as ivaagua,"
                    + "R.pagua,R.palcan,R.psanea,R.rezagua,R.rezalcan,R.rezsanea,R.recagua,R.recalcan,R.recsanea,R.recotros,R.crbomb,R.otros,"
                    + "R.iva,YEAR(facturacion)as anio,(MONTH(facturacion)-1) as bimestre,R.rpu FROM recibos R "
                    + "JOIN " + db_destino + ".dbo.Ope_Cor_Facturacion_Recibos F ON F.RPU = R.rpu "
                    + "WHERE ISNUMERIC(R.rpu)=1 AND foliorecib>0 AND sector<>99 "
                    + "AND R.suspendido=0 AND R.rezagua>0 ORDER BY F.No_Factura_Recibo";
                SqlCommand cmd = new SqlCommand(ConsultaRecibos, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }
    }
}
