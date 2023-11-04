using UnityEngine;
using System.Collections;

public enum Puzzle2States
{
    Idle,
    TimeSet,
}

public partial class Puzzle2 : MarcimanStateMachine
{

    private Puzzle2States _currentState;

    public Puzzle2States CurrentState { get => _currentState; }

    void Start()
    {
        ChangeState(Puzzle2States.Idle);
    }
    public void Change(Puzzle2States newState)
    {
        ChangeState(newState);
    }

    private void ChangeState(Puzzle2States newState)
    {
        _currentState = newState;

        switch (newState)
        {
            case Puzzle2States.Idle:
                SetState(new State_Idle(this));
                break;
            case Puzzle2States.TimeSet:
                SetState(new State_TimeSet(this));
                break;
        }
    }
}

