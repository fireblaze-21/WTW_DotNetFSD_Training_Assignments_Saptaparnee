using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication11.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [Authorize(Roles = "Admin")]
    public class ProductController : ControllerBase
    {


        List<Product> products = new List<Product> {
                new Product(){ Id=1, Name="Laptop-1", Price=65000 },
                new Product(){ Id=2, Name="Laptop-2", Price=65000},
                new Product(){ Id=3, Name="Laptop-3", Price=65000},
                new Product(){ Id=4, Name="Laptop-4", Price=65000},
                new Product(){ Id=5, Name="Laptop-5", Price=65000},
            };

        [HttpGet]
        public IActionResult GetProducts()
        {

            var user = HttpContext.User;
            string str = user.Identity?.Name;
            var roles = user.FindAll(ClaimTypes.Role).ToList();


            return Ok(products);
        }



        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {

            var product = products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound("Requested Product not available");
            }
            else
            {
                return Ok(product);
            }


        }


    }
}
