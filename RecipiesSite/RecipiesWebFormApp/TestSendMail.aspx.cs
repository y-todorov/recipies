using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecipiesWebFormApp
{
    public partial class TestSendMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SendComplexMessage();
        }

        public static RestResponse SendComplexMessage()
        {
            Typesafe.Mailgun.MailgunClient mc = new Typesafe.Mailgun.MailgunClient("https://api.mailgun.net/v2",
                "key-7md8hh5f7cxi062n3x23x7h6nof5fue9");
            //MailMessage

            mc.SendMail(null);




            RestClient client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                               "key-7md8hh5f7cxi062n3x23x7h6nof5fue9");
            IRestRequest request = new RestRequest();
            request.AddParameter("domain",
                                 "app20716.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Excited User <ytodorov@ytodorov.com>");
            request.AddParameter("to", "ytodorov@ytodorov.com");
            request.AddParameter("cc", "ytodorov@ytodorov.com");
            request.AddParameter("bcc", "ytodorov@ytodorov.com");
            request.AddParameter("subject", "Hello");
            request.AddParameter("text", "Testing some Mailgun awesomness!");
            request.AddParameter("html", "<html>HTML version of the body</html>");
            //request.AddFile("attachment", Path.Combine("files", "test.jpg"));
            //request.AddFile("attachment", Path.Combine("files", "test.txt"));
            request.Method = Method.POST;
            RestResponse response = client.Execute(request) as RestResponse;
            return response;
        }
    }
}