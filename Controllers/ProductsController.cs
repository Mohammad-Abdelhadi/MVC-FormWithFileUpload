using Microsoft.AspNetCore.Mvc;
using MVC_CRUD.Data;
using MVC_CRUD.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVC_CRUD.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        private readonly ApplicationContext _context;
        public ProductsController(ApplicationContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]

        public IActionResult Index()
        {
            return View(_context.Products.ToList());

        }
        [HttpGet]
        public ActionResult test()
        {
            return View(_context.Products.ToList());
        }
        [HttpGet]
        public ActionResult View(int Id)
        {
            return View(_context.Products.Find(Id));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Age,country,Image")] Product imageModel)
        {
            if (imageModel.Image != null && imageModel.Image.Length > 0 && ModelState.IsValid)
            {
                try
                {
                    // Save image to wwwroot/images
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(imageModel.Image.FileName);
                    string extension = Path.GetExtension(imageModel.Image.FileName);
                    string uniqueFileName = fileName + "_" + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath, "images", uniqueFileName);

                    // Check if the images folder exists, if not, create it
                    string folderPath = Path.Combine(wwwRootPath, "images");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Save the image
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await imageModel.Image.CopyToAsync(fileStream);
                    }

                    // Set the ImagePath property of the model to the file path
                    imageModel.ImagePath = "/images/" + uniqueFileName;

                    // Insert record
                    _context.Add(imageModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("test");
                }
                catch (Exception ex)
                {
                    // Handle exceptions (log them, show an error message, etc.)
                    ModelState.AddModelError("Image", $"An error occurred: {ex.Message}");
                    return View(imageModel);
                }
            }

            // Handle validation errors
            return View(imageModel);
        }


        [HttpGet]
        public ActionResult Edit(int Id)
        {
            return View(_context.Products.Find(Id));

        }
        [HttpPost]
        public ActionResult Edit(Product obj)
        {
            _context.Update(obj);
            _context.SaveChanges();
            return RedirectToAction("test");

        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var productToDelete = _context.Products.Find(Id);
            if (productToDelete == null)
            {
                return NotFound(); // Or any other appropriate error response
            }
            return View(productToDelete);
        }

        [HttpPost]
        public ActionResult ConfirmDelete(int Id) // Changed the method name to ConfirmDelete
        {
            var productToDelete = _context.Products.Find(Id);
            if (productToDelete == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productToDelete);
            _context.SaveChanges(); // Save changes to the database

            return RedirectToAction("Index"); // Redirect to the action where you list all products, change "Index" to your actual action name if it's different
        }

    }
}
