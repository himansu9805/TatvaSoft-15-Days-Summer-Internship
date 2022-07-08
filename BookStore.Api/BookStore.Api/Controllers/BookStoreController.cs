using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        private readonly UserRepository _userRepository = new ();

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            var user = _userRepository.Login(model);
            if (user == null)
            {
                return Unauthorized(new { message = "Incorrect email or password!" });
            }
            UserModel response = new (user);
            return Ok(response);
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterModel model)
        {
            User user = _userRepository.Register(model);
            if (user == null)
            {
                return Conflict(new { message = "User with this email address already exists!" });
            }
            return Ok(user);
        }
    }
}
