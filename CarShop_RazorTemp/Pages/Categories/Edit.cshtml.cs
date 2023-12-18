using CarShop_RazorTemp.Data;
using CarShop_RazorTemp.Models.CarShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarShop_RazorTemp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        
        public Category? Category { get; set; }

        public void OnGet(int? Id)
        {
            if(Id != null && Id != 0)
            {
                Category = _db.Categories.Find(Id);
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid) // Проверка состояния модели
            {
                _db.Categories.Update(Category);
                _db.SaveChanges();
                TempData["success"] = "Категория была успешно изменена!";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
