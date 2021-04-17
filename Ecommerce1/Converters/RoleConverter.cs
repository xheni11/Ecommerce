using Ecommerce.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Converters
{
    internal class RoleConverter
    {
        internal RoleDTO Convert(IdentityRole role)
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

        internal IdentityRole Convert(RoleDTO role)
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

        internal void Convert(RoleDTO roleDTO, ref IdentityRole appRole)
        {
            appRole.Name = roleDTO.Name;
        }
    }

}
