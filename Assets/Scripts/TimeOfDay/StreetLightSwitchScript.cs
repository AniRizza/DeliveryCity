using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLightSwitchScript : MonoBehaviour
{
    private Light streetLightSource;
    private bool isDay;

    void Start() {
        streetLightSource = GetComponent<Light>();
        isDay = TimeController.isDay;
        SetCorespondingState();
    }

    void Update() {
        if(TimeController.isDay != isDay) {
            isDay = !isDay;
            SetCorespondingState();
        }
    }

    private void SetCorespondingState() {
        if (isDay) {
            streetLightSource.enabled = false;
        }
        else {
            streetLightSource.enabled = true;
        }
    }
}
