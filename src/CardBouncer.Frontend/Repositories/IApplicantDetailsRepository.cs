using System.Threading.Tasks;
using CardBouncer.Frontend.DomainEntities;

namespace CardBouncer.Frontend.Repositories
{
    public interface IApplicantDetailsRepository
    {
        Task<ApplicantDetails> LoadApplicantDetails(ApplicantDetails entity);
        Task<int> Create(ApplicantDetails entity);
        Task<int> Update(ApplicantDetails entity);
        Task<ApplicantDetails> LoadApplicantDetails(int id);
    }
}