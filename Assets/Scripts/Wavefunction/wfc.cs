using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static wfc;
using UnityEngine.Tilemaps;

public class wfc : MonoBehaviour
{
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap hitboxesTileMap;
    [SerializeField] Tilemap middleTileMap;
    [SerializeField] Tilemap aboveTileMap;

    [SerializeField] tileScriptableObject edgeTile;

    [SerializeField] private tileScriptableObject[] tileScriptableObjects;

    [SerializeField] InteractableGenerator interactableGenerating;

    private static int sizeX = 130;
    private static int sizeY = 130;

    public enum Direction { North, South, East, West };

    private static Dictionary<ushort, List<string>> tileRules = new Dictionary<ushort, List<string>>();

    private static Dictionary<ushort, float> tileWeights = new Dictionary<ushort, float>();

    private Tile[,] tiles = new Tile[sizeX, sizeY];

    private float timeStart;

    private void Start()
    {
        //Make tile rules, probably have a better way for designers to change this later
        for (int i = 0; i < tileScriptableObjects.Length; i++)
        {
            tileScriptableObject temp = tileScriptableObjects[i];
            tileRules.Add((ushort)i, new List<string> { temp.edgeNorthLeft + "_" + temp.edgeNorthRight, temp.edgeSouthLeft + "_" + temp.edgeSouthRight, temp.edgeEastUp + "_" + temp.edgeEastDown, temp.edgeWestUp + "_" + temp.edgeWestDown });
            tileWeights.Add((ushort)i, temp.weight);
        }

        //Initialize tile array
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                tiles[x, y] = new Tile();
            }
        }

        //Add neighbors
        //Has to be a separate loop cuz the tiles need to already be initialized
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                //Add neighbors
                //North
                if (y + 1 < sizeY)
                {
                    tiles[x, y].AddNeighbor(Direction.North, tiles[x, y + 1]);
                }
                //South
                if (y > 0)
                {
                    tiles[x, y].AddNeighbor(Direction.South, tiles[x, y - 1]);
                }
                //East
                if (x + 1 < sizeX)
                {
                    tiles[x, y].AddNeighbor(Direction.East, tiles[x + 1, y]);
                }
                //West
                if (x > 0)
                {
                    tiles[x, y].AddNeighbor(Direction.West, tiles[x - 1, y]);
                }

            }
        }

        GetSeededTiles();

        bool done = false;
        while (done == false)
        {
            done = WaveFunctionCollapse();
        }

        PlaceTiles();
        //StartCoroutine(testWFCFastButOnlyIfISaySo()); //Do it fast
        //StartCoroutine(testWFCSlowly()); // Does the generation slowly, only have one uncommented
    }

    private IEnumerator testWFCSlowly()
    {
        bool done = false;
        while (done == false)
        {
            if (Input.GetKey("e"))                 //Uncomment these lines to press e to spawn
            {
                for (int i = 0; i <= 25; i++)
                    done = WaveFunctionCollapse();
                PlaceTiles();
            }
            yield return null;
        }
        //interactableGenerating.generateInteractables(); //Calls the other script (interactable spawning) to start
    }

    private IEnumerator testWFCFastButOnlyIfISaySo()
    {
        bool done = false;
        timeStart = 0f;

        while (done == false)
        {
            yield return null;
            if (Input.GetKey("e"))
            {
                timeStart = Time.realtimeSinceStartup;
                while (done == false)
                {
                    done = WaveFunctionCollapse();
                }
                PlaceTiles();
            }
        }

        Debug.Log("Time taken to wfc: " + ((Time.realtimeSinceStartup - timeStart) * 1000) + " ms");

        //interactableGenerating.generateInteractables(); //Calls the other script (interactable spawning) to start

    }

    private void GetSeededTiles()
    {
        TileBase tileGetGround = null;
        TileBase tileGetHitbox = null;
        TileBase tileGetMiddle = null;
        TileBase tileGetAbove = null;
        Stack<Tile> tileStack = new Stack<Tile>();

        ushort edgeTileID = 0;
        //Get rock tile for edges
        for (int i = 0; i < tileScriptableObjects.Length; i++)
        {
            if (tileScriptableObjects[i] == edgeTile)
            {
                edgeTileID = (ushort)i;
                break;
            }
        }

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                //Make edges rocks
                if (x == 0 || x == sizeX - 1 || y == 0 || y == sizeY - 1)
                {
                    tiles[x, y].SetPossibilities(new List<ushort> { edgeTileID });
                    tileStack.Push(tiles[x, y]);
                    continue;
                }

                tileGetGround = groundTilemap.GetTile(new Vector3Int(x, y, 0));
                tileGetHitbox = hitboxesTileMap.GetTile(new Vector3Int(x, y, 0));
                tileGetMiddle = middleTileMap.GetTile(new Vector3Int(x, y, 0));
                tileGetAbove = aboveTileMap.GetTile(new Vector3Int(x, y, 0));

                for (int i = 0; i < tileScriptableObjects.Length; i++)
                {
                    if (tileScriptableObjects[i].tileGround == tileGetGround && 
                        tileScriptableObjects[i].tileHitbox == tileGetHitbox && 
                        tileScriptableObjects[i].tileMiddle == tileGetMiddle &&
                        tileScriptableObjects[i].tileAbove == tileGetAbove)
                    {
                        tiles[x, y].SetPossibilities(new List<ushort> { (ushort)i });
                        tileStack.Push(tiles[x, y]);
                    } 
                }
            }
        }           

        //Copy pasted from WaveFunctionCollapse()
        //uhh clean this up later
        Tile tempTile;
        bool reduced = false;
        List<ushort> tempTilePossibilities;
        List<Direction> tempTileDirections;
        while (tileStack.Count > 0)
        {
            tempTile = tileStack.Pop();
            tempTilePossibilities = tempTile.GetPossibilities();
            tempTileDirections = tempTile.GetDirections();
            for (int i = 0; i < tempTileDirections.Count; i++)
            {
                Tile tempTileNeighbor = tempTile.GetNeighbor(tempTileDirections[i]);
                if (tempTileNeighbor != null && tempTileNeighbor.GetEntropy() > 1)
                {
                    //Debug.Log(i + "    e   ");
                    reduced = tempTileNeighbor.Constrain(tempTilePossibilities, tempTileDirections[i]);
                    if (reduced == true)
                    {
                        tileStack.Push(tempTileNeighbor);
                    }
                }
            }
        }

    }

    private void PlaceTiles()
    {
        bool hasAHitBox = false;
        TileBase tileToPlaceGround = null;
        TileBase tileToPlaceHitbox = null;
        TileBase tileToPlaceMiddle = null;
        TileBase tileToPlaceAbove = null;

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                if (tiles[x, y].GetEntropy() <= 1)
                {
                    tileToPlaceGround = tileScriptableObjects[tiles[x, y].GetPossibilities()[0]].tileGround;
                    tileToPlaceHitbox = tileScriptableObjects[tiles[x, y].GetPossibilities()[0]].tileHitbox;
                    tileToPlaceMiddle = tileScriptableObjects[tiles[x, y].GetPossibilities()[0]].tileMiddle;
                    tileToPlaceAbove = tileScriptableObjects[tiles[x, y].GetPossibilities()[0]].tileAbove;

                    if (tileToPlaceGround != null)
                    {
                        groundTilemap.SetTile(new Vector3Int(x, y, 0), tileToPlaceGround);
                    }
                    if (tileToPlaceHitbox != null)
                    {
                        hitboxesTileMap.SetTile(new Vector3Int(x, y, 0), tileToPlaceHitbox);
                    }
                    if (tileToPlaceMiddle != null)
                    {
                        middleTileMap.SetTile(new Vector3Int(x, y, 0), tileToPlaceMiddle);
                    }
                    if (tileToPlaceAbove != null)
                    {
                        aboveTileMap.SetTile(new Vector3Int(x, y, 0), tileToPlaceAbove);
                    }
                }
            }
        }
    }

    private List<Tile> GetTilesLowestEntropy()
    {
        int lowestEntropy = new List<ushort>(tileRules.Keys).Count; //number of types of tiles
        int tempEntropy;
        List<Tile> tileList = new List<Tile>();

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                tempEntropy = tiles[x, y].GetEntropy();
                if (tempEntropy > 1)
                {
                    if (tempEntropy < lowestEntropy)
                    {
                        tileList.Clear();
                        lowestEntropy = tempEntropy;
                    }
                    if (tempEntropy == lowestEntropy)
                    {
                        tileList.Add(tiles[x, y]);
                    }
                }
            }
        }
        return tileList;
    }

    //Returns true if the map is already fully generated
    public bool WaveFunctionCollapse()
    {
        List<Tile> tilesLowestEntropy = GetTilesLowestEntropy();
        //Debug.Log("GetTilesLowestEntropy: " + ((Time.realtimeSinceStartup - timeStart) * 1000) + " ms");
        if (tilesLowestEntropy.Count == 0)
        {
            return true;
        }

        //Pick a random lowest entropy tile to collapse
        Tile tileToCollapse = tilesLowestEntropy[Random.Range(0, tilesLowestEntropy.Count)];
        tileToCollapse.Collapse();

        Stack<Tile> tileStack = new Stack<Tile>();
        tileStack.Push(tileToCollapse);
        //Debug.Log("Collapse Random Tile: " + ((Time.realtimeSinceStartup - timeStart) * 1000) + " ms");

        Tile tempTile;
        bool reduced = false;
        List<ushort> tempTilePossibilities;
        List<Direction> tempTileDirections;
        while (tileStack.Count > 0)
        {
            tempTile = tileStack.Pop();
            tempTilePossibilities = tempTile.GetPossibilities();
            tempTileDirections = tempTile.GetDirections();
            for (int i = 0; i < tempTileDirections.Count; i++)
            {
                Tile tempTileNeighbor = tempTile.GetNeighbor(tempTileDirections[i]);
                if (tempTileNeighbor != null && tempTileNeighbor.GetEntropy() > 1)
                {
                    //Debug.Log(i + "    e   ");
                    reduced = tempTileNeighbor.Constrain(tempTilePossibilities, tempTileDirections[i]);
                    if (reduced == true)
                    {
                        tileStack.Push(tempTileNeighbor);
                    }
                }
            }
        }
        //Debug.Log("Constrain Stuff: " + ((Time.realtimeSinceStartup - timeStart) * 1000) + " ms");
        return false;
    }

    //Each tile stores its posibilities, entropy, and references to neighbors
    public class Tile
    {
        private List<ushort> possibilities;
        private Dictionary<Direction, Tile> neighbors = new Dictionary<Direction, Tile>();

        public Tile()
        {
            possibilities = new List<ushort>(tileRules.Keys);
        }

        public void AddNeighbor(Direction direction, Tile neighbor)
        {
            neighbors.Add(direction, neighbor);
        }

        public Tile GetNeighbor(Direction direction)
        {
            return neighbors[direction];
        }

        //Tiles on the edges will return smaller lists
        public List<Direction> GetDirections()
        {
            return new List<Direction>(neighbors.Keys);
        }

        public List<ushort> GetPossibilities()
        {
            return possibilities;
        }

        public void SetPossibilities(List<ushort> newPossibilities)
        {
            possibilities = newPossibilities;
        }

        public int GetEntropy()
        {
            return possibilities.Count;
        }

        public void Collapse()
        {
            //Calculate total weight
            float tileWeightsSum = 0.0f;
            for (int i = 0; i < possibilities.Count; i++)
            {
                tileWeightsSum += tileWeights[possibilities[i]];
            }
            //Pick a random number less than total sum of weights
            float random = Random.Range(0f, tileWeightsSum);
            //Go through all possibilities, subtracting their weight each time until its less than 0
            ushort randomPossibility;
            for (int i = 0; i < possibilities.Count; i++)
            {
                if (random <= tileWeights[possibilities[i]])
                {
                    randomPossibility = possibilities[i];
                    possibilities = new List<ushort> { randomPossibility };
                    return;
                }
                random -= tileWeights[possibilities[i]];
            }
            Debug.Log("SOMETHING HAS GONE VERY, VERYYY WRONG!!!!!!!!!!!!");
        }

        //Direction is the direction it is being constrained FROM
        public bool Constrain(List<ushort> neighbourPossibilities, Direction direction)
        {
            bool reduced = false;

            if (possibilities.Count > 1)
            {
                List<string> connectors = new List<string>();
                for (int i = 0; i < neighbourPossibilities.Count; i++)
                {
                    connectors.Add(tileRules[neighbourPossibilities[i]][(int)direction]);
                }

                Direction oppositeDirection = Direction.North;
                if (direction == Direction.North)
                {
                    oppositeDirection = Direction.South;
                }
                else if (direction == Direction.South)
                {
                    oppositeDirection = Direction.North;
                }
                else if (direction == Direction.East)
                {
                    oppositeDirection = Direction.West;
                }
                else if (direction == Direction.West)
                {
                    oppositeDirection = Direction.East;
                }
                else
                {
                    Debug.Log("A GRAVE ERROR HAS OCCURRED");
                }

                List<ushort> possibilitiesCopy = new List<ushort>(possibilities);
                for (int i = 0; i < possibilitiesCopy.Count; i++)
                {
                    ushort currentPossibility = possibilitiesCopy[i];
                    List<string> currentPossibilityEdges = tileRules[currentPossibility];

                    if (!connectors.Contains(currentPossibilityEdges[(int)oppositeDirection]))
                    {
                        possibilities.Remove(currentPossibility);
                        reduced = true;
                    }
                }
            }

            return reduced;
        }
    }
}
