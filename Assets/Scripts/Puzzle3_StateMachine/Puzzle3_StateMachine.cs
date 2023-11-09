using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public enum Puzzle3_State
{
	Idle,
	Phase1,
	Phase2,
	Phase3,
	Phase4,
	Finished,
}

public partial class Puzzle3_StateMachine : MarcimanStateMachine
{
	[SerializeField] SnapToLocation_Handler cauldronSnapHandler;

	private int _currentElementsPlacedGoal = 0;
	private int _currentElementsPlacedCounter = 0;
	private Puzzle3Elements _currentElementToBePlaced = Puzzle3Elements.Fire;

    private Puzzle3_State _currentState;

    public Puzzle3_State CurrentState { get => _currentState; }

    private void Start()
    {
		ChangeState(Puzzle3_State.Idle);
    }


    private void OnEnable()
    {
        cauldronSnapHandler.OnSnappedToLocation += CauldronSnapHandler_OnSnappedToLocation;
    }

    private void OnDisable()
    {
        cauldronSnapHandler.OnSnappedToLocation -= CauldronSnapHandler_OnSnappedToLocation;
    }


    private void ChangeState(Puzzle3_State newState)
    {
        _currentState = newState;

        switch (newState)
        {
		case Puzzle3_State.Idle:
			SetState(new Puzzle3_State_Idle(this));
			break;
		case Puzzle3_State.Phase1:
			SetState(new Puzzle3_State_Phase1(this));
			break;
		case Puzzle3_State.Phase2:
			SetState(new Puzzle3_State_Phase2(this));
			break;
		case Puzzle3_State.Phase3:
			SetState(new Puzzle3_State_Phase3(this));
			break;
		case Puzzle3_State.Phase4:
			SetState(new Puzzle3_State_Phase4(this));
			break;
		case Puzzle3_State.Finished:
			SetState(new Puzzle3_State_Finished(this));
			break;
        }
    }


    private void CauldronSnapHandler_OnSnappedToLocation(GameObject gameObject)
    {
		var snappedGameObject = gameObject.GetComponent<Puzzle3_Item_Handler>();

		Debug.Log(snappedGameObject.Element);

		if(snappedGameObject.Element == _currentElementToBePlaced)
		{
			UpdatePuzzlePhases(false);
		}
		else
		{
			UpdatePuzzlePhases(true);
		}
    }


	private void UpdatePuzzlePhases(bool reset)
	{
		if(reset)
		{
			ChangeState(Puzzle3_State.Phase1);
			return;
        }

        _currentElementsPlacedCounter++;
        if (_currentElementsPlacedCounter < _currentElementsPlacedGoal)
		{
			return;
		}
		//Changes State to next higher State (i.e. Phase 2 to Phase 3) by going to stateEnum + 1
		ChangeState(_currentState + 1);
	}
}