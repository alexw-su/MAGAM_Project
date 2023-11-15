using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;

public enum Puzzle3_State
{
	Idle,
	Phase1,
	Phase2,
	Phase3,
	Phase4,
	Finished,
}


[System.Serializable]
public class Puzzle3_StateProperties
{
	public int Phase = 1;
	public int StateGoal = 1;
	public Puzzle3Elements Element = Puzzle3Elements.Fire;

	public Transform respawnPosition;

	public GameObject elementFlaskPrefab;
	public GameObject successCloud;
}

public partial class Puzzle3_StateMachine : MarcimanStateMachine
{
	[SerializeField] SnapToLocation_Handler cauldronSnapHandler;
    [SerializeField] GameObject failCloud;
    [SerializeField] List<Puzzle3_StateProperties> phasePropertiesList;
	[SerializeField] Transform particleEffectLocation;

	private int _currentElementsPlacedCounter = 0;


    private Puzzle3_StateProperties _currentActiveStateProperty = null;

    private Puzzle3_State _currentState;

    public Puzzle3_State CurrentState { get => _currentState; }

    private void Start()
    {
		ChangeState(Puzzle3_State.Idle);

		foreach(var property in phasePropertiesList)
		{
            Instantiate(property.elementFlaskPrefab, property.respawnPosition);
        }
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
			SetState(new Puzzle3_State_Idle(this, new Puzzle3_StateProperties()));
			break;
		case Puzzle3_State.Phase1:
			SetState(new Puzzle3_State_Phase1(this, phasePropertiesList[0]));
			break;
		case Puzzle3_State.Phase2:
			SetState(new Puzzle3_State_Phase2(this, phasePropertiesList[1]));
			break;
		case Puzzle3_State.Phase3:
			SetState(new Puzzle3_State_Phase3(this, phasePropertiesList[2]));
			break;
		case Puzzle3_State.Phase4:
			SetState(new Puzzle3_State_Phase4(this, phasePropertiesList[3]));
			break;
		case Puzzle3_State.Finished:
			SetState(new Puzzle3_State_Finished(this, new Puzzle3_StateProperties()));
			break;
        }
    }


    private void CauldronSnapHandler_OnSnappedToLocation(GameObject gameObject)
    {
		var snappedGameObject = gameObject.GetComponent<Puzzle3_Item_Handler>();

		RespawnDroppedFlask(snappedGameObject);

        if (snappedGameObject.Element == _currentActiveStateProperty.Element)
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
			FailReset();
			return;
        }

        _currentElementsPlacedCounter++;
        if (_currentElementsPlacedCounter < _currentActiveStateProperty.StateGoal)
		{
			return;
		}
		//Changes State to next higher State (i.e. Phase 2 to Phase 3) by going to stateEnum + 1
		ChangeState(_currentState + 1);
	}


    //Cycles through all types and respawns the correct one.
    private void RespawnDroppedFlask(Puzzle3_Item_Handler handler)
	{
		foreach(var property in phasePropertiesList)
		{
			if (property.Element != handler.Element)
				continue;

            Instantiate(property.elementFlaskPrefab, property.respawnPosition);
			return;
        }
	}


    private void FailReset()
    {
		Debug.Log("You failed, try again");

        Instantiate(failCloud, particleEffectLocation);
        ChangeState(Puzzle3_State.Phase1);
    }
}