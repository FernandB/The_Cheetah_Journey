using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseController : MonoBehaviour {
    [SerializeField]
    Button btnContinue;
    [SerializeField]
    Button btnExit;
    // Use this for initialization
    void Start()
    {
        btnContinue.onClick.AddListener(() => Continue());
        btnExit.onClick.AddListener(() => ExitGame());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Continue()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
