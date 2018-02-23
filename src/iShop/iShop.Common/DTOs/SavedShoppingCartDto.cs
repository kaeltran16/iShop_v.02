using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using iShop.Common.Base;
using iShop.Common.DataAnnotations;

namespace iShop.Common.DTOs
{
    public class SavedShoppingCartDto: ISavedBaseDto
    {
        public Guid Id { get; set; }
        [GuidFormat(ErrorMessage = "The User Id is missing or not in format.")]
        public Guid UserId { get; set; }
        [NotEmptyCollection(ErrorMessage = "Must contain at least 1 cart item.")]
        public ICollection<CartDto> Carts { get; set; } = new Collection<CartDto>();
    }
}
