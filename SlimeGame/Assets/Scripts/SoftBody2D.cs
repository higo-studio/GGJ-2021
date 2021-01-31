using UnityEngine;
using UnityEngine.U2D;
using System;
using System.Collections.Generic;

class SpringTuple
{
    public SpringJoint2D connectToPrevious;
    public SpringJoint2D connectToCenter;
    public SpringJoint2D connectToNext;
}

public class SoftBody2D : MonoBehaviour
{
    CircleCollider2D[] colliders;
    SpringTuple[] springs;
    CircleCollider2D centerCollider;
    SpriteShapeController shapeController;
    public Sprite sprite;

    public int SubBallCount = 9;
    public int Frequency = 7;

    [Range(0.5f, 2f)]
    public float DebugRadius = 0.5f;
    [Range(0.5f, 2f)]
    public float DebugSubRadius = 0.5f;
    public bool DebugVisual = false;

    private bool _debugVisual = false;

    private float _radius = 0.5f;
    public float Radius
    {
        get => _radius;
        set
        {
            updateRadius(value);
            _radius = value;
        }
    }

    private float _subRadius = 0.5f;
    public float SubRadius
    {
        get => _subRadius;
        set
        {
            _subRadius = value;
            updateSubBallRadius(_subRadius);
        }
    }

    public List<ContactPoint2D> tempContactList = new List<ContactPoint2D>();
    public List<ContactPoint2D> tempContactList1 = new List<ContactPoint2D>();
    public int subBallLayer = 0;
    [Range(0f, 90f)]
    public float slopeLimt = 70f;

    private float _gravityRate = 1f;

    private Vector2 _hitNormal;
    public Vector2 HitNormal => _hitNormal.normalized;
    public float GravityRate {
        get => _gravityRate;
        set {
            _gravityRate = value;
            updateGravityRate(value);
        }
    }


    void Awake()
    {
        // renderer = GetComponent<SpriteRenderer>();
        centerCollider = GetComponent<CircleCollider2D>();
        shapeController = GetComponent<SpriteShapeController>();

        var length = SubBallCount;
        colliders = new CircleCollider2D[length];
        springs = new SpringTuple[length];
        var radius = _radius;
        var stepDeg = 360 / length;
        centerCollider.attachedRigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        for (var i = 0; i < length; i++)
        {
            var gameObject = new GameObject();
            gameObject.transform.parent = transform;
            gameObject.layer = subBallLayer;
            gameObject.transform.localScale = new Vector3(
                0.5f, 0.5f, 1
            );
            var sp = gameObject.AddComponent<SpriteRenderer>();
            sp.sprite = sprite;
            var color = sp.color;
            color.a = 0f;
            sp.color = color;
            var rad = stepDeg * i * Mathf.Deg2Rad;
            gameObject.transform.localPosition = new Vector2(
                radius * Mathf.Cos(rad),
                radius * Mathf.Sin(rad)
            );
            var body = gameObject.AddComponent<Rigidbody2D>();
            body.freezeRotation = true;
            body.interpolation = RigidbodyInterpolation2D.Interpolate;
            body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            var circle = gameObject.AddComponent<CircleCollider2D>();
            // circle.radius = radius;
            colliders[i] = circle;

            // shapeController.spriteShape.cornerSprites
        }

        for (var i = 0; i < length; i++)
        {
            var pre = colliders[((i - 1) + length) % length];
            var cur = colliders[i];
            var next = colliders[((i + 1) + length) % length];

            var preSpring = cur.gameObject.AddComponent<SpringJoint2D>();
            preSpring.connectedBody = pre.attachedRigidbody;
            preSpring.autoConfigureDistance = false;
            preSpring.distance = Vector2.Distance(pre.transform.position, cur.transform.position);

            var nextSpring = cur.gameObject.AddComponent<SpringJoint2D>();
            nextSpring.connectedBody = next.attachedRigidbody;
            nextSpring.autoConfigureDistance = false;
            nextSpring.distance = Vector2.Distance(next.transform.position, cur.transform.position);

            var centerSpring = cur.gameObject.AddComponent<SpringJoint2D>();
            centerSpring.connectedBody = centerCollider.attachedRigidbody;
            centerSpring.autoConfigureDistance = false;
            centerSpring.distance = Vector2.Distance(centerCollider.transform.position, cur.transform.position);

            springs[i] = new SpringTuple()
            {
                connectToPrevious = preSpring,
                connectToCenter = centerSpring,
                connectToNext = nextSpring
            };

            preSpring.frequency = nextSpring.frequency = Frequency;
            centerSpring.frequency = Frequency  * 1.5f;
        }

        InitVerticies();
    }

