using System;
using System.Collections.Generic;
<<<<<<< HEAD
using Code.Configs;
=======
using System.Linq;
using Code.Goods;
using Code.Recipes;
using Code.Services.ConfigProvider;
using Code.Services.GameDataService;
using Code.Services.GameDataService.Data;
using ModestTree;
>>>>>>> 3f2981a6ce365d14b31d0e298b0e3d3d8fd23979
using Random = UnityEngine.Random;

namespace Code.Services.RecipeService
{
    public sealed class RandomRecipeService : IRecipeService
    {
<<<<<<< HEAD
        private readonly List<RecipeConfig> _recipes = new();
        
        public RecipeConfig GetNextRecipe() =>
            _recipes[Random.Range(0, _recipes.Count)];

        public void AddRecipe(RecipeConfig recipeConfig)
=======
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
>>>>>>> 3f2981a6ce365d14b31d0e298b0e3d3d8fd23979
        {
            if(_recipes.Contains(recipeConfig))
                throw new ArgumentException(nameof(recipeConfig));
            
            _recipes.Add(recipeConfig);
        }

<<<<<<< HEAD
        public bool Has(RecipeConfig recipeConfig) =>
            _recipes.Contains(recipeConfig);
=======
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
>>>>>>> 3f2981a6ce365d14b31d0e298b0e3d3d8fd23979
    }
}