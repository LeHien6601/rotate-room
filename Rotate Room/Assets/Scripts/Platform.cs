using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Collider2D coll;
    [SerializeField] private PlatformEffector2D effector;

    //Gets player on platform
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.tag == "Player")
        {
            coll.isTrigger = false;
            collision.gameObject.transform.parent = null;
        }
    }

    //Player left platform
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision == null) return;
        if (collision.collider.tag == "Player")
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
