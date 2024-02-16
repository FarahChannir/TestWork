using System.Collections.Concurrent;
using System.Net.Sockets;
using StreamJsonRpc;

namespace Auction_Client.Models
{

    public class AuctionClient : IAuctionOperations
    {
        private readonly JsonRpc rpc;
        private readonly ManualResetEventSlim connectedEvent = new ManualResetEventSlim(false);
        private readonly ConcurrentDictionary<string, double> productBids = new ConcurrentDictionary<string, double>();
        private readonly ConcurrentDictionary<string, string> productNames = new ConcurrentDictionary<string, string>();
        private readonly ConcurrentDictionary<string, string> auctionWinners = new ConcurrentDictionary<string, string>();

        public AuctionClient(string serverUrl)
        {
            var tcpClient = new TcpClient();
            tcpClient.Connect(new Uri(serverUrl).Host, 1981);
            var stream = tcpClient.GetStream();

            this.rpc = new JsonRpc(stream);

            // Start listening for messages in a separate task
            Task.Run(() => ListenForMessages());
        }

        // Listen for incoming messages from the server
        private async Task ListenForMessages()
        {
            try
            {
                // The JsonRpc.StartListening method must be called before using the JsonRpc
                rpc.StartListening();

                // Indicate that the connection is established
                connectedEvent.Set();

                await rpc.Completion;
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
                Console.WriteLine($"Exception in ListenForMessages: {ex.Message}");
            }
        }

        // Start a new auction
        public async Task StartAuctionAsync(string auctionId, string itemName, double startingPrice)
        {
            connectedEvent.Wait();

            if (rpc.Completion.IsCompleted)
            {
                Console.WriteLine("Error: JsonRpc is not in a state to handle invocations. Cannot invoke method.");
                return;
            }

            await rpc.InvokeAsync("InitiateAuction", auctionId, itemName, startingPrice);
        }

        // Place a bid in an ongoing auction
        public async Task PlaceBid(string auctionId, string bidderName, double bidAmount)
        {
            connectedEvent.Wait();

            await rpc.InvokeAsync("PlaceBid", auctionId, bidderName, bidAmount);
        }

        // Receive bid updates from the server
        public void ReceiveBid(string auctionId, string bidderName, double bidAmount)
        {
            Console.WriteLine($"Received bid on Auction ID: {auctionId} by {bidderName} for {bidAmount} USD");
        }

        // Handle the conclusion of an auction
        public void AuctionConclusion(string auctionId, string winnerName, double winningBid)
        {
            Console.WriteLine($"Auction ID: {auctionId} concluded. Winner: {winnerName}, Winning Bid: {winningBid} USD");

            auctionWinners.AddOrUpdate(auctionId, winnerName, (key, oldValue) => winnerName);
        }

        // Get the winner of a concluded auction
        public string GetAuctionWinner(string auctionId)
        {
            return auctionWinners.TryGetValue(auctionId, out var winner) ? winner : "No Winner";
        }

        // Initiate a new auction
        public void InitiateAuction(string auctionId, string itemName, double startingPrice)
        {
            Console.WriteLine($"Auction ID: {auctionId} initiated for {itemName} at {startingPrice} USD");
        }

        // Enter a new product into the auction
        public void EnterProduct(string productId, string productName)
        {
            Console.WriteLine($"Entered product: {productName} (ID: {productId})");

            productNames.AddOrUpdate(productId, productName, (key, oldValue) => productName);
        }

        // Get the name of a product by its ID
        public string GetProductName(string productId)
        {
            return productNames.TryGetValue(productId, out var name) ? name : "";
        }

        // Set the price for a product
        public void SetProductPrice(string productId, double price)
        {
            rpc.InvokeAsync("NotifyProductPrice", productId, price);
        }

        // Get the bid amount for a specific auction
        public double GetBidAmount(string auctionId)
        {
            return productBids.TryGetValue(auctionId, out var bidAmount) ? bidAmount : 0;
        }

        // Receive general updates from the server
        public void ReceiveUpdate(string updateType, string productId, object data)
        {
            // Handle different types of updates
            switch (updateType)
            {
                case "ProductAdded":
                    HandleProductAdded(productId, (string)data);
                    break;
                case "PriceSet":
                    HandlePriceSet(productId, (double)data);
                    break;
                // Add more cases for other types of updates if needed
                default:
                    Console.WriteLine($"Unhandled update type: {updateType}");
                    break;
            }
        }

        // Handle the addition of a new product
        private void HandleProductAdded(string productId, string productName)
        {
            Console.WriteLine($"Received product addition: {productName} (ID: {productId})");
        }

        // Handle a change in the price of a product
        private void HandlePriceSet(string productId, double price)
        {
            Console.WriteLine($"Received price update for product (ID: {productId}): {price} USD");
        }
    }




}
