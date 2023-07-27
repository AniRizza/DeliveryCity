using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TileClickReaction : MonoBehaviour
{
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) {
            int x = GetComponent<TileCharacteristic>().GetX();
            int y = GetComponent<TileCharacteristic>().GetY();

            //Debug.Log("Pressed left-click on x " + x + " y " + y);
            GameObject.Find("Car").GetComponent<CarMovementPath>().SetTarget(transform.position);
            GameObject.Find("Car").GetComponent<AudioSource>().Play();
        }
    }
}
