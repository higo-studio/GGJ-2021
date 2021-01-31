using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMap : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        Slime slime = collision.GetComponent<Slime>();
        if(slime != null)
        {
            //xishou
            slime.Absorb(0.02f);
        }
    }


}
