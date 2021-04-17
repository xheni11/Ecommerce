using Ecommerce.DTO;
using Ecommerce.Models.Requests;
using Ecommerce.Models.Responses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Converters
{
    internal class UserConverter
    {
        internal UserResponse Convert(UserDTO user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
            };
        }

        internal UserDTO Convert(CreateUserRequest user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }

       

    }
}
