using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tata_momerial.web.dto
{
    public class baseDTO
    {
        public bool showsuccessmessage { get; set; }
        public bool showvalidationmessage { get; set; }
        public string errormessage { get; set; } = string.Empty;

        public const String DATE_FORMAT_DD_MMM_YYYY = "dd MMM, yyyy";
        public const String DATE_TIME_FORMAT_DD_MMM_YYYY_HH_MM = "dd MMM, yyyy HH:MM tt";
        public const String DATE_FORMAT_HH_MM_TT = "hh:mm tt";
        public DateTime? createddate { get; set; }
        public DateTime? updateddate { get; set; }
        public long? createdby { get; set; }
        public long? updatedby { get; set; }
        public int? status { get; set; }

        public bool done { get; set; }

        public string displaycreateddate
        {
            get
            {
                return createddate != null && createddate.HasValue ? createddate.Value.ToString(DATE_FORMAT_DD_MMM_YYYY) : string.Empty;
            }
        }
        public string displaystatus
        {
            get
            {
                return status.HasValue && status.Value > 0 ? "Active" : "In-Active";
            }
        }
      

    }
}
