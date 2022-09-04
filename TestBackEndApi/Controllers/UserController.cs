using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestBackEndApi.Models.ViewModels.ListUserViewModel;
using TestBackEndApi.Services;
using TestBackEndApi.Services.Repository;

namespace TestBackEndApi.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class UserController : ControllerBase
    {
        private readonly UserServicesRepositoryImp _userServicesImp;
        public UserController(UserServicesRepositoryImp userServicesImp)
        {
            _userServicesImp = userServicesImp;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateAsync([Bind("Username,Password")][FromBody] ListUserViewModel model)
        {
            try
            {
                var user = await _userServicesImp.AuthenticateAsync(model.Username, model.Password);

                if (user == null)
                    return BadRequest(new { message = "Usuário ou senha inválidos" });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("login")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsersAsync()
        {
            try
            {
                var users = await _userServicesImp.GetUsersAsync();
                foreach (var item in users)
                {
                    item.Password = Encrypt.DecodePassword(item.Password);
                }

                if (users == null)
                    return BadRequest(new { message = "Usuário não encontrado" });

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

}
