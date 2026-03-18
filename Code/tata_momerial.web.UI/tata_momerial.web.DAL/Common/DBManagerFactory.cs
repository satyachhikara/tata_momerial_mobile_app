//=============================================
//Author:           Himani
//Created Date:	    17-March-2026
//Description:      Data base manager factory for SQL data base
//Modified By:      
//Modified Date:    
//Modified Reason: 
//=============================================
using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace tata_momerial.web.DAL
{
    internal sealed class DbManagerFactory
    {
        private DbManagerFactory()
        {
        }
        /// <summary>
        /// Get connection
        /// </summary>
        /// <returns>SQL Connection</returns>
        public static IDbConnection GetConnection()
        {
            IDbConnection iDbConnection = null;
            iDbConnection = new SqlConnection();
            return iDbConnection;
        }        
    }
}
