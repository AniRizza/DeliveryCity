using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSignScript : MonoBehaviour
{
    public GameObject signDonePrefab;
    public GameObject questHitboxPrefab;
    public GameObject minimapSignPrefab;

    private Vector3 deliveryLocation;
    private Vector3 tileCenter;
    private GameObject minimapObject;

    // Start is called before the first frame update
    void Start()
    {
        minimapObject = Instantiate(minimapSignPrefab, transform.position, minimapSignPrefab.transform.rotation, transform.parent);
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
        Destroy(minimapObject);
        Destroy(gameObject);
    }
}
