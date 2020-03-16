using System;
using System.Collections.Generic;
using System.Text;
using CardBouncer.Frontend.Controllers;
using CardBouncer.Frontend.DomainEntities;
using CardBouncer.Frontend.Models;
using CardBouncer.Frontend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace CardBouncer.Tests
{
    [TestFixture]
    public class CardSelectionTests
    {
        [Test]
        public void CardSelectionShouldReturnEmptyResultForUnderAge()
        {
            var mockRepo = new Mock<IApplicantDetailsRepository>();
            var loadResult = new ApplicantDetails
            {
                Id = 1,
                FirstName = "test",
                AnnualIncome = 20000,
                DateOfBirth = new DateTime(2005, 10, 31)
            };

            loadResult.Initialize();

            mockRepo.Setup(x => x.LoadApplicantDetails(It.IsAny<Guid>())).ReturnsAsync(loadResult);
            var controller = new SearchController(mockRepo.Object, new CardsRepository());

            var result = controller.Selection(Guid.NewGuid());

            Assert.IsInstanceOf<ViewResult>(result.Result);

            var model = (SelectionViewModel) ((ViewResult) result.Result).Model;
            Assert.IsEmpty(model.Cards);
        }
    }
}
