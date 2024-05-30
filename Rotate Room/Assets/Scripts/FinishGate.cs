using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGate : MonoBehaviour
{
    [SerializeField] private float attraction = 1f;
    [SerializeField] private CircleCollider2D circleCollider;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.tag != "Player") return;
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        Vector3 direction = transform.position - collision.transform.position;
        if (direction.magnitude < circleCollider.radius / 2)
        {
            rb.MovePosition(transform.position);
            circleCollider.isTrigger = false;
            Time.timeScale = 1f;
            //Finish game!!!!!!
            GameManager.instance.SetWinState();
            GameManager.instance.ShowContinueMenu();
        }
        else
        {
            rb.velocity /= 2;
            rb.position += Vector2.ClampMagnitude(direction, attraction);
            Time.timeScale = 0.2f;
        }
    }
}
