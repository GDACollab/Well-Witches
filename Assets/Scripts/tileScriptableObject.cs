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
    [Space(5)]
    public string edgeSouthLeft;
    public string edgeSouthRight;
    [Space(5)]
    public string edgeWestUp;
    public string edgeWestDown;
    [Space(5)]
    public string edgeEastUp;
    public string edgeEastDown;

    public Sprite tileImage; //Grabbed from TileBase tile

    //Runs when the inspector changes at all
    private void OnValidate()
    {
        tileImage = ((Tile)tile).sprite;
    }
    /*
    public override void OnInspectorGUI()
    {
        var texture = AssetPreview.GetAssetPreview(sprite);
        GUILayout.Label(texture);
    }*/
}