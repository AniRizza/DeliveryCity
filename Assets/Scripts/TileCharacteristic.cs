using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCharacteristic : MonoBehaviour
{
    private int x;
    private int y;

    [SerializeField]
    private bool isRoadOnTop;
    [SerializeField]
    private bool isRoadOnBot;
    [SerializeField]
    private bool isRoadOnRight;
    [SerializeField]
    private bool isRoadOnLeft;
    [SerializeField]
    private int rotationParameter;

    public int GetNumberOfExits() {
        int numberOfExits = 0;
        if (isRoadOnTop) numberOfExits++;
        if (isRoadOnBot) numberOfExits++;
        if (isRoadOnRight) numberOfExits++;
        if (isRoadOnLeft) numberOfExits++;
        return numberOfExits;
    }

    public int GetTileType() {
        int result = 0;
        int exits = GetNumberOfExits();
        if ((isRoadOnTop != isRoadOnBot) && (isRoadOnLeft != isRoadOnRight)) {
            result = exits;
        }
        else result = 1;
        return result;
    }

    public int GetX() {return x;}

    public int GetY() {return y;}

    public void SetCoordinates(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public bool GetIsRoadOnTop() {return isRoadOnTop;}

    public bool GetIsRoadOnRight() {return isRoadOnRight;}

    public bool GetIsRoadOnBot() {return isRoadOnBot;}

    public bool GetIsRoadOnLeft() {return isRoadOnLeft;}

    public int GetRotationParameter() {return rotationParameter;}
}