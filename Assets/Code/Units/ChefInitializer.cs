using System;
using Code.Configs;
using Code.Services.ConfigProvider;
using UnityEngine;
using Zenject;

namespace Code.Units
{
    public sealed class ChefInitializer : IInitializable 
    {
        private readonly IPlayer _player;
        private readonly IConfigProvider _configProvider;

        public ChefInitializer(IPlayer player, IConfigProvider configProvider)
        {
            _player = player;
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            IChefConfig config = 
                _configProvider.SettingsConfig;
            
            _player.Construct(config);
        }
    }
}