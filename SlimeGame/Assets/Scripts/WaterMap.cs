using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMap : MonoBehaviour
{

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            
        }
        Slime slime = collision.transform.GetComponent<Slime>();
        //Debug.Log(collision.transform.name);
        slime.Absorb(0.02f);
    }


}
