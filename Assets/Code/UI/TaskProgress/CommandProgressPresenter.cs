using System;
using Code.Units;
using UnityEngine;
using Zenject;

namespace Code.UI.TaskProgress
{
    public class CommandProgressPresenter : MonoBehaviour
    {
        private IPlayer _model;
        private ITaskProgressView _view;

        [Inject]
        private void Construct(IPlayerProvider modelProvider)
        {
            _model = modelProvider.Player;
            _view = GetComponent<ITaskProgressView>();
        }

        private void OnEnable()
        {
            _model.TaskStarted += OnTaskStarted;
            _model.TaskEnded += OnTaskEnded;
        }

        private void OnDisable()
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