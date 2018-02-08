using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class ShoppingCartEntity : KeyEntity, IEntityBase
    {
        public Guid? UserId { get; set; }
        public ApplicationUserEntity User { get; set; }
        public ICollection<CartEntity> Carts { get; set; }
        public DateTime PlacedDate { get; set; }

        public ShoppingCartEntity()
        {
            Carts = new Collection<CartEntity>();
            PlacedDate = DateTime.Now;
        }
    }
}
