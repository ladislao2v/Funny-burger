using System.Collections.Generic;
using System.Threading;
using Code.BurgerPlate;
using Code.Ingredients;
using Cysharp.Threading.Tasks;

namespace Code.Services.TutorialService.Steps
{
    public sealed class SpawnBurgerStep : TutorialStep
    {
        private readonly IBurgerPlate _plate;
        private readonly IReadOnlyList<IngredientType> _burger;

        private CancellationTokenSource _cancellation;

        public SpawnBurgerStep(IBurgerPlate plate, IReadOnlyList<IngredientType> burger)
        {
            _plate = plate;
            _burger = burger;
        }

        protected override void OnStarted()
        {
            _cancellation = new CancellationTokenSource();

            Spawn(_cancellation.Token).Forget();
        }

        protected override void OnEnded()
        {
            _cancellation?.Cancel();
            _cancellation?.Dispose();
            _cancellation = null;
        }

        private async UniTaskVoid Spawn(CancellationToken token)
        {
            bool isCanceled = await UniTask
                .NextFrame(token)
                .SuppressCancellationThrow();

            if (isCanceled)
                return;

            if (!_plate.IsEmpty)
                _plate.Clear();

            _plate.AddRange(_burger);

            Complete();
        }
    }
}
