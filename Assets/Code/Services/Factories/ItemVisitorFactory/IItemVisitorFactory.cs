using Code.Services.LevelRewardService;

namespace Code.Services.Factories.ItemVisitorFactory
{
    public interface IItemVisitorFactory
    {
        public IItemVisitor CreateVisitor<T>() where T : IItemVisitor;
    }
}