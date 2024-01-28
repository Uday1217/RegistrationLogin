using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using RegistrationLogin.Mvc.Models;
using System.Text;

namespace RegistrationLogin.Mvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly RegContext _context;
        public ProductController(RegContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {

            //  ViewBag.Categories = new SelectList(_context.ProductCategories, "Name", "Name");
            //  ViewBag.SubCategories = new SelectList(_context.ProductSubCategories, "Name", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormFile Image, Product product)
        {

            try
            {
                if (Image != null && Image.Length > 0)
                {
                    using (var img = new MemoryStream())
                    {
                        Image.CopyTo(img);
                        img.Position = 0; 
                        product.Image = img.ToArray();
                    }
                }

                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "An error occurred while saving changes to the database.");
                return View();
            }

            return View();
        }
        public IActionResult Subcategorys(string categoryId) 
        {
            var subcategories = _context.ProductSubCategories
                .Where(sub => sub.CategoryId == Convert.ToInt32(categoryId))
              //  .Where(sub => sub.Name == categoryId)
                .Select(sub => sub.Name)
               // .Select(sub => sub.Id)
                .ToList();

            return Json(subcategories);
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.Products.ToListAsync();
            return View(data);
        }




    }

    public class StringManipulator
    {
        public string ProcessString(string input)
        {
            StringBuilder result = new StringBuilder();

            foreach (char currentChar in input)
            {
                if (char.IsLetter(currentChar))
                {
                    result.Append(char.ToUpper(currentChar));
                }
                else if (char.IsDigit(currentChar))
                {
                    result.Append(currentChar);
                }
                else if (char.IsWhiteSpace(currentChar))
                {
                    result.Append(' ');
                }
            }

            return result.ToString();
        }
    }
}
