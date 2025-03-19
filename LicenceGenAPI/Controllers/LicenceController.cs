using Asp.Versioning;
using LicenceGenAPI.DbConnection;
using LicenceGenAPI.Models;
using LicenceGenAPI.Rules.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace LicenceGenAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class LicenceController : ControllerBase
    {
        private readonly ILogger<LicenceController> _logger;
        private readonly ILicenceRuleService _ruleService;

        public LicenceController(ILogger<LicenceController> logger, ILicenceRuleService ruleService)
        {
            _logger = logger;
            _ruleService = ruleService;
        }

        [HttpGet("all")]
        public IActionResult Get()
        {
            return Ok(_ruleService.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var objFound = _ruleService.FindById(id);

            if (objFound == null)
                return NotFound();

            return Ok(objFound);
        }

        [HttpPost]
        public IActionResult Post([FromBody] LicenceModel objModel)
        {
            var objFound = _ruleService.Create(objModel);

            if (objModel == null)
                return BadRequest();

            return Ok(objFound);
        }

        [HttpPut]
        public IActionResult Put([FromBody] LicenceModel objModel)
        {
            var objFound = _ruleService.Update(objModel);

            if (objModel == null)
                return BadRequest();

            return Ok(objFound);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _ruleService.Delete(id);
        }
    }
}