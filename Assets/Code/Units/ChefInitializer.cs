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
            IChefConfig config = _staticDataService.GameConfig;
    
            Debug.Log(config);

            if (config == null)
            {
                throw new Exception(nameof(config));
            }
            
            _player.Construct(config);
        }
    }
}