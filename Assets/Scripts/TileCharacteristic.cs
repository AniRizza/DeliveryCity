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

    public void SetCoordinates(int x, int z) {
        this.x = x;
        this.z = z;
    }
}
