using System;
using Code.Units.Commands;

namespace Code.Units
{
    public interface IUnit
    {
        void Do(ICommand command, Action onDo = null);
    }
}