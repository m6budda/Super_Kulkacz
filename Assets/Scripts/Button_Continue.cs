using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button_Continue : MonoBehaviour {

    public Button contin_button;
    public Image pauseMenu;

    void Start()
    {
        Button contin_btn = contin_button.GetComponent<Button>();
        contin_btn.onClick.AddListener(ContinueButton);
    }


    public void ContinueButton()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
        PauseHandling.isPaused = !PauseHandling.isPaused;
    }



}
