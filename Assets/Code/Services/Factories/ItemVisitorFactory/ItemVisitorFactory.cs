using Code.Services.LevelRewardService;
using Zenject;

namespace Code.Services.Factories.ItemVisitorFactory
{
    public class ItemVisitorFactory : IItemVisitorFactory
    {
        private readonly IInstantiator _instantiator;

        public ItemVisitorFactory(IInstantiator instantiator) => 
            _instantiator = instantiator;

        public IItemVisitor CreateVisitor<T>() where T : IItemVisitor => 
            _instantiator.Instantiate<T>();
    }
}