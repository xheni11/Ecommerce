using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DTO.Mappings
{
    public class RoleMapping
    {
        public static RoleDTO ToDTO(IdentityRole role)
        {
            if (role == null)
            {
                return null;
            }

            return new RoleDTO
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static IdentityRole ToEntity(RoleDTO role)
        {
            if (role == null)
            {
                return null;
            }

            return new IdentityRole
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}

