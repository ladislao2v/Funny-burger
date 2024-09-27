using System;
using System.Collections.Generic;
using Code.Configs;
using Code.Ingredients;

namespace Code.Services.GameDataService.Data
{
    [Serializable]
    public sealed class RecipesData : IData
    {
        public List<RecipeData> Recipes { get; set; }

        public RecipesData() { }

        public RecipesData(IEnumerable<RecipeConfig> recipes = null)
        {
            Recipes = new();
            
            if(recipes == null)
                return;
            
            foreach (var recipe in recipes)
                Recipes.Add(CreateRecipeData(recipe));
        }

        private RecipeData CreateRecipeData(RecipeConfig recipe)
        {
            var burgerData = new RecipeData();
            
            foreach (var ingredientConfig in recipe.Burger)
                burgerData.Add(ingredientConfig.Type);

            return burgerData;
        }
    }

    [Serializable]
    public class RecipeData
    {
        public List<IngredientType> Burger { get; set; } = new();

        public void Add(IngredientType type)
        {
            Burger.Add(type);
        }
    }
}