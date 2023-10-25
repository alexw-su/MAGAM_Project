using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Base setup of this State Machine from : "https://unity3d.college/2017/05/26/unity3d-design-patterns-state-basic-state-machine/"
/// </summary>
public class MarcimanStateMachine : MonoBehaviour
{
    [Header("ShowLog")]
    [SerializeField] private bool showLog;

    protected MarcimanState currentState;

    private void Update()
    {
        if (currentState == null)
            return;

        currentState.OnStateRunning();
    }

    public void SetState(MarcimanState newState)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = newState;

        if (currentState != null)
            currentState.OnStateEnter();
    }

    protected void LogMessage(string message)
    {
        if (!showLog)
            return;

        Debug.Log($"{gameObject.name}: {message}");
    }
}
