using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadedBlock : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float duration;
    private float lifeTimer = 0f;
    private float timer = 0f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D coll;
    [SerializeField] private SpriteRenderer sprite;
    private bool isFaded = false;
    private bool isCollapsed = false;
    private float initialAlpha = 0f;
    private List<float> randomShake;
    private int currentShake = 0;
    private float initialX = 0f;

    private void Start()
    {
        initialAlpha = sprite.color.a;
        initialX = transform.position.x;
        float sum = 0;
        randomShake = new List<float>();
        for (int i = 0; i < 10; i++)
        {
            float newElement = Random.Range(-0.02f, 0.02f);
            sum += newElement;
            randomShake.Add(newElement);
        }
        randomShake.Add(-sum);
    }
    private void Update()
    {
        if (isCollapsed)
        {
            Shake();
            lifeTimer -= Time.deltaTime;
            if (lifeTimer <= 0)
            {
                Fade();
                lifeTimer = 0;
            }
        }
        else if (isFaded)
        {
            sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a / 1.1f);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Appear();
                timer = 0;
            }
        }
        else if (initialAlpha > sprite.color.a)
        {
            float newAlpha = Mathf.Min(sprite.color.a + 10, initialAlpha);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, newAlpha);
        }
    }
    //Shaking effect when collapsing
    private void Shake()
    {
        transform.position += Vector3.up * randomShake[currentShake];
        currentShake = (currentShake + 1) % randomShake.Count;
    }
    //Seting when fading
    private void Fade()
    {
        isCollapsed = false;
        isFaded = true;
        coll.enabled = false;
        timer = duration;
        currentShake = 0;
    }
    //Seting when appearing
    private void Appear()
    {
        transform.position = new Vector3(initialX, transform.position.y);
        rb.bodyType = RigidbodyType2D.Kinematic;
        isFaded = false;
        coll.enabled = true;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 50);
    }
    //Collapsed trigger
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        if (isCollapsed) return;
        isCollapsed = true;
        lifeTimer = lifeTime;
    }
}
