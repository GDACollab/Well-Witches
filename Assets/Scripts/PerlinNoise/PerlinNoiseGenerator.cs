using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perlinNoiseMap : MonoBehaviour
{

    //2 diff dictionaries to reference tiles/tileGroups
    Dictionary<int, GameObject> tileset;
    Dictionary<int, GameObject> tile_groups;


    //4 different tiles
    public GameObject TileDirt;
    public GameObject TileGrass;
    public GameObject TileRock;
    public GameObject TileGlass;

    //size of map
    [SerializeField] int mapWidth = 160;
    [SerializeField] int mapHeight = 90;


    //2D Lists to hold tiles and frequency
    List<List<int>> noiseGrid = new List<List<int>>();
    List<List<GameObject>> tileGrid = new List<List<GameObject>>();

    //var to magnify randomness/customization of perlin noise
    //any value between 4 and 20
    [SerializeField] float magnification = 14.0f;

    [SerializeField] int xOffset = -10; //if increase/decrease, move right/left
    [SerializeField] int yOffset = -10; //if inc/dec, move up/down

    //Seed for getFloatUsingPerlin. -1 = random.
    [SerializeField] float seed = -1;
    float seedOffsetMultiplier = 100000;

    void Awake()
    {
        if (seed == -1) generateRandomSeed();
    }

    // Start is called before the first frame update
    void Start()
    {
        //createTileset();
        //createTileGroups();
        //generateMap();

    }

    void createTileset()
    {
        tileset = new Dictionary<int, GameObject>();
        tileset.Add(0, TileDirt);
        tileset.Add(1, TileGrass);
        tileset.Add(2, TileRock);
        tileset.Add(3, TileGlass);
    }

    void createTileGroups()
    {
        tile_groups = new Dictionary<int, GameObject>();

        foreach (KeyValuePair<int, GameObject> prefab_pair in tileset)
        {

            GameObject tile_group = new GameObject(prefab_pair.Value.name);
            tile_group.transform.parent = gameObject.transform;
            tile_group.transform.localPosition = new Vector3(0, 0, 0);
            tile_groups.Add(prefab_pair.Key, tile_group);

        }
    }

    void generateMap()
    {
        for (int x = 0; x < mapWidth; x++)
        {

            noiseGrid.Add(new List<int>());
            tileGrid.Add(new List<GameObject>());

            for (int y = 0; y < mapHeight; y++)
            {
                int tile_id = getIdUsingPerlin(x, y);
                noiseGrid[x].Add(tile_id);
                createTile(tile_id, x, y);
            }

        }
    }

    public void generateRandomSeed() {
        seed = Random.value;
    }

    int getIdUsingPerlin(int x, int y)
    {
        //will generate a bunch of random numbers
        float rawPerlin = getFloatUsingPerlin(x, y);

        //will clamp numbers between 0 and 1, then multiplied by count(4) so that we can assign them to diff tiles
        float clamp_perlin = Mathf.Clamp(rawPerlin, 0, 1);
        float scaledPerlin = clamp_perlin * tileset.Count;

        if (scaledPerlin == 4)
        {
            scaledPerlin = 3;
        }

        return Mathf.FloorToInt(scaledPerlin);
    }

    //Modified getIdUsingPerlin to not mult/floor value, instead just return perlin float value (modded by Ashton G)
    public float getFloatUsingPerlin(int x, int y)
    {
        //will generate a bunch of random numbers
        float rawPerlin = Mathf.PerlinNoise((x - xOffset) / magnification + seed * seedOffsetMultiplier, (y - yOffset) / magnification);

        return rawPerlin;
    }

    void createTile(int tile_id, int x, int y)
    {
        GameObject tilePrefab = tileset[tile_id];
        GameObject tileGroup = tile_groups[tile_id];

        GameObject tile = Instantiate(tilePrefab, tileGroup.transform);

        tile.name = string.Format("tile_x{0}_y{1}", x, y);
        tile.transform.localPosition = new Vector3(x, y, 0);

        tileGrid[x].Add(tile);
    }


}
