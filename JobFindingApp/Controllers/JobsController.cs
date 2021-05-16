using AppLayer;
using AppLayer.Cache;
using JobFindingModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobFindingApp.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobFindingDatabaseRepo _dbRepo;
        private readonly ICacheManager _cache;
        public JobsController(IJobFindingDatabaseRepo dbRepo, ICacheManager cache)
        {
            _dbRepo = dbRepo;
            _cache = cache;
        }
        public async Task<IActionResult> Jobs()
        {
            var result = await _dbRepo.GetJobsForListing();
            return View(result);
        }

        public async Task<IActionResult> GetJob(int id)
        {
            var result = await _dbRepo.GetJobById(id);
            return View(result);
        }

        public async Task<IActionResult> SearchJob(string input)
        {
            var result = await _dbRepo.SearchJob(input);
            return View("Jobs", result);
        }

        public async Task<IActionResult> GetFilters()
        {
            Filters filters = new()
            {
                Categories = _cache.GetOrAdd("_categories", await _dbRepo.GetJobCategories()),
                JobTypes = _cache.GetOrAdd("_types", await _dbRepo.GetJobTypes()),
                Locations = await _dbRepo.GetLocations()
            };
            return View("Filters", filters);
        }

        public async Task<IActionResult> GetFilteredList(List<int> categories, List<int> types, List<string> locations)
        {
            var result = await _dbRepo.GetFilteredList(categories, types, locations);
            return View("Jobs", result);
        }

        public async Task<IActionResult> MarkAsBookmarked(int id)
        {
            await _dbRepo.MarkAsBookmarked(id);
            var result = await _dbRepo.GetJobsForListing();
            return View("Jobs", result);
        }
        public async Task<IActionResult> CreateJob()
        {
            var categories = _cache.GetOrAdd("_categories", await _dbRepo.GetJobCategories());
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var companies = _cache.GetOrAdd("_companies", await _dbRepo.GetCompanies());
            ViewBag.Companies = new SelectList(companies, "Id", "Name");

            var types = _cache.GetOrAdd("_types", await _dbRepo.GetJobTypes());
            ViewBag.JobTypes = new SelectList(types, "Id", "Type");

            return View();
        }


        public async Task<IActionResult> AddJob(Job job)
        {
            await _dbRepo.AddJob(job);
            var result = await _dbRepo.GetJobsForListing();
            return View("Jobs", result);
        }

        public async Task<IActionResult> DeleteJob(int id)
        {
            await _dbRepo.DeleteJob(id);
            var result = await _dbRepo.GetJobsForListing();
            return View("Jobs", result);
        }
    }
}
