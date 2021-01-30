using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botton : MonoBehaviour, IGear
{
    public bool _isTriggering;

    //触发需要的最小重量
    public float triggerWeither = 0f;

    //目前被压的重量
    public float weither = 0f;

    [SerializeField]
    public GameObject[] ArrTargets;

    [SerializeField]
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
        if(_targets!= null && !_targets.Contains(gear))
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
        _targets = new List<IGear>();
        for(int i = 0; i < ArrTargets.Length; i++) { _targets.Add(ArrTargets[i].GetComponent<IGear>()); }
        _triggers = new List<IGear>();
        for (int i = 0; i < ArrTriggers.Length; i++) { _triggers.Add(ArrTriggers[i].GetComponent<IGear>()); }
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Hero")
        {
            weither += collision.transform.GetComponent<Slime>().GetWeither();
        }
        if (collision.transform.tag == "Moveable")
        {
            weither += collision.transform.GetComponent<Move>().GetWeither();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.tag == "Hero")
        {
            weither -= collision.transform.GetComponent<Slime>().GetWeither();
        }
        if (collision.transform.tag == "Moveable")
        {
            weither -= collision.transform.GetComponent<Move>().GetWeither();
        }

        if (weither < 0)
            weither = 0;
    }

    


    private void FixedUpdate()
    {
        if(!_isTriggering && weither >= triggerWeither)
        {
            Triggering();
        }else if (_isTriggering && weither < triggerWeither)
        {
            ShutDown();
        }
    }

    public void Invoke(IGear from)
    {
        
    }

    public void Triggering()
    {
        _isTriggering = true;
        if (_targets != null && _targets.Count > 0)
        {
            foreach(IGear target in _targets)
            {
                target.Invoke(this);
            }
        }
    }

    public void ShutDown()
    {
        _isTriggering = false;
        if(_targets!=null && _targets.Count > 0)
        {
            foreach(IGear target in _targets)
            {
                target.UnInvoke();
            }
        }
    }

    public void UnInvoke()
    {

    }

    List<IGear> _targets;
    List<IGear> _triggers;

}
