using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteMonitoring.Core.Entities.ModelDtos;
using SiteMonitoring.Core.Entities.Models;
using SiteMonitoring.Core.Security;
using SiteMonitoring.Core.Web.Requests;
using SiteMonitoring.Core.Web.Responses;
using SiteMonitoring.Data;

namespace SiteMonitoring.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private DataContext _dataContext;
        private AuthOptions _authOptions;

        public AuthController(DataContext dataContext, AuthOptions authOptions)
        {
            _dataContext = dataContext;
            _authOptions = authOptions;
        }

        [HttpGet("login")]
        public IActionResult Login([FromBody]LoginRequest request)
        {
            var user = _dataContext.Users.Where(u => u.Name == request.UserName && u.Password == request.Password).SingleOrDefault();
            if (user == null)
                return Unauthorized("User not found");
            
            var token = new JwtAuthService().GetEncodedToken(_authOptions, user);

            return Ok(new LoginResponse()
            {
                Token = token
            });
        }

        [Authorize(Roles = "admin")]
        [HttpGet("users/{userName}")]
        public IActionResult GetUser(string userName)
        {
            var user = _dataContext.Users.Where(u => u.Name == userName).Select(u => new UserDto()
            {
                Id = u.Id,
                Name = u.Name,
                Role = u.Role
            });

            return Ok(user);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("users")]
        public IActionResult CreateUser([FromBody]User user)
        {
            var userIsExists = _dataContext.Users.Where(u => u.Name == user.Name && u.Password == user.Password).Any();
            if (userIsExists)
                return Conflict();

            _dataContext.Users.Add(user);
            _dataContext.SaveChanges();

            return Created("/user", user);
        }
    }
}
