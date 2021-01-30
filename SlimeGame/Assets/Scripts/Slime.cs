using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{

    //生命值
    public float HP = 0f;

    public float MaxHP = 0f;

    //重量
    public float weither = 15f;

    public TilesManager tilesManager;


    private void Awake()
    {
        
    }

    

    private void FixedUpdate()
    {
        //UpdateState();
        //RunOnTheCube();
        
    }

    void Hurt(float ver) {
        HP -= ver;
        if (HP < 0)
            HP = 0;
    }


    //根据血量更新玩家数值
    void UpdateState()
    {

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
}
