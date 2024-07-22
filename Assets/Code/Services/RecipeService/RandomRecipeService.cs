using System;
using System.Collections.Generic;
using System.Linq;
using Code.Goods;
using Code.Recipes;
using Code.Services.ConfigProvider;
using Code.Services.GameDataService;
using Code.Services.GameDataService.Data;
using ModestTree;
using Random = UnityEngine.Random;

namespace Code.Services.RecipeService
{
    public sealed class RandomRecipeService : IRecipeService
    {
        private readonly List<Recipe> _recipes = new();
        private readonly IConfigProvider _configProvider;

        public IEnumerable<Recipe> Storage => _recipes;
        public string SaveKey => nameof(RandomRecipeService);

        public Recipe GetNextRecipe() =>
            _recipes[Random.Range(0, _recipes.Count)];

        public RandomRecipeService(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void AddRecipe(Recipe recipe)
        {
            if(_recipes.Contains(recipe))
                throw new ArgumentException(nameof(recipe));
            
            _recipes.Add(recipe);
        }

        public void Load(IData data)
        {
            if (data == null)
                data = new RecipesData();

            if (!(data is RecipesData recipesData))
                throw new ArgumentException(nameof(data));
            
            if(recipesData.Recipes.IsEmpty())
                return;

            IEnumerable<Recipe> allRecipes = _configProvider
                .GetRecipes();
            IEnumerable<List<IngredientType>> savedRecipeDatas = recipesData
                .Recipes
                .Select(recipeData => recipeData.Burger);

            IEnumerable<Recipe> savedRecipes = allRecipes.Where(recipe =>
            {
                IEnumerable<IngredientType> ingredients = recipe
                    .Burger
                    .Select(config => config.Type);
                
                return savedRecipeDatas
                    .Any(savedRecipe => 
                        savedRecipe
                            .SequenceEqual(ingredients));
            });

            _recipes.AddRange(savedRecipes);

        }

        public IData Save() => new RecipesData(_recipes);
    }
}