using AnkleBitersCreche.Models;
using AnkleBitersCreche.Data;
using AnkleBitersCreche.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnkleBitersCreche.Pages.OurServices
{
    [Authorize(Roles = SD.AdminEndUser)] //restrictaccess to Admin user
    public class IndexModel : PageModel
    {
        
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IList<OurService> OurService { get; set; }

        public async Task<IActionResult> OnGet()
        {
            OurService = await _db.OurService.ToListAsync();
            return Page();
        }
    }
}