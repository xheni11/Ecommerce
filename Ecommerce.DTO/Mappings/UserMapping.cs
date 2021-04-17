using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DTO.Mappings
{
    public class UserMapping
    {
        public static UserDTO ToDTO(IdentityUser user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Password = null,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.LockoutEnd == null || user.LockoutEnd.Value <= DateTime.Now
            };
        }

        public static IdentityUser ToEntity(UserDTO user)
        {
            if (user == null)
            {
                return null;
            }

            return new IdentityUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }

        public static IdentityUser ToEntityCreate(UserDTO user)
        {
            if (user == null)
            {
                return null;
            }

            return new IdentityUser
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }

    }
}
