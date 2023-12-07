using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;
using TMPro;
using MoreMountains.Feedbacks;

public enum Puzzle3_State
{
    Idle,
    Potion_Phase1,
    Potion_Phase2,
    Potion_Phase3,
    Potion_Phase4,
    Finished_Cauldron,
    Book_Phase1,
    Book_Phase2,
    Book_Phase3,
    Finished_Book
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


[System.Serializable]
public class Puzzle3_BookPhase_Properties
{
    [Header("Matches the bookcolor to its supposed state")]
    public Puzzle3_State bookState = Puzzle3_State.Book_Phase1;
    public Puzzle3_BookColor bookColor;
}

public partial class Puzzle3_StateMachine : MarcimanStateMachine
{
    [Header("General")]
    [SerializeField] GameObject failCloud;
    [SerializeField] List<Puzzle3_StateProperties> phasePropertiesList;
    [SerializeField] Transform particleEffectLocation;

    [Header("Cauldron")]
    [SerializeField] SnapToLocation_Handler cauldronSnapHandler;
    [Tooltip("The last Element is always white, because thats the pause inbetween the pulses")]
    [SerializeField] List<Color> solvedPuzzleColorSequence;
    [SerializeField] Puzzle3_Cauldron_ColorSequence_Handler cauldronColorHandler;
    [SerializeField] float solvedPuzzleInterval = 2f;
    [Space]
    [SerializeField] VFX_Dissolve_Handler wallDissolve;
    [SerializeField] float dissolveDuration = 2.5f;
    [SerializeField] AudioClip successDrop;
    [SerializeField] AudioClip failDrop;
    [SerializeField] AudioClip puzzleSuccesSound;

    [Header("Books")]
    [SerializeField] List<Puzzle3_BookInteraction_Handler> bookInteractionHandlerList;
    [SerializeField] List<Puzzle3_BookPhase_Properties> bookPhaseProperties;

    [Header("Painting Piece")]
    [SerializeField] GameObject paintingPiece;
    [SerializeField] Transform paintingSpawnPosition;

    [Header("FEEL")]
    [SerializeField] MMF_Player smallBoing_mmfPlayer;
    [SerializeField] MMF_Player bigBoing_mmfPlayer;

    private int _currentElementsPlacedCounter = 0;

    private Material _cauldronMaterial;


    private Puzzle3_StateProperties _currentActiveStateProperty = null;

    private Puzzle3_State _currentState;

    public Puzzle3_State CurrentState { get => _currentState; }

    private void Start()
    {
        ChangeState(Puzzle3_State.Idle);

        foreach (var property in phasePropertiesList)
        {
            Instantiate(property.elementFlaskPrefab, property.respawnPosition);
        }
    }

    private void OnEnable()
    {
        cauldronSnapHandler.OnSnappedToLocation += CauldronSnapHandler_OnSnappedToLocation;


        foreach (var book in bookInteractionHandlerList)
        {
            book.OnBookInteraction += Book_OnBookInteraction;
        }
    }

    private void OnDisable()
    {
        cauldronSnapHandler.OnSnappedToLocation -= CauldronSnapHandler_OnSnappedToLocation;


        foreach (var book in bookInteractionHandlerList)
        {
            book.OnBookInteraction -= Book_OnBookInteraction;
        }
    }


    private void ChangeState(Puzzle3_State newState)
    {
        _currentState = newState;

        switch (newState)
        {
            case Puzzle3_State.Idle:
                SetState(new Puzzle3_State_Idle(this, new Puzzle3_StateProperties()));
                break;
            case Puzzle3_State.Potion_Phase1:
                SetState(new Puzzle3_State_Potion_Phase1(this, phasePropertiesList[0]));
                break;
            case Puzzle3_State.Potion_Phase2:
                SetState(new Puzzle3_State_Potion_Phase2(this, phasePropertiesList[1]));
                break;
            case Puzzle3_State.Potion_Phase3:
                SetState(new Puzzle3_State_Potion_Phase3(this, phasePropertiesList[2]));
                break;
            case Puzzle3_State.Potion_Phase4:
                SetState(new Puzzle3_State_Potion_Phase4(this, phasePropertiesList[3]));
                break;
            case Puzzle3_State.Finished_Cauldron:
                SetState(new Puzzle3_State_Finished_Cauldron(this, new Puzzle3_StateProperties()));
                break;
            case Puzzle3_State.Book_Phase1:
                SetState(new Puzzle3_State_Book_Phase1(this, new Puzzle3_StateProperties()));
                break;
            case Puzzle3_State.Book_Phase2:
                SetState(new Puzzle3_State_Book_Phase2(this, new Puzzle3_StateProperties()));
                break;
            case Puzzle3_State.Book_Phase3:
                SetState(new Puzzle3_State_Book_Phase3(this, new Puzzle3_StateProperties()));
                break;
            case Puzzle3_State.Finished_Book:
                SetState(new Puzzle3_State_Finished_Book(this, new Puzzle3_StateProperties()));
                break;
        }
    }

    #region Event Subscriptions
    private void CauldronSnapHandler_OnSnappedToLocation(GameObject gameObject)
    {
        var snappedGameObject = gameObject.GetComponent<Puzzle3_Item_Handler>();

        RespawnDroppedFlask(snappedGameObject);

        if (snappedGameObject.Element == _currentActiveStateProperty.Element)
        {
            UpdatePuzzlePhases(false);

            smallBoing_mmfPlayer.PlayFeedbacks();

            FindObjectOfType<AudioManager>().PlaySFX(successDrop);

        }
        else if ((int)_currentState < 5)
        {
            UpdatePuzzlePhases(true, Puzzle3_State.Potion_Phase1);
            FindObjectOfType<AudioManager>().PlaySFX(failDrop);
        }
    }


    private void Book_OnBookInteraction(bool isSolutionBook, Puzzle3_BookColor bookColor)
    {
        if ((int)_currentState < (int)Puzzle3_State.Book_Phase1)
        {
            Debug.Log("Don't touch this yet, my G");
            return;
        }

        if (!isSolutionBook)
        {
            UpdatePuzzlePhases(true, Puzzle3_State.Book_Phase1);
            return;
        }


        foreach (var bookCombi in bookPhaseProperties)
        {
            if (_currentState != bookCombi.bookState)
                continue;

            if (bookCombi.bookColor != bookColor)
            {
                UpdatePuzzlePhases(true, Puzzle3_State.Book_Phase1);
                break;
            }

            UpdatePuzzlePhases();
            break;

        }
    }
    #endregion

    //Use this for bookPhase
    private void UpdatePuzzlePhases()
    {
        ChangeState(_currentState + 1);
    }

    //Use this for Cauldron Phase
    private void UpdatePuzzlePhases(bool reset, Puzzle3_State resetToState = Puzzle3_State.Idle)
    {
        if (reset)
        {
            FailReset(resetToState);
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
        foreach (var property in phasePropertiesList)
        {
            if (property.Element != handler.Element)
                continue;

            Instantiate(property.elementFlaskPrefab, property.respawnPosition);
            return;
        }
    }


    //If Reset State == Potion_Phase 1, then spawn cloud
    //If Reset State == Book_Phase 1, then spawn something else
    private void FailReset(Puzzle3_State resetState)
    {
        if (resetState == Puzzle3_State.Potion_Phase1)
            Instantiate(failCloud, particleEffectLocation);

        ChangeState(resetState);
    }
}