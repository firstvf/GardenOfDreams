public class FixedJoystick : Joystick
{
    protected override void Start()
    {
        base.Start();
        Player.Instance.SetJoystick(this);
    }
}