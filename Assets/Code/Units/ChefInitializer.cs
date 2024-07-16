using Code.Configs;
using Code.Services.StaticDataService;
using Zenject;

namespace Code.Units
{
    public class ChefInitializer : IInitializable
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

            _player.Construct(config);
        }
    }
}