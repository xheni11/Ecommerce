using System;
using System.Globalization;

namespace Ecommerce.Common.Exception
{


    public class EcommerceException : SystemException
    {
        public EcommerceException(string message) : base(message)
        {
        }

        public EcommerceException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

}
