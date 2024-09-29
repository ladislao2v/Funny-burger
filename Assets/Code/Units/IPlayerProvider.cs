namespace Code.Units
{
    public interface IPlayerProvider
    {
        public IPlayer Player { get; }

        public void Set(IPlayer player);
    }
}