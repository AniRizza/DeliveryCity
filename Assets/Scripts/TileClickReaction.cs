using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TileClickReaction : MonoBehaviour
{
    [SerializeField]
    private bool isTileOnPath;
    public GameObject blobPrefab;
    // Start is called before the first frame update
    void Start()
    {
        isTileOnPath = false;
    }

    // Update is called once per frame
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) {
            int x = GetComponent<TileCharacteristic>().GetX();
            int y = GetComponent<TileCharacteristic>().GetY();

            Debug.Log("Pressed left-click on x " + x + " y " + y);
            GameObject.Find("Car").GetComponent<CarMovementPath>().SetTarget(transform);
            //navMeshAgent.SetDestination(transform.position);
        }
        if (Input.GetMouseButtonDown(1)) {
            Debug.Log("Pressed right-click on x " + GetComponent<TileCharacteristic>().GetX() + " y " + GetComponent<TileCharacteristic>().GetY());
        }
    }

    public void AddTileToPath() {
        if (!isTileOnPath) {
            isTileOnPath = true;
            Instantiate(blobPrefab, new Vector3(transform.position.x, 0.4f, transform.position.z), blobPrefab.transform.rotation);
        }
    }
}
