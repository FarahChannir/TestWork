using Auction_Client.Models;

internal class Program
{

        private static async Task Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: Client.exe <ClientName>");
                return;
            }

            var clientName = args[0];
            var serverUrl = "http://localhost:1981";

            var client = new AuctionClient(serverUrl);

            Console.WriteLine($"Welcome, {clientName}!");

            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Start a new auction");
                Console.WriteLine("2. Place a bid");
                Console.WriteLine("3. Exit");

                if (!int.TryParse(Console.ReadLine(), out var choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        await StartAuction(client, clientName);
                        break;
                    case 2:
                        await PlaceBid(client, clientName);
                        break;
                    case 3:
                        return; // Exit the application
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
        }

        private static async Task StartAuction(AuctionClient client, string clientName)
        {
            Console.WriteLine("Enter the product ID for your auction:");
            var productId = Console.ReadLine();

            Console.WriteLine("Enter the product name:");
            var productName = Console.ReadLine();

            Console.WriteLine("Enter the starting price:");
            if (!double.TryParse(Console.ReadLine(), out var startingPrice))
            {
                Console.WriteLine("Invalid price. Auction setup canceled.");
                return;
            }

            // Initiate the auction
            await client.StartAuctionAsync($"Auction_{clientName}", productName, startingPrice);

            Console.WriteLine($"Auction started for {productName} at {startingPrice} USD");
        }

        private static async Task PlaceBid(AuctionClient client, string clientName)
        {
            Console.WriteLine("Enter the auction ID:");
            var auctionId = Console.ReadLine();

            Console.WriteLine("Enter your bid amount:");
            if (!double.TryParse(Console.ReadLine(), out var bidAmount))
            {
                Console.WriteLine("Invalid bid amount. Bid canceled.");
                return;
            }

            // Place a bid
            await client.PlaceBid(auctionId, $"Bidder_{clientName}", bidAmount);

            Console.WriteLine($"Bid placed on Auction ID: {auctionId} by {clientName} for {bidAmount} USD");
        }
    }




//string serverUrl = "http://localhost:1981";

//AuctionClient auctionClient = new AuctionClient(serverUrl);
//Console.WriteLine("Auction Initialization:");

//// Example auction initiation
//await auctionClient.StartAuctionAsync("1", "Pic#1", 75);

//// You can add more auction initiations here

//Console.ReadLine(); // Keep console window open for testing
