using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Earth : MonoBehaviour, ICube
{
    public bool _pollutable = true;

    public bool _isProlluted = false;

    public bool _hurtable = true;

    public float _verlise = 1.0f;

    public bool Pollutable
    {
        get
        {
            return _pollutable;
        }
    }

    public bool IsPolluted
    {
        get
        {
            return _isProlluted;
        }
    }

    public bool Hurtable
    {
        get
        {
            return _hurtable;
        }
    }

    public float Verlies
    {
        get
        {
            return _verlise;
        }
    }

    public Sprite earth_slime;
    TileBase tile;
    Tilemap map;
    Vector2 pos;


    public Earth(bool isProlluted, TileBase tile, Sprite _grass_slime, Tilemap map, Vector2 pos)
    {
        _isProlluted = isProlluted;
        earth_slime = _grass_slime;
        this.tile = tile;
        this.map = map;
        this.pos = pos;
    }

    //被污染(供Player调用)
    public float PollutedByRun()
    {
        if (!_isProlluted)
        {
            _isProlluted = true;
        }
        ChangeCubeTile();
        return Verlies;
    }

    //被污染(供粘液球调用)
    public void PollutedByShoot()
    {
        if (!_isProlluted)
        {
            _isProlluted = true;
        }
        ChangeCubeTile();
    }


    void ChangeCubeTile()
    {
        Tile tile = new Tile();
        tile.sprite = earth_slime;
        map.SetTile(new Vector3Int((int)pos.x, (int)pos.y, 0), tile);
        Debug.Log("Grass Change To Slime");
    }
}
