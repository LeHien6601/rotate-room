using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    //Kills player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        if (collision.gameObject.tag == "Player" && collision.rigidbody.bodyType == RigidbodyType2D.Dynamic)
        {
            collision.gameObject.GetComponent<PlayerMovement>().Dead();
        }
    }
}
