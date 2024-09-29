
using Code.Configs;

namespace Code.Services.LevelRewardService
{
    public interface IItemVisitor
    {
        void Visit(RecipeConfig recipeConfig);
        void Visit(GemConfig recipeConfig);
    }
}