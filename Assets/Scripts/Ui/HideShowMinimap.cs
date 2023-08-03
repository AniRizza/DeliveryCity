using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideShowMinimap : MonoBehaviour
{
    public GameObject miniMapObject;
    
    private bool isMinimapOpen = true;

    public void ButtonClick() {
        isMinimapOpen = !isMinimapOpen;
        miniMapObject.SetActive(isMinimapOpen);
        if (isMinimapOpen) {
            Vector3 position = transform.position;
            transform.position = new Vector3(470, position.y, position.z);
            transform.rotation = Quaternion.Euler (0f, 0f, 180f);
        }
        else {
            Vector3 position = transform.position;
            transform.position = new Vector3(30, position.y, position.z);
            transform.rotation = Quaternion.Euler (0f, 0f, 0f);
        }
    }
}
