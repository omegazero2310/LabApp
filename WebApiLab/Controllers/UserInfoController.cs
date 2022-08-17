using CommonClass.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebApiLab.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private IServiceProvider _serviceProvider;
        private UserInfoService _userInfoService;
        public UserInfoController(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            this._userInfoService = new UserInfoService(this._serviceProvider);
        }
        // GET: api/<UserInfoController>
        [HttpGet]
        public Task<IEnumerable<UserInfo>> Get()
        {
            return this._userInfoService.Gets();
        }

        // GET api/<UserInfoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserInfoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserInfoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserInfoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
