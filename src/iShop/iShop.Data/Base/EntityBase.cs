using System;

namespace iShop.Data.Base
{
    public class EntityBase
    {
        public Guid Id { get; set; }

        public EntityBase()
        {
            if (Id == Guid.Empty)
                Id = Guid.NewGuid();
        }
    }
}
