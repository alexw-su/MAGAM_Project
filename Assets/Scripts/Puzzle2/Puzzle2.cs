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
    [SerializeField] SnapToLocation_Handler pointerSnapHandler;
    [SerializeField] PuzzleLeave puzzle2Leave;
    public GameObject clock1;
    public GameObject clock2;
    public GameObject crystal;
    public GameObject doorBlock;
    [Space]
    public Material skyBoxMaterialNight;

    [Header("Particle Systems")]
    public ParticleSystem particles1;
    public ParticleSystem particles2;
    public Puzzle2States CurrentState { get => _currentState; }
    public bool correctTime = false;
    public StringPair messageUnsolved;
    public StringPair messageSolved;

    void Start()
    {
        ChangeState(Puzzle2States.Idle);

        crystal.SetActive(false);
        particles1.gameObject.SetActive(false);
        particles2.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        pointerSnapHandler.OnSnappedToLocation += PointerSnapHandler_OnSnappedToLocation;
        puzzle2Leave.OnLeavePainting += PaintingLeaveHandler;
    }

    private void OnDisable()
    {
        pointerSnapHandler.OnSnappedToLocation -= PointerSnapHandler_OnSnappedToLocation;
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
    private void PointerSnapHandler_OnSnappedToLocation(GameObject gameObject)
    {
        var snappedGameObject = gameObject.GetComponent<Puzzle3_Item_Handler>();
        Debug.Log("PointerSnapHandler_OnSnappedToLocation" + gameObject.name);
    }
    private void PaintingLeaveHandler()
    {
        MessageBus messageBus = FindObjectOfType<MessageBus>();
        if (correctTime)
        {
            ChangeState(Puzzle2States.TimeSet);
            clock2.tag = "Untagged";
            if (messageSolved != null && messageBus != null)
            {
                messageBus.AddMessage(messageSolved.Category, messageSolved.Key);
                messageSolved = null;
            }
        } else {
            if (messageUnsolved != null && messageBus != null)
            {
                messageBus.AddMessage(messageUnsolved.Category, messageUnsolved.Key);
                messageUnsolved = null;
            }
        }

    }
}

