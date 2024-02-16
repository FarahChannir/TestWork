using Microsoft.Extensions.Logging;
using StreamJsonRpc;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
namespace Auction_Server.Models
{
    public class AuctionServer : IAuctionOperations
    {
        private readonly ILogger<AuctionServer> logger;
        private readonly List<JsonRpc> connectedClients = new List<JsonRpc>();
        private readonly ConcurrentDictionary<string, double> productPrices = new ConcurrentDictionary<string, double>();
        private readonly ConcurrentDictionary<string, string> productNames = new ConcurrentDictionary<string, string>();
        private readonly ConcurrentDictionary<string, string> productAuctionWinners = new ConcurrentDictionary<string, string>();

        public AuctionServer(ILogger<AuctionServer> logger)
        {
            this.logger = logger;
        }

        public void InitiateAuction(string auctionId, string itemName, double startingPrice)
        {
            logger.LogInformation($"Auction ID: {auctionId} started for {itemName} at {startingPrice} USD");

            AddProduct(auctionId, itemName);
        }

        public void PlaceBid(string auctionId, string bidderName, double bidAmount)
        {
            Console.WriteLine($"Bid placed on Auction ID: {auctionId} by {bidderName} for {bidAmount} USD");

            BroadcastBidToClients(auctionId, bidderName, bidAmount);
        }

        public async Task StartServerAsync()
        {
            var listener = new TcpListener(IPAddress.Loopback, 1981);
            listener.Start();

            Console.WriteLine("Server listening on port 1981...");

            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                var stream = client.GetStream();

                var rpc = JsonRpc.Attach(stream, this);
                connectedClients.Add(rpc);

                _ = Task.Run(() => HandleClientAsync(rpc));
            }
        }

        public async Task HandleClientAsync(JsonRpc client)
        {
            try
            {
                await client.Completion;
            }
            catch (Exception ex) when (ex is OperationCanceledException)
            {
                Console.WriteLine($"Client disconnected: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in HandleClientAsync: {ex.Message}");
            }
            finally
            {
                connectedClients.Remove(client);
            }
        }

        public void ConcludeAuction(string auctionId, string winnerName, double winningBid)
        {
            Console.WriteLine($"Auction ID: {auctionId} concluded. Winner: {winnerName}, Winning Bid: {winningBid} USD");

            NotifyClientsAuctionConclusion(auctionId, winnerName, winningBid);

            productAuctionWinners.AddOrUpdate(auctionId, winnerName, (key, oldValue) => winnerName);
        }

        public void EnterProduct(string productId, string productName)
        {
            Console.WriteLine($"Client entered product: {productName} (ID: {productId})");

            productNames.AddOrUpdate(productId, productName, (key, oldValue) => productName);
        }

        public string GetProductName(string productId)
        {
            return productNames.TryGetValue(productId, out var name) ? name : "";
        }

        public double GetProductPrice(string productId)
        {
            return productPrices.TryGetValue(productId, out var price) ? price : 0;
        }

        public void AddProduct(string productId, string productName)
        {
            Console.WriteLine($"Client added product: {productName} (ID: {productId})");

            productNames.AddOrUpdate(productId, productName, (key, oldValue) => productName);

            BroadcastUpdateToClients("ProductAdded", productId, productName);
        }

        public void SetProductPrice(string productId, double price)
        {
            Console.WriteLine($"Client set price for product (ID: {productId}): {price} USD");

            productPrices.AddOrUpdate(productId, price, (key, oldValue) => price);

            BroadcastUpdateToClients("PriceSet", productId, price);
        }

        private void BroadcastUpdateToClients(string updateType, string productId, object data)
        {
            foreach (var client in connectedClients)
            {
                try
                {
                    client.NotifyAsync("ReceiveUpdate", updateType, productId, data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to notify client: {ex.Message}");
                }
            }
        }

        private void NotifyClientsAuctionConclusion(string auctionId, string winnerName, double winningBid)
        {
            foreach (var client in connectedClients)
            {
                try
                {
                    client.NotifyAsync("AuctionConclusion", auctionId, winnerName, winningBid);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to notify client: {ex.Message}");
                }
            }
        }

        private void BroadcastBidToClients(string auctionId, string bidderName, double bidAmount)
        {
            foreach (var client in connectedClients)
            {
                try
                {
                    client.NotifyAsync("ReceiveBid", auctionId, bidderName, bidAmount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to notify client: {ex.Message}");
                }
            }
        }
    }

}
