using System;
using UnityEngine;

public class AbsortWater : MonoBehaviour
{
    private SlimeCharacterController controller;
    private Slime slime;

    public ParticleSystem sukingParticle;

    protected void Awake()
    {
        controller = transform.parent.GetComponent<SlimeCharacterController>();
        slime = transform.parent.GetComponent<Slime>();
        //sukingParticle.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            Debug.Log("WAter!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            slime.Absorb(0.02f);
            controller.RefreshRadius();
        }
    }
    // void OnTriggerStay2D(Collider2D other)
    // {
    //     Debug.Log("OnTriggerStay2D" + other.name);
    // }

}