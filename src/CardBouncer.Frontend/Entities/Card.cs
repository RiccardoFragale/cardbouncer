using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardBouncer.Frontend.Entities
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
