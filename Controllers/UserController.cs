using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UserManagementAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private static List<User> users = new List<User>();
        private static int nextId = 1;

        // GET: api/user
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            try
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return NotFound(new { message = "Usuario no encontrado" });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        // POST: api/usery
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest(new { message = "Usuario no puede ser nulo" });
                }

                // Validar propiedades no nulas o vacías
                if (string.IsNullOrWhiteSpace(user.Name))
                {
                    return BadRequest(new { message = "El nombre no puede estar vacío" });
                }

                if (string.IsNullOrWhiteSpace(user.LastName))
                {
                    return BadRequest(new { message = "El apellido no puede estar vacío" });
                }

                if (string.IsNullOrWhiteSpace(user.Username))
                {
                    return BadRequest(new { message = "El nombre de usuario no puede estar vacío" });
                }

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    return BadRequest(new { message = "El correo electrónico no puede estar vacío" });
                }

                // Validar formato de correo electrónico
                try
                {
                    var addr = new System.Net.Mail.MailAddress(user.Email);
                    if (addr.Address != user.Email)
                    {
                        return BadRequest(new { message = "El formato del correo electrónico no es válido" });
                    }
                }
                catch
                {
                    return BadRequest(new { message = "El formato del correo electrónico no es válido" });
                }

                if (string.IsNullOrWhiteSpace(user.Position))
                {
                    return BadRequest(new { message = "La posición no puede estar vacía" });
                }

                if (string.IsNullOrWhiteSpace(user.Phone))
                {
                    return BadRequest(new { message = "El teléfono no puede estar vacío" });
                }

                user.Id = nextId++;
                user.CreateDate = DateTime.UtcNow;
                user.UpdateDate = DateTime.UtcNow;
                user.Active = true;
                users.Add(user);
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                var existingUser = users.FirstOrDefault(u => u.Id == id);
                if (existingUser == null)
                {
                    return NotFound(new { message = "Usuario no encontrado" });
                }

                if (user == null)
                {
                    return BadRequest(new { message = "Usuario no puede ser nulo" });
                }

                // Validar propiedades no nulas o vacías
                if (string.IsNullOrWhiteSpace(user.Name))
                {
                    return BadRequest(new { message = "El nombre no puede estar vacío" });
                }

                if (string.IsNullOrWhiteSpace(user.LastName))
                {
                    return BadRequest(new { message = "El apellido no puede estar vacío" });
                }

                if (string.IsNullOrWhiteSpace(user.Username))
                {
                    return BadRequest(new { message = "El nombre de usuario no puede estar vacío" });
                }

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    return BadRequest(new { message = "El correo electrónico no puede estar vacío" });
                }

                // Validar formato de correo electrónico
                try
                {
                    var addr = new System.Net.Mail.MailAddress(user.Email);
                    if (addr.Address != user.Email)
                    {
                        return BadRequest(new { message = "El formato del correo electrónico no es válido" });
                    }
                }
                catch
                {
                    return BadRequest(new { message = "El formato del correo electrónico no es válido" });
                }

                if (string.IsNullOrWhiteSpace(user.Position))
                {
                    return BadRequest(new { message = "La posición no puede estar vacía" });
                }

                if (string.IsNullOrWhiteSpace(user.Phone))
                {
                    return BadRequest(new { message = "El teléfono no puede estar vacío" });
                }

                existingUser.Name = user.Name;
                existingUser.LastName = user.LastName;
                existingUser.Username = user.Username;
                existingUser.Email = user.Email;
                existingUser.Position = user.Position;
                existingUser.Phone = user.Phone;
                existingUser.UpdateDate = DateTime.UtcNow;

                return Ok(existingUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return NotFound(new { message = "Usuario no encontrado" });
                }

                users.Remove(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }
    }
}
