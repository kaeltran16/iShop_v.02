using System;
using Microsoft.AspNetCore.Identity;

namespace iShop.Data.Entities
{
    public class ApplicationRole: IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
