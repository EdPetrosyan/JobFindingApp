using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppLayer;
using JobFindingModels;

namespace JobFindingApp.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobFindingDatabaseRepo _dbRepo;
        public JobsController(IJobFindingDatabaseRepo dbRepo)
        {
            _dbRepo = dbRepo;
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
                Categories = await _dbRepo.GetJobCategories(),
                JobTypes = await _dbRepo.GetJobTypes(),
                Locations = await _dbRepo.GetLocations()
            };
            return View("Filters", filters);
        }

        [HttpPost]
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
        public IActionResult CreateJob()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> AddJob([FromBody]Job job)
        {
            await _dbRepo.AddJob(job);
            return Ok();
        }
    }
}
