using System;
using iShop.Data.Base;
using Microsoft.AspNetCore.Identity;

namespace iShop.Data.Models
{
    public class ApplicationRole: IdentityRole<Guid>, IModelBase
    {
        public string Description { get; set; }
    }
}
