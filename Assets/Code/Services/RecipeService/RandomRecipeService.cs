using System;
using System.Collections.Generic;
using Code.Configs;
using Random = UnityEngine.Random;

namespace Code.Services.RecipeService
{
    public sealed class RandomRecipeService : IRecipeService
    {
        private readonly List<RecipeConfig> _recipes = new();
        
        public RecipeConfig GetNextRecipe() =>
            _recipes[Random.Range(0, _recipes.Count)];

        public void AddRecipe(RecipeConfig recipeConfig)
        {
            if(_recipes.Contains(recipeConfig))
                throw new ArgumentException(nameof(recipeConfig));
            
            _recipes.Add(recipeConfig);
        }

        public bool Has(RecipeConfig recipeConfig) =>
            _recipes.Contains(recipeConfig);
    }
}