using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseHandling : MonoBehaviour {

    public static bool isPaused = false;
    public Image pauseImg;

	// Use this for initialization
	void Start () {
        pauseImg.gameObject.SetActive(false);
	}
    
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if(isPaused == true)
            {
                Time.timeScale = 0;
                pauseImg.gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pauseImg.gameObject.SetActive(false);
            }

        }
	}
}
