﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnkleBitersCreche.Data;
using AnkleBitersCreche.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AnkleBitersCreche.Pages.Services
{
    [Authorize]
    public class DetailsModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public DetailsModel(ApplicationDbContext Db)
        {
            _db = Db;
        }

        public ServiceHeader ServiceHeader { get; set; }
        public List<ServiceDetails> ServiceDetails { get; set; }

        public void OnGet(int serviceId)
        {
            ServiceHeader = _db.ServiceHeader
                .Include(s => s.Student)
                .Include(s => s.Student.ApplicationUser)
                .FirstOrDefault(s => s.Id == serviceId);
            ServiceDetails = _db.ServiceDetails.Where(s => s.ServiceHeaderId == serviceId).ToList();
        }
    }
}