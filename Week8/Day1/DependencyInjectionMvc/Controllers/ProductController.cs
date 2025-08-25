using Microsoft.AspNetCore.Mvc;
using DependencyInjectionMvc.Models;
using DependencyInjectionMvc.Services;

namespace DependencyInjectionMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        // GET: /Product
        public IActionResult Index()
        {
            var products = _service.GetProducts();
            return View(products);
        }

        // GET: /Product/Details/5
        public IActionResult Details(int id)
        {
            var product = _service.GetProductById(id);
            if (product == null)
            {
                ViewBag.ErrorMessage = "Product not found.";
                return View();
            }
            return View(product);
        }

        // GET: /Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _service.CreateProduct(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: /Product/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _service.GetProductById(id);
            if (product == null)
            {
                ViewBag.ErrorMessage = "Product not found.";
                return View();
            }

            return View(product);
        }

        // POST: /Product/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateProduct(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: /Product/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _service.GetProductById(id);
            if (product == null)
            {
                ViewBag.ErrorMessage = "Product not found.";
                return View();
            }

            return View(product);
        }

        // POST: /Product/Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
