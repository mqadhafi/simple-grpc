using System.Threading.Tasks;
using CreditRatingService;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

namespace CreditRatingClient.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetersController : ControllerBase
    {
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001");
            Greeter.GreeterClient greetingClient = new Greeter.GreeterClient(channel);
            HelloRequest greeterRequest = new HelloRequest { Name = name };
            HelloReply greeterResponse = await greetingClient.SayHelloAsync(greeterRequest);
            return Ok(greeterResponse.Message);
        }
    }
}