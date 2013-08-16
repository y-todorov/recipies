using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using RecipiesWebFormApp;
using System.Timers;
using System.Net;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using System.Text;

namespace RecipiesWebFormApp
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            //BackDatabases();


            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Yordan, test that site is not asleep after 20 mins. of inactivity
            // By the way this works very well :)
            Timer timer = new Timer(TimeSpan.FromMinutes(10).TotalMilliseconds); // 10 minutes
            timer.Elapsed += timer_Elapsed;
            timer.Start();


        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            WebClient client = new WebClient();
            string res = client.DownloadStringTaskAsync(new Uri("http://recipies.apphb.com/")).Result;
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs      
        }

        private void BackDatabases()
        {
            // test adventure works
            Backup backup = new Backup();
            string serverName = "2a3247ec-fc2f-4ba5-8560-a21c00fb62d5.sqlserver.sequelizer.com";
            string userName = "bmrztolvtyhlfhpr";
            string password = "VgTTLZHir8tJLobcykbRBtCaQqoupBRkDjv8o38k3GMVoZoG6pbar3nBLTcvKo2W";
            SqlConnectionInfo sci = new SqlConnectionInfo(serverName, userName, password);
            ServerConnection sc = new ServerConnection(sci);
            Microsoft.SqlServer.Management.Smo.Server svr = new Microsoft.SqlServer.Management.Smo.Server(sc);

            try
            {
                //backup.Database = "db2a3247ecfc2f4ba58560a21c00fb62d5";

                ////BackupDevice bs = new BackupDevice();
                ////bs.BackupDeviceType = BackupDeviceType.Disk;



                //backup.Devices.AddDevice("test", DeviceType.File);

                ////backup.Devices.AddDevice("C:\\test.bak", DeviceType.File);

                //backup.Complete += backup_Complete;


                //backup.SqlBackup(svr);


                //svr.Databases["db2a3247ecfc2f4ba58560a21c00fb62d5"].Drop();

                //Restore r = new Restore();

                //r.Database = "db804b3a50697f4e00b7cca1e6015cf402";
                //r.NoRecovery = true;
                //r.Devices.AddDevice("test", DeviceType.File);

                //r.SqlRestore(svr);



                //var scipt = r.Script(svr);

                ScriptingOptions options = new ScriptingOptions();
                //options.ScriptData = true;
                options.ScriptDrops = false;
                options.EnforceScriptingOptions = true;
                options.ScriptSchema = true;
                options.IncludeHeaders = true;         
                options.Indexes = true;
                options.DriAll = true;
                


                //
                Scripter scr = new Scripter(svr);
                scr.Options = options;
                SqlSmoObject[] objects = new SqlSmoObject[1];
                objects[0] = svr.Databases[0];

                var testtest = scr.Script(objects);

                //

                StringBuilder sbMain = new StringBuilder();

                foreach (Table table in svr.Databases[0].Tables)
                {
                    IEnumerable<string> strColl = table.EnumScript(options);
                    StringBuilder sb = new StringBuilder();
                    foreach (string s in strColl)
                    {
                        sb.AppendLine(s);
                    }
                    sbMain.AppendLine(sb.ToString());
                }

                string res = sbMain.ToString();








            }
            catch (Exception ex)
            {
                string mes = ex.Message;
            }



        }

        void backup_Complete(object sender, ServerMessageEventArgs e)
        {

        }

    }
}
