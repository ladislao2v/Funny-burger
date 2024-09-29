using Code.Services.AssetProvider;
using Code.Services.AudioService;
using Code.Services.BurgerOrderService;
using Code.Services.ClientsService;
using Code.Services.ConfigProvider;
using Code.Services.Factories.ClientFactory;
using Code.Services.Factories.IngredientFactory;
using Code.Services.Factories.ItemShopFactory;
using Code.Services.Factories.ItemVisitorFactory;
using Code.Services.Factories.PopupFactory;
using Code.Services.Factories.PrefabFactory;
using Code.Services.GameDataService;
using Code.Services.GameTimeService;
using Code.Services.Input;
using Code.Services.LevelRewardService;
using Code.Services.LevelService;
using Code.Services.LocalizationService;
using Code.Services.PopupService;
using Code.Services.RecipeService;
using Code.Services.ResourceStorage;
using Code.Services.SaveDataService;
using Code.Services.SceneLoader;
using Code.Services.ShopService;
using Code.Units;
using Plugins.StateMachine.StateFactory;
using Zenject;

namespace Code.CompositionRoot
{
    public sealed class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAssetProvider();
            BindStaticData();
            BindPrefabFactory();
            BindFactories();
            BindSceneLoader();
            BindLevelService();
            BindWalletService();
            BindLevelRewardService();
            BindGameDataService();
            BindInputService();
            BindShop();
            BindAudioService();
            BindOrderService();
            BindGameTimeService();
            BindLocalizationService();
            BindPlayer();
            BindClientService();
            BindPopupService();
            BindStateFactory();
            BindStateMachine();
        }

        private void BindLevelRewardService()
        {
            Container.BindInterfacesAndSelfTo<LevelRewardService>().AsCached();
        }

        private void BindClientService()
        {
            Container.BindInterfacesAndSelfTo<ClientsServiceProvider>().AsSingle();
        }

        private void BindPopupService()
        {
            Container.BindInterfacesAndSelfTo<PopupContainerProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<PopupService>().AsSingle();
        }

        private void BindPlayer()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerProvider>()
                .AsSingle();
        }
        
        private void BindInputService()
        {
            Container.BindInterfacesAndSelfTo<JoystickProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<JoystickInput>().AsCached();
        }

        private void BindLocalizationService()
        {
            Container.BindInterfacesAndSelfTo<LocalizationService>().AsCached();
        }

        private void BindGameTimeService()
        {
            Container.BindInterfacesAndSelfTo<GameTimeService>().AsSingle();
        }

        private void BindOrderService()
        {
            Container.BindInterfacesAndSelfTo<BurgerOrderService>().AsSingle();
        }

        private void BindShop()
        {
            Container.BindInterfacesAndSelfTo<RandomRecipeService>().AsSingle();
            Container.BindInterfacesAndSelfTo<RecipeShop>().AsCached();
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
            Container.BindInterfacesAndSelfTo<ShopItemViewFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ClientFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ItemVisitorFactory>().AsSingle();
        }

        private void BindGameDataService()
        {
            Container.BindInterfacesAndSelfTo<PlayerPrefsDataSaver>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameDataService>().AsSingle();
        }

        private void BindWalletService()
        {
            Container.BindInterfacesAndSelfTo<ResourceStorage>().AsCached();
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

        private void BindStateMachine()
        {
            Container.BindInterfacesAndSelfTo<Plugins.StateMachine.Core.StateMachine>().AsCached();
        }

        private void BindStateFactory()
        {
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
        }
    }
}