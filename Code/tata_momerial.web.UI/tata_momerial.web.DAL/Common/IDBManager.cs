//=============================================
//Author:           Himani
//Created Date:	    17-March-2026
//Description:      Data base manager interface
//Modified By:      
//Modified Date:    
//Modified Reason: 
//=============================================
using System;
using System.Data;
using System.Data.SqlClient;

namespace tata_momerial.web.DAL
{
    public interface IDBManager
    {
        /// <summary>
        /// Connection String
        /// </summary>
        string ConnectionString
        {
            get;
            set;
        }
        /// <summary>
        /// Connection
        /// </summary>
        IDbConnection Connection
        {
            get;
        }

        /// <summary>
        /// Open Connection
        /// </summary>
        void Open();
        /// <summary>
        /// Close connection
        /// </summary>
        void Close();
        /// <summary>
        /// Dispose
        /// </summary>
        void Dispose();

       
    }
}

