﻿using Code.Services.AssetProvider;
using Code.Services.AudioService;
using Code.Services.ConfigProvider;
using Code.Services.Factories.IngredientFactory;
using Code.Services.Factories.PopupFactory;
using Code.Services.Factories.PrefabFactory;
using Code.Services.GameDataService;
using Code.Services.LevelService;
using Code.Services.PopupService;
using Code.Services.SaveDataService;
using Code.Services.SceneLoader;
using Code.Services.WalletService;
using Zenject;

namespace Code.CompositionRoot
{
    public sealed class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAssetProvider();
            BindStaticData();
            BindSceneLoader();
            BindLevelService();
            BindWalletService();
            BindGameDataService();
            BindPrefabFactory();
            BindFactories();
            BindAudioService();
        }
        
        private void BindAudioService()
        {
            Container.BindInterfacesAndSelfTo<AudioService>().AsSingle();
        }

        private void BindPrefabFactory()
        {
            Container.BindInterfacesAndSelfTo<PrefabFactory>().AsSingle();
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<IngredientFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PopupFactory>().AsSingle();
        }

        private void BindGameDataService()
        {
            Container.BindInterfacesAndSelfTo<PlayerPrefsDataSaver>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameDataService>().AsSingle();
        }

        private void BindWalletService()
        {
            Container.BindInterfacesAndSelfTo<Wallet>().AsCached();
        }

        private void BindLevelService()
        {
            Container.BindInterfacesAndSelfTo<LevelService>().AsCached();
        }

        private void BindStaticData()
        {
            Container.BindInterfacesAndSelfTo<ConfigProvider>().AsSingle();
        }

        private void BindAssetProvider()
        {
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
        }

        private void BindSceneLoader()
        {
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
        }

    }
}