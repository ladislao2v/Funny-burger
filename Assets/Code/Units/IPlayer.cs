using System;
using Code.BurgerPlate;
using Code.Configs;
using Code.Movement;
using UniRx;

namespace Code.Units
{
    public interface IPlayer : IUnit
    {
        IChefConfig Config { get; }
        IBurgerPlate Plate { get; }
        IMovement Movement { get; }
        
        event Action TaskStarted;
        event Action TaskEnded; 
        void Construct(IChefConfig config);
    }
}