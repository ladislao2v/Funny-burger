namespace Code.Units
{
    public sealed class PlayerProvider : IPlayerProvider
    {
        public IPlayer Player { get; private set; }
        
        public void Set(IPlayer player) => 
            Player = player;
    }
}