using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TileClickReaction : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) {
            int x = GetComponent<TileCharacteristic>().GetX();
            int y = GetComponent<TileCharacteristic>().GetY();

            Debug.Log("Pressed left-click on x " + x + " y " + y);
            GameObject.Find("Car").GetComponent<NavMeshAgent>().SetDestination(transform.position);
        }
        if (Input.GetMouseButtonDown(1)) {
            Debug.Log("Pressed right-click on x " + GetComponent<TileCharacteristic>().GetX() + " y " + GetComponent<TileCharacteristic>().GetY());
        }
    }
}
