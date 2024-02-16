using StreamJsonRpc;

namespace Auction_Client.Models
{
    public interface IAuctionOperations
    {
        [JsonRpcMethod("initiateAuction")]
        void InitiateAuction(string auctionId, string itemName, double startingPrice);

        
        [JsonRpcMethod("PlaceBid")]
        Task PlaceBid(string auctionId, string bidderName, double bidAmount);

        [JsonRpcMethod("ReceiveBid")]
        void ReceiveBid(string auctionId, string bidderName, double bidAmount);

       

        [JsonRpcMethod("AuctionConclusion")]
        void AuctionConclusion(string auctionId, string winnerName, double winningBid);

       
        [JsonRpcMethod("EnterProduct")]
        void EnterProduct(string productId, string productName);

        [JsonRpcMethod("SetProductPrice")]
        void SetProductPrice(string productId, double price);

    }
}
