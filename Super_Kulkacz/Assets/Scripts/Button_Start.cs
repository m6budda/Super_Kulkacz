using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button_Start : MonoBehaviour {

    public Button start_button;

    void Start()
    {
        Button start_btn = start_button.GetComponent<Button>();
        start_btn.onClick.AddListener(StartingGame);
    }

    void StartingGame()
    {
        SceneManager.LoadScene("Level");
    }

}
