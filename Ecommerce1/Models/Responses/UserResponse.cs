using Ecommerce.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public RoleDTO Role { get; set; }
        public bool IsActive { get; set; }
    }
}
