using System;
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
        
        private Action _lastOnDo;

        public IBurgerPlate Plate { get; } = new Plate();
        public IChefConfig Config { get; private set; }
        public IMovement Movement { get; private set; }
        public bool IsBusy => _timer != null;
        public event Action TaskStarted;
        public event Action TaskEnded;

        private void Awake() => 
            Movement = GetComponent<IMovement>();

        public void Construct(IChefConfig config) => 
            Config = config;

        public void Do(ICommand command, Action onDo)
        {
            if (_timer != null)
                return;
            
            TaskStarted?.Invoke();
            
            _lastOnDo = onDo;
            
            var timerTime = 
                TimeSpan.FromSeconds(Config.TaskTime);

            _timer = Observable.Timer(timerTime)
                .Subscribe(_ =>
                {
                    command.Execute();
                    _lastOnDo?.Invoke();
                    _lastOnDo = null;
                    
                    Reset();
                });
        }

        public void Reset()
        {
            if (_timer == null)
                return;
            
            _timer.Dispose();
            _timer = null;
            
            TaskEnded?.Invoke();
        }
    }
}