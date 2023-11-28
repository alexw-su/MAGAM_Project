using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle4_PaintingAssembly_Handler : MonoBehaviour
{
    [SerializeField] PaintingScript paintingScript;

    private void Start()
    {
        //As long as painting is not assembled, don't do do painting functionality
        paintingScript.enabled = false;
        gameObject.tag = "Untagged";
    }
}
