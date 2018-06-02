using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    [SerializeField]
    GameObject player;
    [SerializeField]
    Transform gun;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
   float bulletSpeed;
    [SerializeField]
    float lengthRay;
    private SpriteRenderer[] rend;
    Rigidbody2D rigid;
    [SerializeField]
    float speed = 1f;
    bool bulletReady=true;
    bool isFlipped=false;
    enum enemyState
    {
        PATROL,
        SHOOT
    };

    private enemyState state;
	// Use this for initialization
	void Start () {
		state = enemyState.PATROL;
        rigid = GetComponent<Rigidbody2D>();
        rend = GetComponents<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case enemyState.PATROL:Patrol();
                break;
            case enemyState.SHOOT:
                if (bulletReady)
                    StartCoroutine("Shoot");
                break;
           
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
       
        Gizmos.DrawRay(gun.position, gun.right * lengthRay);
    }
    private void Patrol()
    {
        RaycastHit2D hit = Physics2D.Raycast(gun.position, gun.right*lengthRay);
       

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag.Equals("Player"))
            {
                state = enemyState.SHOOT;
            }
            else
            {
                Move();
                
            }
        }
        else
        {
            Move();
            
        }



    }

    
    private void Move()
    {
        rigid.velocity = speed*-transform.right;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("limLeft")|| collider.gameObject.tag.Equals("limRight"))
        {
            StartCoroutine("Flip");
            
        }
    }
   

    IEnumerator Flip()
    {
      
        transform.Rotate(0, -180, 0);
        
        yield return new WaitForSeconds(0.5f);
        

    }

    IEnumerator Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(gun.position, gun.right * lengthRay);


        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag.Equals("Player"))
            {
                bulletReady = false;
                GameObject bulletTemp = Instantiate(bullet,gun.transform);
                bulletTemp.SetActive(true);
                bulletTemp.GetComponent<Rigidbody2D>().velocity = bulletSpeed * gun.transform.right;
                Destroy(bulletTemp, 6);
                yield return new WaitForSeconds(1f);
                bulletReady = true;
            }
            else
            {
                state = enemyState.PATROL;
            }
        }
        else
        {
            state = enemyState.PATROL;
        }


    }
}
