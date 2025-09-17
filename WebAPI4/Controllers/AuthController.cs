using Business.Abstract;
using Entities.Concrete.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            try
            {
                var userExists = _authService.UserExists(userForRegisterDto.Email);
                if (!userExists.Success)
                {
                    return BadRequest(userExists.Message);
                }

                var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
                var result = _authService.CreateAccessToken(registerResult.Data);
                if (result.Success)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result.Message);
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine(ex.Message);

                var inner = ex.InnerException;
                while (inner != null)
                {
                    sb.AppendLine(inner.Message);
                    inner = inner.InnerException;
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = sb.ToString()
                });



                //var innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                //return BadRequest(new
                //{
                //    Success = false,
                //    Message = "DbUpdateException: " + innerMessage
                //});
            }



            
        }
    }
}
