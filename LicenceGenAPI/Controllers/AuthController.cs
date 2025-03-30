using Asp.Versioning;
using LicenceGenAPI.Data.VO;
using LicenceGenAPI.Rules.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LicenceGenAPI.Controllers
{
    [ApiVersion("1")]
    [Route("api[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : Controller
    {
        private ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Singnin([FromBody] UserVO objUserVO)
        {
            if (objUserVO == null) return BadRequest("Invalid client request");

            var objToken = _loginService.ValidateCredentials(objUserVO);

            if ((objToken == null)) return Unauthorized();

            return Ok(objToken);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVo)
        {
            if (tokenVo is null) return BadRequest("Ivalid client request");
            var token = _loginService.ValidateCredentials(tokenVo);
            if (token == null) return BadRequest("Ivalid client request");
            return Ok(token);
        }


        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var username = User?.Identity?.Name;
            var result = _loginService.RevokeToken(username!);

            if (!result) return BadRequest("Ivalid client request");
            return NoContent();
        }
    }
}