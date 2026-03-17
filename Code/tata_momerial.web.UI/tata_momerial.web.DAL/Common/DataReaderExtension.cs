using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tata_momerial.web.Common
{
    public static class DataReaderExtensions
    {
        public static bool GetBoolean(this System.Data.IDataReader reader, string fieldName)
        {
            return reader.IsDBNull(reader.GetOrdinal(fieldName)) ? false : Convert.ToBoolean(reader[reader.GetOrdinal(fieldName)]);
        }
        public static DateTime? GetDateTime(this System.Data.IDataReader reader, string fieldName)
        {
            return reader.IsDBNull(reader.GetOrdinal(fieldName)) ? null : Convert.ToDateTime(reader[reader.GetOrdinal(fieldName)]);
        }
        public static long? GetLong(this System.Data.IDataReader reader, string fieldName)
        {
            return reader.IsDBNull(reader.GetOrdinal(fieldName)) ? null : Convert.ToInt64(reader[reader.GetOrdinal(fieldName)]);
        }
        public static decimal? GetDecimal(this System.Data.IDataReader reader, string fieldName)
        {
            return reader.IsDBNull(reader.GetOrdinal(fieldName)) ? null : Convert.ToDecimal(reader[reader.GetOrdinal(fieldName)]);
        }
        public static double? GetDouble(this System.Data.IDataReader reader, string fieldName)
        {
            return reader.IsDBNull(reader.GetOrdinal(fieldName)) ? null : Convert.ToDouble(reader[reader.GetOrdinal(fieldName)]);
        }
        public static int? GetInt(this System.Data.IDataReader reader, string fieldName)
        {
            return reader.IsDBNull(reader.GetOrdinal(fieldName)) ? null : Convert.ToInt32(reader[reader.GetOrdinal(fieldName)]);
        }
        public static Int16? GetInt16(this System.Data.IDataReader reader, string fieldName)
        {
            return reader.IsDBNull(reader.GetOrdinal(fieldName)) ? null : Convert.ToInt16(reader[reader.GetOrdinal(fieldName)]);
        }
        public static float? GetFloat(this System.Data.IDataReader reader, string fieldName)
        {
            return reader.IsDBNull(reader.GetOrdinal(fieldName)) ? null : (float)Convert.ToDouble(reader[reader.GetOrdinal(fieldName)]);
        }
        public static byte[]? GetByte(this System.Data.IDataReader reader, string fieldName)
        {
            return reader.IsDBNull(reader.GetOrdinal(fieldName)) ? null : (byte[])(reader[reader.GetOrdinal(fieldName)]);
        }
        public static string? GetString(this System.Data.IDataReader reader, string fieldName)
        {
            return reader.IsDBNull(reader.GetOrdinal(fieldName)) ? null : Convert.ToString(reader[reader.GetOrdinal(fieldName)])?.Trim();
        }
    }
}
