using Code.Configs;
using Code.Services.LevelRewardService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Buttons
{
    public class RewardButtonPresenter : MonoBehaviour
    {
        [SerializeField] private RewardConfig config;
        
        private ILevelRewardService _levelRewardService;
        private Button _button;

        [Inject]
        private void Construct(ILevelRewardService levelRewardService)
        {
            _levelRewardService = levelRewardService;
            _button = GetComponent<Button>();
            ActivateButton(_levelRewardService.PreviousReward);
        }

        private void OnEnable() => 
            _levelRewardService.RewardGot += ActivateButton;

        private void OnDisable() => 
            _levelRewardService.RewardGot -= ActivateButton;

        private void ActivateButton(RewardConfig rewardConfig)
        {
            if (rewardConfig == null || config == null)
                return;

            if (rewardConfig.RequiredLevel >= config.RequiredLevel)
                _button.interactable = true;
        }
    }
}