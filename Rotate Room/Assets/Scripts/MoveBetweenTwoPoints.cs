using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenTwoPoints : MonoBehaviour
{
    [SerializeField] private Vector2 head;
    [SerializeField] private Vector2 tail;
    private Vector2 origin = Vector2.zero;
    [SerializeField] private float smoothness;
    [SerializeField] private float speed;
    private bool headToTail = true;
    private float amplitude = 0f;

    private void Start()
    {
        origin = transform.position;
        amplitude = (head - tail).magnitude / 2f;
    }

    //Moves between head and tail
    private void FixedUpdate()
    {
        Vector2 diff = origin - (Vector2)transform.position;
        if (headToTail)
        {
            diff += tail;
            if (diff.magnitude < 0.05f) headToTail = false;
        }
        else
        {
            diff += head;
            if (diff.magnitude < 0.05f) headToTail = true;
        }
        if (Mathf.Abs(amplitude - diff.magnitude) > smoothness * amplitude)
        {
            diff = diff.normalized * (diff.magnitude / amplitude);
            if (diff.magnitude < 0.1f) diff = diff.normalized * 0.1f;
        }
        else
        {
            diff = diff.normalized;
        }
        transform.position += (Vector3)diff * speed * Time.fixedDeltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (origin == Vector2.zero)
        {
            Gizmos.DrawLine(head + (Vector2)transform.position, tail + (Vector2)transform.position);
        }
        else Gizmos.DrawLine(head + origin, tail + origin);
    }
}
