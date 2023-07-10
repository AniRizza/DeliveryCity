using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCharacteristic : MonoBehaviour
{
    public bool isRoadOnTop;
    public bool isRoadOnBot;
    public bool isRoadOnRight;
    public bool isRoadOnLeft;

    public int x;
    public int z;

    public int possibility;
    private Vector3 rotationCurrent = Vector3(-90,0,0);

    void Start() {
        transform.eulerAngles = rotationCurrent;
    }

    public void SetCoordinates(int x, int z) {
        this.x = x;
        this.z = z;
    }

    public void Rotate90Clockwise() {
        rotationCurrent.z += 90;
        transform.eulerAngles = rotationCurrent;

        bool temp = isRoadOnTop;
        isRoadOnTop = isRoadOnLeft;
        isRoadOnLeft = isRoadOnBot;
        isRoadOnBot = isRoadOnRight;
        isRoadOnRight = temp;
    }
}
