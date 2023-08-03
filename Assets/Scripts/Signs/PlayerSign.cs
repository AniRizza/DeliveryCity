using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSign : MonoBehaviour
{
    public GameObject player;
    public float hightAbovePlayer;
    public GameObject minimapSignPrefab;

    private Vector3 offset;
    private GameObject minimapObject;

    void Start()
    {
        minimapObject = Instantiate(minimapSignPrefab, transform.position, minimapSignPrefab.transform.rotation, transform.parent);
        offset = new Vector3(0, hightAbovePlayer, 0);
    }

    void FixedUpdate()
    {
        transform.position = player.transform.position + offset;
        minimapObject.transform.position = player.transform.position + offset;
    }
}
