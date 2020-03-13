using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CardBouncer.Frontend.Data;
using CardBouncer.Frontend.Entities;

namespace CardBouncer.Frontend.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext DbContext;

        public SearchController(ApplicationDbContext context)
        {
            DbContext = context;
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
                var existingApplicantDetails = await DbContext.ApplicantDetails
                    .Where(x => x.LastName.ToLower() == applicantDetails.LastName.ToLower())
                    .Where(x => x.DateOfBirth.Date == applicantDetails.DateOfBirth.Date)
                    .FirstOrDefaultAsync(x => x.FirstName.ToLower() == applicantDetails.FirstName.ToLower());

                if (existingApplicantDetails == null)
                {
                    applicantDetails.Initialize();
                    DbContext.Add(applicantDetails);
                }
                else
                {
                    existingApplicantDetails.AnnualIncome = applicantDetails.AnnualIncome;

                    DbContext.Update(existingApplicantDetails);                    
                }

                await DbContext.SaveChangesAsync();

                var id = applicantDetails.Id;
                if (existingApplicantDetails != null)
                {
                    id = existingApplicantDetails.Id;
                }

                return RedirectToAction(nameof(Details),new { id });
            }

            return View(applicantDetails);
        }

        public IActionResult Details(int id)
        {
            var model = DbContext.ApplicantDetails.Find(id);

            return View(model);
        }
    }
}
