using System;
using Code.Configs;
using Code.Services.ShopService;

namespace Code.Services.LevelRewardService
{
    public interface ILevelRewardService
    {
        RewardConfig NextReward { get; }
        RewardConfig PreviousReward { get; }
        event Action<RewardConfig> RewardGot;
        event Action<RewardConfig> RewardUpdated;
        void RefreshNextReward();
    }
}