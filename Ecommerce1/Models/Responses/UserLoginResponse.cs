using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.Responses
{
    public class UserLoginResponse
    {

        public string UserName { get; set; }
        public string  Token {get;set;}
    }
}
