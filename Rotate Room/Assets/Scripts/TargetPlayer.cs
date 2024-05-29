using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circleCollider;

    //Aims to player
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == null || collision.tag != "Player")
        {
            return;
        }
        Vector2 direction = collision.transform.position - transform.position;
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction, circleCollider.radius);
        bool hasPlayer = false;
        float playerDistance = 0f;
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.tag == "Player")
            {
                hasPlayer = true;
                playerDistance = hit[i].distance;
                break;
            }
        }
        if (!hasPlayer) return;
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.tag != "Player" && hit[i].collider.tag != "Canon")
            {
                if (playerDistance > hit[i].distance)
                {
                    return;
                }
            }
        }
        float targetAngle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, targetAngle);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (circleCollider != null)
            Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
    }
}
