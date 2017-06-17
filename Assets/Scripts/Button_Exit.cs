using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Exit : MonoBehaviour {

    public Button exit_button;

    void Start()
    {
        Button exit_btn = exit_button.GetComponent<Button>();
        exit_btn.onClick.AddListener(ExitGame);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
