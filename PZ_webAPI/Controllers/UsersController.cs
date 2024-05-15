using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PZ_webAPI.Models;

namespace PZ_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly string connestingString;

        public UsersController(IConfiguration configuration)
        {
            connestingString = configuration["ConnectionStrings:DefaultConnection"] ?? "";
        }

        [HttpPost]
        public IActionResult CreateUsers(UsersDTO usersDTO)
        {
            try
            {
                using (var connection = new SqlConnection(connestingString))
                {
                    connection.Open();

                    string sql = "Insert into Users " +
                        "(UserName, Email) Values " +
                        "(@UserName, @Email)";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", usersDTO.UserName);
                        command.Parameters.AddWithValue("@Email", usersDTO.Email);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Users", "Извините, произошла ошибка");
                return BadRequest(ModelState);
            }
            return Ok();
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            List<Users> users = new List<Users>();

            try
            {
                using (var connection = new SqlConnection(connestingString))
                {
                    connection.Open();
                    string sql = "Select * from Users";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Users user = new Users();

                                user.UserID = reader.GetInt32(0);
                                user.UserName = reader.GetString(1);
                                user.Email = reader.GetString(2);

                                users.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Users", "Извините, произошла ошибка");
                return BadRequest(ModelState);
            }
            return Ok(users);
        }
        [HttpGet("{id}")]
        public IActionResult GetUsers(int id)
        {
            Users users = new Users();
            try
            {
                using (var connection = new SqlConnection(connestingString))
                {
                    connection.Open();

                    string sql = "Select * from Users Where UserID=@id";

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
                ModelState.AddModelError("Users", "Извините, произошла ошибка");
                return BadRequest(ModelState);
            }
            return Ok(users);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUsers(int id, UsersDTO usersDTO)
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
                ModelState.AddModelError("Users", "Извините, произошла ошибка");
                return BadRequest(ModelState);
            }

            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUsers(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connestingString))
                {
                    connection.Open();

                    string sql = "Delete from Users Where UserID = @id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Users", "Извините, произошла ошибка");
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
