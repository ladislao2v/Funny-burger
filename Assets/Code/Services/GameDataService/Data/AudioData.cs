namespace Code.Services.GameDataService.Data
{
    public sealed class AudioData : IData
    {
        public bool IsActive { get; set; }
        public float CurrentVolume { get; set; }

        public AudioData(bool isActive = true, float currentVolume = 1)
        {
            IsActive = isActive;
            CurrentVolume = currentVolume;
        }
    }
}