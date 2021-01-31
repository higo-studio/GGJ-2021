using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slime : MonoBehaviour
{

    //生命值
    public float HP = 20f;

    public float MaxHP = 20f;

    //重量
    public float weither = 15f;

    public SoftBody2D body;

    public TilesManager tilesManager;

    public Image HpUI;

    public GameObject deathAnimation;

    private void Update()
    {
        HpUI.fillAmount = HP / MaxHP;
    }
    private void Awake()
    {
    }



    private void FixedUpdate()
    {
        // UpdateState();
        //RunOnTheCube();

    }

    public void Hurt(float ver)
    {
        HP -= ver;
        if (HP < 0)
            HP = 0;
    }


    //根据血量更新玩家数值
    void UpdateState()
    {
        // float scale = HP / MaxHP;

        // body.DebugRadius = Mathf.Lerp(0.5f, 1.75f, scale);
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

    public void Absorb(float hel, bool over = false)
    {
        HP += hel;
        if (HP > MaxHP)
        {
            if (over)
            {
                Dead();
            }
            else
            {
                HP = MaxHP;
            }

        }
    }

    void Dead()
    {
        Debug.Log("awsl");
        gameObject.SetActive(false);
        Instantiate(deathAnimation, transform.position, Quaternion.identity);
    }
}
