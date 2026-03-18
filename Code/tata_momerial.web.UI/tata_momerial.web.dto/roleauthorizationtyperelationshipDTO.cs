using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tata_momerial.web.dto
{
    public class roleauthorizationtyperelationshipDTO : baseDTO
    {
        public long? roleauthorizationtyperelationshipid {  get; set; } 
        public long? roleid { get; set; }
        public int? isview { get; set; }
        public int? isinsert { get; set; }
        public int? isupdate { get; set; }
        public int? isdelete { get; set; }
        public int? authorizationtypeid { get; set; }
        public string? authorizationdescription { get; set; }
        public string? rolename { get; set; }
    }
}
