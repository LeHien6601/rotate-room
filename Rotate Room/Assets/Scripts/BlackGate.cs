using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackGate : MonoBehaviour
{
    [SerializeField] private float attraction = 1f;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private Transform whiteGate;
    private Vector2 initialVelocity;
    private float initialAlpha;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.tag != "Player") return;
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        initialVelocity = rb.velocity;
        initialAlpha = collision.GetComponent<SpriteRenderer>().color.a;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.tag != "Player") return;
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        SpriteRenderer sprite = collision.GetComponent<SpriteRenderer>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        Vector3 direction = transform.position - collision.transform.position;
        if (direction.magnitude < circleCollider.radius / 4)
        {
            collision.transform.position = whiteGate.position;
            //Teleports to white gate!!!!!!
            rb.bodyType = RigidbodyType2D.Dynamic;
            float angle = Vector2.SignedAngle(transform.right, whiteGate.right) * Mathf.Deg2Rad;
            rb.velocity = new Vector2(initialVelocity.x * Mathf.Cos(angle) - initialVelocity.y * Mathf.Sin(angle),
                                      initialVelocity.x * Mathf.Sin(angle) + initialVelocity.y * Mathf.Cos(angle));
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, initialAlpha);
        }
        else
        {
            //Attracts to black gate!!!!!
            rb.velocity /= 2;
            rb.position += Vector2.ClampMagnitude(direction, attraction);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a / 2);
        }
    }
}
