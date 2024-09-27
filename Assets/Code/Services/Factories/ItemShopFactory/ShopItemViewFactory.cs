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
        
        public async UniTask<IItemView> Create(IItem item)
        {
            GameObject gameObject = await _prefabFactory
                .Create(AssetKey.ShopItem);

            if (!gameObject.TryGetComponent(out IItemView view))
                throw new Exception(nameof(AssetKey.ShopItem));
            
            view.Construct(item);

            return view;
        }
    }
}