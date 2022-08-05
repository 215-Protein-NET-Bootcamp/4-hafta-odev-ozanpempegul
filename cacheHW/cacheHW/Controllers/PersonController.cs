using AutoMapper;
using cacheHW.Base;
using cacheHW.Data;
using cacheHW.Dto;
using cacheHW.Service;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace cacheHW
{
    [Route("protein/v1/api/[controller]")]
    [ApiController]
    public class PersonController : BaseController<PersonDto, Person>
    {
        private readonly IPersonService personService;

        public PersonController(IPersonService personService, IMapper mapper) : base(personService, mapper)
        {
            this.personService = personService;
        }

        [ResponseCache(CacheProfileName = "Duration45")]
        [HttpGet]
        public async Task<IActionResult> GetPaginationAsync([FromQuery] int page, [FromQuery] int pageSize)
        {
            Log.Information($"{User.Identity?.Name}: get pagination person.");

            QueryResource pagintation = new QueryResource(page, pageSize);

            var result = await personService.GetPaginationAsync(pagintation, null);

            if (!result.Success)
                return BadRequest(result);

            if (result.Response is null)
                return NoContent();

            return Ok(result);
        }
    }
}
