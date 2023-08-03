using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEventSignScript : MonoBehaviour
{
    public GameObject questHitboxPrefab;
    public GameObject minimapSignPrefab;

    private Vector3 tileCenter;
    private GameObject minimapObject;

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
        GameObject.Find("Audio Manager").GetComponent<AudioManager>().PlaySound("RoadEvent");
    }

    public void EndEvent() {
        Destroy(minimapObject);
        Destroy(gameObject);
    }
}
