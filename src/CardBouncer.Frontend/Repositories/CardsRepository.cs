using System;
using System.Collections.Generic;
using System.Linq;
using CardBouncer.Frontend.DomainEntities;

namespace CardBouncer.Frontend.Repositories
{
    public class CardsRepository : ICardsRepository
    {
        private const decimal BARKLEYS_THRESHOLD = 30000;
        private const int MINIMUM_AGE = 18;

        public IEnumerable<Card> LoadCards(int age, decimal annualIncome)
        {
            var cards = new List<Card>();

            if (age > MINIMUM_AGE)
            {
                if (annualIncome > BARKLEYS_THRESHOLD)
                {
                    cards.Add(new Card
                    {
                        Name = "Vanquis",
                        Apr = 20,
                        PromotionalMessage = "Acquire the power of Vanquis"
                    });
                }
                else
                {
                    cards.Add(new Card
                    {
                        Name = "Barkleys",
                        Apr = 10,
                        PromotionalMessage = "Creating opportunities to rise"
                    });
                }
            }

            return cards;
        }
    }
}