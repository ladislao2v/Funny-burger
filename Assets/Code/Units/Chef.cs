using System;
using System.Threading;
using Code.BurgerPlate;
using Code.Configs;
using Code.Movement;
using Code.Units.Commands;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Code.Units
{
    public sealed class Chef : MonoBehaviour, IPlayer
    {
        private IDisposable _timer;
        
        public IBurgerPlate Plate { get; } = new Plate();
        public IChefConfig Config { get; private set; }
        public IMovement Movement { get; private set; }
        public event Action TaskStarted;
        public event Action TaskEnded;

        private void Awake() => 
            Movement = GetComponent<IMovement>();

        public void Construct(IChefConfig config) => 
            Config = config;

        public void Do(ICommand command, Action onDo)
        {
            TaskStarted?.Invoke();
            
            var timerTime = 
                TimeSpan.FromSeconds(Config.TaskTime);

            _timer = Observable.Timer(timerTime)
                .Subscribe(_ =>
                {
                    command.Execute();
                    onDo?.Invoke();
                    
                    Reset();
                });
        }

        public void Reset()
        {
            TaskEnded?.Invoke();
            _timer?.Dispose();
        }
    }
}