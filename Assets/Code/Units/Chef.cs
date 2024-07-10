using Code.Configs;
using Code.Units.Commands;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.Units
{
    public class Chef : MonoBehaviour, IUnit, IPlayer
    {
        public ReactiveCommand TaskStarted { get; private set; }
        public ChefConfig Config { get; private set; }

        [Inject]
        private void Construct(ChefConfig config)
        {
            TaskStarted = new ReactiveCommand();
            Config = config;
        }

        public void Do(ICommand command)
        {
            command?.Execute();
            TaskStarted.Execute();
        }
    }
}