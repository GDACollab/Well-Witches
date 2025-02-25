using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractableGenerator : MonoBehaviour
{
    [SerializeField] perlinNoiseMap perlinNoiseGen;
    [SerializeField] bool testing = true;
    public GameObject interactable;
    public Vector2 mapSize = new Vector2(100,100);
    //offset moves the interactables so that they're centered around (0,0) when spawned, rather than having 0,0 be the bottom left corner
    [SerializeField] Vector2 offset = new Vector2(-50, -50);
    //queue structure tracking the 3 most recent spots
    [SerializeField] Vector2Int[] recentValues = { new Vector2Int(-10, -10), new Vector2Int(-10, -10), new Vector2Int(-10, -10) };
    [SerializeField] float spawnCutoff = 0.75f;
    //min distance from recent values nessecary for a new interactable to be spawned
    [SerializeField] int recentRange = 3;
    [SerializeField] Tilemap tilemap;
    //List containing all tile scriptable objects
    [SerializeField] List<tileScriptableObject> tileScriptableObjects;
    //Dictionary to map tile bases to tilescript objects
    private Dictionary<TileBase, tileScriptableObject> dataFromFiles;

    // Start is called before the first frame update
    void Start()
    {
        dataFromFiles = new Dictionary<TileBase, tileScriptableObject>();

        foreach(var tileData in tileScriptableObjects)
        {
            dataFromFiles.Add(tileData.tileGround, tileData);
            dataFromFiles.Add(tileData.tileHitbox, tileData);
        }

        if (testing)
        {
            generateInteractables();
        }
    }

    public void generateInteractables()
    {
        //skip very edges; we don't want interactables to spawn there
        for(int x = 1; x < mapSize.x -1; x++)
        {
            for (int y = 1; y < mapSize.y - 1; y++)
            {
                //if high enough value...
                if(getValFromPerlinNoise(x,y) >= spawnCutoff)
                {
                    float spotVal = getValFromPerlinNoise(x, y);
                    bool isValid = true;
                    //check neighboring spots (cardinal directions) to see if it's a 'peak' or within a 'peak' of values
                    Vector2Int[] cardDirect = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
                    foreach (Vector2Int cardinal in cardDirect)
                    {
                        //We don't need a check for breaking out of the map since we're skipping the edges
                        //skip spots that would be outside the map
                        //if (x + cardinal.x < 0 || x + cardinal.x > mapSize.x || y + cardinal.y < 0 || y + cardinal.y > mapSize.y)
                        //    continue;

                        if(spotVal < getValFromPerlinNoise(x + cardinal.x, y + cardinal.y)) //if neighbor is greater (not lesser/equal), it's not a peak
                        {
                            isValid = false;
                            break;
                        }
                    }
                    //if it's NOT a peak in values, don't spawn anything
                    if (!isValid)
                    {
                        continue;
                    }
                    //go through recent spots; if any are too close, skip
                    foreach(Vector2Int value in recentValues)
                    {
                        //since we're !currently! going from 0 upwards, we don't need to check if set to a lower number.
                        if(value.x + recentRange > x && value.y + recentRange > y) //if within range, it's not a valid position
                        {
                            isValid = false;
                            break;
                        }
                    }
                    if (!isValid)
                    {
                        continue;
                    }

                    Vector3 worldPosition = new Vector3(x, y, 0);
                    Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

                    TileBase foundTile = tilemap.GetTile(gridPosition);
                    
                    if (foundTile != null && dataFromFiles[foundTile].canPlaceBush)
                    {
                        //Add new spot to recent Values
                        pushToRecentValues(new Vector2Int(x, y));
                        Instantiate(interactable, new Vector3(x + offset.x, y + offset.y, -1), Quaternion.identity, transform); // Z layer of interactables is -1
                    }

                }
            }
        }
    }

    //pushes a value to the recentValues queue, pushing all previous items 'back' and out of the queue
    void pushToRecentValues(Vector2Int pushedVal)
    {
        for(int i = 0; i < recentValues.Length - 1; i++)
        {
            recentValues[i] = recentValues[i + 1];
        }
        recentValues[recentValues.Length - 1] = pushedVal;
    }

    //gets a perlin noise value
    public float getValFromPerlinNoise(int x, int y)
    {
        float myVal = perlinNoiseGen.getFloatUsingPerlin(x, y);
        //Debug.Log(myVal);
        return myVal;
        //temp code for testing; implement perlin reading later
        //return ((float)x + 1.5f * (float)y) % 2;
    }
}
