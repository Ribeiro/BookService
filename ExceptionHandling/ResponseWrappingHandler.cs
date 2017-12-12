using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BookService.ExceptionHandling
{
    public class ResponseWrappingHandler : DelegatingHandler
    {
        private const string DOT_AND_WHITE_SPACE = ". ";
        private const string EMPTY_STRING = "";
        private const string MODEL_VALIDATION_ERROR_MESSAGE = "Não foi possível concluir sua solicitação!";
        private const string MESSAGE = "Message";
        private const string REQUEST_SUCCEEDED_MESSAGE = "Solicitação atendida com sucesso!";
        private const string UNAUTHORIZED_MESSAGE = "Desculpe! Você não está autenticado para acessar a url solicitada!";
        private const string FORBIDDEN_MESSAGE = "Desculpe! Você não possui permissão de acesso para a url solicitada!";
        private const string UNEXPECTED_ERROR_MESSAGE = "Desculpe! Um erro inesperado aconteceu!";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            return BuildApiResponse(request, response);
        }

        private HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            object content;
            var modelStateErrors = new List<string>();
            var message = REQUEST_SUCCEEDED_MESSAGE;

            if (response.TryGetContentValue(out content) || !response.IsSuccessStatusCode)
            {
                var error = content as HttpError;
                if (null != error)
                {
                    content = null;
                    if (null != error.ModelState)
                    {
                        message = MODEL_VALIDATION_ERROR_MESSAGE;
                        var httpErrorObject = response.Content.ReadAsStringAsync().Result;
                        var anonymousErrorObject =
                            new { message = EMPTY_STRING, ModelState = new Dictionary<string, string[]>() };
                        var deserializedErrorObject = JsonConvert.DeserializeAnonymousType(httpErrorObject,
                            anonymousErrorObject);
                        var modelStateValues = deserializedErrorObject.ModelState.Select(kvp => string.Join(DOT_AND_WHITE_SPACE, kvp.Value[0]));

                        for (var i = 0; i < modelStateValues.Count(); i++)
                        {
                            modelStateErrors.Add(modelStateValues.ElementAt(i));
                        }
                    }
                    else
                    {
                        message = BuildMessageAccording(response, error);
                        foreach (var err in error)
                        {
                            modelStateErrors.Add(err.Value.ToString());
                        }
                    }
                }
                else
                {
                    message = BuildMessageAccording(response);
                }
            }

            var newResponse = request.CreateResponse(response.StatusCode,
                new ResponsePackage(content, modelStateErrors, message));

            foreach (var header in response.Headers)
            {
                newResponse.Headers.Add(header.Key, header.Value);
            }

            return newResponse;
        }

        private string BuildMessageAccording(HttpResponseMessage response, HttpError error = null)
        {
            var result = EMPTY_STRING;
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                result = UNAUTHORIZED_MESSAGE;
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                result = FORBIDDEN_MESSAGE;
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                result = UNEXPECTED_ERROR_MESSAGE;
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                result = REQUEST_SUCCEEDED_MESSAGE;
            }
            else
            {
                if (null != error)
                {
                    result = error[MESSAGE].ToString();
                }
            }

            return result;
        }
    }
}