using API.Exceptions;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace API.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class UsersController(UserService userService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserDto addUserDto)
        {
            try
            {
                var user = await userService.AddUser(addUserDto);
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await userService.GetAll();
                return Ok(users);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto updateUserDto)
        {
            try
            {
                var user = await userService.UpdateUser(updateUserDto);
                return Ok(user);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            try
            {
                var user = await userService.Delete(id);
                return Ok(user);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
