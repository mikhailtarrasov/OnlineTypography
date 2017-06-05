using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Vera.Domain.Entity.Identity
{
    public class Role : IdentityRole
    {
        public Role(string roleName) : base(roleName)
        {
            Name = roleName;
        }

        public Role()
        {
        }
    }
}