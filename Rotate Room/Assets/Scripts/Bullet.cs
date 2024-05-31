using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
    }
    //Hits and explodes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && !isExploded)
        {
            if(collision.gameObject.tag == "Obstacle")
            {
                collision.gameObject.GetComponent<DestroyObstacle>().DestroyThis();
                isExploded = true;
                particle.Play();
            }
            else if(collision.gameObject.tag == "Wall")
            {
                isExploded = true;
                particle.Play();
            }

        }
    }
    //Flies out of canon
}
