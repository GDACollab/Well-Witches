using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "WFCTile", menuName = "ScriptableObjects/WFCTile")]
public class tileScriptableObject : ScriptableObject
{
    [Space(10), Header("Tile Asset")]
    [Tooltip("A tile asset, will be placed in a tilemap")]
    public TileBase tile;

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
    [HideInInspector] public string tileID; //Grabbed from name of TileBase tile, used in wfc

    //Runs when the inspector changes at all
    private void OnValidate()
    {
        tileID = tile.name;
        tileImage = ((Tile)tile).sprite;
    }
    
}