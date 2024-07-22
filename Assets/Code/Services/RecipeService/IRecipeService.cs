using System.Collections.Generic;
using Code.Recipes;
using Code.Services.GameDataService;

namespace Code.Services.RecipeService
{
    public interface IRecipeService : ISavable
    {
        IEnumerable<Recipe> Storage { get; }
        Recipe GetNextRecipe();
        void AddRecipe(Recipe recipe);
    }
}