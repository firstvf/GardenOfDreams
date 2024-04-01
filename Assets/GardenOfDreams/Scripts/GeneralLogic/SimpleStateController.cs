using UnityEngine.Events;

public class SimpleStateController
{
    private States _currentState;

    private enum States
    {
        Active,
        Combat,
        Patrol,
        Die
    }

    public SimpleStateController()
    {
        _currentState = States.Active;
    }

    public bool IsAbleToMove()
    => _currentState == States.Active;
}