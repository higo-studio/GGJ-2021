using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using UnityEngine.UI;
>>>>>>> 03a1281e0e828ba687d048786133969a28119a6b

public class Slime : MonoBehaviour
{

    //生命值
    public float HP = 0f;

    public float MaxHP = 0f;

<<<<<<< HEAD
    //重量
    public float weither = 15f;

    public SoftBody2D body;

    public TilesManager tilesManager;
=======
    public Image HPUI;

    //重量
    public float weither = 15f;

    //public TilesManager tilesManager;
>>>>>>> 03a1281e0e828ba687d048786133969a28119a6b


    private void Awake()
    {
        
    }

    

    private void FixedUpdate()
    {
<<<<<<< HEAD
        UpdateState();
        //RunOnTheCube();
=======
        //UpdateState();
        //RunOnTheCube();
        UpdateHPUI();
>>>>>>> 03a1281e0e828ba687d048786133969a28119a6b
        
    }

    void Hurt(float ver) {
        HP -= ver;
        if (HP < 0)
            HP = 0;
    }


    //根据血量更新玩家数值
    void UpdateState()
    {
<<<<<<< HEAD
        float scale = HP / MaxHP;
        scale *= 1.5f;
        body.DebugRadius = scale;
    }

=======

    }
/*
>>>>>>> 03a1281e0e828ba687d048786133969a28119a6b
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
<<<<<<< HEAD

=======
*/
>>>>>>> 03a1281e0e828ba687d048786133969a28119a6b
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

<<<<<<< HEAD
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Water")
        {
            Debug.Log("WAter!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Absorb(0.02f);
        }
=======
    void UpdateHPUI()
    {
        HPUI.fillAmount = HP / MaxHP;
>>>>>>> 03a1281e0e828ba687d048786133969a28119a6b
    }
}
