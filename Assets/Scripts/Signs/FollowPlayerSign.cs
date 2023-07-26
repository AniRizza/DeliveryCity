using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerSign : MonoBehaviour
{
    public GameObject player;
    public float hightAbovePlayer;
    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(0, hightAbovePlayer, 0);
    }

    void FixedUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
