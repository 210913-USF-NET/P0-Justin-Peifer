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
}