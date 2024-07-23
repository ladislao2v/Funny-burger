<<<<<<< HEAD
using Code.Configs;
=======
using System.Collections.Generic;
using Code.Recipes;
using Code.Services.GameDataService;
>>>>>>> 3f2981a6ce365d14b31d0e298b0e3d3d8fd23979

namespace Code.Services.RecipeService
{
    public interface IRecipeService : ISavable
    {
<<<<<<< HEAD
        RecipeConfig GetNextRecipe();
        void AddRecipe(RecipeConfig recipeConfig);
        bool Has(RecipeConfig recipeConfig);
=======
        IEnumerable<Recipe> Storage { get; }
        Recipe GetNextRecipe();
        void AddRecipe(Recipe recipe);
>>>>>>> 3f2981a6ce365d14b31d0e298b0e3d3d8fd23979
    }
}