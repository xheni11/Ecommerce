using System;
namespace Ecommerce.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public RoleDTO Role { get; set; }
        public bool IsActive { get; set; }

    }
}
