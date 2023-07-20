using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryEventsController : MonoBehaviour
{
    public GameObject signQuestPrefab;
    public GameObject signDonePrefab;

    private List<Vector3> allBuildingsCoordinatesList;
    [SerializeField]
    private float floatingSignOffset = 5;

    public void StartDeliveryEvents() {
        allBuildingsCoordinatesList = new List<Vector3>();
        GameObject[] buildingsObjects = GameObject.FindGameObjectsWithTag("Building");
        Debug.Log("all buildings " + buildingsObjects.Length);
        foreach (GameObject building in buildingsObjects)
        {
            allBuildingsCoordinatesList.Add(building.transform.position);
        }
        Debug.Log("here");
        GenerateEvent();
    }

    public void GenerateEvent() {
        int indexFrom = UnityEngine.Random.Range(0, allBuildingsCoordinatesList.Count);
        int indexTo = UnityEngine.Random.Range(0, allBuildingsCoordinatesList.Count - 1);
        if (indexTo >= indexFrom) indexTo++;
        //if buildings to close thet reselect second

        GameObject blobFrom = Instantiate(signQuestPrefab,
            new Vector3(allBuildingsCoordinatesList[indexFrom].x, floatingSignOffset, allBuildingsCoordinatesList[indexFrom].z),
            signQuestPrefab.transform.rotation);
        GameObject blobTo = Instantiate(signDonePrefab,
            new Vector3(allBuildingsCoordinatesList[indexTo].x, floatingSignOffset, allBuildingsCoordinatesList[indexTo].z),
            signDonePrefab.transform.rotation);
    }

}
