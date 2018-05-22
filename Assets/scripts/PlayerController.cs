using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private Transform maxpos;

    [SerializeField]
    private Transform minpos;
    [SerializeField]
    private Transform hillPos;
    [SerializeField]
    private Transform treePos;
    private static float hillBottom;
   [SerializeField]
    private  Transform minposFlag;
    [SerializeField]
    private GameObject CanvasPause;
    [SerializeField]
    private Transform maxposFlag;
    private static float treeBottom;
    [SerializeField]
    GameObject[] hills;
    [SerializeField]
    GameObject[] trees;
  
   
    [SerializeField]
    GameObject finalFlag;

    Camera cameraMain;


    [SerializeField]
    Rigidbody2D rigid;
    float speed = 3f;
    bool isJumping = false;

    // Use this for initialization
    void Start () {
        hillBottom = hillPos.position.y;
        treeBottom = treePos.position.y;
        CreateLevel(true);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Move();
        GetInput();
	}

    private void GetInput()
    {
        if(Input.GetKeyDown("escape")&&!CanvasPause.activeSelf)
        {
            CanvasPause.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void Move()
    {
        //Only jump and acceleration and decelleration forward are allowed in this game
        if (Input.GetKeyDown("space")&&!isJumping)
        {
            isJumping = true;
            rigid.AddForce(new Vector2(5f, 5f), ForceMode2D.Impulse);
        }

        if (!isJumping)
        {
            if (Input.GetAxis("Horizontal") > 0)
                rigid.velocity = Vector2.right * speed;
            if (Input.GetAxis("Horizontal") < 0)
                rigid.velocity = Vector2.left * speed;
        }
    }


    void CreateLevel(bool firstLevel)
    {
        ChunkManager.DeleteObjects();
        CreateBackground();

        if (!firstLevel)
        {
            PlaceFinalFlagPos();
            ChunkManager.buildChunk();
        }
    }

    

    private void PlaceFinalFlagPos()
    {
        float yPos = UnityEngine.Random.Range(minposFlag.position.y, maxposFlag.position.y);
        finalFlag.transform.Translate(new Vector3(0, yPos));

    }

    void CreateBackground()
    {
        
        float backgroundRandomValue = Mathf.Round(UnityEngine.Random.Range(1f, 3f));
        if (backgroundRandomValue == 1f)
        {
            
            //Background with only hills
            for (int i=0; i<trees.Length;i++)
            {
                trees[i].SetActive(false);
            }
            for (int i = 0; i < hills.Length; i++)
            {
                hills[i].SetActive(true);
                hills[i].transform.position =  new Vector3(UnityEngine.Random.Range(minpos.position.x, maxpos.position.x), hillBottom);
            }
        }
        else
        {
            if (backgroundRandomValue == 2f)
            {
                
                //Background with only trees
                for (int i= 0; i < trees.Length; i++)
                {
                    trees[i].SetActive(true);
                    trees[i].transform.position = new Vector3(UnityEngine.Random.Range(minpos.position.x, maxpos.position.x), treeBottom);
                }
                for (int i = 0; i < hills.Length; i++)
                {
                    hills[i].SetActive(false);
                }
            }
            else
            {
                if (backgroundRandomValue == 3f)
                {
                  
                    //Background with trees and collines
                    for(int i= 0; i < trees.Length; i++)
                    {
                        trees[i].SetActive(true);
                        trees[i].transform.position = new Vector3(UnityEngine.Random.Range(minpos.position.x, maxpos.position.x), treeBottom);
                    }
                    for(int i = 0; i < hills.Length; i++)
                    {
                        hills[i].SetActive(true);
                        hills[i].transform.position = new Vector3(UnityEngine.Random.Range(minpos.position.x, maxpos.position.x), hillBottom);
                    }
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("floor"))
            isJumping = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.tag.Equals("limitLeft"))
        {
            
            transform.Translate(new Vector3(2f, 0f));
            
        }
        else
        {
            if (collider.tag.Equals("limitRight"))
            {
                rigid.gravityScale = 0f;
                rigid.velocity = Vector2.zero;
                transform.Translate(new Vector3(-25f, 0f));
                CreateLevel(false);
                rigid.gravityScale = 1;
            }
            else
            {
                if (collider.tag.Equals("gameOver"))
                {
                    SceneManager.LoadScene("GameOver");
                }
            }
        }
    }
}
