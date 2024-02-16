using StreamJsonRpc;

namespace Auction_Server.Models
{

    public interface IAuctionOperations
    {
        [JsonRpcMethod("InitiateAuction")]
        void InitiateAuction(string auctionId, string itemName, double startingPrice);

        [JsonRpcMethod("PlaceBid")]
        void PlaceBid(string auctionId, string bidderName, double bidAmount);

        [JsonRpcMethod("ConcludeAuction")]
        void ConcludeAuction(string auctionId, string winnerName, double winningBid);


        [JsonRpcMethod("EnterProduct")]
        void EnterProduct(string productId, string productName);

        [JsonRpcMethod("SetProductPrice")]
        void SetProductPrice(string productId, double price);
    }
}
