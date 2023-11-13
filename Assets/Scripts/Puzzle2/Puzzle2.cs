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
    public ClockScript clock;
    public GameObject crystal;
    public GameObject doorBlock;
    [Space]
    public Material skyBoxMaterialNight;

    [Header("Particle Systems")]
    public ParticleSystem particles1;
    public ParticleSystem particles2;

    public Puzzle2States CurrentState { get => _currentState; }

    void Start()
    {
        ChangeState(Puzzle2States.Idle);

        crystal.SetActive(false);
        particles1.gameObject.SetActive(false);
        particles2.gameObject.SetActive(false);
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
}

