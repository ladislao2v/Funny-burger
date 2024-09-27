using System;
using Code.Constants;
using Code.Services.Factories.PrefabFactory;
using Code.Services.ShopService;
using Code.UI.Shop;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Services.Factories.ItemShopFactory
{
    public class ShopItemViewFactory : IShopItemViewFactory
    {
        private readonly IPrefabFactory _prefabFactory;

        public ShopItemViewFactory(IPrefabFactory prefabFactory)
        {
            _prefabFactory = prefabFactory;
        }
        
        public async UniTask<IShopItemView> Create(IShopItem item)
        {
            GameObject gameObject = await _prefabFactory
                .Create(AssetKey.ShopItem);

            if (!gameObject.TryGetComponent(out IShopItemView view))
                throw new Exception(nameof(AssetKey.ShopItem));
            
            view.Construct(item);

            return view;
        }
    }
}