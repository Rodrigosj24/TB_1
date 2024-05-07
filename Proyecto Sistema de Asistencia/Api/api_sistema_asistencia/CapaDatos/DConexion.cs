using Decoder_BL_SEG;
using Microsoft.Extensions.Configuration;
//using Oracle.ManagedDataAccess.Client;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DConexion
    {
        private readonly IConfiguration _configuration;
        private SqlConnection _cn;

        public DConexion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection Conectar_Cadena_Conexion_BD_Produccion()
        {
            try
            {
                _cn = new SqlConnection();

                string server = _configuration["CredentialBD:server_db_sql_desa"];
                string database = _configuration["CredentialBD:database_db_sql_desa"];
                string user = _configuration["CredentialBD:user_db_sql_desa"];
                string password = _configuration["CredentialBD:password_db_sql_desa"];

                string connectionString = _configuration.GetConnectionString("conexionBD_Sql");
                connectionString = string.Format(connectionString, server, database, user, password) + "TrustServerCertificate=true"; 

                _cn.ConnectionString = connectionString;
            }
            catch (Exception e)
            {
                throw new Exception("Error al conectar con la base de datos SQL Server.", e);
            }

            return _cn;
        }

        //public OracleConnection Conectar_Cadena_Conexion_BD_Produccion()
        //{
        //    try
        //    {
        //        _cn = new OracleConnection();
        //        string _server = Crypto.Decrypt(_configuration.GetSection("CredentialBD").GetSection("server_db_produccion_ora").Value);
        //        string _port = Crypto.Decrypt(_configuration.GetSection("CredentialBD").GetSection("port_db_produccion_ora").Value);
        //        string _database = Crypto.Decrypt(_configuration.GetSection("CredentialBD").GetSection("database_db_produccion_ora").Value);
        //        string _user = Crypto.Decrypt(_configuration.GetSection("CredentialBD").GetSection("user_db_produccion_ora").Value);
        //        string _password = Crypto.Decrypt(_configuration.GetSection("CredentialBD").GetSection("password_db_produccion_ora").Value);

               

        //        string _ruta_contenar = _configuration.GetConnectionString("conexionBD_Oracle").ToString();
        //        _cn.ConnectionString = string.Format(_ruta_contenar, _server, _port, _database, _user, _password);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    return _cn;
        //}


    }
}
