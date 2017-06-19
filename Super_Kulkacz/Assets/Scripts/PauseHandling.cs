using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseHandling : MonoBehaviour {

    // deklaracje: m. in. zmienna określająca stan wstrzymania gry, obrazek wyświetlający się w trakcie pauzy
    public static bool isPaused = false;
    public Image pauseImg;
    public AudioSource audSrc;

	void Start () {
        pauseImg.gameObject.SetActive(false);   // na początku obrazek pauzy jest nieaktywny
	}
    
    // po naciśnięciu Esc, aktywuje / dezaktywuje się obrazek pauzy, zmienna określająca stan wstrzymania gry ulega zmianie, muzyka w grze się zatrzymuje / wznawia, --
	void Update () {                                                              // -- zmienia się stan wyświetlania kursora, oraz czas ulega zatrzymaniu / wznowieniu
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if(isPaused == true)
            {
                audSrc.Pause();
                Time.timeScale = 0;
                pauseImg.gameObject.SetActive(true);
                Cursor.visible = true;
            }
            else
            {
                audSrc.Play();
                Time.timeScale = 1 * Movement.timeSpeed;
                pauseImg.gameObject.SetActive(false);
                Cursor.visible = false;
            }
        }
	}
}