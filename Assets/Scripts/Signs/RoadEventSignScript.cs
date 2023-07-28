using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEventSignScript : MonoBehaviour
{
    public GameObject questHitboxPrefab;
    private Vector3 tileCenter;

    void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(new Vector3(transform.position.x, 0, transform.position.z), 1f /* Radius */);
        foreach(var collider in colliders) {
            if (collider.tag == "Tile"){
                tileCenter = collider.gameObject.transform.position;
            }
        }
        Instantiate(questHitboxPrefab, tileCenter, questHitboxPrefab.transform.rotation, transform);
        GameObject.Find("Audio Manager").GetComponent<AudioManager>().PlaySound("RoadEvent");
    }
}
