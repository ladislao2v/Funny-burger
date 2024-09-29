namespace Code.Services.Input
{
    public sealed class JoystickProvider : IJoystickProvider
    {
        public Joystick Joystick { get; private set; }
        
        public void Set(Joystick joystick) => 
            Joystick = joystick;
    }
}