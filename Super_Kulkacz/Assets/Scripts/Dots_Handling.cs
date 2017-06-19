using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dots_Handling : MonoBehaviour {

    // deklaracje: m. in. dźwięki - zdobywanie kulek, eksplozja po przegranej
    public Text pointsText;  // tekst w lewym górnym rogu gry pokazujący liczbę zdobytych punktów
    public int points;       // liczba zdobytych punktów
    public AudioClip collectSound;
    public AudioClip explodeSound;
    private AudioSource src;

    void Awake()
    {
        src = GetComponent<AudioSource>();
    }

    // gdy gracz zderzy się z kulką, włączy się dźwięk zebrania kulki, kulka zniknie i dodany zostanie 1 punkt (potworki nie zderzają się z kulkami; kulki są Triggered
    void OnTriggerEnter(Collider collid)
    {
        if (collid.gameObject.tag == "dot")
        {
            src.PlayOneShot(collectSound, 1f);
            Destroy(collid.gameObject);
            points++;
        }
    }

    // gdy gracz zderzy się z potworkiem, włączy się dzwięk eksplozji, gracz zostanie zatrzymany, włączy się klatkowa animacja eksplozji (korutyna), przeciwnik zniknie --
    void OnCollisionEnter(Collision coll)  // -- żeby nie odpalało wybuchu cały czas jak się będzie wciąż pchał (robi się głośno..)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            src.PlayOneShot(explodeSound, 1f);
            Movement.blockMovement = true;
            Movement.rb.velocity = new Vector3(0, 0, 0);
            StartCoroutine(ExplosionCour());
            Destroy(coll.gameObject);
        }
    }

    // aktualizacja tekstu z liczbą punktów. Gdy wszystkie kulki znikną z mapy, włącza się menu zwycięstwa
    void Update()
    {
        pointsText.text = "Points: " + points;

        if(GameObject.FindGameObjectsWithTag("dot").Length == 0)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("You_Won");
            Cursor.visible = true;
        }
    }

    // klatkowa animacja eksplozji po zderzeniu się z potworkiem, włączenie menu przegranej
    public IEnumerator ExplosionCour()
    {
        for(int i = 0; i < Explosion_Place._length; i++)
        {
            Explosion_Place.imgs[i].SetActive(true);
            yield return new WaitForSeconds(0.1f * Movement.timeSpeed);
            if(i != Explosion_Place._length - 1)    // ostatnia klatka się nie wyłączy, ale zostanie na końcu
            {
                Explosion_Place.imgs[i].SetActive(false);
            }
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("You_Lost");
        Cursor.visible = true;
    }
}