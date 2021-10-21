using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace TaskAssign.Extension
{
    public static class IdentityExtension
    {
        public static string GetName(this IIdentity identity)
        {
            if (identity == null)
                return null;

            var fullName = (identity as ClaimsIdentity).FirstOrNull("Name");

            return fullName;
        }


        internal static string FirstOrNull(this ClaimsIdentity identity, string claimType)
        {
            var val = identity.FindFirst(claimType);

            return val == null ? null : val.Value;
        }
    }
}

