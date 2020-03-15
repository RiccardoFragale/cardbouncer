using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CardBouncer.Frontend.Data;
using CardBouncer.Frontend.DomainEntities;
using CardBouncer.Frontend.Repositories;

namespace CardBouncer.Frontend.Controllers
{
    public class SearchController : Controller
    {
        private readonly IApplicantDetailsRepository Repository;

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

                var id = applicantDetails.Id;
                if (existingApplicantDetails != null)
                {
                    id = existingApplicantDetails.Id;
                }

                return RedirectToAction(nameof(Details),new { id });
            }

            return View(applicantDetails);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await Repository.LoadApplicantDetails(id);

            return View(model);
        }
    }
}
