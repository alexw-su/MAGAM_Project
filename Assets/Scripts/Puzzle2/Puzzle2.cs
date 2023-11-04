using UnityEngine;
using System.Collections;
using static UnityEngine.ParticleSystem;

public enum Puzzle2States
{
    Idle,
    TimeSet,
}

public partial class Puzzle2 : MarcimanStateMachine
{

    private Puzzle2States _currentState;
    public GameObject crystal;
    public GameObject doorBlock;
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
                OnStateEnter(newState);
                break;
        }
    }
    private void OnStateEnter(Puzzle2States state)
    {
        crystal.SetActive(true);
        doorBlock.GetComponent<Collider>().enabled = false;
        particles1.gameObject.SetActive(true);
        particles2.gameObject.SetActive(true);
    }
}

