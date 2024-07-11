using Code.Recipes;
using Code.Services.StaticDataService;
using UnityEngine;

namespace Code.Services.RecipeService
{
    public sealed class RandomRecipeService : IRecipeService
    {
        private readonly IStaticDataService _staticDataService;

        public RandomRecipeService(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
        
        public Recipe GetRecipe()
        {
            Recipe[] recipes = _staticDataService.GetRecipes();

            return recipes[Random.Range(0, recipes.Length)];
        }
    }
}