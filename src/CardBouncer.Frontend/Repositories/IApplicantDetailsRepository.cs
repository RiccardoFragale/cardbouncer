using System;
using System.Threading.Tasks;
using CardBouncer.Frontend.DomainEntities;

namespace CardBouncer.Frontend.Repositories
{
    public interface IApplicantDetailsRepository
    {
        Task<ApplicantDetails> LoadApplicantDetails(ApplicantDetails entity);
        Task<int> Create<T>(T entity);
        Task<int> Update(ApplicantDetails entity);
        Task<ApplicantDetails> LoadApplicantDetails(int id);
        Task<ApplicantDetails> LoadApplicantDetails(Guid guid);
    }
}