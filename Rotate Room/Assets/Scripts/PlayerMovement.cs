using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] FollowPlayer camFollow;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpStrength = 2f;
    [SerializeField] private float gravityScale = 5f;
    public List<Vector2> directions = new List<Vector2>() { Vector2.up, Vector2.right, Vector2.down, Vector2.left};
    private int jumpCount = 0;
    private bool jumpTrigger = false;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private SpriteRenderer sprite;
    private bool isDead = false;
    private bool onGround = false;
    private float offGroundTimer = 0f;
    private Color initialColor;
    private void Start()
    {
        particle.Stop();
        initialColor = sprite.color;
    }

    private void Update()
    {
        //Stable camera's state -> Ready for new rotation
        if (camFollow.timer == 0)   
        {
            if (Input.GetKeyDown(KeyCode.E))    //Rotate clockwise
            {
                transform.parent = null;
                camFollow.RotateCamera(90);
                transform.RotateAround(transform.position, Vector3.forward, 90);
                directions.Insert(0, directions[3]);
                directions.RemoveAt(4);
            }
            else if (Input.GetKeyDown(KeyCode.Q))   //Rotate counter-clockwise
            {
                transform.parent = null;
                camFollow.RotateCamera(-90);
                transform.RotateAround(transform.position, Vector3.forward, -90);
                directions.Add(directions[0]);
                directions.RemoveAt(0);
            }
        }
        //Dead trigger
        if (isDead)
        {
            if (particle.isStopped) //Reload scence after stopping particle system duration
            {
                //End game!!!!!!!
                if (SceneManager.GetActiveScene().name == "MainMenu")
                {
                    sprite.color = initialColor;
                    isDead = false;
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    return;
                }
                GameManager.instance.SetLoseState();
                GameManager.instance.ShowRestartMenu();
            }
            //Faded player
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a / 2);
        }
        //Check if player on ground or not -> Be able to jump
        if (!OnGround() && offGroundTimer <= 0) return;
        if (Input.GetKeyDown(KeyCode.W) && jumpCount > 0)
        {
            jumpTrigger = true;
        }
    }

    private void FixedUpdate()
    {

        if (rb.bodyType != RigidbodyType2D.Dynamic) return;

        //Check if player on platform or not
        Collider2D platform = OnPlatform();
        if (platform != null && !platform.isTrigger)
        {
            //Player sticks with platform -> Be child of platform
            gameObject.transform.SetParent(platform.transform);
            if (Input.GetKeyDown(KeyCode.S))    //Player off platform
            {
                platform.isTrigger = true;
            }
        }
        else
        {
            //Normal gravity if not on platform
            rb.AddForce(directions[2] * gravityScale);
        }

        //Jump trigger
        if (jumpTrigger)
        {
            if (offGroundTimer <= 0)
            {
                offGroundTimer = 0;
            }
            else
            {
                offGroundTimer -= Time.deltaTime;
            }
            rb.velocity += directions[0] * jumpStrength;
            jumpCount--;
            jumpTrigger = false;
        }

        //Player moves Left/Right
        if (rb.bodyType != RigidbodyType2D.Dynamic) return;
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += (Vector3) directions[3] * speed * Time.fixedDeltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += (Vector3) directions[1] * speed * Time.fixedDeltaTime;
        }
    }
    //Checks player on ground or not
    private bool OnGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position + directions[2] * 0.5f, new Vector2(1f, 0.1f), 
                                              Vector2.SignedAngle(Vector2.left, directions[1]), directions[2], 0f, LayerMask.GetMask("Obstacle"));
        if (hit.collider == null)
        {
            if (onGround)
            {
                offGroundTimer = 1f;
            }
            onGround = false;
            return false;
        }
        if (directions[0] == Vector2.up || directions[0] == Vector2.down)
        {
            if (rb.velocity.y == 0f) jumpCount = 1;
        }
        else
        {
            if (rb.velocity.x == 0f) jumpCount = 1;
        }
        onGround = true;
        return true;
    }
    //Dead trigger
    public void Dead()
    {
        rb.bodyType = RigidbodyType2D.Static;
        particle.Play();
        isDead = true;
    }
    //Returns platform player stands on
    private Collider2D OnPlatform()
    {
        RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position + directions[2] * 0.5f, new Vector2(1f, 0.1f),
                                              Vector2.SignedAngle(Vector2.left, directions[1]), directions[2], 0f, LayerMask.GetMask("Obstacle"));
        if (hit.collider == null) return null;
        if (hit.collider.tag == "Platform")
        {
            return hit.collider;
        }
        return null;
    }
    //Attracted to finish gate
    public void Finish(Vector3 finishGate, float attraction, float radius)
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        Vector3 direction = finishGate - transform.position;
        if (direction.magnitude < radius / 2)
        {
            rb.MovePosition(finishGate);
            //Finish game!!!!!!
        }
        else
        {
            rb.velocity /= 2;
            transform.position += Vector3.ClampMagnitude(direction, attraction);
        }
    }
}
