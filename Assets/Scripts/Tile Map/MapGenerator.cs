using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;
using Unity.AI.Navigation;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> roadTilePrefabs;

    public List<GameObject> buildingsTilePrefabsCurve;
    public List<GameObject> buildingsTilePrefabsStraight;
    public List<GameObject> buildingsTilePrefabs3Way;
    public List<GameObject> buildingsTilePrefabsCrossroad;

    private GameObject centralTile;
    public int xMin;
    public int xMax;
    public int yMin;
    public int yMax;

    private int tileLength;
    private List<GameObject> createdTiles;
    public NavMeshSurface surface;
    public GameObject parentObject;

    void Start() {
        createdTiles = new List<GameObject>();
        centralTile = GameObject.Find("Central_Tile");
        tileLength = (int) (centralTile.GetComponent<BoxCollider>().size.x * centralTile.transform.localScale.x);
        GenerateMap();
        GenerateCircularRoad();
        GenerateBuildingsDecor();
        surface.BuildNavMesh();
        GameObject.Find("DeliveryEventsController").GetComponent<DeliveryEventsController>().StartDeliveryEvents();
    }

    private void GenerateMap(){
        //createdTiles.Add((GameObject) Instantiate(centralTilePrefab, new Vector3(0, 0, 0), centralTilePrefab.transform.rotation));
        createdTiles.Add(centralTile);
        for (int i = 0; i < ((xMax - xMin + 1) * (yMax - yMin + 1)); i++) {
            ValidateAndCreateTile(createdTiles[i].GetComponent<TileCharacteristic>().GetX(), createdTiles[i].GetComponent<TileCharacteristic>().GetY() + 1);
            ValidateAndCreateTile(createdTiles[i].GetComponent<TileCharacteristic>().GetX() + 1, createdTiles[i].GetComponent<TileCharacteristic>().GetY());
            ValidateAndCreateTile(createdTiles[i].GetComponent<TileCharacteristic>().GetX(), createdTiles[i].GetComponent<TileCharacteristic>().GetY() - 1);
            ValidateAndCreateTile(createdTiles[i].GetComponent<TileCharacteristic>().GetX() - 1, createdTiles[i].GetComponent<TileCharacteristic>().GetY());
        }
    }

    private void ValidateAndCreateTile(int x, int y) {
        if (x >= xMin && x <= xMax && y >= yMin && y <= yMax) {
            if (GetTileByCoordinates(x, y).Count == 0) {
                bool isRoadOnTop = true;
                bool isRoadOnRight = true;
                bool isRoadOnBot = true;
                bool isRoadOnLeft = true;

                var topTile = GetTileByCoordinates(x, y + 1);
                if (topTile.Count != 0) isRoadOnTop = topTile[0].GetComponent<TileCharacteristic>().GetIsRoadOnBot();
                var rightTile = GetTileByCoordinates(x + 1, y);
                if (rightTile.Count != 0) isRoadOnRight = rightTile[0].GetComponent<TileCharacteristic>().GetIsRoadOnLeft();
                var botTile = GetTileByCoordinates(x, y - 1);
                if (botTile.Count != 0) isRoadOnBot = botTile[0].GetComponent<TileCharacteristic>().GetIsRoadOnTop();
                var leftTile = GetTileByCoordinates(x - 1, y);
                if (leftTile.Count != 0) isRoadOnLeft = leftTile[0].GetComponent<TileCharacteristic>().GetIsRoadOnRight();

                CreateTileConsiderable(isRoadOnTop, isRoadOnRight, isRoadOnBot, isRoadOnLeft,
                                    topTile.Count != 0, rightTile.Count != 0, botTile.Count != 0, leftTile.Count != 0, x, y);
            }
            //else Debug.Log("invalid not empty");
        }
        //else Debug.Log("invalid border");
    }

    private List<GameObject> GetTileByCoordinates(int x, int y) {
        List<GameObject> result = createdTiles.FindAll(tile => 
            (tile.GetComponent<TileCharacteristic>().GetX() == x) && (tile.GetComponent<TileCharacteristic>().GetY() == y));
        return result;
    }

    private void CreateTileConsiderable(bool isRoadOnTop, bool isRoadOnRight, bool isRoadOnBot, bool isRoadOnLeft,
                                        bool isTopConsiderable, bool isRightConsiderable, bool isBotConsiderable, bool isLeftConsiderable,
                                        int x, int y) { 
        List<GameObject> allowedTilesList = new List<GameObject>();
        allowedTilesList.AddRange(roadTilePrefabs);

        if (isTopConsiderable) allowedTilesList.RemoveAll(prefab => prefab.GetComponent<TileCharacteristic>().GetIsRoadOnTop() != isRoadOnTop);
        if (isRightConsiderable) allowedTilesList.RemoveAll(prefab => prefab.GetComponent<TileCharacteristic>().GetIsRoadOnRight() != isRoadOnRight);
        if (isBotConsiderable) allowedTilesList.RemoveAll(prefab => prefab.GetComponent<TileCharacteristic>().GetIsRoadOnBot() != isRoadOnBot);
        if (isLeftConsiderable) allowedTilesList.RemoveAll(prefab => prefab.GetComponent<TileCharacteristic>().GetIsRoadOnLeft() != isRoadOnLeft);

        List<int> indexArrayForRandomization = new List<int>();
        for (int i = 0; i < allowedTilesList.Count; i++) {
            int possibility = 0;
            switch (allowedTilesList[i].GetComponent<TileCharacteristic>().GetTileType()) {
                case 4:
                    possibility = POSSIBILITY_CROSSROAD;
                    break;
                case 3:
                    possibility = POSSIBILITY_3WAY;
                    break;
                case 2:
                    possibility = POSSIBILITY_CURVE;
                    break;
                case 1:
                    possibility = POSSIBILITY_STRAIGHT;
                    break;
            }
            for (int j = 0; j < possibility; j++)
                indexArrayForRandomization.Add(i);
        }
        
        //List<string> strings = indexArrayForRandomization.ConvertAll<string>(x => x.ToString());
        //Debug.Log(String.Join(", ", strings));

        int winner = UnityEngine.Random.Range(0,indexArrayForRandomization.Count);
        GameObject newTile = Instantiate(allowedTilesList[indexArrayForRandomization[winner]], 
                new Vector3(x, 0, y) * tileLength, allowedTilesList[indexArrayForRandomization[winner]].transform.rotation, parentObject.transform);
        newTile.GetComponent<TileCharacteristic>().SetCoordinates(x, y);
        createdTiles.Add(newTile);
        //Debug.Log("New tile added with coordinates " + x + " and " + y);
    }

    private void GenerateCircularRoad() {
        Vector2 direction = new Vector2(0, -1);
        int x = xMax + 1;
        int y = yMax;
        bool isRoadOnTopCorner = false;
        bool isRoadOnRightCorner = false;
        bool isRoadOnBotCorner = true;
        bool isRoadOnLeftCorner = true;

        for (int i = 0; i < ((xMax - xMin + 1) * 2 + (yMax - yMin + 1) * 2 + 4); i++) {
            bool isRoadOnTop = true;
            bool isRoadOnRight = true;
            bool isRoadOnBot = true;
            bool isRoadOnLeft = true;
            
            if (((x == xMax + 1) || (x == xMin - 1)) && ((y == yMax + 1) || (y == yMin - 1))) {              
                if ((x == xMin - 1) && (y == yMin - 1)) {
                    direction.x = Math.Abs(direction.x);
                    direction.y = Math.Abs(direction.y);
                }
                var tempDir = direction.x;
                direction.x = direction.y;
                direction.y = tempDir;

                bool temp = isRoadOnTopCorner;
                isRoadOnTopCorner = isRoadOnLeftCorner;
                isRoadOnLeftCorner = isRoadOnBotCorner;
                isRoadOnBotCorner = isRoadOnRightCorner;
                isRoadOnRightCorner = temp;

                isRoadOnTop = isRoadOnTopCorner;
                isRoadOnRight = isRoadOnRightCorner;
                isRoadOnBot = isRoadOnBotCorner;
                isRoadOnLeft = isRoadOnLeftCorner;
            }
            else {
                if (direction.x == 0) {
                    if (direction.y < 0) {
                        isRoadOnRight = false;
                        isRoadOnLeft = GetTileByCoordinates(x - 1, y)[0].GetComponent<TileCharacteristic>().GetIsRoadOnRight();
                    }
                    else {
                        isRoadOnLeft = false;
                        isRoadOnRight = GetTileByCoordinates(x + 1, y)[0].GetComponent<TileCharacteristic>().GetIsRoadOnLeft();
                    }
                }
                else {
                    if (direction.x < 0) {
                        isRoadOnBot = false;
                        isRoadOnTop = GetTileByCoordinates(x, y + 1)[0].GetComponent<TileCharacteristic>().GetIsRoadOnBot();
                    }
                    else {
                        isRoadOnTop = false;
                        isRoadOnBot = GetTileByCoordinates(x, y - 1)[0].GetComponent<TileCharacteristic>().GetIsRoadOnTop();
                    }
                }
            }
            CreateTileConsiderable(isRoadOnTop, isRoadOnRight, isRoadOnBot, isRoadOnLeft,
                                    true, true, true, true, x, y);
            x += (int) direction.x;
            y += (int) direction.y;
        }
    }

    private void GenerateBuildingsDecor() {
        for (int i = 1; i < createdTiles.Count; i++) {
            List<GameObject> buildingsTilesList = new List<GameObject>();
            switch (createdTiles[i].GetComponent<TileCharacteristic>().GetTileType()) {
                case 4:
                    buildingsTilesList.AddRange(buildingsTilePrefabsCrossroad);
                    break;
                case 3:
                    buildingsTilesList.AddRange(buildingsTilePrefabs3Way);
                    break;
                case 2:
                    buildingsTilesList.AddRange(buildingsTilePrefabsCurve);
                    break;
                case 1:
                    buildingsTilesList.AddRange(buildingsTilePrefabsStraight);
                    break;
            }
            int winner = UnityEngine.Random.Range(0,buildingsTilesList.Count);

            Instantiate(buildingsTilesList[winner],
                    new Vector3(createdTiles[i].GetComponent<TileCharacteristic>().GetX(), 0, createdTiles[i].GetComponent<TileCharacteristic>().GetY()) * tileLength,
                    buildingsTilesList[winner].transform.rotation * Quaternion.Euler (0f, 90f * createdTiles[i].GetComponent<TileCharacteristic>().
                    GetRotationParameter(), 0f), parentObject.transform);
        }    
    }
}
