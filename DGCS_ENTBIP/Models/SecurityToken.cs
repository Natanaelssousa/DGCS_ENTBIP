using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class SecurityToken
    {
        public SecurityToken(string bdchaveToken)
        {
            chaveTokenOriginal = "N@tura#DGcs1";
            chaveToken = bdchaveToken;
        }
        public string chaveToken { get; set; }

        public string chaveTokenOriginal { get; set; }


        public bool validaChaveToken()
        {
            if (chaveToken == chaveTokenOriginal)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
    }
}