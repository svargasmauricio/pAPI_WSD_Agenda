﻿using pAPI_WSD_Agenda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace pAPI_WSD_Agenda.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        /// <summary>
        /// Success 200 - OK
        /// </summary>
        public CustomActionResult<T> GetResultOK<T>(T content) where T : pAPI_WSD_Agenda.ViewModels.BaseResponse
        {
            return new CustomActionResult<T>(Request, HttpStatusCode.OK, content);
        }

        /// <summary>
        /// Success 201 - Created
        /// </summary>
        public CustomActionResult<T> GetResultCreated<T>(T content) where T : BaseResponse
        {
            return new CustomActionResult<T>(Request, HttpStatusCode.Created, content);
        }

        /// <summary>
        /// Success 202 - Accepted
        /// </summary>
        public CustomActionResult<T> GetResultAccepted<T>(T content) where T : BaseResponse
        {
            return new CustomActionResult<T>(Request, HttpStatusCode.Accepted, content);
        }

        /// <summary>
        /// Erro 400 - BadRequest
        /// </summary>
        public CustomActionResult<T> GetResultBadRequest<T>(T content) where T : BaseResponse
        {
            return new CustomActionResult<T>(Request, HttpStatusCode.BadRequest, content);
        }

        /// <summary>
        /// Erro 404 - NotFound
        /// </summary>
        public CustomActionResult<T> GetResultNotFound<T>(T content) where T : BaseResponse
        {
            return new CustomActionResult<T>(Request, HttpStatusCode.NotFound, content);
        }

        /// <summary>
        /// Erro 401 - Unauthorized - Usuário não está logado
        /// </summary>
        public CustomActionResult<T> GetResultUnauthorized<T>(T content) where T : BaseResponse
        {
            return new CustomActionResult<T>(Request, HttpStatusCode.Unauthorized, content);
        }

        /// <summary>
        /// Erro 500 - InternalServerError - Erro interno no servidor (exception)
        /// </summary>
        public CustomActionResult<T> GetResultInternalServerError<T>(T content) where T : BaseResponse
        {
            return new CustomActionResult<T>(Request, HttpStatusCode.InternalServerError, content);
        }
    }
}