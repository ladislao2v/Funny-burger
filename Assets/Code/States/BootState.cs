using Code.Services.AssetProvider;
using Plugins.StateMachine.Core.Interfaces;
using UnityEngine;

namespace Code.States
{
    public sealed class BootState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IAssetProvider _assetProvider;

        public BootState(IStateMachine stateMachine, IAssetProvider assetProvider)
        {
            _stateMachine = stateMachine;
            _assetProvider = assetProvider;
        }
        
        public async void Enter()
        {
            await _assetProvider.Load();
            
            _stateMachine.EnterState<DataLoadState>();   
        }
    }
}