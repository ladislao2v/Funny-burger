using Code.Configs;

namespace Code.Services.RecipeService
{
    public interface IRecipeService
    {
        RecipeConfig GetNextRecipe();
        void AddRecipe(RecipeConfig recipeConfig);
        bool Has(RecipeConfig recipeConfig);
    }
}