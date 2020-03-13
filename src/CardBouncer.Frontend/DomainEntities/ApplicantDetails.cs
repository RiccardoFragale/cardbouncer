using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardBouncer.Frontend.DomainEntities
{
    public class ApplicantDetails : EntityBase
    {
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public decimal AnnualIncome { get; set; }

        public virtual IEnumerable<SearchResult> SearchResults { get; set; }

        public void Initialize()
        {
            GuId = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }
    }
}

