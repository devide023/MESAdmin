using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
namespace ZDToolHelper
{
    public class TokenHelper
    {
        public static string GetToken
        {
            get
            {
                string token = string.Empty;
                var headers = HttpContext.Current.Request.Headers;
                var tokens = headers.GetValues("Authorization");
                if (tokens != null)
                {
                    if (tokens.Length > 0)
                    {
                        token = tokens[0].Replace("Bearer ", "");
                    }
                }
                return token;
            }
        }
    }
}
