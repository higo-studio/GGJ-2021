using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMap : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        Slime2 slime = collision.GetComponent<Slime2>();
        if(slime != null)
        {
            //xishou
        }
    }


}
