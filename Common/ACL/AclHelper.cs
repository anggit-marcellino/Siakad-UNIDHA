using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Common.ACL
{
    public static class AclHelper
    {
        public static string GetClaim(this IEnumerable<Claim> claims, string claimKey)
        {
            return claims.FirstOrDefault(x => x.Type == claimKey)?.Value;
        }
    }
}
