using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSignScript : MonoBehaviour
{
    public GameObject signDonePrefab;

    private Vector3 deliveryLocation;
    private Vector3 tileCenter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetQuestDeliveryLocation(Vector3 deliveryLocation) {
        this.deliveryLocation = deliveryLocation;
    }

    private void TakeDeloveryQuest() {
        Instantiate(signDonePrefab, deliveryLocation, signDonePrefab.transform.rotation);
        Destroy(gameObject);
    }
}
