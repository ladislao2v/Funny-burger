using Code.Configs;
using Code.Movement;
using Code.Services.Input;
using Code.Units;
using UnityEngine;
using Zenject;

namespace Code.CompositionRoot
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [Header("Configs")] 
        [SerializeField] private ChefConfig _chefConfig;
        
        [Header("UI")]
        [SerializeField] private Joystick _joystick;

        [Header("Units")] 
        [SerializeField] private Chef _chef;

        public override void InstallBindings()
        {
            BindChefConfig();
            BindJoystick();
            BindInputService();
            BindPlayer();
            BindPlayerRouter();
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
                .AsCached();
        }

        private void BindChefConfig()
        {
            Container
                .Bind<ChefConfig>()
                .FromInstance(_chefConfig)
                .AsSingle();
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
    }
}