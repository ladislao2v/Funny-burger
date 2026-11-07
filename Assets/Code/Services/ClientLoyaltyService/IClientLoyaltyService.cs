using System;
using Code.Services.GameDataService;

namespace Code.Services.ClientLoyaltyService
{
    public interface IClientLoyaltyService : ISavable
    {
        float Loyalty { get; }
        
        public event Action<float> LoyaltyChanged;
        
        void IncreaseLoyalty();
        void ReduceLoyalty();
        int GetTip(int orderPrice);
        float GetCookTime(float cookTime);
        float GetClientInterval(float interval);
    }
}