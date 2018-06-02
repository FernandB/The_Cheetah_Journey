using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private GameObject CanvasPause;
    // Use this for initialization
    void Start () {
        rigidPlayer = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
      
        GetInput();
    }

    private void GetInput()
    {

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            rigidPlayer.AddForce(new Vector2(move, jumpDist), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown("escape") && !CanvasPause.activeSelf)
        {
            CanvasPause.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void FixedUpdate()
    {

        move = Input.GetAxis("Horizontal");
        
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

    void Flip()
    {
        if (!isFlipped)

            rend.flipX = false;
        else
            rend.flipX = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("spikes"))
        {
            SceneManager.LoadScene("gameOver");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("floor"))
        {
            isJumping = false;
        }
        if (collision.gameObject.tag.Equals("over"))
        {
            SceneManager.LoadScene("gameOver");
        }

        if (collision.gameObject.tag.Equals("victory"))
        {
            SceneManager.LoadScene("mainScene");
        }
    }

}
