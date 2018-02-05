using System;
using Microsoft.AspNetCore.Identity;

namespace iShop.Domain.Entities.Entities
{
    public class ApplicationRole: IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
