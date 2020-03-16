using System.Collections.Generic;
using System.Threading.Tasks;
using CardBouncer.Frontend.DomainEntities;

namespace CardBouncer.Frontend.Repositories
{
    public interface ICardsRepository
    {
        IEnumerable<Card> LoadCards(int age, decimal annualIncome);
    }
}