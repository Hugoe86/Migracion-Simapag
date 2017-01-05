using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Migracion_Facturacion
{
    class class_pagos
    {
        public DataTable CargarPagos_Caso1(string db_origen)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnn = new SqlConnection("Data Source=172.16.0.115;Initial Catalog="+db_origen+";User ID=usrsimapag;Password=C0nt3l16"))
            {
                string ConsultaPagos = "SELECT rpu,(SELECT TOTAL FROM recibos "
                    +"WHERE foliorecib = pagos.foliorecib) AS total_pagar,"
	                +"importe,efectivo,cheque,tdc,fechapago,foliorecib,"
	                +"importe-piva as importe_cobrado,piva,ivaagua,importe as total_cobrado "
	                +"FROM pagos WHERE foliorecib IN "
	                +"(SELECT foliorecib FROM recibos "
	                +"WHERE suspendido = 0 AND rezagua = 0 AND ISNUMERIC(RPU) = 1 "
	                +"AND foliorecib > 0 AND sector <> 99 "
	                +"AND MONTH(facturacion) = 5 AND YEAR(facturacion) = 2016) "
                    +"ORDER BY foliorecib";
                SqlCommand cmd = new SqlCommand(ConsultaPagos, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable CargarPagos_Caso2(string db_origen)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnn = new SqlConnection("Data Source=172.16.0.115;Initial Catalog=" + db_origen + ";User ID=usrsimapag;Password=C0nt3l16"))
            {
                string ConsultaPagos = "SELECT rpu,(SELECT TOTAL FROM recibos "
                    + "WHERE foliorecib = pagos.foliorecib) AS total_pagar,"
                    + "importe,efectivo,cheque,tdc,fechapago,foliorecib,"
                    + "importe-piva as importe_cobrado,piva,ivaagua,importe as total_cobrado "
                    + "FROM pagos WHERE foliorecib IN "
                    + "(SELECT foliorecib FROM recibos "
                    + "WHERE (suspendido = 0 AND rezagua = 0 AND ISNUMERIC(RPU) = 1 "
                    + "AND foliorecib > 0 AND sector <> 99 "
                    + "AND MONTH(facturacion) <> 5) OR "
                    +"(suspendido = 0 AND rezagua = 0 AND ISNUMERIC(RPU) = 1 "
                    + "AND foliorecib > 0 AND sector <> 99 "
                    + "AND YEAR(facturacion) <> 2016))"
                    + "ORDER BY foliorecib";
                SqlCommand cmd = new SqlCommand(ConsultaPagos, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
    }
}
