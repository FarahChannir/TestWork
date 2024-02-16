using Auction_Client.Models;
using Auction_Server.Models;
using Microsoft.Extensions.Logging;

class Program
{
    static async Task Main(string[] args)
    {
        var server = new AuctionInitializationServer(new LoggerFactory().CreateLogger<AuctionInitializationServer>());
        _ = server.StartServerAsync();

        // Simulate the auction scenario
        var client1 = new AuctionClient("http://localhost:1981");
        var client2 = new AuctionClient("http://localhost:1981");
        var client3 = new AuctionClient("http://localhost:1981");

        // Enter products
        string productAId = "P1";
        string productBId = "P2";

        client1.EnterProduct(productAId, "Fancy Watch");
        client2.EnterProduct(productBId, "Vintage Painting");

        // Set starting prices for products
        client1.SetProductPrice(productAId, 200);
        client2.SetProductPrice(productBId, 150);

        // Start auctions
        await client1.StartAuctionAsync(productAId, "Auction for Fancy Watch", 200);
        await client2.StartAuctionAsync(productBId, "Auction for Vintage Painting", 150);

        // Place bids
        await client2.PlaceBid(productAId, "ArtCollector#1", 220);
        await client3.PlaceBid(productAId, "ArtCollector#2", 250);
        await client2.PlaceBid(productAId, "ArtCollector#1", 280);

        await client1.PlaceBid(productBId, "WatchEnthusiast#1", 160);
        await client3.PlaceBid(productBId, "ArtCollector#2", 180);
        await client1.PlaceBid(productBId, "WatchEnthusiast#1", 200);

        await Task.Delay(2000); // Adding a delay to allow time for auction conclusion

        string winnerProductA = client1.GetAuctionWinner(productAId);
        string winnerProductB = client2.GetAuctionWinner(productBId);

        // Display the results
        Console.WriteLine($"Winner for {client1.GetProductName(productAId)}: {winnerProductA}");
        Console.WriteLine($"Winner for {client2.GetProductName(productBId)}: {winnerProductB}");

        // Wait for user input to keep the console window open
        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
    }


}
