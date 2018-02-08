using System;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class ShippingEntity : KeyEntity, IEntityBase
    {
        public Guid OrderId { get; set; }
        public OrderEntity Order { get; set; }
        public DateTime ShippingDate { get; set; }
        public ShippingStateEntity ShippingState { get; set; }
        public double Charge { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName  { get; set; }
        public ShippingEntity()
        {
            ShippingState = ShippingStateEntity.None;
        }
    }
}
