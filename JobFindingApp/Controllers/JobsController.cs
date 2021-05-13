﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDbLayer;

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

    }
}