using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Puzzle3Elements
{
    Fire,
    Water,
    Air,
    Earth
}


public class Puzzle3_Item_Handler : MonoBehaviour
{
    [SerializeField] Puzzle3Elements element = Puzzle3Elements.Fire;
    public Puzzle3Elements Element { get => element; }
}
