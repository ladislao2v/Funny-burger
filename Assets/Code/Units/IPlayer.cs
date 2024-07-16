using Code.BurgerPlate;
using Code.Configs;
using Code.Movement;
using UniRx;

namespace Code.Units
{
    public interface IPlayer : IUnit
    {
        ReactiveCommand TaskStarted { get; }
        IChefConfig Config { get; }
        IBurgerPlate BurgerPlate { get; }
        IMovement Movement { get; }
        void Construct(IChefConfig config);
    }
}