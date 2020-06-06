using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AnkleBitersCreche.Data;
using AnkleBitersCreche.Models;
using AnkleBitersCreche.Utility;

namespace AnkleBitersCreche.Pages.Users
{
    [Authorize(Roles = SD.AdminEndUser)] //only allow a Admin account to delete a user
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id.Trim().Length == 0)
            {
                return NotFound();
            }

            ApplicationUser = await _db.ApplicationUser.FirstOrDefaultAsync(m => m.Id == id);

            if (ApplicationUser == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userInDb = await _db.Users.SingleOrDefaultAsync(u => u.Id == ApplicationUser.Id);

            _db.Users.Remove(userInDb);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }


    }
}