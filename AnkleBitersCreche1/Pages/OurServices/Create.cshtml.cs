
using AnkleBitersCreche1.Models;
using AnkleBitersCreche1.Utility;
using AnkleBitersCreche1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace AnkleBitersCreche1.Pages.OurServices
{
    [Authorize(Roles = SD.AdminEndUser)] //restrict access to Admin user
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]

        public OurService OurService { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }




        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(OurService OurService)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.OurService.Add(OurService);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}