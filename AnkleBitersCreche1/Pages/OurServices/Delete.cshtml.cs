using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnkleBitersCreche1.Data;
using AnkleBitersCreche1.Models;
using AnkleBitersCreche1.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;



namespace AnkleBitersCreche1.Pages.OurServices
{
    [Authorize(Roles = SD.AdminEndUser)] //restrictaccess to Admin user

    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public OurService OurService { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OurService = await _db.OurService.FirstOrDefaultAsync(m => m.Id == id);

            if (OurService == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (OurService == null)
            {
                return NotFound();
            }

            _db.OurService.Remove(OurService);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

