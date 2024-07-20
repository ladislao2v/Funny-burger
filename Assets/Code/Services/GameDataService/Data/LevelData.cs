namespace Code.Services.GameDataService.Data
{
    public class LevelData : IData
    {
        public int Current { get; set; }
        public int Progress { get; set; }

        public LevelData(int current, int progress)
        {
            Current = current;
            Progress = progress;
        }
    }
}