using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tata_momerial.web.Common.Cookie
{
    public static class ExceptionHandler
    {
        public static T SwallowOnException<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch
            {
                return default(T);
            }
        }
    }
}
