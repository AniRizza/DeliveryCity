using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneSignScript : MonoBehaviour
{
    public GameObject questHitboxPrefab;
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
    }

    public void FinishDeliveryQuest() {
        GetComponent<AudioSource>().Play();
        Destroy(gameObject);
    }
}
