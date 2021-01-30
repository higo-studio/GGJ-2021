using System;
using UnityEngine;

public class BbBullet : MonoBehaviour
{
    public float Speed = 1;
    public float GravityRate = 1;
    public Vector2 _initDir;

    public float Radius = 0.4f;
    public bool isHit = false;

    public Vector2 _verticalVelocity;
    public LayerMask layerMask = 0;

    private RaycastHit2D[] _tempRaycastHitArr = new RaycastHit2D[10];
    protected void FixedUpdate()
    {
        if (isHit) return;
        var dt = Time.fixedDeltaTime;
        var deltaPos = new Vector2();
        var gravity = Physics2D.gravity * GravityRate;
        deltaPos += Speed * _initDir * dt;

        _verticalVelocity += gravity * dt;
        deltaPos += _verticalVelocity * dt;
        var resultCount = Physics2D.CircleCastNonAlloc(transform.position, Radius, deltaPos.normalized, _tempRaycastHitArr, deltaPos.magnitude, layerMask);
        Debug.DrawRay(transform.position, deltaPos, Color.red, 1);
        if (resultCount > 0)
        {
            var result = _tempRaycastHitArr[0];
            isHit = true;
            Speed = 0;
            GravityRate = 0;
            transform.Translate(result.distance * deltaPos.normalized);
        }
        else
        {
            transform.Translate(deltaPos);
        }
    }
}