using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> tilePrefabs;
    public GameObject centralTilePrefab;
    public int mapSize;

    private int tileLength;
    private List<GameObject> createdTiles;

    // Start is called before the first frame update
    void Start() {
        tileLength = (int) (centralTilePrefab.GetComponent<BoxCollider>().size.x * centralTilePrefab.transform.localScale.x);
        createdTiles = new List<GameObject>();

        GenerateMap(- mapSize / 2, mapSize / 2, - mapSize / 2, mapSize / 2);
        GenerateCircularRoad();
    }

    private void GenerateMap(int xMin, int xMax, int zMin, int zMax) {
        createdTiles.Add((GameObject) Instantiate(centralTilePrefab, new Vector3(0, 0, 0), centralTilePrefab.transform.rotation));
        for (int i = 0; i < (mapSize * mapSize); i++) {
            //Top tile work

            //check if top tile coordinates is empty
            if (IsCoordinatesEmpty(createdTiles[i].GetComponent<TileCharacteristic>().x + 1, createdTiles[i].GetComponent<TileCharacteristic>().z)) {
                //check if top tile is not out of border
                if ((createdTiles[i].GetComponent<TileCharacteristic>().x + 1) <= xMax) {
                    CreateNewCityTile(createdTiles[i].GetComponent<TileCharacteristic>().x + 1, createdTiles[i].GetComponent<TileCharacteristic>().z);
                }
                else Debug.Log("Coordinates out of border " + (createdTiles[i].GetComponent<TileCharacteristic>().x + 1) + " and " + createdTiles[i].GetComponent<TileCharacteristic>().z);
            }
            else Debug.Log("Coordinates not empty " + (createdTiles[i].GetComponent<TileCharacteristic>().x + 1) + " and " + createdTiles[i].GetComponent<TileCharacteristic>().z);

            //Right tile work

            //check if right tile coordinates is empty
            if (IsCoordinatesEmpty(createdTiles[i].GetComponent<TileCharacteristic>().x, createdTiles[i].GetComponent<TileCharacteristic>().z - 1)) {
                //check if right tile is not out of border
                if ((createdTiles[i].GetComponent<TileCharacteristic>().z - 1) >= zMin) {
                    CreateNewCityTile(createdTiles[i].GetComponent<TileCharacteristic>().x, createdTiles[i].GetComponent<TileCharacteristic>().z - 1);
                }
                else Debug.Log("Coordinates out of border" + createdTiles[i].GetComponent<TileCharacteristic>().x + " and " + (createdTiles[i].GetComponent<TileCharacteristic>().z - 1));
            }
            else Debug.Log("Coordinates not empty " + createdTiles[i].GetComponent<TileCharacteristic>().x + " and " + (createdTiles[i].GetComponent<TileCharacteristic>().z - 1));

            //Bottom tile work

            //check if bottom tile coordinates is empty
            if (IsCoordinatesEmpty(createdTiles[i].GetComponent<TileCharacteristic>().x - 1, createdTiles[i].GetComponent<TileCharacteristic>().z)) {
                //check if bottom tile is not out of border
                if ((createdTiles[i].GetComponent<TileCharacteristic>().x - 1) >= xMin) {
                    CreateNewCityTile(createdTiles[i].GetComponent<TileCharacteristic>().x - 1, createdTiles[i].GetComponent<TileCharacteristic>().z);
                }
                else Debug.Log("Coordinates out of border " + (createdTiles[i].GetComponent<TileCharacteristic>().x - 1) + " and " + createdTiles[i].GetComponent<TileCharacteristic>().z);
            }
            else Debug.Log("Coordinates not empty " + (createdTiles[i].GetComponent<TileCharacteristic>().x - 1) + " and " + createdTiles[i].GetComponent<TileCharacteristic>().z);

            //Left tile work

            //check if left tile coordinates is empty
            if (IsCoordinatesEmpty(createdTiles[i].GetComponent<TileCharacteristic>().x, createdTiles[i].GetComponent<TileCharacteristic>().z + 1)) {
                //check if left tile is not out of border
                if ((createdTiles[i].GetComponent<TileCharacteristic>().z + 1) <= zMax) {
                    CreateNewCityTile(createdTiles[i].GetComponent<TileCharacteristic>().x, createdTiles[i].GetComponent<TileCharacteristic>().z + 1);
                }
                else Debug.Log("Coordinates out of border " + createdTiles[i].GetComponent<TileCharacteristic>().x + " and " + (createdTiles[i].GetComponent<TileCharacteristic>().z + 1));
            }
            else Debug.Log("Coordinates not empty " + createdTiles[i].GetComponent<TileCharacteristic>().x + " and " + (createdTiles[i].GetComponent<TileCharacteristic>().z + 1));
        }
    }

    private bool IsCoordinatesEmpty(int x, int z) {
        bool result = true;
        foreach (var tile in createdTiles) {
            if (tile.GetComponent<TileCharacteristic>().x == x && tile.GetComponent<TileCharacteristic>().z ==z)
                result = false;
        }
        return result;
    }

    private GameObject GetTileByCoordinates(int x, int z) {
        GameObject result = null;
        foreach (var tile in createdTiles) {
            if (tile.GetComponent<TileCharacteristic>().x == x && tile.GetComponent<TileCharacteristic>().z == z)
                result = tile;
        }
        return result;
    }

    private void CreateNewCityTile(int x, int z) {
        List<GameObject> allowedTilesList = new List<GameObject>();
        allowedTilesList.AddRange(tilePrefabs);
        //if top tile exist then take it position into consideration
        if (!IsCoordinatesEmpty(x + 1, z)){
            allowedTilesList.RemoveAll(prefab => prefab.GetComponent<TileCharacteristic>().isRoadOnTop !=
                GetTileByCoordinates((x + 1), z).GetComponent<TileCharacteristic>().isRoadOnBot);
            Debug.Log("Top tile taken in consider");
        }
        //if right tile exist then take it position into consideration
        if (!IsCoordinatesEmpty(x, z - 1)){
            allowedTilesList.RemoveAll(prefab => prefab.GetComponent<TileCharacteristic>().isRoadOnRight !=
                GetTileByCoordinates(x, (z - 1)).GetComponent<TileCharacteristic>().isRoadOnLeft);
            Debug.Log("Right tile taken in consider");
        }
        //if bottom tile exist then take it position into consideration
        if (!IsCoordinatesEmpty(x - 1, z)){
            allowedTilesList.RemoveAll(prefab => prefab.GetComponent<TileCharacteristic>().isRoadOnBot !=
                GetTileByCoordinates((x - 1), z).GetComponent<TileCharacteristic>().isRoadOnTop);
            Debug.Log("Bottom tile taken in consider");
        }
        //if left tile exist then take it position into consideration
        if (!IsCoordinatesEmpty(x, z + 1)){
            allowedTilesList.RemoveAll(prefab => prefab.GetComponent<TileCharacteristic>().isRoadOnLeft !=
                GetTileByCoordinates(x, (z + 1)).GetComponent<TileCharacteristic>().isRoadOnRight);
            Debug.Log("Left tile taken in consider");
        }
        if (allowedTilesList.Count != 0) {
            //select random tile from the allowed list
            int randomTileIndex = UnityEngine.Random.Range(0, allowedTilesList.Count);
            GameObject newTile = Instantiate(
                allowedTilesList[randomTileIndex],
                new Vector3(x, 0, z) * tileLength,
                allowedTilesList[randomTileIndex].transform.rotation);
            newTile.GetComponent<TileCharacteristic>().SetCoordinates(x, z);
            createdTiles.Add(newTile);
            Debug.Log("New tile added with coordinates " + x + " and " + z);
        }
        else Debug.Log("No tiles to chouse from");
    }

    private void GenerateCircularRoad() {
        Vector3 direction = new Vector3(0, 0, -1);
        int x = mapSize / 2 + 1;
        int z = mapSize / 2;

        for (int i = 0; i < (mapSize * 4 + 4); i++) {
            bool borderExpectedTop = true;
            bool borderExpectedBot = true;
            bool borderExpectedLeft = true;
            bool borderExpectedRight = true;

            if (Math.Abs(x) == Math.Abs(z)) {                
                if (direction.x < direction.z) {
                    if (direction.z == 0) {
                        direction.z++;
                        borderExpectedRight = false;
                    }
                    else {
                        direction.z--;
                        borderExpectedLeft = false;
                    }
                    direction.x++;
                    borderExpectedBot = false;
                }
                else {
                    if (direction.z == 0) {
                        direction.z--;
                        borderExpectedLeft = false;
                    }
                    else {
                        direction.z++;
                        borderExpectedRight = false;
                    }
                    direction.x--;
                    borderExpectedTop = false;
                }
                Debug.Log("New direction vector " + direction.x + " and " + direction.z); 
            }
            else {
                if (direction.z == 0) {
                    if (direction.x < 0) {
                        borderExpectedRight = false;
                        Debug.Log("coordinates " + x + " and " + (z+1));
                        borderExpectedLeft = GetTileByCoordinates(x, (z + 1)).GetComponent<TileCharacteristic>().isRoadOnRight;
                    }
                    else {
                        borderExpectedLeft = false;
                        Debug.Log("coordinates " + x + " and " + (z-1));
                        borderExpectedRight = GetTileByCoordinates(x, (z - 1)).GetComponent<TileCharacteristic>().isRoadOnLeft;
                    }
                }
                else {
                    if (direction.z < 0) {
                        borderExpectedTop = false;
                        Debug.Log("coordinates " + (x-1) + " and " + (z));
                        borderExpectedBot = GetTileByCoordinates((x - 1), z).GetComponent<TileCharacteristic>().isRoadOnTop;
                    }
                    else {
                        borderExpectedBot = false;
                        Debug.Log("coordinates " + (x+1) + " and " + (z));
                        borderExpectedTop = GetTileByCoordinates((x + 1), z).GetComponent<TileCharacteristic>().isRoadOnBot;
                    }
                }
            }

            var borderTilePrefab = GetPrefabByTileCharacteristic(borderExpectedTop, borderExpectedBot, borderExpectedRight, borderExpectedLeft);
            GameObject newTile = Instantiate(borderTilePrefab, new Vector3(x, 0, z) * tileLength, borderTilePrefab.transform.rotation);
            newTile.GetComponent<TileCharacteristic>().SetCoordinates(x, z);
            createdTiles.Add(newTile);
            Debug.Log("New circular road tile added with coordinates " + x + " and " + z);

            x += (int) direction.x;
            z += (int) direction.z;
        }
    }

    private GameObject GetPrefabByTileCharacteristic(bool isRoadOnTop, bool isRoadOnBot, bool isRoadOnRight, bool isRoadOnLeft) {
        List<GameObject> result = tilePrefabs.FindAll(prefab => 
            (prefab.GetComponent<TileCharacteristic>().isRoadOnTop == isRoadOnTop) &&
            (prefab.GetComponent<TileCharacteristic>().isRoadOnBot == isRoadOnBot) &&
            (prefab.GetComponent<TileCharacteristic>().isRoadOnRight == isRoadOnRight) &&
            (prefab.GetComponent<TileCharacteristic>().isRoadOnLeft == isRoadOnLeft));
        return result[0];
    }
}
