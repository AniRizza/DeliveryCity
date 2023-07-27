using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryEventsController : MonoBehaviour
{
    public GameObject signQuestPrefab;

    private GameObject[] allBuildingsObjects;
    [SerializeField]
    private float floatingSignOffset = 5;

    public void StartDeliveryEvents() {
        allBuildingsObjects = GameObject.FindGameObjectsWithTag("Building");
        //Debug.Log("all buildings " + allBuildingsObjects.Length);
        InvokeRepeating("GenerateEvent", 5, 60);
    }

    public void GenerateEvent() {
        int indexFrom = UnityEngine.Random.Range(0, allBuildingsObjects.Length);
        int indexTo = UnityEngine.Random.Range(0, allBuildingsObjects.Length);
        
        //if buildings to close thet reselect second
        while (Vector3.Distance(allBuildingsObjects[indexFrom].transform.position, allBuildingsObjects[indexTo].transform.position) < 16){
            indexTo = UnityEngine.Random.Range(0, allBuildingsObjects.Length);
        }

        GameObject blobFrom = Instantiate(signQuestPrefab,
            new Vector3(allBuildingsObjects[indexFrom].transform.position.x, floatingSignOffset, allBuildingsObjects[indexFrom].transform.position.z),
            signQuestPrefab.transform.rotation);
        blobFrom.GetComponent<QuestSignScript>().SetQuestDeliveryLocation(
            new Vector3(allBuildingsObjects[indexTo].transform.position.x, floatingSignOffset, allBuildingsObjects[indexTo].transform.position.z));
    }

}
