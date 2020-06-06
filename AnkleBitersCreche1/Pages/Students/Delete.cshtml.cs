using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AnkleBitersCreche.Data;
using AnkleBitersCreche.Models;

namespace AnkleBitersCreche.Pages.Students
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [TempData]
        public string StatusMessage { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _db.Student
                .Include(c => c.ApplicationUser).FirstOrDefaultAsync(m => m.Id == id);

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Student == null)
            {
                return NotFound();
            }
            var userId = Student.UserId;

            _db.Student.Remove(Student);
            await _db.SaveChangesAsync();
            StatusMessage = "Student deleted successfully.";
            return RedirectToPage("./Index", new { userId });
        }
    }
}
