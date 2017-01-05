using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace Migracion_Facturacion
{
    class Cls_Conexion
    {
        public static string Str_Conexion_Origen = ConfigurationManager.ConnectionStrings["db_origen"].ConnectionString;
        public static string Str_Conexion_Destino = ConfigurationManager.ConnectionStrings["db_destino"].ConnectionString;

        //  Credito y cobranza
        public static string Str_Conexion_CreditoC_Contel = ConfigurationManager.ConnectionStrings["Credito_Cobranza_BDContel"].ConnectionString;
        public static string Str_Conexion_CreditoC_Simapag = ConfigurationManager.ConnectionStrings["Credito_Cobranza_BDSimapag"].ConnectionString;
        public static string Str_Conexion_CreditoC_Ajuste_Saldos = ConfigurationManager.ConnectionStrings["Credito_Cobranza_Ajuste_Saldos"].ConnectionString;
        public static string Str_Conexion_CreditoC_Ajuste_Final = ConfigurationManager.ConnectionStrings["Credito_Cobranza_Ajuste_Final"].ConnectionString;

        
    }
}
