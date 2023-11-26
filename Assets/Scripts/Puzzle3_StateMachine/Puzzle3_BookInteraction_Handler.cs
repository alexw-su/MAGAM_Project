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

    public bool IsSolutionBook { get => isSolutionBook; }
    public Puzzle3_BookColor BookColor { get => bookColor; }


    public delegate void BookInteraction(bool isSolutionBook, Puzzle3_BookColor bookColor);
    public event BookInteraction OnBookInteraction;


    public void OnInteractionStart(bool isGrabbing)
    {
        if (isGrabbing)
            return;

        OnBookInteraction?.Invoke(isSolutionBook, bookColor);
    }
}
