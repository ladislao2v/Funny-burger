using Code.Services.AssetProvider;
using Code.Services.Factories.IngredientFactory;
using Code.Services.Factories.PrefabFactory;
using Code.Services.SceneLoader;
using Code.Services.StaticDataService;
using Zenject;

namespace Code.CompositionRoot
{
    public sealed class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAssetProvider();
            BindStaticData();
            BindFactories();
            BindSceneLoader();
        }

        private void BindStaticData()
        {
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<DiPrefabFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<IngredientFactory>().AsSingle();
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