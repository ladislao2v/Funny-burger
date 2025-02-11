using System;
using System.Collections.Generic;
using System.Linq;
using Code.Services.ConfigProvider;
using Code.Services.Factories.ItemVisitorFactory;
using Code.Services.LevelService;
using Code.Services.ShopService;
using UnityEngine;
using Zenject;

namespace Code.Services.LevelRewardService
{
    public class LevelRewardService : IInitializable, IDisposable, ILevelRewardService
    {
        private readonly ILevelService _levelService;
        private readonly IItemVisitorFactory _itemVisitorFactory;
        private readonly IEnumerable<IItem> _rewards;
        public IItem NextReward { get; private set; }

        public event Action<IItem> RewardGot;
        public event Action<IItem> RewardUpdated;

        public LevelRewardService(ILevelService levelService, IItemVisitorFactory itemVisitorFactory, 
            IConfigProvider configProvider)
        {
            _levelService = levelService;
            _itemVisitorFactory = itemVisitorFactory;

            IEnumerable<IItem> recipes = configProvider.GetRecipes();
            IEnumerable<IItem> gems = configProvider.GetGems();
            
            _rewards = new List<IItem>(recipes)
                .Concat(gems)
                .OrderBy(x =>x.RequiredLevel);

            NextReward = PickNextReward(_levelService.Next);
        }

        public void Initialize() => 
            _levelService.LevelChanged += OnLevelChanged;

        public void Dispose() => 
            _levelService.LevelChanged -= OnLevelChanged;

        private void OnLevelChanged(int current, int next)
        {
            RewardGot?.Invoke(NextReward);

            GiveReward();
            NextReward = PickNextReward(next);
            
            RewardUpdated?.Invoke(NextReward);
        }

        private void GiveReward()
        {
            IItemVisitor itemVisitor = _itemVisitorFactory
                .CreateVisitor<RewardGiverVisitor>();

            NextReward.Accept(itemVisitor);
        }

        private IItem PickNextReward(int next)
        {
            IItem reward = _rewards
                .FirstOrDefault(x => x.RequiredLevel == next);
            
            Debug.Log(reward?.Name);

            if (reward == null)
                return NextReward;
            
            return reward;
        }
    }
}