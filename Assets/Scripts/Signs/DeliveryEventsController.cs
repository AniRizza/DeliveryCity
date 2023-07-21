using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryEventsController : MonoBehaviour
{
    public GameObject signQuestPrefab;
    public GameObject signDonePrefab;

    private GameObject[] allBuildingsObjects;
    [SerializeField]
    private float floatingSignOffset = 5;

    public void StartDeliveryEvents() {
        allBuildingsObjects = GameObject.FindGameObjectsWithTag("Building");
        Debug.Log("all buildings " + allBuildingsObjects.Length);
        GenerateEvent();
    }

    public void GenerateEvent() {
        int indexFrom = UnityEngine.Random.Range(0, allBuildingsObjects.Length);
        int indexTo = UnityEngine.Random.Range(0, allBuildingsObjects.Length - 1);
        if (indexTo >= indexFrom) indexTo++;
        //if buildings to close thet reselect second

        GameObject blobFrom = Instantiate(signQuestPrefab,
            new Vector3(allBuildingsObjects[indexFrom].transform.position.x, floatingSignOffset, allBuildingsObjects[indexFrom].transform.position.z),
            signQuestPrefab.transform.rotation);
        blobFrom.GetComponent<QuestSignScript>().SetQuestDeliveryLocation(allBuildingsObjects[indexTo].transform.position);
    }

}
