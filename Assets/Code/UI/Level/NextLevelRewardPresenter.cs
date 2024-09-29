using Code.Services.LevelRewardService;
using UnityEngine;
using Zenject;

namespace Code.UI.Level
{
    public sealed class NextLevelRewardPresenter : MonoBehaviour
    {
        private ILevelRewardService _levelRewardService;
        private INextLevelRewardView _levelRewardView;

        [Inject]
        private void Construct(ILevelRewardService levelRewardService)
        {
            _levelRewardService = levelRewardService;
            _levelRewardView = GetComponent<INextLevelRewardView>();
            
            _levelRewardView.PresentNextReward(_levelRewardService.NextReward);
        }

        private void OnEnable() => 
            _levelRewardService.RewardUpdated += _levelRewardView.PresentNextReward;

        private void OnDisable() => 
            _levelRewardService.RewardUpdated -= _levelRewardView.PresentNextReward;
    }
}