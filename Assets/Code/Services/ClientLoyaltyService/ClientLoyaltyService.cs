using System;
using Code.Services.GameDataService;
using Code.Services.GameDataService.Data;
using UnityEngine;

namespace Code.Services.ClientLoyaltyService
{
    public class ClientLoyaltyService : IClientLoyaltyService
    {
        private const float StartLoyalty = 0.5f;
        private const float MinLoyalty = 0f;
        private const float MaxLoyalty = 1f;
        private const float LoyaltyIncreaseModifier = 0.05f;
        private const float LoyaltyReduceModifier = 0.1f;
        private const float NeutralLoyalty = 0.5f;
        private const float MaxTip = 0.2f;
        private const float MaxCookTimePenalty = 0.2f;
        private const float MaxClientIntervalMultiplier = 8f;
        
        public float Loyalty { get; private set; } = StartLoyalty;
        
        public event Action<float> LoyaltyChanged;

        public string SaveKey => nameof(ClientLoyaltyService);

        public void IncreaseLoyalty()
        {
            Loyalty = Mathf.Clamp(Loyalty + LoyaltyIncreaseModifier, MinLoyalty, MaxLoyalty);
            
            LoyaltyChanged?.Invoke(Loyalty);
        }

        public void ReduceLoyalty()
        {
            Loyalty = Mathf.Clamp(Loyalty - LoyaltyReduceModifier, MinLoyalty, MaxLoyalty);
            
            LoyaltyChanged?.Invoke(Loyalty);
        }

        public int GetTip(int orderPrice)
        {
            if (Loyalty <= NeutralLoyalty)
                return 0;

            float tip = Mathf.InverseLerp(NeutralLoyalty, MaxLoyalty, Loyalty) * MaxTip;
            
            return RoundToUnits(orderPrice * tip);
        }

        public float GetCookTime(float cookTime)
        {
            if (Loyalty >= NeutralLoyalty)
                return cookTime;

            float penalty = Mathf.InverseLerp(NeutralLoyalty, MinLoyalty, Loyalty) * MaxCookTimePenalty;
            
            return cookTime - RoundToUnits(cookTime * penalty);
        }

        public float GetClientInterval(float interval)
        {
            if (Loyalty >= NeutralLoyalty)
                return interval;

            float penalty = Mathf.InverseLerp(NeutralLoyalty, MinLoyalty, Loyalty);
            
            return interval * Mathf.Lerp(1f, MaxClientIntervalMultiplier, penalty);
        }

        public void Load(IData data)
        {
            if (data == null)
                data = new ClientLoyaltyData(StartLoyalty);
            
            if (data is not ClientLoyaltyData clientLoyaltyData)
                throw new ArgumentException(nameof(data));

            Loyalty = clientLoyaltyData.Loyalty;
            
            LoyaltyChanged?.Invoke(Loyalty);
        }

        public IData Save() => 
            new ClientLoyaltyData(Loyalty);

        private static int RoundToUnits(float value) => 
            (int)Math.Round(value, MidpointRounding.AwayFromZero);
    }
}