using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class EmailHelper
    {
        public static RestResponse SendComplexMessage(byte[] files)
        {
            //Typesafe.Mailgun.MailgunClient mc = new Typesafe.Mailgun.MailgunClient("https://api.mailgun.net/v2",
            //    "key-7md8hh5f7cxi062n3x23x7h6nof5fue9");
            //MailMessage

            //mc.SendMail(null);

            RestClient client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator = new HttpBasicAuthenticator("api", "key-7md8hh5f7cxi062n3x23x7h6nof5fue9");

            IRestRequest request = new RestRequest();
            request.AddParameter("domain", "app20716.mailgun.org", ParameterType.UrlSegment);

            request.Resource = "{domain}/messages";

            request.AddParameter("from", "BLUE BAR <ytodorov@ytodorov.com>");
            request.AddParameter("to", "ytodorov@ytodorov.com");
            //request.AddParameter("cc", "ytodorov@ytodorov.com");
            //request.AddParameter("bcc", "ytodorov@ytodorov.com");
            request.AddParameter("subject", "Purchase Order");
            request.AddParameter("text", "Testing some Mailgun awesomness!");
            request.AddParameter("html", "<html>Hello, this is a test email for a purchase order. This text will be replaced with something more appropriate.</html>");


            request.AddFile("attachment",files, "PurchaseOrder.pdf");
            //request.AddFile("attachment", Path.Combine("files", "test.jpg"));
            //request.AddFile("attachment", Path.Combine("files", "test.txt"));
            request.Method = Method.POST;
            RestResponse response = client.Execute(request) as RestResponse;
            
            return response;
        }
    }
}
