using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tata_momerial.web.DAL
{
    public sealed class DALFactory
    {
        private static DALFactory instance = null;

        private DALFactory() { }

        /// <summary>
        /// DAL Factory object
        /// </summary>
        public static DALFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new DALFactory();
                return instance;
            }
        }
    }
}
