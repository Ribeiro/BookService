using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BookService.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        private const string httpPutMethod = "PUT";
        private const string nullArgumentMessage = "O argumento não pode ser nulo!";
        private const string missingIdValue = "Não foi informado o Id do registro para atualização!";
        private const string id = "Id";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string methodType = actionContext.Request.Method.Method;
            if (actionContext.ActionArguments.Any(kv => kv.Value == null))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, nullArgumentMessage);

            }else if(httpPutMethod.Equals(methodType.ToUpper()))
            {
                if (actionContext.ActionArguments.Select(x => x.Key == id) == null)
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, missingIdValue);
                }
               
            }
            else if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse((HttpStatusCode)422, actionContext.ModelState);

            }
        }

    }
}