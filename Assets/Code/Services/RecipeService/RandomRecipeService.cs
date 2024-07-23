using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using Code.Configs;
using Code.Goods;
using Code.Services.ConfigProvider;
using Code.Services.GameDataService;
using Code.Services.GameDataService.Data;
using Random = UnityEngine.Random;

namespace Code.Services.RecipeService
{
    public sealed class RandomRecipeService : IRecipeService
    {
        private readonly List<RecipeConfig> _recipes = new();
        private readonly IConfigProvider _configProvider;

        public IEnumerable<RecipeConfig> Storage => _recipes;
        public string SaveKey => nameof(RandomRecipeService);

        public RecipeConfig GetNextRecipe() =>
            _recipes[Random.Range(0, _recipes.Count)];

        public RandomRecipeService(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void AddRecipe(RecipeConfig recipeConfig)
        {
            if(_recipes.Contains(recipeConfig))
                throw new ArgumentException(nameof(recipeConfig));
            
            _recipes.Add(recipeConfig);
        }
        
        public bool Has(RecipeConfig recipeConfig) =>
            _recipes.Contains(recipeConfig);
        
        public void Load(IData data)
        {
            if (data == null)
                data = new RecipesData();

            if (!(data is RecipesData recipesData))
                throw new ArgumentException(nameof(data));
            
            if(recipesData.Recipes.IsEmpty())
                return;

            IEnumerable<RecipeConfig> allRecipes = _configProvider.GetRecipes();
            
            IEnumerable<List<IngredientType>> savedRecipeDatas = recipesData
                .Recipes
                .Select(recipeData => recipeData.Burger);

            IEnumerable<RecipeConfig> savedRecipes = allRecipes.Where(recipe =>
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