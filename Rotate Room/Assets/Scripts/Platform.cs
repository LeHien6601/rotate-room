using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Collider2D coll;
    [SerializeField] private PlatformEffector2D effector;
    //Player left platform
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.tag == "Player" )
        {
            coll.isTrigger = false;
            collision.gameObject.transform.SetParent(null);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision == null) return;
        if (collision.collider.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
    //Player's on platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        if (collision.collider.tag == "Player")
        {
            BoxCollider2D boxCollider = collision.collider.GetComponent<BoxCollider2D>();
            if (collision.transform.position.y + boxCollider.offset.y - boxCollider.size.y/2f >= transform.position.y)
                collision.gameObject.transform.SetParent(transform);
        }
    }
}