    void Update()
    {
        UpdateVerticies();
// #if UNITY_EDITOR
        if (DebugRadius != Radius)
        {
            Radius = DebugRadius;
        }

        if (DebugSubRadius != SubRadius)
        {
            SubRadius = DebugSubRadius;
        }

        if (DebugVisual != _debugVisual)
        {
            _debugVisual = DebugVisual;
            updateDebugVisual(_debugVisual);
        }
// #endif
    }

    void InitVerticies()
    {
        shapeController.spline.Clear();
        for (var i = 0; i < SubBallCount; i++)
        {
            var cur = colliders[i];
            shapeController.spline.InsertPointAt(i, cur.transform.localPosition);
            shapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
        }
    }

    void UpdateVerticies()
    {
        for (int i = 0; i < SubBallCount; i++)
        {
            var cur = colliders[i];

            try
            {
                // var pre = colliders[((i - 1) + SubBallCount) % SubBallCount];
                // var next = colliders[((i + 1) + SubBallCount) % SubBallCount];
                shapeController.spline.SetPosition(i, cur.transform.localPosition + cur.transform.localPosition.normalized * 0.2f);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }

            shapeController.spline.SetLeftTangent(i, Quaternion.Euler(0, 0, -90) * cur.transform.localPosition * 0.2f);
            shapeController.spline.SetRightTangent(i, Quaternion.Euler(0, 0, 90) * cur.transform.localPosition * 0.2f);
        }
    }

    private bool _isOnGround = false;
    private bool _isOnWall = false;
    public bool IsOnground => _isOnGround;
    public bool IsOnWall => _isOnWall;

    void FixedUpdate()
    {
        _isOnWall = false;
        _isOnGround = false;
        tempContactList1.Clear();
        _hitNormal = Vector2.zero;

        foreach (var c in colliders)
        {
            c.GetContacts(tempContactList);
            tempContactList1.AddRange(tempContactList);
        }

        foreach (var c in tempContactList1)
        {
            if (
                c.collider.gameObject.layer == subBallLayer
                || c.collider.gameObject.layer == centerCollider.gameObject.layer
            )
            {
                continue;
            }

            _hitNormal += c.normal;
            var normalAngle = Vector2.Angle(Vector2.up, c.normal);
            if (normalAngle > slopeLimt)
            {
                _isOnWall = true;
                continue;
            }
            _isOnGround = true;
        }
    }

    public bool Jump(float verticalVel)
    {
        if (!(_isOnGround || _isOnWall)) return false;
        Debug.Log("jump");
        {
            var sourceVel = centerCollider.attachedRigidbody.velocity;
            sourceVel.y = verticalVel;
            centerCollider.attachedRigidbody.velocity = sourceVel;
        }
        foreach (var c in colliders)
        {
            // c.attachedRigidbody.AddForce(impluse, ForceMode2D.Impulse);
            var sourceVel = c.attachedRigidbody.velocity;
            sourceVel.y = verticalVel;
            c.attachedRigidbody.velocity = sourceVel;
        }
        return true;
    }

    private void updateRadius(float inRadius)
    {
        inRadius /= 2;
        var stepDeg = 360 / SubBallCount;
        var circle_chord_length = 2 * inRadius * Mathf.Sin(stepDeg * Mathf.Deg2Rad / 2);
        Debug.Log("circle" + circle_chord_length);
        for (var i = 0; i < SubBallCount; i++)
        {
            var c = colliders[i];
            var length = SubBallCount;

            var springTuple = springs[i];
            var preSpring = springTuple.connectToPrevious;
            var centerSpring = springTuple.connectToCenter;
            var nextSpring = springTuple.connectToNext;

            preSpring.distance = nextSpring.distance = circle_chord_length;
            centerSpring.distance = inRadius;
        }

        centerCollider.attachedRigidbody.AddForce(new Vector2(0, 2f));
    }

    private void updateSubBallRadius(float inRadius)
    {
        for (var i = 0; i < SubBallCount; i++)
        {
            var c = colliders[i];
            c.transform.localScale = new Vector2(inRadius, inRadius);
        }
    }

    private void updateDebugVisual(bool inDebugVisual)
    {
        for (var i = 0; i < SubBallCount; i++)
        {
            var c = colliders[i];
            var sp = c.GetComponent<SpriteRenderer>();
            var color = sp.color;
            color.a = inDebugVisual ? 0.5f : 0;
            sp.color = color;
        }
    }

    private void updateGravityRate(float rate)
    {
        centerCollider.attachedRigidbody.gravityScale = rate;
        foreach(var c in colliders)
        {
            c.attachedRigidbody.gravityScale = rate;
        }
    }
}