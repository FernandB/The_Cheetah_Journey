using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject gunPrefab;
    Rigidbody2D rigid;

	// Use this for initialization
	void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine("Shoot");
	}
	
	// Update is called once per frame
	void Update ()
    {
        Follow();
        
	}

    private void Follow()
    {
        if (player.transform.position.x < transform.position.x)
            rigid.velocity = Vector2.left;
        else
            rigid.AddForce(new Vector2(-4f, 4f), ForceMode2D.Impulse); 
    }

    IEnumerator Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab,gunPrefab.transform);
        bullet.SetActive(true);
        Rigidbody2D rigidBullet = bullet.GetComponent<Rigidbody2D>();
        rigidBullet.velocity = new Vector2(-15f, 0f);
        yield return new WaitForSeconds(2.5f);
        StartCoroutine("Shoot");
    }
}
