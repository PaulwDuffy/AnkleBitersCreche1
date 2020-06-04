using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnkleBitersCreche1.Data;
using AnkleBitersCreche1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AnkleBitersCreche1.Pages.Services
{
    public class HistoryModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public HistoryModel(ApplicationDbContext Db)
        {
            _db = Db;
        }

        [BindProperty]
        public List<ServiceHeader> ServiceHeader{ get; set; }

        public async Task OnGet(int StudentId)
        {
            ServiceHeader = await _db.ServiceHeader.Include(s => s.Student)
                .Include(c => c.Student.ApplicationUser)
                .Where(c => c.StudentId == StudentId)
                .ToListAsync();
        }
    }
}