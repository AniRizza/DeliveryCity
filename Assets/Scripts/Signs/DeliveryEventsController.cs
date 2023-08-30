using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryEventsController : MonoBehaviour
{
    public GameObject signQuestPrefab;
    public GameObject roadEventPrefab;

    private GameObject[] allBuildingsObjects;
    private GameObject[] allTileObjects;
    [SerializeField]
    private float floatingSignOffset = 5;

    public void StartDeliveryEvents() {
        allBuildingsObjects = GameObject.FindGameObjectsWithTag("Building");
        allTileObjects = GameObject.FindGameObjectsWithTag("Tile");
        InvokeRepeating("GenerateDelivery", 5, 30);
        InvokeRepeating("GenerateRoadEvent", 20, 100);
    }

    private void GenerateDelivery() {
        int indexFrom = UnityEngine.Random.Range(0, allBuildingsObjects.Length);
        int indexTo = UnityEngine.Random.Range(0, allBuildingsObjects.Length);
        
        //if buildings to close then reselect second
        while (Vector3.Distance(allBuildingsObjects[indexFrom].transform.position, allBuildingsObjects[indexTo].transform.position) < 16){
            indexTo = UnityEngine.Random.Range(0, allBuildingsObjects.Length);
        }

        GameObject blobFrom = Instantiate(signQuestPrefab,
            new Vector3(allBuildingsObjects[indexFrom].transform.position.x, floatingSignOffset, allBuildingsObjects[indexFrom].transform.position.z),
            signQuestPrefab.transform.rotation, transform);
        blobFrom.GetComponent<QuestSignScript>().SetQuestDeliveryLocation(
            new Vector3(allBuildingsObjects[indexTo].transform.position.x, floatingSignOffset, allBuildingsObjects[indexTo].transform.position.z));
    }

    private void GenerateRoadEvent() {
        int randomTileIndex = UnityEngine.Random.Range(0, allTileObjects.Length);

        //if central tile then reselect
        while (allTileObjects[randomTileIndex].transform.position.x == 0 && allTileObjects[randomTileIndex].transform.position.z == 0){
            randomTileIndex = UnityEngine.Random.Range(0, allTileObjects.Length);
        }

        GameObject eventSignBlob = Instantiate(roadEventPrefab,
            new Vector3(allTileObjects[randomTileIndex].transform.position.x, floatingSignOffset, allTileObjects[randomTileIndex].transform.position.z),
            roadEventPrefab.transform.rotation, transform);
        StartCoroutine(RoadEventCountdownRoutine(eventSignBlob));
    }

    IEnumerator RoadEventCountdownRoutine(GameObject eventSignBlob) {
        yield return new WaitForSeconds(30);
        eventSignBlob.GetComponent<RoadEventSignScript>().EndEvent();
    }

}
