using Code.Configs;
using System.Collections.Generic;
using Code.Services.GameDataService;

namespace Code.Services.RecipeService
{
    public interface IRecipeService : ISavable
    {
        IEnumerable<RecipeConfig> Storage { get; }

        RecipeConfig GetNextRecipe();

        void AddRecipe(RecipeConfig recipeConfig);

        bool Has(RecipeConfig recipeConfig);
    }
}