using Code.Model.Goods;
using Code.Model.Triggers;
using Code.Models.Units;
using UnityEngine;

namespace Code.Model.Kitchen.Box
{
    public class Box : ObservableTrigger<IUnit>
    {
        [SerializeField] private GoodType _goodType;
        
        protected override void InteractWith(IUnit unit)
        {
            unit.Do(null);
        }
    }
}