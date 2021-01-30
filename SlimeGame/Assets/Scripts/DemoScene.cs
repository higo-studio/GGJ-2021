using UnityEngine;
using System.Collections;
using Prime31;


public class DemoScene : MonoBehaviour
{
    // movement config
    public float gravity = -25f;
    public float runSpeed = 8f;
    public float groundDamping = 20f; // how fast do we change direction? higher means faster
    public float inAirDamping = 5f;
    public float jumpHeight = 3f;

    [HideInInspector]
    private float normalizedHorizontalSpeed = 0;

    public SlimeController Controller;
    public Animator Animator;
    private RaycastHit2D _lastControllerColliderHit;
    private Vector3 _velocity;


    void Awake()
    {
        // Animator = GetComponent<Animator>();
        Controller = GetComponent<SlimeController>();

        // listen to some events for illustration purposes
        Controller.onControllerCollidedEvent += onControllerCollider;
        Controller.onTriggerEnterEvent += onTriggerEnterEvent;
        Controller.onTriggerExitEvent += onTriggerExitEvent;
    }


    #region Event Listeners

    void onControllerCollider(RaycastHit2D hit)
    {
        // bail out on plain old ground hits cause they arent very interesting
        if (hit.normal.y == 1f)
            return;

        // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
        //Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
    }


    void onTriggerEnterEvent(Collider2D col)
    {
        Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
    }


    void onTriggerExitEvent(Collider2D col)
    {
        Debug.Log("onTriggerExitEvent: " + col.gameObject.name);
    }

    #endregion

    void Update()
    {
        var jump = Input.GetButton("Jump");
        if (jump)
        {
            Controller.Jump();
        }
    }
    // the Update loop contains a very simple example of moving the character around and controlling the animation
    void FixedUpdate()
    {
        var dt = Time.fixedDeltaTime;
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        Controller.SetWalkDirection(horizontal * dt * runSpeed, vertical* dt * runSpeed);
    }
}
