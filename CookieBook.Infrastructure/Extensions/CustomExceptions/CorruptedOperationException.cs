using System;

namespace CookieBook.Infrastructure.Extensions.CustomExceptions
{
    public class CorruptedOperationException : Exception
    {
        public CorruptedOperationException() { }
        public CorruptedOperationException(string message) : base(message) { }
        public CorruptedOperationException(string message, Exception inner):
            base(message, inner) { }
    }
}