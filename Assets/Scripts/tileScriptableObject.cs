using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "WFCTile", menuName = "ScriptableObjects/WFCTile")]
public class tileScriptableObject : ScriptableObject
{
    [Space(10), Header("Tile Asset")]
    public TileBase tile;
    [Space(10), Header("Tile ID (Must be unique)")]
    public string tileID;
    [Space(10), Header("Edge definitions:")]
    public string edgeNorthLeft;
    public string edgeNorthRight;

    public string edgeSouthLeft;
    public string edgeSouthRight;

    public string edgeWestUp;
    public string edgeWestDown;

    public string edgeEastUp;
    public string edgeEastDown;

}
