using DevGardenAPI;
using DevGardenAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Reflection;

namespace DevGardenAPI.Controllers
{
    [Route("api/devgarden")]
    [ApiController]
    public abstract class DevGardenController<T> : ControllerBase
    {
        public DevGardenController()
        {
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<T>>? GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<T>? Get(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<T> Post(T entity)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<T>? Put(T entity)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}

[Route("api/devgardencontroller")]
[ApiController]
public class DevGardenController : ControllerBase
{

    public DevGardenController()
    {
    }

    [HttpPost("")]
    public async Task<IActionResult> LouBrodaEstNull(string loubrobro)
    {
        throw new NotImplementedException();
    }

}