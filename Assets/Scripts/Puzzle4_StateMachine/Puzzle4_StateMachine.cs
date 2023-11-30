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

    [Header("Painting Assembly")]
    [SerializeField] Puzzle4_PaintingAssembly_Handler paintingAssemblyHandler;
    [SerializeField] VFX_Dissolve_Handler wallDissolveHandler;
    [Header("Maze Part")]
    [SerializeField] OnPlayerEnter_Trigger playerTrigger;
    [SerializeField] VFX_Dissolve_Handler crystalHandler;

    private Puzzle4_State _currentState;

    public Puzzle4_State CurrentState { get => _currentState; }


    private void Start()
    {
        ChangeState(Puzzle4_State.PaintingAssemble);

        paintingAssemblyHandler.OnPaintingAssembled += PaintingAssemblyHandler_OnPaintingAssembled;
    }

    private void OnDestroy()
    {
        paintingAssemblyHandler.OnPaintingAssembled -= PaintingAssemblyHandler_OnPaintingAssembled;
    }


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


    private void PaintingAssemblyHandler_OnPaintingAssembled()
    {
        //wallDissolveHandler.InitDissapperance(4.0f);
        ChangeState(Puzzle4_State.MazePhase);
    }
}