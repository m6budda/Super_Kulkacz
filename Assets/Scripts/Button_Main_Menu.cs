using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button_Main_Menu : MonoBehaviour {

    public Button menu_button;

    void Start()
    {
        Button menu_btn = menu_button.GetComponent<Button>();
        menu_btn.onClick.AddListener(GoToMenu);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main_Menu");
    }
}
