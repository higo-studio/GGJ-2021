using UnityEngine;
using System;
using System.Collections.Generic;

public class RunningParticleInfo
{
    public ParticleSystem system;
    public float endTime;
}
public class EffectManager : MonoBehaviour
{
    private static EffectManager _instance;
    public static EffectManager ins
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("EffectManager");
                _instance = obj.AddComponent<EffectManager>();

            }
            return _instance;
        }
    }
    public GameObject sputteringParicle;

    private Stack<ParticleSystem> _sputteringPool;
    private RunningParticleInfo[] _runningParticleArr;
    private int _runningParticleCount = 0;

    protected void Awake()
    {
        _sputteringPool = new Stack<ParticleSystem>();
        _runningParticleArr = new RunningParticleInfo[100];
        _instance = this;
    }

    public void GenHitEffect(Vector2 position, Vector2 normal)
    {
        GenHitEffect(position, normal, 0);
    }

    public void GenHitEffect(Vector2 position, Vector2 normal, float liftTime)
    {
        ParticleSystem particle;
        if (_sputteringPool.Count > 0)
        {
            particle = _sputteringPool.Pop();
        }
        else
        {
            var obj = Instantiate(sputteringParicle, transform);
            particle = obj.GetComponent<ParticleSystem>();
        }
        // 自动回收
        if (liftTime == 0)
        {
            liftTime = particle.main.duration;
        }

        particle.transform.position = position;
        particle.transform.up = normal;
        particle.gameObject.SetActive(true);
        particle.Play(true);
        _runningParticleArr[_runningParticleCount] = new RunningParticleInfo()
        {
            system = particle,
            endTime = Time.time + liftTime
        };
        _runningParticleCount++;
    }
    public void GenDeathEffect(Vector2 position, Vector2 normal, float liftTime)
    {
        ParticleSystem particle;
        if (_sputteringPool.Count > 0)
        {
            particle = _sputteringPool.Pop();
        }
        else
        {
            var obj = Instantiate(sputteringParicle, transform);
            particle = obj.GetComponent<ParticleSystem>();
        }
        // 自动回收
        if (liftTime == 0)
        {
            liftTime = particle.main.duration;
        }

        particle.transform.position = position;
        particle.transform.up = normal;
        particle.gameObject.SetActive(true);
        particle.Play(true);
        _runningParticleArr[_runningParticleCount] = new RunningParticleInfo()
        {
            system = particle,
            endTime = Time.time + liftTime
        };
        _runningParticleCount++;
    }

    protected void LateUpdate()
    {
        var now = Time.time;
        for (var i = 0; i < _runningParticleCount;)
        {
            var info = _runningParticleArr[i];
            if (info.endTime <= now)
            {
                // 回收
                var system = info.system;
                system.gameObject.SetActive(false);
                _sputteringPool.Push(system);

                _runningParticleArr[i] = _runningParticleArr[_runningParticleCount - 1];
                _runningParticleCount--;
            }
            else
            {
                i++;
            }
        }
    }
}