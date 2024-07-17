using System;
using Code.Configs;
using Code.Services.StaticDataService;
using UnityEngine;
using Zenject;

namespace Code.Units
{
    public sealed class ChefInitializer : IInitializable 
    {
        private readonly IPlayer _player;
        private readonly IStaticDataService _staticDataService;

        public ChefInitializer(IPlayer player, IStaticDataService staticDataService)
        {
            _player = player;
            _staticDataService = staticDataService;
        }

        public void Initialize()
        {
            IChefConfig config = 
                _staticDataService.SettingsConfig;
            
            _player.Construct(config);
        }
    }
}