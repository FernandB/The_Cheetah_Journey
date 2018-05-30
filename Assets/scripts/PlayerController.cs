using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    float speedMove = 5f;
    [SerializeField]
    float jumpDist = 5f;
    Animator anim;
    private SpriteRenderer rend;
    private Rigidbody2D rigidPlayer;
    private bool isFlipped=false;
    private bool isWalking = false;
    private bool isJumping = false;
    private float move;
    // Use this for initialization
    void Start () {
        rigidPlayer = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
       if(Input.GetButtonDown("Jump")&&!isJumping)
        {
            isJumping = true;
            rigidPlayer.AddForce(new Vector2(move, jumpDist), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {

        move = Input.GetAxis("Horizontal");
        if (!isJumping)
        {
            rigidPlayer.velocity = new Vector2(move * speedMove, rigidPlayer.velocity.y);
        
            if (move < 0)
            {
                isFlipped = true;
                anim.SetBool("walk", true);
            }
            else
            {

                if (move > 0)
                {
                    isFlipped = false;
                    anim.SetBool("walk", true);
                }
                else
                {
                    anim.SetBool("walk", false);
                }
            }

            Flip();
        }


    }

    void Flip()
    {
        if (!isFlipped)

            rend.flipX = false;
        else
            rend.flipX = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("floor"))
        {
            isJumping = false;
        }
    }

}
