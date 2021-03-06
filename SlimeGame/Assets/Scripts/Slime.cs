using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Slime : MonoBehaviour
{

    //生命值
    public float HP = 0f;

    public float MaxHP = 0f;

    //重量
    public float weither = 15f;

    public SoftBody2D body;

    public TilesManager tilesManager;

    public bool enableToClimb = false;

    

    private void Awake()
    {
        
    }

    public Image HpUI;
    private void Update()
    {
        HpUI.fillAmount = HP / MaxHP;
    }

    private void FixedUpdate()
    {
        UpdateState();
        RunOnTheCube();
        
    }

    public void Hurt(float ver) {
        HP -= ver;
        if (HP < 0)
            HP = 0;
    }


    //根据血量更新玩家数值
    void UpdateState()
    {
        float scale = HP / MaxHP;
        scale *= 1.5f;
        body.DebugRadius = scale;
    }

    void RunOnTheCube()
    {
        ICube[] objs = tilesManager.GetTileMesByPosition(transform.position);
        if (objs[0] != null && !objs[0].IsPolluted)
        {
            Hurt(objs[0].PollutedByRun());
            
        }
        if (objs[1] != null && !objs[1].IsPolluted)
        {
            Hurt(objs[1].PollutedByRun());
            
        }
        if (objs[2] != null && !objs[2].IsPolluted)
        {
            Hurt(objs[2].PollutedByRun());
            
        }
        if (objs[3] != null && !objs[3].IsPolluted)
        {
            Hurt(objs[3].PollutedByRun());
            
        }

        foreach(ICube _cube in objs)
        {
            if (_cube != null && _cube.IsPolluted)
            {
                enableToClimb = true;
                break;
            }
            enableToClimb = false;
        }
        
    }

    public float GetWeither()
    {
        return weither;
    }

    public void Absorb(float hel)
    {
        HP += hel;
        if (HP > MaxHP)
            Dead();
    }

    void Dead()
    {
        Debug.Log("awsl");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Water")
        {
            Debug.Log("WAter!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Absorb(0.02f);
        }
    }
}
