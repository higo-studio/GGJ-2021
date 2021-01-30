using System;
using UnityEngine;
using System.Collections.Generic;

public interface ICharacterController
{
    bool IsOnGround();
    void SetWalkDirection(float x, float y);
    void SetWalkDirection(Vector2 dir);
    void Jump();
    void SetJumpForce();
    void SetGravity(float inGravity);
    Rigidbody2D Body();
}

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class SlimeController : MonoBehaviour, ICharacterController
{
    private bool _isOnGround = false;
    private Rigidbody2D _body;
    private CircleCollider2D _collider;
    public float Gravity = 20;
    public float JumpForce = 1;
    private float _gravityVerticalVelocity;
    private float _jumpVerticalVelocity;
    private List<ContactPoint2D> _tempContacts;
    private Vector2 _walkDir;



    public Rigidbody2D Body() => _body;
    public CircleCollider2D Collider => _collider;
    public bool IsOnGround() => _isOnGround;

    public event Action<RaycastHit2D> onControllerCollidedEvent;
    public event Action<Collider2D> onTriggerEnterEvent;
    public event Action<Collider2D> onTriggerStayEvent;
    public event Action<Collider2D> onTriggerExitEvent;

    [Range(0f, 90f)]
    public float slopeLimt = 70;

    # region unity lifecycle
    protected void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();

        _body.bodyType = RigidbodyType2D.Kinematic;
        _tempContacts = new List<ContactPoint2D>(20);
    }

    protected void FixedUpdate()
    {
        var dt = Time.fixedDeltaTime;

        Debug.Log(_isOnGround);
        if (_isOnGround)
        {
            _gravityVerticalVelocity = 0;
        }

        _isOnGround = false;

        Recover();
        HandleVertical(dt);
        HandleHorizontal(dt);
    }


    # endregion

    # region public interface
    public void Jump()
    {
        Debug.Log("Jump");
        if (!_isOnGround) return;
        _jumpVerticalVelocity = JumpForce;
    }

    public void SetWalkDirection(float x, float y)
    {
        _walkDir.Set(x, y);
    }

    public void SetWalkDirection(Vector2 dir)
    {
        SetWalkDirection(dir.x, dir.y);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("OnTriggerEnter2D");
        if (onTriggerEnterEvent != null)
            onTriggerEnterEvent(col);
    }


    public void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log("OnTriggerStay2D");
        if (onTriggerStayEvent != null)
            onTriggerStayEvent(col);
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("OnTriggerExit2D");
        if (onTriggerExitEvent != null)
            onTriggerExitEvent(col);
    }

    public void SetJumpForce()
    {

    }

    public void SetGravity(float inGravity)
    {
        Gravity = inGravity;
    }

    #endregion

    # region private function
    protected void HandleVertical(float dt)
    {
        if (_jumpVerticalVelocity > 0)
        {
            _gravityVerticalVelocity = _jumpVerticalVelocity;
            _jumpVerticalVelocity = 0;
            Debug.Log("111");
        }
        _gravityVerticalVelocity += -Gravity * dt;
        var y = _gravityVerticalVelocity * dt;
        _body.position += new Vector2(0, y);

    }

    protected void HandleHorizontal(float dt)
    {
        _body.position += _walkDir;
    }

    private Collider2D[] _tempCollider = new Collider2D[20];
    protected bool Recover()
    {
        var scale = Mathf.Max(transform.localScale.x, transform.localScale.y);
        var hitCount = Physics2D.OverlapCircleNonAlloc(_body.position, _collider.radius * scale, _tempCollider);
        _isOnGround = false;
        var sourcePos = _body.position;
        if (hitCount <= 0) return false;
        for (var i = 0; i < hitCount; i++)
        {
            var c = _tempCollider[i];
            if (c == _collider) continue;
            var distance = c.Distance(_collider);
            if (distance.isOverlapped)
            {
                var angle = Vector2.Angle(distance.normal, Vector2.up);
                Debug.Log($"angle: {angle}");
                if (angle < slopeLimt && _gravityVerticalVelocity < 0)
                {
                    _isOnGround = true;
                }
                sourcePos += (distance.pointA - distance.pointB) * 0.8f;
            }
        }

        _body.position = sourcePos;
        // _body.GetContacts(_tempContacts);
        // var sourcePos = _body.position;
        // foreach (var c in _tempContacts)
        // {
        //     sourcePos += Mathf.Abs(c.separation) * c.normal;
        //     _isOnGround = true;
        // }

        // _body.position = sourcePos;
        return _isOnGround;
        // return true;
    }

    protected bool ShouldCollide(Collider2D a, Collider2D b)
    {
        return true;
    }

    # endregion
}