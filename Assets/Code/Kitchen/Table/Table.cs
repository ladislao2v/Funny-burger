using System;
using Code.BurgerPlate;
using Code.Units;
using Code.Units.Commands;
using UnityEngine;

namespace Code.Kitchen.Table
{
    [RequireComponent(typeof(TablePlateView))]
    public sealed class Table : Trigger<IPlayer>
    {
        private readonly IBurgerPlate _plate = new Plate();

        private void Awake()
        {
            var plateView = GetComponent<TablePlateView>();
            plateView.Construct(_plate);
        }

        protected override bool TryInteractWith(IPlayer player)
        {
            if (_plate.IsEmpty && player.Plate.IsEmpty)
                return false;
            
            ICommand command = 
                new SwitchBurgersWithTable(player.Plate, _plate);
            
            player.Do(command);

            return true;
        }
    }
}