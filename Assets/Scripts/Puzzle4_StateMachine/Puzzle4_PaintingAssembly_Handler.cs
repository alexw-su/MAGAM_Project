using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle4_PaintingAssembly_Handler : MonoBehaviour
{
    [SerializeField] PaintingScript paintingScript;
    [SerializeField] SnapToLocation_Handler snapToLocation_Handler;

    [Header("Painting")]
    [SerializeField] MeshRenderer canvasMesh;
    [SerializeField] Texture fullyAssembledTexture;
    [SerializeField] Texture halfAssembledTexture;


    private Material _canvasMaterial;

    private int _paintingAssemblyCounter = 0;

    private void Start()
    {
        //As long as painting is not assembled, don't do do painting functionality
        paintingScript.enabled = false;
        gameObject.tag = "Untagged";

        _canvasMaterial = new Material(canvasMesh.material);
        canvasMesh.material = _canvasMaterial;

        snapToLocation_Handler.OnSnappedToLocation += SnapToLocation_Handler_OnSnappedToLocation;
    }

    private void OnDestroy()
    {
        snapToLocation_Handler.OnSnappedToLocation -= SnapToLocation_Handler_OnSnappedToLocation;
    }

    private void SnapToLocation_Handler_OnSnappedToLocation(GameObject gameObject)
    {
        _paintingAssemblyCounter++;

        if (_paintingAssemblyCounter == 1)
        {
            _canvasMaterial.mainTexture = halfAssembledTexture;
        }
        else if (_paintingAssemblyCounter >= 2)
        {
            _canvasMaterial.mainTexture = fullyAssembledTexture;
        }
    }
}
