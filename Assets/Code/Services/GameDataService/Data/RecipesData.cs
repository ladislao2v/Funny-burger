using System;
using System.Collections.Generic;
using Code.Goods;
using Code.Recipes;

namespace Code.Services.GameDataService.Data
{
    public sealed class RecipesData : IData
    {
        public List<RecipeData> Recipes { get; set; }

        public RecipesData(IEnumerable<Recipe> recipes = null)
        {
            Recipes = new();
            
            if(recipes == null)
                return;
            
            foreach (var recipe in recipes)
                Recipes.Add(CreateRecipeData(recipe));
        }

        private RecipeData CreateRecipeData(Recipe recipe)
        {
            var burgerData = new RecipeData();

            foreach (var config in recipe.Burger)
                burgerData.Add(config.Type);

            return burgerData;
        }
    }

    [Serializable]
    public class RecipeData
    {
        public List<IngredientType> Burger { get; set; }

        public void Add(IngredientType type)
        {
            Burger.Add(type);
        }
    }
}