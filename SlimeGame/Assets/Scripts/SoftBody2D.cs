using UnityEngine;
using UnityEngine.U2D;
using System;

public class SoftBody2D : MonoBehaviour
{
    CircleCollider2D[] colliders;
    CircleCollider2D centerCollider;
    SpriteShapeController shapeController;
    public Sprite sprite;

    public int SubBallCount = 9;
    public int Frequency = 7;


    void Awake()
    {
        // renderer = GetComponent<SpriteRenderer>();
        centerCollider = GetComponent<CircleCollider2D>();
        shapeController = GetComponent<SpriteShapeController>();

        var length = SubBallCount;
        colliders = new CircleCollider2D[length];
        var radius = 0.5f;
        var stepDeg = 360 / length;
        for (var i = 0; i < length; i++)
        {
            var gameObject = new GameObject();
            gameObject.transform.parent = transform;
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

            preSpring.frequency = nextSpring.frequency = centerSpring.frequency = Frequency;
        }

        InitVerticies();
    }

    void Update()
    {
        UpdateVerticies();
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
            // var pre = colliders[((i - 1) + SubBallCount) % SubBallCount];
            var cur = colliders[i];
            // var next = colliders[((i + 1) + SubBallCount) % SubBallCount];
            shapeController.spline.SetPosition(i, cur.transform.localPosition + cur.transform.localPosition.normalized * 0.2f);
            shapeController.spline.SetLeftTangent(i, Quaternion.Euler(0, 0, -90) * cur.transform.localPosition * 0.2f);
            shapeController.spline.SetRightTangent(i, Quaternion.Euler(0, 0, 90) * cur.transform.localPosition * 0.2f);
        }
    }
}