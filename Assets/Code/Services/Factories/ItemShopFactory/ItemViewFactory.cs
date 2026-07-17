using System;
using Code.Configs;
using Code.Constants;
using Code.Services.Factories.PrefabFactory;
using Code.Services.ResourceStorage;
using Code.Services.ShopService;
using Code.UI.Shop;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Services.Factories.ItemShopFactory
{
    public class ItemViewFactory : IItemViewFactory
    {
        private readonly IPrefabFactory _prefabFactory;

        public ItemViewFactory(IPrefabFactory prefabFactory)
        {
            _prefabFactory = prefabFactory;
        }
        
        public async UniTask<IItemView> Create(
            Item item, 
            int? level = null, 
            ResourceType? currency = null, 
            int? price = null)
        {
            GameObject gameObject = await _prefabFactory
                .Create(AssetKey.ItemView);

            if (!gameObject.TryGetComponent(out IItemView view))
                throw new Exception(nameof(AssetKey.ItemView));
            
            view.Construct(item, level, currency, price);

            return view;
        }
    }
}