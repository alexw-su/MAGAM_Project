using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Base setup of this State Machine from : "https://unity3d.college/2017/05/26/unity3d-design-patterns-state-basic-state-machine/"
/// </summary>
public abstract class MarcimanState
{

    public virtual void OnStateEnter() { }
    public abstract void OnStateRunning();
    public virtual void OnStateExit() { }
}
