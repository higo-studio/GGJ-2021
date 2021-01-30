using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour, ICube
{
    public bool _pollutable = true;

    public bool _isProlluted = false;

    public bool _hurtable = false;

    public float _verlise = 0f;

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

    public Grass(bool isProlluted)
    {
        _isProlluted = isProlluted;
    }

    //被污染(供Player调用)
    public float PollutedByRun()
    {
        return 0;
    }

    //被污染(供粘液球调用)
    public void PollutedByShoot()
    {
        if (!_isProlluted)
        {
            _isProlluted = true;
        }
    }
}
