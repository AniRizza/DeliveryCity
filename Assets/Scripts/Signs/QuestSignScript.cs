using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSignScript : MonoBehaviour
{
    public GameObject signDonePrefab;
    public GameObject questHitboxPrefab;

    private Vector3 deliveryLocation;
    private Vector3 tileCenter;

    // Start is called before the first frame update
    void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(new Vector3(transform.position.x, 0, transform.position.z), 1f /* Radius */);
        foreach(var collider in colliders) {
            if (collider.tag == "Tile"){
                tileCenter = collider.gameObject.transform.position;
            }
        }
        Instantiate(questHitboxPrefab, tileCenter, questHitboxPrefab.transform.rotation, transform);
        GameObject.Find("Audio Manager").GetComponent<AudioManager>().PlaySound("QuestAppear");
    }

    public void SetQuestDeliveryLocation(Vector3 deliveryLocation) {
        this.deliveryLocation = deliveryLocation;
    }

    public void StartDeliveryQuest() {
        Instantiate(signDonePrefab, deliveryLocation, signDonePrefab.transform.rotation);
        Destroy(gameObject);
    }
}
