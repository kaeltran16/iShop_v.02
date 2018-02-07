using System;
using System.Collections.Generic;

namespace iShop.Common.DTOs
{
    public class ShoppingCartDto
    {
        public Guid Id { get; set; }
        public ApplicationUserDto User { get; set; }
        public ICollection<CartDto> Carts { get; set; }
        public DateTime PlacedDate { get; set; }
    }
}
