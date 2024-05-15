using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PZ_webAPI.Models;

namespace PZ_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly string connestingString;

        public OrdersController(IConfiguration configuration)
        {
            connestingString = configuration["ConnectionStrings:DefaultConnection"] ?? "";
        }

        [HttpPost]
        public IActionResult CreateOrders(OrdersDTO ordersDTO)
        {
            try
            {
                using (var connection = new SqlConnection(connestingString))
                {
                    connection.Open();

                    string sql = "Insert into Orders " +
                        "(UsersID, OrderDate, TotalAmount) Values " +
                        "(@UsersID, @OrderDate, @TotalAmount)";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UsersID", ordersDTO.UserID);
                        command.Parameters.AddWithValue("@OrderDate", ordersDTO.OrderDate);
                        command.Parameters.AddWithValue("@TotalAmount", ordersDTO.TotalAmount);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Orders", "Извините, произошла ошибка");
                return BadRequest(ModelState);
            }
            return Ok();
        }
        [HttpGet]
        public IActionResult GetOrders()
        {
            List<Orders> orders = new List<Orders>();

            try
            {
                using (var connection = new SqlConnection(connestingString))
                {
                    connection.Open();
                    string sql = "Select * from Orders";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Orders order = new Orders();

                                order.OrderID = reader.GetInt32(0);
                                //order.UserID = reader.();
                                //order.OrderDate = reader.GetDateTime(2);
                                order.TotalAmount = reader.GetDecimal(3);

                                orders.Add(order);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Orders", "Извините, произошла ошибка");
                return BadRequest(ModelState);
            }
            return Ok(users);
        }
        [HttpGet("{id}")]
        public IActionResult GetOrders(int id)
        {
            Orders orders = new Orders();
            try
            {
                using (var connection = new SqlConnection(connestingString))
                {
                    connection.Open();

                    string sql = "Select * from Orders Where UserID=@id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                users.UserID = reader.GetInt32(0);
                                users.UserName = reader.GetString(1);
                                users.Email = reader.GetString(2);
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
                ModelState.AddModelError("Orders", "Извините, произошла ошибка");
                return BadRequest(ModelState);
            }
            return Ok(users);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateOrders(int id, UsersDTO usersDTO)
        {
            try
            {
                using (var connection = new SqlConnection(connestingString))
                {
                    connection.Open();

                    string sql = "UPDATE Products SET UserName=@UserName, Email=@Email WHERE UserID=@id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", usersDTO.UserName);
                        command.Parameters.AddWithValue("@Email", usersDTO.Email);
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Orders", "Извините, произошла ошибка");
                return BadRequest(ModelState);
            }

            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteOrders(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connestingString))
                {
                    connection.Open();

                    string sql = "Delete from Orders Where UserID = @id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Orders", "Извините, произошла ошибка");
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}