using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[System.Serializable]
public struct KeyValue
{
    public Tile key;
    public TileBase value;
}
[ExecuteInEditMode]
public class TilemapToolInspeactor : MonoBehaviour
{
    public Tilemap src;
    public Tilemap dst;
    public KeyValue[] keyValueArr;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
}
