using AnkleBitersCreche1.Models;
using AnkleBitersCreche1.Data;
using AnkleBitersCreche1.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnkleBitersCreche1.Pages.OurServices
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