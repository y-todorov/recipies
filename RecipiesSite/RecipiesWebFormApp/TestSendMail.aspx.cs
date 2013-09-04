using PubNubMessaging.Core;
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

        public static void SendComplexMessage()
        {
            Pubnub pubnub = new Pubnub("pub-c-cc6cdb68-ab44-4f1a-8553-ccc30d96f87a",
                "sub-c-bde0a3b8-1538-11e3-bc51-02ee2ddab7fe",
                "sec-c-NzUzZTU0MzktNDlhOC00YWVlLThmZTYtNzFhMjg4NDI2N2Vi");
            pubnub.Publish("Products", "Inserted", (o) => Test(o), (o) => Test(o));

            pubnub.Subscribe("Products", (t) => Test(t), null, null);
        }

        public static void Test(object o)
        {

        }
    }
}