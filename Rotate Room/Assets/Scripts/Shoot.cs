using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shoot : MonoBehaviour
{
    [SerializeField] int RotationState = 0;
    [SerializeField] int maxNumBullets = 15;
    [SerializeField] FollowPlayer camFollow;
    [SerializeField] float speed;
    [SerializeField] GameObject bulletPrefab;
    // [SerializeField] GameObject bulletCounterPrefab;
    [SerializeField] TMP_Text count;
    [SerializeField] ParticleSystem particle;
    [SerializeField] int Direction;
    [SerializeField] int numOfBullets;
    private Vector2 bulletCountCurrentPos;
    private Vector2 firePosition;
    private Vector2 bulletVelocity;
    private List<GameObject> bullets;
    public int NumOfBullets
    {
        get
        {
            return numOfBullets;
        }
        set
        {
            if(value < maxNumBullets) numOfBullets = value;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if(numOfBullets > maxNumBullets) numOfBullets = maxNumBullets;

        // bullets = new List<GameObject>();

        // bulletCountCurrentPos = new Vector2(transform.position.x - 0.3f, transform.position.y + 0.6f);
        // for(int i = 0; i < numOfBullets; i++)
        // {
        //     GameObject tempbullet = Instantiate(bulletCounterPrefab, bulletCountCurrentPos, Quaternion.identity, transform.parent);
        //     bullets.Add(tempbullet);
        //     bulletCountCurrentPos += new Vector2(0.1f, 0.0f);
        // }
    }
    // Update is called once per frame

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && numOfBullets > 0)
        {
            Fire();            
            // Destroy(bullets[numOfBullets-1]);
            numOfBullets--;
        }

        count.text = numOfBullets.ToString() + "/" + maxNumBullets.ToString();

        if (camFollow.timer == 0)   
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RotationState = (RotationState + 1) % 4;
            }
            else if(Input.GetKeyDown(KeyCode.E))
            {
                RotationState = (RotationState - 1) % 4;
            }
        }

        switch(RotationState)
        {
            case 0:
            firePosition = new Vector2(transform.position.x + Direction * 0.7f, transform.position.y);
            bulletVelocity = new Vector2(Direction, 0);
            break;
            case 1:
            firePosition = new Vector2(transform.position.x, transform.position.y - Direction * 0.7f);
            bulletVelocity = new Vector2(0, -Direction);
            break;
            case 2:
            firePosition = new Vector2(transform.position.x - Direction * 0.7f, transform.position.y);
            bulletVelocity = new Vector2(-Direction, 0);
            break;
            case 3:
            firePosition = new Vector2(transform.position.x, transform.position.y + Direction * 0.7f);
            bulletVelocity = new Vector2(0, Direction);
            break;
        }        
    }
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.A) && Direction == 1)
        {
            transform.Rotate(180, 0, 0);
            Direction = -1;         
        }
        if(Input.GetKey(KeyCode.D) && Direction == -1)
        {
            transform.Rotate(180, 0, 0);
            Direction = 1;            
        }


    }


    void Fire()
    {
        //Copy tu Cannon.cs
        particle.Play();
        GameObject bullet = Instantiate(bulletPrefab, firePosition, Quaternion.identity);        
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = bulletVelocity * speed;
    }
}
