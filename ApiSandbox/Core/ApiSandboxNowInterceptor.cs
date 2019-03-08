using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiSandboxAPI.Core
{
    public class ApiSandboxInterceptor : ActionFilterAttribute
    {
        private readonly ApiSandboxLogger _logger;

        public ApiSandboxInterceptor(ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger("ApiSandboxInterceptor");
            _logger = new ApiSandboxLogger(logger);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var pathTitle = $"{context.HttpContext.Request.Path} - {context.HttpContext.Request.Method}";
            var objs = context.ActionArguments.Select(a => (object)a.Value);
            _logger.LogInfoObject(pathTitle, objs.ToList());
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            //ApiSandboxAPIResponse result;

            //context.HttpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            //var respBody = new StreamReader(context.HttpContext.Response.Body).ReadToEnd();
            //var respObj = JsonConvert.DeserializeObject(respBody);

            //switch (context.HttpContext.Response.StatusCode)
            //{
            //    case (int)HttpStatusCode.OK:
            //        result = ApiSandboxAPIResponse.Create(HttpStatusCode.OK, respObj, string.Empty);
            //        break;
            //    case (int)HttpStatusCode.Forbidden:
            //        var forbiddenError = "You are not authorized to request this content.";
            //        result = ApiSandboxAPIResponse.Create(HttpStatusCode.Forbidden, null, forbiddenError);
            //        break;
            //    default:
            //        var standardError = "An error occurred and has been logged.";
            //        result = ApiSandboxAPIResponse.Create(HttpStatusCode.InternalServerError, null, standardError);
            //        break;
            //}

            //byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result));
            //context.HttpContext.Response.Body.Write(data, 0, data.Length);

            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.HttpContext.Response.StatusCode != 200)
            {
                var pathTitle = $"{context.HttpContext.Request.Path} - {context.HttpContext.Request.Method}";
                var apisLog = new ApiSandboxLog()
                {
                    title = pathTitle,
                    messages = null
                };
                Exception ex = null;

                if (context.Result is ObjectResult)
                {
                    var objResult = context.Result as ObjectResult;

                    if (objResult.Value is Exception)
                    {
                        ex = objResult.Value as Exception;
                    }
                }

                _logger.LogMessage(LogType.error, apisLog, ex);
            }

            base.OnResultExecuted(context);
        }

    }
}
