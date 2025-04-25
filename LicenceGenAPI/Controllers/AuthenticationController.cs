using Asp.Versioning;
using LicenceGenAPI.Data.VO;
using LicenceGenAPI.Models;
using LicenceGenAPI.Rules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LicenceGenAPI.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:ApiVersion}")]
    [ApiController]
    public class AuthenticationController : Controller
    {

        private ILoginRule _loginRule;

        public AuthenticationController(ILoginRule loginRule)
        {
            _loginRule = loginRule;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO objUserVO)
        {
            if (objUserVO == null) return BadRequest("Ivalid client request");
            var token = _loginRule.ValidateCredentials(objUserVO);

            if (token == null) return Unauthorized();

            return Ok(token);
        }

        [HttpPost]
        [Route("Refresh")]
        public IActionResult Refresh([FromBody] TokenVO objTokenVO)
        {
            if (objTokenVO == null) return BadRequest("Ivalid client request");
            var token = _loginRule.ValidateCredentials(objTokenVO);

            if (token == null) return BadRequest("Ivalid client request");

            return Ok(token);
        }

        [HttpGet]
        [Route("Revoke")]
        [Authorize]
        public IActionResult Revoke()
        {
            string? objUserName = User?.Identity?.Name;

            bool result = _loginRule.RevokeToken(objUserName);

            if (!result) return BadRequest("Ivalid client request");

            return NoContent();
        }
    }
}
