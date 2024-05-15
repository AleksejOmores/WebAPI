using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PZ_webAPI.Models;

namespace PZ_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly string connestingString;

        public ProductsController(IConfiguration configuration)
        {
            connestingString = configuration["ConnectionStrings:DefaultConnection"] ?? "";
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductDTO productDTO)
        {
            try
            {
                using (var connection = new SqlConnection(connestingString))
                {
                    connection.Open();
                    string sql = "Insert into Products " +
                        "(ProductName, Price) Values " +
                        "(@ProductName, @Price)";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", productDTO.ProductName);
                        command.Parameters.AddWithValue("@Price", productDTO.Price);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Products","Извините, произошла ошибка");
                return BadRequest(ModelState);
            }
            return Ok();
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            List<Products> products = new List<Products>();

            try
            {
                using (var connection = new SqlConnection(connestingString))
                {
                    connection.Open();
                    string sql = "Select * from Products";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Products product = new Products();

                                product.ProductID = reader.GetInt32(0);
                                product.ProductName = reader.GetString(1);
                                product.Price = reader.GetInt32(2);

                                products.Add(product);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Products", "Извините, произошла ошибка");
                return BadRequest(ModelState);
            }
            return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            Products products = new Products();
            try
            {
                using (var connection = new SqlConnection(connestingString))
                {
                    connection.Open();

                    string sql = "Select * from Products Where ProductID=@id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                products.ProductID = reader.GetInt32(0);
                                products.ProductName = reader.GetString(1);
                                products.Price = reader.GetInt32(2);
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Products", "Извините, произошла ошибка");
                return BadRequest(ModelState);
            }
            return Ok(products);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductDTO productDTO)
        {
            try
            {
                using (var connection = new SqlConnection(connestingString))
                {
                    connection.Open();

                    string sql = "UPDATE Products SET ProductName=@ProductName, Price=@Price WHERE ProductID=@id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", productDTO.ProductName);
                        command.Parameters.AddWithValue("@Price", productDTO.Price);
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Products", "Извините, произошла ошибка");
                return BadRequest(ModelState);
            }

            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connestingString))
                {
                    connection.Open();

                    string sql = "Delete from Products Where ProductID = @id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Products", "Извините, произошла ошибка");
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
