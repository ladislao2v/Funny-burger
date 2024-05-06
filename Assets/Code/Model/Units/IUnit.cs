using Code.Model.Commands;

namespace Code.Models.Units
{
    public interface IUnit
    {
        void Do(ICommand command);
    }
}