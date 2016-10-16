using UnityEngine;
using System.Collections;

public class DamageTextBehaviour : MonoBehaviour {

    public Vector3 WorldOffset = Vector3.up * 2.0f;
    public Vector3 ScreenOffset = Vector3.zero;
    new private Transform transform;
    new private Camera camera;
    private Transform target;
    private Vector3 lastSeenTargetPosition;

    public void Awake()
    {
        transform = GetComponent<Transform>();
        target = transform.parent;
        camera = Camera.main;
    }

    public void LateUpdate()
    {
        target = transform.parent;
        if (target != null)
        {
            transform.position = camera.WorldToViewportPoint(target.position + WorldOffset) + ScreenOffset;
            lastSeenTargetPosition = target.position;
        }
        else
        {
            transform.position = camera.WorldToViewportPoint(lastSeenTargetPosition + WorldOffset) + ScreenOffset;
        }
    }
}
