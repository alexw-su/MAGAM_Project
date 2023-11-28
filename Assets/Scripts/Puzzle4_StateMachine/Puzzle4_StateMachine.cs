using UnityEngine;
using System.Collections;

public enum Puzzle4_State
{
	Idle,
	PaintingAssemble,
	MazePhase,
	Finished,
}

public partial class Puzzle4_StateMachine : MarcimanStateMachine {

    private Puzzle4_State _currentState;

    public Puzzle4_State CurrentState { get => _currentState; }



    private void ChangeState(Puzzle4_State newState)
    {
        _currentState = newState;

        switch (newState)
        {
		case Puzzle4_State.Idle:
			SetState(new Puzzle4_State_Idle(this));
			break;
		case Puzzle4_State.PaintingAssemble:
			SetState(new Puzzle4_State_PaintingAssemble(this));
			break;
		case Puzzle4_State.MazePhase:
			SetState(new Puzzle4_State_MazePhase(this));
			break;
		case Puzzle4_State.Finished:
			SetState(new Puzzle4_State_Finished(this));
			break;
        }
    }
}