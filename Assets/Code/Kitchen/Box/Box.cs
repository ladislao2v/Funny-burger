using Code.Goods;
using Code.Triggers;
using Code.Units;
using Code.Units.Commands;
using UnityEngine;

namespace Code.Kitchen.Box
{
    [RequireComponent(typeof(SphereCollider))]
    public sealed class Box : ObservableTrigger<IPlayer>
    {
        [SerializeField] private IngredientType _ingredientType;
        protected override void InteractWith(IPlayer player)
        {
            var addIngredientCommand = 
                new AddIngredientCommand(player.BurgerPlate, _ingredientType);
            
            player.Do(addIngredientCommand);
        }
    }
}