using CardBouncer.Frontend.DomainEntities;
using System.Collections.Generic;

namespace CardBouncer.Frontend.Models
{
    public class SelectionViewModel
    {
        public string Message { get; set; }
        public ICollection<Card> Cards { get; set; }
    }
}
