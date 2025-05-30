using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    [SerializeField] Tilemap tilemapGround;
    [SerializeField] Tilemap tilemapHitbox;
    [SerializeField] Tilemap tilemapHitboxSorted;
    [SerializeField] Tilemap tilemapMiddle;

    //List containing all tile scriptable objects
    [SerializeField] List<tileScriptableObject> tileScriptableObjects;
    //Dictionary to map tile bases to tilescript objects
    private Dictionary<TileBase, tileScriptableObject> dataFromFiles;

    // Start is called before the first frame update
    void Awake()
    {
        // This dictionary will store tiles with thier corresponding tile scriptable object
        // dataFromFiles[tile gameobject (not to be confused with the scriptable objects with the same name)] -> that tiles scriptable object
        dataFromFiles = new Dictionary<TileBase, tileScriptableObject>();


        // All walkable tiles are found within a tile scriptable objects "Tile Ground" parameter
        // This for loop goes through all the given scriptable objects in that god-forsaken list and pairs them up with the correct
        // tile gameobject by getting what that tile should be from the "Tile Ground" parameter

        // We only do this with "Tile Ground" because walkable areas are the only places we want bushes to be

        foreach (var tileData in tileScriptableObjects)
        {
            if(tileData.tileGround != null)
            {
                //Debug.Log(tileData.tileGround);
                dataFromFiles.Add(tileData.tileGround, tileData);
            }

        }

        // This WILL break if two tile scriptable objects refer to the same tile, so uhhhhh dont do that teehee :D
        

        /*
        if (testing)
        {
            generateInteractables();
        }
        */
        
        
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


                    //Code for avoiding objects in tilemap. Currently we have no tilemap, so it's commented out to avoid crashes
                    
                    Vector3 worldPosition = new Vector3(x, y, 0);
                    Vector3Int gridPosition = tilemapGround.WorldToCell(worldPosition);

                    // Given x y coords, returns the tile gameobject at those coords
                    TileBase foundGroundTile = tilemapGround.GetTile(gridPosition);

                    //Debug.Log(foundTile);
                    //Debug.Log("size of dict: " + dataFromFiles.Count);

                    //Dont spawn rock if theres hitboxes/middle tiles at the location
                    if (tilemapHitbox.GetTile(gridPosition) == null && tilemapHitboxSorted.GetTile(gridPosition) == null && tilemapMiddle.GetTile(gridPosition) == null)
                    {
                        // Use the dictionary to go to that tiles scriptable object and see if the "can place bush" bool is true
                        if (foundGroundTile != null && dataFromFiles.ContainsKey(foundGroundTile) && dataFromFiles[foundGroundTile].canPlaceBush)
                        {
                            //Add new spot to recent Values
                            pushToRecentValues(new Vector2Int(x, y));
                            //Debug.Log("Correctly spawned tile");
                            Instantiate(interactable, new Vector3(x + offset.x, y + offset.y, -1), Quaternion.identity, transform); // Z layer of interactables is -1
                        }
                    }

                    //pushToRecentValues(new Vector2Int(x, y));
                    //Instantiate(interactable, new Vector3(x + offset.x, y + offset.y, -1), Quaternion.identity, transform); // Z layer of interactables is -1
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
