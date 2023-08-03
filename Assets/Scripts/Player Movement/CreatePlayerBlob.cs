using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayerBlob : MonoBehaviour
{
    public GameObject playerBlobPrefab;
    public GameObject player;
    [SerializeField]
    private float hightAbovePlayer;

    void Start()
    {
        GameObject playerBlob = Instantiate(playerBlobPrefab, new Vector3(0, hightAbovePlayer, 0), playerBlobPrefab.transform.rotation, transform);
        playerBlob.GetComponent<PlayerSign>().hightAbovePlayer = hightAbovePlayer;
        playerBlob.GetComponent<PlayerSign>().player = player;
    }

}
