using Code.Configs;
using UniRx;

namespace Code.Units
{
    public interface IPlayer
    {
        public ReactiveCommand TaskStarted { get; }
        public ChefConfig Config { get; }
    }
}