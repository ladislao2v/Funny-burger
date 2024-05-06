using Code.Model.Commands;
using UnityEngine;

namespace Code.Models.Units
{
    public class Chef : MonoBehaviour, IUnit
    {
        public void Do(ICommand command)
        {
            command.Execute();
        }
    }
}