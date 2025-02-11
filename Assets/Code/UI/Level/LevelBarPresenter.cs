using Code.Services.LevelService;
using UnityEngine;
using Zenject;

namespace Code.UI.Level
{
    public class LevelBarPresenter : MonoBehaviour
    {
        private ILevelService _model;
        private ILevelBarView _view;

        [Inject]
        private void Construct(ILevelService levelService)
        {
            _model = levelService;
            _view = GetComponent<ILevelBarView>();

            _model.LevelChanged += _view.OnLevelChanged;
            _model.ProgressChanged += _view.OnProgressChanged;
            
            _view.OnLevelChanged(_model.Current, _model.Next);
            _view.OnProgressChanged(_model.Progress, _model.RequiredProgress);
        }

        private void OnDestroy()
        {
            _model.LevelChanged += _view.OnLevelChanged;
            _model.ProgressChanged += _view.OnProgressChanged;
        }
    }
}