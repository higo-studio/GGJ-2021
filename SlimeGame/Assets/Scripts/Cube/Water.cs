using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Water : MonoBehaviour, ICube
{
    public bool _pollutable = true;

    public bool _isProlluted = false;

    public bool _hurtable = true;

    public float _verlise = 0f;

    //玩家吸收速率
    public float recovery = 0f;

    //玩家在该水体中能恢复的最大生命值
    public float maxRecovery = 0f;

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

    public Water(bool isProlluted, TileBase tile)
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
        if (!_isProlluted)
        {
            _isProlluted = true;

        }
    }

    //将水体变成果冻
    void ChangeToJelly()
    {
        //Do sth
    }
}
