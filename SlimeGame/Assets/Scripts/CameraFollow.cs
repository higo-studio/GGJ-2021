using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;

    protected void LateUpdate()
    {
        var dt = Time.deltaTime;
        var source = transform.position;
        source.x = Mathf.Lerp(source.x, target.transform.position.x, dt * 2);
        transform.position = source;
    }
}