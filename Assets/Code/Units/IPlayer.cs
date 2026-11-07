using System;
using Code.BurgerPlate;
using Code.Configs;
using Code.Movement;
using UnityEngine;

namespace Code.Units
{
    public interface IPlayer : IUnit
    {
        IChefConfig Config { get; }
        IBurgerPlate Plate { get; }
        IMovement Movement { get; }
        Transform Transform { get; }
        bool IsBusy { get; }

        event Action TaskStarted;
        event Action TaskEnded; 
        void Construct(IChefConfig config);
        void Reset();
        void Teleport(Vector3 position);
    }
}