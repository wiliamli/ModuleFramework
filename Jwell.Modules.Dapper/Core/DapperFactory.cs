using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;

namespace Jwell.Modules.Dapper.Core
{
    public class DapperFactory
    {
        private static readonly string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString;


        public static OracleConnection CreateOracleConnection()
        {
            var connection = new OracleConnection(ConnectionString);
            return connection;
        }
    }
}
