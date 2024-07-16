using Code.BurgerPlate;
using Code.Configs;
using Code.Movement;
using UniRx;

namespace Code.Units
{
    public interface IPlayer : IUnit
    {
        ReactiveCommand TaskStarted { get; }
        ChefConfig Config { get; }
        IBurgerPlate BurgerPlate { get; }
        IMovement Movement { get; }
    }
}