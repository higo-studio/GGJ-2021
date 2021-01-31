using System;
using UnityEngine;

public class CollectBall : MonoBehaviour
{
    private SlimeCharacterController controller;
    private Slime slime;

    protected void Awake()
    {
        controller = transform.parent.GetComponent<SlimeCharacterController>();
        slime = transform.parent.GetComponent<Slime>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var bullet = other.GetComponent<BbBullet>();
        if (bullet == null) return;
        if (!bullet.isHit) return;
        Debug.Log("OnTriggerEnter2D" + other.name);
        bullet.follow = gameObject;
    }

    public void Recover()
    {
        controller.CollectBall();

    }
    // void OnTriggerStay2D(Collider2D other)
    // {
    //     Debug.Log("OnTriggerStay2D" + other.name);
    // }

}