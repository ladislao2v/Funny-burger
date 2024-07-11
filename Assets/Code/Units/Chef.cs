using System;
using Code.BurgerPlate;
using Code.Configs;
using Code.Units.Commands;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.Units
{
    public sealed class Chef : MonoBehaviour, IPlayer
    {
        public ReactiveCommand TaskStarted { get; private set; }
        public ChefConfig Config { get; private set; }
        public IBurgerPlate BurgerPlate { get; private set; }

        [Inject]
        private void Construct(ChefConfig config)
        {
            TaskStarted = new ReactiveCommand();
            Config = config;
            BurgerPlate = GetComponent<IBurgerPlate>();
        }

        public void Do(ICommand command)
        {
            TaskStarted.Execute();

            Observable.Timer(TimeSpan.FromSeconds(Config.TaskTime))
                .Subscribe(_ => command?.Execute()).
                Dispose();
        }
    }
}