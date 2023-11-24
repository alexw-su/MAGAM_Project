using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScaling : MonoBehaviour
{
    public GameObject treeContainer;
    public float minScale = 1f;
    public float maxScale = 6f;
    public bool randomizeRotation = true;

    void Start()
    {
        ScaleAndRotateTrees();
    }

    void ScaleAndRotateTrees()
    {
        foreach (Transform child in treeContainer.transform)
        {
            // Randomly scale the tree with float range
            float randomScale = Random.Range(minScale, maxScale);
            child.localScale = new Vector3(randomScale, randomScale, randomScale);

            // Randomly rotate the tree if enabled
            if (randomizeRotation)
            {
                float randomRotationY = Random.Range(0f, 360f);
                child.rotation = Quaternion.Euler(0f, randomRotationY, 0f);
            }
        }
    }
}
