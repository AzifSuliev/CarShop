using CarShop_RazorTemp.Data;
using CarShop_RazorTemp.Models.CarShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarShop_RazorTemp.Pages.Categories
{
    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public List<Category> CategoryList { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();
        }
    }
}
