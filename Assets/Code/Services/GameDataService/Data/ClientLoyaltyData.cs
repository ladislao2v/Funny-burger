namespace Code.Services.GameDataService.Data
{
    public class ClientLoyaltyData : IData
    {
        public float Loyalty { get; set; }

        public ClientLoyaltyData(float loyalty)
        {
            Loyalty = loyalty;
        }
    }
}