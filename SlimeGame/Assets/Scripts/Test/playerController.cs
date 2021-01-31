using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public TilesManager tmanager;

    public float speed;
    public float jumpForce;
    public Rigidbody2D rb;
    public GameObject bullet;
    public float shootingSpan = 0.2f;
    private float bulletTimer = 0;
    public Vector2 bOffset;
    public float recoilForce;
    public Text bulletNumberText;
    private int bulletNumber = 9999;


    // Update is called once per frame
    void Update()
    {
        playerMove();
        playerJump();

        
        ICube[] objs = tmanager.GetTileMesByPosition(transform.position);
        if(objs[0] != null)
        {
            Debug.Log(objs[0].Pollutable);
        }
        
        
    }


    public void playerMove()
    {
        float horizontalmove;
        horizontalmove = Input.GetAxisRaw("Horizontal");

        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(speed * horizontalmove, rb.velocity.y);
            //rb.transform.localScale = new Vector3(horizontalmove, 1, 1);
            //anim.SetFloat("speed", Mathf.Abs(horizontalmove));
        }
    }

    public void playerJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //anim.SetBool("jumping", true);
        }
    }

    
    
    
    
    
}
