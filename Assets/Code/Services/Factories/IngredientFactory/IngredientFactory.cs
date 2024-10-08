﻿using System;
using Code.Configs;
using Code.Ingredients;
using Code.Services.ConfigProvider;
using Code.Services.Factories.PrefabFactory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Services.Factories.IngredientFactory
{
    public sealed class IngredientFactory : IIngredientFactory
    {
        private readonly IPrefabFactory _prefabFactory;
        private readonly IConfigProvider _configProvider;

        public IngredientFactory(IPrefabFactory prefabFactory, IConfigProvider configProvider)
        {
            _prefabFactory = prefabFactory;
            _configProvider = configProvider;
        }
        
        public async UniTask<Ingredient> Create(IngredientType ingredientType)
        {
            IngredientConfig config = _configProvider
                .GetIngredientConfig(ingredientType);
            
            GameObject gameObject = await _prefabFactory
                .Create(config.PrefabReference);

            if (!gameObject.TryGetComponent(out Ingredient ingredient))
                throw new ArgumentException(nameof(gameObject));
            
            return ingredient;
        }
    }
}