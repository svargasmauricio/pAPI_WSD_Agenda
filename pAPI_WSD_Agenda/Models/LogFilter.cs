using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;

namespace pAPI_WSD_Agenda.Models
{
    
    public class LogFilter : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Method == HttpMethod.Post)
            {
                var postData = actionContext.ActionArguments;

                var jsonSettings = new Newtonsoft.Json.JsonSerializerSettings();
                jsonSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                jsonSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(postData.Values, jsonSettings);

                try
                {
                    string path = HttpContext.Current.Server.MapPath("~/Log-api.txt");
                    var line = Environment.NewLine + Environment.NewLine; 

                    using (StreamWriter sw = File.AppendText(path))
                    {
                        
                        sw.WriteLine("-----------LOG API " + " " + DateTime.Now.ToString() + "-----------------");
                        sw.WriteLine("-------------------------------------------------------------------------------------");
                        sw.WriteLine(line);
                        sw.WriteLine(json);
                        sw.WriteLine("--------------------------------*End*------------------------------------------");
                        sw.WriteLine(line);
                        sw.Flush();
                        sw.Close();

                    } 
                }
                catch { }

                //do logging here
            }
        }
    }
    
}