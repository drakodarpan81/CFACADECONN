using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CFACADEFUN;
using CFACADESTRUC;
using Npgsql;

namespace CFACADECONN
{
    public class CConeccion
    {
        public static bool conexionPostgre(CEstructura ep, ref NpgsqlConnection conn, ref string sError)
        {
            bool bRegresa = true;
            ep.Pass = CFuncionesGral.GeneraPassWord(ep.Usuario, ep.BaseDeDatos);

            string connString = String.Format("Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                    ep.Ip,
                    ep.Usuario,
                    ep.BaseDeDatos,
                    ep.Puerto,
                    ep.Pass);
            try
            {
                conn = new NpgsqlConnection(connString);
                conn.Open();
            }
            catch(NpgsqlException ex)
            {
                sError = String.Format("No se Pudo Conectar a la Base de Datos...\n{0}" + ex.Message.ToString());
                bRegresa = false;
            }

            return bRegresa;
        }

        public static bool conexionSQL(CFACADESTRUC.CEstructura ep, ref OdbcConnection conn, ref string sError)
        {
            bool bRegresa = true;
            ep.Pass = CFACADEFUN.CFuncionesGral.GeneraPassWord(ep.Usuario, ep.BaseDeDatos);

            string sCadenaConexion = "Driver={SQL Server};database=" + ep.BaseDeDatos.Trim() + ";server=" + ep.Ip.Trim() + ";uid=" + ep.Usuario.Trim() + ";pwd=" + ep.Pass.Trim();
            try
            {
                conn.ConnectionString = sCadenaConexion;
                conn.ConnectionTimeout = 1000000000;
                conn.Open();
            }
            catch (OdbcException oex)
            {
                sError = String.Format("Error de Conexion...\n{0}", oex.Message.ToString());
                bRegresa = false;
            }
            catch (Exception ex)
            {
                sError = String.Format("No se Pudo Conectar a la Base de Datos...\n{0}", ex.Message.ToString());
                bRegresa = false;
            }

            return bRegresa;
        }
    }

}
