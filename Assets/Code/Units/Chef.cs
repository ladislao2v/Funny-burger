﻿using System;
using Code.BurgerPlate;
using Code.Configs;
using Code.Movement;
using Code.Units.Commands;
using UniRx;
using UnityEngine;

namespace Code.Units
{
    public sealed class Chef : MonoBehaviour, IPlayer
    {
        private IDisposable _timer;
        public IBurgerPlate BurgerPlate { get; } = new Plate();
        public IChefConfig Config { get; private set; }
        public IMovement Movement { get; private set; }
        public event Action TaskStarted;
        public event Action TaskEnded; 

        public void Construct(IChefConfig config)
        {
            Config = config;
            Movement = GetComponent<IMovement>();
        }

        public void Do(ICommand command)
        {
            TaskStarted?.Invoke();

            var timerTime = TimeSpan.FromSeconds(Config.TaskTime);

            _timer = Observable.Timer(timerTime)
                .Subscribe(_ =>
                {
                    command.Execute();
                    TaskEnded?.Invoke();
                    _timer.Dispose();
                });
        }
    }
}