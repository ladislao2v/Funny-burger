using System;
using System.Collections.Generic;
using System.Linq;
using Code.Constants;
using Code.Goods;
using Code.Recipes;
using UnityEngine;

namespace Code.Services.StaticDataService
{
    public sealed class StaticDataService : IStaticDataService
    {
        private readonly Dictionary<IngredientType,IngredientConfig> _configs;
        private readonly Recipe[] _recipes;

        public StaticDataService()
        {
            _configs = Resources
                .LoadAll<IngredientConfig>(ResourcePathes.Configs)
                .ToDictionary(x => x.Type);

            _recipes = Resources
                .LoadAll<Recipe>(ResourcePathes.Recipes)
                .ToArray();
        }
        
        public IngredientConfig GetIngredientConfig(IngredientType ingredientType)
        {
            if (_configs.ContainsKey(ingredientType) == false)
                throw new ArgumentException(nameof(ingredientType));

            return _configs[ingredientType];
        }

        public Recipe[] GetRecipes() => 
            _recipes;
    }
}