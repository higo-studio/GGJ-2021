using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TestTileMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Tilemap map = GetComponent<Tilemap>();
        TileBase t = map.GetTile(new Vector3Int(0,2,0));
        
        Debug.Log(t.name);
        /*
        GameObject obj = map.GetInstantiatedObject(new Vector3Int(0, 2, 0));
        Debug.Log(obj.name);
        */
    }
}
