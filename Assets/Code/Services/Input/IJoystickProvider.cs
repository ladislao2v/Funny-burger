namespace Code.Services.Input
{
    public interface IJoystickProvider
    {
        public Joystick Joystick { get; }

        public void Set(Joystick joystick);
    }
}