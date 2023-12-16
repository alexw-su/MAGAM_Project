using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle4_OnEnter_PlayElevator : MonoBehaviour
{
    [SerializeField] MMF_Player door_ClosePlayer;
    [SerializeField] float timeOffset = 2f;

    private MMF_Player _elevatorPlayer;
    private OnPlayerEnter_Trigger _onPlayerEnterTrigger;

    void Start()
    {
        _elevatorPlayer = GetComponent<MMF_Player>();

        _onPlayerEnterTrigger = GetComponent<OnPlayerEnter_Trigger>();
        _onPlayerEnterTrigger.OnPlayerEnterTrigger += OnPlayerEnterTrigger_OnPlayerEnterTrigger;
    }

    private void OnDestroy()
    {
        _onPlayerEnterTrigger.OnPlayerEnterTrigger -= OnPlayerEnterTrigger_OnPlayerEnterTrigger;
    }

    private void OnPlayerEnterTrigger_OnPlayerEnterTrigger()
    {
        StartCoroutine(Elevator_Routine());
    }

    private IEnumerator Elevator_Routine()
    {
        door_ClosePlayer.PlayFeedbacks();
        yield return new WaitForSeconds(timeOffset);
        _elevatorPlayer.PlayFeedbacks();
    }
}
