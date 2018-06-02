using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BulletController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            if (collider.gameObject.tag.Equals("enemy"))
                Destroy(collider.gameObject);
            else
                if(!collider.gameObject.tag.Equals("limLeft")&&!collider.gameObject.tag.Equals("limRight"))
                Destroy(gameObject);
        }
    }

}
