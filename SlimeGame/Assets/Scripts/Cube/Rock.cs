using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, ICube
{
    public bool _pollutable = false;

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

    public Rock(bool isProlluted)
    {
        
    }

    public float PollutedByRun()
    {
        return Verlies;
    }

    public void PollutedByShoot()
    {

    }

}
