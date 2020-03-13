using System;

namespace CardBouncer.Frontend.DomainEntities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public Guid GuId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

