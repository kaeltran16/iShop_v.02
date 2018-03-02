using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace iShop.Repo.Extensions
{
    public static class IdentityResultExtension
    {
        public static string GetErrorDescription(this IdentityResult result)
        {
            var error = result.Errors.First(g => g.Description != " ");
            return error.Description;
        }
    }
}
