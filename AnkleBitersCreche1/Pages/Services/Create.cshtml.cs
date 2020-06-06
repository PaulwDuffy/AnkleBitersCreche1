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
using AnkleBitersCreche.Models.ViewModel;
using AnkleBitersCreche.Utility;

namespace AnkleBitersCreche.Pages.Services
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public StudentServiceViewModel StudentServiceVM { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGet(int StudentId)
        {
            StudentServiceVM = new StudentServiceViewModel
            {
                Student = await _db.Student.Include(c => c.ApplicationUser).FirstOrDefaultAsync(c => c.Id == StudentId),
                ServiceHeader = new Models.ServiceHeader()
            };

            List<String> lstServiceTypeInShoppingCart = _db.ServiceShoppingCart
                                                            .Include(c => c.OurService)
                                                            .Where(c => c.StudentId == StudentId)
                                                            .Select(c => c.OurService.Name)
                                                            .ToList();

            IQueryable<OurService> lstService = from s in _db.OurService
                                                where !(lstServiceTypeInShoppingCart.Contains(s.Name))
                                                select s;

            StudentServiceVM.OurServiceList = lstService.ToList();

            StudentServiceVM.ServiceShoppingCart = _db.ServiceShoppingCart
                                                    .Include(c => c.OurService)
                                                    .Where(c => c.StudentId == StudentId)
                                                    .ToList();
            StudentServiceVM.ServiceHeader.TotalPrice = 0;

            foreach (var item in StudentServiceVM.ServiceShoppingCart)
            {
                StudentServiceVM.ServiceHeader.TotalPrice += item.OurService.Price;
            }

            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                StudentServiceVM.ServiceHeader.DateAdded = DateTime.Now;
                StudentServiceVM.ServiceShoppingCart = _db.ServiceShoppingCart.Include(c => c.OurService)
                    .Where(c => c.StudentId == StudentServiceVM.Student.Id).ToList();

                foreach (var item in StudentServiceVM.ServiceShoppingCart)
                {
                    StudentServiceVM.ServiceHeader.TotalPrice += item.OurService.Price;
                }
                StudentServiceVM.ServiceHeader.StudentId = StudentServiceVM.Student.Id;

                _db.ServiceHeader.Add(StudentServiceVM.ServiceHeader);
                await _db.SaveChangesAsync();

                foreach (var detail in StudentServiceVM.ServiceShoppingCart)
                {
                    ServiceDetails serviceDetails = new ServiceDetails
                    {
                        ServiceHeaderId = StudentServiceVM.ServiceHeader.Id,
                        ServiceName = detail.OurService.Name,
                        ServicePrice = detail.OurService.Price,
                        OurServiceId = detail.OurServiceId
                    };

                    _db.ServiceDetails.Add(serviceDetails);

                }
                _db.ServiceShoppingCart.RemoveRange(StudentServiceVM.ServiceShoppingCart);

                await _db.SaveChangesAsync();

                return RedirectToPage("../Students/Index", new { userId = StudentServiceVM.Student.UserId });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCart()
        {
            ServiceShoppingCart objServiceCart = new ServiceShoppingCart()
            {
                StudentId = StudentServiceVM.Student.Id,
                OurServiceId = StudentServiceVM.ServiceDetails.OurServiceId
            };

            _db.ServiceShoppingCart.Add(objServiceCart);
            await _db.SaveChangesAsync();
            return RedirectToPage("Create", new { StudentId = StudentServiceVM.Student.Id });
        }

        public async Task<IActionResult> OnPostRemoveFromCart(int OurserviceId)
        {
            ServiceShoppingCart objServiceCart = _db.ServiceShoppingCart
                .FirstOrDefault(u => u.StudentId == StudentServiceVM.Student.Id && u.OurServiceId == OurserviceId);


            _db.ServiceShoppingCart.Remove(objServiceCart);
            await _db.SaveChangesAsync();
            return RedirectToPage("Create", new { StudentId = StudentServiceVM.Student.Id });
        }
    }
}