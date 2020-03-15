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
        private readonly IApplicantDetailsRepository Repository;
        private const decimal BARKLEYS_THRESHOLD = 30000;
        private const int MINIMUM_AGE = 18;

        public SearchController(IApplicantDetailsRepository repository)
        {
            Repository = repository;
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
                var existingApplicantDetails = await Repository.LoadApplicantDetails(applicantDetails);

                if (existingApplicantDetails == null)
                {
                    applicantDetails.Initialize();
                    await Repository.Create(applicantDetails);
                }
                else
                {
                    existingApplicantDetails.AnnualIncome = applicantDetails.AnnualIncome;

                    await Repository.Update(existingApplicantDetails);                    
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
            var model = await Repository.LoadApplicantDetails(id);

            return View(model);
        }

        public async Task<IActionResult> Selection(Guid guid)
        {
            var model = await Repository.LoadApplicantDetails(guid);
            int age = model.DateOfBirth.CalculateAge();

            var viewModel = new SelectionViewModel
            {
                Cards = new List<Card>()
            };

            if (age > MINIMUM_AGE)
            {
                if (model.AnnualIncome > BARKLEYS_THRESHOLD)
                {
                    viewModel.Cards.Add(new Card { Name = "Vanquish" });
                }
                else
                {
                    viewModel.Cards.Add(new Card { Name = "Barkleys" });
                }
            }
            else
            {
                viewModel.Message = "No credit cards available.";
            }

            var searchResult = new SearchResult
            {
                ResultsAsString = string.Join(",", viewModel.Cards.Select(x => x.Name).ToList()),
                ExtApplicantDetailsId = model.Id
            };

            searchResult.Initialize();

            await Repository.Create(searchResult);

            return View(viewModel);
        }
    }
}
