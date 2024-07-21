using Code.Recipes;

namespace Code.Services.RecipeService
{
    public interface IRecipeService
    {
        Recipe GetNextRecipe();
        void AddRecipe(Recipe recipe);
    }
}