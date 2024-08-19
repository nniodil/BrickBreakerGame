using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool InPlay;
    public Transform paddle;
    public float speed;
    public Transform ParticleEffects;
    public GameManager gm;
    public Transform ExtraLife;
    AudioSource ballHit;

   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ballHit = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (gm.gameOver)
        {
            return;
        }

        if (!InPlay)
        {
            transform.position = paddle.position;
        }

        if (Input.GetButtonDown("Jump") && !InPlay)
        {
            InPlay = true;
            rb.AddForce(Vector2.up * speed);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bottom"))
        {
            Debug.Log("ball hit the bottom of the screen");
            rb.velocity = Vector2.zero;
            InPlay = false;
            gm.UpdateLives(-1);
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("brick"))
        {
            BrickScript brickScript = other.gameObject.GetComponent<BrickScript>();
            if (brickScript.hitsToBreaks > 1)
            {
                brickScript.BrickBreak();
            }
            else
            {   
                if (gm.lives < 3)
                {
                    int randChance = Random.Range(0, 101);
                    
                    if (randChance < 20)
                    {
                        Instantiate(ExtraLife, other.transform.position, other.transform.rotation);
                    }
                }
                Transform newParticleEffects = Instantiate(ParticleEffects, other.transform.position, other.transform.rotation);
                Destroy(newParticleEffects.gameObject, 2.5f);
                gm.UpdateScore(brickScript.points);
                gm.UpdateNumberOfBricks();
                Destroy(other.gameObject);
            }
        }
        
        GetComponent<AudioSource>().Play();
        }      
}
