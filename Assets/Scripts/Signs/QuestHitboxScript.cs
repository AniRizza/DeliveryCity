using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHitboxScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (transform.parent.name.Contains("Sign_Quest")) transform.parent.GetComponent<QuestSignScript>().StartDeliveryQuest();
            if (transform.parent.name.Contains("Sign_Done")) transform.parent.GetComponent<DoneSignScript>().FinishDeliveryQuest();

            Destroy(gameObject);
        }
            
    }
}
