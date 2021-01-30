using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Jelly : MonoBehaviour, ICube
{
    public bool _pollutable = false;

    public bool _isProlluted = false;

    public bool _hurtable = false;

    public float _verlise = 0f;

    //弹跳比率 v *= accRatio
    public float accRatio = 1f;

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


    TileBase tile;

    public Jelly(bool isProlluted, TileBase tile)
    {
        _isProlluted = isProlluted;
        this.tile = tile;
    }


    //被污染(供Player调用)
    public float PollutedByRun()
    {
        return Verlies;
    }

    //被污染(供粘液球调用)
    public void PollutedByShoot()
    {
        
    }

}
