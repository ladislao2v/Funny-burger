namespace Code.Services.AudioService
{
    public interface ISoundEmitter
    {
        void Enable();
        void Disable();
        void SetVolume(float volume);
    }
}