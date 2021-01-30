/**
 * 机关门
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IGear
{

    public bool _isTriggering;

    //期望激活源数量
    public int tCount = 1;

    //已激活源数
    public int count = 0;

    public Animator animator;

    public AnimationClip openClip;

    public AnimationClip closeClip;

    public GameObject[] ArrTargets;

    public GameObject[] ArrTriggers;

    public List<IGear> Targets
    {
        get
        {
            return _targets;
        }
    }

    public List<IGear> Triggers
    {
        get
        {
            return _triggers;
        }
    }

    public bool IsTriggering
    {
        get
        {
            return _isTriggering;
        }
    }

    public void AddTarget(IGear gear)
    {
        if (_targets != null && !_targets.Contains(gear))
        {
            _targets.Add(gear);
        }
    }

    public void AddTrigger(IGear gear)
    {
        if (_triggers != null && !_triggers.Contains(gear))
        {
            _triggers.Add(gear);
        }
    }

    public void RemoveTarget(IGear gear)
    {
        if (_targets != null && _targets.Contains(gear))
        {
            _targets.Remove(gear);
        }
    }

    public void RemoveTrigger(IGear gear)
    {
        if (_triggers != null && _triggers.Contains(gear))
        {
            _triggers.Remove(gear);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _targets = new List<IGear>();
        for (int i = 0; i < ArrTargets.Length; i++) { _targets.Add(ArrTargets[i].GetComponent<IGear>()); }
        _triggers = new List<IGear>();
        for (int i = 0; i < ArrTriggers.Length; i++) { _triggers.Add(ArrTriggers[i].GetComponent<IGear>()); }

    }

    private void FixedUpdate()
    {
        if (!_isTriggering && count >= tCount)
        {
            Triggering();
        }
        else if (_isTriggering && count < tCount)
        {
            ShutDown();
        }

    }

    public void Invoke(IGear from)
    {
        count++;
    }

    public void Triggering()
    {
        /*
        _isTriggering = true;
        Debug.Log("门开了################################################");
        animator.Play(openClip.name);
        */

        _isTriggering = !_isTriggering;
        if (_isTriggering)
        {
            Debug.Log("门开了################################################");
            animator.Play(openClip.name);
        }
        else
        {
            Debug.Log("门关了################################################");
            animator.Play(closeClip.name);
        }

    }

    public void ShutDown()
    {
        _isTriggering = !_isTriggering;
        if (_isTriggering)
        {
            Debug.Log("门开了################################################");
            animator.Play(openClip.name);
        }
        else
        {
            Debug.Log("门关了################################################");
            animator.Play(closeClip.name);
        }

    }

    public void UnInvoke()
    {
        count--;
    }


    List<IGear> _targets;
    List<IGear> _triggers;
    
}
