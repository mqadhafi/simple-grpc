using System.Threading.Tasks;
using CreditRatingService;
using Microsoft.AspNetCore.Mvc;

namespace CreditRatingClient.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetersController : ControllerBase
    {
        private readonly Greeter.GreeterClient _client;

        public GreetersController(Greeter.GreeterClient client)
        {
            _client = client;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            HelloReply greeterResponse = await _client.SayHelloAsync(new HelloRequest { Name = name });
            return Ok(greeterResponse.Message);
        }
    }
}