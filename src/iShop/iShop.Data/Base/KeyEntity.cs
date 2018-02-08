using System;

namespace iShop.Data.Base
{
    public class KeyEntity
    {
        public Guid Id { get; set; }

        public KeyEntity()
        {
            if (Id == Guid.Empty)
                Id = Guid.NewGuid();
        }
    }
}
