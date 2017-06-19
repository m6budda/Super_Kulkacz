using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button_Handling : MonoBehaviour {

    // deklaracje; m. in. obrazek pauzy i obrazek instrukcji gry w menu
    public Image pauseMenu;
    public AudioSource audSource;
    public Image instructions;

    private void Start()
    {
        instructions.gameObject.SetActive(false);   //wszystko dziala idealnie, ale krzyczy program (w logach), że to tu jest...
    }

    // przycisk kontynuowania gry po pauzie
    public void ContinueButton()
    {
        audSource.Play();
        Time.timeScale = 1f * Movement.timeSpeed; // zwiększenie prędkości gry (i zmniejszenie prędkości potworów; skrypt Enemy_Movement) potrzebne --
        pauseMenu.gameObject.SetActive(false);    // -- żeby potworki poruszały się bardziej kinematycznie niż dynamicznie
        PauseHandling.isPaused = !PauseHandling.isPaused;
        Cursor.visible = false;  // w grze kursor jest niewidoczny
    }

    // przycisk wyjścia z gry
    public void ExitGame()
    {
        Application.Quit();
    }

    // przycisk powrotu do menu głównego
    public void GoToMenu()
    {
        Time.timeScale = 1f;                    // w menu prędkość gry normalna (x1)
        SceneManager.LoadScene("Main_Menu");
    }

    // przycisk rozpoczęcia gry
    public void StartingGame()
    {
        Time.timeScale = 1f * Movement.timeSpeed;
        SceneManager.LoadScene("Level");
        Cursor.visible = false;
    }

    // przycisk włączenia instrukcji gry w menu głównym
    public void InstructionsEnter()
    {
        instructions.gameObject.SetActive(true);
    }

    // przycisk wyłączenia instrukcji gry w menu głównym
    public void InstructionsExit()
    {
        instructions.gameObject.SetActive(false);
    }
}