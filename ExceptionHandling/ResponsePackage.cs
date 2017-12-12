using System.Collections.Generic;

namespace BookService.ExceptionHandling
{
    internal class ResponsePackage
    {
        public ResponsePackage(object result, List<string> errors, string message)
        {
            Errors = errors;
            Result = result;
            Message = message;
        }

        public List<string> Errors { get; set; }
        public object Result { get; set; }
        public string Message { get; set; }
    }
}