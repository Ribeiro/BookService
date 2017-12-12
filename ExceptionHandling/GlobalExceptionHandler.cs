using BookService.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace BookService.ExceptionHandling
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public IExceptionHandler InnerHandler { get; }

        public GlobalExceptionHandler(IExceptionHandler innerHandler)
        {
            if (innerHandler == null)
            {
                throw new ArgumentNullException(nameof(innerHandler));
            }

            InnerHandler = innerHandler;
        }

        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            Handle(context);
            return Task.FromResult<object>(null);
        }

        private void Handle(ExceptionHandlerContext context)
        {
            var exception = context.Exception;
            var httpException = exception as HttpException;
            if (null != httpException)
            {
                context.Result = new SimpleErrorResult(context.Request,
                    (HttpStatusCode)httpException.GetHttpCode(), httpException.Message);
                return;
            }
            if (exception is ResourceNotFoundException)
            {
                context.Result = new SimpleErrorResult(context.Request, HttpStatusCode.NotFound, exception.Message);
                return;
            }

            //if (exception is BusinessRuleViolationException)
            //{
            //    context.Result = new SimpleErrorResult(context.Request, HttpStatusCode.BadRequest,
            //        exception.Message);
            //    return;
            //}

            //if (exception is RequestConflictException)
            //{
            //    context.Result = new SimpleErrorResult(context.Request, HttpStatusCode.Conflict,
            //        exception.Message);
            //    return;
            //}

            //if (exception is UnprocessableEntityException)
            //{
            //    context.Result = new SimpleErrorResult(context.Request, (HttpStatusCode)422,
            //        exception.Message);
            //    return;
            //}

            //if (exception is MultipleOptionsException)
            //{
            //    context.Result = new JsonOptionsErrorResult(context.Request, HttpStatusCode.NotAcceptable,
            //        exception.Message, new JsonOptionsException(((MultipleOptionsException)exception).Results));
            //    return;
            //}

            //if (exception is SecurityTokenValidationException)
            //{
            //    context.Result = new SimpleErrorResult(context.Request, HttpStatusCode.Unauthorized,
            //        exception.Message);
            //    return;
            //}

            //if (exception is MissingPermissionException)
            //{
            //    context.Result = new SimpleErrorResult(context.Request, HttpStatusCode.Forbidden,
            //       exception.Message);
            //    return;
            //}

            //if (exception is PermissionDeniedException)
            //{
            //    context.Result = new SimpleErrorResult(context.Request, HttpStatusCode.Forbidden,
            //      exception.Message);
            //    return;
            //}

            context.Result = new SimpleErrorResult(context.Request, HttpStatusCode.InternalServerError,
                exception.Message);
        }
    }
}