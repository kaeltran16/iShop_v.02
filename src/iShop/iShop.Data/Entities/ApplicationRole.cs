using System;
using iShop.Data.Base;
using Microsoft.AspNetCore.Identity;

namespace iShop.Data.Entities
{
    public class ApplicationRole: IdentityRole<Guid>, IEntityBase
    {
        public string Description { get; set; }
    }
}
