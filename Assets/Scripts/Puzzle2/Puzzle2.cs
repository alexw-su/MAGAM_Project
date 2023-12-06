using UnityEngine;
using System.Collections;
using static UnityEngine.ParticleSystem;

public enum Puzzle2States
{
    Idle,
    PointerPlaced,
    TimeSet,
}

public partial class Puzzle2 : MarcimanStateMachine
{
    private Puzzle2States _currentState;

    [Header("PuzzleElements")]
    [SerializeField] PuzzleLeave puzzle2Leave;
    public GameObject clock1;
    public GameObject button;
    [Space]
    public Material skyBoxMaterialNight;

    public Puzzle2States CurrentState { get => _currentState; }
    public StringPair messageUnsolved;
    public StringPair messageSolved;

    [Header("PaintingPart")]
    [SerializeField] GameObject paintingPart;
    [SerializeField] Transform paintingPartSpawnPoint;

    void Start()
    {
        ChangeState(Puzzle2States.Idle);
    }
    private void OnEnable()
    {
        puzzle2Leave.OnLeavePainting += PaintingLeaveHandler;
    }

    private void OnDisable()
    {
        puzzle2Leave.OnLeavePainting += PaintingLeaveHandler;
    }
    private void ChangeState(Puzzle2States newState)
    {
        _currentState = newState;

        switch (newState)
        {
            case Puzzle2States.Idle:
                SetState(new State_Idle(this));
                break;
            case Puzzle2States.PointerPlaced:
                SetState(new State_PointerPlaced(this));
                break;
            case Puzzle2States.TimeSet:
                SetState(new State_TimeSet(this));
                break;
        }
    }
    private void PaintingLeaveHandler()
    {
        MessageBus messageBus = FindObjectOfType<MessageBus>();
        if (_currentState != Puzzle2States.TimeSet)
        {
            if (messageUnsolved != null && messageBus != null)
            {
                messageBus.AddMessage(messageUnsolved.Category, messageUnsolved.Key);
                messageUnsolved = null;
            }
        }

    }
}

