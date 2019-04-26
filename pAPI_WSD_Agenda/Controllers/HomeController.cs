using pAPI_WSD_Agenda.Filters;
using pAPI_WSD_Agenda.Models;
using pAPI_WSD_Agenda.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace pAPI_WSD_Agenda.Controllers
{
    [RoutePrefix("")]
    public class HomeController : BaseApiController
    {
        //NOTA: Essa controller é utilizado somente para retornar uma página padrão ao acessar a url da API

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Home()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("   <title>API</title>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("   <h1>API</h1>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            string html = sb.ToString();

            var response = new HttpResponseMessage();
            response.Content = new StringContent(html);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/html");
            return response;
        }

        //[Route("DadosFatura")]
        //[HttpGet]
        //[AuthorizationFilter]
        //public CustomActionResult<GetFatura> getDadosFatura()
        //{
        //    return GetResultOK(getFatura(""));
        //}

        //[Route("DadosFatura/{FATURA}")]
        //[HttpGet]
        //[AuthorizationFilter]
        //public CustomActionResult<GetFatura> getDadosFatura(String FATURA)
        //{
        //    return GetResultOK(getFatura(FATURA));
        //}

    }
}
