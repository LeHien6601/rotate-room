using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Canon : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float speed;
    [SerializeField] private GameObject canonBallPrefab;
    [SerializeField] private Vector2 firePoint;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float delayTime;
    private float timer = 0f;
    private Vector2 firePosition;
    private void Start()
    {
        timer = duration + delayTime;
    }
    private void Update()
    {
        if (timer == 0f)
        {
            //Fire canonball
            timer = duration;
            Fire();
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer < 0f) timer = 0f;
        }
    }

    void Fire()
    {
        particle.Play();
        GameObject canonBall = Instantiate(canonBallPrefab, firePosition + (Vector2)transform.position, Quaternion.identity, transform.parent);
        Rigidbody2D ballRb = canonBall.GetComponent<Rigidbody2D>();
        float rad = transform.eulerAngles.z * Mathf.Deg2Rad;
        ballRb.velocity = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized * speed;
    }

    private void OnDrawGizmos()
    {
        float angle = Vector2.SignedAngle(Vector2.right, transform.right) * Mathf.Deg2Rad;
        firePosition = new Vector2(firePoint.x * Mathf.Cos(angle) - firePoint.y * Mathf.Sin(angle), firePoint.x * Mathf.Sin(angle) + firePoint.y * Mathf.Cos(angle));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePosition + (Vector2)transform.position, 0.3f);
    }
}
