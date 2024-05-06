using Code.Configs.Chef;
using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.CompositionRoot
{
    public class SceneInstaller : MonoInstaller
    {
        [Header("Configs")] 
        [SerializeField] private ChefConfig _chefConfig;
        
        [Header("UI")]
        [SerializeField] private Joystick _joystick;

        public override void InstallBindings()
        {
            BindChefConfig();
            BindJoystick();
            BindInputService();
        }

        private void BindChefConfig()
        {
            Container.Bind<ChefConfig>().FromInstance(_chefConfig).AsSingle();
        }

        private void BindInputService()
        {
            Container.BindInterfacesTo<JoystickInput>().AsSingle();
        }

        private void BindJoystick()
        {
            Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
        }
    }
}