using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnkleBitersCreche1.Data;
using AnkleBitersCreche1.Models;
using Microsoft.AspNetCore.Authorization;
using AnkleBitersCreche1.Utility;

namespace AnkleBitersCreche1.Pages.OurServices
{
    [Authorize(Roles = SD.AdminEndUser)] //restrictaccess to Admin user
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditModel(AnkleBitersCreche1.Data.ApplicationDbContext db)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var serviceFronDb = await _db.OurService.FirstOrDefaultAsync(s => s.Id == OurService.Id);

            serviceFronDb.Name = OurService.Name;
            serviceFronDb.Price = OurService.Price;
            await _db.SaveChangesAsync();


            //try
            //{
            //    await _db.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!OurServiceExists(OurService.Id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return RedirectToPage("./Index");
        }

        //private bool OurServiceExists(int id)
        //{
        //    return _db.OurService.Any(e => e.Id == id);
        //}
    }
}
