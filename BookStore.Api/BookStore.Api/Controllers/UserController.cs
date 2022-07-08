using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        UserRepository _userRepository = new UserRepository();

        [HttpGet("list")]
        public IActionResult GetUsers(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            var users = _userRepository.GetUsers(pageIndex, pageSize, keyword);
            ListResponse<UserModel> listResponse = new ListResponse<UserModel>()
            {
                records = users.records.Select(c => new UserModel(c)).ToList(),
                totalRecords = users.totalRecords
            };

            return Ok(listResponse);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id = 0)
        {
            User user = _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found!" });
            }
            UserModel response = new UserModel(user);
            return Ok(response);
        }

        [HttpPut("update")]
        public IActionResult UpdateUser(UserModel model)
        {
            if (model != null)
                return BadRequest();

            User user = new User()
            {
                Id = model.Id,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Email = model.Email,
                Roleid = model.Roleid,
            };

            user = _userRepository.UpdateUser(user);
            return Ok(user);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var response = _userRepository.DeleteUser(id);
            if (response == false)
            {
                return NotFound(new { message = "User not found!" });
            }
            return Ok(new { message = "User removed successfully" });
        }

        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            var roles = _userRepository.GetRoles();
            ListResponse<RoleModel> listResponse = new ListResponse<RoleModel>()
            {
                records = roles.records.Select(c => new RoleModel(c)).ToList(),
                totalRecords = roles.totalRecords
            };

            return Ok(listResponse);
        }
    }
}
