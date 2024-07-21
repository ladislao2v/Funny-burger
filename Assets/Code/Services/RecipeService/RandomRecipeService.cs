using System;
using System.Collections.Generic;
using Code.Recipes;
using Random = UnityEngine.Random;

namespace Code.Services.RecipeService
{
    public sealed class RandomRecipeService : IRecipeService
    {
        private readonly List<Recipe> _recipes = new();
        
        public Recipe GetNextRecipe() =>
            _recipes[Random.Range(0, _recipes.Count)];

        public void AddRecipe(Recipe recipe)
        {
            if(_recipes.Contains(recipe))
                throw new ArgumentException(nameof(recipe));
            
            _recipes.Add(recipe);
        }
    }
}