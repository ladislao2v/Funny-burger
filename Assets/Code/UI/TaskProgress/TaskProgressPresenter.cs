using System;
using Code.Units;
using UnityEngine;
using Zenject;

namespace Code.UI.TaskProgress
{
    public class TaskProgressPresenter : MonoBehaviour
    {
        private IPlayerProvider _modelProvider;
        private ITaskProgressView _view;

        [Inject]
        private void Construct(IPlayerProvider modelProvider)
        {
            _modelProvider = modelProvider;
            _view = GetComponent<ITaskProgressView>();
        }

        private void Start()
        {
            _modelProvider.Player.TaskStarted += OnTaskStarted;
            _modelProvider.Player.TaskEnded += OnTaskEnded;
        }

        private void OnDestroy()
        {
            _modelProvider.Player.TaskStarted -= OnTaskStarted;
            _modelProvider.Player.TaskEnded -= OnTaskEnded;
        }

        private void OnTaskStarted()
        {
            _view.Show();
            
            float taskTime = _modelProvider.Player.Config.TaskTime;
            
            _view.OnCommandProgressed(taskTime);
        }

        private void OnTaskEnded()
        {
            _view.Hide();
        }
    }
}