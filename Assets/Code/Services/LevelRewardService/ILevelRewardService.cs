using System;
using Code.Services.ShopService;

namespace Code.Services.LevelRewardService
{
    public interface ILevelRewardService
    {
        IItem NextReward { get; }
        event Action<IItem> RewardGot;
        event Action<IItem> RewardUpdated;
    }
}