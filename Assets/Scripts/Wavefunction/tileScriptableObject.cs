using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "WFCTile", menuName = "ScriptableObjects/WFCTile")]
public class tileScriptableObject : ScriptableObject
{
    [Space(10), Header("Tile Assets")]
    [Tooltip("A tile asset, will be placed in a tilemap")]
    public TileBase tileGround = null;

    [Tooltip("A tile asset, will be placed in a tilemap")]
    public TileBase tileDecorator = null;

    [Tooltip("A tile asset, will be placed in a tilemap")]
    public TileBase tileHitbox = null;

    [Tooltip("A tile asset, will be placed in a tilemap")]
    public TileBase tileHitboxSorted = null;

    [Tooltip("A tile asset, will be placed in a tilemap")]
    public TileBase tileMiddle = null;

    [Tooltip("A tile asset, will be placed in a tilemap")]
    public TileBase tileAbove = null;

    [Tooltip("A prefab, will be placed in a tilemap")]
    public GameObject tilePrefab = null;

    [Space(10), Header("Tile Weight")]
    [Tooltip("Float, high values means its more common")]
    public float weight;

    [Space(10), Header("Has Hitbox?")]
    [Tooltip("Will have a full box collision")]
    public bool canPlaceBush;

    [Space(10), Header("Edge definitions:")]
    [Tooltip("String, the edge on the left portion of the north edge")]
    public string edgeNorthLeft;
    [Space(1)]
    [Tooltip("String, the edge on the right portion of the north edge")]
    public string edgeNorthRight;

    [Space(7)]
    [Tooltip("String, the edge on the left portion of the south edge")]
    public string edgeSouthLeft;
    [Space(1)]
    [Tooltip("String, the edge on the right portion of the north edge")]
    public string edgeSouthRight;

    [Space(7)]
    [Tooltip("String, the edge on the upper portion of the east edge")]
    public string edgeEastUp;
    [Space(1)]
    [Tooltip("String, the edge on the lower portion of the east edge")]
    public string edgeEastDown;

    [Space(7)]
    [Tooltip("String, the edge on the upper portion of the west edge")]
    public string edgeWestUp;
    [Space(1)]
    [Tooltip("String, the edge on the lower portion of the west edge")]
    public string edgeWestDown;

    [HideInInspector] public Sprite tileImage; //Grabbed from TileBase tile, used in tileScriptableObjectEditor.cs

    //Runs when the inspector changes at all
    
    private void OnValidate()
    {
        //tileID = tile.name;
        if (tileGround != null)
        {
            tileImage = ((Tile)tileGround).sprite;
        }
        else
        {
            tileImage = ((Tile)tileHitbox).sprite;
        }
    }
    

}