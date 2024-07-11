using System;
using Code.UI.View.CommandProgress;
using Code.Units;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.UI.Presenter.CommandProgress
{
    public class CommandProgressPresenter : MonoBehaviour
    {
        private readonly CompositeDisposable _disposables = new();
        
        private IPlayer _model;
        private ICommandProgressView _view;

        [Inject]
        private void Construct(IPlayer model, ICommandProgressView view)
        {
            _model = model;
            _view = view;
        }

        private void OnEnable()
        {
            _model.TaskStarted.Subscribe(command =>
            {
                float taskTime = _model.Config.TaskTime;
                TimeSpan limit = TimeSpan.FromSeconds(taskTime);
                
                Observable
                    .Interval(limit)
                    .Subscribe(time =>
                        _view.OnCommandProgressed(time / taskTime))
                    .AddTo(_disposables);
            }).AddTo(_disposables);
        }

        private void OnDisable()
        {
            _disposables.Dispose();
        }
    }
}