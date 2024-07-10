using Code.Goods;
using Code.Triggers;
using Code.Units;
using UnityEngine;

namespace Code.Kitchen.Box
{
    public class Box : ObservableTrigger<IUnit>
    {
        [SerializeField] private IngredientType _ingredientType;
        
        protected override void InteractWith(IUnit unit)
        {
            unit.Do(null);
        }
    }
}