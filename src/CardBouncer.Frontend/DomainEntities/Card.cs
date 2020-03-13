using System.ComponentModel.DataAnnotations;

namespace CardBouncer.Frontend.DomainEntities
{
    public class Card : EntityBase
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public decimal Apr { get; set; }

        [MaxLength(500)]
        public string PromotionalMessage { get; set; }
    }
}
