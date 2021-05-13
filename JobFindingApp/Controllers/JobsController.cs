using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppLayer;

namespace JobFindingApp.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobFindingDatabaseRepo _dbRepo;
        public JobsController(IJobFindingDatabaseRepo dbRepo)
        {
            _dbRepo = dbRepo;
        }
        public IActionResult Index()
        {
            return View();
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
    }
}
