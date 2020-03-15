using CardBouncer.Frontend.DomainEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CardBouncer.Frontend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicantDetails> ApplicantDetails { get; set; }
        public DbSet<SearchResult> SearchResults { get; set; }
    }
}
