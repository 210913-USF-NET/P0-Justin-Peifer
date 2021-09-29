using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Models
{
    [System.Serializable]
    public class InputInvalidException : System.Exception
    {
        public InputInvalidException() { }
        public InputInvalidException(string message) : base(message) { }
        public InputInvalidException(string message, System.Exception inner) : base(message, inner) { }
        protected InputInvalidException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class InvalidUserNameException : System.Exception
    {
        public InvalidUserNameException() { }
        public InvalidUserNameException(string message) : base(message) { }
        public InvalidUserNameException(string message, System.Exception inner) : base(message, inner) { }
        protected InvalidUserNameException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public class EmailVerificationException : System.Exception
    {
        public EmailVerificationException() { }
        public EmailVerificationException(string message) : base(message) { }
        public EmailVerificationException(string message, System.Exception inner) : base(message, inner) { }
        protected EmailVerificationException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public class NegativeInventoryException : System.Exception
    {
        public NegativeInventoryException() { }
        public NegativeInventoryException(string message) : base(message) { }
        public NegativeInventoryException(string message, System.Exception inner) : base(message, inner) { }
        protected NegativeInventoryException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}