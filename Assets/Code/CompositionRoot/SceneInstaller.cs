using System.Linq;
using Code.Configs;
using Code.Movement;
using Code.Services.AudioService;
using Code.Services.AudioService.Emitters;
using Code.Services.Factories.IngredientFactory;
using Code.Services.Factories.PopupFactory;
using Code.Services.Factories.PrefabFactory;
using Code.Services.Input;
using Code.Services.PopupService;
using Code.Units;
using Plugins.StateMachine.StateFactory;
using UnityEngine;
using Zenject;

namespace Code.CompositionRoot
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [Header("Configs")]
        
        [Header("UI")]
        [SerializeField] private Joystick _joystick;
        [SerializeField] private PopupContainer _popupContainer;

        [Header("Units")] 
        [SerializeField] private Chef _chef;

        public override void InstallBindings()
        {
            BindJoystick();
            BindInputService();
            BindPlayer();
            BindPlayerRouter();
            BindPopupService();
            BindAudioService();
            BindFactories();
            BindStateFactory();
            BindStateMachine();
        }
        
        

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<PrefabFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<IngredientFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PopupFactory>().AsSingle();
        }

        private void BindAudioService()
        {
            Container.BindInterfacesAndSelfTo<AudioService>()
                .AsSingle()
                .WithArguments(
                    FindObjectsOfType<Emitter>()
                        .Cast<ISoundEmitter>());
        }

        private void BindPopupService()
        {
            Container.BindInterfacesAndSelfTo<PopupContainer>().FromInstance(_popupContainer).AsSingle();
            Container.BindInterfacesAndSelfTo<PopupService>().AsSingle();
        }

        private void BindPlayerRouter()
        {
            Container
                .BindInterfacesAndSelfTo<FixedPlayerRouter>()
                .AsCached();
        }

        private void BindPlayer()
        {
            Container
                .BindInterfacesAndSelfTo<Chef>()
                .FromInstance(_chef)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<ChefInitializer>()
                .AsCached();
        }

        private void BindInputService()
        {
            Container
                .BindInterfacesAndSelfTo<JoystickInput>()
                .AsCached();
        }

        private void BindJoystick()
        {
            Container
                .Bind<Joystick>()
                .FromInstance(_joystick)
                .AsSingle();
        }
        
        
        private void BindStateMachine()
        {
            Container.BindInterfacesAndSelfTo<Plugins.StateMachine.Core.StateMachine>().AsSingle();
        }

        private void BindStateFactory()
        {
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
        }
    }
}