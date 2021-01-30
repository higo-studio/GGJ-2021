using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

[CustomEditor(typeof(TilemapToolInspeactor))]

public class TilemapToolEditor : Editor
{
    TilemapToolInspeactor tool;
    Dictionary<Tile, TileBase> ruleMap;
    void OnEnable()
    {
        tool = (TilemapToolInspeactor)target;
        ruleMap = new Dictionary<Tile, TileBase>();
        foreach (var tuple in tool.keyValueArr)
        {
            ruleMap[tuple.key] = tuple.value;
        }
    }

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        if (GUILayout.Button("生成"))
        {
            Debug.Log("heelo");
            var bounds = tool.src.cellBounds;
            for (var x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (var y = bounds.yMin; y < bounds.yMax; y++)
                {
                    var tilePos = new Vector3Int(x, y, 0);
                    var srcTile = tool.src.GetTile<Tile>(tilePos);
                    if (!srcTile) continue;
                    ruleMap.TryGetValue(srcTile, out var vvv);
                    if (vvv)
                    {
                        tool.dst.SetTile(tilePos, vvv);
                    }
                    // tool.ruleMap
                    // tool.dst.SetTile();
                }
            }
        }
    }
}
