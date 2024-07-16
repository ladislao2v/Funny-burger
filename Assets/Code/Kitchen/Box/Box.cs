using Code.Goods;
using Code.Services.Factories.IngredientFactory;
using Code.Triggers;
using Code.Units;
using Code.Units.Commands;
using UnityEngine;
using Zenject;

namespace Code.Kitchen.Box
{
    [RequireComponent(typeof(SphereCollider))]
    public sealed class Box : ObservableTrigger<IPlayer>
    {
        [SerializeField] private IngredientType _ingredientType;
        
        private IIngredientFactory _ingredientFactory;

        [Inject]
        private void Construct(IIngredientFactory ingredientFactory)
        {
            _ingredientFactory = ingredientFactory;
        }
        
        protected override async void InteractWith(IPlayer player)
        {
            var ingredient = await 
                _ingredientFactory.Create(_ingredientType);
            
            var addIngredientCommand = 
                new AddIngredientCommand(player.BurgerPlate, ingredient);
            
            player.Do(addIngredientCommand);
        }
    }
}