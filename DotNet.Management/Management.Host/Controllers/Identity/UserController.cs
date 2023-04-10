using Management.Application;
using Management.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Management.Host.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<PageResultDto<UserDto>>> Get(QueryUsersDto queryDto)
        {
            return await _userAppService.GetListAsync(queryDto);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto?>> Get(long id)
        {
            return await _userAppService.GetAsync(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<UserDto?>> Post([FromBody] CreateUserInputDto createDto)
        {
            return await _userAppService.CreateAsync(createDto);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreateUserInputDto createDto)
        {
             await _userAppService.UpdateAsync(id, createDto);
            return NoContent();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _userAppService.DeleteAsync(id);
        }
    }
}
