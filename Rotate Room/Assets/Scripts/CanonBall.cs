using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    [SerializeField] private Collider2D coll;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private ParticleSystem particle;
    private bool isExploded = false;
    private void Start()
    {
        particle.Stop();
    }
    void Update()
    {
        if (isExploded)
        {
            if (particle.isStopped)
            {
                Destroy(gameObject);
            }
            coll.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a / 1.1f);
        }
        else if (rb.velocity.magnitude < 0.05f) {
            isExploded = true;
            particle.Play();
        }
    }
    //Hits and explodes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && !isExploded)
        {
            isExploded = true;
            particle.Play();
        }
    }
    //Flies out of canon

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            coll.isTrigger = false;
        }
    }
    //Hits player and explodes (still in canon)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().Dead();
            isExploded = true;
            particle.Play();
        }
    }
}
