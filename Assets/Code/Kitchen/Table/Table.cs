using Code.Triggers;
using Code.Units;
using Cysharp.Threading.Tasks;

namespace Code.Kitchen.Table
{
    public sealed class Table : ObservableTrigger<IPlayer>
    {
        protected override UniTask InteractWith(IPlayer player)
        {
            return new UniTask();
        }
    }
}