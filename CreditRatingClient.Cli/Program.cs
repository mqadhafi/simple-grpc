using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CreditRatingService;
using Grpc.Net.Client;

namespace CreditRatingClient.Cli
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var serverAddress = "https://localhost:5001";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // The following statement allows you to call insecure services. To be used only in development environments.
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                serverAddress = "http://localhost:5000";
            }

            GrpcChannel channel = GrpcChannel.ForAddress(serverAddress);

            Greeter.GreeterClient greetingClient = new Greeter.GreeterClient(channel);
            HelloRequest greeterRequest = new HelloRequest { Name = "Dhafi" };
            HelloReply greeterResponse = await greetingClient.SayHelloAsync(greeterRequest);
            Console.WriteLine(greeterResponse.Message);

            CreditRatingCheck.CreditRatingCheckClient creditRatingClient = new CreditRatingCheck.CreditRatingCheckClient(channel);
            CreditRequest creditRequest = new CreditRequest { CustomerId = "id0201", Credit = 7000 };
            CreditReply creditRatingResponse = await creditRatingClient.CheckCreditRequestAsync(creditRequest);
            Console.WriteLine($"Credit for customer {creditRequest.CustomerId} {(creditRatingResponse.IsAccepted ? "approved" : "rejected")}!");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}