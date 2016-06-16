using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MVCWebUI.Helpers
{
    public class MailContent
    {
        public static string CustomerActivate(Customer customer) {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");

            sb.Append("<head>");
            sb.Append("<title>ÜyelikAktivasyonu</title>");
            sb.Append("</head>");

            sb.Append("<body>");
            sb.Append("<div> Sayin"+customer.Customer_Name+"</div>");
            sb.Append("</body>");



            sb.Append("</html>");
            return sb.ToString();
        }



    }
}