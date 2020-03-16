using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CardBouncer.Frontend.DomainEntities;
using CardBouncer.Frontend.Repositories;
using System;
using CardBouncer.Frontend.Extensions;
using CardBouncer.Frontend.Models;
using System.Collections.Generic;

namespace CardBouncer.Frontend.Controllers
{
    public class SearchController : Controller
    {
        private readonly IApplicantDetailsRepository ApplicantDetailsRepository;
        private readonly ICardsRepository CardsRepository;

        public SearchController(IApplicantDetailsRepository applicantDetailsRepository, ICardsRepository cardsRepository)
        {
            ApplicantDetailsRepository = applicantDetailsRepository;
            CardsRepository = cardsRepository;
        }

        // GET: Search
        public IActionResult Index()
        {
            return RedirectToAction("Create");
        }

        // GET: Search/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,DateOfBirth,AnnualIncome")] ApplicantDetails applicantDetails)
        {
            if (ModelState.IsValid)
            {
                var existingApplicantDetails = await ApplicantDetailsRepository.LoadApplicantDetails(applicantDetails);

                if (existingApplicantDetails == null)
                {
                    applicantDetails.Initialize();
                    await ApplicantDetailsRepository.Create(applicantDetails);
                }
                else
                {
                    existingApplicantDetails.AnnualIncome = applicantDetails.AnnualIncome;

                    await ApplicantDetailsRepository.Update(existingApplicantDetails);
                }

                var guid = applicantDetails.GuId;
                if (existingApplicantDetails != null)
                {
                    guid = existingApplicantDetails.GuId;
                }

                return RedirectToAction(nameof(Selection), new { guid });
            }

            return View(applicantDetails);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await ApplicantDetailsRepository.LoadApplicantDetails(id);

            return View(model);
        }

        public async Task<IActionResult> Selection(Guid guid)
        {
            var model = await ApplicantDetailsRepository.LoadApplicantDetails(guid);
            int age = model.DateOfBirth.CalculateAge();

            var viewModel = new SelectionViewModel();
            var cards = CardsRepository.LoadCards(age, model.AnnualIncome).ToList();

            if (cards.Any())
            {
                viewModel.Cards = cards;
                var searchResult = new SearchResult
                {
                    ResultsAsString = string.Join(",", viewModel.Cards.Select(x => x.Name).ToList()),
                    ExtApplicantDetailsId = model.Id
                };

                searchResult.Initialize();

                await ApplicantDetailsRepository.Create(searchResult);
            }
            else
            {
                viewModel.Message = "No credit cards available.";
            }

            return View(viewModel);
        }
    }
}
