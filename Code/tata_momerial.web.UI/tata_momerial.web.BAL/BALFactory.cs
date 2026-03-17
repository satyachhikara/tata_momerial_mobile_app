using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tata_momerial.web.BAL
{
    public sealed class BALFactory
    {
        private static BALFactory instance = null;


        private BALFactory() { }

        /// <summary>
        /// BAL Factory object
        /// </summary>
        public static BALFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new BALFactory();
                return instance;
            }
        }
    }
}
