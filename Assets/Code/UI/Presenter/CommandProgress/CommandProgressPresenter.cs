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
        private IPlayer _model;
        private ICommandProgressView _view;

        [Inject]
        private void Construct(IPlayer model)
        {
            _model = model;
            _view = GetComponent<ICommandProgressView>();
            
            
            _model.TaskStarted += OnTaskStarted;
            _model.TaskEnded += OnTaskEnded;
        }

        private void OnDestroy()
        {
            _model.TaskStarted -= OnTaskStarted;
            _model.TaskEnded -= OnTaskEnded;
        }

        private void OnTaskStarted()
        {
            _view.Show();
            
            float taskTime = _model.Config.TaskTime;
            
            _view.OnCommandProgressed(taskTime);
        }

        private void OnTaskEnded()
        {
            _view.Hide();
        }
    }
}