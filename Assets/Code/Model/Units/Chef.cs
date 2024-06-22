using Code.Model.Commands;
using Code.Models.Units;
using UnityEngine;

namespace Code.Model.Units
{
    public class Chef : MonoBehaviour, IUnit
    {
        public void Do(ICommand command)
        {
            command?.Execute();
        }
    }
}