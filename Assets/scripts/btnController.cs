using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class btnController : MonoBehaviour {
    [SerializeField]
    Button btnStart;
    [SerializeField]
    Button btnExit;
    // Use this for initialization
    void Start () {
       
            
                btnStart.onClick.AddListener(() => StartGame());
            
           
                btnExit.onClick.AddListener(() => ExitGame());
                
            
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void StartGame()
    {
        SceneManager.LoadScene("lvl1");
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
