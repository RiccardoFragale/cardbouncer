using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CardBouncer.Frontend.Models;
using CardBouncer.Frontend.Entities;

namespace CardBouncer.Frontend.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CardBouncer.Frontend.Entities.ApplicantDetails> ApplicantDetails { get; set; }
    }
}
