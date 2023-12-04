using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    public bool enabled = false;
    public int debuggedPuzzle;
    public GameObject player;
    public GameObject puzzle1;
    public GameObject puzzle2;
    public GameObject puzzle3;
    public GameObject puzzle4;

    private void Awake()
    {
        if (enabled)
        {
            switch (debuggedPuzzle)
            {
                case 1:
                    {
                        player.transform.position = puzzle1.transform.position;

                    }
                    break;
                case 2:
                    {
                        player.transform.position = puzzle2.transform.position;
                    }
                    break;
                case 3:
                    {
                        player.transform.position = puzzle3.transform.position;
                    }
                    break;
                case 4:
                    {
                        player.transform.position = puzzle4.transform.position;
                    }
                    break;
                default: break;
            }
        }

    }
}
