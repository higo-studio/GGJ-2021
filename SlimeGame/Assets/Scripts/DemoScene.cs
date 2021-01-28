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

	public CharacterController2D Controller;
	public Animator Animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;


	void Awake()
	{
		// Animator = GetComponent<Animator>();
		Controller = GetComponent<CharacterController2D>();

		// listen to some events for illustration purposes
		Controller.onControllerCollidedEvent += onControllerCollider;
		Controller.onTriggerEnterEvent += onTriggerEnterEvent;
		Controller.onTriggerExitEvent += onTriggerExitEvent;
	}


	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void onTriggerEnterEvent( Collider2D col )
	{
		Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
	}


	void onTriggerExitEvent( Collider2D col )
	{
		Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	}

	#endregion


	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
		if( Controller.isGrounded )
			_velocity.y = 0;

		var goTrans = Controller.gameObject.transform;
		if( Input.GetKey( KeyCode.RightArrow ) )
		{
			normalizedHorizontalSpeed = 1;
			if( goTrans.localScale.x < 0f )
				goTrans.localScale = new Vector3( -goTrans.localScale.x, goTrans.localScale.y, goTrans.localScale.z );

			// if( Controller.isGrounded )
			// 	Animator.Play( Animator.StringToHash( "Run" ) );
		}
		else if( Input.GetKey( KeyCode.LeftArrow ) )
		{
			normalizedHorizontalSpeed = -1;
			if( goTrans.localScale.x > 0f )
				goTrans.localScale = new Vector3( -goTrans.localScale.x, goTrans.localScale.y, goTrans.localScale.z );

			// if( Controller.isGrounded )
			// 	Animator.Play( Animator.StringToHash( "Run" ) );
		}
		else
		{
			normalizedHorizontalSpeed = 0;

			// if( Controller.isGrounded )
			// 	Animator.Play( Animator.StringToHash( "Idle" ) );
		}


		// we can only jump whilst grounded
		if( Controller.isGrounded && Input.GetKeyDown( KeyCode.UpArrow ) )
		{
			_velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
			// Animator.Play( Animator.StringToHash( "Jump" ) );
		}


		// apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
		var smoothedMovementFactor = Controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );

		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

		// if holding down bump up our movement amount and turn off one way platform detection for a frame.
		// this lets us jump down through one way platforms
		if( Controller.isGrounded && Input.GetKey( KeyCode.DownArrow ) )
		{
			_velocity.y *= 3f;
			Controller.ignoreOneWayPlatformsThisFrame = true;
		}

		Controller.move( _velocity * Time.deltaTime );

		// grab our current _velocity to use as a base for all calculations
		_velocity = Controller.velocity;
	}

}
