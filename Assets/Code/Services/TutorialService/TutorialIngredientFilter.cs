using System.Collections.Generic;
using System.Linq;
using Code.BurgerPlate;
using Code.Ingredients;

namespace Code.Services.TutorialService
{
    public sealed class TutorialIngredientFilter : ITutorialIngredientFilter
    {
        private readonly List<IngredientType> _burgerFromBottom = new();

        private bool _isEnabled;

        public bool CanTake(IBurgerPlate plate, IngredientType ingredient)
        {
            if (!_isEnabled)
                return true;

            List<IngredientType> plateFromBottom = plate
                .Ingredients
                .Reverse()
                .ToList();

            if (plateFromBottom.Count >= _burgerFromBottom.Count)
                return false;

            for (int i = 0; i < plateFromBottom.Count; i++)
            {
                if (plateFromBottom[i] != _burgerFromBottom[i])
                    return false;
            }

            return _burgerFromBottom[plateFromBottom.Count] == ingredient;
        }

        public void Enable(IEnumerable<IngredientType> burger)
        {
            _burgerFromBottom.Clear();
            _burgerFromBottom.AddRange(burger.Reverse());
            _isEnabled = true;
        }

        public void Disable() =>
            _isEnabled = false;
    }
}
