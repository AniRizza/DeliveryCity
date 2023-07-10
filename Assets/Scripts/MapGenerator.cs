using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private int tileLength;
    private List<GameObject> createdTiles;

    public List<GameObject> tilePrefabs;
    public GameObject centralTilePrefab;
    public int mapSize;
    
    // Start is called before the first frame update
    void Start()
    {
        createdTiles = new List<GameObject>();
        tileLength = (int) (centralTilePrefab.GetComponent<BoxCollider>().size.x * centralTilePrefab.transform.localScale.x);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
