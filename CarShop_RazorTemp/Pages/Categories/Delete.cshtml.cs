using CarShop_RazorTemp.Data;
using CarShop_RazorTemp.Models.CarShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarShop_RazorTemp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Category Category { get; set; }

        public void OnGet(int? Id)
        {
            if(Id != null && Id != 0)
            {
                Category = _db.Categories.Find(Id);
            }
        }

        public IActionResult OnPost()
        {
            if (Category == null) return NotFound();
            _db.Categories.Remove(Category);
            _db.SaveChanges();
            TempData["success"] = "Категория была успешно удалена!";
            return RedirectToPage("Index");
        }
    }
}
