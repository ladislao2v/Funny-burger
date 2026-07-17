using System;
using Code.Configs;
using Code.Services.ShopService;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Level
{
    public sealed class NextLevelRewardView : MonoBehaviour, INextLevelRewardView
    {
        [SerializeField] private Image _logo;
        
        public void PresentNextReward(RewardConfig item) => 
            _logo.sprite = item.Item.Logo;
    }
}