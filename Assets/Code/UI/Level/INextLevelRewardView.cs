using Code.Configs;
using Code.Services.ShopService;

namespace Code.UI.Level
{
    public interface INextLevelRewardView
    {
        void PresentNextReward(RewardConfig item);
    }
}