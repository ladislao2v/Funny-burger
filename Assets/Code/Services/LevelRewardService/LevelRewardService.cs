using System;
using System.Collections.Generic;
using Code.Configs;
using Code.Configs.ItemVisitors;
using Code.Constants;
using Code.Services.ConfigProvider;
using Code.Services.Factories.ItemVisitorFactory;
using Code.Services.LevelService;
using Zenject;

namespace Code.Services.LevelRewardService
{
    public class LevelRewardService : IInitializable, IDisposable, ILevelRewardService
    {
        private readonly ILevelService _levelService;
        private readonly IItemVisitor _itemVisitor;
        private readonly IDictionary<int, RewardConfig> _rewardsByLevel;
        public RewardConfig NextReward { get; private set; }
        public RewardConfig PreviousReward { get; private set; }

        public event Action<RewardConfig> RewardGot;
        public event Action<RewardConfig> RewardUpdated;

        public LevelRewardService(ILevelService levelService, IItemVisitorFactory itemVisitorFactory, 
            IConfigProvider configProvider)
        {
            _levelService = levelService;;
            _itemVisitor = itemVisitorFactory.CreateVisitor<ItemGiverVisitor>();

            _rewardsByLevel = configProvider.RewardsConfig.RewardsByLevel;
        }

        public void Initialize()
        {
            RefreshNextReward();
            
            var initialReward = PickRewardByLevel(GameplayConstants.InitialLevel).Item;
            
            if(_levelService.Current == GameplayConstants.InitialLevel)
                GiveReward(initialReward);
            
            _levelService.LevelChanged += OnLevelChanged;
        }

        public void RefreshNextReward()
        {
            PreviousReward = PickRewardByLevel(_levelService.Current);
            NextReward = PickRewardByLevel(_levelService.Next);
            RewardUpdated?.Invoke(NextReward);
        }

        public void Dispose() => 
            _levelService.LevelChanged -= OnLevelChanged;

        private void OnLevelChanged(int current, int next)
        {
            RewardGot?.Invoke(NextReward);

            GiveReward(NextReward.Item);
            NextReward = PickRewardByLevel(next);
            
            RewardUpdated?.Invoke(NextReward);
        }

        private void GiveReward(Item reward) => 
            reward.Accept(_itemVisitor);

        private RewardConfig PickRewardByLevel(int level)
        {
            if (_rewardsByLevel.TryGetValue(level, out var reward))
                return reward;

            RewardConfig lastReward = null;
            foreach (var pair in _rewardsByLevel)
            {
                if (pair.Key <= level && (lastReward == null || pair.Key > lastReward.RequiredLevel))
                    lastReward = pair.Value;
            }

            return lastReward;
        }
    }
}