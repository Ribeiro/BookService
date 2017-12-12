using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookService.Exceptions
{
    [Serializable]
    public class ResourceNotFoundException : Exception
    {
        private const string recordNotFoundMessage = "Registro não encontrado!";
        public ResourceNotFoundException(string message) : base(message)
        {
        }

        public ResourceNotFoundException() : base(recordNotFoundMessage)
        {
        }
    }
}