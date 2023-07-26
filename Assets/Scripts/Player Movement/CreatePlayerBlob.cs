using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayerBlob : MonoBehaviour
{
    public GameObject playerBlobPrefab;
    [SerializeField]
    private float hightAbovePlayer;

    void Start()
    {
        GameObject playerBlob = Instantiate(playerBlobPrefab, new Vector3(0, hightAbovePlayer, 0), playerBlobPrefab.transform.rotation, transform);
        playerBlob.GetComponent<FollowPlayerSign>().hightAbovePlayer = hightAbovePlayer;
        playerBlob.GetComponent<FollowPlayerSign>().player = gameObject;
    }

}
