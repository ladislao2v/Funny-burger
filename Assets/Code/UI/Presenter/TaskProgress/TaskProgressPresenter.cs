using System;
using Code.UI.View.TaskProgressView;
using Code.Units;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.UI.Presenter.TaskProgress
{
    public class TaskProgressPresenter : MonoBehaviour
    {
        private readonly CompositeDisposable _disposables = new();
        
        private IPlayer _model;
        private ITaskProgressView _view;

        [Inject]
        private void Construct(IPlayer model, ITaskProgressView view)
        {
            _model = model;
            _view = view;
        }

        private void OnEnable()
        {
            _model.TaskStarted.Subscribe(_ =>
            {
                Observable
                    .Timer(TimeSpan.FromSeconds(_model.Config.TaskTime))
                    .Subscribe(time => 
                        _view.OnTaskProgressChanged(time / _model.Config.TaskTime));
            }).AddTo(_disposables);
        }

        private void OnDisable()
        {
            _disposables.Dispose();
        }
    }
}