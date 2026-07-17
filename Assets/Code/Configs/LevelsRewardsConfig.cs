using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ModestTree;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create Rewards", fileName = "Rewards", order = 0)]
    public class LevelsRewardsConfig : ScriptableObject
    {
        [SerializeField] private List<RewardConfig> _rewards;

        public IDictionary<int, RewardConfig> RewardsByLevel => _rewards
            .OrderBy(x => x.RequiredLevel)
            .ToDictionary(x => x.RequiredLevel, x => x);
    }
}