using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ecommerce.Common.Exception
{
    public class NotFoundException : SystemException
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, SystemException innerException) : base(message, innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NotFoundException(string name, object key)
            : base($"Entity '{name}' (Id: {key}) was not found.")
        {
        }
    }

}
