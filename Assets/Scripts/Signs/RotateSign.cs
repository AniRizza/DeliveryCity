using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSign : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuScript.IsGamePaused) transform.Rotate(new Vector3(0, 0.5f, 0));
    }
}
