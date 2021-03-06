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
    public class HistoryModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public HistoryModel(ApplicationDbContext Db)
        {
            _db = Db;
        }

        [BindProperty]
        public List<ServiceHeader> ServiceHeader{ get; set; }

        public string UserId { get; set; }

        public async Task OnGet(int StudentId)
        {
            ServiceHeader = await _db.ServiceHeader.Include(s => s.Student)
                .Include(c => c.Student.ApplicationUser)
                .Where(c => c.StudentId == StudentId)
                .ToListAsync();

            UserId = _db.Student.Where(u => u.Id == StudentId).ToList().FirstOrDefault().UserId;
        }
    }
}