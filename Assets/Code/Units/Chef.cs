using System;
using Code.BurgerPlate;
using Code.Configs;
using Code.Movement;
using Code.Units.Commands;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.Units
{
    public sealed class Chef : MonoBehaviour, IPlayer
    {
        public ReactiveCommand TaskStarted { get; private set; }
        public IChefConfig Config { get; private set; }
        public IBurgerPlate BurgerPlate { get; private set; }
        public IMovement Movement { get; private set; }

        public void Construct(IChefConfig config)
        {
            Config = config;
            TaskStarted = new ReactiveCommand();
            BurgerPlate = GetComponent<IBurgerPlate>();
            Movement = GetComponent<IMovement>();
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