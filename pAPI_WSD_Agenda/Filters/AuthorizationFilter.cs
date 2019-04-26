using pAPI_WSD_Agenda.Models;
using pAPI_WSD_Agenda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace pAPI_WSD_Agenda.Filters
{
    public class AuthorizationFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var httpContext = new HttpContextWrapper(System.Web.HttpContext.Current);

            //try
            //{
            //    string [] DADOS_ACESSO = GetToken(httpContext).Split('-');

            //    Usuario user = new Usuario();
            //    user.NOLOGIN = DADOS_ACESSO[0];
            //    user.SENHA = DADOS_ACESSO[1];

            //    user = AuthSession.Login_SetToken(user.NOLOGIN, user.SENHA);

            //    if (user == null)
            //    {
            //        //Erro de autenticação
            //        UtilsApi.WriteError(string.Format("Acces to URL: {0} Unauthorized", httpContext.Request.Url.ToString()));
            //        base.OnAuthorization(actionContext);
            //        return;
            //    }

            //    //ok
            //    SetAuthSessionInContext(httpContext, user);
            //}
            //catch (System.Exception ex)
            //{
            //    UtilsApi.WriteError(string.Format("Error in AuthorizationFilter URL: {0} -- {1}", httpContext.Request.Url.ToString(), ex.Message), ex);
            //    base.OnAuthorization(actionContext);
            //}
        }

        private string GetToken(System.Web.HttpContextBase httpContext)
        {
            string token = (httpContext.Request.Headers["Authorization"] != null ? httpContext.Request.Headers["Authorization"].ToString() : null);
            if (token != null)
                token = token.Replace("Token", "").Trim();

            if (token == null)
                token = httpContext.Request.QueryString["authorization"];

            return token;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var objResponse = new BaseResponse();
            objResponse.AddMessage("401", "Unauthorized");

            var jsonSettings = new Newtonsoft.Json.JsonSerializerSettings();
            jsonSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            jsonSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(objResponse, jsonSettings);

            var response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
            response.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            actionContext.Response = response;
        }

        private void SetAuthSessionInContext(System.Web.HttpContextBase httpContext, Usuario authSession)
        {
            httpContext.Items[UtilsApi.HTTP_CONTEXT_ITEM_AUTH_SESSION] = authSession;
        }

        //TODO: Obter os dados do usuário usando o token
        private AuthSession GetAuthSession(string token)
        {
            //if (string.IsNullOrEmpty(token))
                return null;

            //Usuario user = AuthSession.getUserFromToken(token);
            //if (user == null)
            //{
            //    //Token inválido
            //    return null;
            //}
            //else
            //{
            //    return new AuthSession() { Name = user.NOLOGIN, Email = user.NOEMAIL };
            //}
             
        }
    }
}