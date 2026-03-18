//=============================================
//Author:           Himani
//Created Date:	    17-March-2026
//Description:      Data base manager implement IDBManager interface
//Modified By:      
//Modified Date:    
//Modified Reason: 
//=============================================
using System.Data;
using tata_momerial.web.common;
namespace tata_momerial.web.DAL
{
    internal sealed class DBManager : IDBManager, IDisposable
    {
        private IDbConnection idbConnection;
        private string strConnection;
        public ApplicationConfiguration Config => AppServicesHelper.ApplicationConfiguration;


        /// <summary>
        /// Constructor 
        /// </summary>
        public DBManager()
        {
            this.ConnectionString = Config.connectionstring;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public DBManager(string connectionString)
        {
            this.strConnection = connectionString;
        }
        /// <summary>
        /// Connection
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                return idbConnection;
            }
        }
      
        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return strConnection;
            }
            set
            {
                strConnection = value;
            }
        }
      
        /// <summary>
        /// Open data base connection
        /// </summary>
        public void Open()
        {
            idbConnection =
            DbManagerFactory.GetConnection();
            idbConnection.ConnectionString = this.ConnectionString;
            if (idbConnection.State != ConnectionState.Open)
                idbConnection.Open();
        }
        /// <summary>
        /// Close data base connection
        /// </summary>
        public void Close()
        {
            if (idbConnection != null && idbConnection.State != ConnectionState.Closed)
                idbConnection.Close();
        }
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Close();
            this.idbConnection = null;
        }

       
    }
}
