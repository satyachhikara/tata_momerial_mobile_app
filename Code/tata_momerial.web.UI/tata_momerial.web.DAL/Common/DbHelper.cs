//=============================================
//Author:           Himani
//Created Date:	    17-March-2026
//Description:      Data base helper class
//Modified By:      
//Modified Date:    
//Modified Reason: 
//=============================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace tata_momerial.web.DAL
{
    internal class DbHelper
    {         
        /// <summary>
        /// Static methods only. Hence, making constructor as private.
        /// </summary>
        private DbHelper()
        {
        }

        /// <summary>
        /// Get truncated value from left
        /// </summary>
        /// <param name="stringName"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        internal static string TuncateStringRight(string stringName, int length)
        {
            string retrunString = stringName;
            if (!string.IsNullOrWhiteSpace(stringName))
            {
                if (stringName.Length > length)
                    retrunString = stringName.Substring(stringName.Length - 100);
            }
            return retrunString;
        }
        internal static string TruncateValue(object obj, int length)
        {
            if (obj == DBNull.Value || obj == null)
                return string.Empty;
            else
            {
                string value = (string)obj;
                if (string.IsNullOrWhiteSpace(value))
                    value = value.Trim();
                if (value.Length > length)
                    value = value.Substring(0, length-1);
                return value;
            }
        }      
     
        /// <summary>
        /// Replace string space with hyphen
        /// </summary>
        /// <param name="stringName"></param>
        /// <returns></returns>
        internal static string ReplaceStringSpaceWithHyphen(string stringName)
        {
            string retrunString = string.Empty;
            if (!string.IsNullOrWhiteSpace(stringName))
                retrunString = Regex.Replace(stringName.Trim(), @"\s+", "-").ToLower();

            return retrunString;
        }

        /// <summary>
        /// Replace all special characters with hyphen
        /// </summary>
        /// <param name="stringName"></param>
        /// <returns></returns>
        internal static string GetUniqueName(string strName)
        {
            if (!string.IsNullOrWhiteSpace(strName))
            {
                strName = Regex.Replace(strName.Trim(), " ", "-").ToLower();
                strName = Regex.Replace(strName.Trim(), @"\$", "-");
                strName = Regex.Replace(strName.Trim(), "~", "-");
                strName = Regex.Replace(strName.Trim(), "`", "-");
                strName = Regex.Replace(strName.Trim(), "@", "-");
                strName = Regex.Replace(strName.Trim(), "#", "-");
                strName = Regex.Replace(strName.Trim(), "%", "-");
                strName = Regex.Replace(strName.Trim(), @"\^", "-");
                strName = Regex.Replace(strName.Trim(), "&", "-");
                strName = Regex.Replace(strName.Trim(), @"\*", "-");
                strName = Regex.Replace(strName.Trim(), @"\(", "-");
                strName = Regex.Replace(strName.Trim(), @"\)", "-");
                strName = Regex.Replace(strName.Trim(), "_", "-");
                strName = Regex.Replace(strName.Trim(), "=", "-");
                strName = Regex.Replace(strName.Trim(), @"\+", "-");
                strName = Regex.Replace(strName.Trim(), @"\|", "-");
                strName = Regex.Replace(strName.Trim(), "\"", "-");
                strName = Regex.Replace(strName.Trim(), "/", "-");
                strName = Regex.Replace(strName.Trim(), @"\?", "-");
                strName = Regex.Replace(strName.Trim(), ">", "-");
                strName = Regex.Replace(strName.Trim(), "<", "-");
                strName = Regex.Replace(strName.Trim(), "{", "-");
                strName = Regex.Replace(strName.Trim(), "}", "-");
                strName = Regex.Replace(strName.Trim(), @"\[", "-");
                strName = Regex.Replace(strName.Trim(), @"\]", "-");
                strName = Regex.Replace(strName.Trim(), "-", "-");
            }
            return strName;
        }
       
    }
}
