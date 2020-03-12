﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardBouncer.Frontend.Entities
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
    }
}
