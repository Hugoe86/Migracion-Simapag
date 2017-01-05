using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Migracion_Facturacion
{
    class class_recibos
    {
        string serv = "200.33.34.9"; //172.16.0.115
        string user = "usrsimapag"; //usrsimapag
        string pass = "C0nt3l16"; //C0nt3l16

        public DataTable CargarRecibos_Caso1(string db_origen,string db_destino) //----- caso 1: RECIBOS SIN REZAGO -----
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnn = new SqlConnection("Data Source=" + serv + ";Initial Catalog=" + db_origen + ";User ID=" + user + ";Password=" + pass + ""))
            {
                string ConsultaRecibos = "SET LANGUAGE Spanish SELECT cuenta,foliorecib,P.Region_ID,P.Predio_ID,P.Usuario_ID,nummedidor,P.Tarifa_ID,lecanterior,lecactual,consumo,"
                    + "SUBSTRING(periodo,0,11) as fecha_inicio,SUBSTRING(periodo,12,10) as fecha_termino, case when convert( varchar (10),vencimient,103)='30/12/1899' then '01/01/1991' else convert( varchar (10),vencimient,103) end  as vencimient,convert( varchar (10),facturacion ,103) as facturacion,periodo,"
                    + "(SELECT iva from tarifas where codtarifa = R.tarifa and fecha like '2016%')as ivaagua,"
                    + "R.pagua,R.palcan,R.psanea,R.crbomb,R.iva,R.ppagua,R.ppalcan,R.ppsanea,R.pcrbomb,R.piva,R.panticipo,"
                    + "CASE WHEN (MONTH(facturacion)-1) =0 THEN YEAR(facturacion)-1 "
                    + "  ELSE YEAR(facturacion) "
                    + "END as anio,"
                    + "CASE WHEN (MONTH(facturacion)-1) =0 THEN 12 "
                    + "  ELSE (MONTH(facturacion)-1)  "
                    + "END as bimestre,R.rpu, case  when convert( varchar (10),R.fechapago,103) = '30/12/1899' then '01/01/1991' else convert( varchar (10),R.fechapago,103) end  as fechapago FROM recibos R "
                    + "FULL OUTER JOIN " + db_destino + ".dbo.Cat_Cor_Predios P ON P.RPU = R.rpu "
                    + "WHERE ISNUMERIC(R.rpu)=1 AND foliorecib > 0 AND sector<>99 "
                    + "AND R.suspendido = 0 AND R.rezagua = 0 AND P.Predio_ID IS NOT NULL ";
                    //+"and R.rpu = '000910900013'";

                SqlCommand cmd = new SqlCommand(ConsultaRecibos, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }

        public DataTable CargarRecibos_Caso3(string db_origen, string db_destino) //----- caso 3: RECIBOS CON REZAGO -----
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = new SqlConnection("Data Source="+serv+";Initial Catalog=" + db_origen + ";User ID="+user+";Password="+pass+""))
            {
                string ConsultaRecibos = "SET LANGUAGE Spanish SELECT cuenta,foliorecib,P.Region_ID,P.Predio_ID,P.Usuario_ID,nummedidor,P.Tarifa_ID,lecanterior,lecactual,consumo,"
                    + "\n" + "SUBSTRING(periodo,0,11) as fecha_inicio,"
                    + "\n" + "CASE WHEN SUBSTRING(SUBSTRING(periodo,12,10),6,2) NOT like '/%' THEN STUFF(SUBSTRING(periodo,12,10),6,4,'/'+SUBSTRING(SUBSTRING(periodo,12,10),6,4)) "
                    + "\n" + "   ELSE SUBSTRING(periodo,12,10) "
                    + "\n" + "END as fecha_termino,"
                    + "\n" + "case when convert( varchar (10),vencimient,103)='30/12/1899' then '01/01/1991' else convert( varchar (10),vencimient,103) end as vencimient, convert( varchar (10),facturacion,103) as facturacion,periodo,"
                    + "\n" + "(SELECT iva from tarifas where codtarifa = R.tarifa and fecha like '2016%')as ivaagua,"
                    + "\n" + "R.pagua,R.palcan,R.psanea,R.rezagua,R.rezalcan,R.rezsanea,R.recagua,R.recalcan,R.recsanea,R.crbomb,R.iva,R.importepago,"
                    + "\n" + "R.ppagua,R.ppalcan,R.ppsanea,R.prezagua,R.prezalcan,R.prezsanea,R.precagua,R.precalcan,R.precsanea,R.pcrbomb,R.piva,R.panticipo,"
                    + "\n" + "CASE WHEN (MONTH(facturacion)-1) =0 THEN YEAR(facturacion)-1 "
                    + "\n" + "  ELSE YEAR(facturacion) "
                    + "\n" + "END as anio,"
                    + "\n" + "CASE WHEN (MONTH(facturacion)-1) =0 THEN 12 "
                    + "\n" + "  ELSE (MONTH(facturacion)-1)  "
                    + "\n" + "END as bimestre,R.rpu,case  when convert( varchar (10),R.fechapago,103) = '30/12/1899' then '01/01/1991' else convert( varchar (10),R.fechapago,103) end  as fechapago "
                    + "\n" + ", (select "
                    + "\n" + "top 1 CONVERT(VARCHAR(10),hr.facturacion,103) "
                    + "\n" + "from hisrecibos hr "
                    + "\n" + "    where hr.rpu = R.rpu "
                    + "\n" + "        and (hr.estado = 'pagado' "
                    + "\n" + "       OR hr.estado = 'Conv Admvo' "
                    + "\n" + "        OR hr.estado = 'A favor') "
                    + "\n" + "  order by hr.facturacion DESC) fecha_con "
                    + "\n" + ", (select "
                    + "\n" + "top 1 CONVERT(VARCHAR(10),GETDATE(),103)	"
                    + "\n" + "from hisrecibos hr "
                    + "\n" + "    where hr.rpu = R.rpu "
                    + "\n" + "        and (hr.estado = 'PAGADO' "
                    + "\n" + "        OR hr.estado = 'Conv Admvo' "
                    + "\n" + "        OR hr.estado = 'A FAVOR' "
                    + "\n" + "        OR hr.estado = 'SUBSIDIO') "
                    + "\n" + "   order by hr.facturacion DESC) fecha_act "
                    + "\n" + "FROM recibos R "
                    + "\n" + "FULL OUTER JOIN " + db_destino + ".dbo.Cat_Cor_Predios P ON P.RPU = R.rpu "
                    + "\n" + "WHERE (ISNUMERIC(R.rpu)=1 AND foliorecib>0 AND sector<>99 AND R.tarifa IN "
                    + "\n" + "(SELECT codtarifa from tarifas where fecha LIKE '2016%') "
                    + "\n" + "AND Suspendido=0 AND Rezagua>0 "
                    + "\n" + "AND R.rpu IN (SELECT rpu FROM hisrecibos GROUP BY rpu HAVING COUNT(rpu)>0) "
                    + "\n" + " ) OR "  // AGRUEGAR RPU AQUI
                    //+ "AND R.rpu = '000040817258' ) OR " // AGRUEGAR RPU AQUI
                    + "\n" + "(ISNUMERIC(R.rpu)=1 AND foliorecib>0 AND sector<>99 AND R.tarifa IN "
                    + "\n" + "(SELECT codtarifa from tarifas where fecha LIKE '2016%') "
                    + "\n" + "AND Suspendido<>0 "
                    + "\n" + "AND R.rpu IN (SELECT rpu FROM hisrecibos GROUP BY rpu HAVING COUNT(rpu)>0) "
                    //+ " AND R.rpu = '000040817258')";  // AGREGAR RPU AQUI TAMBIEN
                    + "  )";  // AGREGAR RPU AQUI TAMBIEN 

                SqlCommand cmd = new SqlCommand(ConsultaRecibos, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }

        public DataTable CargarRecibos_Caso5(string db_origen, string db_destino) //----- caso 5: RECIBOS SIN HOSTORIAL -----
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = new SqlConnection("Data Source="+serv+";Initial Catalog=" + db_origen + ";User ID="+user+";Password="+pass+""))
            {
                string ConsultaRecibos = "SET LANGUAGE Spanish SELECT cuenta,foliorecib,P.Region_ID,P.Predio_ID,P.Usuario_ID,nummedidor,P.Tarifa_ID,lecanterior,lecactual,consumo,"
                    + "SUBSTRING(periodo,0,11) as fecha_inicio,"
                    + "CASE WHEN SUBSTRING(SUBSTRING(periodo,12,10),6,2) NOT like '/%' THEN STUFF(SUBSTRING(periodo,12,10),6,4,'/'+SUBSTRING(SUBSTRING(periodo,12,10),6,4)) "
                    + "   ELSE SUBSTRING(periodo,12,10) "
                    + "END as fecha_termino,"
                    + "case when convert( varchar (10),vencimient,103)='30/12/1899' then '01/01/1991' else convert( varchar (10),vencimient,103) end as vencimient,convert( varchar (10),facturacion,103) as facturacion,periodo,"
                    + "(SELECT iva from tarifas where codtarifa = R.tarifa and fecha like '2016%')as ivaagua,"
                    + "R.pagua,R.palcan,R.psanea,R.rezagua,R.rezalcan,R.rezsanea,R.recagua,R.recalcan,R.recsanea,R.crbomb,R.iva,R.importepago,"
                    + "R.ppagua,R.ppalcan,R.ppsanea,R.prezagua,R.prezalcan,R.prezsanea,R.precagua,R.precalcan,R.precsanea,R.pcrbomb,R.piva,R.panticipo,"
                    + "CASE WHEN (MONTH(facturacion)-1) =0 THEN YEAR(facturacion)-1 "
                    + "  ELSE YEAR(facturacion) "
                    + "END as anio,"
                    + "CASE WHEN (MONTH(facturacion)-1) =0 THEN 12 "
                    + "  ELSE (MONTH(facturacion)-1)  "
                    + "END as bimestre,R.rpu,case  when convert( varchar (10),R.fechapago,103) = '30/12/1899' then '01/01/1991' else convert( varchar (10),R.fechapago,103) end  as fechapago FROM recibos R "
                    + "FULL OUTER JOIN " + db_destino + ".dbo.Cat_Cor_Predios P ON P.RPU = R.rpu "
                    + "WHERE ISNUMERIC(R.rpu)=1 AND foliorecib>0 AND sector<>99 AND R.tarifa IN "
                    + "(SELECT codtarifa from tarifas where fecha LIKE '2016%') "
                    + "AND R.rpu NOT IN (SELECT rpu FROM hisrecibos GROUP BY rpu HAVING COUNT(rpu)>0) ";
                //+ "And R.rpu = '000981254767'";

                SqlCommand cmd = new SqlCommand(ConsultaRecibos, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }

        public DataTable CargarRecibos_Caso6(string db_origen, string db_destino) //----- caso 6: RECIBOS SALDOS INCORRECTOS -----
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = new SqlConnection("Data Source=" + serv + ";Initial Catalog=" + db_origen + ";User ID=" + user + ";Password=" + pass + ""))
            {
                string ConsultaRecibos = "SET LANGUAGE Spanish SELECT cuenta,foliorecib,P.Region_ID,P.Predio_ID,P.Usuario_ID,nummedidor,P.Tarifa_ID,lecanterior,lecactual,consumo,"
                    + "SUBSTRING(periodo,0,11) as fecha_inicio,"
                    + "CASE WHEN SUBSTRING(SUBSTRING(periodo,12,10),6,2) NOT like '/%' THEN STUFF(SUBSTRING(periodo,12,10),6,4,'/'+SUBSTRING(SUBSTRING(periodo,12,10),6,4)) "
                    + "   ELSE SUBSTRING(periodo,12,10) "
                    + "END as fecha_termino,"
                    + "case when convert( varchar (10),vencimient,103)='30/12/1899' then '01/01/1991' else convert( varchar (10),vencimient,103) end as vencimient,convert( varchar (10),facturacion,103) as facturacion,periodo,"
                    + "(SELECT iva from tarifas where codtarifa = R.tarifa and fecha like '2016%')as ivaagua,"
                    + "R.pagua,R.palcan,R.psanea,R.rezagua,R.rezalcan,R.rezsanea,R.recagua,R.recalcan,R.recsanea,R.crbomb,R.iva,R.importepago,"
                    + "R.ppagua,R.ppalcan,R.ppsanea,R.prezagua,R.prezalcan,R.prezsanea,R.precagua,R.precalcan,R.precsanea,R.pcrbomb,R.piva,R.panticipo,"
                    + "CASE WHEN (MONTH(facturacion)-1) =0 THEN YEAR(facturacion)-1 "
                    + "  ELSE YEAR(facturacion) "
                    + "END as anio,"
                    + "CASE WHEN (MONTH(facturacion)-1) =0 THEN 12 "
                    + "  ELSE (MONTH(facturacion)-1)  "
                    + "END as bimestre,R.rpu,convert( varchar (10),R.fechapago,103) as fechapago FROM recibos R "
                    + "FULL OUTER JOIN " + db_destino + ".dbo.Cat_Cor_Predios P ON P.RPU = R.rpu "
                    + "WHERE R.rpu IN "
                    + "(SELECT F.RPU FROM " + db_destino + ".dbo.Ope_Cor_Facturacion_Recibos F JOIN recibos Re ON F.RPU = Re.rpu "
                    + "GROUP BY F.RPU,Re.total,Re.importepago "
                    + "HAVING SUM(F.Saldo) - "
                    + "     (SELECT (pagua+palcan+psanea+rezagua+rezalcan+rezsanea+recagua+recalcan+recsanea+iva+crbomb)-"
                    + "         (ppagua+ppalcan+ppsanea+prezagua+prezalcan+prezsanea+precagua+precalcan+precsanea+piva+pcrbomb) "
                    + "     FROM " + db_origen + ".dbo.recibos WHERE rpu = F.RPU) "
                    + "NOT BETWEEN -10 AND 10 "
                    + ") ";
                //+ "And R.rpu = '000981254767'";

                SqlCommand cmd = new SqlCommand(ConsultaRecibos, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }

        public DataTable Cargar_Recibos_Caso_Aux(string db_origen, string db_destino) // ****** CASO AUXILIAR ****** \\
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = new SqlConnection("Data Source=" + serv + ";Initial Catalog=" + db_origen + ";User ID=" + user + ";Password=" + pass + ""))
            {
                string ConsultaRecibos = "SET LANGUAGE Spanish SELECT DISTINCT "
	                + " h.rpu "
	                + ",(SELECT top 1 cuenta FROM hisrecibos WHERE rpu=h.rpu) as cuenta "
	                + ",(SELECT Region_ID FROM "+db_destino+".dbo.Cat_Cor_Predios WHERE RPU = h.rpu) as Region_ID "
	                + ",(SELECT Predio_ID FROM "+db_destino+".dbo.Cat_Cor_Predios WHERE RPU = h.rpu) as Usuario_ID "
	                + ",(SELECT Usuario_ID FROM "+db_destino+".dbo.Cat_Cor_Predios WHERE RPU = h.rpu) as Predio_ID "
	                + ",(SELECT top 1 nummedidor FROM hisrecibos WHERE rpu=h.rpu) as nummedidor "
	                + ",(SELECT Tarifa_ID FROM "+db_origen+".dbo.Cat_Cor_Predios WHERE RPU = h.rpu) as Tarifa_ID "
                    + "FROM (SELECT DISTINCT rpu FROM hisrecibos WHERE ISNUMERIC(rpu)=1) "
                //+ "WHERE R.rpu = '000981254767'";
                    +" AS h";

                SqlCommand cmd = new SqlCommand(ConsultaRecibos, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable CargarTarifasDetalles(string db_destino) //----- tarifas detalles para conocer cuota base, cuota consumo y precio m3 -----
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = new SqlConnection("Data Source=" + serv + ";Initial Catalog=" + db_destino + ";User ID=" + user + ";Password=" + pass + ""))
            {
                string ConsultaRecibos = "SELECT * FROM Cat_Cor_Tarifas_Detalles WHERE Año = 2016";
                SqlCommand cmd = new SqlCommand(ConsultaRecibos, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }


        public DataTable Consulta_historico(string rpu, string db_origen)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = new SqlConnection("Data Source=" + serv + ";Initial Catalog=" + db_origen + ";User ID=" + user + ";Password=" + pass + ""))
            {
                string Consulta_foliorecib = "SET LANGUAGE Spanish SELECT foliorecib,lecactual,lecanterior,consumo,SUBSTRING(periodo,0,11) as fecha_inicio,"
                    + "CASE WHEN SUBSTRING(SUBSTRING(periodo,12,10),6,2) NOT like '/%' THEN STUFF(SUBSTRING(periodo,12,10),6,4,'/'+SUBSTRING(SUBSTRING(periodo,12,10),6,4)) "
                    + "    ELSE SUBSTRING(periodo,12,10) "
                    + "END as fecha_termino,"
                    + "case when convert( varchar (10),vencimient,103)='30/12/1899' then '01/01/1991' else convert( varchar (10),vencimient,103) end as vencimient,convert( varchar (10),facturacion,103) as facturacion,periodo,"
                    + "pagua,palcan,psanea,rezagua,rezalcan,rezsanea,recagua,recalcan,recsanea,crbomb,"
                    + "iva,importepago,"
                    + "ppagua,ppalcan,ppsanea,prezagua,prezalcan,prezsanea,precagua,precalcan,precsanea,pcrbomb,piva,"
                    + "estado,"
                    + "CASE WHEN (MONTH(facturacion)-1) =0 THEN YEAR(facturacion)-1 "
                    + "  ELSE YEAR(facturacion) "
                    + "END as anio,"
                    + "CASE WHEN (MONTH(facturacion)-1) = 0 THEN 12 "
                    + " ELSE (MONTH(facturacion)-1) "
                    + "END as bimestre,case  when convert( varchar (10),fechapago,103) = '30/12/1899' then '01/01/1991' else convert( varchar (10),fechapago,103) end  as fechapago FROM hisrecibos ";

                if (!string.IsNullOrEmpty(rpu))
                {
                    Consulta_foliorecib = Consulta_foliorecib + "WHERE rpu = '" + rpu + "' AND estado<>'Modificado' AND foliorecib>0 ";
                }
                Consulta_foliorecib = Consulta_foliorecib + " ORDER by rpu, hisrecibos.facturacion DESC";

                SqlCommand cmd = new SqlCommand(Consulta_foliorecib, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable Consulta_Rango_fechas(string rpu, string db_origen)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = new SqlConnection("Data Source=" + serv + ";Initial Catalog=" + db_origen + ";User ID=" + user + ";Password=" + pass + ""))
            {
                string Consulta_foliorecib = "SET LANGUAGE Spanish SELECT top 1 CONVERT(VARCHAR(10),facturacion,103)as fecha_con,CONVERT(VARCHAR(10),GETDATE(),103)as fecha_act from hisrecibos "
                    + "where (rpu = '" + rpu + "' AND estado = 'pagado') OR (rpu = '" + rpu + "' AND estado = 'Conv Admvo') OR (rpu = '" + rpu + "' AND estado = 'A favor')"
                    + "OR (rpu = '" + rpu + "' AND estado = 'SUBSIDIO') "
                    + "order by facturacion DESC";
                SqlCommand cmd = new SqlCommand(Consulta_foliorecib, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable Consulta_historico_confechas(string rpu, string fecha_con, string fecha_act, string db_origen)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = new SqlConnection("Data Source=" + serv + ";Initial Catalog=" + db_origen + ";User ID=" + user + ";Password=" + pass + ""))
            {
                string Consulta_foliorecib = "SET LANGUAGE Spanish SELECT foliorecib,lecactual,lecanterior,consumo,SUBSTRING(periodo,0,11) as fecha_inicio,"
                    + "CASE WHEN SUBSTRING(SUBSTRING(periodo,12,10),6,2) NOT like '/%' THEN STUFF(SUBSTRING(periodo,12,10),6,4,'/'+SUBSTRING(SUBSTRING(periodo,12,10),6,4)) "
                    + "    ELSE SUBSTRING(periodo,12,10) "
                    + "END as fecha_termino,"
                    + "case when convert( varchar (10),vencimient,103)='30/12/1899' then '01/01/1991' else convert( varchar (10),vencimient,103) end as vencimient,convert( varchar (10),facturacion,103) as facturacion,periodo,"
                    + "pagua,palcan,psanea,rezagua,rezalcan,rezsanea,recagua,recalcan,recsanea,crbomb,"
                    + "iva,importepago,"
                    + "ppagua,ppalcan,ppsanea,prezagua,prezalcan,prezsanea,precagua,precalcan,precsanea,pcrbomb,piva,"
                    + "estado,"
                    + "CASE WHEN (MONTH(facturacion)-1) =0 THEN YEAR(facturacion)-1 "
                    + "  ELSE YEAR(facturacion) "
                    + "END as anio,"
                    + "CASE WHEN (MONTH(facturacion)-1) = 0 THEN 12 "
                    + " ELSE (MONTH(facturacion)-1) "
                    + "END as bimestre,case  when convert( varchar (10),fechapago,103) = '30/12/1899' then '01/01/1991' else convert( varchar (10),fechapago,103) end  as fechapago FROM hisrecibos "
                    + "WHERE rpu = '" + rpu + "' AND hisrecibos.facturacion BETWEEN '" + fecha_con + "' AND '" + fecha_act + "' AND estado<>'Modificado' "
                    + "ORDER by hisrecibos.facturacion DESC";
                SqlCommand cmd = new SqlCommand(Consulta_foliorecib, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable Consulta_historico_inverso(string rpu, string fecha_con, string db_origen)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = new SqlConnection("Data Source=" + serv + ";Initial Catalog=" + db_origen + ";User ID=" + user + ";Password=" + pass + ""))
            {
                string Consulta_foliorecib = "SET LANGUAGE Spanish SELECT foliorecib,lecactual,lecanterior,consumo,SUBSTRING(periodo,0,11) as fecha_inicio,"
                    + "CASE WHEN SUBSTRING(SUBSTRING(periodo,12,10),6,2) NOT like '/%' THEN STUFF(SUBSTRING(periodo,12,10),6,4,'/'+SUBSTRING(SUBSTRING(periodo,12,10),6,4)) "
                    + "    ELSE SUBSTRING(periodo,12,10) "
                    + "END as fecha_termino,"
                    + "case when convert( varchar (10),vencimient,103)='30/12/1899' then '01/01/1991' else convert( varchar (10),vencimient,103) end as vencimient,convert( varchar (10),facturacion,103) as facturacion,periodo,"
                    + "pagua,palcan,psanea,rezagua,rezalcan,rezsanea,recagua,recalcan,recsanea,crbomb,"
                    + "iva,importepago,"
                    + "ppagua,ppalcan,ppsanea,prezagua,prezalcan,prezsanea,precagua,precalcan,precsanea,pcrbomb,piva,"
                    + "estado,"
                    + "CASE WHEN (MONTH(facturacion)-1) =0 THEN YEAR(facturacion)-1 "
                    + "  ELSE YEAR(facturacion) "
                    + "END as anio,"
                    + "CASE WHEN (MONTH(facturacion)-1) = 0 THEN 12 "
                    + " ELSE (MONTH(facturacion)-1) "
                    + "END as bimestre,case  when convert( varchar (10),fechapago,103) = '30/12/1899' then '01/01/1991' else convert( varchar (10),fechapago,103) end  as fechapago FROM hisrecibos "
                    + "WHERE rpu = '" + rpu + "' AND hisrecibos.facturacion BETWEEN '1899-01-01' AND '" + fecha_con + "' AND estado<>'Modificado' "
                    + "ORDER by hisrecibos.facturacion DESC";
                SqlCommand cmd = new SqlCommand(Consulta_foliorecib, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable CargarTODO(string db_origen, string db_destino) //----- caso 1: RECIBOS SIN REZAGO -----
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = new SqlConnection("Data Source=" + serv + ";Initial Catalog=" + db_origen + ";User ID=" + user + ";Password=" + pass + ""))
            {
                string ConsultaRecibos = "SET LANGUAGE Spanish SELECT R.RPU, importepago FROM recibos R "
                    + "FULL OUTER JOIN " + db_destino + ".dbo.Cat_Cor_Predios P ON P.RPU = R.rpu "
                    + "WHERE ISNUMERIC(R.rpu)=1 AND foliorecib > 0 AND sector<>99 ";

                SqlCommand cmd = new SqlCommand(ConsultaRecibos, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }

        public int Consulta_id(string db_destino)
        {
            DataTable dt = new DataTable();
            int result;

            using (SqlConnection cnn = new SqlConnection("Data Source=" + serv + ";Initial Catalog=" + db_destino + ";User ID=" + user + ";Password=" + pass + ""))
            {
                string Consulta_No_factura = "SELECT top 1 No_factura_Recibo as no_fact FROM Ope_Cor_Facturacion_Recibos ORDER BY No_factura_Recibo DESC";
                SqlCommand cmd = new SqlCommand(Consulta_No_factura, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            if (dt.Rows.Count > 0)
            {
                DataRow row;
                row = dt.Rows[0];
                result = int.Parse(row["no_fact"].ToString());
            }
            else
            {
                result = 0;
            }
            return result;
        }

        public int Consulta_id_caso4(string db_destino)
        {
            DataTable dt = new DataTable();
            int result;

            using (SqlConnection cnn = new SqlConnection("Data Source=" + serv + ";Initial Catalog=" + db_destino + ";User ID=" + user + ";Password=" + pass + ""))
            {
                string Consulta_No_factura = "SELECT top 1 No_factura_Recibo as no_fact FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_creo='Migracion caso 4' ORDER BY No_factura_Recibo DESC";
                SqlCommand cmd = new SqlCommand(Consulta_No_factura, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            if (dt.Rows.Count > 0)
            {
                DataRow row;
                row = dt.Rows[0];
                result = int.Parse(row["no_fact"].ToString());
            }
            else
            {
                result = 0;
            }
            return result;
        }

        public DataTable Cargar_Recibos_Fantasma(string db_origen, string db_destino)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnn = new SqlConnection("Data Source=" + serv + ";Initial Catalog=" + db_destino + ";User ID=" + user + ";Password=" + pass + ""))
            {
                string ConsultaRecibos = "SELECT "
                + "    R.cuenta, R.foliorecib, P.Region_ID, P.Predio_ID, P.Usuario_ID, "
                + "    R.nummedidor, P.Tarifa_ID, R.lecanterior, R.lecactual, R.consumo, "
                + "    SUBSTRING(R.periodo,0,11) as fecha_inicio, "
                + "    SUBSTRING(periodo,12,10) as fecha_termino, "
                + "    convert(varchar (10),R.vencimient,103)AS vencimient, convert(varchar (10),R.facturacion,103)AS facturacion,  "
                + "    R.periodo, R.rpu, R.mesadeudo,"
                + "    CASE WHEN (MONTH(facturacion)-1) =0 THEN YEAR(facturacion)-1 "
                + "        ELSE YEAR(facturacion) "
                + "    END as anio,"
                + "    CASE WHEN (MONTH(facturacion)-1) =0 THEN 12 "
                + "        ELSE (MONTH(facturacion)-1)  "
                + "    END as bimestre "
                + "FROM " + db_origen + ".dbo.recibos R "
                + "    JOIN Cat_Cor_Predios P ON P.RPU = R.rpu "
                + "WHERE R.rpu IN "
                + "(SELECT DISTINCT RPU FROM Ope_Cor_Facturacion_Recibos "
                + "   WHERE Usuario_Creo = 'migracion caso 5' OR Usuario_Creo = 'migracion caso 6') "
                + "AND R.mesadeudo>2";
                //+ "AND R.rpu = '000860201810'";
                SqlCommand cmd = new SqlCommand(ConsultaRecibos, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }

    }
}
