using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowGlassMaterialSwaper : MonoBehaviour
{
    public Material dayMaterial;
    public Material nightMaterial;
    private bool isDay;

    void Start() {
        isDay = TimeController.isDay;
        SetCorespondingMaterial();
    }

    void Update() {
        if(TimeController.isDay != isDay) {
            isDay = !isDay;
            SetCorespondingMaterial();
        }
    }

    private void SetCorespondingMaterial() {
        if (isDay) {
            GetComponent<MeshRenderer>().material = dayMaterial;
        }
        else {
            GetComponent<MeshRenderer>().material = nightMaterial;
        }
    }
}
