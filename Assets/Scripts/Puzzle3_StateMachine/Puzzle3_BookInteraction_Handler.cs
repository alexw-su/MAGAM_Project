using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Puzzle3_BookColor
{
    Nothing,
    Red,
    Green,
    Blue
}


public class Puzzle3_BookInteraction_Handler : MonoBehaviour, IInteractable
{
    [SerializeField] bool isSolutionBook;
    [SerializeField] Puzzle3_BookColor bookColor;

    private MMF_Player _bookFeedback;

    public bool IsSolutionBook { get => isSolutionBook; }
    public Puzzle3_BookColor BookColor { get => bookColor; }


    public delegate void BookInteraction(bool isSolutionBook, Puzzle3_BookColor bookColor);
    public event BookInteraction OnBookInteraction;

    private void Start()
    {
        _bookFeedback = GetComponent<MMF_Player>();
    }


    public void OnInteractionStart(bool isGrabbing)
    {
        if (isGrabbing)
            return;

        OnBookInteraction?.Invoke(isSolutionBook, bookColor);

        if (_bookFeedback != null)
            _bookFeedback.PlayFeedbacks();
    }
}
