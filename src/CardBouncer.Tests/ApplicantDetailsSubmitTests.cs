using System;
using CardBouncer.Frontend.Controllers;
using CardBouncer.Frontend.DomainEntities;
using CardBouncer.Frontend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace CardBouncer.Tests
{
    [TestFixture]
    public class ApplicantDetailsSubmitTests
    {
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

        [Test]
        public void SubmitShouldUpdateUserDetailsWhenExistingUser()
        {
            var mockRepo = new Mock<IApplicantDetailsRepository>();
            var loadResult = new ApplicantDetails
            {
                Id = 1,
                FirstName = "test",
                AnnualIncome = 20000
            };

            loadResult.Initialize();

            mockRepo.Setup(x => x.LoadApplicantDetails(It.IsAny<ApplicantDetails>())).ReturnsAsync(loadResult);

            var controller = new SearchController(mockRepo.Object);

            var newApplicant = new ApplicantDetails
            {
                FirstName = "test",
                DateOfBirth = new DateTime(2002, 01, 10),
                AnnualIncome = 40000
            };

            var result = controller.Create(newApplicant);

            Assert.IsInstanceOf<RedirectToActionResult>(result.Result);
            Assert.AreEqual(loadResult.GuId, ((RedirectToActionResult) result.Result).RouteValues["guid"]);
        }
    }
}