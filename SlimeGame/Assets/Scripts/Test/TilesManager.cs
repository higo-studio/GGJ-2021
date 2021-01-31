using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TilesManager : MonoBehaviour
{

    //行
    public int row = 0;

    //列
    public int colume = 0;

    public Transform tilemapTransform;

    public ICube[,] tilesMes;

    public Tilemap map;

    public Tilemap spriteMap;

    public Sprite earth_slime_sprite;

    public Sprite grass_slime_sprite;

    Vector2 mapPosition;
    private void Start()
    {   
        mapPosition = tilemapTransform.position;
        /**
         * 读取地图方块信息
         */
        tilesMes = new ICube[row, colume];
        for(int i = 0; i < row; i++)
        {
            for(int j = 0; j < colume; j++)
            {
                int row_i = i - (int)Mathf.Floor(tilemapTransform.position.x);
                int colume_j = j - (int)Mathf.Floor(tilemapTransform.position.y);
                TileBase tile = map.GetTile(new Vector3Int(row_i, colume_j, 0));
                TileBase tileSprite = spriteMap.GetTile(new Vector3Int(row_i, colume_j, 0));
                if (tile != null)
                {
                    
                    switch (tile.name)
                    {
                        case "collTile_1":
                            tilesMes[i,j] = new Earth(false, (Tile)tileSprite, earth_slime_sprite);
                            break;
                        case "collTile_4":
                            tilesMes[i, j] = new Earth(true, (Tile)tileSprite, earth_slime_sprite);
                            break;
                        case "collTile_0":
                            tilesMes[i, j] = new Grass(false, (Tile)tileSprite, grass_slime_sprite);
                            break;
                        case "collTile_3":
                            tilesMes[i, j] = new Grass(true, (Tile)tileSprite, grass_slime_sprite);
                            break;
                        case "collTile_2":
                            tilesMes[i, j] = new Rock(false, tile);
                            break;
                            
                    }
                }
            }
        }
    }


    public ICube[] GetTileMesByPosition(Vector3 position)
    {
        //position += mapPosition;
        position.x = Mathf.Floor(position.x);
        position.y = Mathf.Floor(position.y);
        //Debug.Log(position);
        Vector3Int _pos = new Vector3Int((int)position.x, (int)position.y, 0);
        // 0上  1下  2左  3右
        ICube[] tiles = new ICube[4];
        if ((_pos.y + 1) < row && tilesMes[_pos.x, _pos.y + 1] != null)
        {
            tiles[0] = tilesMes[_pos.x, _pos.y + 1];
        }
        if ((_pos.y - 1)>-1 && tilesMes[_pos.x, _pos.y - 1] != null)
        {
            tiles[1] = tilesMes[_pos.x, _pos.y - 1];
        }
        if ((_pos.x - 1) > 0 && tilesMes[_pos.x - 1, _pos.y] != null)
        {
            tiles[2] = tilesMes[_pos.x - 1, _pos.y];
        }
        if ((_pos.x + 1) < colume && tilesMes[_pos.x + 1, _pos.y] != null)
        {
            tiles[3] = tilesMes[_pos.x + 1, _pos.y];
        }
        return tiles;
    }

    public void ShootOn(Vector2 position, Vector2 normal)
    {
        position.x = Mathf.Floor(position.x);
        position.y = Mathf.Floor(position.y);
        //Debug.Log(position);
        Vector3Int _pos = new Vector3Int((int)position.x, (int)position.y, 0);
        ICube[] tiles = new ICube[4];
        if ((_pos.y + 1) < row && tilesMes[_pos.x, _pos.y + 1] != null)
        {
            tiles[0] = tilesMes[_pos.x, _pos.y + 1];
        }
        if ((_pos.y - 1) > -1 && tilesMes[_pos.x, _pos.y - 1] != null)
        {
            tiles[1] = tilesMes[_pos.x, _pos.y - 1];
        }
        if ((_pos.x - 1) > 0 && tilesMes[_pos.x - 1, _pos.y] != null)
        {
            tiles[2] = tilesMes[_pos.x - 1, _pos.y];
        }
        if ((_pos.x + 1) < colume && tilesMes[_pos.x + 1, _pos.y] != null)
        {
            tiles[3] = tilesMes[_pos.x + 1, _pos.y];
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
