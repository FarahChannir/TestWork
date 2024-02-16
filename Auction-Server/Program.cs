using Auction_Server.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

class Program
{
    private static async Task Main(string[] args)
    {
        var server = new AuctionServer(new LoggerFactory().CreateLogger<AuctionServer>());

        Console.WriteLine("Server is running. Press Enter to exit.");

        // Start the server asynchronously
        await server.StartServerAsync();

        // Keep the application running until Enter is pressed
        Console.ReadLine();
    }
}
