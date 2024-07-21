using Code.Services.AssetProvider;
using Code.Services.Factories.IngredientFactory;
using Code.Services.Factories.PopupFactory;
using Code.Services.Factories.PrefabFactory;
using Code.Services.LevelService;
using Code.Services.PopupService;
using Code.Services.SceneLoader;
using Code.Services.StaticDataService;
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
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();
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