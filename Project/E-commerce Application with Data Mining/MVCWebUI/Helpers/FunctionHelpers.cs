using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;

namespace MVCWebUI.Helpers
{
    public class FunctionHelpers
    {
        public static string UserSession { get { return "UserSession"; } }

        public static void SessionControl()
        {
            //bool result = false;

            if (HttpContext.Current.Session[UserSession] == null)
                HttpContext.Current.Response.Redirect("~/Dashboard/Login");

            //result = true;
            //return result;
        }


        public static bool MailSend(string title, string detail, List<string> mailto)
        {
            bool result = false;
            MailMessage email = new MailMessage();
            email.From = new MailAddress("cengfulya@gmail.com", "fulya çetin");
            email.ReplyTo = new MailAddress("bilgi", "volksn");

            foreach (string item in mailto)
            {
                email.To.Add(item);
            }
            email.Subject = title;
            email.Body = detail;
            email.IsBodyHtml = true;

            try
            {
                SendAsyncEmail(email);
                result = true;
            }
            catch (Exception)
            {
                result = false;
                throw;
            }
            return result;
        }

        public static void SendAsyncEmail(MailMessage message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("xxx", "xx")
            };
            client.SendCompleted += (sender, error) =>
            {
                if (error.Error != null)
                {
                    throw new WarningException("Eposta gönderildiğinde bir hata oluştu");
                }
                client.Dispose();
                message.Dispose();
            };
            ThreadPool.QueueUserWorkItem(o => client.SendAsync(message, Tuple.Create(client, message)));
        }

        public static string createActivationCode(int passwordLength)
        {
            string allowedChars = "abcdefghijklmnprstuvyzABCDEFGHIJKLMNPRSTUVYZ";
            char[] chars = new char[passwordLength];
            Random rnd = new Random();
            for (int i = 0; i < passwordLength; i++)
            {
                chars[i]= allowedChars[rnd.Next(0,allowedChars.Length)];
            }

            return new string(chars);
        }



    }
}