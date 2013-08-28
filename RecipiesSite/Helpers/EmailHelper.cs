﻿using RestSharp;
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
        public static RestResponse SendComplexMessage(string from, string to, string bcc, string subject, string textBody, string htmlBody, byte[] attachmentBytes, string attachmentNameWithExtension)
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

            request.AddParameter("from", from);
            request.AddParameter("to", to);
            //request.AddParameter("cc", "ytodorov@ytodorov.com");
            request.AddParameter("bcc", bcc);
            request.AddParameter("subject", subject);
            request.AddParameter("text", textBody);
            request.AddParameter("html", htmlBody);



            request.AddFile("attachment", attachmentBytes, attachmentNameWithExtension);
            //request.AddFile("attachment", Path.Combine("files", "test.jpg"));
            //request.AddFile("attachment", Path.Combine("files", "test.txt"));
            request.Method = Method.POST;
            RestResponse response = client.Execute(request) as RestResponse;
            
            return response;
        }
    }
}