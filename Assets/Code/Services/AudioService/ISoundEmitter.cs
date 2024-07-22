namespace Code.Services.AudioService
{
    public interface ISoundEmitter
    {
        void SetActive(bool active);
        void SetVolume(float volume);
    }
}