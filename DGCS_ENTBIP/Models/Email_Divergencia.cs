using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class Email_Divergencia
    {
        public string id_emails { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string servidor_email { get; set; }
        public string host_envio { get; set; }

    }
}