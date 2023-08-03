using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class TileClickReaction : MonoBehaviour
{
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (!EventSystem.current.IsPointerOverGameObject()){
                int x = GetComponent<TileCharacteristic>().GetX();
                int y = GetComponent<TileCharacteristic>().GetY();

                //Debug.Log("Pressed left-click on x " + x + " y " + y);
                GameObject.Find("Car").GetComponent<CarMovementPath>().SetTarget(transform.position);
                GameObject.Find("Audio Manager").GetComponent<AudioManager>().PlaySound("Car");
            }
        }
    }
}
