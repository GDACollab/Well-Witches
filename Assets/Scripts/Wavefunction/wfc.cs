using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static wfc;
using UnityEngine.Tilemaps;

/*
DIRECTIONS:
1 - north
2 - south
3 - east
4 - west
*/

public class wfc : MonoBehaviour
{
    [SerializeField] TileBase grassTile;
    [SerializeField] TileBase grassFlowerTile;
    [SerializeField] TileBase waterTile;
    [SerializeField] TileBase sandTile;
    [SerializeField] TileBase grassSandSouthTile;
    [SerializeField] TileBase grassSandEastTile;
    [SerializeField] TileBase grassSandNorthTile;
    [SerializeField] TileBase grassSandWestTile;
    [SerializeField] TileBase grassSandSouthEastTile;
    [SerializeField] TileBase grassSandSouthWestTile;
    [SerializeField] TileBase grassSandNorthEastTile;
    [SerializeField] TileBase grassSandNorthWestTile;

    [SerializeField] Tilemap groundTilemap;

    private static int sizeX = 25;
    private static int sizeY = 25;

    private static Dictionary<string, List<string>> tileRules = new Dictionary<string, List<string>>();

    private Tile[,] tiles = new Tile[sizeX, sizeY];

    private void Start()
    {
        //Make tile rules, probably have a better way for designers to change this later

        tileRules.Add("grass", new List<string>{"grass", "grass", "grass", "grass" });
        tileRules.Add("grass_flower", new List<string>{ "grass", "grass", "grass", "grass" });
        tileRules.Add("sand", new List<string>{ "sand", "sand", "sand", "sand" });
        tileRules.Add("water", new List<string>{ "water", "water", "water", "water" });
        
        tileRules.Add("grass_sand_south", new List<string> { "grass", "sand", "grass_sand", "grass_sand" });
        tileRules.Add("grass_sand_east", new List<string> { "grass_sand", "grass_sand", "sand", "grass" });
        tileRules.Add("grass_sand_north", new List<string> { "sand", "grass", "sand_grass", "sand_grass" });
        tileRules.Add("grass_sand_west", new List<string> { "sand_grass", "sand_grass", "grass", "sand" });

        tileRules.Add("grass_sand_south_east", new List<string> { "grass", "grass_sand", "grass_sand", "grass" });
        tileRules.Add("grass_sand_south_west", new List<string> { "grass", "sand_grass", "grass", "grass_sand" });
        tileRules.Add("grass_sand_north_east", new List<string> { "grass_sand", "grass", "sand_grass", "grass" });
        tileRules.Add("grass_sand_north_west", new List<string> { "sand_grass", "grass", "grass", "sand_grass" });

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
                    tiles[x, y].AddNeighbor(1, tiles[x, y + 1]);
                }
                //South
                if (y > 0)
                {
                    tiles[x, y].AddNeighbor(2, tiles[x, y - 1]);
                }
                //East
                if (x + 1 < sizeX)
                {
                    tiles[x, y].AddNeighbor(3, tiles[x + 1, y]);
                }
                //West
                if (x > 0)
                {
                    tiles[x, y].AddNeighbor(4, tiles[x - 1, y]);
                }

            }
        }

        /*bool done = false;
        while (done == false)
        {
            done = WaveFunctionCollapse();
        }*/

        PlaceTiles();
        StartCoroutine(testWFCSlowly());
    }

    private IEnumerator testWFCSlowly()
    {
        bool done = false;
        while (done == false)
        {
            if (Input.GetKey("e"))
            {
                done = WaveFunctionCollapse();
                PlaceTiles();
            }
            yield return null;
        }
    }

    private void PlaceTiles()
    {
        //Debug.Log(" ");
        for (int x = 0; x < sizeX; x++)
        {
            //string debug = "";
            for (int y = 0; y < sizeY; y++)
            {
                //debug += tiles[x, y].GetEntropy();
                //debug += " ";
                TileBase tileToPlace = null;
                if (tiles[x, y].GetPossibilities().Count == 1)
                {
                    if (tiles[x, y].GetPossibilities()[0] == "grass")
                    {
                        tileToPlace = grassTile;
                    }
                    else if (tiles[x, y].GetPossibilities()[0] == "grass_flower")
                    {
                        tileToPlace = grassFlowerTile;
                    }
                    else if (tiles[x, y].GetPossibilities()[0] == "water")
                    {
                        tileToPlace = waterTile;
                    }
                    else if (tiles[x, y].GetPossibilities()[0] == "sand")
                    {
                        tileToPlace = sandTile;
                    }
                    else if (tiles[x, y].GetPossibilities()[0] == "grass_sand_south")
                    {
                        tileToPlace = grassSandSouthTile;
                    }
                    else if (tiles[x, y].GetPossibilities()[0] == "grass_sand_east")
                    {
                        tileToPlace = grassSandEastTile;
                    }
                    else if (tiles[x, y].GetPossibilities()[0] == "grass_sand_north")
                    {
                        tileToPlace = grassSandNorthTile;
                    }
                    else if (tiles[x, y].GetPossibilities()[0] == "grass_sand_west")
                    {
                        tileToPlace = grassSandWestTile;
                    }
                    else if (tiles[x, y].GetPossibilities()[0] == "grass_sand_south_east")
                    {
                        tileToPlace = grassSandSouthEastTile;
                    }
                    else if (tiles[x, y].GetPossibilities()[0] == "grass_sand_south_west")
                    {
                        tileToPlace = grassSandSouthWestTile;
                    }
                    else if (tiles[x, y].GetPossibilities()[0] == "grass_sand_north_east")
                    {
                        tileToPlace = grassSandNorthEastTile;
                    }
                    else if (tiles[x, y].GetPossibilities()[0] == "grass_sand_north_west")
                    {
                        tileToPlace = grassSandNorthWestTile;
                    }
                }

                if (tileToPlace != null)
                {
                    groundTilemap.SetTile(new Vector3Int(x, y, 0), tileToPlace);
                }
            }
            //Debug.Log(debug);
        }
        //Debug.Log(" ");
    }

    private int GetEntropy(int x, int y)
    {
        return tiles[x, y].GetEntropy();
    }

    private string GetType(int x, int y)
    {
        return tiles[x, y].GetPossibilities()[0];
    }

    private int GetLowestEntropy()
    {
        int lowestEntropy = new List<string>(tileRules.Keys).Count; //number of types of tiles
        int tempEntropy;
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                tempEntropy = tiles[x, y].GetEntropy();
                if (tempEntropy > 0 && tempEntropy < lowestEntropy)
                {
                    lowestEntropy = tempEntropy;
                }
            }
        }
        return lowestEntropy;
    }

    private List<Tile> GetTilesLowestEntropy()
    {
        int lowestEntropy = new List<string>(tileRules.Keys).Count; //number of types of tiles
        int tempEntropy;
        List<Tile> tileList = new List<Tile>();

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                tempEntropy = tiles[x, y].GetEntropy();
                if (tempEntropy > 0)
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

        if (tilesLowestEntropy.Count == 0)
        {
            return true;
        }

        //Pick a random lowest entropy tile to collapse
        Tile tileToCollapse = tilesLowestEntropy[Random.Range(0, tilesLowestEntropy.Count)];
        tileToCollapse.Collapse();

        Stack<Tile> tileStack = new Stack<Tile>();
        tileStack.Push(tileToCollapse);

        Tile tempTile;
        bool reduced = false;
        List<string> tempTilePossibilities;
        List<int> tempTileDirections;
        while (tileStack.Count > 0)
        {
            tempTile = tileStack.Pop();
            tempTilePossibilities = tempTile.GetPossibilities();
            tempTileDirections = tempTile.GetDirections();
            for (int i = 0; i < tempTileDirections.Count; i++)
            {
                Tile tempTileNeighbor = tempTile.GetNeighbor(tempTileDirections[i]);
                if (tempTileNeighbor != null && tempTileNeighbor.GetEntropy() != 0)
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
        return false;
    }

    //Each tile stores its posibilities, entropy, and references to neighbors
    public class Tile
    {
        private List<string> possibilities;
        private int entropy;
        private Dictionary<int, Tile> neighbors = new Dictionary<int, Tile>();

        public Tile()
        {
            possibilities = new List<string>(tileRules.Keys);
            entropy = possibilities.Count;
        }

        public void AddNeighbor(int direction, Tile neighbor)
        {
            neighbors.Add(direction, neighbor);
        }

        public Tile GetNeighbor(int direction)
        {
            try
            {
                return neighbors[direction];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        //Tiles on the edges will return smaller lists
        public List<int> GetDirections()
        {
            return new List<int>(neighbors.Keys);
        }

        public List<string> GetPossibilities()
        {
            return possibilities;
        }

        public int GetEntropy()
        {
            return entropy;
        }

        //no weights yet
        public void Collapse()
        {
            int random = Random.Range(0, possibilities.Count);
            string randomPossibility = possibilities[random];
            possibilities = new List<string> { randomPossibility };
            entropy = 0;
        }

        //Direction is the direction it is being constrained FROM
        public bool Constrain(List<string> neighbourPossibilities, int direction)
        {
            bool reduced = false;

            if (entropy > 0)
            {
                List<string> connectors = new List<string>();
                for (int i = 0; i < neighbourPossibilities.Count; i++)
                {
                    connectors.Add(tileRules[neighbourPossibilities[i]][direction-1]);
                }

                int oppositeDirection = 1;
                if (direction == 1)
                {
                    oppositeDirection = 2;
                }
                else if (direction == 2)
                {
                    oppositeDirection = 1;
                }
                else if (direction == 3)
                {
                    oppositeDirection = 4;
                }
                else if (direction == 4)
                {
                    oppositeDirection = 3;
                }
                else
                {
                    Debug.Log("A GRAVE ERROR HAS OCCURRED");
                }

                List<string> possibilitiesCopy = new List<string>(possibilities);
                for (int i = 0; i < possibilitiesCopy.Count; i++)
                {
                    string currentPossibility = possibilitiesCopy[i];
                    List<string> currentPossibilityEdges = tileRules[currentPossibility];

                    if (!connectors.Contains(currentPossibilityEdges[oppositeDirection - 1]))
                    {
                        possibilities.Remove(currentPossibility);
                        reduced = true;
                    }
                }
                entropy = possibilities.Count;
            }

            return reduced;
        }
    }
}
