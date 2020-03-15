using CardBouncer.Frontend.Controllers;
using CardBouncer.Frontend.Data;
using CardBouncer.Frontend.DomainEntities;
using CardBouncer.Frontend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace CardBouncer.Tests
{
    public class ApplicantDetailsSubmitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SubmitShouldCreateUserDetails()
        {
            var mockRepo = new Mock<IApplicantDetailsRepository>();
            mockRepo.Setup(x => x.LoadApplicantDetails(It.IsAny<ApplicantDetails>())).ReturnsAsync((ApplicantDetails)null);

            var controller = new SearchController(mockRepo.Object);

            var newApplicant = new ApplicantDetails
            {
                FirstName = "test"
            };

            var result = controller.Create(newApplicant);

            Assert.IsInstanceOf<RedirectToActionResult>(result.Result);
        }
    }
}