using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TilesManager : MonoBehaviour
{
    [SerializeField]
    public Tilemap map;

    [SerializeField]
    public Tilemap spriteMap;

    [SerializeField]
    public RuleTile earth_slime_rule;

    public RuleTile grass_slime_rule;

    Dictionary<Vector3Int, ICube> tilesMes;
    Vector3Int boundMin;
    Vector3Int boundMax;

    private void Awake()
    {
        tilesMes = new Dictionary<Vector3Int, ICube>();
    }

    private void Start()
    {
        /**
         * 读取地图方块信息
         */
        
        boundMax = map.cellBounds.max;
        boundMin = map.cellBounds.min;

        Debug.Log($"MinPosition:{boundMin}   MaxPosition:{boundMax}");

        for (int i = boundMin.y; i <= boundMax.y; i++)
        {
            for(int j = boundMin.x; j <= boundMax.x; j++)
            {
                TileBase tile = map.GetTile(new Vector3Int(j, i, 0));
                TileBase tileSprite = spriteMap.GetTile(new Vector3Int(j, i, 0));
                if (tile != null)
                {
                    
                    switch (tile.name)
                    {
                        case "collTile_1":

                            tilesMes.Add(new Vector3Int(j, i, 0), new Earth(false, tileSprite, earth_slime_rule, spriteMap, new Vector2(j, i)));
                            break;

                        case "collTile_4":
                            tilesMes.Add(new Vector3Int(j, i, 0), new Earth(true, tileSprite, earth_slime_rule, spriteMap, new Vector2(j, i)));
                            break;

                        case "collTile_0":
                            tilesMes.Add(new Vector3Int(j, i, 0), new Grass(false, tileSprite, grass_slime_rule, spriteMap, new Vector2(j, i)));

                            break;

                        case "collTile_3":
                            tilesMes.Add(new Vector3Int(j, i, 0), new Grass(true, tileSprite, grass_slime_rule, spriteMap, new Vector2(j, i)));
                            break;

                        case "collTile_2":
                            //tilesMes[i, j] = new Rock(false, tileSprite);
                            break;
                            
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        //spriteMap.RefreshAllTiles();
    }

    public ICube[] GetTileMesByPosition(Vector3 position)
    {
        
        Vector3Int _pos = map.WorldToCell(position);
        // 0上  1下  2左  3右  4左上  5右上  6左下  7右下
        ICube[] tiles = new ICube[8];
        Vector3Int upPos = new Vector3Int(_pos.x, _pos.y + 1, 0);
        Vector3Int downPos = new Vector3Int(_pos.x, _pos.y - 1, 0);
        Vector3Int leftPos = new Vector3Int(_pos.x - 1, _pos.y, 0);
        Vector3Int rightPos = new Vector3Int(_pos.x + 1, _pos.y, 0);

        Vector3Int up_leftPos = new Vector3Int(_pos.x - 1, _pos.y + 1, 0);
        Vector3Int down_leftPos = new Vector3Int(_pos.x - 1, _pos.y - 1, 0);
        Vector3Int down_rightPos = new Vector3Int(_pos.x + 1, _pos.y - 1, 0);
        Vector3Int up_rightPos = new Vector3Int(_pos.x + 1, _pos.y + 1, 0);

        if (tilesMes.ContainsKey(upPos))
        {
            tiles[0] = tilesMes[upPos];
        }
        if (tilesMes.ContainsKey(downPos))
        {
            tiles[1] = tilesMes[downPos];
        }
        if (tilesMes.ContainsKey(leftPos))
        {
            tiles[2] = tilesMes[leftPos];
        }
        if (tilesMes.ContainsKey(rightPos))
        {
            tiles[3] = tilesMes[rightPos];
        }
        if (tilesMes.ContainsKey(up_leftPos))
        {
            tiles[4] = tilesMes[up_leftPos];
        }
        if (tilesMes.ContainsKey(up_rightPos))
        {
            tiles[5] = tilesMes[up_rightPos];
        }
        if (tilesMes.ContainsKey(down_leftPos))
        {
            tiles[6] = tilesMes[down_leftPos];
        }
        if (tilesMes.ContainsKey(down_rightPos))
        {
            tiles[7] = tilesMes[down_rightPos];
        }
        return tiles;
    }

    
    public void ShootOn(Vector2 position, Vector2 normal)
    {
        Vector3Int hitPos = map.WorldToCell(new Vector3(position.x, position.y, 0));

        //Debug.Log(position);

        ICube[] tiles = new ICube[4];
        Vector3Int upPos = new Vector3Int(hitPos.x, hitPos.y + 1, 0);
        Vector3Int downPos = new Vector3Int(hitPos.x, hitPos.y - 1, 0);
        Vector3Int leftPos = new Vector3Int(hitPos.x - 1, hitPos.y, 0);
        Vector3Int rightPos = new Vector3Int(hitPos.x + 1, hitPos.y, 0);

        if (tilesMes.ContainsKey(upPos))
        {
            tiles[0] = tilesMes[upPos];
        }
        if (tilesMes.ContainsKey(downPos))
        {
            tiles[1] = tilesMes[downPos];
        }
        if (tilesMes.ContainsKey(leftPos))
        {
            tiles[2] = tilesMes[leftPos];
        }
        if (tilesMes.ContainsKey(rightPos))
        {
            tiles[3] = tilesMes[rightPos];
        }

        foreach (ICube cube in tiles)
        {
            if(cube != null)
            {
                cube.PollutedByShoot();
            }
        }
    }
    
    
}
