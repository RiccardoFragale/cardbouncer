using System;
using System.Linq;
using System.Threading.Tasks;
using CardBouncer.Frontend.Data;
using CardBouncer.Frontend.DomainEntities;
using Microsoft.EntityFrameworkCore;

namespace CardBouncer.Frontend.Repositories
{
    public class ApplicantDetailsRepository : IApplicantDetailsRepository
    {
        private readonly ApplicationDbContext DbContext;

        public ApplicantDetailsRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Task<ApplicantDetails> LoadApplicantDetails(ApplicantDetails entity)
        {
            return DbContext.ApplicantDetails
                .Where(x => x.LastName.ToLower() == entity.LastName.ToLower())
                .Where(x => x.DateOfBirth.Date == entity.DateOfBirth.Date)
                .FirstOrDefaultAsync(x => x.FirstName.ToLower() == entity.FirstName.ToLower());
        }

        public async Task<int> Create<T>(T entity)
        {
            await DbContext.AddAsync(entity);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> Update(ApplicantDetails entity)
        {
            DbContext.Update(entity);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<ApplicantDetails> LoadApplicantDetails(int id)
        {
            return await DbContext.ApplicantDetails.FindAsync(id);
        }

        public async Task<ApplicantDetails> LoadApplicantDetails(Guid guid)
        {
            return await DbContext.ApplicantDetails.FirstAsync(x=>x.GuId == guid);
        }

    }
}
